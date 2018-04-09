﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptProductionLine.aspx.cs" Inherits="Admin_DeptProductionLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="920px" bgcolor="white" border="0">
             <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    部门
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 100px;">
                    <asp:DropDownList ID="ddlDept" runat="server" Width="200px" 
                        DataValueField="departmentID" DataTextField="name" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    生产线
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                   <%-- <asp:TextBox ID="txtLine" runat="server"  Height="20px"
                        Width="150px"></asp:TextBox>--%>
                      <asp:DropDownList ID="ddlline" runat="server" Width="200px" 
                        DataValueField="workshopID" DataTextField="name">
                    </asp:DropDownList>
                    <asp:Label ID="lblId" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>
                <td align="right" style="width: 45px; height: 27px">
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="查询" />
                </td>
            </tr>
            <tr>
                
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    线长
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtLineLeader" runat="server"  Height="20px" 
                        Width="150px"></asp:TextBox>
                    <asp:Button ID="btnLineLeader" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnLineLeader_Click" Text="选择" />
                    <asp:HiddenField ID="lblLineLeaderId" runat="server" />     
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    过控员
                </td>
                <td align="left" style="width: 100px; height: 27px">
                     <asp:TextBox ID="txtProcessMonitor" runat="server"  Height="20px"
                        Width="150px"></asp:TextBox>
                     <asp:Button ID="btnProcessMonitor" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnProcessMonitor_Click" Text="选择" />
                    <asp:HiddenField ID="lblProcessMonitorId" runat="server" />   
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
            AutoGenerateColumns="False" onitemcommand="DataGrid1_ItemCommand" 
            onpageindexchanged="DataGrid1_PageIndexChanged">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="Department" ReadOnly="True" HeaderText="<b>部门</b>">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Director" HeaderText="<b>主任</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ViceDirector" HeaderText="<b>副主任</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ProductionLine" HeaderText="<b>生产线</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LineLeader" HeaderText="<b>线长</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ProcessMonitor" HeaderText="<b>过控员</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LineLeaderId" HeaderText="<b>线长</b>" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ProcessMonitorId" HeaderText="<b>过控员</b>" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" CommandName="UpdateBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                 <asp:BoundColumn DataField="workshopID" HeaderText="<b>workshopID</b>" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
