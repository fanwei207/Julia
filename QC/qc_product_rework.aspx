<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_rework.aspx.cs" Inherits="QC_qc_product_rework" %>

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
      <div align="left">
      
        <table width="1000" class="MainContent_inner" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    加工单号:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    ID号:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="122px" AutoPostBack="True"
                        OnTextChanged="txtID_TextChanged"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    生产车间:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFloor" runat="server" CssClass="SmallTextBox" Width="140px" ReadOnly="True"></asp:TextBox>
                </td>
                <td align="left">
                    印刷板号:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtBoard" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    检验日期:
                </td>
                <td align="left" colspan="4">
                    <table style="width: 89%; height: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="137px"></asp:TextBox>
                            </td>
                            <td style="border: 1px solid Black; background-color: silver">
                                --<asp:TextBox ID="txtDate2" runat="server" CssClass="smalltextbox Date" onkeydown="event.returnValue=false;"
                                    onpaste="return false" Width="130px"></asp:TextBox><font color="red">(请在查询时设置)</font>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
                <td>
                    报检人:
                </td>
                <td>
                    <asp:TextBox ID="txtInspector" runat="server" CssClass="SmallTextBox" Width="122px"></asp:TextBox>
                </td>
                <td>
                    QAD号:
                </td>
                <td>
                    <asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" Width="140px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    周期章:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPeriod" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    供应商:
                </td>
                <td>
                    <asp:TextBox ID="txtGuest" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    加工单数量:
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" CssClass="SmallTextBox" Width="122px"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    本次数量:
                </td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    检验员:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtExaminer" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td>
                </td>
                <div runat="server" visible="false">
                    <td>
                        线长:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLineMgt" runat="server" CssClass="SmallTextBox" Width="150px" ></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        过控员:</td>
                    <td>
                        <asp:TextBox ID="txtProcessMgt" runat="server" CssClass="SmallTextBox" 
                            Width="122px" MaxLength="10"></asp:TextBox>
                    </td>
                </div>
                <td>
                    <asp:Label ID="lblOut" runat="server" Text="外厂:" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropOut" runat="server" Width="70px" Visible="False">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropLine" runat="server" DataTextField="lnd_line" DataValueField="lnd_line"
                        Width="70px" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    备注:
                </td>
                <td align="left" colspan="7">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallTextBox" Width="428px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Visible="True"
                        Width="34px" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="添加" Visible="True"
                        Width="34px" OnClick="btnAdd_Click" /><asp:Button ID="btnClear" runat="server" CssClass="SmallButton3"
                            Text="清空" Visible="True" Width="34px" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <asp:CheckBox ID="chkTcp" runat="server" Visible="False" />
                    <asp:CheckBox ID="chkOut" runat="server" Visible="False" />
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CellPadding="1"
                        GridLines="Vertical" TabIndex="5" Width="1510px" OnRowDataBound="gvProduct_RowDataBound"
                        DataKeyNames="prdID,prdOrder,prdLine,prdqad,wo_qty_ord" OnRowCancelingEdit="gvProduct_RowCancelingEdit"
                        OnRowDeleting="gvProduct_RowDeleting" OnRowEditing="gvProduct_RowEditing" OnRowUpdating="gvProduct_RowUpdating"
                        OnRowCommand="gvProduct_RowCommand" ShowFooter="True" CssClass="GridViewStyle AutoPageSize">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <HeaderStyle Width="20px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="加工单号">
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%# Eval("prdOrder") %>
                                </ItemTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID号">
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <%# Eval("prdLine") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tGuest" runat="server" Text='<%# Bind("prdGuest") %>' Width="55px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <%# Eval("prdGuest") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="生产车间">
                                <ControlStyle Font-Bold="False" />
                                <ItemStyle Font-Bold="False" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <%# Eval("prdFloor") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="印刷板号">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tBoard" runat="server" Text='<%# Bind("prdBoard") %>' Width="55px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <%# Eval("prdBoard") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="检验日期">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tDate" runat="server" Text='<%# Bind("prdDate") %>' Width="70px"
                                        onkeydown="event.returnValue=false;" onpaste="return false" CssClass="smalltextbox Date"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%# Eval("prdDate", "{0:yyyy-MM-dd}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="报检人">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tInspector" runat="server" Text='<%# Bind("prdInspector") %>' Width="40px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <%# Eval("prdInspector") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="检验员">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tExaminer" runat="server" Text='<%# Bind("prdExaminer") %>' Width="40px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <%# Eval("prdExaminer")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD号">
                                <EditItemTemplate>
                                    <%# Eval("prdqad") %>
                                </EditItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%# Eval("prdqad") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="周期章">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tPeriod" runat="server" Text='<%# Bind("prdPeriod") %>' Width="45px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemTemplate>
                                    <%# Eval("prdPeriod") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="加工单数量">
                                <HeaderStyle Width="75px" />
                                <ItemTemplate>
                                    <%# Eval("wo_qty_ord", "{0:N0}")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="tOrderNum" runat="server" Text='<%# Bind("wo_qty_ord", "{0:N0}") %>'
                                        Width="60px"></asp:Label></label>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="本次数量">
                                <HeaderStyle Width="65px" />
                                <ItemTemplate>
                                    <%# Eval("prdNum", "{0:N0}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="prdLine,prdOrder,prdNum,wo_part,prdID"
                                DataNavigateUrlFormatString="qc_product_input_rework.aspx?line={0}&amp;ord={1}&amp;rec={2}&amp;part={3}&amp;ID={4}"
                                HeaderText="缺陷项目" Text="添加/查看" Target="_blank">
                                <ControlStyle Font-Underline="True" />
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="光色检验">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="add">添加</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="det">查看</asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="线长" Visible="false">
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%# Eval("prdLineMgt") %>
                                </ItemTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="线号">
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%# Eval("prdLineNo")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tRemarks" runat="server" Text='<%# Bind("prdRemarks") %>' Width="360px"
                                        CssClass="SmallTextBox"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%# Eval("prdRemarks") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="370px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:TextBox ID="txtPageIndex" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
                        ID="txtPageCount" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
                            ID="txtIndex" runat="server" Visible="False" Width="49px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </form>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
