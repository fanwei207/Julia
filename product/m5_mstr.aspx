<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_mstr.aspx.cs" Inherits="m5_mstr" %>

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
                    var _src = "/product/m5_mstr_closeReason.aspx?no=" + _no;
                    $.window("变更申请单关闭原因", "50%", "50%", _src, "", true);
                    return false;
                }
            });
            //endClose
            $(".new").click(function () {
                //if (typeof ($.confirm_retValue) == "undefined" ||
                //    (typeof ($.confirm_retValue) != "undefined" && $.confirm_retValue)) {
                //    var _no = $(this).parent().parent().find(".no").html();
                //    var _src = "/product/m5_appNew.aspx?no=" + _no;
                //    $.window("变更申请试流单", "50%", "50%", _src, "", true);
                //    return false;
                //}
                var _no = $(this).parent().parent().find(".no").html();
                //var _src = "/product/m5_appNew.aspx";
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
            <table cellspacing="0" cellpadding="0" style="width: 1000px; text-align: left;">
                <%--style="width: 1200px;"--%>

                <tr>
                    <td></td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>

                </tr>
                <tr>

                    <td style="text-align: left;">Market</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlMarket" runat="server" Width="145" DataTextField="m5mk_name" DataValueField="m5mk_ID" CssClass="Param"></asp:DropDownList></td>

                    <td style="text-align: left;">Type</td>

                    <td style="text-align: left;">
                        <asp:DropDownList ID="dropProject" runat="server" DataTextField="m5p_projectEn" DataValueField="m5p_id" Width="108px" CssClass="Param">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">No.</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_no" runat="server" Width="150px" CssClass="Param"></asp:TextBox></td>
                    <td style="text-align: left;">ModelNo.</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtModelNo" runat="server" Width="80px" CssClass="Param"></asp:TextBox></td>


                    <td>Status
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatu" runat="server" Width="85px" CssClass="Param">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">In Process</asp:ListItem>
                            <asp:ListItem Value="2">Reject</asp:ListItem>
                            <asp:ListItem Value="3">Complete</asp:ListItem>
                            <asp:ListItem Value="4">Cancel</asp:ListItem>
                        </asp:DropDownList></td>

                    <td rowspan="2">

                        <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                    </td>
                      <td style="text-align: left;" rowspan="2"> 
                         BY Myself

                    </td>
                    <td style="text-align: left;" rowspan="2">
                        <asp:CheckBox ID="chkIsBySelf" runat="server" Checked="false" OnCheckedChanged="chkIsBySelf_CheckedChanged" AutoPostBack ="true" />
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 100px;"><span>Application </span>Date</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtStdDate" CssClass="Date Param" runat="server" Width="70px"></asp:TextBox>
                        -<asp:TextBox ID="txtEndDate" CssClass="Date Param" runat="server" Width="70px"></asp:TextBox></td>

                    <td>Level</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="soque_degreeName" DataValueField="soque_did" Width=" 80px"></asp:DropDownList></td>

                    <td style="text-align: left;">Content</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtDesc" runat="server" Width="150px" CssClass="Param"></asp:TextBox></td>
                  
                         <td style="text-align: left;">Apply By</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCreateName" runat="server" Width="80px" CssClass="Param"></asp:TextBox></td>
                    <td>Waiting For</td>

                    <td>

                        <asp:DropDownList ID="ddlType" runat="server" Width="80px" CssClass="Param">

                            <asp:ListItem Value="0">--All--</asp:ListItem>
                            <asp:ListItem Value="1">Review</asp:ListItem>
                            <%--    <asp:ListItem Value="2">Decision Of Verification </asp:ListItem>
                          <asp:ListItem Value="3">Verification</asp:ListItem>
                            --%>
                            <asp:ListItem Value="2">Notice</asp:ListItem>
                            <asp:ListItem Value="5">Result</asp:ListItem>
                        </asp:DropDownList></td>


                </tr>


            </table>
        </div>
        <div align="center">
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="m5_no,m5_createBy,m5_isClose,m5_closeDate,m5_apprName,m5_createName" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1500px">
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
                    <asp:TemplateField HeaderText="NO." Visible="true">
                        <ItemStyle HorizontalAlign="Center" Width="120" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="Label1" CssClass="no" runat="server" Text='<%# Bind("m5_no") %>'
                                CommandName="ViewDetail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="110"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Type" DataField="m5p_projectEn" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="About Safety" DataField="m5_isAboutSafety" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Marketing" DataField="m5_market" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Level" DataField="soque_degreeName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ModelNo." DataField="m5_modelNumber" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="AboutBoom" DataField="m5_AboutBoom" ReadOnly="True" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Notice" DataField="m5_isAgreed" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Applicant" DataField="m5_createName" ReadOnly="True"
                        DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Application Date" DataField="m5_createDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Content" DataField="m5_desc" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="450px" />
                        <ItemStyle HorizontalAlign="Left" Width="450px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Delete"><%--16--%>
                        <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Cancel"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Agree By" DataField="m5_apprNameEn" ReadOnly="True"><%--9--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Agree Date" DataField="m5_apprDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Notice By" DataField="m5_agreeNameEn" ReadOnly="True"><%--11--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Notice Date" DataField="m5_agreeDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Execute By" DataField="m5_executeName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Execute Date" DataField="m5_executeDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>


                    <%--   <asp:TemplateField HeaderText="Flow Sheet">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkNew" CssClass="new" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Detail</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>--%>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
