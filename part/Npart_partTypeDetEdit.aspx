<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_partTypeDetEdit.aspx.cs" Inherits="part_Npart_partTypeDetEdit" %>

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
                <div>
                    <asp:Label ID="lbmstrName" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidMstrID" runat="server" />
                    <asp:HiddenField ID="hidDetID" runat="server" />
                </div>
                <table>
                    <tr>
                        <td >
                            <asp:TextBox ID="txtColName" runat="server" CssClass="SmallTextBox5" Width="75px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtColEnglishName" runat="server" CssClass="SmallTextBox5" Width="75px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrefix" runat="server" CssClass="SmallTextBox5" Width="43px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuffix" runat="server" CssClass="SmallTextBox5" Width="43px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSort" runat="server" CssClass="SmallTextBox5 Numeric" Width="75px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkNumber" runat="server" Width="50px"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDate" runat="server" Width="50px"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSpace" runat="server" Width="50px"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEnum" runat="server" Width="50px"/>
                        </td>
                        
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Width="50px" CssClass=" SmallButton2" Text="新增" OnClick="btnAdd_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Width="50px" CssClass=" SmallButton2" Text="清空" OnClick="btnClear_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Width="50px" CssClass=" SmallButton2" Text="返回" OnClick="btnReturn_Click"  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10" align="left">
                            <asp:GridView ID="gvDet" runat="server" 
                                AutoGenerateColumns="False"
                                CssClass="GridViewStyle " DataKeyNames="ID,colIsCanUpdate,colIsEnum" OnRowCommand="gvDet_RowCommand" OnRowDataBound="gvDet_RowDataBound">
                                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                    <asp:BoundField HeaderText="列名" DataField="colName" >
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="英文名" DataField="colEnglishName" >
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="前缀" DataField="colPrefix">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="后缀" DataField="colSuffix">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                        <asp:BoundField HeaderText="排序" DataField="colSort">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="是数字" DataField="colIsNumber">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="是日期" DataField="colIsDate">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="有空格" DataField="colSpace">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                      <asp:TemplateField>
                                        <HeaderTemplate>是枚举</HeaderTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" ></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbEideEnum" runat="server" Text='<%# Eval("colIsEnum") %>' CommandName="lkbEideEnum" CommandArgument="ID" Font-Underline="true" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     

                                

                                    <asp:TemplateField>
                                        <HeaderTemplate>可修改</HeaderTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" Font-Underline="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbEide" runat="server" Text='<%# Eval("colIsCanUpdate") %>' CommandName="lkbEide" CommandArgument="ID" Font-Underline="true"></asp:LinkButton>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField>
                                        <HeaderTemplate>删除</HeaderTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" Font-Underline="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbDelete" runat="server" Text="删除" CommandName="lkbDelete" CommandArgument="ID" Font-Underline="true"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                           <%--         <asp:BoundField HeaderText="创建人" DataField="createdName" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="创建日期" DataField="createdDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="初次使用者" DataField="modifiedName" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="初次使用日期" DataField="modifiedDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:BoundField>--%>


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
