<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_AssignPermission.aspx.cs" Inherits="rmInspection_GuestComplaint_AssignPermission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客诉-主要负责人分发权限</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="920px" bgcolor="white" border="0">
             <tr>
                <td align="left" colspan="1" style="width: 40px; height: 27px">
                    模块
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 100px;">
                     <asp:DropDownList ID="ddlModule" runat="server" Width="200px" 
                        DataValueField="ModuleId" DataTextField="ModuledName" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblId" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>                
                <td align="right" colspan="1" style="width: 40px; height: 27px">
                    处理人
                </td>
                <td align="left" colspan="1" style="width: 200px; height: 27px">
                   <%-- <asp:DropDownList ID="ddlViceDirector" runat="server" Width="110px" DataValueField="userID" DataTextField="name">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtHandlePerson" runat="server"  Height="20px" 
                        Width="150px"></asp:TextBox>
                    <asp:Button ID="btnHandlePerson" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnHandlePerson_Click" Text="选择" />
                    <asp:HiddenField ID="lblHandlePersonId" runat="server" />
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
            Width="920px" CssClass="GridViewStyle AutoPageSize" AllowPaging="True" 
            PageSize="25" DataKeyField="id"
            DataKeyNames="id,moduleName,HandlePerson,HandlePersonId"
            AutoGenerateColumns="False" onitemcommand="DataGrid1_ItemCommand" 
            onpageindexchanged="DataGrid1_PageIndexChanged">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="moduleName" ReadOnly="True" HeaderText="<b>模块</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HandlePersonName" HeaderText="<b>处理人</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" CommandName="UpdateBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>取消</u>" CommandName="CancelBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
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
