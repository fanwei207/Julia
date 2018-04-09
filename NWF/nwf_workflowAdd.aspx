﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nwf_workflowAdd.aspx.cs" Inherits="NWF_nwf_workflowAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table border="0" cellpadding="9" cellspacing="0" width="800" style="color: black;
            background-image: url(../images/bg_tb8.jpg); background-repeat: repeat-x; margin-top: 10px;">
            <tr>
                <td rowspan="6" style="width: 4px; background-image: url(../images/bg_tb7.jpg); background-repeat: no-repeat;
                    background-position: left top;">
                </td>
                <td align="right" style="width: 90px; height: 20px;" valign="middle">
                    流程模板名称:
                </td>
                <td style="width: 180px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtFlowName" runat="server" Height="20px" Width="190px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" style="width: 180px; height: 20px" valign="middle">                   
                </td>
                <td style="width: 180px; height: 20px" valign="middle">
                </td>
                <td rowspan="6" style="width: 4px; background-image: url(../images/bg_tb9.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板描述:
                </td>
                <td style="height: 20px" colspan="3">
                    <asp:TextBox ID="txtFlowDescription" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="584px"></asp:TextBox>
                </td>
            </tr>
         
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板源表:
                </td>
                <td>
                    Database<asp:DropDownList ID="ddlDatabase" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Schema
                    <asp:DropDownList ID="ddl" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Table
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板状态:
                </td>
                <td colspan="3" style="height: 20px">
                    <asp:RadioButton ID="rbNormal" runat="server" Text="正常" GroupName="gpStatus" Checked="True" />
                    &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbAbandon" runat="server" Text="停用" GroupName="gpStatus" />
                </td>
            </tr>
            <tr style="text-align: center;">
                <td colspan="4" style="height: 25px">
                    <br />
                    <asp:Button ID="btn_add" runat="server" Text="添加" CssClass="SmallButton3" CausesValidation="true"
                        OnClick="btn_add_Click" Width="43px" Height="25px"></asp:Button>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btn_return" runat="server" Text="返回" CssClass="SmallButton3" CausesValidation="true"
                        Width="53px" OnClick="btn_return_Click" Height="25px"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
	  <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
