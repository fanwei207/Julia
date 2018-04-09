<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_wofirsttracklist.aspx.cs" Inherits="plan_wo_wofirsttracklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".GridViewPagerStyle a:first").css({
                "line-height": "20px",
                "font-weight": "bold",
                "padding-left": "20px"
            }).text("上一页");

            $(".GridViewPagerStyle a:last").css({
                "line-height": "20px",
                "font-weight": "bold",
                "padding-left": "20px"
            }).text("下一页");
        })
    
    </script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1420px">
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    域
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 80px;">
                    <asp:DropDownList ID ="ddlDomain" runat="server">
                        <asp:ListItem Text="--" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="SZX" ></asp:ListItem>
                         <asp:ListItem Text="ZQL" ></asp:ListItem>
                         <asp:ListItem Text="YQL" ></asp:ListItem>
                         <asp:ListItem Text="HQL" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    加工单
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 80px;">
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox Param" 
                        Height="20px" Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    上线日期
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtActDateFrom" runat="server" 
                        CssClass="SmallTextBox Date Param" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtActDateTo" runat="server" CssClass="SmallTextBox Date Param"
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    QAD
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox Param" 
                        Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    生产线
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:DropDownList ID="dropLine" runat="server" DataTextField="ln_desc" 
                        DataValueField="ln_line" Width="100px" CssClass="Param">
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    成本中心
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtctr" runat="server" CssClass="SmallTextBox Param" 
                        Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlonline" runat="server" CssClass="Param">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">未上线</asp:ListItem>
                        <asp:ListItem Value="2">进行中</asp:ListItem>
                        <asp:ListItem Value="3">已下线</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlststus" runat="server" CssClass="Param">
                        <asp:ListItem Value="all" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="F">F</asp:ListItem>
                        <asp:ListItem Value="R">R</asp:ListItem>
                        <asp:ListItem>C</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlGet" runat="server" CssClass="Param" Visible="False">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">未领料</asp:ListItem>
                        <asp:ListItem Value="2">已领料</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="1" style="height: 27px">
                    <asp:DropDownList ID="ddlHasTracking" runat="server" CssClass="Param">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">无</asp:ListItem>
                        <asp:ListItem Value="2">有</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td align="left" colspan="1" style="height: 27px;width:50px">
                    <asp:DropDownList ID="ddlConfirm" runat="server" CssClass="Param">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">未确认</asp:ListItem>
                        <asp:ListItem Value="2">已确认</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="查询" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                       Text="Excel" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging"
            PageSize="12" Width="1250px" DataKeyNames="wo_id,wo_nbr,wo_lot,wo_domain,wo_site,wo_line,wo_part,wo_get,wo_routing"
            OnRowCommand="gvlist_RowCommand" OnRowDataBound="gvlist_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" Height="40px" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1200px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="加工单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="下达日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="实际日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="变更日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="wo_nbr" HeaderText="加工单">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_lot" HeaderText="ID">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_part" HeaderText="QAD">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_rel_date" HeaderText="下达日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_plandate" HeaderText="计划日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_rel_date_act" HeaderText="评审日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_rel_date_act" HeaderText="上线日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_line" HeaderText="生产线">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_ctr" HeaderText="成本中心">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_onlinestuts" HeaderText="工单上下线">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_status" HeaderText="状态">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wof_complete" HeaderText="首件确认通过">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_createdName" HeaderText="确认人">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:linkButton ID="first" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Text="明细" CommandName="first" Font-Underline="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:BoundField DataField="wof_inspection" HeaderText="首件确认（巡检员）通过">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wof_inspectionName" HeaderText="确认人">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:linkButton ID="firstInspection" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Text="明细" CommandName="firstInspection" Font-Underline="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="wof_remark" HeaderText="备注">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
