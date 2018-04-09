<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_new.aspx.cs" Inherits="m5_new" %>

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
                    <td align="left">No.：<asp:Label ID="lblNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">Type：</td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:RadioButtonList ID="radProject" runat="server" RepeatDirection="Horizontal" DataTextField="m5p_projectEn" DataValueField="m5p_id">
                            <asp:ListItem>人员</asp:ListItem>
                            <asp:ListItem>设备</asp:ListItem>
                            <asp:ListItem>物料</asp:ListItem>
                            <asp:ListItem>工艺</asp:ListItem>
                            <asp:ListItem>环境</asp:ListItem>
                            <asp:ListItem>其他</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Marketing：<%--<asp:TextBox ID="txtMarketZone" runat="server" MaxLength="30"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlMarketing" runat="server" DataTextField="m5mk_name" DataValueField ="m5mk_ID" Width="200px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                     <td style="text-align: left; height: 15px;">
                         Level：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID ="ddlLevel" runat="server" DataTextField ="soque_degreeName" DataValueField="soque_did" Width="200px" >
                         </asp:DropDownList>
                     </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">
                        Model No.:&nbsp;&nbsp; <asp:TextBox ID="txtModelNo" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>   
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Reason：</td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Attachment：<input id="fileReason" style="width: 90%; height: 23px" type="file" size="45" name="filename2"
                        runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">Content：</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Attachment：<input id="fileDesc" style="width: 90%; height: 23px" type="file" size="45" name="filename1"
                        runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnDone" runat="server" Text="Submit" CssClass="SmallButton3" OnClick="btnDone_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
