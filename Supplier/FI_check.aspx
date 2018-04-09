<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FI_check.aspx.cs" Inherits="Supplier_FI_check" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
           
            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15"  DataKeyNames="" Visible="False">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle"  VerticalAlign="Top" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="各单位责任" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="部门">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="FI_real" HeaderText="去实地">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="评价" DataField="FI_desc">
                                <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="是否通过" DataField="FI_use">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="备注" DataField="FI_remark">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             
                             <asp:TemplateField HeaderText="验厂人员"><%--17--%>
                        <ItemTemplate>
                            <asp:Label ID ="lblcheck" runat="server"  Font-Size="12px" Text='<%# Bind("FI_Member") %>'></asp:Label>
                            
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                             <asp:BoundField HeaderText="部门主管" DataField="UName">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                               <asp:BoundField HeaderText="评价人" DataField="FI_createname">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             
                        </Columns>
                    </asp:GridView>
           
            <table id ="tablecheck" runat="server">
                <tr>
                    
                          <td colspan ="3" style="vertical-align:top; text-align:left;">
                        <asp:Label ID="lbldepart" runat="server" Text="Label" Font-Italic="False" Font-Size="15pt"  ></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                  
                    <td colspan ="3">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        结论：是否可以使用
                        

                    </td>
                     <td style="text-align: left;">
                        <asp:RadioButtonList ID="rbtnuse" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                            <asp:ListItem Value="2">其他</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                     <td style="text-align: left;">
                        <asp:TextBox ID="txtother" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        备注：</td>
                    <td style="text-align: left;" colspan="2">
                         <asp:TextBox ID="txtremark" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                      <td style="text-align: right;">
                        评价人：</td>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lblcreateby" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan ="3" style="text-align: center;">
                       
                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="SmallButton3" OnClick="btnsave_Click"/>
                        <asp:Button ID="btncancle" runat="server" Text="Cancle" CssClass="SmallButton3" OnClick="btncancle_Click" Visible="False"/>
                    </td>
                </tr>
            </table>
            <table>
                <tr>

                    <td>
                         <asp:Button ID="btnback" runat="server" Text="Back" CssClass="SmallButton3" OnClick="btnback_Click" Visible="False"/>
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
