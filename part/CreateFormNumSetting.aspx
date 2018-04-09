<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateFormNumSetting.aspx.cs" Inherits="CreateFormNumSetting" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 530px" class="main_top">
            <tr>
                <td width="100px">
                    <asp:Label ID = "lb_resultpro" Text = "名称" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_formtype" runat ="server" Width="120px" DataValueField = "form_id" DataTextField ="form_name" AutoPostBack="True"
                        onselectedindexchanged="ddl_formtype_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">出运单</asp:ListItem>
                        <asp:ListItem Value = "2">采购单</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="150px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                </td>
                <td></td>
            <table  style="width: 530px" >
                <tr>
                    <td width="40px">
                        <asp:Label ID="lb_name" runat="server" Text="名称" width="40px"></asp:Label>
                    </td>
                    <td width="100px">
                        <asp:TextBox ID="txt_name" runat="server" width="100px"></asp:TextBox>
                    </td>
                    <td width="40px">
                        <asp:Label ID="lb_code" runat="server" Text="标码" width="40px"></asp:Label>
                    </td>
                    <td width="40px">
                        <asp:TextBox ID="txt_code" runat="server" width="60px"></asp:TextBox>
                    </td>
                    <td width="40px">
                        <asp:Label ID="lb_rule" runat="server" Text="规则" width="40px"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cb_rule" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="y">年份</asp:ListItem>
                            <asp:ListItem Value="m">月份</asp:ListItem>
                            <asp:ListItem Value="d">日期</asp:ListItem>
                        </asp:CheckBoxList>

                    </td>
                    <td>
                        <asp:Button ID="btn_createnumSetting" runat="server" CssClass="SmallButton3" OnClick="btn_createnumSetting_Click"
                            TabIndex="0" Text="保存" />
                    </td>
                </tr>
            </table>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="530px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="form_id" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="form_id" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="名称" DataField="form_name" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="标码" DataField="form_size" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="年份" DataField="form_year" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="月份" DataField="form_month" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日期" DataField="form_day" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建人" DataField="form_createdname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="form_createddate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
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
