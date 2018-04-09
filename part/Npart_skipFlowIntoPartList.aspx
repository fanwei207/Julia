<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_skipFlowIntoPartList.aspx.cs" Inherits="part_Npart_skipFlowIntoPartList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
        .auto-style2 {
            width: 110px;
            height: 28px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div align="center">
        <table>
            <tr>
                <td>
                    模板选择：
                </td>
                <td align ="left">
                    <asp:DropDownList ID="ddlModle" runat="server" DataValueField="partTypeID" DataTextField ="partTypeName" Width="200px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                
                 <td align="right" class="auto-style1">
                    导入文件: &nbsp;
                </td>
                <td colspan="2" valign="top" class="auto-style1">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImport_Click" />
                </td>

                

            </tr>
            <tr>
                <td align="right">
                    下载模板:
                </td>
                <td colspan="3" align="left">
                    <asp:LinkButton ID="lkbModle" runat="server" Text="导入模板" Font-Underline ="true" CommandName="down" OnClick="lkbModle_Click"></asp:LinkButton>
                </td>
            </tr>

            
        </table>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
