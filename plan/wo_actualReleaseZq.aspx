<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_actualReleaseZq.aspx.cs" Inherits="wo_actualReleaseZq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function(){
            
            $(".GridViewRowStyle, .GridViewAlternatingRowStyle")
            .find("td:eq(0)").css({
                "text-decoration":"underline",
                "cursor": "pointer"
            })
            .click(function(){
                
                var _nbr = $(this).text();
                var _lot = $(this).parent().find("td:eq(1)").text();
                var _href = "/plan/wo_actualReleaseZqDet.aspx?nbr=" + _nbr + "&lot=" + _lot;
                $.window("料单", 900, 600, _href);
            });
        
        })


    </script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="1100px">
                <tr>
                    <td align="left" colspan="1">周期章:<asp:TextBox ID="txtZq" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="70px"></asp:TextBox>
                    </td>
                    <td align="right" colspan="1">加工单
                    </td>
                    <td align="left" colspan="1">
                        <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="70px"></asp:TextBox>
                    </td>
                    <td align="right" colspan="1">QAD下达日期
                    </td>
                    <td align="left" colspan="1">
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="SmallTextBox Date" Height="20px"
                            Width="80px"></asp:TextBox>--<asp:TextBox ID="txtDateTo" runat="server" CssClass="SmallTextBox Date"
                                Height="20px" Width="80px"></asp:TextBox>
                    </td>
                    <td align="right" colspan="1">评审日期
                    </td>
                    <td align="left" colspan="1">
                        <asp:TextBox ID="txtActDateFrom" runat="server" CssClass="SmallTextBox Date" Height="20px"
                            Width="80px"></asp:TextBox>--<asp:TextBox ID="txtActDateTo" runat="server" CssClass="SmallTextBox Date"
                                Height="20px" Width="80px"></asp:TextBox>
                    </td>
                    <td align="right" colspan="1">QAD
                    </td>
                    <td align="left" colspan="1">
                        <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="100px"></asp:TextBox>
                    </td>

                    <td align="right" colspan="1">工厂
                    </td>
                    <td align="left" colspan="1">
                        <asp:DropDownList ID="ddlDomain" runat="server" Width="60px">
                            <asp:ListItem Value="0" Text="--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="SZX"></asp:ListItem>
                            <asp:ListItem Value="2" Text="ZQL"></asp:ListItem>
                            <asp:ListItem Value="3" Text="YQL"></asp:ListItem>
                            <asp:ListItem Value="4" Text="HQL"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 50px; height: 27px">
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnSearch_Click" Text="查询" />
                    </td>
                    <td align="right" style="width: 45px; height: 27px">
                        <asp:Button ID="btnExcel" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnExcel_Click" Text="导出" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False"
                CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging"
                PageSize="18" Width="1200px" DataKeyNames="wo_domain,wo_nbr,wo_lot,wo_bom_code" OnRowDataBound="gvlist_RowDataBound" AllowPaging="True">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical" Width="1100px">
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
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_lot" HeaderText="ID">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_part" HeaderText="QAD">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_rel_date" HeaderText="QAD下达日期" DataFormatString="{0:MM/dd/yyyy}"
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
                    <asp:BoundField DataField="wo_online" HeaderText="上线时间" DataFormatString="{0:MM/dd/yyyy}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_offline" HeaderText="下线时间" DataFormatString="{0:MM/dd/yyyy}"
                        HtmlEncode="False">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_site" HeaderText="地点">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
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
                    <asp:BoundField DataField="wo_zhouqizhang" HeaderText="周期章">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="确认信息">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_createdName" HeaderText="创建人">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
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
