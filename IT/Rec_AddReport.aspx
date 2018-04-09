<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rec_AddReport.aspx.cs" Inherits="Rec_AddReport" %>

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
<div>
    <form id="form1" runat="server">
    <table id="tb1" border="0" cellspacing="0" cellpadding="0" width="700px" align="center">
    	<tr>
    		<td valign="middle" style="width:500px;height:30px;">名称：<asp:TextBox runat="Server" ID="txtRepName" TextMode="SingleLine" Width="450px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnSave" runat="Server" Text="保存" Width="52px" 
                CssClass="SmallButton3" onclick="btnSave_Click" />&nbsp;&nbsp
                <asp:Button ID="btnDel" runat="Server" Text="删除" Width="52px" 
                CssClass="SmallButton3" onclick="btnDel_Click" />
                    </td>
    	</tr>
        <tr style="height:10px;"><td></td></tr>
        <tr>
        <td valign="top" style="width:500px;height:110px;" > 
           描述：<asp:TextBox runat="server" ID="txtDescrip" TextMode="MultiLine" Width="450px" Height="100px"></asp:TextBox>
        </td>
        <td valign="top"><asp:Button ID="btnBack" runat="Server" Text="返回" Width="52px" 
                CssClass="SmallButton3" onclick="btnBack_Click" /></td>
        </tr>
    </table >
    <table id="tb2" border="0" cellspacing="0" cellpadding="0" width="700px" align="center">
        <tr>
            <td width="700px">
                <asp:GridView id="gvReport" runat="Server" AutoGenerateColumns="False"  DataKeyNames="id"
                    CssClass="GridViewStyle" onrowcancelingedit="gvReport_RowCancelingEdit" 
                    onrowediting="gvReport_RowEditing" onrowupdating="gvReport_RowUpdating">
                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="tb3" Width="700px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="NULL" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                         </EmptyDataTemplate>
                            <Columns>
                            <asp:BoundField DataField="id" runat="Server"  Visible="false"/>
                                <asp:TemplateField HeaderText="ReportName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("reportname") %>'></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripion">
                                <EditItemTemplate>
                                    <asp:TextBox runat="Server" ID="txtDes"  Text='<%# Eval("description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="550px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" CancelText="取消" EditText="编辑" UpdateText="更新">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField >
                            </Columns>
                        
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
