<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Users.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" Text="DB: " ></asp:Label>
        <asp:TextBox ID="DB_TextBox" runat="server"></asp:TextBox>
        <asp:Label runat="server" Text="User: " style="margin-left: 20px"></asp:Label>
        <asp:TextBox ID="User_TextBox" runat="server"></asp:TextBox>
        <asp:Label runat="server" Text="Pass: " style="margin-left: 20px"></asp:Label>
        <input id="Pass_TextBox" type="password" runat="server" />
        <asp:Button ID="Connection_Button" runat="server" Text="Test Connection" style="margin-left: 20px" OnClick="Connection_Button_Click" />
        <asp:Label ID="Status_Label" runat="server" Text="" style="margin-left: 20px"></asp:Label>
    </div>
    <div style="height:auto; width:auto">
    
        <asp:Table ID="User_Table1" runat="server" Height="100%" Width="100%" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" style="margin-top: 15px" >
            <asp:TableRow >
                <asp:TableCell><asp:Label runat="server" Text="User Name:"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Label runat="server" Text="User Email:"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Label runat="server" Text="User Phone:"></asp:Label></asp:TableCell>          
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="UserName_TextBox" runat="server" width="150px"></asp:TextBox>
                    <asp:Button ID="AddChangeUserName_Button" runat="server" style="margin-left: 20px" Text="Add/Change user name" width="150px" OnClick="AddChangeUserName_Button_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="NewMail_TextBox" runat="server" width="150px"></asp:TextBox>
                    <asp:Button ID="NewMail_Button" runat="server" style="margin-left: 20px" Text="Add new mail" width="150px" OnClick="NewMail_Button_Click"  />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="NewPhone_TextBox" runat="server" width="150px"></asp:TextBox>
                    <asp:Button ID="NewPhone_Button" runat="server" style="margin-left: 20px" Text="Add new phone" width="150px" OnClick="NewPhone_Button_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="User Info:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell ID="User_TableCell" runat="server">
                </asp:TableCell>
                <asp:TableCell>
                     <asp:Button ID="AddRecord_Button" runat="server" Text="Add/Update record" width="150px" OnClick="AddRecord_Button_Click" />
                     <asp:Button ID="DeleteSelected_Button" runat="server" style="margin-left: 20px" Text="Delete selected" width="150px" OnClick="DeleteSelected_Button_Click"  />
                </asp:TableCell>          
            </asp:TableRow>

        </asp:Table>
    
    </div>
    <div>
    
        <asp:DropDownList ID="Users_DropDownList" runat="server" Height="22px" Width="234px" >

        </asp:DropDownList>
    
        <asp:Button ID="LoadUser_Button" runat="server" style="margin-left: 20px" Text="Load selected user" Width="150px" OnClick="LoadUser_Button_Click" />
    </div>
    </form>
</body>
</html>
