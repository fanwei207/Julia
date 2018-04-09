<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_purSearchdet.aspx.cs" Inherits="Purchase_RP_purSearchdet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">

        <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" Text="Back" OnClick="btnback_Click" />
       
     <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames=""                      
                        AllowPaging="False" PageSize="20" OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
                         OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="QAD" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="130px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单位" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="价格" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述1" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述2" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="用途" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="描述" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="规格" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="送货单号" >
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_nbr" runat="server" CssClass="SmallTextBox CCPPart cssQAD"  Text='<%# Bind("prd_nbr") %>'
                                    Width="65px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="65px" />
                                <ItemTemplate>
                                    <%#Eval("prd_nbr")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="采购单">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_po_nbr" runat="server" CssClass="SmallTextBox Supplier cssVend" Text='<%# Bind("prd_po_nbr") %>'
                                    Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <%#Eval("prd_po_nbr")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="行号">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_line" runat="server" CssClass="SmallTextBox cssVendName" Text='<%# Bind("prd_line") %>'
                                    Width="45px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="45px" />
                                <ItemTemplate>
                                    <%#Eval("prd_line")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_Um" HeaderText="单位" ReadOnly="True">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="QAD">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_part" runat="server" CssClass="SmallTextBox cssUm" Text='<%# Bind("prd_part") %>'
                                    Width="30px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <%#Eval("prd_part")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="prd_xpart" HeaderText="描述" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="rp_Price" HeaderText="价格" ReadOnly="True">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="单位">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_um" runat="server" CssClass="SmallTextBox cssPrice" Text='<%# Bind("prd_um") %>'
                                    Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <%#Eval("prd_um")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="订单数量">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_qty_ord" runat="server" CssClass="SmallTextBox cssQADDesc1" Text='<%# Bind("prd_qty_ord") %>'
                                    Width="90px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <%#Eval("prd_qty_ord")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_QADDesc2" HeaderText="物料描述2" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="送货数量">
                                <EditItemTemplate>
                                <asp:TextBox ID="prd_qty_dev" runat="server" CssClass="SmallTextBox cssQADDesc2" Text='<%# Bind("prd_qty_dev") %>'
                                    Width="90px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <%#Eval("prd_qty_dev")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="prd_box_ent" HeaderText="箱数" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createDate" HeaderText="创建时间" ReadOnly="True">
                                <HeaderStyle Width="110px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                            </asp:BoundField>
                         
                        </Columns>
                    </asp:GridView>



        <asp:GridView ID="dtgList" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="21"
                    OnRowDataBound="dtgList_RowDataBound" OnPageIndexChanging="PageChange" OnRowCommand="dtgList_RowCommand"
                    DataKeyNames="po_nbr">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <Columns>
                        <asp:BoundField DataField="prh_receiver" HeaderText="送货单">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_nbr" HeaderText="采购单">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_vend" HeaderText="供应商">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_ship" HeaderText="地点">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_domain" HeaderText="域">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_ord_date" HeaderText="订货日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_due_date" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prh_crt_date" HeaderText="生成日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prh_appv" HeaderText="验收状态" 
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkdetail" Text="详细" ForeColor="Black" Font-Size="12px" Font-Underline="true" runat="server"
                                    CommandArgument='<%# Eval("prh_receiver") + "," + Eval("po_nbr") + "," + Eval("po_domain")%>' CommandName="det" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            <asp:GridView ID="gvdet" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowSorting="True" AllowPaging="True" PageSize="20" 
                        AutoGenerateColumns="False" OnRowDataBound="dtgList_RowDataBound"
                        DataKeyNames="isExists,prd_factory,pt_part,prd_part,prd_qty_sum,prd_qty_ord,prd_line,prd_qty_dev" 
                        OnRowCommand="dtgList_RowCommand" 
                        onpageindexchanging="dtgList_PageIndexChanging" Width="1000px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                          
                            <asp:BoundField DataField="prd_line" HeaderText="行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_part" HeaderText="物料编码">
                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_xpart" HeaderText="品名/规格/型号">
                                <HeaderStyle Width="270px" HorizontalAlign="Center" />
                                <ItemStyle Width="270px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_um" HeaderText="单位">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_ord" HeaderText="订单数">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_short" HeaderText="欠交数">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_sum" HeaderText="已送出">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="prd_qty_dev" HeaderText="本次送货数量">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_appv" HeaderText="验收状态">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
    </div>
    </form>
</body>
</html>