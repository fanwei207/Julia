﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_partApplyForCommon.aspx.cs" Inherits="part_Npart_partApplyForCommon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //$(function () {
        //    $("input[name$='chkAll']:eq(1)").remove();
        //    $("#chkAll").click(function () {
        //        $("#gvDet input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
        //    })
        //})

        $(function () {
            $("input[name='gvDet$ctl01$chkAll']:eq(1)").remove();
            $("#gvDet_ctl01_chkAll").click(function () {
                $("#gvDet input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"));
                $("#gvDet input[type='checkbox'][id$='chk']").each(GetSelectedIndex);

                event.stopPropagation();
            })
            $("#gvDet input[type='checkbox'][id$='chk']").change(GetSelectedIndex)
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div align="left">
                <table>
                    <tr>
                        <td>模板：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlModleList" runat="server" Width="100px" DataTextField="partTypeName" DataValueField="partTypeID"></asp:DropDownList>
                        </td>

                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="查询" Width="80px" CssClass=" SmallButton3" OnClick="btnSelect_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="通过" Width="80px" CssClass=" SmallButton3" OnClick="btnSubmit_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRefuse" runat="server" Text="拒绝" Width="80px" CssClass=" SmallButton3" OnClick="btnRefuse_Click"></asp:Button>
                        </td>
                        <td></td>
                        <td></td>
                    </tr> 


                </table>
                <asp:GridView ID="gvDet" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="GridViewStyle GridViewRebuild" EmptyDataText="No data">
                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <Columns>
                    </Columns>
                </asp:GridView>



            </div>
            <script type="text/javascript">
                <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
            </script>
            <asp:HiddenField ID="hidFlowID" runat="server" />
            <asp:HiddenField ID="hidNodeID" runat="server" />
            <asp:HiddenField ID ="hidModleID" runat="server" />
        </div>
    </form>
</body>
</html>
