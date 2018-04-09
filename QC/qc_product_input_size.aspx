<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_input_size.aspx.cs" Inherits="QC_qc_product_input_size" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript">
        $(function () {
            /*
            这里需要做一个按回车转下一个的功能
            */
            //var gv = $("#gvInfo")
            //var txt = gv.children()
            //$(window).keydown(function(event){
            //    if(event.keyCode == 13)
            //    {


            //    }

            //})


            //$("#btnReturn").click(function(){
            //    alert("1");
            //    $("j-object-loading").remove();

            //})
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <tr>
                    <%--误差--%>
                    <td colspan="4" align="left">误差：<asp:TextBox runat="server" ID="txtError" CssClass="SmallTextBox5 Numeric"></asp:TextBox>
                        &nbsp; % &nbsp;
                    <asp:HiddenField ID="hidID" runat="server" />
                    </td>
                    <%--按钮--%>
                    <td colspan="3" align="right">
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3 "
                            Text="保存" Width="40px" OnClick="btnSave_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp;

                    </td>

                </tr>
                <tr>
                    <td colspan="7">
                        <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            OnRowDataBound="gv_RowDataBound" DataKeyNames="ID">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Owner" Width="100%" HorizontalAlign="center">No Data</asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="boxNo" HeaderText="箱号">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" Font-Underline="false" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="长（cm）">
                                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="false" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlong" runat="server" CssClass="SmallTextBox " Text='<%# Eval("long") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽（cm）">
                                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="false" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtwide" runat="server" CssClass="SmallTextBox " Text='<%# Eval("wide") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="高（cm）">
                                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="false" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txthigh" runat="server" CssClass="SmallTextBox " Text='<%# Eval("high") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="质量（kg）">
                                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="false" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtquality" runat="server" CssClass="SmallTextBox " Text='<%# Eval("quality") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>
                    <%--GV--%>
                </tr>
                <tr>
                    <td>平均值：</td>
                    <td>长：<asp:Label ID="lbAvgLong" runat="server"></asp:Label></td>
                    <td>宽：<asp:Label ID="lbAvgWide" runat="server"></asp:Label></td>
                    <td>高：<asp:Label ID="lbAvgHigh" runat="server"></asp:Label></td>
                    <td>体积：<asp:Label ID="lbAvgVolume" runat="server"></asp:Label></td>
                    <td>质量：<asp:Label ID="lbAvgQuality" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
