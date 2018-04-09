<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_purchaseDetbyld.aspx.cs" Inherits="Purchase_rp_purchaseDet" %>

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
            var plantCode = $("#hidPlant").val();
            $("#btnSearch").click(function () {
                search();
                return false;
            });
            //
            $("#txtQAD").blur(function(){
                if($(this).val() == '')
                {
                    $("#txtVend").val('');
                    $("#txtVendName").val('');
                    $("#txtUm").val('');
                    $("#txtPrice").val('0.00000');
                    $("#txtQADDesc1").val('');
                    $("#txtQADDesc2").val('');
                }
            });
            //
            function search()
            {
                if ($("#txtQAD").val() != '') {
                    //供应商
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "../Ajax/QAD.ashx",
                        dataType: "html",
                        data: "qad=" + $("#txtQAD").val() + "&type=vend&vend=" + $("#txtVend").val(),
                        success: function (result) {
                            var vend = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVend").val(vend);
                            var vendname = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVendName").val(vendname);
                            var um = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtUm").val(um);
                            var price = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPrice").val(price);
                            var desc1 = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtQADDesc1").val(desc1);
                            var desc2 = result.substring(result.indexOf(":") + 1);
                            $("#txtQADDesc2").val(desc2);
                        },
                        error: function (XMLHttpRequest, textStaus, errThrown) {
                            //$("#txtVend").val('');
                            //$("#txtVendName").val('');
                            $("#txtUm").val('');
                            $("#txtPrice").val('0.00000');
                            $("#txtQADDesc1").val('');
                            $("#txtQADDesc2").val('');
                        }
                    })
                }
            }
            $("#txtVend").blur(function(){
                //var vend = $("#txtVend").val();
                search();
                //$(this).val(vend);
            });
            //
            $("#btnSubmit").click(function(){
                var BusDept = $("#ddlBusDept").val();
                if(BusDept == 0)
                {
                    alert("请选择业务部门");
                    return false;
                }
            });
            //
            $("#btnNew").click(function(){
                var QAD = $("#txtQAD").val();
                var Desc = $("#txtDesc").val();
                var Uses = $("#txtUses").val();
                var flag = 0;                
                if(Desc == '')
                {
                    alert("描述不能为空");
                    return false;
                }
                if(QAD != '')
                {
                    flag = 1;
                }
                if(flag == 0)
                {
                    if(Desc != '')
                    {
                        flag = 1;
                    }
                }
                if(flag == 0)
                { 
                    if(Uses != '')
                    {
                        flag = 1;
                    }
                }
                if(flag == 0)
                {
                    alert("QAD、描述或用途至少填一项");
                    return false;
                }
                var qty = $("#txtQty").val();
                if(qty != '')
                {
                    if(isNumeric(qty) != 1)
                    {
                        alert("数量必须为数字");
                        $("#txtQty").val('');
                        return false;
                    }
                }
                else
                {
                    alert("数量不能为空！");
                    return false;
                }
            });
            //查看库存
            $("#labStock").dblclick(function(){
                var site;
                if(plantCode == 1)
                {
                    site = 1000
                }
                else if(plantCode == 2)
                {
                    site = 2000
                }
                else if(plantCode == 5)
                {
                    site = 4000
                }
                else if(plantCode == 8)
                {
                    site = 5000
                }
                var param = 'qad=' + $("#txtQAD").val() + '&site=' + site
                var _src = '../Purchase/RP_ld_search.aspx?' + param;
                $.window("查看库存", "80%", "80%", _src, "", false);
                return false;
            });
        });
    </script>
    <style type="text/css">
        .title{
            background-color:#efefef;
        }
        .tdclass {
            width:130px;
            border-top:0px solid #ffffff;
            border-left:0px solid #ffffff;
            border-right:0px solid #ffffff;
        }
        /*.FixedGridTD {
            width: 50px;
            height: 25px;
            border-top:1px solid #fff;
            border-right:1px solid #fff;
        }
        .FixedGridTDLeftCorner {
         width:1px;
         border-left:1px solid #fff;
         border-bottom:1px solid #000;
        }
        .FixedGridLeft {
            width: 100px;
            text-align:center;
        }
       .FixedGridRight {
            width: 100px;
            text-align:center;
        }
        table tr {
            border-right: 1px solid #000;
            border:1px solid #fff;
        }

        table td {            
            width: 100px;
            height: 25px;
            border:1px solid #000;
            text-align: left;
            word-break: break-all;
            word-wrap: break-word;

        }
        .auto-style1 {
            width: 100px;
        }*/
        .FixedGridTD {
            width: 50px;
            height: -10px;
            border-top:none;
            border-right:none;
            border-left:none;
        }
        .NoTrHover {
            border-top:none;
            border-right:none;
            border-left:none;
        }
        .FixedGridTDLeftCorner {
         width:1px;
         border-left:1px solid #fff;
         border-bottom:1px solid #000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <%--<table style="margin-top:20px;"  width="1040px" border="1" cellspacing="0" cellpadding="1">
            <tr>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
                <td class="tdclass"></td>
            </tr>
            <tr>
                <td colspan="8" style="text-align:center; background-color:#5E8FBD;">物料信息查询</td>
            </tr>
            <tr>
                <td class="title" rowspan="2">QAD</td>
                <td rowspan="2">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td class="title">供应商代码</td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
                <td class="title">单位</td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>
                <td class="title">描述1</td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">供应商名称</td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
                <td class="title">价格</td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
                <td class="title">描述2</td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8" style="text-align:center; background-color:#5E8FBD;">物料信息</td>
            </tr>
            <tr>
                <td class="title">规格</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
                <td class="title">数量</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">描述</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox14" runat="server" TextMode="MultiLine" Width="440px" Height="50px" ></asp:TextBox>
                </td>
                <td class="title">用途</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox15" runat="server" TextMode="MultiLine" Width="440px" Height="50px" ></asp:TextBox>
                </td>
            </tr>
        </table>--%>
        <table style="margin-top:20px;"  width="90%" border="1" cellspacing="0" cellpadding="1" >
            <tr class="NoTrHover">
                <td style="width:200px;" class="FixedGridTD FixedGridTDLeftCorner"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
                <td style="width:200px;" class="FixedGridTD"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
            </tr>
          
         
            
            <tr>
                <td colspan="4" style="text-align:center; background-color:#7da7f2">采购单明细</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_Index,rp_QAD,rp_Descript,rp_Uses,rp_Format,rp_Qty,rp_Supplier,rp_SupplierName,rp_Um,rp_Price,rp_QADDesc1,rp_QADDesc2,rp_status"                      
                        AllowPaging="False" PageSize="20" OnRowDeleting="gv_RowDeleting">
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
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSinger" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                            </asp:TemplateField>
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
                             <asp:TemplateField HeaderText="领料数">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_prd_qty_dev" Text='<%# Bind("rp_Qty") %>' runat="server"
                                        CssClass="TextRight" Width="92%" Style="ime-mode: disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46) event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="rp_Format" HeaderText="规格">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="生成领料单" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidStatus" runat="server" />
        <asp:HiddenField ID="hidPlant" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
