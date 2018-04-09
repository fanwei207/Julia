<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_SignOfLeader.aspx.cs" Inherits="Supplier_supplier_SignOfLeader" %>

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
            var Level = $("#hidLevel").val();
            if(type == '0')
            {
                $("#trFile").hide();
            }if(Level != '1')
            {
                $("#trLevel").hide();
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
            <tr id="trLevel">
                <td style="text-align:right;">等级：</td>
                <td>
                    <asp:DropDownList ID="ddlFactoryInspectionLevel" runat="server" CssClass="txtDDLSupplier">
                        <asp:ListItem Text="A" Value="144E0A30-68D6-419A-8DE3-D126998F9801">A</asp:ListItem>
                        <asp:ListItem Text="B" Value="1D54B71A-3B18-4714-B596-DFD227803B1B">B</asp:ListItem>
                        <asp:ListItem Text="C" Value="0299A567-ECA3-4280-B330-AB874D093B37">C</asp:ListItem>
                        <asp:ListItem Text="D" Value="F102BEAE-CD6C-488B-9669-BE67761E1D04">D</asp:ListItem>
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2" style="text-align:center;">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton2" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidType" runat="server" />
        <asp:HiddenField ID="hidLevel" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
