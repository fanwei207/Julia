<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingList1.aspx.cs" Inherits="SID_PackingList1" %>

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
        <table cellspacing="1" cellpadding="1" width="1060px" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="系统货运单号(PK):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKNo" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
<%--                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="参考(Ref):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKRef" runat="server" Width="80px" TabIndex="2" CssClass="smalltextbox"></asp:TextBox>
                </td>--%>
                <td width="80px">
                    出运日期:
                </td>
                <td align="right" width="200px">
                    <asp:TextBox ID="txtShipDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="80px"></asp:TextBox>-<asp:TextBox ID="txtShipDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="80px"></asp:TextBox>
                </td>
                <td Width="55px">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" Text="出运单号:"></asp:Label>
                </td>
                <td Width="80px">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="80px" TabIndex="3"></asp:TextBox>
                </td>
                <td Width="55px">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="单证价格:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="ddl_pricestatus" runat="server" OnSelectedIndexChanged="ddl_pricestatus_SelectedIndexChanged" AutoPostBack = "true">
                        <asp:ListItem Value="0" Selected="True">未确认</asp:ListItem>
                        <asp:ListItem Value="1">已确认</asp:ListItem>
                        <asp:ListItem Value="2">未确认-ATL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Label ID="lblDomain" runat="server" Width="20px" CssClass="LabelRight" Text="域:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDomain" runat="server" Width="60px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="打印" Width="50px" OnClick="btnAddNew_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="80px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td>
                    <asp:Literal ID="lt_CombineNbr" runat="server" Text="合并出运单号"></asp:Literal>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txt_CombineNbr" runat="server" Width="500px" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="7">
                    <asp:Button ID="btn_Confirm" runat="server" Text="确认" 
                        onclick="btn_Confirm_Click" CssClass="SmallButton2"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Submit" runat="server" Text="合并"  CssClass="SmallButton2" 
                        onclick="btn_Submit_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" Text="取消" 
                        onclick="btn_Cancel_Click" CssClass="SmallButton2"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPacking" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvSID_PreRender" OnRowCommand ="gvPacking_RowCommand"
            DataKeyNames="SID,Nbr" OnPageIndexChanging="gvPacking_PageIndexChanging" Width="1020px"
            OnRowDataBound="gvPacking_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1060px" CellPadding="-1" CellSpacing="0" runat="server"
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
                        <asp:TableCell Text="发票日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
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
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
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
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="出厂日期">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" />
                    <ItemStyle Width="160px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PackingDate" HeaderText="发票日期">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
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
                <asp:TemplateField HeaderText="出运单合并">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNbr" runat="server" CommandName="detail" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text='<%# Bind("NbrCombine") %>' CommandArgument='<%# Bind("Nbr") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="160px" />
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:TemplateField>
<%--                <asp:BoundField DataField="NbrCombine" HeaderText="出运单合并">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="作废">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="报关">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拒绝">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org3_con") %>' Text='<%# Eval("SID_org3_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
