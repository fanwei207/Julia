<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_partTypeEnumManager.aspx.cs" Inherits="part_Npart_partTypeEnumManager" %>

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
                <asp:HiddenField ID="hidMstrID" runat="server" />
                <asp:HiddenField ID="hidDetId" runat="server" />
                <table>
                    <tr>
                        <td style="width: 75PX">枚举值：
                        </td>
                        <td style="width: 200PX">
                            <asp:TextBox ID="txtEnum" runat="server" CssClass=" SmallTextBox5" Width="200px"></asp:TextBox>
                        </td >
                        <td style="width: 75PX">替换值：
                        </td>
                        <td style="width: 200PX">
                            <asp:TextBox ID="txtReplace" runat="server" CssClass=" SmallTextBox5" Width="200px"></asp:TextBox>
                        </td >
                        <td style="width: 275PX" colspan="2">
                              <asp:Button ID="btnAdd" runat="server" Width="50px" CssClass=" SmallButton2" Text="新增" OnClick="btnAdd_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Width="50px" CssClass=" SmallButton2" Text="清空" OnClick="btnClear_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>


                    </tr>
                    
                    <tr>
                        <td colspan="8">
                            <asp:GridView ID="gvDet" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="GridViewStyle " DataKeyNames="ID,isCanUpdate" OnRowCommand="gvDet_RowCommand" OnRowDataBound="gvDet_RowDataBound">
                                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                    <asp:BoundField HeaderText="枚举值" DataField="partEnumValue">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="替换值" DataField="partReplaceEnumValue">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>删除</HeaderTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" Font-Underline="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbDelete" runat="server" Text="删除" CommandName="lkbDelete" CommandArgument="ID" Font-Underline="true"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                </Columns>
                            </asp:GridView>
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
