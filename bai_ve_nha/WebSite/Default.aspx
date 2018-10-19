<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Bài toán:</h1>
        <asp:Label ID = "Label1" runat = "server">Nội dung đề bài</asp:Label>
        <h4>Chạy thử  </h4>
        <asp:TextBox ID = "TextBox1" runat = "server"></asp:TextBox>
        <asp:Button ID = "Button1" runat = "server" Text = "Khởi tạo G và giải" />
        <br/>
        <asp:Label ID = "Label2" runat = "server" ></asp:Label>
        
        
        
    
    </div>
    </form>
</body>
</html>
