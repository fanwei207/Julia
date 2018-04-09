<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monit_SiteList.aspx.cs" Inherits="Performance_CCTV_LogList" %>

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
            <table id="tb1" Width="980px">
                <tr>
                    <td style="width:130px;height: 30px" align="Left">公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_plant" Width="70px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="width:300px">摄像头编号<asp:TextBox ID="txt_mID" runat="server" Width="200px"></asp:TextBox></td>
                    <td><asp:Button ID="btn_Query" runat="server" CssClass="SmallButton3" TabIndex="0" Text="查询" OnClick="btn_Query_Click"  /></td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="980px" PageSize="20" DataKeyNames="Monit_ID,Monit_mID" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting" OnRowCommand="gv_RowCommand">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:table ID="tb1" Width="980px" runat="server" CellPadding="-1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="公司" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                            <asp:TableCell Text="摄像头编号" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                            <asp:TableCell Text="区域" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                            <asp:TableCell Text="生产线" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="分辨率" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="创建人" HorizontalAlign="Center" Width="80px"></asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                            <asp:TableCell Text="备注" HorizontalAlign="Center" Width="250px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="公司" DataField="Monit_PlantName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="摄像头编号" DataField="Monit_mID" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="区域" DataField="Monit_Area" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="生产线" DataField="Monit_Beltline" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="分辨率" DataField="Monit_Resolution" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建人" DataField="Monit_CreateBy" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetail" runat="server" Text="Detail" ForeColor="Black" CommandName="myEdit"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:BoundField HeaderText="备注" DataField="Monit_Remark" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
