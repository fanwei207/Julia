<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_typeManage.aspx.cs" Inherits="part_Npart_typeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div align="center">
                <table>
                    <tr>
                          <td>
                              模板名称：
                          </td>
                        <td align="left" style="width:250px">
                            <asp:TextBox ID="txtTypeName" runat ="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID ="btnSearch" Text="查询" runat="server" CssClass="SmallButton2" Width="50px" OnClick="btnSearch_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button ID ="btnAdd" Text="新增" runat="server" CssClass="SmallButton2" Width="50px" OnClick="btnAdd_Click"/>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">
                              <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" AllowPaging="true"
                    Width="800px" DataKeyNames="partTypeID,isCanUpdate" OnRowDataBound="gvInfo_RowDataBound" OnRowCommand="gvInfo_RowCommand" PageSize ="30" OnPageIndexChanging="gvInfo_PageIndexChanging">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="模板名称" DataField="partTypeName">
                            <HeaderStyle Width="200px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="创建时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="创建人" DataField="createdName">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="初次使用时间" DataField="modifiedDate" DataFormatString="{0:yyyy-MM-dd}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="初次使用人" DataField="modifiedName">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="编辑">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtnEdit" runat="server" CommandName="lkbtnEdit" CommandArgument='<%# Eval("partTypeID") %>'
                                    Text="编辑"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="删除">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                               <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("partTypeID") %>'
                                    Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                    </Columns>


                </asp:GridView>
                              </td>
                    </tr>
                    <tr>
                        <td colspan ="3" style="color:red" align="left">
                                * 当模板被申请后将不能被删除
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
