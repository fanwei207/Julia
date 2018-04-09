<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ResultVersion.aspx.cs" Inherits="pur_ResultVersion" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 830px" class="main_top">
            <tr>
                <td width="100px">
                    <asp:Label ID = "lb_resultpro" Text = "版本" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_version" runat ="server" Width="120px" AutoPostBack="True" DataValueField="pur_versionId" DataTextField="pur_versionName"
                        onselectedindexchanged="ddl_version_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">品质考评</asp:ListItem>
                        <asp:ListItem Value = "2">采购考评</asp:ListItem>
                        <asp:ListItem Value = "3">技术考评</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lb_valueTotal" Text="总分值" runat="server" Width="40px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_valueTotal" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" OnClick="btn_add_Click"
                        TabIndex="0" Text="新增" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_start" runat="server" CssClass="SmallButton3" OnClick="btn_start_Click"
                        TabIndex="0" Text="启用" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_stop" runat="server" CssClass="SmallButton3" OnClick="btn_stop_Click"
                        TabIndex="0" Text="停用" />
                </td>
            </tr>
        </table>
        <table style="width: 830px" >
            <div id="div_add" runat="server" visible ="false" width="830px">
                <tr>
                    <td width="100px">
                        <asp:Label ID="lb_valuefrom" Text="版本名称" runat="server"></asp:Label>
                    </td>
                    <td width="120px">
                        <asp:TextBox ID="txt_VersionName" runat="server" width="120px"></asp:TextBox>
                    </td>
                    &nbsp;
                    <td>
                        &nbsp;
                        <asp:Button ID="btn_save" runat="server" CssClass="SmallButton3" OnClick="btn_save_Click" Visible="false"
                            TabIndex="0" Text="保存" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" OnClick="btn_cancel_Click" Visible="false"
                            TabIndex="0" Text="取消" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" Text="有效起始日期" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_StartDate" runat="server" width="120px" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" Text="有效截至日期" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_EndDate" runat="server" width="120px" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                </tr>
            </div>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="830px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowCommand="gvShip_RowCommand"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_versionId,pur_versionName,pur_versionFlag" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="版本名称" DataField="pur_versionName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="起始日期" DataField="pur_strartDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="截至日期" DataField="pur_endDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="状态" DataField="pur_versionFlag" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail" Text="<u>详细</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="创建人" DataField="pur_createdName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="pur_createdDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
<%--                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>--%>
<%--                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>--%>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
