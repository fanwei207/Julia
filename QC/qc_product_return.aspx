<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_return.aspx.cs" Inherits="QC_qc_product_return" %>

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
    <div>
<%--        <label >加工单号：<asp:Label runat="server" ID="lbWoNbr"></asp:Label></label>--%>
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" Width="80px" Text="新建"  Enabled ="false" style="left:90px" OnClick="btnNew_Click" />
        <div>
                                <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                Width="800px" CssClass="GridViewStyle " PageSize="20" 
                                OnRowCommand="gvUpload_RowCommand" 
                                DataKeyNames="qprs_ID,qprs_result" OnPageIndexChanging="gvUpload_PageIndexChanging">
                                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                                <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <EmptyDataTemplate>
                                    <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                        <asp:TableRow>
                                            <asp:TableCell Text="序号" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="审批文档" Width="200px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="质量报告" Width="200px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="结果" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="文档上传人" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="文档上传时间" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="质量报告上传人" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="质量报告上传时间" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="创建时间" Width="80px" HorizontalAlign="center"></asp:TableCell>

                                      
                                        </asp:TableRow>
                                    </asp:Table>
                                </EmptyDataTemplate>
                                <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnNo" runat="server" CommandName="lkbtnNo" CommandArgument='<%# Eval("qprs_fileUrl") %>'
                                        Text='<%#Container.DataItemIndex+1%> '></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="返工单">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnFile" runat="server" CommandName="lkbtnFile" CommandArgument='<%# Eval("qprs_fileUrl") %>'
                                        Text='<%# Eval("qprs_fileFileName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                      <asp:TemplateField HeaderText="质量报告">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnReport" runat="server" CommandName="lkbtnReport" CommandArgument='<%# Eval("qprs_reportUrl") %>'
                                        Text='<%# Eval("qprs_reportFileName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="结果">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnResult" runat="server" CommandName="lkbtnResult" CommandArgument='<%# Eval("qprs_reportUrl") %>'
                                        Text='<%# Eval("qprs_result") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                      <asp:BoundField DataField="qprs_fileCreatedName" HeaderText="返工单上传人">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="qprs_fileCreatedDate" HeaderText="返工单上传时间" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="qprs_reportCreatedName" HeaderText="质量报告上传人">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="qprs_reportCreatedDate" HeaderText="质量报告上传时间" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                      <%--<asp:BoundField DataField="createdDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("qprs_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
