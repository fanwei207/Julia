<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleNotesMaintain.aspx.cs"
    Inherits="supplier_SampleNotesMaintain" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr>
                <td class="style5">
                    &nbsp;打样单:
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_bosnbr" runat="server" Width="110px" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    生成日期：<asp:TextBox ID="txt_bosDate" runat="server" Width="149px" ReadOnly="true" CssClass="Date"></asp:TextBox>
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;供应商：
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="ddl_vend" runat="server" Height="22px" Width="249px" DataTextField="ad_name"
                        DataValueField="ad_addr">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trCompleteDate" runat="server">
                <td>完工日期:</td>
                <td>
                    <asp:TextBox ID="txtCompleteDate" runat="server" Enabled="false" CssClass="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;备 注：
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtRmks" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_Vend" runat="server">
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:CheckBox ID="chk_isVendConfirm" runat="server" Text="供应商确认" Enabled="false" />
                    <asp:Label ID="lbl_VendNote" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr id="tr_Receipt" runat="server">
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:CheckBox ID="chk_isReciept" runat="server" Text="已收货" Enabled="false" />
                    <asp:Label ID="lbl_RecpitpNote" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
             <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:Label ID="lblState" runat="server" Text="订单状态：已取消" Visible="false"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button ID="btn_Add" runat="server" Text="增加" OnClick="btn_Add_Click" CssClass="SmallButton2"
                        Width="49px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Save" runat="server" Text="保存" Width="53px" OnClick="btn_Save_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Delete" runat="server" Text="删除" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Delete_Click" OnClientClick="return confirm('确定删除该打样单');" />
                    &nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Cancel_Click" OnClientClick="return confirm('确定取消该打样单');" 
                        Text="取消" Width="56px" />&nbsp; &nbsp;
                    <asp:Button ID="btn_Back" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    &nbsp;<br />
                    <asp:Label ID="lblAddTip" runat="server" Text="新增时,请选择供应商,填写备注；打样单号、生成日期自动生成,点增加就可"
                        ForeColor="#6600ff" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table id="tb_bosdet" runat="server" style="text-align: center; width: 900px;">
            <tr id="tr_bosdet" runat="server">
                <td colspan="2" align="left" valign="bottom" style="border-spacing: 0; border: 0px;"
                    class="style2">
                    <asp:TextBox ID="txt_bosdetLine" runat="server" Width="42px" MaxLength="2"></asp:TextBox><asp:TextBox
                        ID="txt_bosdetCode" runat="server" Width="206px" AutoPostBack="true" OnTextChanged="txt_bosdetCode_TextChanged"
                        MaxLength="50"></asp:TextBox><asp:TextBox ID="txt_bosdetQAD" runat="server" Width="93px"
                            Enabled="false" ForeColor="#999999"></asp:TextBox><asp:TextBox ID="txt_bosdetQty"
                                runat="server" Width="50px" MaxLength="4"></asp:TextBox><asp:TextBox ID="txt_bosdetreqDate"
                                    runat="server" Width="73px" CssClass="Date"></asp:TextBox><asp:TextBox ID="txt_bosdetRmks"
                                        runat="server" Width="258px" MaxLength="100"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnSaveDet" runat="server" Text="添加" CssClass="SmallButton2"
                        OnClick="btnSaveDet_Click" Width="40px" />
                    &nbsp;
                    <asp:Button ID="btn_detCancel" runat="server" Text="取消" CssClass="SmallButton2" OnClick="btn_detCancel_Click"
                        Width="40px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <asp:GridView ID="gv_det" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging" DataKeyNames="bos_nbr,bos_det_line"
                        OnRowDataBound="gv_det_RowDataBound" OnRowDeleting="gv_det_RowDeleting" OnRowCancelingEdit="gv_det_RowCancelingEdit"
                        OnRowEditing="gv_det_RowEditing" OnRowUpdating="gv_det_RowUpdating" OnRowCommand="gv_det_RowCommand"
                        PageSize="8" Width="900px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="行号" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="零件Code" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="零件QAD" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="需求日期" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="备注"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="关联文档" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="单据无明细记录" ColumnSpan="9"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="bos_det_line" HeaderText="行号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_det_code" HeaderText="部件号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_det_qad" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量">
                                <ItemTemplate>
                                    <%# Eval("bos_det_qty")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetqty" Text='<%# Bind("bos_det_qty") %>' runat="server" Width="60px"
                                        MaxLength="4" CssClass="Date" />
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="right" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="需求日期">
                                <ItemTemplate>
                                    <%# Eval("bos_det_requireDate","{0:yyyy-MM-dd}")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetrequireDate" Text='<%# Bind("bos_det_requireDate","{0:yyyy-MM-dd}") %>'
                                        runat="server" Width="90px" />
                                </EditItemTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <%# Eval("bos_det_rmks")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetrmks" Text='<%# Bind("bos_det_rmks") %>' runat="server"
                                        Width="200px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="关联文档">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Doc" runat="server" CommandName="EditDoc" Font-Underline="True"
                                        CommandArgument='<%# Container.DataItemIndex %>' ForeColor="Black"> 查看</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:CommandField EditText="编辑" ShowEditButton="True" CancelText="取消" UpdateText="更新">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="删除">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
