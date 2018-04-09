<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userApporveResume.aspx.cs"
    Inherits="userApporveResume" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 90;
            height: 18px;
        }
        .style2
        {
            width: 500;
            height: 18px;
        }
        .style3
        {
            width: 90;
            height: 20px;
        }
        .style4
        {
            width: 500;
            height: 20px;
        }
    </style>
</head>
<body>
    <div style="text-align: center;" align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White;
            text-align: center;" border="0">
            <tr>
                <td align="right" class="style3">
                    </td>
                <td valign="top" style="text-align: left;" class="style4">
                    <asp:Label ID="lbplantID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    工号：</td>
                <td valign="top" style="text-align: left;" class="style2">
                    
                    <asp:Label ID="lblUserNo" runat="server" CssClass="SmallTextBox"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    姓名：</td>
                <td valign="top" style="text-align: left;" class="style2">
                    
                    <asp:Label ID="lblUserName" runat="server" CssClass="SmallTextBox"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    部门：</td>
                <td valign="top" style="text-align: left;" class="style2">
                    
                    <asp:Label ID="lblDeptName" runat="server" CssClass="SmallTextBox"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    工段：</td>
                <td valign="top" style="text-align: left;" class="style2">
                    
                    <asp:Label ID="lblRoleName" runat="server" CssClass="SmallTextBox"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="style3">
                </td>
                <td valign="top" style="text-align: left;" class="style4">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" style="width: 500; text-align: left;">
                    仅限.doc、.docx、html、txt格式的简历
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90">
                    简历原件: &nbsp;
                </td>
                <td valign="top" style="width: 500; text-align: left;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="479px" />
&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50; height: 21px;">
                    &nbsp;
                </td>
                <td align="center" style="height: 21px;">
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" OnClick="btnUpload_Click"
                        Text="开始上传" Width="68px" />
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="返回" 
                        onclick="btn_back_Click" Visible="false"></asp:Button>
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
