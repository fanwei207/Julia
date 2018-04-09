<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyInternetAcc.aspx.cs"
    Inherits="AccessApply_ApplyInternetAcc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        
    </title>
   <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server"> 
            <table style="width: 620px; margin-top: 5px; text-align: left;">
                <tr>
                    <td style="height: 41px">
                        <asp:Label ID="Label1" runat="server" Text="������������100����:" Height="18px" Width="240px"></asp:Label>
                        <asp:TextBox ID="txtApplyReason" runat="server" TextMode="MultiLine" Width="564px"
                            Height="62px" MaxLength="180" CssClass="SmallTextBox2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 25px; color: gray;">
                        (���ύ��SZX����Ϣ����Ա����˵��������������100ϵͳ�����ɣ��Ա���������׼)</td>
                </tr>
                <tr>
                    <td style="height: 25px">
                        <asp:Label ID="Label2" runat="server" Text="�ύ��:" Width="64px"></asp:Label>
                        <asp:TextBox ID="txt_approveName" runat="server" Width="107px" CssClass="SmallTextBox4"
                            Height="20px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btn_chooseApprove" runat="server" Text="ѡ��������" CssClass="SmallButton2"
                            Width="80px" OnClick="btn_chooseApprove_Click" />
                        <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; height: 16px" valign="middle">
                        <asp:Label ID="Label4" runat="server" Text="�����ַ:" Height="8px" Width="62px"></asp:Label>
                        <asp:TextBox ID="txt_approveEmail" runat="server" Width="179px" CssClass="SmallTextBox4"
                            Height="20px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 72px;" align="center">
                        &nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="�ύ" CssClass="SmallButton2" Width="80px"
                            OnClick="btnSubmit_Click" /></td>
                </tr>
            </table>
             
        </form> 
    </div>
    <script type ="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
</body>
</html>
