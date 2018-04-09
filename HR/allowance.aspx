<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allowance.aspx.cs" Inherits="Wage.hr_allowance" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table3" cellpadding="0" cellspacing="0" width="780px" runat="server">
            <tr>
                <td style="width: 4021px; height: 28px;">
                    年月
                </td>
                <td style="width: 97px; height: 28px;">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" Width="40px" AutoPostBack="True"
                        Font-Size="10pt" CssClass="smallbutton2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 38px; height: 28px">
                    部门
                </td>
                <td style="width: 26px; height: 28px">
                    <asp:DropDownList ID="dropDept" runat="server" TabIndex="2" Width="120px" DataTextField="Name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td style="width: 106px; height: 28px">
                    职位
                </td>
                <td style="width: 58px; height: 28px">
                    <asp:DropDownList ID="dropRole" runat="server" TabIndex="3" Width="120px" DataTextField="RoleName"
                        DataValueField="RoleID">
                    </asp:DropDownList>
                </td>
                <td style="width: 17827px; height: 28px">
                    津贴类型<asp:DropDownList ID="dropType" runat="server" Width="120px" AutoPostBack="True"
                        OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        <asp:ListItem Value="0">其他津贴</asp:ListItem>
                        <asp:ListItem Value="1">独身子女津贴</asp:ListItem>
                        <asp:ListItem Value="2">高温费</asp:ListItem>
                        <asp:ListItem Value="3">招工津贴</asp:ListItem>
                        <asp:ListItem Value="4">新工人补贴</asp:ListItem>
                        <asp:ListItem Value="5">学生津贴</asp:ListItem>
                        <asp:ListItem Value="6">奖励</asp:ListItem>
                        <asp:ListItem Value="7">手机费贴</asp:ListItem>
                        <asp:ListItem Value="8">公里贴</asp:ListItem>
                        <asp:ListItem Value="9">装箱贴</asp:ListItem>
                        <asp:ListItem Value="10">出差贴</asp:ListItem>
                        <asp:ListItem Value="11">值班贴</asp:ListItem>
                        <asp:ListItem Value="12">岗位贴</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 4021px; height: 24px;">
                    工号
                </td>
                <td style="width: 97px; height: 24px;">
                    <asp:TextBox ID="txtUserNo" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtUserNo_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 38px; height: 24px;">
                    姓名
                </td>
                <td style="width: 26px; height: 24px;">
                    <asp:TextBox runat="server" ID="txtName" Width="60px" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 106px; height: 24px;">
                    津贴金额
                </td>
                <td style="width: 58px; height: 24px;">
                    <asp:TextBox ID="txtAmount" CssClass="SmallTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td rowspan="2" style="width: 17827px">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>
                    &nbsp;<asp:Button ID="btnSave" runat="server" Width="60px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    &nbsp;<asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" 
                        Text="Excel" Width="60px" onclick="ButExcel_Click"></asp:Button>
                    <asp:Label ID="lblAllowID" Visible="False" runat="server">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 4021px; height: 24px;">
                    备注
                </td>
                <td colspan="5" style="height: 24px">
                    <asp:TextBox ID="txtCom" runat="server" Width="536px" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgAllow" runat="server" Width="1190px" PageSize="16" AutoGenerateColumns="False"
            AllowPaging="True" OnItemDataBound="dgAllow_ItemDataBound" OnCancelCommand="dgAllow_CancelCommand"
            OnEditCommand="dgAllow_EditCommand" OnPageIndexChanged="dgAllow_PageIndexChanged"
            CssClass="GridViewStyle AutoPageSize" DataKeyField="ID" OnDeleteCommand="dgAllow_DeleteCommand"
            OnUpdateCommand="dgAllow_UpdateCommand">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn HeaderText="序号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Amount" HeaderText="金额">
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="comment" SortExpression="comment" HeaderText="备注">
                    <ItemStyle HorizontalAlign="Left" Width="700px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
