<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionAssign.aspx.cs" Inherits="NWF_PermissionAssign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>权限分发</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
        <div align="center">
        <form id="Form1" method="post" runat="server">
            <table cellspacing="0" cellpadding="0" width="1080px" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="1" style="width: 40px; height: 27px">流名
                    </td>
                    <td align="left" colspan="1" style="height: 27px; width: 100px;">
                        <asp:DropDownList ID="ddlFlow" runat="server" Width="200px"
                            DataValueField="Flow_Id" DataTextField="Flow_Name" AutoPostBack="true" OnSelectedIndexChanged="ddlFlow_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                    <td align="left" colspan="1" style="width: 40px; height: 27px">步骤名
                    </td>
                    <td align="left" colspan="1" style="height: 27px; width: 100px;">
                        <asp:DropDownList ID="ddlStep" runat="server" Width="200px"
                            DataValueField="StepID" DataTextField="StepName" AutoPostBack="false" OnSelectedIndexChanged="ddlStep_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblId" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                    <td align="right" colspan="1" style="width: 40px; height: 27px">处理人
                    </td>
                    <td align="left" colspan="1" style="width: 200px; height: 27px">
                        <asp:TextBox ID="txtPerson" runat="server" Height="20px"
                            Width="150px"></asp:TextBox>
                        <asp:Button ID="btnPerson" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnPerson_Click" Text="选择" />
                        <asp:HiddenField ID="lblPersonId" runat="server" />
                    </td>
                    <td align="right" style="width: 45px; height: 27px">
                        <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnSearch_Click" Text="查询" />
                    </td>
                    <td align="right" style="width: 45px; height: 27px">
                        <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnSave_Click" Text="新增" />
                    </td>
                </tr>

            </table>
            <asp:DataGrid ID="DataGrid1" runat="server"
                Width="1080px" CssClass="GridViewStyle AutoPageSize" AllowPaging="True"
                PageSize="25" DataKeyField="id"
                DataKeyNames="id,FlowID,FlowName,StepID,StepName,PersonsID,PersonsName"
                AutoGenerateColumns="False" OnItemCommand="DataGrid1_ItemCommand"
                OnPageIndexChanged="DataGrid1_PageIndexChanged">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <ItemStyle CssClass="GridViewRowStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>
                    <asp:BoundColumn DataField="FlowName" ReadOnly="True" HeaderText="<b>流名</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="StepName" ReadOnly="True" HeaderText="<b>步骤名</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PersonsName" HeaderText="<b>处理人</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Person_CreatedByName" ReadOnly="True" HeaderText="<b>创建人</b>">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Person_CreatedDate" HeaderText="<b>创建时间</b>">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                        <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                        <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
