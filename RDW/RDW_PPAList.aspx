<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_PPAList.aspx.cs" Inherits="RDW_RDW_PPAList" %>

<!DOCTYPE html>

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
        <div align="Center">
            <table width="1100px">
                <tr>
                    
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="PPA:"
                            Width="80px"></asp:Label>
                        <asp:TextBox runat="server" ID="txt_ppa" Width="200px"></asp:TextBox>

                        <asp:Button runat="server" ID="bnt_query" Text="Query" CssClass="SmallButton2" OnClick="bnt_query_Click" />&nbsp;
                        <asp:Button runat="server" ID="btn_export" Text="Export" CssClass="SmallButton2" OnClick="btn_export_Click" />&nbsp;
                        <asp:Button runat="server" ID="btn_new" Text="New" CssClass="SmallButton2" OnClick="btn_new_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="ppa_mstrID,canDelete"
                PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />

            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
