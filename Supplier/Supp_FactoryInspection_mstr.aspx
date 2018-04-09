<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supp_FactoryInspection_mstr.aspx.cs" Inherits="Supplier_Supp_FactoryInspection_mstr" %>

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
        <div align="center">
            <table cellspacing="0" cellpadding="0" style="width: 1100px;">
                
                <tr>
                    <td style="text-align: left; width: 40px;">Type</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="droptype" runat="server" Width="108px" CssClass="Param" DataTextField="type" DataValueField ="type_id">
                           
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right; width: 50px;">No.</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_no" runat="server" Width="120px" CssClass="Param"></asp:TextBox></td>
                    <td style="width: 80px;">供应商代码</td>
                    <td>
                        <asp:TextBox ID="txtvent" runat="server" Width="120px" CssClass="Param"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">供应商名称</td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server" Width="120px" CssClass="Param"></asp:TextBox></td>

                    <td>状态

                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatu" runat="server" Width="85px" CssClass="Param">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">进行中</asp:ListItem>
                            <asp:ListItem Value="2">已完成</asp:ListItem>
                            <asp:ListItem Value="-1">已取消</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="FI_id,FI_createby,FI_NO" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1500px">
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
                    <asp:TemplateField HeaderText="验厂单号" Visible="true">
                        <ItemStyle HorizontalAlign="Center" Width="120" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblno" CssClass="no" runat="server" Text='<%# Bind("FI_NO") %>'
                                CommandName="ViewDetail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="110"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="验厂类型" DataField="FI_type" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商代码" DataField="FI_Vent" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商名称" DataField="FI_Name" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="验厂时间" DataField="FI_Date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
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

                    <asp:TemplateField HeaderText="取消"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="状态" DataField="FI_Status" ReadOnly="True"><%--11--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="验厂考核人" DataField="FI_agreename" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="考核时间" DataField="FI_agreedate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="创建人" DataField="FI_createName" ReadOnly="True"><%--9--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建时间" DataField="FI_createDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
