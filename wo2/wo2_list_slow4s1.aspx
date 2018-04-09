<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_list_slow4s1.aspx.cs"
    Inherits="wo2_list_slow4s1" %>

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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 960px">
            <tr>
                <td>
                    成本中心:<asp:TextBox ID="txtCostCenter" runat="server" CssClass="SmallTextBox" MaxLength="4"
                        Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp; 工单号:<asp:TextBox ID="txtNbr" runat="server"
                            CssClass="SmallTextBox" MaxLength="15" Width="90px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    ID号:<asp:TextBox ID="txtLot" runat="server" CssClass="SmallTextBox" MaxLength="15"
                        Width="90px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; QAD号:<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox Part"
                        MaxLength="15" Width="123px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导出" OnClick="btnExport_Click" Width="40px" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    下达日期:<asp:TextBox ID="txtRelDate1" runat="server" CssClass="SmallTextBox Date" MaxLength="10"
                        Width="80px"></asp:TextBox>—<asp:TextBox ID="txtRelDate2" runat="server" CssClass="SmallTextBox Date"
                            MaxLength="10" Width="80px"></asp:TextBox>&nbsp;&nbsp;结算日期:<asp:TextBox ID="txtEnterDate1"
                                runat="server" CssClass="SmallTextBox Date" MaxLength="10" Width="80px"></asp:TextBox>—<asp:TextBox
                                    ID="txtEnterDate2" runat="server" CssClass="SmallTextBox Date" MaxLength="10"
                                    Width="80px"></asp:TextBox>&nbsp;&nbsp;生效日期:<asp:TextBox ID="txtRepDate1" runat="server"
                                        CssClass="SmallTextBox Date" MaxLength="10" Width="80px"></asp:TextBox>—<asp:TextBox
                                            ID="txtRepDate2" runat="server" CssClass="SmallTextBox Date" MaxLength="10" Width="80px"></asp:TextBox>&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="查询" OnClick="btnSearch_Click" Width="40px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            OnPageIndexChanging="gvSummary_PageIndexChanging" PageSize="16" DataKeyNames="wo_site,wo_lot,ro_tool"
            OnRowCommand="gvSummary_RowCommand" Width="960px" 
            CssClass="GridViewStyle GridViewRebuild">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="地点" DataField="wo_site">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="成本中心" DataField="wo_flr_cc">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单号" DataField="wo_nbr">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID号" DataField="wo_lot">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工艺代码" DataField="wo_routing">
                    <HeaderStyle Width="110px" />
                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单数量" DataField="wo_qty_ord">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="标准工时" DataField="ro_tool">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="入库数量" DataField="wo_qty_comp">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单工时" DataField="wo_cost">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="汇报工时" DataField="rep_cost">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="差异" DataField="rep_diff">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDetails" runat="server" CommandName="Details" Font-Bold="False"
                            Font-Size="11px" Font-Underline="True" ForeColor="Black"><u>详细</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="流水线" DataField="wo_line">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="结算日期" DataField="wo_close_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
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
