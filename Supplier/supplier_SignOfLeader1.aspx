<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_SignOfLeader1.aspx.cs" Inherits="Supplier_supplier_SignOfLeader1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function(){
            var type = $("#hidType").val();
            if(type == '0')
            {
                $("#trFile").hide();
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top: 20px;">
        <table cellspacing="4" cellpadding="0" width="100%" style="border: 0px solid #d7d7d7; margin-top: 2px;">
            <tr>
                <td style="text-align:right; width:10%;">
                    <asp:Label ID="Label1" runat="server" Text="意见："></asp:Label>
                </td>
                <td style="text-align:left; width:90%;">
                    <asp:TextBox ID="txtOpinion" runat="server" TextMode="MultiLine" 
                         BorderStyle="NotSet"
                        Width="90%" Height="120px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr id="trFile">
                <td style="text-align:right;">附件：</td>
                <td>
                    <asp:FileUpload ID="FileUpload1" BorderStyle="NotSet" Width="91%" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2" style="text-align:center;">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交S" CssClass="SmallButton2" OnClick="btnSubmit_Click" Visible="false" />
                    <asp:Button ID="btnYes" runat="server" CssClass="SmallButton2" Text="同意" OnClick="btnYes_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNo" runat="server" CssClass="SmallButton2" Text="驳回" OnClick="btnNo_Click"/>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidType" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>