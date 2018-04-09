<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UMP_ApproverlistAppv.aspx.cs" Inherits="wo2_UMP_ApproverlistAppv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 318px;
        }
    </style>
</head>
<body>
    <div style="text-align: left;">
        <form id="form1" runat="server">
            <table>

                <tr>
                    
                </tr>
                <tr>
                    <td align="left">
                             <table style="width: 800px;" cellpadding="1" cellspacing="1" id="appv" runat="server" visible="true">
                <tr align="right">
                    <td align="right" style="width: 50px;">
                        <asp:Label ID="Label3" runat="server" Text="提交给：" Width="76px" CssClass="LabelRight"></asp:Label>
                    </td>
                    <td align="left" class="style7">&nbsp;<asp:TextBox
                        ID="txtApproveName" runat="server"
                        CssClass="SmallButton2" Width="78px"
                        Height="21px"></asp:TextBox>
                        &nbsp;
                    <asp:TextBox ID="txt_ApproveEmail" runat="server" CssClass="SmallButton2" Width="161px"
                        Height="21px"></asp:TextBox>
                        <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                        <asp:Button ID="btn_Approver" runat="server" Text="选择提交人" OnClick="btn_Approver_Click"
                            CssClass="SmallButton2" Width="93px" Height="21px" />
                        &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="发送邮件" Checked="true" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" Text="理由:" Width="100px" CssClass="LabelRight"></asp:Label>
                    </td>
                    <td align="left" class="style7">
                        <asp:TextBox ID="txtApplyReason" runat="server" CssClass="SmallTextBox" Width="520px"
                            MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblApplyDate" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        
                    </td>
                    <td class="style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_submit" runat="server" Text="提交" CssClass="SmallButton2"
                        OnClick="btn_submit_Click" />
                        &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="通过" CssClass="SmallButton2"
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="拒绝" Width="70px" OnClick="btn_diaApp_Click" />&nbsp;
                    &nbsp;
                    
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
                    </td>
                </tr>
            </table>




       
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
