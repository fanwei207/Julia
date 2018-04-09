<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_mark" CodeFile="perf_mark.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" width="750">
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label1" Width="50px">类型：</asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbl_type" Width="300px"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label runat="server" ID="Label7" Width="60px">工号：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_no" runat="server" Width="300" TextMode="SingleLine" AutoPostBack="True"
                        TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" rowspan="4" valign="top">
                    <asp:Label runat="server" ID="Label3" Width="50px">原因：</asp:Label>
                </td>
                <td rowspan="4">
                    <asp:TextBox ID="txb_cause" runat="server" Height="100px" Width="300" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label runat="server" ID="Label6" Width="60px">部门：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_dept" runat="server" Width="300px" AutoPostBack="true" TabIndex="10">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label2" Width="60px">工号姓名：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_user" runat="server" Width="300px" AutoPostBack="false"
                        TabIndex="11">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="countLabel" Width="60px" TabIndex="2">考评对象：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_duty" runat="server" Width="250px" AutoPostBack="true" TabIndex="5">
                        <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                      
                        <asp:ListItem Selected="false" Value="2">第二责任人</asp:ListItem>
                        <asp:ListItem Selected="false" Value="3">第三责任人</asp:ListItem>
                         <asp:ListItem Selected="false" Value="4">第四责任人</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label4" Width="60px">评分：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_ref" runat="server" ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    <asp:TextBox ID="txb_mark" runat="server" Enabled="true" TextMode="SingleLine" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="Label5" runat="server"> 说明：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_comm" runat="server" Height="200px" Width="300" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lbl_appcontent" runat="server"> 描述：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_note" runat="server" Height="200px" Width="300" TextMode="MultiLine"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="center" colspan="4">
                      <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="保存"
                        TabIndex="20"></asp:Button>
                     </td>

            </tr>
            <tr>
                <td colspan="4" height="10">

                     <asp:Label ID="lblup" runat="server" Text="上传图片" Visible="false"></asp:Label>
                    <asp:LinkButton ID="lbn_doc" runat="server" Text="" Visible="false">LinkButton</asp:LinkButton>
                    <asp:Label ID="lbldoc" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    整改意见：
                </td>
                 <td colspan="3" height="10">
                     <asp:TextBox ID="txtmark" runat="server" Height="100px" Width="650" TextMode="MultiLine"
                        ></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td align="center" colspan="4">
                     <asp:Button ID="btn_mark" runat="server" CssClass="SmallButton2" Width="80px" Text="确认整改"
                        TabIndex="20"></asp:Button>
                   
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
