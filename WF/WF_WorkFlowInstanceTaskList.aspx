<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowInstanceTaskList.aspx.cs"
    Inherits="PM_HeaderList" %>

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
<body runat="server">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="950px" class="main_top" border="0">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:Label ID="lblNbr" runat="server" Width="60px" CssClass="LabelRight" Text="申请号:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" Width="120px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblFlowName" runat="server" Width="60px" CssClass="LabelRight" Text="流程模板:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlWorkFlow" runat="server" DataTextField="Flow_Name"
                        DataValueField="Flow_ID" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblReqDate" runat="server" Width="60px" CssClass="LabelRight" Text="申请日期:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReqDate" runat="server" Width="80px" TabIndex="3" CssClass="smallTextboxDate Date"></asp:TextBox>
                </td>
                <td style="width: 66px">
                    <asp:Label ID="lblStatus" runat="server" Width="60px" CssClass="LabelRight" Text="流程状态:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="radUndo" runat="server" Text="待处理" GroupName="radGroup" Checked="true"
                        TabIndex="4" Width="60px" AutoPostBack="True" OnCheckedChanged="radUndo_CheckedChanged" />
                </td>
                <td>
                    <asp:RadioButton ID="radFinish" runat="server" Text="已通过" GroupName="radGroup" TabIndex="5"
                        Width="60px" AutoPostBack="True" OnCheckedChanged="radFinish_CheckedChanged" />
                </td>
                <td>
                    <asp:RadioButton ID="radCancel" runat="server" Text="未通过" GroupName="radGroup" TabIndex="6"
                        Width="60px" AutoPostBack="True" OnCheckedChanged="radCancel_CheckedChanged" />&nbsp;
                </td>
                <td>
                    &nbsp;<asp:Label ID="Label1" runat="server" Width="46px"></asp:Label>
                </td>
                <td style="width: 64px" align="right">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" TabIndex="7" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWFI" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" OnPreRender="gvWFI_PreRender"
            DataKeyNames="WFN_Nbr" OnRowDataBound="gvWFI_RowDataBound" OnPageIndexChanging="gvWFI_PageIndexChanging"
            Width="990px" OnRowCommand="gvWFI_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="990px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="域" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请号" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="流程模板" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="截止日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="表单" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="操作日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="50px" HorizontalAlign="center"></asp:TableCell>
                         <asp:TableCell Text="备注" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="plantCode" HeaderText="域">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_Nbr" HeaderText="申请号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Flow_Name" HeaderText="流程模板">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_ReqDate" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_DueDate" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="表单">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkform" ForeColor="Blue" Font-Underline="true" Text='<%# Bind("WFN_FormName") %>'
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("WFN_Nbr") %>' CommandName="View">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" Width="150px" />
                    <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="WFN_CreatedBy" HeaderText="申请人">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WFN_CreatedDate" HeaderText="操作日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="明细">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdetail" Text="明细" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("WFN_Nbr") %>' CommandName="Detail" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:BoundField DataField="FNI_Remark" HeaderText="备注">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
