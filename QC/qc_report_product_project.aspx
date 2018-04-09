<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report_product_project.aspx.cs" Inherits="QC_qc_report_product_project" %>

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
            <asp:Button ID="btnPass" runat="server" CssClass="SmallButton2" Text ="通过" OnClick="btnPass_Click"  />
             &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFail" runat="server" CssClass="SmallButton2" Text ="不通过" OnClick="btnFail_Click"  />
             &nbsp;&nbsp;&nbsp;
            

        </div>
        <div>
            <hr />
            TCP质量检测报告
        </div>
        <div>
              <div>
                            <input id="fileSize" name="filename" type="file" runat="server"
                                style="width: 400px; height: 24px;" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton2" OnClick="btnUpload_Click" />
                        </div>
                        <div>
                             <asp:GridView ID="gvSize" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false" DataKeyNames="importID,URL" OnPageIndexChanging="gvSize_PageIndexChanging" OnRowCommand="gvSize_RowCommand" >
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="fileName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("URL") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("importID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                        </div>

        </div>
        <hr />
        <div>
            供应商检测报告
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
