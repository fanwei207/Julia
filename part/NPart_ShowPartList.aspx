<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_ShowPartList.aspx.cs" Inherits="part_NPart_ShowPartList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                            <asp:DropDownList ID="ddlModleList" runat="server" Width="100px" DataTextField="partTypeName" DataValueField="partTypeID" OnSelectedIndexChanged="ddlModleList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>QAD：</td>
                        <td>
                            <asp:TextBox runat="server" ID="QadNumber"></asp:TextBox>
                        </td>
                        <td>部件号：</td>
                        <td>
                            <asp:TextBox runat="server" ID="PartNumber"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="查询" Width="80px" CssClass=" SmallButton3" OnClick="btnSelect_Click"   ></asp:Button>&nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btnExport" runat="server" Text="导出" Width="80px" CssClass=" SmallButton3" OnClick="btnExport_Click"   ></asp:Button>&nbsp;&nbsp;&nbsp;                                                     
                        </td>
                        <td>
                            <table runat="server" id="tableInselect" >
                                <tr>
                                    <td>

                                    </td>
                                </tr>
                            </table>
                        </td>
                         <td>
                            
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
        <asp:HiddenField id="hidFlowID" runat="server"/>
        <asp:HiddenField ID ="hidNodeID" runat="server" />
        </div>
    </form>
</body>
</html>
