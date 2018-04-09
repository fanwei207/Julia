<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_addNPartType.aspx.cs" Inherits="part_NPart_addNPartType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.dev.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <tr>
                    <td align="right">
                        <asp:Label runat="server">供应商类型：</asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlSupplieType" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplieType_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="width:100px">
                        <asp:TextBox ID="txtSupplieType" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label runat="server">大区分类：</asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlBroadHeading" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBroadHeading_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="width:100px">
                        <asp:TextBox ID="txtBroadHeading" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label runat="server">细部区分：</asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlSubDivision" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="width:100px">
                        <asp:TextBox ID="txtSubDivision" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label runat="server">子物料：</asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlSubMaterial" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubMaterial_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="width:100px">
                          <asp:TextBox ID="txtSubMaterial" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label runat="server">等级：</asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddllevel" Width="100px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddllevel_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                         
                    </td>
                    <td style="width:100px">
                          <asp:TextBox ID="txtlevel" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>
                        <td>
                </tr>
            </table>
            <div>
                <asp:Button ID="btnAdd" runat="server" Text="添加"  CssClass ="SmallButton2" OnClick="btnAdd_Click"/> &nbsp;&nbsp;&nbsp;
                <asp:Button  ID="btnReturn" runat="server" Text="返回"  CssClass ="SmallButton2" OnClick="btnReturn_Click"/>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
