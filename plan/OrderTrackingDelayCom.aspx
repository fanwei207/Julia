<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderTrackingDelayCom.aspx.cs" Inherits="EDI_OrderTrackingDelayCom" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <table>
    <tr>
        <td>
            编号:<asp:TextBox ID="txtItem" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="100px"></asp:TextBox>
        </td>
        <td>
             原因：<asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="100px"></asp:TextBox>
        </td>
        <td>
           <asp:Button ID="btnupdate" runat="server" CssClass="SmallButton2" Text="新增" Width="70px" OnClick="btnupdate_Click" />
        </td>
    </tr>
    </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" Width="300px"
                            CssClass="GridViewStyle" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowDataBound="gvlist_RowDataBound" OnRowDeleted="gvlist_RowDeleted" DataKeyNames="Delay_Item" OnRowDeleting="gvlist_RowDeleting">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="编号" DataField="Delay_Item">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="原因" DataField="Delay_name">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                               


                                <asp:CommandField ShowDeleteButton="True" />


                            </Columns>
                        </asp:GridView>
    </div>
         <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
</body>
</html>
