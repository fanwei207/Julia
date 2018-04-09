<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_FlowNodePersonChoose.aspx.cs"
    Inherits="WF_FlowNodePersonChoose" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" type="text/css" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center" runat="server">
            <tr>
                <td align="left" style="height: 20px" valign="top" width="450">
                    <fieldset style="font-family: 微软雅黑;">
                        <legend>请选择岗位或人员:</legend>&nbsp;<asp:RadioButton ID="rbRole" runat="server" GroupName="typeGroup"
                            Text="岗位" Checked="True" OnCheckedChanged="rbRole_CheckedChanged" AutoPostBack="True" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:RadioButton ID="rbUser" runat="server" GroupName="typeGroup" Text="人员" OnCheckedChanged="rbUser_CheckedChanged"
                            AutoPostBack="True" />
                        <br />
                        <br />
                        &nbsp;
                        <asp:Label ID="lblPlant" runat="server" Text="公司:"></asp:Label><asp:DropDownList ID="ddlPlant" runat="server" Width="200px" AutoPostBack="True"
                            DataTextField="description" DataValueField="plantID" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                        &nbsp;
                        <asp:Label ID="lblDept" runat="server" Text="部门:"></asp:Label><asp:DropDownList ID="ddlDept"
                            runat="server" Width="200px" DataTextField="name" AutoPostBack="True" DataValueField="departmentID"
                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" Enabled="false">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </fieldset>
                </td>
            </tr>
        </table>
        <table id="table3" cellpadding="0" cellspacing="0" width="400" align="center" runat="server">
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="400px" Height="300px"
                        BorderWidth="1">
                        <asp:RadioButtonList ID="radRoleOrUser" runat="server" DataTextField="info" DataValueField="id"
                            AutoPostBack="True" OnSelectedIndexChanged="radRoleOrUser_SelectedIndexChanged">
                        </asp:RadioButtonList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtID" runat="server" Width="200px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtInfo" runat="server" Width="200px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 32px">
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="SmallButton3" OnClick="BtnSave_Click">
                    </asp:Button>
                    &nbsp;
                    <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="SmallButton3" OnClientClick="window.close();">
                    </asp:Button>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
            <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
