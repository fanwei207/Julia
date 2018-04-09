<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_woFirstInspectionList.aspx.cs" Inherits="QC_qc_woFirstInspectionList" %>

<!DOCTYPE html>
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

            $(".GridViewRowStyle td:eq(0), .GridViewAlternatingRowStyle td:eq(0)").click(function(){

                $.window("工单操作", "200", "150", "", "请选择一项操作", false);
            });        
            $("#gvlist>tbody").children().each(function(index){
                var $btn=$(this).find(".smallbutton2");
                $btn.bind("click",function(){
                    window.parent.parent.$(".modal-dialog-title").text("加工单明细"); //更改窗体的名称
                });
            });

        })
    
    </script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1150px">
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    加工单
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 80px;">
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox " 
                        Height="20px" Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    评审日期
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtActDateFrom" runat="server" 
                        CssClass="SmallTextBox Date " Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtActDateTo" runat="server" CssClass="SmallTextBox Date "
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 30px; height: 27px">
                    QAD
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox " 
                        Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    生产线
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:DropDownList ID="dropLine" runat="server" DataTextField="ln_desc" 
                        DataValueField="ln_line" Width="100px" CssClass="">
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    成本中心
                </td>
                <td align="left" colspan="1" style="width: 60px; height: 27px">
                    <asp:TextBox ID="txtctr" runat="server" CssClass="SmallTextBox " 
                        Height="20px" Width="60px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlonline" runat="server" CssClass="">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">未上线</asp:ListItem>
                        <asp:ListItem Value="2">进行中</asp:ListItem>
                        <asp:ListItem Value="3">已下线</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlststus" runat="server" CssClass="">
                        <asp:ListItem Value="all" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="F">F</asp:ListItem>
                        <asp:ListItem Value="R">R</asp:ListItem>
                        <asp:ListItem>C</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlGet" runat="server" CssClass="" Visible="False">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">未领料</asp:ListItem>
                        <asp:ListItem Value="2">已领料</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td align="left" colspan="1" style="height: 27px">
                    <asp:DropDownList ID="ddlHasTracking" runat="server" CssClass="">
                        <asp:ListItem Value="0" Selected="True">--</asp:ListItem>
                        <asp:ListItem Value="1">无</asp:ListItem>
                        <asp:ListItem Value="2">有</asp:ListItem>
                    </asp:DropDownList>
                </td>
 
                <td  align="right" colspan="1" style="width: 50px; height: 27px">
                    <asp:DropDownList ID="ddlXunJian" runat="server" CssClass="" AutoPostBack="True" OnSelectedIndexChanged="ddlXunJian_SelectedIndexChanged">
                        <asp:ListItem Value="0">所有工单</asp:ListItem>
                        <asp:ListItem Value="1">线长未确认</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">巡检员未确认</asp:ListItem>
                        <asp:ListItem Value="3">巡检员确认</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="查询" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging"
            PageSize="12" Width="1150px" DataKeyNames="wo_id,wo_nbr,wo_lot,wo_domain,wo_site,wo_line,wo_part,wo_get,first_status"
            OnRowCommand="gvlist_RowCommand" OnRowDataBound="gvlist_RowDataBound">
             <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
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
                <asp:BoundField DataField="wo_status" HeaderText="工单状态">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                  <asp:BoundField DataField="first_status" HeaderText="首件状态">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:Button ID="Detail" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Text="查看" CommandName="detail" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
