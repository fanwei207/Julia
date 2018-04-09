<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_FinCheckApplyPart.aspx.cs" Inherits="price_pcm_FinCheckApplyPart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.dev.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="gvDet" runat="server" Width="1100px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle"  DataKeyNames="DetId" 
                        EmptyDataText="No data">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="QAD号" DataField="part" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="部件号" DataField="ItemCode" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="供应商" DataField="Vender" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="供应商名称" DataField="VenderName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="指定" DataField="InfoFrom" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单位" DataField="UM" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="币种" DataField="Curr" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="期望价格" DataField="applyPrice" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="期望价格执行时间" DataField="applyPriceStarDate" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="核价" DataField="CheckPrice" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
    </div>
    </form>
</body>
</html>
