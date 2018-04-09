﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_valid_det.aspx.cs" Inherits="m5_valid_det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left;">Responsibility</td>
                    <td style="text-align: left;"><asp:DropDownList ID="dropValid" runat="server" Width="200px" DataTextField="m5v_descEn" DataValueField="m5v_id"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align: left;">Attachment</td>
                    <td style="width: 700px; text-align: left;">
                        <input id="file1" style="width: 100%; height: 23px" type="file" size="45" name="filename1"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: middle; text-align: left;">
                        <asp:RadioButton ID="radYes" runat="server" GroupName="valid" Text="Yes" />
&nbsp;
                        <asp:RadioButton ID="radNo" runat="server" GroupName="valid" Text="No" />
                    </td>  
                </tr>
                <tr>
                    <td style="text-align: left;">Comment</td>
                    <td style="width: 700px; text-align: left;"><asp:TextBox ID="txtMsg" runat="server" Height="200px" TextMode="MultiLine"
                        Width="100%" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: middle; text-align: center;">
                        <asp:Button ID="btn_submit" runat="server" Text="SAVE" CssClass="SmallButton2"
                            OnClick="btn_submit_Click" Width="100px" />
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
