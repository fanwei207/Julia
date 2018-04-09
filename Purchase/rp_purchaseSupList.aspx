<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseSupList.aspx.cs" Inherits="Purchase_rp_purchaseSupList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {            
                var ID = $("#hidID").val();
            
                $(".GridViewRowStyle").dblclick(function () {
                    var ID = $(this).find("td:eq(0)").text().trim();
                    $("#hidGvID").val(ID);
                    $("#txtQAD").val($(this).find("td:eq(1)").text().trim());
                    $("#txtVend").val($(this).find("td:eq(2)").text().trim());
                    $("#txtVendName").val($(this).find("td:eq(3)").text().trim());
                    $("#txtUm").val($(this).find("td:eq(4)").text().trim());
                    $("#txtPrice").val($(this).find("td:eq(5)").text().trim());
                    $("#txtQADDesc1").val($(this).find("td:eq(6)").text().trim());
                    $("#txtQADDesc2").val($(this).find("td:eq(7)").text().trim());
                    $("#txtUses").val($(this).find("td:eq(8)").text().trim());
                    $("#txtDescript").val($(this).find("td:eq(9)").text().trim());
                    $("#txtQty").val($(this).find("td:eq(10)").text().trim());
                    $("#txtFormat").val($(this).find("td:eq(11)").text().trim());
                    $("#hidQAD").val($(this).find("td:eq(1)").text().trim());
                    $("#hidUm").val($(this).find("td:eq(4)").text().trim());
                    $("#hidPrice").val($(this).find("td:eq(5)").text().trim());
                    $("#hidDesc1").val($(this).find("td:eq(6)").text().trim());
                    $("#hidDesc2").val($(this).find("td:eq(7)").text().trim());
                    return false;
                })
                $(".GridViewAlternatingRowStyle").dblclick(function () {
                    var ID = $(this).find("td:eq(0)").text().trim();
                    $("#hidGvID").val(ID);
                    $("#txtQAD").val($(this).find("td:eq(1)").text().trim());
                    $("#txtVend").val($(this).find("td:eq(2)").text().trim());
                    $("#txtVendName").val($(this).find("td:eq(3)").text().trim());
                    $("#txtUm").val($(this).find("td:eq(4)").text().trim());
                    $("#txtPrice").val($(this).find("td:eq(5)").text().trim());
                    $("#txtQADDesc1").val($(this).find("td:eq(6)").text().trim());
                    $("#txtQADDesc2").val($(this).find("td:eq(7)").text().trim());
                    $("#txtUses").val($(this).find("td:eq(8)").text().trim());
                    $("#txtDescript").val($(this).find("td:eq(9)").text().trim());
                    $("#txtQty").val($(this).find("td:eq(10)").text().trim());
                    $("#txtFormat").val($(this).find("td:eq(11)").text().trim());
                    $("#hidQAD").val($(this).find("td:eq(1)").text().trim());
                    $("#hidUm").val($(this).find("td:eq(4)").text().trim());
                    $("#hidPrice").val($(this).find("td:eq(5)").text().trim());
                    $("#hidDesc1").val($(this).find("td:eq(6)").text().trim());
                    $("#hidDesc2").val($(this).find("td:eq(7)").text().trim());
                    return false;
                });
            
                $("#btnUpdate").click(function(){
                    //先判断是否有记录
                    if($("#hidGvID").val() == '')
                    {
                        alert('请先选择一条记录，再更新');
                        return false;
                    }
                });
                $("#txtVend").blur(function () {
                    if ($(this).val() == '') 
                    {
                        $("#txtVendName").val('');
                    }
                    else
                    {
                        search();
                    }
                });

                function search() {
                    //供应商
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "../Ajax/QAD.ashx",
                        dataType: "html",
                        //data: "qad=" + $("#txtQAD").val() + "&type=vend&vend=" + $("#txtVend").val() + "&vendName=" + $("#txtVendName").val() + "&um=" + $("#txtUm").val(),
                        data: "qad=" + $("#hidQAD").val() + "&type=vend&vend=" + $("#txtVend").val() + "&vendName=" + $("#txtVendName").val() + "&um=" + $("#hidUm").val(),
                        success: function (result) {
                            var vend = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVend").val(vend);
                            var vendname = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVendName").val(vendname);
                            var um = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            var Ptum = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            var price = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPrice").val(price);
                            $("#hidPrice").val(price);
                            var desc1 = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            var desc2 = result.substring(result.indexOf(":") + 1);
                        },
                        error: function (XMLHttpRequest, textStaus, errThrown) {
                            $("#txtPrice").val('0.00000');
                            $("#hidPrice").val('0.00000');
                        }
                    })
                }
            });
    </script>
    <style type="text/css">
        .hidden { display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td>
                    <asp:HiddenField ID="hidGvID" runat="server" />
                    <asp:TextBox ID="txtQAD" Enabled="false" Width="90px" AutoComplete="Off" CssClass="CCPPart" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtVend" Width="65px" AutoComplete="Off" CssClass="Supplier" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtVendName" Width="120px" AutoComplete="Off" CssClass="SupplierNameOutput"  runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUm" Enabled="false" Width="40px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtPrice" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQADDesc1" Enabled="false" Width="120px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQADDesc2" Enabled="false" Width="120px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUses" Enabled="false" Width="120px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtDescript" Enabled="false" Width="120px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQty" Enabled="false" Width="60px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtFormat" Enabled="false" Width="60px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="SmallButton3" Text="更新" OnClick="btnUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="Label1" Visible="false" runat="server" Text="Label"></asp:Label>
                    <asp:Button ID="btnInquiry" runat="server" CssClass="SmallButton2" Text="询价" OnClick="btnInquiry_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_Index,rp_QAD,rp_Descript,rp_Uses,rp_Format,rp_Qty,rp_Supplier,rp_SupplierName,rp_Um,rp_Price,rp_QADDesc1,rp_QADDesc2,rp_status"                      
                        AllowPaging="False" PageSize="20">
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
                                    <asp:TableCell HorizontalAlign="center" Text="DAQ" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="130px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单位" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="价格" Width="350px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述1" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述2" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="用途" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="描述" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="85px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="规格" Width="85px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle Width="10px" CssClass="hidden" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="10px" CssClass="hidden" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QAD" HeaderText="QAD">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Supplier" HeaderText="供应商">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_SupplierName" HeaderText="供应商名称">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Um" HeaderText="单位">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Price" HeaderText="价格">
                                <HeaderStyle Width="35px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="35px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc2" HeaderText="物料描述2">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Uses" HeaderText="用途">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Descript" HeaderText="描述">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Qty" HeaderText="数量">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Format" HeaderText="规格">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidQAD" runat="server" />
        <asp:HiddenField ID="hidUm" runat="server" />
        <asp:HiddenField ID="hidPtUm" runat="server" />
        <asp:HiddenField ID="hidQty" runat="server" />
        <asp:HiddenField ID="hidPrice" runat="server" />
        <asp:HiddenField ID="hidDesc1" runat="server" />
        <asp:HiddenField ID="hidDesc2" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
