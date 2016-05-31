using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_App
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public bool addOrder(string email, string name, string type, int quantity, string company)
        {
            SqlConnection conn = new SqlConnection("Data Source=" + Environment.MachineName + "\\SQLEXPRESS;Initial Catalog=tdin_bank;" + "Integrated Security=SSPI");
            conn.Open();
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string queryString =
                "INSERT INTO ORDERS OUTPUT INSERTED.ID VALUES('" + email + "','" + name + "','" + type + "'," + quantity + ",'" + company + "','" + sqlFormattedDate + "',null,0,null)";
            SqlCommand command = new SqlCommand(queryString, conn);

            // Send order to message queue
            Order o = new Order();
            o.Id = (Int32)command.ExecuteScalar();
            o.Email = email;
            o.Name = name;
            o.Type = type;
            o.Quantity = quantity;
            o.Company = company;
            o.Order_time = myDateTime;
            putOrderOnQeue(o);

            return true;
        }

        public List<Order> getOrders(string email)
        {
            List<Order> orders = new List<Order>();

            SqlConnection conn = new SqlConnection("Data Source=" + Environment.MachineName + "\\SQLEXPRESS;Initial Catalog=tdin_bank;" + "Integrated Security=SSPI");
            conn.Open();
            string queryString = "SELECT * FROM ORDERS WHERE client_email = '" + email + "'";
            SqlCommand command = new SqlCommand(queryString, conn);

            using (command)
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order o = new Order();
                    o.Id = reader.GetInt32(0);
                    o.Email = reader.GetString(1);
                    o.Name = reader.GetString(2);
                    o.Type = reader.GetString(3);
                    o.Quantity = reader.GetInt32(4);
                    o.Company = reader.GetString(5);
                    o.Order_time = reader.GetDateTime(6);
                    if (reader.IsDBNull(7)) o.Order_execution_time = null;
                    else o.Order_execution_time = reader.GetDateTime(7);
                    o.Executed = reader.GetBoolean(8);
                    if (reader.IsDBNull(9)) o.Quotation = null;
                    else o.Quotation = reader.GetDecimal(9);

                    orders.Add(o);
                }
            }

            return orders;
        }

        private void putOrderOnQeue(Order o)
        {
            MessageQueue messageQueue1 = null;
            string queueName = @".\private$\tdin2_stock";

            // check if queue exists, if not create it
            if (!MessageQueue.Exists(queueName)) messageQueue1 = MessageQueue.Create(queueName);
            else messageQueue1 = new MessageQueue(queueName);

            try
            {
                messageQueue1.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                messageQueue1.Send(o);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
