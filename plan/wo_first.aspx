<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_first.aspx.cs" Inherits="plan_wo_first" %>

<!DOCTYPE html>
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
                        <table border="0" cellpadding="0" cellspacing="0" width="780px">
                <tr>
                    <td>加工单号:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Height="20px" Width="130px"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>批量:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtqty" runat="server" CssClass="SmallTextBox" Height="20px" Width="100px"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>产品名称:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPartname" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="150px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">日期:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date" Height="20px" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>样品数量:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtsample" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="100px" ></asp:TextBox>
                    </td>
                    <td>确认工序:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlprocess" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlprocess_SelectedIndexChanged">
                            <asp:ListItem>组装</asp:ListItem>
                            <asp:ListItem>线路板</asp:ListItem>
                            <asp:ListItem>包装</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>机号:
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtnum" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="150px"></asp:TextBox>
                    </td>

                    <td style="text-align: left;">申请人:
                    </td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server" CssClass="SmallTextBox" Height="20px" Width="100px"></asp:TextBox>
                    </td>
                </tr>

            <tr>
              <td style="text-align: left;">确认信息:
                    </td>
                    <td style="text-align: left;" colspan="5">
                        <asp:TextBox ID="txtul" runat="server" CssClass="SmallTextBox" Height="20px" Width="321px"></asp:TextBox>
                    </td>
                    <td style="text-align: right;" colspan="2">
                        <asp:Button ID="btn_back" runat="server" Text="返回" CssClass="smallbutton2" OnClick="btn_back_Click"
                             Width="80px" />
                    </td>
            </tr>
            </table>
            <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="wofd_addr,wofd_confirm,wofd_num,wofd_remark,part,ps_level,ps__log01" 
                 Width="780px" ShowFooter="True" OnRowDataBound="gvlist_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" Height="30px" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical" Width="780px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="不良率" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="生产时间" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="每小时产量" Width="100px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="测试不良" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="调光不良" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="备注" Width="80px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="number" HeaderText="序号">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="part" HeaderText="物料号">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="desc1" HeaderText="描述">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="供应商">
                        <ItemTemplate>

                            <asp:DropDownList ID="ddladdr" runat="server" Width="100%" Enabled="false"></asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Right" Width="200px" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="批号">
                        <ItemTemplate>

                            <asp:TextBox ID="txtnum" runat="server" Width="100%" Enabled="false"></asp:TextBox>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Right" Width="150px" VerticalAlign="Middle" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>

                            <asp:TextBox ID="txtremark" runat="server" Width="100%" Enabled="false"></asp:TextBox>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Right" Width="150px" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Select" runat="server" Width="20px"/>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
