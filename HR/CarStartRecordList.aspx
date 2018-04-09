<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarStartRecordList.aspx.cs" Inherits="HR_CarStartRecordList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发车列表</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" runat="server" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="SQL" Value="1">SQL</asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5">YQL</asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8">HQL</asp:ListItem>
                        <asp:ListItem Text="TCB" Value="11">TCB</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>车号</td>
                <td>
                    <asp:DropDownList ID="ddlCarNumber" runat="server" Width="85px" DataTextField="CarNumber" DataValueField="CarNumber" AutoPostBack="True" OnSelectedIndexChanged="ddlCarNumber_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>驾驶员</td>
                <td>
                    <asp:DropDownList ID="ddlDriver" runat="server" DataTextField="DriverName" DataValueField="DriverID" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlDriver_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>发车日期</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="Date" Width="80px"></asp:TextBox>
                </td>
                <td>状态</td>
                <td>
                    <asp:DropDownList ID="ddlCarStartStatus" runat="server" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlCarStartStatus_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="待使用" Value="1">使用中</asp:ListItem>
                        <asp:ListItem Text="使用中" Value="2">待收车</asp:ListItem>
                        <asp:ListItem Text="待收车" Value="3">已收车</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>审批状态</td>
                <td>
                    <asp:DropDownList ID="ddlCarStartType" runat="server" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlCarStartType_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="已审批" Value="1">已审批</asp:ListItem>
                        <asp:ListItem Text="待审批" Value="2">待审批</asp:ListItem>
                        <asp:ListItem Text="已拒绝" Value="3">已拒绝</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCarStartReacord" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" 
        DataKeyNames="id,CarRecordStatus,CarStartType" OnRowDeleting="gvCarStartReacord_RowDeleting" 
            OnRowDataBound="gvCarStartReacord_RowDataBound" OnRowCommand="gvCarStartReacord_RowCommand"
             OnRowEditing="gvCarStartReacord_RowEditing" OnRowCancelingEdit="gvCarStartReacord_RowCancelingEdit"
             OnRowUpdating="gvCarStartReacord_RowUpdating" AllowPaging="True" OnPageIndexChanging="gvCarStartReacord_PageIndexChanging" PageSize="13">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <EmptyDataTemplate>
            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                GridLines="Vertical">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="日期" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="车号" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="出发时间" Width="150px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="出发地" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="到达时间" Width="150px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="到达地" Width="50px"></asp:TableCell>                        
                    <asp:TableCell HorizontalAlign="center" Text="用途" Width="300px"></asp:TableCell>                       
                    <asp:TableCell HorizontalAlign="center" Text="货车装载情况" Width="100px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="CarNumber" HeaderText="车号" ReadOnly="True">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="DriverName" HeaderText="驾驶员" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="StartTime" HeaderText="发车时间" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="OverTime" HeaderText="收车时间" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CarRecordStatus" HeaderText="状态" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="LastKilometers" HeaderText="车辆原里程数(Km)" ReadOnly="True">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="发车里程数(Km)" >
                <EditItemTemplate>
                <asp:TextBox ID="txtStartKilometers" runat="server" CssClass="SmallTextBox" Text='<%# Bind("StartKilometers") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("StartKilometers")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收车里程数(Km)" >
                <EditItemTemplate>
                <asp:TextBox ID="txtOverKilometers" runat="server" CssClass="SmallTextBox" Text='<%# Bind("OverKilometers") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("OverKilometers")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="totalKilometers" DataFormatString="{0:0.00}" HeaderText="本次公里数(Km)" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="发车备注" >
                <EditItemTemplate>
                <asp:TextBox ID="txtCarStartReason" runat="server" CssClass="SmallTextBox" Text='<%# Bind("CarStartReason") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("CarStartReason")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LeaveCarName" HeaderText="交车人" ReadOnly="True">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ReceiveCarName" HeaderText="收车人" ReadOnly="True">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField Text="收车" HeaderText="Receive" CommandName="Receive">
                <ControlStyle Font-Bold="False" Font-Underline="True" />
                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                <ItemStyle Width="60px" HorizontalAlign="Center" ForeColor="Black" />
            </asp:ButtonField>
            <asp:TemplateField HeaderText="Delete">
                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                        CommandName="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审批">
                <ItemTemplate>
                    <asp:LinkButton ID="linkYes" runat="server" CommandName="Yes"><u>同意</u></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkNo" runat="server" CommandName="No"><u>拒绝</u></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" Width="80px"/>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
