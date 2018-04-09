<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_productSupplierBasis.aspx.cs" Inherits="QC_qc_productSupplierBasis" %>

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
        <div>
             加工单号：<asp:Label ID="lbOrder" runat="server" ></asp:Label>
            &nbsp;&nbsp;&nbsp;
            行号：<asp:Label ID="lbLine" runat="server" ></asp:Label>
            &nbsp;&nbsp;&nbsp;
            物料号：<asp:Label ID="lbPart" runat="server" ></asp:Label>
             &nbsp;&nbsp;&nbsp;
            客户号：<asp:Label ID="lbguest" runat="server" ></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text ="返回" OnClick="btnReturn_Click" />
             &nbsp;&nbsp;&nbsp;
        </div>
           <div>
          <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass ="GridViewStyle AutoPageSize"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" DataKeyNames="id"
                        GridLines="Vertical"  Height="20px" PageSize="20"
                        Width="880px" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowCommand="gvlist_RowCommand"   >
                       <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                      <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="800px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="" Width="800px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="prd_nbr" HeaderText="送货单">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_typeName" HeaderText="文档类型">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_doc_filename" HeaderText="名称">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createName" HeaderText="上传者">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_createDate" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("prd_po_vend") + "," + Eval("prd_doc_id") + "," + Eval("prd_path")+ "," + Eval("prd_transferStatus") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="查看"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="prd_doc_desc" HeaderText="文件描述">
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Left" Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_transferStatus" HeaderText="转移状态">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
               </div>
    </div>
    </form>
     <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
