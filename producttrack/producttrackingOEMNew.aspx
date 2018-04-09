<%@ Page Language="C#" AutoEventWireup="true" CodeFile="producttrackingOEMNew.aspx.cs" Inherits="producttrack_producttrackingOEMNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    BOM：<asp:TextBox ID="txtBOM" 
                        runat="server" Width="120px" TabIndex="3" Height="22"
                        MaxLength="14" CssClass="SmallTextBox Part Param"></asp:TextBox>
                    (可加通配符*号)&nbsp; BOM日期：<asp:TextBox ID="txtBomDate" runat="server" Width="70px" TabIndex="3"
                        Height="22" MaxLength="10" CssClass="SmallTextBox Param" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_search" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>
                    &nbsp;&nbsp;&nbsp;
                    
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExportError" runat="server" Width="67px" 
                        CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btnExportError_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" PageSize="20" DataKeyNames="product"
            OnRowCommand="gv_RowCommand" AutoGenerateColumns="false">
            <RowStyle CssClass="GridViewRowStyle" Font-Names="Tahoma,Arial" Font-Size="8pt" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <Columns>
                <asp:BoundField DataField="pro_code" HeaderText="部件号">
                    <HeaderStyle Width="250px" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="QAD">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkQad" runat="server" CommandName="Link" Font-Underline="True"
                            Text='<%# Bind("product") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="guigeshu" HeaderText="OEM规格书">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="jiance" HeaderText="OEM检测标准">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="siyin" HeaderText="丝印">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="baozhuang" HeaderText="包装图纸">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>

            </Columns>
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
