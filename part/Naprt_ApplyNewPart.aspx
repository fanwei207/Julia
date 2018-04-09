<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Naprt_ApplyNewPart.aspx.cs" Inherits="part_Naprt_ApplyNewPart" %>

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
                            <asp:DropDownList ID="ddlModleList" runat="server" Width="100px" DataTextField="partTypeName" DataValueField="partTypeID" ></asp:DropDownList>
                        </td>
                        <td>状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" >
                                <asp:ListItem Value="0" Text="可提交" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="10" Text="已提交"></asp:ListItem>
                                <asp:ListItem Value="-10" Text="被拒绝"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="查询" Width="80px" CssClass=" SmallButton3" OnClick="btnSelect_Click"   ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="提交" Width="80px" CssClass=" SmallButton3" OnClick="btnSubmit_Click" ></asp:Button>
                        </td>
                         <td>
                            <asp:Button ID="btnExport" runat="server" Text="导出拒绝" Width="80px" CssClass=" SmallButton3" OnClick="btnExport_Click"   ></asp:Button>
                        </td>
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
        </div>
    </form>
</body>
</html>
