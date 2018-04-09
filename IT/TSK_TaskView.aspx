<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TaskView.aspx.cs" Inherits="TSK_TaskView" %>

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
                <td align="left">
                    任务描述（最原始的任务需求）：
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="600px" Height="200px"
                        MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    申请人：<asp:TextBox ID="txtUserNo" runat="server" Width="353px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    附&nbsp;&nbsp;&nbsp; 件：<asp:HyperLink ID="hlinkFile" 
                        runat="server" NavigateUrl="~/Docs/Alter_Qad_Stru_Template.xls"
                        Target="_blank">副本TCP-CHINA Operating Management System.xlsx</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;任务补充（跟踪人关于任务的要求、分析，或需求补充）：
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;">
                    <asp:TextBox ID="txtExtreDesc" runat="server" TextMode="MultiLine" 
                        Width="600px" Height="200px"
                        MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 15px;">
                    说明：<br />
&nbsp;&nbsp;&nbsp; 1、开发任务流程：开发 =&gt; 测试 =&gt; Log =&gt; 更新 =&gt; 跟踪；其中，责任人上传Log后，表示该任务明细完成<br />
&nbsp;&nbsp;&nbsp; 2、测试任务流程：测试 =&gt; 跟踪；测试方案完成（不计较通过，或否决）后，即表示该任务明细完成<br />
&nbsp;&nbsp;&nbsp; 3、维护、分析任务流程：维护、分析 =&gt; 跟踪；当维护、分析完成后，即表示该任务明细完成</td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
