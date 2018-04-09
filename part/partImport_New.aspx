<%@ Page Language="C#" AutoEventWireup="true" CodeFile="partImport_New.aspx.cs" Inherits="ManualPoImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <div style="width: 820px; margin: 20px auto; padding-top: 2px;
            padding-bottom: 2px;">
            <fieldset style="width: 790px;">
                <legend style="padding-left: 2px;"><b>File Import</b></legend>
                <table cellpadding="6" cellspacing="0" style="width: 778px;" border="0">
                    <tr>
                        <td style="height: 5; width: 219px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 219px">
                            File:
                        </td>
                        <td valign="top" colspan="2" style="width: 826px">
                            <input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            &nbsp;&nbsp;<asp:Button ID="btnRouting" runat="server" 
                                onclick="btnRouting_Click" Text="Import" CssClass="SmallButton2" />
&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 20px; width: 219px;">
                            Download:
                        </td>
                        <td style="height: 20px; text-align: left;">
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="11px"
                                Font-Underline="True" NavigateUrl="~/docs/Importpart.xls" Width="62px" 
                                Target="_blank">Template</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5; width: 219px;">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
