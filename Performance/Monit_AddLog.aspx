<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monit_AddLog.aspx.cs" Inherits="Performance_Monit_AddLog" %>

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
            <table id="tb1" style="width: 600px">
                <tr align="left">
                    <td style="height: 30px;width:300px">公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:DropDownList runat="server" ID="ddl_plant" Width="50px" AutoPostBack="true" OnSelectedIndexChanged="ddl_plant_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                        
                    </td>
                    
                    <td style="height: 30px" align="right">摄像头编号<asp:DropDownList runat="server" ID="ddl_mID"  AutoPostBack="true" Width="200px" DataTextField="Monit_mID" DataValueField="Monit_mID" OnSelectedIndexChanged="ddl_mID_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left">
                        日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_actualDate" Width="150px"></asp:TextBox>
                    </td>
                    <td align="right">
                        类型&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_falg" Width="80px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">正常</asp:ListItem>
                        <asp:ListItem Value="2">异常</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style=" height: 30px" align="left">
                        区域&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList runat="server" ID="ddl_area" Width="150px"  Visible="false" DataTextField="Monit_Name" DataValueField="Monit_ID">
                    </asp:DropDownList>
                    </td >
                    <td align="right">生产线&nbsp;&nbsp<asp:TextBox ID="txt_beltline" Width="150px" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr align="left">
                    <td colspan="2" valign="Top">日志内容&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_log" Width="530px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 0px">
                    <td colspan="2">
                        <input id="hidLogID" type="hidden" runat="server" />
                        <asp:CheckBox ID="chkIsModify" runat="server" Visible="False" />
                    </td>
                </tr>
                <tr align="left">
                    <td valign="top" style="text-align: left;" colspan="2">
                        依据：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<input id="FileUpload2" runat="server" style="width: 400px;" name="resumename" type="file" />&nbsp;
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="Center">
                        <asp:Button ID="btnContinue" runat="server" CssClass="SmallButton3" Width="70px" Text="继续录入" OnClick="btnContinue_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Button ID="btn_Back" runat="server" CssClass="SmallButton3" Width="70px" Text="返回" OnClick="btn_Back_Click" Visible="false"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton3" Width="70px" Text="保存"
                        OnClick="btnUpload_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="600px" PageSize="20" AllowPaging="True" 
                DataKeyNames="Monit_id,Monit_AllPath" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting"
                >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="tb2" Width="600px" runat="server" CellPadding="-1" CellSpacing="0"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="序号" HorizontalAlign="Center" Width="40px"></asp:TableCell>
                            <asp:TableCell Text="文件名" HorizontalAlign="Center" Width="500px"></asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" Width="60px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="序号" DataField="rowid" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="文件名" DataField="Monit_PicName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="400px" />
                        <ItemStyle HorizontalAlign="left" Width="400px" />
                    </asp:BoundField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                    <asp:TemplateField>
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtndown" runat="server" Text="下载" ForeColor="Black" CommandName="download"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
