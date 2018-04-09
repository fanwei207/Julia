<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_FaultAssessment.aspx.cs" Inherits="new_FaultAssessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 469px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table style="width: 730px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left" class="style1">
                记过考核类型：<asp:TextBox ID="txtassessment" runat="server" Width="175px" ></asp:TextBox>
                </td>
                <td align="left">
                    
                    
                   <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" Width="50px"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1">
                    备注:<asp:TextBox ID="txtNote" runat="server" Width="375px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblId" runat="server" Visible="False" Width="0px"></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" Width="50px"
                        OnClick="btnAdd_Click" Visible="false" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton3" Visible="false"
                        Width="50px" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="SmallButton3"
                        Width="50px" OnClick="btnCancel_Click"  />
                     
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="730px" DataKeyNames="perfdm_id,perfdm_type,perfdm_remark"
            OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" 
            onpageindexchanging="gv_PageIndexChanging" onrowdeleting="gv_RowDeleting"
             >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="730px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="记过考核类型" Width="230px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注" Width="400px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="操作" Width="300px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="无符合条件的信息" ColumnSpan="5"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="记过考核id" DataField="perfdm_id"  Visible="false">
                    <HeaderStyle  Width="0px"  />
                    <ItemStyle Width="0px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="记过考核类型" DataField="perfdm_type" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="230px" />
                    <ItemStyle HorizontalAlign="Left" Width="230px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="perfdm_remark" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="400px" />
                    <ItemStyle HorizontalAlign="left" Width="400px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" Text="Edit" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("perfdm_id") %>' CommandName="ModifyDesc" />
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkDelete" Text="Delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("perfdm_id") %>' CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
        </form>
    </div>
</body>
</html>
