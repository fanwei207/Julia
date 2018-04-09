<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseEquipment.aspx.cs" Inherits="Purchase_rp_purchaseEquipment" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
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
                $("#txtPtUm").val($(this).find("td:eq(4)").text().trim());
                $("#txtUm").val($(this).find("td:eq(5)").text().trim());
                $("#txtQty").val($(this).find("td:eq(6)").text().trim());
                $("#txtPrice").val($(this).find("td:eq(7)").text().trim());
                $("#txtQADDesc1").val($(this).find("td:eq(8)").text().trim());
                $("#txtQADDesc2").val($(this).find("td:eq(9)").text().trim());
                $("#txtUses").val($(this).find("td:eq(10)").text().trim());
                $("#txtDescript").val($(this).find("td:eq(11)").text().trim());
                $("#txtFormat").val($(this).find("td:eq(12)").text().trim());
                $("#hidVend").val($(this).find("td:eq(2)").text().trim());
                $("#hidVendName").val($(this).find("td:eq(3)").text().trim());
                $("#hidPrice").val($(this).find("td:eq(7)").text().trim());
                $("#hidDesc1").val($(this).find("td:eq(8)").text().trim());
                $("#hidDesc2").val($(this).find("td:eq(9)").text().trim());
                return false;
            })
            $(".GridViewAlternatingRowStyle").dblclick(function () {
                var ID = $(this).find("td:eq(0)").text().trim();
                $("#hidGvID").val(ID);
                $("#txtQAD").val($(this).find("td:eq(1)").text().trim());
                $("#txtVend").val($(this).find("td:eq(2)").text().trim());
                $("#txtVendName").val($(this).find("td:eq(3)").text().trim());
                $("#txtPtUm").val($(this).find("td:eq(4)").text().trim());
                $("#txtUm").val($(this).find("td:eq(5)").text().trim());
                $("#txtQty").val($(this).find("td:eq(6)").text().trim());
                $("#txtPrice").val($(this).find("td:eq(7)").text().trim());
                $("#txtQADDesc1").val($(this).find("td:eq(8)").text().trim());
                $("#txtQADDesc2").val($(this).find("td:eq(9)").text().trim());
                $("#txtUses").val($(this).find("td:eq(10)").text().trim());
                $("#txtDescript").val($(this).find("td:eq(11)").text().trim());
                $("#txtFormat").val($(this).find("td:eq(12)").text().trim());
                $("#hidVend").val($(this).find("td:eq(2)").text().trim());
                $("#hidVendName").val($(this).find("td:eq(3)").text().trim());
                $("#hidPrice").val($(this).find("td:eq(7)").text().trim());
                $("#hidDesc1").val($(this).find("td:eq(8)").text().trim());
                $("#hidDesc2").val($(this).find("td:eq(9)").text().trim());
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

            $("#txtQAD").blur(function () {
                if ($("#txtQAD").val() != '') 
                {
                    //范蔚要求不要超过18位即可
                    //if ($("#txtQAD").val().length != 15 && $("#txtQAD").val().length != 14) 
                    if ($("#txtQAD").val().length > 18) 
                    {
                        alert("QAD号位数不能超过18位");
                        return false;
                    }
                    else 
                    {                    
                        //判断是否是5-8的物料
                        var first = $("#txtQAD").val().substring(0,1);
                        if (first <= 4) 
                        {
                            alert('零星采购只允许采购5-8的物料');
                            $("#txtQAD").val('');
                            return false;
                        }
                    }
                }
                else
                {
                    $("#txtPrice").text('0.00000');
                    $("#txtQADDesc1").text('');
                    $("#txtQADDesc2").text('');
                    $("#hidPrice").val('0.00000');
                    $("#hidDesc1").val('');
                    $("#hidDesc2").val('');
                }
                search();
            });
            function search() {
                if ($("#txtQAD").val() != '') 
                {
                    //供应商
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "../Ajax/QAD.ashx",
                        dataType: "html",
                        data: "qad=" + $("#txtQAD").val() + "&type=vend&vend=" + $("#hidVend").val() + "&vendName=" + $("#hidVendName").val() + "&um=" + $("#hidUm").val(),
                        //data: "qad=" + $(".cssQAD").val() + "&type=vend&vend=" + $(".cssVend").val() + "&vendName=" + $(".cssVendName").val() + "&um=" + $(".cssUm").text(),
                        success: function (result) {
                            var vend = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            //$(".cssVend").val(vend);
                            var vendname = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            //$(".cssVendName").val(vendname);
                            var um = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            //$(".cssUm").text(um);
                            //$("#hidUm").val(um);
                            var Ptum = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPtUm").text(Ptum);
                            $("#hidPtUm").val(Ptum);
                            var price = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPrice").text(price);
                            $("#hidPrice").val(price);
                            var desc1 = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtQADDesc1").text(desc1);
                            $("#hidDesc1").val(desc1);
                            var desc2 = result.substring(result.indexOf(":") + 1);
                            $("#txtQADDesc2").text(desc2);
                            $("#hidDesc2").val(desc2);
                        },
                        error: function (XMLHttpRequest, textStaus, errThrown) {
                            //$(".cssVend").val('');
                            //$(".cssVendName").val('');
                            //$("#txtPtUm").text('EA');
                            $("#txtPrice").text('0.00000');
                            $("#txtQADDesc1").text('');
                            $("#txtQADDesc2").text('');
                            //$("#hidPtUm").val('EA');
                            $("#hidPrice").val('0.00000');
                            $("#hidDesc1").val('');
                            $("#hidDesc2").val('');
                        }
                    })
                }
                else
                {
                    //$(".cssVend").val('');
                    //$(".cssVendName").val('');
                    //$("#txttUm").text('EA');
                    $("#txtPrice").text('0.00000');
                    $("#txtQADDesc1").text('');
                    $("#txtQADDesc2").text('');
                    //$("#hidPtUm").val('EA');
                    $("#hidPrice").val('0.00000');
                    $("#hidDesc1").val('');
                    $("#hidDesc2").val('');
                }
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
        <table>
            <tr>
                <td>
                    <asp:Label ID="labNo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hidGvID" runat="server" />
                    <asp:TextBox ID="txtQAD" Width="100px" AutoComplete="Off" CssClass="CCPPart" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtVend" Enabled="false" Width="55px" AutoComplete="Off" CssClass="Supplier" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtVendName" Enabled="false" Width="95px" AutoComplete="Off" CssClass="SupplierNameOutput"  runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUm" Enabled="false" Width="30px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtPtUm" Enabled="false" Width="30px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQty" Enabled="false" Width="40px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtPrice" Enabled="false" Width="50px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQADDesc1" Enabled="false" Width="90px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtQADDesc2" Enabled="false" Width="90px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUses" Enabled="false" Width="100px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtDescript" Enabled="false" Width="100px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtFormat" Enabled="false" Width="100px" AutoComplete="Off" runat="server"></asp:TextBox>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="SmallButton3" Text="更新" OnClick="btnUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_Index,rp_QAD,rp_Descript,rp_Uses,rp_Format,rp_Qty,rp_Supplier,rp_SupplierName,rp_Um,rp_Price,rp_QADDesc1,rp_QADDesc2,rp_status,rp_PtUm"                      
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
                                    <asp:TableCell HorizontalAlign="center" Text="QAD" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="130px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单位" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="价格" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述1" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述2" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="用途" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="描述" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="规格" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle Width="100px" CssClass="hidden" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="100px" CssClass="hidden" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QAD" HeaderText="QAD" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Supplier" HeaderText="供应商" ReadOnly="True">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_SupplierName" HeaderText="供应商名称" ReadOnly="True">
                                <HeaderStyle Width="95px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="95px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Um" HeaderText="采购单位" ReadOnly="True">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_PtUm" HeaderText="基本单位" ReadOnly="True">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Qty" HeaderText="数量" ReadOnly="True">
                                <HeaderStyle Width="40px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Price" HeaderText="价格" ReadOnly="True">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc2" HeaderText="物料描述2" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Uses" HeaderText="用途" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Descript" HeaderText="描述" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Format" HeaderText="规格" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidUm" runat="server" />
        <asp:HiddenField ID="hidPtUm" runat="server" />
        <asp:HiddenField ID="hidQty" runat="server" />
        <asp:HiddenField ID="hidPrice" runat="server" />
        <asp:HiddenField ID="hidDesc1" runat="server" />
        <asp:HiddenField ID="hidDesc2" runat="server" />
        <asp:HiddenField ID="hidVend" runat="server" />
        <asp:HiddenField ID="hidVendName" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
