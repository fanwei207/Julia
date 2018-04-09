<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_New.aspx.cs" Inherits="Page_New" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {

            $("#btnConfig").click(function () {

                var _pageID = $("#hidPageID").val();
                var _src = "../IT/Page_NewConfig.aspx?pageID=" + _pageID;
                $.window("任务明细", 800, 600, _src, "", true);
            });

        })
    
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <input id="hidPageID" runat="server" type="hidden" />
        <input id="hidBackPageID" runat="server" type="hidden" />
        <input id="hidSaveProc" runat="server" type="hidden" />
        <input id="hidEditProc" runat="server" type="hidden" />
        <input id="hidType" runat="server" type="hidden" />
        <table cellspacing="0" cellpadding="0" style="width: 630px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" OnClick="btnSave_Click"
                        TabIndex="0" Text="保存" />
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" OnClick="btnBack_Click"
                        TabIndex="0" Text="返回" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <table id="tblContainer" runat="server" cellspacing="0" cellpadding="0">
        </table>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
