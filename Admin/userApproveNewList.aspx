<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userApproveNewList.aspx.cs" Inherits="Admin_userApproveNewList" %>

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
        <table style=" width:650px">
            <tr>
                <td style=" width:60px">工号：</td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="60px"></asp:TextBox>
                </td>
                 <td align="right"    style=" width:60px">姓名：</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td align="right" style=" width:60px">状态：</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="80px" 
                        AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>未申请</asp:ListItem>
                        <asp:ListItem>已申请</asp:ListItem>
                        <asp:ListItem>已通过</asp:ListItem>
                        <asp:ListItem>已拒绝</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" 
                        onclick="btnSearch_Click" />
                </td>
                <td style=" width:340px"></td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="23" OnPageIndexChanging="gv_PageIndexChanging"
             Width="650px" DataKeyNames="ResumePath" onrowcommand="gv_RowCommand" 
            onrowdatabound="gv_RowDataBound" >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="650px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="简历" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请状态" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="userNo" HeaderText="工号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="姓名">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="deptName" HeaderText="部门">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="workName" HeaderText="工段">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="简历">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkresume" Text="<u>下载</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("ResumePath") %>' CommandName="myResume" />
                            <asp:LinkButton ID="linkupload" Text="<u>上传</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("userNo") + "," + Eval("userName") + "," + Eval("deptName") + "," + Eval("workName") + "," +  Eval("plantid") %>' CommandName="myUpload" />
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="applyStatus" HeaderText="申请状态">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnHandle" Text="<u>申请</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("userNo") + "," + Eval("userName") %>' CommandName="myApply" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
     <script language="javascript" type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
