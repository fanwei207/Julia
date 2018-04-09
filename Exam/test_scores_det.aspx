<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test_scores_det.aspx.cs" Inherits="Test_test_scores_det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#chkNotApprove").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotValid").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkNotValid").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkNotAgree").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotValid").prop("checked", false)
                    $("#chkDecideValid").prop("checked", false)
                }
            })
            $("#chkDecideValid").click(function () {
                if ($(this).prop("checked")) {
                    $("#chkNotApprove").prop("checked", false)
                    $("#chkNotValid").prop("checked", false)
                    $("#chkNotAgree").prop("checked", false)
                }
            })



            $(".close").click(function () {
                if (typeof ($.confirm_retValue) == "undefined" ||
                    (typeof ($.confirm_retValue) != "undefined" && $.confirm_retValue)) {
                    var _no = $(this).parent().parent().find(".no").html();
                    var _src = "/Supplier/FI_closeReason.aspx?no=" + _no;
                    $.window("验厂单关闭原因", "50%", "50%", _src, "", true);
                    return false;
                }
            });
          
            $(".new").click(function () {
                var _no = $(this).parent().parent().find(".no").html();
              
                var _src = "/RDW/prod_Report.aspx?from=rdw&name=" + _no;
                $.window("变更申请试流单", "90%", "95%", _src, "", true);
                return false;
            });
        }

        )

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="left">
            <table cellspacing="0" cellpadding="0" style="width: 1100px;">
                
                <tr>
                    
                   
                    <td rowspan="2" style="text-align:left;width:100px;" >
                        <asp:Button ID="btnback" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnback_Click" />
                     
                    </td>   
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="ques_id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1000px">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />    
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Width="100%" HorizontalAlign="center">无数据</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                   
                    <asp:BoundField HeaderText="题目" DataField="ques_title" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="选择" DataField="answer" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="答案" DataField="markanswer" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>



                     <asp:BoundField HeaderText="是否正确" DataField="istrue" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="得分" DataField="scores" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="分值" DataField="scores1" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>

                   
                    <asp:TemplateField HeaderText="报表">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>

                   
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>