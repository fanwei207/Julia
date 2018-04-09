<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_reports_Summary.aspx.cs" Inherits="qc_reports_Summary" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="3" style="width: 626px; border-top:1px solid #b8d2f0; border-left:1px solid #b8d2f0; border-right:1px solid #b8d2f0; margin-top:2px;">
            <tr class="MainContent_top">
                <td style="width: 132px;">
                    <b>统计报表</b>
                </td>
            </tr>
            <tr>
                <td style=" border-top:1px solid #b8d2f0;">
                    <b>产线抽检统计报表</b>
                </td>
                <td style="width: 411px; height: 18px; border-top:1px solid #b8d2f0;" align="center">
                    &nbsp; &nbsp;
                </td>
                <td style=" border-top:1px solid #b8d2f0;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate12" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate12" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_CFLLineSampling" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btn_btn_CFLLineSamplingreport_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>—结束日期:<asp:TextBox
                        ID="txtEndDate" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_LEDLineSampling" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btn_LEDLineSamplingreport_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>成品检验统计报表</b>
                </td>
                <td style="width: 411px; height: 18px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate22" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>—结束日期:<asp:TextBox
                        ID="txtEndDate22" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_CFPLampSampling" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btn_CFPLampSamplingreport_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate21" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate21" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_LEDLampSampling" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_LEDLampSampling_Click" />
                </td>
            </tr>
             <tr>
                <td align="right">
                    灯丝灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate23" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate23" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_DSD" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_DSD_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED T8直管灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate24" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate24" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_T8" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_T8_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>TCP成品检验统计报表</b>
                </td>
                <td style="width: 411px; height: 18px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate31" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>—结束日期:<asp:TextBox
                        ID="txtEndDate31" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_TcpCFPLampSampling" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btn_TcpCFPLampSamplingreport_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate32" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate32" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_TcpLEDLampSampling" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_TcpLEDLampSampling_Click" />
                </td>
            </tr>
             <tr>
                <td align="right">
                    灯丝灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate33" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate33" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_TcpDSD" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_TcpDSD_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED T8直管灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate34" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate34" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_TcpT8" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btn_TcpT8_Click" />
                </td>
            </tr>
        </table>
        </form>
        <script language="vbscript" type="text/vbscript"> 
                <!-- 
                    Sub document_onkeydown 
                                        
                    if window.event.srcElement.tagName ="A" then
                       exit sub
                    end if
                    if window.event.keyCode=13 then 
                    window.event.keyCode=9 
                    end if 
                    End Sub 
                    //--> 
        </script>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
