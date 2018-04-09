<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail_checkPo.aspx.cs" Inherits="mail_checkPo" validateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link media="all" href="../../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            // $("body", parent.parent.document).find("#divLoading").hide();
            $.loading("none");
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table>
    <tr>
    <td>
    采购单:<asp:TextBox ID="txtOrder" runat="server" Width="120px"></asp:TextBox>
    </td>
    <td>
    行号:<asp:TextBox ID="txtLine" runat="server" Width="120px"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnTrack" runat="server" Text="跟踪" CssClass="SmallButton3" 
            onclick="btnTrack_Click" />
    </td>
    </tr>
    </table>
    <asp:GridView ID="gvStatus" runat="server" width="400px" CssClass="GridViewStyle" 
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
             <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="400px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="跟踪内容" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="结果" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
            <asp:BoundField DataField="title" HeaderText="跟踪内容">
                    <HeaderStyle Width="300px" HorizontalAlign="center" />
                    <ItemStyle Width="300px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="result" HeaderText="结果">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
