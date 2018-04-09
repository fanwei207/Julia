<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingReportForFin.aspx.cs" Inherits="SID_PackingReportForFin" %>

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
        <table cellspacing="0" cellpadding="0" width="990px" border="0" >
            <tr>
                <td class="main_left">
                </td>
                <td align="right" width="80px">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="ϵͳ���˵���(PK):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td width="90px">
                    <asp:TextBox ID="txtSysPKNo" runat="server" Width="120px" TabIndex="1"></asp:TextBox>
                </td>

                <td  align="right" width=55px>
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="���˵���:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td width="90px">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="120px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td width="55px" align="center">
                    <asp:Label ID="Label3" runat="server" Width="55px" CssClass="LabelRight" Text="��������:"
                        Font-Bold="false"></asp:Label>
                    
                </td>
                <td align="left" width="170px">
                    <asp:TextBox ID="txtShipDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="80px"></asp:TextBox>-<asp:TextBox ID="txtShipDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="80px"></asp:TextBox>
                </td>


                <td align="Left" width="55px">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" TabIndex="4"
                        Text="��ѯ" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left" width="255px">
                    &nbsp;<asp:Button ID="btn_packingexport" runat="server" TabIndex="4"
                        Text="����" Width="50px" OnClick="btn_packingexport_Click" />
                </td>
                
            </tr>
            <tr>


                 <td class="main_left">
                 </td>

                <td align="right">
                    <asp:Label ID="lblDomain" runat="server" Width="55px" CssClass="LabelRight" Text="���۵���:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_saleno" runat="server" Width="120px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right" Width="55px" >
                    <asp:Label ID="Label1" runat="server" Width="55px" CssClass="LabelRight" Text="��&nbsp;&nbsp;Ʊ&nbsp;&nbsp;��:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_invoiceno" runat="server" Width="120px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>



                 <td align="center">
                    <asp:Label ID="Label2" runat="server" Width="55px" CssClass="LabelRight" Text="�ᵥ����:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" width="170px">
                    <asp:TextBox ID="txt_bldate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="80px"></asp:TextBox>-<asp:TextBox ID="txt_bldate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="80px"></asp:TextBox>
                </td>           
                <td colspan="2">
                </td>
            </tr>
            <tr>


                 <td class="main_left">
                 </td>

                <td align="right">
                    <asp:Label ID="Label4" runat="server" Width="55px" CssClass="LabelRight" Text="��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_cust" runat="server" Width="120px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right" Width="55px" >
                    <asp:Label ID="Label5" runat="server" Width="55px" CssClass="LabelRight" Text="���տͻ�:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_lastcust" runat="server" Width="120px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>


 
                <td colspan="2">
                    <asp:DropDownList ID="ddl_type" runat="server">
                        <asp:ListItem Value = 0>���</asp:ListItem>
                        <asp:ListItem Value = 1>δ���</asp:ListItem>
                        <asp:ListItem Value = 2>������ȷ��</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPacking" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvPacking_PreRender" OnRowCommand ="gvPacking_RowCommand"
            DataKeyNames="nbr" OnPageIndexChanging="gvPacking_PageIndexChanging"
            OnRowDataBound="gvPacking_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <%--<asp:TableCell Text="��" Width="20px" HorizontalAlign="center"></asp:TableCell>--%>
                        <%--<asp:TableCell Text="���˵���" Width="60px" HorizontalAlign="center"></asp:TableCell>--%>
                        <%--<asp:TableCell Text="ϵͳ���˵���" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="װ��ص�" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ϱ���" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        
                        
                        <asp:TableCell Text="�ͻ�����" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="70px" HorizontalAlign="center"></asp:TableCell>--%>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
<%--                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
              <%--  <asp:BoundField DataField="sid_nbr" HeaderText="���˵���">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>--%>
               <%-- <asp:BoundField DataField="SID_PK" HeaderText="ϵͳ���˵���">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="SID_shipdate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_site" HeaderText="װ��ص�">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_QAD" HeaderText="���ϱ���">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_cust_part" HeaderText="�ͻ�����">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_qty_set" HeaderText="��������">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>--%>
<%--                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="����ֻ��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
--%>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
