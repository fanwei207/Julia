<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_report.aspx.cs" Inherits="vc_report" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 980px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Domain" runat="server" onselectedindexchanged="ddl_company_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>
                    起 始 日:<asp:TextBox  ID="txt_datestart" runat="server" Width="98px" CssClass="SmallTextBox Date" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    截 止 日:<asp:TextBox ID="txt_dateend" runat="server" Width="98px" CssClass="SmallTextBox Date" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    供 应 商:<asp:TextBox 
                        ID="txt_Custer" runat="server" Width="98px"
                        MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Button ID="btn_Query" runat="server" CssClass="SmallButton3" OnClick="btn_Query_Click"
                        TabIndex="0" Text="查询" Width="54px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Export" runat="server" CssClass="SmallButton3" OnClick="btn_Export_Click"
                        TabIndex="0" Text="导出" Width="50px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="980px"
            PageSize="20" DataKeyNames="vc_date" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
<%--                <asp:BoundField HeaderText="序号" DataField="vcc_index" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>--%>

                <asp:BoundField HeaderText="域" DataField="vc_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="日期">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("vc_date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("vc_date") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="供应商" DataField="vc_vend" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>

                <asp:BoundField HeaderText="供应商名称" DataField="usr_companyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="left" Width="220px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="确认率" DataField="checkrate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="确认罚款" DataField="checkmount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="到货率" DataField="arriverate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="到货罚款" DataField="arrivemount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="合格率" DataField="qcrate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="检验罚款" DataField="qcmount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="车间赔付" DataField="homemount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="客户索赔" DataField="cusmount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="其他" DataField="othermount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>

            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
