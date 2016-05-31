using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_App_Counter.ServiceReference1;


using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BankBalconyApp
{
    public partial class Form1 : Form
    {
        Service1Client proxy;
        private BindingSource bsOrders;

        public Form1()
        {
            InitializeComponent();

            proxy = new Service1Client();
            bsOrders = new BindingSource();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bsOrders;
            dataGridView1.Columns[0].DataPropertyName = "Email";
            dataGridView1.Columns[1].DataPropertyName = "Name";
            dataGridView1.Columns[2].DataPropertyName = "Type";
            dataGridView1.Columns[3].DataPropertyName = "Quantity";
            dataGridView1.Columns[4].DataPropertyName = "Company";
            dataGridView1.Columns[5].DataPropertyName = "Order_time";
            dataGridView1.Columns[6].DataPropertyName = "Order_execution_time";
            dataGridView1.Columns[7].DataPropertyName = "Executed";
            dataGridView1.Columns[8].DataPropertyName = "Quotation";

        }

        private bool isEmail(string str)
        {
            return Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private void btBuy_Click(object sender, EventArgs e)
        {
            order("buy");
        }

        private void btSell_Click(object sender, EventArgs e)
        {
            order("sell");
        }

        private void order(string type)
        {
            progressBar2.Visible = true;
            Application.DoEvents();
            btBuy.Enabled = false;
            btSell.Enabled = false;
            if (insert_email.Text == null || insert_email.Text == "" || !isEmail(insert_email.Text) ||
               insert_name.Text == null || insert_name.Text == "" ||
               insert_quantity.Value == null || insert_quantity.Value < 1 ||
               insert_company.Text == null || insert_company.Text == "")
            {

                progressBar2.Visible = false;
                Application.DoEvents();
                btBuy.Enabled = true;
                btSell.Enabled = true;
                MessageBox.Show("Missing order parameters.");
                return;
            }

            bool res = proxy.addOrder(insert_email.Text, insert_name.Text, type, (int)insert_quantity.Value, insert_company.Text);

            progressBar2.Visible = false;
            Application.DoEvents();
            btBuy.Enabled = true;
            btSell.Enabled = true;
           

            if (res)
                MessageBox.Show("Order added sucessfully.");
            else
                MessageBox.Show("Failed to add order.");
        }

        private void btConsult_Click(object sender, EventArgs e)
        {
            bsOrders.Clear();
            btConsult.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();
            var orders = proxy.getOrders(textBox4.Text);
            if (orders == null || orders.Length == 0)
            {
                progressBar1.Visible = false;
                btConsult.Enabled = true;
                Application.DoEvents();
                MessageBox.Show("No orders found for the specified email.");
                return;
            }

            foreach (Order order in proxy.getOrders(textBox4.Text))
            {
                bsOrders.Add(order);
            }
            progressBar1.Visible = false;
            btConsult.Enabled = true;
            Application.DoEvents();
        }

     
    }
}
