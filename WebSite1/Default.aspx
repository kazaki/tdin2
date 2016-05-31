<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid black">
    
        <br />
        &nbsp;&nbsp;&nbsp;
        Client Email:
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
        Client Name:
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
        Quantity:
        <asp:TextBox ID="TextBox4" runat="server" Width="55px"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
        Company:
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Buy" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Sell" />
        <br />
        <br />
    
    </div>
        <br />
    <div style="border: 1px solid black">
    
        <br />
&nbsp;&nbsp;&nbsp;
        Client Email:
        <asp:TextBox ID="consult_email" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="btConsult_Click" Text="Consult" />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;
        <asp:Table style="margin-left:15px;" ID="consult_table" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" CellPadding="5" CellSpacing="2" GridLines="Both">
        </asp:Table>
        <br />
    
        <br />
    
    </div>
    </form>
</body>
</html>
