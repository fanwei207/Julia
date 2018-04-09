<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userApproveHandel.aspx.cs"
    Inherits="Admin_userApproveHandel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
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
    <style type="text/css">
        .style2
        {
            width: 80px;
            height: 21px;
        }
        .style3
        {
            height: 21px;
        }
        .style4
        {
            height: 20px;
        }
    </style>
</head>
<body onunload="javascript: var w; if(w) w.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" width="750">
            <tr>
                <td align="right" class="style2">
                   申请人：
                </td>
                <td align="left" class="style3" >
                    <asp:Label ID="lbl_applyname" runat="server"></asp:Label>
                    <asp:Label ID="lbl_applyEmail" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_userNo" runat="server" Visible="false"></asp:Label>
                    <%--    <asp:Label ID="lbl_userName" runat="server" Visible="false"></asp:Label>--%>
                </td>
            </tr>
             <tr>
                <td align="right" valign="top" class="style4" valign="middle">
                    工号：</td>
                <td align="left" class="style4">
                    <asp:Label ID="lkb" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名：<asp:Label ID="lbl_userName" runat="server"></asp:Label>
                    <asp:Label ID="lbDomain" runat="server" Visible="false"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkDetails" runat="server" onclick="linkDetails_Click">获取详细信息</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    内容：
                </td>
                <td align="left">
                    <asp:Label ID="lbl_dispcontent" runat="server" Width="670px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" Width="670px">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="670px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="意见" Width="670px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="detail" HeaderText="意见">
                                <HeaderStyle Width="670px" HorizontalAlign="Center" />
                                <ItemStyle Width="670px" HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    提交给：
                </td>
                <td align="left" style=" width:670px">
                    <asp:TextBox ID="txt_approveName" runat="server" Width="81px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox><asp:TextBox ID="txt_approveEmail" runat="server" Width="200" TextMode="SingleLine"></asp:TextBox>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton3" Text="选择" OnClick="btn_choose_Click">
                    </asp:Button>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Label ID="lbl_ApproveID" runat="server" Text="lblApproveId" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="right">
                    <asp:Label ID="lbl_sug" runat="server"> 意见：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_sug" runat="server" Height="200px" Width="670" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server"> 邮箱地址：</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtemail" runat="server" Width="200" TextMode="SingleLine"></asp:TextBox>
                    <font style="color: red">请在左边邮件地址栏中正确填写你本人的邮件地址,否则对方无法收到你的联系单.</font>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton3" Text="提交"
                        OnClick="btn_next_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btn_reject" runat="server"  CssClass="SmallButton3" Text="拒绝" 
                        onclick="btn_reject_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btn_agree" runat="server" CssClass="SmallButton3" Text="批准" 
                        onclick="btn_agree_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="返回" 
                        onclick="btn_back_Click"></asp:Button>
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
