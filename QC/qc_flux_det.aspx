<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_flux_det.aspx.cs" Inherits="QC_qc_flux_det" %>

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
        <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="962px" BorderWidth="1px"
            BorderColor="Black" ScrollBars="Auto" Height="411px">
            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="958px" AllowPaging="True" PageSize="20" OnRowDataBound="gvReport_RowDataBound"
                DataKeyNames="id" OnPageIndexChanging="gvReport_PageIndexChanging" OnRowDeleting="gvReport_RowDeleting">
                <Columns>
                    <asp:BoundField HeaderText="���">
                        <HeaderStyle Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createdate" HeaderText="��������">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��Ʒ�ͺ�" DataField="ProductType" />
                    <asp:BoundField DataField="TestType" HeaderText="��ȼ��ʽ" />
                    <asp:BoundField HeaderText="ɫ�ݲ�" DataField="Err" />
                    <asp:BoundField HeaderText="����" DataField="I1" />
                    <asp:BoundField HeaderText="����" DataField="P1" />
                    <asp:BoundField HeaderText="��������" DataField="PF1" />
                    <asp:BoundField HeaderText="��ͨ��" DataField="Flux" />
                    <asp:BoundField HeaderText="��Ч" DataField="Efficiency" />
                    <asp:BoundField HeaderText="��ɫָ��" DataField="Ra" />
                    <asp:BoundField HeaderText="R9" DataField="R9" />
                    <asp:BoundField HeaderText="ɫ��" DataField="TC" />
                    <asp:BoundField HeaderText="ɫƷ����(x/y)" DataField="x/y" />
                    <asp:BoundField HeaderText="����" DataField="Temperature" />
                    <asp:BoundField HeaderText="DUV" DataField="DUV" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;SPAN onclick=&quot;return confirm('��ȷ��Ҫɾ����?')&quot;&gt;ɾ��&lt;SPAN&gt;" />
                </Columns>
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            </asp:GridView>
        </asp:Panel>
        <br />
             <asp:GridView ID="gvBasisInfo" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false" 
                        onpageindexchanging="gvBasisInfo_PageIndexChanging" 
                        onrowcommand="gvBasisInfo_RowCommand">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="�ļ�����" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="��ʾ" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="ɾ��" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="�ϴ���" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="�ϴ�ʱ��" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="û���ҵ�����" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="�ļ�����" DataField="fli_FlieName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="��ʾ">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("fli_FlieURL") %>'
                                        Text="��ʾ"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ɾ��">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("fli_ID") %>'
                                        Text="ɾ��"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="�ϴ���" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="�ϴ�ʱ��" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
            <br />
        <asp:Button ID="btnClose" runat="server" CausesValidation="False" CssClass="SmallButton3"
            Text="�ر�" Visible="True" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
            OnClick="btnExport_Click" Text="����" Visible="True" /></form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
