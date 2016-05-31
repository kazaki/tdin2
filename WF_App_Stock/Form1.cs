using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using WF_App_Stock.ServiceReference1;

namespace WF_App_Stock
{
    public partial class Form1 : Form
    {
        Service1Client proxy;
        private BindingSource bsOrders;
        private MessageQueue messageQueue1 = null;
        private static string queueName = @".\private$\tdin2_stock";

        public Form1()
        {
            bsOrders = new BindingSource();
            proxy = new Service1Client();

            InitializeComponent();
            CustomInitializeComponent();

            // check if queue exists, if not create it
            if (!MessageQueue.Exists(queueName)) messageQueue1 = MessageQueue.Create(queueName);
            else messageQueue1 = new MessageQueue(queueName);

            //messageQueue1.MessageReadPropertyFilter.LookupId = true;
            //messageQueue1.SynchronizingObject = this;

            getQueuedMessages();
        }

        private void getQueuedMessages()
        {

            statusStrip1.Text = "";
            try
            {
                messageQueue1.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                System.Messaging.Message[] msqs = messageQueue1.GetAllMessages();
                messageQueue1.Purge();
                foreach (System.Messaging.Message msq in msqs)
                {
                    var message = (Order)msq.Body;
                    bsOrders.Add((Order)message);
                }

                statusStrip1.Text = "Message queue verified.";
            }
            catch (MessageQueueException ex)
            {
                statusStrip1.Text = "";
                statusStrip1.Text = ex.Message;
            }
            catch (Exception ex1)
            {
                statusStrip1.Text = "";
                statusStrip1.Text = ex1.Message;
            }
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            try
            {
                messageQueue1.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                Order o = new Order();
                o.Id = 0;
                o.Email = "email@email.com";
                o.Name = "JOSE";
                o.Type = "buy";
                o.Quantity = 12;
                o.Company = "Google";
                o.Order_time = DateTime.Now;
                //messageQueue1.Send(o, "MeuTitulo");
                messageQueue1.Send(o);
            }
            catch (MessageQueueException ex)
            {
                statusStrip1.Text = "";
                statusStrip1.Text = ex.Message;
            }
            catch (Exception ex1)
            {
                statusStrip1.Text = "";
                statusStrip1.Text = ex1.Message;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getQueuedMessages();
        }
    }
}
