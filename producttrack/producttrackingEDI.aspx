<%@ Page Language="C#" AutoEventWireup="true" CodeFile="producttrackingEDI.aspx.cs"
    Inherits="producttrackingEDI" %>

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
                    条目：<asp:DropDownList ID="dropTackingType" runat="server" Width="60px" 
                        DataTextField="ptt_type" DataValueField="ptt_type" CssClass="Param">
                    </asp:DropDownList>
                    &nbsp; QAD：<asp:TextBox ID="txtQad" runat="server" Width="120px" 
                        TabIndex="3" Height="22"
                        MaxLength="15" CssClass="SmallTextBox Part Param"></asp:TextBox>
                    (可加通配符*号)&nbsp; 订单号：<asp:TextBox ID="txtNbr1" runat="server" Width="70px" 
                        TabIndex="3" Height="22"
                        MaxLength="8" CssClass="SmallTextBox Param"></asp:TextBox>
                    -<asp:TextBox ID="txtNbr2" runat="server" Width="70px" 
                        TabIndex="3" Height="22"
                        MaxLength="8" CssClass="SmallTextBox Param"></asp:TextBox>
                    &nbsp;接收日期：<asp:TextBox ID="txtDueDate1" runat="server" Width="70px" 
                        TabIndex="3" Height="22"
                        MaxLength="10" CssClass="SmallTextBox Date Param"></asp:TextBox>
                    -<asp:TextBox ID="txtDueDate2" runat="server" Width="70px" 
                        TabIndex="3" Height="22"
                        MaxLength="10" CssClass="SmallTextBox Date Param"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_search" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;<asp:Button 
                        ID="btnExportError" runat="server" Width="67px" CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btnExportError_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound"
            CssClass="GridViewStyle GridViewRebuild" AllowPaging="True" 
            onpageindexchanging="gv_PageIndexChanging" PageSize="20" 
            DataKeyNames="product,box_weight,box_size" onrowcommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" Font-Names="Tahoma,Arial" Font-Size="8pt" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <Columns>
                <asp:BoundField DataField="sod_nbr" HeaderText="订单号" >
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_line" HeaderText="行号" >
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="code" HeaderText="部件号" >
                <HeaderStyle Width="250px" />
                <ItemStyle Width="250px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="QAD">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkQad" runat="server" CommandName="Link" 
                            Font-Underline="True" Text='<%# Bind("product") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="f/n" HeaderText=" 文档情况">
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
