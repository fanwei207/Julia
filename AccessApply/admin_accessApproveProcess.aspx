<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_accessApproveProcess.aspx.cs"
    Inherits="AccessApply_admin_accessApproveProcess" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        document.onkeydown = function () {
            if (event.keyCode == 8)//清空
            {
                if (document.activeElement == Form1.txt_approveName) {
                    Form1.txt_approveName.value = '';
                }
            }

            if (event.keyCode == 13)//回车
            {
                if (document.activeElement == Form1.txt_approveName) {
                    Form1.txt_approveName.focus();
                    return;
                }
            }
        }   

    </script>

   <script language="javascript" type="text/javascript">         
       function approveFun(e) 
        {
           return confirm("你确定要批准审批吗？");
        }
     
</script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table style="width: 809px; margin-top: 5px;" cellpadding="0" cellspacing="0">
            <tr style="text-align: left;">
                <td style="height: 25px" colspan="1">
                    <asp:Label ID="Label2" runat="server" Text="申请人:" Width="54px"></asp:Label><asp:TextBox
                        ID="txtApplyUser" runat="server" Width="77px" ForeColor="DimGray" ReadOnly="True"
                        CssClass="SmallTextBox4" Height="20px"></asp:TextBox>
                    <asp:Label ID="lblApplyUserID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblApplyId" runat="server" Text="申请号Id" Visible="false"></asp:Label>
                    <asp:Label ID="lblCurrentApproveID" runat="server" Text="Label" Visible="false"></asp:Label>
                    &nbsp;
                </td>
                <td style="text-align: right;">
                    部门:<asp:TextBox ID="txtApplyDept" runat="server" ForeColor="DimGray" ReadOnly="True"
                        CssClass="SmallTextBox4" Height="20px" Width="132px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: text-top; text-align: left" valign="middle">
                    <asp:Label ID="Label1" runat="server" Text="申请理由:" Height="20px" Width="52px"></asp:Label>
                    <asp:TextBox ID="txtApplyReason" runat="server" TextMode="MultiLine" Width="100%"
                        Height="30px" ForeColor="DimGray" ReadOnly="True" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" rowspan="1" style="padding-left: 2px">
                    审批历史记录:
                </td>
            </tr>
            <tr>
                <td colspan="2" style="margin: 0px;" valign="top">
                    <asp:GridView ID="gv_ApproveRecords" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="6" CssClass="GridViewStyle" runat="server" Width="100%" DataKeyNames="aamId"
                        OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="both">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="序号" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="审批人" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="部门" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="职务" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="提交给" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="审核日期" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="审批意见" Width="190px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="#000099">
                                    <asp:TableCell HorizontalAlign="Justify" Text="没有审批记录" ColumnSpan="5"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="序号" DataField="approveLayer" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批人" DataField="approveUserName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="65px" />
                                <ItemStyle HorizontalAlign="Center" Width="65px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="公司" DataField="plantCode" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="部门" DataField="deptName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="职务" DataField="roleName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批时间" DataField="approveDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批意见" DataField="approveView" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批结果" DataField="ApproveState">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="GridviewRowStyle" />
                    </asp:GridView>
                </td>
                <td colspan="1" style="margin: 0px;" valign="top">
                </td>
            </tr>
            <tr valign="top">
                <td valign="top" style="padding-left: 2px; margin-left: 4px; width: 313px; height: 226px;
                    vertical-align: top; text-align: left;" align="left">
                    <br />
                    &nbsp;申请人申请的访问菜单:<asp:Panel ID="Panel1" runat="server" Height="300px" Width="300px"
                        BorderWidth="1" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkBL_applyedModule" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <div style="color: red;">
                        取消勾选的菜单项，表示不分配此菜单权限给申请人</div>
                </td>
                <td style="width: 427px; margin-left: 2px; height: 226px; vertical-align: top; text-align: left;"
                    valign="top">
                    <br />
                    <asp:Label ID="lblApproveView" runat="server" Text="审批意见："></asp:Label><br />
                    <asp:TextBox ID="txtApproveView" runat="server" TextMode="MultiLine" Width="100%"
                        Height="108px" CssClass="SmallTextBox"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 5px;" valign="bottom">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 230px;" valign="bottom">
                    <asp:Button ID="btn_backlook" runat="server" Text="返回" Width="73px" OnClick="btn_back_Click"
                        CssClass="SmallButton2" Height="22px" Visible="False" />
                </td>
            </tr>
            <tr id="tr1">
                <td colspan="2" style="height: 21px;" valign="bottom">
                    <asp:Label ID="lbl_submitnext" runat="server" Text=" 提交给下一审批人:" Width="100px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txt_approveName" runat="server" Width="75px" CssClass="SmallTextBox4"
                        Height="20px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox><asp:TextBox
                            ID="txt_approveEmail" runat="server" Height="20px" CssClass="SmallTextBox4" Width="301px"></asp:TextBox>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Button ID="btn_selectApprove" runat="server" Text="选择审批人" OnClick="btn_selectApprove_Click"
                        CssClass="SmallButton2" Width="70px" />&nbsp;
                </td>
            </tr>
            <tr id="tr2">
                <td colspan="2">
                    <asp:Label ID="lbl_currentApproveEmail" runat="server" Text="邮箱地址:" Width="100px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txt_currentApproveEmail" runat="server" OnTextChanged="txt_currentApproveEmail_TextChanged"
                        Width="374px" CssClass="SmallTextBox4" Height="20px"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr3">
                <td colspan="2">
                    <asp:Label ID="lbl_emailnote" runat="server" Text="请在上面邮件地址栏中正确填写你本人的邮件地址,否则下一审批人无法收到你转交的申请"
                        ForeColor="#C00000"></asp:Label>
                </td>
            </tr>
            <tr id="tr4">
                <td colspan="2" style="padding-left: 100px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="padding-left: 80px; height: 24px;">
                    <asp:Button ID="btn_submit" runat="server" Text="提交" Width="75px" OnClick="btn_submit_Click"
                        CssClass="SmallButton2" Height="22px" />
                    <asp:Button ID="btn_deny" runat="server" Text="拒绝" Width="67px" CssClass="SmallButton2"
                        Height="22px" OnClick="btn_deny_Click" />
                    <asp:Button ID="btn_approve" runat="server" Text="批准" Width="71px" CssClass="SmallButton2"
                        Height="22px" OnClientClick="return approveFun()" OnClick="btn_approve_Click" Enabled="False"   />
                    <asp:Button ID="btn_back" runat="server" Text="返回" Width="73px" OnClick="btn_back_Click"
                        CssClass="SmallButton2" Height="22px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbl_CCapplyEmail" runat="server" Text=" 抄送给申请人:" Width="100px" Visible="False"></asp:Label>
                    <asp:TextBox ID="txt_applyUserEmail" runat="server" Width="175px" CssClass="SmallTextBox4"
                        Height="20px" Visible="False"></asp:TextBox><asp:Label ID="lblannote" runat="server"
                            Text="(申请人邮箱)" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table> 
        </form>
        <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
