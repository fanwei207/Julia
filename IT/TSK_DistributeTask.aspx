<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DistributeTask.aspx.cs"
    Inherits="IT_TSK_DistributeTask" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellpadding="0" cellspacing="0" width="790px">
            <tr style="height: 20px;">
                <td style="width: 100px">
                    &nbsp;
                </td>
                <td style="width: 100px">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td style="width: 350px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    系统模块：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlSystem" runat="server" Width="130px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSystem_SelectedIndexChanged">
                        <asp:ListItem Value="--">--请选择一个系统--</asp:ListItem>
                        <asp:ListItem>JULIA</asp:ListItem>
                        <asp:ListItem>ANGELA</asp:ListItem>
                        <asp:ListItem>QAD</asp:ListItem>
                        <asp:ListItem >仓库条码</asp:ListItem>
                        <asp:ListItem >工单汇报</asp:ListItem>
                        <asp:ListItem >门卫扫描</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; &nbsp;<asp:DropDownList ID="ddlModule" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    任务类别：
                </td>
                <td colspan="3" align="left">
                    <asp:RadioButtonList ID="radlType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="UPGRADE">UPGRADE</asp:ListItem>
                        <asp:ListItem Value="DEBUG">DEBUG</asp:ListItem>
                        <asp:ListItem>ISHELP</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    紧急程度：
                </td>
                <td colspan="3" align="left">
                    <asp:RadioButtonList ID="radlDegree" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">普通</asp:ListItem>
                        <asp:ListItem Value="1">优先</asp:ListItem>
                        <asp:ListItem Value="2">紧急</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    任务补充：
                </td>
                <td colspan="3">
                    (*可用简短的文字从技术性角度对任务描述进行补充说明)
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    <asp:TextBox ID="txtExtreDesc" runat="server" TextMode="MultiLine" Width="100%" Height="128px"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 15px;">
                <td colspan="3" style="text-align: left;">
                    &nbsp;
                </td>
                <td style="text-align: right;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;">
                    任务内容：（这里可以对任务进行分拆处理）
                </td>
                <td style="text-align: right;">
                    <asp:LinkButton ID="linkAdd" Style="text-align: right;" runat="server" OnClick="linkAdd_Click"><u>New</u></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" DataKeyNames="tskd_mstrNbr,tskd_id,tskd_process,tskd_isCanceled,tskd_isCompleted,tskd_testing,tskd_testingDate"
                        Width="840px" OnRowCommand="gv_RowCommand" 
                        OnRowDataBound="gv_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="序号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="类型" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="任务内容" Width="600px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="开始时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="截止时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskd_index" HeaderText="序号" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_id" HeaderText="" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_type" HeaderText="类型" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_desc" HeaderText="任务内容">
                                <HeaderStyle Width="500px" HorizontalAlign="Center" />
                                <ItemStyle Width="500px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_stdDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_dueDate" HeaderText="截止时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_chargeName" HeaderText="责任人">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDelete" Text="<u>删除</u>" ForeColor="Blue" Font-Size="12px"
                                        runat="server" CommandName="myDelete" />
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                    &nbsp;
                </td>
                <td colspan="3" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                    跟踪人员：
                </td>
                <td colspan="3" align="left">
                    <asp:DropDownList ID="dropTracker" runat="server" Width="100px" DataTextField="userEmail"
                        DataValueField="userID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                    更新人员：
                </td>
                <td colspan="3" align="left">
                    <asp:DropDownList ID="dropUpdater" runat="server" Width="100px" DataTextField="userEmail"
                        DataValueField="userID">
                    </asp:DropDownList>
                    （*暂时只有一个人有权限）
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                    测试人员：
                </td>
                <td colspan="3" align="left">
                    <asp:DropDownList ID="dropTester1" runat="server" Width="100px" DataTextField="userEmail"
                        DataValueField="userID">
                    </asp:DropDownList>
                    &nbsp;And
                    <asp:DropDownList ID="dropTester2" runat="server" Width="100px" DataTextField="userEmail"
                        DataValueField="userID">
                    </asp:DropDownList>
                    （*每个任务必须有两个测试人员）</td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center;">
                    <asp:Button ID="txtDone" runat="server" Text="SAVE" CssClass="SmallButton3" OnClick="txtDone_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="txtBack_Click" />
                    <asp:CheckBox ID="chkApplyEmailed" runat="server" Visible="False" />
                    <input id="hidApplyDesc" type="hidden" runat="server" />
                    <input id="hidApplyEmail" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
     <script language="JavaScript" type="text/javascript">

         $(function () {

             $(".GridViewRowStyle").dblclick(function () {

                 var _tskNbr = $(this).find("td:eq(1)").html();
                 var _src = "../IT/TSK_GanntDetail.aspx?id=" + _tskNbr ;
                 $.window("任务处理进度", 1300, 600, _src);
               
             })

             $(".GridViewAlternatingRowStyle").dblclick(function () {

                 var _tskNbr = $(this).find("td:eq(1)").html();
                 var _src = "../IT/TSK_GanntDetail.aspx?id=" + _tskNbr ;
                 $.window("任务处理进度", 1300, 600, _src);
             })



         })
        

    </script>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
