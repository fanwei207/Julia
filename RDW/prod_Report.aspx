<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prod_Report.aspx.cs" Inherits="RDW_prod_Report" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <tr>
                    <td>项目名称/ECN编号：</td>
                    <td>
                        <asp:TextBox ID="txtProjectName" runat="server" Width="90px" CssClass="SmallTextBox Param"></asp:TextBox>
                        <asp:HiddenField ID="hidDID" runat="server" />
                        <asp:HiddenField ID="hidMID" runat="server" />
                    </td>
                    <td align="right">跟踪号：</td>
                    <td>
                        <asp:TextBox ID="txtNo" runat="server" Width="90px" CssClass="SmallTextBox  Param"></asp:TextBox>
                    </td>
                    <td>申请日期：</td>
                    <td>
                        <asp:TextBox ID="txtCreateDate1" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>-
                    <asp:TextBox ID="txtCreateDate2" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>
                    </td>
                    <td>截止日期：</td>
                    <td>
                        <asp:TextBox ID="txtEndDate1" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>-
                        <asp:TextBox ID="txtEndDate2" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>
                    </td>
                    <td>
                        试流单处理：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProdStatus" runat="server" Width="90px" CssClass="SmallTextBox  Param">
                            <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="样品发出" Value="1"></asp:ListItem>
                            <asp:ListItem Text="采购情况" Value="2"></asp:ListItem>
                            <asp:ListItem Text="工艺确认" Value="3"></asp:ListItem>
                            <asp:ListItem Text="计划情况" Value="4"></asp:ListItem>
                            <asp:ListItem Text="试流确认" Value="5"></asp:ListItem>
                            <asp:ListItem Text="试流总分析" Value="6"></asp:ListItem>
                            <asp:ListItem Text="试流确认结果" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnReach" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnReach_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="增加" CssClass="SmallButton2" OnClick="btnNew_Click" />
                        <asp:Button ID="btnM5New" runat="server" Text="M5增加" CssClass="SmallButton2" Visible="false" OnClick="btnM5New_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">项目代码：</td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="90px" CssClass="SmallTextBox  Param"></asp:TextBox>
                    </td>
                    <td align="right">状态：</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="90px" CssClass="SmallTextBox  Param" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Text="--全部--" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                            <asp:ListItem Text="工单完工" Value="2"></asp:ListItem>
                            <asp:ListItem Text="步骤完成" Value="3"></asp:ListItem>
                            <asp:ListItem Text="项目取消" Value="4"></asp:ListItem>
                            <asp:ListItem Text="步骤关闭" Value="5"></asp:ListItem>                            
                            <asp:ListItem Text="试流单取消" Value="6"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>计划日期：</td>
                    <td>
                        <asp:TextBox ID="txtPlanDate1" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>-
                    <asp:TextBox ID="txtPlanDate2" runat="server" Width="80px" CssClass="SmallTextBox Date  Param"></asp:TextBox>
                    </td>
                    <td style="display: none;">完成日期：</td>
                    <td style="display: none;">
                        <asp:TextBox ID="txtOverDate" runat="server" Width="110px" CssClass="SmallTextBox Date  Param"></asp:TextBox></td>
                        &nbsp;&nbsp;&nbsp;
                    <td colspan="2"></td>
                    <td>试流单状态</td>
                    <td>                        
                        <asp:DropDownList ID="ddlType" runat="server" Width="80px" CssClass="SmallTextBox  Param">
                            <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="PCD过期" Value="1"></asp:ListItem>
                            <asp:ListItem Text="24H分析过期" Value="2"></asp:ListItem>
                            <asp:ListItem Text="72H方案过期" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnExport_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnExportLine" runat="server" Text="导出(分行)" CssClass="SmallButton2" OnClick="btnExportLine_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                DataKeyNames="prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy,prod_CreateByName,prod_mid,prod_did,
                prod_Status,prod_EndDate,prod_PlanDate,wo_nbr,wo_part,wo_rel_date,wo_qty_ord,wo_qty_comp,wo_close_date,prod_id"
                OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" PageSize="20">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="跟踪号" Width="50px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="项目名称" Width="70px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="项目代码" Width="60px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="QAD" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="线路板" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="申请人" Width="50px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="50px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="计划日期" Width="50px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="截止日期" Width="50px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>

                    <asp:BoundField DataField="prod_No" HeaderText="跟踪号">
                        <HeaderStyle Width="210px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="210px" HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="prod_ProjectName" HeaderText="项目名称/ECN编号">
                        <HeaderStyle Width="210px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="210px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_Code" HeaderText="项目代码">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_QAD" HeaderText="QAD">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_PCB" HeaderText="线路板">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_CreateByName" HeaderText="申请人">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_CreateByDate" HeaderText="申请日期">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_PlanDate" HeaderText="PCD">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_EndDate" HeaderText="截止日期">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="试流">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDet" runat="server" CommandName="det" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>详细</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="步骤">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkStepDet" runat="server" CommandName="stepDet" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>详细</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="取消">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkCancel" runat="server" CommandName="cancel" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>取消</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="测试">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkTest" runat="server" CommandName="test" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>测试</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="prod_Status" HeaderText="试流状态">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_nbr" HeaderText="工单号">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_domain" HeaderText="域">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_part" HeaderText="物料">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_status" HeaderText="工单状态">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_rel_date" HeaderText="下达日期">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_cancelName" HeaderText="取消人">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prod_cancelDate" HeaderText="取消日期">
                        <HeaderStyle Width="110px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_qty_ord" HeaderText="订单数量">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_qty_comp" HeaderText="完工数量">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <script type="text/javascript">
                <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
            </script>
        </div>
    </form>
</body>
</html>
