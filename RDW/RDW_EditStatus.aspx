<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_EditStatus.aspx.cs" Inherits="RDW_EditStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table cellspacing="0" cellpadding="0" width="650" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="2">
                        Project Name<asp:TextBox ID="txt_projectName" CssClass="smalltextbox" runat="server" Width="201px" ReadOnly="true"></asp:TextBox>
                        Project Code<asp:TextBox ID="txt_projectCode" CssClass="smalltextbox" runat="server" Width="101px" ReadOnly="true"></asp:TextBox>
                        &nbsp;

                        Status
                        <asp:DropDownList ID="ddl_status" runat="server">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="PROCESS">In Process</asp:ListItem>
                            <asp:ListItem Value="SUSPEND">Suspend</asp:ListItem>
                            <asp:ListItem Value="CANCEL">Cancel</asp:ListItem>
                            <asp:ListItem Value="CLOSE">Close</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="Btn_check" runat="server" CssClass="SmallButton2" Text="Confirm" Width="50"
                            OnClick="Btn_check_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                        Reason
                    </td>
                    <td>
                        <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Width="500px" Height="160px" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                DataKeyNames="RDW_histBy" AllowPaging="True" PageSize="5" OnPageIndexChanging="gv_PageIndexChanging">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>
                    <asp:BoundField DataField="userName" HeaderText="Name">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="histDate" HeaderText="Date">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_Status" HeaderText="Old Status">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_Remark" HeaderText="Remark">
                        <HeaderStyle Width="100px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </form>
    </div>

    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
