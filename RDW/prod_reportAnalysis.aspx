<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prod_reportAnalysis.aspx.cs" Inherits="RDW_prod_reportAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".chkYes").click(function () { 
                var chkYes = $(".chkYes", $(this).parent().parent()).children()[0];
                var chkNo = $(".chkNo", $(this).parent().parent()).children()[0];
               
                if($(chkYes).prop("checked")){
                    $(chkNo).prop("checked", false);
                }
            });
 
            $(".chkNo").click(function () {               
                var chkYes = $(".chkYes", $(this).parent().parent()).children()[0];
                var chkNo = $(".chkNo", $(this).parent().parent()).children()[0];
               
                if($(chkNo).prop("checked")){
                    $(chkYes).prop("checked", false);
                }
            }); 
            //end
            //$("#Button1").click(function(){
            //    $("#gv").each(function(){
            //        var $_qq =$(".chkYes",$(this).find(".chkYes")).children()[0];

            //        var qq = $_qq.prop("checked");

            //        });
            //    alert(_qq.prop("checked"));
            //    return false;
            //});



        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="height:20px;">
            <tr>
                <td style="width:150px;">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="提交"  CssClass="SmallButton3" Width="80px" OnClick="Button1_Click"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                DataKeyNames="prod_flowName,prod_flowValueYes,prod_flowValueNo" OnRowDataBound="gv_RowDataBound" 
                AllowPaging="True" PageSize="25">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="试流标准列表" Width="150px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="prod_flowName" HeaderText="试流标准">
                        <HeaderStyle Width="150px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="通过">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkYes" CssClass="chkYes" runat="server" Checked='<%# Convert.ToBoolean(Eval("prod_flowValueYes")) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="不通过">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkNo" CssClass="chkNo" runat="server" Checked='<%# Convert.ToBoolean(Eval("prod_flowValueNo")) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="通过">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEnableYes" CssClass="chkEnableYes" runat="server" Checked='<%# Convert.ToBoolean(Eval("prod_flowValueYes")) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="不通过">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEnableNo" CssClass="chkEnableNo" runat="server" Checked='<%# Convert.ToBoolean(Eval("prod_flowValueNo")) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
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
