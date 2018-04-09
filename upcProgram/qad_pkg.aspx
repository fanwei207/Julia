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
                    产品QAD:<asp:TextBox ID="txtPar" runat="server" MaxLength="18" TabIndex="1" Width="110px"
                        CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;产品编码:<asp:TextBox ID="txtParCode" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; 客户零件:<asp:TextBox ID="txtParItemNumber" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp; 子件QAD:<asp:TextBox ID="txtComp" runat="server" MaxLength="18"
                        TabIndex="1" Width="110px" CssClass="smalltextbox"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; 子件编码:<asp:TextBox ID="txtCompCode" runat="server" MaxLength="50"
                        TabIndex="1" Width="130px" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="30" CssClass="SmallButton3" Text="查询"
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
                <asp:BoundField DataField="par" HeaderText="产品QAD">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_desc" HeaderText="描述">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_code" HeaderText="产品编码">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="par_itemNumber" HeaderText="客户零件">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp" HeaderText="子件QAD">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp_code" HeaderText="子件编码">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="upc" HeaderText="UPC">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="ipi" HeaderText="中箱">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="mpi" HeaderText="外箱">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="comp_desc" HeaderText="子件描述">
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
