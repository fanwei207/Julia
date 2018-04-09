<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_SetCategoryDetials.aspx.cs" Inherits="RDW_setCategoryDetials" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .LabelRight
        {
        }
        .style1
        {
            width: 169px;
        }
    </style>   
</head>
<body>
    <div align="center">
    <form id="Form1" method="post" runat="server">

    <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0">
        <tr align="center">
            <td class="style1" align="right">Category:</td>
            <td align="left">
                <asp:Label ID="lbl_cate" runat="Server" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
            <td></td>

        </tr>
    	<tr>
    		<td align="right" class="style1">Template:</td>
            <td align="Left" colspan="2">
                <asp:DropDownList ID='drop_template' runat="Server" DataTextField="RDW_Project" DataValueField="RDW_MstrID"
                 Width="350px" AutoPostBack="true">
                </asp:DropDownList>
            </td>
    	</tr>
        <tr>
            <asp:TextBox id="txt_templateid" runat="server"  Visible=false/>
        </tr>
       <tr id="upload" runat="server">
                <td align="right" class="style1">
                    <asp:Label ID="lblUpload" runat="server" Width="100px" CssClass="LabelRight" Text="File Upload:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="4">
                    &nbsp;<Upload:InputFile ID="fileAttachFile" runat="server" Width="330px" />
                    <br />
                    (*Max 100M)</td>
                <td align="center">
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="Upload"
                        Width="60px" onclick="btnUpload_Click"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="BtnDoc" runat="server" CssClass="SmallButton2" Text="From Docs" 
                        Width="60px" onclick="BtnDoc_Click"  Visible="false"></asp:Button>        
                </td>
            </tr>
            <tr>
                <td  >
                    
                </td>
                <td align="right">
                    <asp:Button runat="Server" ID="btn_save" Text="Save" onclick="btn_save_Click" />
                </td>
                <td  align="left">
                    <asp:Button runat="Server" ID="btn_cancel" Text="Back" 
                        onclick="btn_cancel_Click" />
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
