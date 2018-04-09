<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_selectBasis.aspx.cs" Inherits="price_pcm_selectBasis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        //        $(function () {
        //            $("#btnReturn").click(window.close());
        //        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td align="left">
                    申请单号：&nbsp;<asp:Label ID="lbIMID" runat="server"></asp:Label>
                  
                </td>

                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvBasisInfo" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="15" AutoGenerateColumns="false" 
                        onpageindexchanging="gvBasisInfo_PageIndexChanging" 
                        onrowcommand="gvBasisInfo_RowCommand" onrowdatabound="gvBasisInfo_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="367px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="类型" Width="67px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="filename">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="类型" DataField="type">
                                <HeaderStyle Width="67px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("url") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdBy">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

