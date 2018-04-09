<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DistributeList.aspx.cs"
    Inherits="IT_TSK_DistributeList" %>

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

            $(".GridViewRowStyle, .GridViewAlternatingRowStyle").dblclick(function () {

                var _tskNbr = $(this).find("td a:eq(0)").html();
                var _src = "../IT/TSK_TaskView.aspx?tskNbr=" + _tskNbr;
                $.window("任务明细", 650, 600, _src);
            })


        })
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 1230px;">
            <tr>
                <td align="left">
                    任务号：<asp:TextBox ID="txtNbr" runat="server" Width="60px" CssClass="Param"></asp:TextBox>&nbsp;创建时间：
                    <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    &nbsp;&nbsp; 跟踪人：<asp:DropDownList ID="dropTracker" runat="server" Width="100px"
                        DataTextField="userName" DataValueField="userID" CssClass="Param">
                    </asp:DropDownList>
                    &nbsp;状态：<asp:DropDownList ID="dropStatus" runat="server" CssClass="Param">
                        <asp:ListItem Value="All">全部</asp:ListItem>
                        <asp:ListItem Value="Disting">未分配</asp:ListItem>
                        <asp:ListItem Value="Disted">已分配</asp:ListItem>
                        <asp:ListItem Value="Testing">测试中</asp:ListItem>
                        <asp:ListItem Value="Tested">测试完成</asp:ListItem>
                        <asp:ListItem Value="Updating">更新中</asp:ListItem>
                        <asp:ListItem Value="Updated">更新完成</asp:ListItem>
                        <asp:ListItem Value="Tracking">跟踪中</asp:ListItem>
                        <asp:ListItem Value="Tracked">跟踪完成</asp:ListItem>
                        <asp:ListItem Value="Closed">已关闭</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    （斜体蓝字表示该步骤已完成）
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            PageSize="20" AllowPaging="True" Width="1230px" DataKeyNames="tsk_nbr,tsk_isDistribute,tsk_isComplete,tsk_updating,tsk_tracking,tskf_isNew,tsk_testing"
            OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="任务描述" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="跟踪人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建时间" Width="90px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="任务号">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNbr" runat="server" Font-Bold="False" Font-Size="12px" CommandName="myEdit"
                            Font-Underline="True" ForeColor="Blue" Text='<%# Bind("tsk_nbr") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                </asp:TemplateField>
                <asp:BoundField DataField="tsk_desc" HeaderText="任务描述">
                    <HeaderStyle Width="610px" HorizontalAlign="Center" />
                    <ItemStyle Width="610px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_applyName" HeaderText="申请人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_trackName" HeaderText="跟踪人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_createName" HeaderText="创建人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="tsk_createDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd hh:mm}"
                    HtmlEncode="False">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDistribute" runat="server" CommandName="Distribute"><u>分配</u></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkTest" runat="server" CommandName="Test"><u>测试</u></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkUpdate" runat="server" CommandName="myUpdate"><u>更新</u></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkTrack" runat="server" CommandName="Track"><u>跟踪</u></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkDelete" runat="server" CommandName="myDelete"><u>删除</u></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="linkClose" runat="server" CommandName="Close"><u>关闭</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="tsk_testers" HeaderText="测试员">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
