<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_Apply.aspx.cs" Inherits="plan_PCD_Apply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.dev.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
        <script language="javascript" type="text/javascript">
            $(function () {
                $("#btnSetPCD").click(function () {
                    var date = $("#txtPlanDate").val();
                    $(".ApplyPCD").val(date);
                })
                $("#chkAll").click(function () { 
                    $("#gvDet input[type='checkbox'][id$='chk']").prop("checked",$(this).prop("checked"))
                })
            })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr>
                <td class="style1" align="right">
                    订&nbsp;&nbsp;单&nbsp;&nbsp;号：
                </td>
                <td>
                    <asp:Label ID="lblPoNbr" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right">
                    &nbsp;&nbsp;PCD：
                </td>
                <td>
                    <asp:TextBox ID="txtPlanDate" runat="server" Width="149px" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSetPCD" runat="server" Text="设置" CssClass="SmallButton2"/>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chk_isApproved" runat="server" Text="已审批" Enabled="false" />
                    <asp:Label ID="lbl_ApproveNote" runat="server" Text=" "></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" 
                        onclick="btnBack_Click"/>
                </td>
            </tr>
        </table>

        <table style="text-align: center; width: 800px;">
                        <tr>
                <td>
                        <asp:GridView ID="gvDet" runat="server" Width="800px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle"  DataKeyNames="DetId,poLine,planDate,ordQty" 
                        EmptyDataText="No data" onrowdatabound="gvDet_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                     <input id="chkAll" type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="poLine" HeaderText="行号" HtmlEncode="False">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="partNbr" HeaderText="客户零件号">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qadPart" HeaderText="QAD号">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ordQty" HeaderText="数量">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cusCode" HeaderText="客户">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PoRecDate" HeaderText="订单日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="reqDate" HeaderText="需求日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="planDate" HeaderText="原PCD" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="申请数量">
                                <ItemTemplate>
                                    <asp:Textbox ID="txtApplyQty" runat="server"  Text='<%# Eval("applyQty")%>' CssClass="Numeric" Width="60px"></asp:Textbox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请PCD">
                                <ItemTemplate>
                                    <asp:Textbox ID="txtApplyPCD" runat="server"  Text='<%# Eval("applyPlanDate")%>' CssClass="Date ApplyPCD" Width="80px"></asp:Textbox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </asp:TemplateField>
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
                    <asp:Label ID="lblApplyer" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblApplyerEmail" runat="server" Text="" Visible="false"></asp:Label>
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
                        CssClass="GridViewStyle" PageSize="20"  DataKeyNames="ApproveId" EmptyDataText="No data">
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
