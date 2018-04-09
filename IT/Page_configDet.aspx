<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_configDet.aspx.cs" Inherits="IT_Page_configDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td colspan="7" style="height:20px;"></td>
            </tr>
            <tr>
                <td style="color:red;">PageID:</td>
                <td>
                     <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
                <td style="color:red;">数据库名：</td>
                <td>
                     <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td style="widows:30px;"></td>
                <td style="color:red;">表名：</td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
                <td style=" widows:70px;"></td>
                <td>
                    <asp:Button ID="btnSave" runat="server"  CssClass="SmallButton2" Text="保存至Page_Det" OnClick="btnSave_Click" Width="95px" />
                </td>
            </tr>
            <tr>
                <td colspan="8">自定义保存：<asp:TextBox ID="txtSaveProc" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    自定义编辑：<asp:TextBox ID="txtEditProc" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;
                    批量导入验证：<asp:TextBox ID="txtValidateProc" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;
                </td>                
                <td>
                    <asp:Button ID="btnConfig" runat="server"  CssClass="SmallButton2" Text="配置并保存至Page_Det" OnClick="btnConfig_Click"  Width="125px" />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    自定义删除：<asp:TextBox ID="txtDelProc" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    自定义导入：<asp:TextBox ID="txtImportPorc" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
        DataKeyNames="isnullable,pd_isImport,pd_isExport,pd_import_index" OnRowDataBound="gv_RowDataBound">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="列名" DataField="ColName">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="数据类型" DataField="TypeName">
                <HeaderStyle Width="90px" />
                <ItemStyle Width="90px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="长度" DataField="length">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID ="key" Width="30px"  Text='<%# Eval("key")%>' ></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <HeaderTemplate>
                    <Label>主键</Label>
                </HeaderTemplate>
            </asp:TemplateField>           
            <asp:BoundField HeaderText="自增长" DataField="identity">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="NULL" DataField="isnullable">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="默认值" DataField="default">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkImport" runat="server" Checked='<%# Convert.ToBoolean(Eval("pd_isImport")) %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                <HeaderTemplate>
                    <label>可导入</label>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID ="importIndex" Width="30px"  Text='<%# Eval("pd_import_index")%>' ></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <HeaderTemplate>
                    <Label>导入顺序</Label>
                </HeaderTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkExport" runat="server" Checked='<%# Convert.ToBoolean(Eval("pd_isExport")) %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                <HeaderTemplate>
                    <label>可导出</label>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox runat="server"  CssClass="td"  ID ="exportIndex" Width="30px"  Text='<%# Eval("pd_export_index")%>' ></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <HeaderTemplate>
                    <Label>导出顺序</Label>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox runat="server"  CssClass="td"  ID ="orderbyIndex"  Width="30px"  Text='<%# Eval("pd_orderby_index")%>' ></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <HeaderTemplate>
                    <Label>字段排序</Label>
                </HeaderTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
