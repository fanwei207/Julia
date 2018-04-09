<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Tracking.aspx.cs" Inherits="TSK_Tracking" %>

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
        <table style="width: 600px">
            <tr>
                <td style="text-align: left; height: 15px;">
                    跟踪内容：(*不可留空,100字以内):
                </td>
                <td style="text-align: right; height: 15px;">
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="SmallButton3" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="txtBack_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtTrackDesc" runat="server" TextMode="MultiLine" Width="600px"
                        Height="100px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 5px;">
                </td>
                <td style="text-align: center; height: 5px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 15px;">
                    跟踪日志：（不用填写。至少应在更新完成后跟踪一次）
                </td>
                <td style="text-align: center; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 15px;" colspan="2">
                    <div id="divTrackingLog" style="width: 100%; height: 300px; border: 1px solid #000;
                        overflow-y: scroll;" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="CANCEL, 重启任务" CssClass="SmallButton3"
                        OnClick="btnCancel_Click" Width="110px" />
                    &nbsp;
                    <asp:Button ID="btnDone" runat="server" Text="DONE, 邮件通知" CssClass="SmallButton3"
                        OnClick="btnDone_Click" Width="110px" />
                    <asp:Label ID="lbtskNbr" runat="server" Visible="False">0</asp:Label>
                    <input id="hidNbr" type="hidden" runat="server" />
                    <input id="hidDesc" type="hidden" runat="server" />
                    <input id="hidApplyEmail" type="hidden" runat="server" />
                    <input id="hidTrackBy" type="hidden" runat="server" />
                    <input id="hidTrackingEmailed" type="hidden" runat="server" />
                    <br />
                    （请谨慎点击：CANCEL重启某项任务; DONE之后邮件给申请人！） 
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
