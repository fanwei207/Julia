<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TaskList.aspx.cs" Inherits="IT_TSK_TaskList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table style="width: 1220px">
            <tr>
                <td align="left">
                    任务号：<asp:TextBox ID="txtNbr" runat="server" Width="60px" CssClass="Param"></asp:TextBox>&nbsp;开始日期：
                    <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    &nbsp;&nbsp; 责任人：<asp:DropDownList ID="dropUsers" runat="server" Width="100px" DataTextField="userName"
                        DataValueField="userID" CssClass="Param">
                    </asp:DropDownList>
                    &nbsp;<asp:CheckBox ID="chkNotComplete" runat="server" Checked="True" Text="仅未完成"
                        CssClass="Param" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chkNotCancel" runat="server" Text="仅未取消" CssClass="Param" Checked="True" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                </td>
                <td style="font: 10px;" align="left">
                    （c表示任务已完成, x表示任务已取消, 斜体蓝字表示该步骤已完成）
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            PageSize="20" AllowPaging="True" Width="1220px" OnRowDataBound="gv_RowDataBound"
            DataKeyNames="tskd_id,tskd_isCanceled,tskd_process,tskd_testing,tskd_logging,tskd_type,tskd_mstrNbr"
            OnRowCommand="gv_RowCommand" OnRowUpdating="gv_RowUpdating" OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1220px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="状态" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="任务号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系统" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类别" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="程度" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="任务描述" Width="520px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="责任人" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="开始时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="截止时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建时间" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="处理过程" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="测试员" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="跟踪人" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="tskd_status" HeaderText="状态">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_mstrNbr" HeaderText="任务号">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_sys" HeaderText="系统">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_type" HeaderText="类别">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_degree" HeaderText="程度">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_desc" HeaderText="任务描述">
                    <HeaderStyle Width="520px" HorizontalAlign="Center" />
                    <ItemStyle Width="520px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_chargeName" HeaderText="责任人">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_stdDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd hh:mm}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tskd_dueDate" HeaderText="截止时间" DataFormatString="{0:yyyy-MM-dd hh:mm}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="处理过程">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkProcess" runat="server" CommandName="Process"><u>开发</u></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkLog" runat="server" CommandName="Log"><u>LOG</u></asp:LinkButton>
                        &nbsp;&nbsp;<u runat="server" id="uLog" tskdID='<%# Eval("tskd_id") %>' class="IT_uLog" style=" cursor:pointer;">日志</u>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="tsk_tester" HeaderText="测试员">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_trackName" HeaderText="跟踪人">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".GridViewRowStyle").dblclick(function () {

                var _tskNbr = $(this).find("td:eq(1)").html();
                var _src = "../IT/TSK_TaskView.aspx?tskNbr=" + _tskNbr;
                $.window("任务明细", 650, 600, _src);
            })

            $(".GridViewAlternatingRowStyle").dblclick(function () {

                var _tskNbr = $(this).find("td:eq(1)").html();
                var _src = "../IT/TSK_TaskView.aspx?tskNbr=" + _tskNbr;
                $.window("任务明细", 650, 600, _src);
            })

            $(".IT_uLog").click(function () {

                var _date = $("#txtCrtDate1").val();
                var _year = new Date(_date).getFullYear();
                var _month = new Date(_date).getMonth();
                var _tskdID = $(this).attr("tskdID");
                var _src = "../IT/TSK_GanntDetail.aspx?year=" + _year + "&month=" + _month + "&id=" + _tskdID + "&type=Devp";
                $.window("任务明细", 1300, 600, _src);
            })

        })
        

    </script>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
