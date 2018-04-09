<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monit_LogList.aspx.cs" Inherits="Performance_Monit_LogList" %>

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
    <table width="1140px">
        <tr align="left">
            <td style="height: 30px;width:250px">公司<asp:DropDownList runat="server" ID="ddl_plant" Width="50px" AutoPostBack="true" OnSelectedIndexChanged="ddl_plant_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                类型&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_falg" Width="80px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">正常</asp:ListItem>
                        <asp:ListItem Value="2">异常</asp:ListItem></asp:DropDownList>
            </td>
            <td width="280px">
                摄像头编号<asp:TextBox ID="txt_mID" Width="150px" runat="server"></asp:TextBox>
            </td>
            <td>
                负责人(工号)<asp:TextBox runat="server" ID="txt_PIC" Width="100px"></asp:TextBox>
            </td>
            <td>
                    日期<asp:TextBox ID="txtDate1" 
                        runat="server" Width="70px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>--<asp:TextBox
                        ID="txtDate2" runat="server" Width="70px" 
                        CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>
                </td>
            
            <td align="right">
                    <asp:Button runat="server" Text="查询" CssClass="SmallButton3" Width="70px" ID="btn_check" OnClick="btn_check_Click"
                     />
                </td>
        </tr>
    </table>
        <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1140px" PageSize="20" AllowPaging="True" DataKeyNames="Monit_id" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting" OnRowDataBound="gv_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="tb1" Width="1140px" runat="server" CellPadding="-1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="公司" HorizontalAlign="Center" Width="40px"></asp:TableCell>
                        <asp:TableCell Text="区域" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="生产线" HorizontalAlign="Center" Width="70px"></asp:TableCell>
                        <asp:TableCell Text="类型" HorizontalAlign="Center" Width="30px"></asp:TableCell>
                        <asp:TableCell Text="摄像头编号" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="日志内容" HorizontalAlign="Center" Width="300px"></asp:TableCell>
                        <asp:TableCell Text="提交者" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="提交日期" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                        <asp:TableCell Text="负责人" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                        <asp:TableCell Text="" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="公司" DataField="Monit_Plant" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="区域" DataField="Monit_Area" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="生产线" DataField="Monit_Beltline" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="类型" DataField="Monit_Flag" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="摄像头编号" DataField="Monit_mID" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日志内容" DataField="Monit_Content" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="提交者" DataField="Monit_CreateBy" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="提交日期" DataField="Monit_CreateDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="负责人"> 
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkb_Pic" runat="server" Text='<%# Bind("Monit_PIC") %>' ForeColor="Black" CommandName="PIC"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"> </asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                </asp:TemplateField>
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
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
     <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
