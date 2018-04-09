<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleDeliveryMaintenance.aspx.cs" Inherits="SampleDelivery_SampleDeliveryMaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr id="tr_Project" runat="server">
                <td class="style1" align="right">
                    项目名称：
                </td>
                <td>
                     <asp:TextBox ID="txt_ProjectName" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="right">
                    项目编号：
                </td>
                <td>
                     <asp:TextBox ID="txt_ProjectCode" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    送&nbsp;&nbsp;样&nbsp;&nbsp;单：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_nbr" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_detId" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    生成日期：
                </td>
                <td class="style3">
                   <asp:TextBox ID="txt_createdDate" runat="server" Width="149px" ReadOnly="true" CssClass="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    接&nbsp;&nbsp;收&nbsp;&nbsp;方：
                </td>
                <td>
                     <asp:TextBox ID="txt_receiver" runat="server" Width="149px"></asp:TextBox>
                </td>
                <td align="right">
                    送&nbsp;&nbsp;达&nbsp;&nbsp;地：
                </td>
                <td>
                     <asp:TextBox ID="txt_shipto" runat="server" Width="149px"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="style1" align="right">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtRmks" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
             <tr id="tr_approve" runat="server">
                <td class="style1">
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:CheckBox ID="chk_isApproved" runat="server" Text="已审批" Enabled="false" />
                    <asp:Label ID="lbl_ApproveNote" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr id="tr_check" runat="server">
                <td class="style1">
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:CheckBox ID="chk_isChecked" runat="server" Text="已检测" Enabled="false" />
                    <asp:Label ID="lbl_CheckNote" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr id="tr_send" runat="server">
                <td class="style1">
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:CheckBox ID="chk_isSended" runat="server" Text="已发送" Enabled="false" />
                    <asp:Label ID="lbl_SendNote" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
             <td class="style1">
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
                    <asp:Button ID="btn_Submit" runat="server" Text="重新提交" Width="53px" OnClick="btn_Submit_Click"
                        CssClass="SmallButton2"  Visible="false"/>
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
                    <asp:Label ID="lblAddTip" runat="server" Text="新增时,请填写接收方,备注；送样单号、生成日期自动生成,点增加就可"
                        ForeColor="#6600ff" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <br/>
        <table id="tb_det" runat="server" style="text-align: center; width: 900px;">
            <tr id="tr_det" runat="server">
                <td colspan="2" align="left" valign="bottom" style="border-spacing: 0; border: 0px;"
                    class="style2">
                    <asp:TextBox
                        ID="txt_detCode" runat="server" Width="180px" AutoPostBack="true" OnTextChanged="txt_detCode_TextChanged"
                        MaxLength="50"></asp:TextBox><asp:TextBox ID="txt_detQAD" runat="server" Width="80px"
                            Enabled="false" ForeColor="#999999"></asp:TextBox><asp:TextBox ID="txt_detQty"
                                runat="server" Width="60px" MaxLength="4"></asp:TextBox><asp:TextBox ID="txt_detRmks"
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
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging" DataKeyNames="Id"
                        OnRowDataBound="gv_det_RowDataBound" OnRowDeleting="gv_det_RowDeleting" OnRowCancelingEdit="gv_det_RowCancelingEdit"
                        OnRowEditing="gv_det_RowEditing" OnRowUpdating="gv_det_RowUpdating" OnRowCommand="gv_det_RowCommand"
                        PageSize="8" Width="1036px" EnableModelValidation="True">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1036px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="部件号" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="QAD号" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="备注" Width="250px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="关联文档" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" " Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测结果" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测人" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测原因" Width="200px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="单据无明细记录" ColumnSpan="10"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PartCode" HeaderText="部件号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QadNo" HeaderText="QAD号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量">
                                <ItemTemplate>
                                    <%# Eval("Quantity")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetqty" Text='<%# Bind("Quantity") %>' runat="server" Width="60px"
                                        MaxLength="4" />
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="right" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <%# Eval("Remarks")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_gvdetrmks" Text='<%# Bind("Remarks") %>' runat="server"
                                        Width="200px" />
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="关联文档">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Doc" runat="server" CommandName="EditDoc" Font-Underline="True"
                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' ForeColor="Black"> 查看</asp:LinkButton>
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
                            <asp:TemplateField HeaderText="检测结果">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCheck" runat="server" Text='<%# Eval("CheckResult")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            <asp:BoundField DataField="Checker" HeaderText="检测人" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Checker" HeaderText="检测原因" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

            <tr>
            <td align="center">
            <table id="tbApply"  style="width: 800px;" cellpadding="1" cellspacing="1" runat="server">
            <tr align="right">
                <td align="right" class="style1">
                    <asp:Label ID="Label3" runat="server" Text="下一级审批人：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    &nbsp;<asp:TextBox 
                        ID="txtApproveName" runat="server" 
                        CssClass="SmallButton2" Width="78px"
                        Height="21px"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txt_ApproveEmail" runat="server" CssClass="SmallButton2" Width="161px"
                        Height="21px"></asp:TextBox>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Button ID="btn_Approver" runat="server" Text="选择审批人" OnClick="btn_Approver_Click"
                        CssClass="SmallButton2" Width="93px" Height="21px" />
                    &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" />
                </td>
                <td>
                    <asp:Label ID="lblApplyId" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="Label5" runat="server" Text="申请原因：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApplyReason" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="approveopinion" visible="false">
                <td align="right" class="style1" >
                    <asp:Label ID="Label6" runat="server" Text="审批备注：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApprOpin" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAproveDate" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td align="right" class="style1">
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_ApplySubmit" runat="server" Text="提交" CssClass="SmallButton2"
                        OnClick="btn_ApplySubmit_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="审批通过" CssClass="SmallButton2"
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="审批拒绝" Width="70px" OnClick="btn_diaApp_Click" />&nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
            </td>
            
            </tr>
             <tr>
                <td align="left">
                    审批记录
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvApprove" runat="server" Width="800px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" PageSize="20"  DataKeyNames="Id" EmptyDataText="No data">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                GridLines="Vertical"  CssClass="GridViewHeaderStyle">
                                <asp:TableRow>
                                    <asp:TableCell Text="审批人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批意见" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批结果" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批日期" Width="200px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="没有记录" Width="100px" HorizontalAlign="center" ColumnSpan="4"></asp:TableCell>
                                    
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Approver" HeaderText="审批人" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveNote" HeaderText="审批意见" HtmlEncode="False">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveResult" HeaderText="审批结果">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveDate" HeaderText="审批日期">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

         
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
