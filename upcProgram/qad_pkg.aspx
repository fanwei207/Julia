<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qad_pkg.aspx.cs" Inherits="qad_pkg" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#gv_hac").toSuperTable({ width: "1000px", height: "460px", fixedCols: 7, headerRows: 1 })
        .find("tr:even").addClass("altRow");
        });
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td style="height: 23px; width: 953px;">
                    ��ƷQAD:<asp:TextBox ID="txtPar" runat="server" MaxLength="18" TabIndex="1" Width="110px"
                        CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;��Ʒ����:<asp:TextBox ID="txtParCode" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; �ͻ����:<asp:TextBox ID="txtParItemNumber" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp; �Ӽ�QAD:<asp:TextBox ID="txtComp" runat="server" MaxLength="18"
                        TabIndex="1" Width="110px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; �Ӽ�����:<asp:TextBox ID="txtCompCode" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="��ѯ"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_qad_pkg" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" OnPageIndexChanging="gv_qad_pkg_PageIndexChanging"
            PageSize="23" Width="2050px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="par" HeaderText="��ƷQAD">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_desc" HeaderText="����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_code" HeaderText="��Ʒ����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_itemNumber" HeaderText="�ͻ����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp" HeaderText="�Ӽ�QAD">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp_code" HeaderText="�Ӽ�����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="upc" HeaderText="UPC">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="ipi" HeaderText="����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="mpi" HeaderText="����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp_desc" HeaderText="�Ӽ�����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
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
