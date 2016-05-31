using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    static ServiceReference1.Service1Client proxy;
    static DataTable dt = new DataTable();

    private bool isEmail(string str)
    {
        return Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            proxy = new ServiceReference1.Service1Client();
            initializeTable();
        }
    }

    protected void initializeTable()
    {
        consult_table.Rows.Clear();

        TableCell c1 = new TableCell(); c1.Text = "Email";
        TableCell c2 = new TableCell(); c2.Text = "Name";
        TableCell c3 = new TableCell(); c3.Text = "Type";
        TableCell c4 = new TableCell(); c4.Text = "Quantity";
        TableCell c5 = new TableCell(); c5.Text = "Company";
        TableCell c6 = new TableCell(); c6.Text = "Order Time";
        TableCell c7 = new TableCell(); c7.Text = "Execution Time";
        TableCell c8 = new TableCell(); c8.Text = "Executed";
        TableCell c9 = new TableCell(); c9.Text = "Quotation";

        TableRow row = new TableRow();
        row.BackColor = System.Drawing.Color.LightGray;
        row.Controls.Add(c1);
        row.Controls.Add(c2);
        row.Controls.Add(c3);
        row.Controls.Add(c4);
        row.Controls.Add(c5);
        row.Controls.Add(c6);
        row.Controls.Add(c7);
        row.Controls.Add(c8);
        row.Controls.Add(c9);

        consult_table.Controls.Add(row);
    }

    private string convertNull(string str)
    {
        if (str == null || str == "")
            return "n/a";
        return str;
    }

    protected void btConsult_Click(object sender, EventArgs e)
    {
        initializeTable();

        var orders = proxy.getOrders(consult_email.Text);
        if (orders == null || orders.Length == 0)
        {
            return;
        }

        foreach (WCF_App.Order order in orders)
        {
            TableCell c1 = new TableCell(); c1.Text = order.Email;
            TableCell c2 = new TableCell(); c2.Text = order.Name;
            TableCell c3 = new TableCell(); c3.Text = order.Type;
            TableCell c4 = new TableCell(); c4.Text = order.Quantity.ToString();
            TableCell c5 = new TableCell(); c5.Text = order.Company;
            TableCell c6 = new TableCell(); c6.Text = order.Order_time.ToString();
            TableCell c7 = new TableCell(); c7.Text = convertNull(order.Order_execution_time.ToString());
            TableCell c8 = new TableCell(); c8.Text = order.Executed.ToString();
            TableCell c9 = new TableCell(); c9.Text = convertNull(order.Quotation.ToString());

            TableRow row = new TableRow();
            row.Controls.Add(c1);
            row.Controls.Add(c2);
            row.Controls.Add(c3);
            row.Controls.Add(c4);
            row.Controls.Add(c5);
            row.Controls.Add(c6);
            row.Controls.Add(c7);
            row.Controls.Add(c8);
            row.Controls.Add(c9);

            consult_table.Controls.Add(row);


        }

    }

    protected void btBuy_Click(object sender, EventArgs e)
    {

    }

    protected void btSell_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }


}