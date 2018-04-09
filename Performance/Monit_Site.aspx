<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monit_Site.aspx.cs" Inherits="Performance_CCTV_Site" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="Center">
            <table id="tb1" width="550px">
                <tr>
                    <td style="width: 200px; height: 30px" align="Left">
                        公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_plant" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddl_plant_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    </td >
                    <td style="width: 350px; height: 30px" align="Right">
                        区域&nbsp;&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_area" Width="150px" DataTextField="Monit_Name" DataValueField="Monit_ID">
                    </asp:DropDownList>
                    </td >
                </tr>
                <tr>
                    <td style="height: 30px" align="left">生产线&nbsp;&nbsp<asp:TextBox ID="txt_beltline" Width="150px" runat="server"></asp:TextBox></td>
                    <td style="height: 30px" align="right">
                        摄像头编号&nbsp;&nbsp<asp:TextBox runat="server" Width="200px" ID="txt_MonitID"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td style="width: 200px; height: 30px">
                        分辨率&nbsp;&nbsp<asp:TextBox ID="txt_resolution" Width="150px" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="2"  valign="Top">备注&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" TextMode="MultiLine" Width="490px" Height="50px" ID="txt_remark"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  colspan="2" style="width:550px" align="center">
                        <asp:Button ID="btn_Add" runat="server" CssClass="SmallButton3" TabIndex="0" Text="保存" OnClick="btn_Add_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Button ID="btn_Back" runat="server" CssClass="SmallButton3" TabIndex="0" Text="返回" OnClick="btn_Back_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
