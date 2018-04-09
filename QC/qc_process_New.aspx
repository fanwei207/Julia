<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_process_New.aspx.cs" Inherits="qc_process_New" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table runat="server" id="tbHeader" cellspacing="0" cellpadding="2" width="1000" class="MainContent_inner">
            <tr>
                <td style="height: 24px">
                    加工单号:
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtWorkOrder" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td style="height: 24px">
                    ID号: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="140px" AutoPostBack="True"
                        OnTextChanged="txtID_TextChanged"></asp:TextBox>
                </td>
                <td style="height: 24px">
                    加工零件:<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox" Width="140px"
                        AutoPostBack="True" OnTextChanged="txtPart_TextChanged"></asp:TextBox>
                </td>
                <td style="height: 24px;">
                    总批量: &nbsp; &nbsp;
                </td>
                <td style="height: 24px;">
                    <asp:TextBox ID="txtOrdered" runat="server" CssClass="SmallTextBox" Width="140px"
                        ReadOnly="True">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    供应商: &nbsp;&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtProvider" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td>
                    批号、编号:
                </td>
                <td>
                    <asp:TextBox ID="txtNO" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td>
                    中间部件:<asp:TextBox ID="txtPartM" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
                <td>
                    生产班组:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkGroup" runat="server" CssClass="SmallTextBox" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    本次投入/抽样数:
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtIn" runat="server" CssClass="SmallTextBox" Width="98px">0</asp:TextBox>
                </td>
                <td style="height: 24px">
                    合格产出/抽样数:
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtOut" runat="server" CssClass="SmallTextBox" Width="106px">0</asp:TextBox>
                </td>
                <td colspan="3" style="border-right: black 1px solid; border-top: black 1px solid;
                    border-left: black 1px solid; border-bottom: black 1px solid; height: 24px; background-color: silver;">
                    检验日期:<asp:TextBox ID="txtDate1" runat="server" CssClass="smalltextbox Date" onkeydown="event.returnValue=false;"
                        onpaste="return false" Width="100px"></asp:TextBox>--<asp:TextBox ID="txtDate2" runat="server"
                            CssClass="smalltextbox Date" Width="100px" onkeydown="event.returnValue=false;"
                            onpaste="return false"></asp:TextBox><font color="red">(请在查询时设置)</font>
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    线长:
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtLineMgt" runat="server" CssClass="SmallTextBox" Width="150px"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td style="height: 24px">
                    &nbsp;
                </td>
                <td style="height: 24px">
                    &nbsp;
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    备注: &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallTextBox"
                        Width="562px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="添加" Visible="True"
                        Width="38px" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Visible="True"
                        Width="38px" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="SmallButton3" OnClick="btnClear_Click"
                        Text="清空" Visible="True" Width="34px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProcess" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="1480px" PageSize="18" OnRowDataBound="gvProcess_RowDataBound" DataKeyNames="prcID"
            OnRowCancelingEdit="gvProcess_RowCancelingEdit" OnRowDeleting="gvProcess_RowDeleting"
            OnRowEditing="gvProcess_RowEditing" OnRowUpdating="gvProcess_RowUpdating" ShowFooter="True">
            <Columns>
                <asp:TemplateField Visible="False">
                    <HeaderStyle Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加工单号">
                    <EditItemTemplate>
                        <%# Eval("prcOrder") %>
                    </EditItemTemplate>
                    <HeaderStyle Width="70px" />
                    <ItemTemplate>
                        <%# Eval("prcOrder") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID号">
                    <EditItemTemplate>
                        <%# Eval("prcLine") %>
                    </EditItemTemplate>
                    <HeaderStyle Width="70px" />
                    <ItemTemplate>
                        <%# Eval("prcLine") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加工零件">
                    <EditItemTemplate>
                        <asp:Label ID="tWoPart" runat="server" Text='<%# Bind("wo_part") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <%# Eval("wo_part") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总批量">
                    <EditItemTemplate>
                        <asp:Label ID="tQty" runat="server" Text='<%# Bind("wo_qty_ord") %>' Width="60px"></asp:Label>
                    </EditItemTemplate>
                    <ControlStyle Font-Bold="False" />
                    <ItemStyle Font-Bold="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemTemplate>
                        <%# Eval("wo_qty_ord", "{0:N0}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商">
                    <EditItemTemplate>
                        <asp:TextBox ID="tGuest" runat="server" Text='<%# Bind("prcProvider") %>' Width="65px"
                            CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="70px" />
                    <ItemTemplate>
                        <%# Eval("prcProvider") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批号编号">
                    <EditItemTemplate>
                        <asp:TextBox ID="tNo" runat="server" Text='<%# Bind("prcNO") %>' Width="65px" CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="120px" />
                    <ItemTemplate>
                        <%# Eval("prcNO") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="投入/抽样数">
                    <EditItemTemplate>
                        <asp:TextBox ID="tIn" runat="server" Text='<%# Bind("prcIn") %>' Width="60px" CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="105px" />
                    <ItemTemplate>
                        <%# Eval("prcIn", "{0:N0}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="产出/抽样数">
                    <EditItemTemplate>
                        <asp:TextBox ID="tOut" runat="server" Text='<%# Bind("prcOut") %>' Width="60px" CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="105px" />
                    <ItemTemplate>
                        <%# Eval("prcOut", "{0:N0}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="中间部件号">
                    <EditItemTemplate>
                        <asp:TextBox ID="tPart" runat="server" Text='<%# Bind("prcPart") %>' Width="60px"
                            CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <%# Eval("prcPart") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="生产班组">
                    <EditItemTemplate>
                        <asp:TextBox ID="tWorkGroup" runat="server" Text='<%# Bind("prcWorkGroup") %>' Width="60px"
                            CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <%# Eval("prcWorkGroup")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="线长">
                    <EditItemTemplate>
                        <asp:TextBox ID="tLineMgt" runat="server" Text='<%# Bind("prcLineMgt") %>' Width="60px"
                            CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <%# Eval("prcLineMgt")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField HeaderText="缺陷项目" Text="添加/查看" DataNavigateUrlFields="prcLine,prcOrder,diff,prcID"
                    DataNavigateUrlFormatString="qc_process_item_New.aspx?line={0}&amp;mar={1}&amp;qty={2}&amp;id={3}"
                    Target="_blank">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="备注">
                    <EditItemTemplate>
                        <asp:TextBox ID="tRemarks" runat="server" Text='<%# Bind("prcRemarks") %>' Width="300px"
                            CssClass="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="500px" />
                    <ItemTemplate>
                        <%# Eval("prcRemarks") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        <asp:TextBox ID="txtPageIndex" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
            ID="txtPageCount" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
                ID="txtIndex" runat="server" Visible="False" Width="49px"></asp:TextBox></form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
