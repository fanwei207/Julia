<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_fixablefee.aspx.cs" Inherits="wo2_fixablefee" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="HEAD1" runat="server">
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 686px;">
            <tr>
                <td style="width: 769px;">
                    <asp:DropDownList ID="dropYear" runat="server" Width="68px" DataTextField="ft_name"
                        DataValueField="ft_id" AutoPostBack="True" OnSelectedIndexChanged="dropYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="dropGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 769px">
                    <asp:DropDownList ID="dropType" runat="server" Width="204px" DataTextField="ft_name"
                        DataValueField="ft_id" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtAmt" runat="server" CssClass="smalltextbox Numeric" Width="100px"></asp:TextBox>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="smalltextbox Numeric" Width="104px"></asp:TextBox>
                    <asp:DropDownList ID="dropMonth" runat="server" Width="106px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">01月</asp:ListItem>
                        <asp:ListItem Value="2">02月</asp:ListItem>
                        <asp:ListItem Value="3">03月</asp:ListItem>
                        <asp:ListItem Value="4">04月</asp:ListItem>
                        <asp:ListItem Value="5">05月</asp:ListItem>
                        <asp:ListItem Value="6">06月</asp:ListItem>
                        <asp:ListItem Value="7">07月</asp:ListItem>
                        <asp:ListItem Value="8">08月</asp:ListItem>
                        <asp:ListItem Value="9">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="新增" Width="56px" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="查询" Width="56px" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 769px">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="15" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting"
                        DataKeyNames="ff_id,ff_type_id,ff_eff_month" OnRowDataBound="gv_RowDataBound"
                        OnRowCommand="gv_RowCommand" CssClass="GridViewStyle">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="ff_type_name" HeaderText="费用类别">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ff_amt" HeaderText="单只预算">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ff_total" HeaderText="预算总额">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ff_eff_date" HeaderText="生效日期">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="myEdit"
                                        Text="<u>编辑</u>"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 769px">
                    说明:1、日产量、有效工作天数的数值维护在“预算总额”里面，“单只预算”列留空<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 2、基准产品工时、AB类计时、A类计件所占的比例，维护在“单只预算”里面，“预算总额”列留空<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3、固定费用只维护“预算总额”，“单只预算”列留空<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 4、变动费用只维护“单只预算”，“预算总额”列留空
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
