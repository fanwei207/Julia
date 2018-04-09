<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingNbrCombine.aspx.cs" Inherits="SID_PackingNbrCombine" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="1" cellpadding="1" width="1020px" border="0">
            <tr>
                <td Width="55px">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" Text="出运单号:"></asp:Label>
                </td>
                <td Width="80px">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="80px" TabIndex="3" ReadOnly ="true"></asp:TextBox>
                </td>
                <td Width="80px">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td Width="80px">
                    <asp:Button ID="btn_Confirm" runat="server" Text="确认" 
                        onclick="btn_Confirm_Click" CssClass="SmallButton2"/>
                </td>
                <td Width="80px">
                    <asp:Button ID="btn_Submit" runat="server" Text="合并"  CssClass="SmallButton2" 
                        onclick="btn_Submit_Click" Height="22px"/>
                </td>
                <td Width="80px">
                    <asp:Button ID="btn_cancel" runat="server" Text="取消"  CssClass="SmallButton2" 
                        onclick="btn_cancel_Click" Height="22px"/>
                </td>
                <td Width="80px">
                    <asp:Button ID="btn_Add" runat="server" Text="添加" 
                        onclick="btn_Add_Click" CssClass="SmallButton2"/>
                </td>
                <td>
                    <asp:Button ID="btn_Back" runat="server" Text="返回" 
                        onclick="btn_Back_Click" CssClass="SmallButton2"/>
                </td>
            </tr>
            <tr>
                <td align="left" Width="20px">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="80px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td>
                    <asp:Literal ID="lt_CombineNbr" runat="server" Text="合并出运单号"></asp:Literal>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txt_CombineNbr" runat="server" Width="500px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPacking" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvSID_PreRender" OnRowCommand ="gvPacking_RowCommand"
            DataKeyNames="SID,Nbr,NbrCombine" OnPageIndexChanging="gvPacking_PageIndexChanging" Width="1020px"
            OnRowDataBound="gvPacking_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1020px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="√" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系统货运单号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="参考" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运单号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单证发票" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="装箱地点" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出厂日期" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运输方式" Width="70px" pxHorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运往" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="域" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运单合并" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单证" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="报关" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="拒绝" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PK" HeaderText="系统货运单号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PKRef" HeaderText="参考">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nbr" HeaderText="出运单号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="shipno" HeaderText="单证发票">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Site" HeaderText="装箱地点">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="出厂日期">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Via" HeaderText="运输方式">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail1" Text="<u>附件</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="Shipto" HeaderText="运往">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="域">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NbrCombine" HeaderText="出运单合并">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
