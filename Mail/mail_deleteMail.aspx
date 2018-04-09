<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail_deleteMail.aspx.cs" Inherits="Mail_mail_deleteMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table width="700px">
    <tr>
    <td>
    </td>
    <td>
        <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" 
            onclick="btnBack_Click" />
    </td>
    </tr>
    <tr>
    <td style="height:10px">
    </td>
    </tr>
    <tr>
    <td>
       日期: <asp:TextBox ID="txtBdate" runat="server" 
            CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>--
        <asp:TextBox ID="txtEdate" runat="server" CssClass="SmallTextBox Date" 
            Width="120px"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="SmallButton3" 
            onclick="btnDelete_Click" />
    </td>
        </tr>
    </table>
    <asp:GridView ID="gvMail" runat="server" AutoGenerateColumns="False" 
        CssClass="GridViewStyle">
        <Columns>
        <asp:BoundField DataField="ad_addr" HeaderText="发件人">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
        <asp:BoundField DataField="ad_addr" HeaderText="主题">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
        <asp:BoundField DataField="ad_addr" HeaderText="Cust Code">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
        <asp:BoundField DataField="ad_addr" HeaderText="Cust Code">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
        </Columns>
    </asp:GridView>
    </div>
    
    </form>
</body>
</html>
