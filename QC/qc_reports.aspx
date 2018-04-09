<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_reports.aspx.cs" Inherits="QC_qc_reports" %>

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
                <td style="width: 112px;">
                    
                </td>
                <td style="width: 411px" align="center">
                    <asp:DropDownList ID="dropType1" runat="server">
                        <asp:ListItem>整件</asp:ListItem>
                        <asp:ListItem>元器件</asp:ListItem>
                        <asp:ListItem>零部件</asp:ListItem>
                        <asp:ListItem>包装</asp:ListItem>
                        <asp:ListItem>辅料</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropYear1" runat="server">
                        <asp:ListItem>2009</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnYear1" runat="server" CssClass="SmallButton3" OnClick="btnYear1_Click"
                        TabIndex="24" Text="月报表" Width="40px" />
                    <asp:DropDownList ID="dropMonth1" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnMonth1" runat="server" CssClass="SmallButton3" TabIndex="24" Text="日报表"
                         OnClick="btnMonth1_Click" />
                </td>
                <td style="width: 95px">
                </td>
            </tr>
            <tr>
                <td style=" border-top:1px solid #b8d2f0;">
                    <b>进料检验日报表</b>
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
                    整件
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>—结束日期:<asp:TextBox
                        ID="txtEndDate" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDaily" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btnDaily_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    元器件
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox5" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox6" runat="server" CssClass="smalltextbox Date Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnComponent" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btnComponent_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    零部件
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox7" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox8" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnPart" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="btnPart_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    包装
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox9" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox10" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnPackage" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="btnPackage_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    辅料
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox11" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox12" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="btnAcc" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="btnAcc_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>过程检验日报表</b>
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
                    CFL成品</td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox13" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox14" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button3_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品</td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox43" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox44" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button18" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" onclick="Button18_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox15" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox16" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button4_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇江毛管
                </td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox29" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox30" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="Button11" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button11_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox17" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox18" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button5_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 87px; height: 18px">
                    直管
                </td>
                <td style="width: 411px; height: 18px">
                    起始日期:<asp:TextBox ID="TextBox21" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox22" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button7" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button7_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>成品检验日报表</b>
                </td>
                <td style="width: 411px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate_Product" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate_Product" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDaily_Product" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" OnClick="btnDaily_Product_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate_LED" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate_LED" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDaily_lED" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" OnClick="btnDaily_lED_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    灯丝灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox71" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox72" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDSD" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="btnDSD_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED T8直管灯
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox75" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox76" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button33" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="btnT8_Click" />
                </td>
            </tr>

            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox1" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox2" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="Button1_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox3" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox4" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    明管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox31" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox32" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button12" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button12_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    直管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox19" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox20" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button6" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button6_Click" />
                </td>
            </tr>
                <tr>
                <td>
                    <b>车间返工单报表</b>
                </td>
                <td style="width: 411px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox47" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox48" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button20" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="Button20_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox49" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox50" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button21" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="Button21_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox51" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox52" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button22" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                         Width="88px" OnClick="Button22_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox53" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox54" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button23" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button23_Click"  />
                </td>
            </tr>
            <tr>
                <td align="right">
                    明管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox55" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox56" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button24" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button24_Click"  />
                </td>
            </tr>
            <tr>
                <td align="right">
                    直管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox57" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox58" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button25" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button25_Click"/>
                </td>
            </tr>
                        <tr>
                <td>
                    <b>质检第二次检验报表</b>
                </td>
                <td style="width: 411px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox59" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox60" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button26" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="Button26_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox61" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox62" runat="server" CssClass="smalltextbox Date"
                        Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button27" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出"  Width="88px" OnClick="Button27_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox63" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox64" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button28" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                         Width="88px" OnClick="Button28_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox65" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox66" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button29" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button29_Click"  />
                </td>
            </tr>
            <tr>
                <td align="right">
                    明管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox67" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox68" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button30" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button30_Click"  />
                </td>
            </tr>
            <tr>
                <td align="right">
                    直管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox69" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox70" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button31" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button31_Click"  />
                </td>
            </tr>
        </table>
        <table id="tblOut" runat="server" style="border-left:1px solid #b8d2f0; border-right:1px solid #b8d2f0;">
            <tr>
                <td style="width: 112px">
                    <b>外厂检验日报表</b>
                </td>
                <td style="width: 411px" align="center">
                    &nbsp; &nbsp;
                </td>
                <td style="width: 95px">
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox33" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox34" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button13" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btnDaily_ProductOut_Click" Width="88px" />
                </td>
            </tr>
                        <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="txtStdDate_Out_LED" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate_Out_LED" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDaily_ProductOut_LED" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btnDaily_ProductOut_LED_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox35" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox36" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button14" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="Button14_Click" Width="88px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px; height: 24px;">
                    起始日期:<asp:TextBox ID="TextBox37" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox38" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button15" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button15_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    明管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox39" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox40" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button16" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button16_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    直管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox41" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox42" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button17" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button17_Click" />
                </td>
            </tr>
             <tr>
                <td align="right">
                    半成品贴片
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox45" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox46" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button19" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" onclick="Button19_Click" />
                </td>
            </tr>
        </table>
        <table id="tblTcp" runat="server" style="border-bottom:1px solid #b8d2f0; border-left:1px solid #b8d2f0; border-right:1px solid #b8d2f0;">
            <tr>
                <td align="left" style="width:112px;">
                    <b>TCP日报表</b>
                </td>
                <td class="style2">
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td align="right">
                    CFL成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox23" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox24" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button8" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button8_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    LED成品
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="txtStdDate_LED_TCP" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="txtEndDate_LED_TCP" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnDaily_lED_TCP" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="btnDaily_lED_TCP_Click" />
                </td>
            </tr>
              <tr>
                <td align="right">
                    灯丝灯
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox73" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox74" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button32" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button32_Click"  />
                </td>
            </tr>
              <tr>
                <td align="right">
                    LED T8直管灯
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox77" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox78" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button34" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button34_Click"  />
                </td>
            </tr>
            <tr>
                <td align="right">
                    毛管
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox25" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox26" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button9" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button9_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    镇流器
                </td>
                <td style="width: 411px">
                    起始日期:<asp:TextBox ID="TextBox27" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                    —结束日期:<asp:TextBox ID="TextBox28" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button10" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        Width="88px" OnClick="Button10_Click" />
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
