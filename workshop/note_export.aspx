<%@ Page Language="C#" AutoEventWireup="true" CodeFile="note_export.aspx.cs" Inherits="workshop_note_export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="0" cellpadding="0" style="width: 1000px;">
            <tr>
                <td>
                    工单
                </td>
                <td style="width:50px;">
                    <asp:TextBox ID="txtNbr" runat="server" Text="" Width="80px"></asp:TextBox>
                </td> 
                <td>
                    地点
                </td>
                <td style="width:50px;">
                    <asp:TextBox ID="txtSite" runat="server" Text="" Width="50px"></asp:TextBox>
                </td>                
                <td>上线日期</td>
                <td>
                    <asp:TextBox ID="txtStartOnLine" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtEndOnLine" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>下线日期</td>
                <td>
                    <asp:TextBox ID="txtStartOffDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtEndOffDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    职责
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="80px">
                        <asp:ListItem Value="2" Selected="True">工单检查</asp:ListItem>
                        <asp:ListItem Value="3">过程控制</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnQuery_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="导出" OnClick="btn_export_Click"/>
                </td>
                
                
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1020px" PageSize="20"  AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound" DataKeyNames="note_id" OnRowCommand="gv_RowCommand"
            >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                    GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="center" Text="工单" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="域" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="地点" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="生产线" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="日期" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="车间" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="线长" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="工单上下线" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="备注" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="事项" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="工作内容" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="是否勾选" Width="80px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="工单" DataField="wo_nbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID" DataField="wo_lot" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="域" DataField="wo_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                  <asp:BoundField HeaderText="地点" DataField="wo_site" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="生产线" DataField="note_line" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单上下线" DataField="wo_onlinestuts" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日期" DataField="note_cDate" ReadOnly="True" 
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="车间" DataField="note_workshop" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
               
                <asp:BoundField HeaderText="线长" DataField="note_createName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField> 
                <asp:BoundField HeaderText="备注" DataField="note_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>                                         
              <%--  <asp:BoundField HeaderText="事项" DataField="dutyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="工作内容" DataField="tempName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否勾选" DataField="isSelected" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>--%>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkDetail" runat="server" CommandName="Detail"><u>明细</u></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
