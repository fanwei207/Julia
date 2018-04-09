<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.addpart" CodeFile="addpart.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 69px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0">
           <tr>
             <td style=" vertical-align:top;">
               <div style=" width:500px;">
                  <fieldset>
                     <legend>基础信息：</legend>
                     <table>
                        <tr style="height: 10px;">
                          <td align="right" class="style1">
                          部件号:
                          </td>
                          <td align="left" style="width: 150px;">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="150px"></asp:TextBox>
                          </td>
                          <td align="right" style="width: 60px;">
                              QAD:
                          </td>
                          <td align="left" style="width: 150px;">
                          <asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" MaxLength="16"
                           Width="150px"></asp:TextBox>
                              <input id="hidQad" type="hidden" runat="server" />
                          </td> 
                       </tr>
                        <tr style="height: 10px;">
                          <td align="right" class="style1">
                    最小库存量:</td>
                          <td align="left" colspan="2">
                    <asp:TextBox ID="txtMinQty" runat="server" CssClass="SmallTextBox" MaxLength="20"
                        Width="70px"></asp:TextBox>
                    零表示不设最小库存量</td>
                          <td align="left" style="width: 150px;">
                              &nbsp;</td> 
                       </tr>
                       <tr style="height: 10px;">
                <td valign="top" align="right" class="style1">
                    &nbsp;单位:
                </td>
                <td align="left" valign="top">
                    &nbsp;<asp:TextBox ID="txtUnit" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="150px"></asp:TextBox>
                </td>
                <td valign="top" align="right">
                          分类:
                          </td>
                <td valign="top" align="left">
                          <asp:TextBox ID="txtCategory" runat="server" CssClass="SmallTextBox" MaxLength="50"
                           Width="150px"></asp:TextBox>
            </tr>
            <tr style="height: 10px;">
                <td align="right" class="style1">
                    转换系数:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRate" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="150px"></asp:TextBox>
                </td>
                <td align="right">
                    转换前单位:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTranUnit" runat="server" CssClass="SmallTextBox" MaxLength="20"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 10px;">
                <td align="right" class="style1">
                                        条形码:</td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtmpi" runat="server" CssClass="SmallTextBox" 
                        MaxLength="20" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 10px;">
                <td align="right" class="style1">
                    状态:
                </td>
                <td align="left" colspan="3">
                    <asp:RadioButton ID="radNormal" runat="server" GroupName="radGroup" Enabled="True"
                        Checked="True" Text="使用"></asp:RadioButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" GroupName="radGroup" Enabled="True" Checked="False"
                        Text="试用"></asp:RadioButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" GroupName="radGroup" Enabled="True"
                        Checked="False" Text="停用"></asp:RadioButton>
                </td>
            </tr>
            <tr style="height: 10px;">
                <td align="right" class="style1">
                    描述:
                </td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="60px" Width="410px" Wrap="True"></asp:TextBox>
            </tr>
            <tr style="height: 10px;">
                <td colspan="4" style="height: 15px" align="center">
                    <asp:Button ID="BtnModify" runat="server" CssClass="SmallButton2" Visible="False"
                        Width="50" Text="修改"></asp:Button>&nbsp;
                    <asp:Button ID="BtnAddNew" runat="server" CssClass="SmallButton2" Visible="False"
                        Width="50" Text="添加"></asp:Button>&nbsp;
                    <asp:Button ID="BtnDelete" runat="server" CssClass="SmallButton2" Visible="True"
                        Width="50" Text="删除"></asp:Button>&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton2" CausesValidation="False"
                        Visible="true" Width="50" Text="返回"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
                    </table>
                 </fieldset>
               </div>
               
             </td>
             <td style=" width:3px;"></td>
             <td style=" vertical-align:top;">
                <div>
                  <fieldset>
                     <legend>文档关联信息：</legend>
                     <table>
                        <tr>
                           <td align="left" style="width: 150px;">
                    <asp:GridView ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle" runat="server"
                        Width="450px" DataKeyNames="ptt_id,isExists,isExists150">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="420px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="NO." Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="条目类型" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="产品分类" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="文档数" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Width="30px"><input 
                                            id="Checkbox1" type="checkbox" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="无符合条件的信息" ColumnSpan="5"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="NO.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex +1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="条目类型" DataField="ptt_type" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="产品分类" DataField="ptt_detail" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="文档数">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" Text='<%# Bind("itm_amount") %>' runat="server" Width="100%"
                                        Style="ime-mode: disabled; text-align: right" onkeypress="if ((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46) event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSinger" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="150">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSinger150" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                        </tr>
                     </table>
                  </fieldset>
                </div>
             </td>
           </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
