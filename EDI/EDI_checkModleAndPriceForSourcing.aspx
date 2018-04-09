<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI_checkModleAndPriceForSourcing.aspx.cs" Inherits="EDI_EDI_checkModleAndPriceForSourcing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">

    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {


            var _index = $("#hidTabIndex").val();

            var $tabs = $("#divTabs").tabs({ active: _index });
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <div id="divTabs">
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;Price&nbsp;&nbsp;</a></li>
                    <li><a href="#tabs-2">&nbsp;&nbsp;Modle&nbsp;&nbsp;</a></li>
                    <%--<li><a id="ecnProcess" href="#">&nbsp;PROCESS&nbsp;</a></li>--%>
                    <li>
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        <%--<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" Width="80px" />
                       
                        <input id="hidEmailAddress" type="hidden" value="" runat="server" />--%>
                    </li>
                </ul>

                <div id="tabs-1" style="margin-top: 10px;" align="center">
                    <asp:DataGrid ID="gvPrice" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle ">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="LEVEL" ReadOnly="True" HeaderText="level">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_comp" ReadOnly="True" HeaderText="Part">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="pt_um" ReadOnly="True" HeaderText="um">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_qty_per" ReadOnly="True" HeaderText="qty">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="qty" ReadOnly="True" HeaderText="OrderQty">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="pt_pur_lead" ReadOnly="True" HeaderText="Lead Time">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPTION" ReadOnly="True" HeaderText="Description">
                                <HeaderStyle Width="500px"></HeaderStyle>
                                <ItemStyle Width="500px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>

                        </Columns>
                    </asp:DataGrid>


                </div>
                <div id="tabs-2">

                    <asp:DataGrid ID="gvModle" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="LEVEL" ReadOnly="True" HeaderText="level">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_comp" ReadOnly="True" HeaderText="Part">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="pt_um" ReadOnly="True" HeaderText="um">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_qty_per" ReadOnly="True" HeaderText="qty">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="qty" ReadOnly="True" HeaderText="OrderQty">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle Width="90px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="pt_pur_lead" ReadOnly="True" HeaderText="Lead Time">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPTION" ReadOnly="True" HeaderText="Description">
                                <HeaderStyle Width="500px"></HeaderStyle>
                                <ItemStyle Width="500px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>

                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
