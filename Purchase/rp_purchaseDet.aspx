<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseDet.aspx.cs" Inherits="Purchase_rp_purchaseDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js?ver=1" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js?ver=1" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var plantCode = $("#hidPlant").val();
            //按钮查询触发

            $("#txtVend").change(function(){$("#txtVendName").val('');})
            
            $("#btnSearch").click(function () {
                $("#txtUmTemp").val('');
                search();
                
                searchUm();
                return false;
            });


            //$("#txtQAD").change(function()
            //{
            //    var QAD=$(this).val();
            //    if (QAD.length ==15  || QAD.length ==14)
            //    {
            //        search();
            //        searchUm();
            //    }
            //    else if($.trim(QAD)==0)
            //    {
            //        $("#txtPtUm").val('');
            //        $("#txtPrice").val('0.00000');
            //        $("#txtQADDesc1").val('');
            //        $("#txtQADDesc2").val('');
            //        $("#hidPtUm").val('');
            //        $("#hidPrice").val('0.00000');
            //        $("#hidDesc1").val('');
            //        $("#hidDesc2").val('');
            //    }
            //})

            //当ID为txtQAD的对象失去焦点的时候
            $("#txtQAD").blur(function()
            {
                
                if($(this).val() == '')
                {
                    $("#txtPtUm").val('');
                    $("#txtPrice").val('0.00000');
                    $("#txtQADDesc1").val('');
                    $("#txtQADDesc2").val('');
                    $("#hidPtUm").val('');
                    $("#hidPrice").val('0.00000');
                    $("#hidDesc1").val('');
                    $("#hidDesc2").val('');
                }
                else
                {
                    search();
                    searchUm();
                }
                return false;
            });




            function searchUm()
            {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "../Ajax/Um.ashx",
                    dataType: "html",
                    data: "qad=" + $("#txtQAD").val() + "&vend=" + $("#txtVend").val(),
                    success: function (result) {
                        $("#ddlUmTemp").empty();
                        var jsonUm=JSON.stringify(result); 
                        var objectUm=JSON.parse(result); 
                        var UmTemp = $("#ddlUmTemp");
                        var data=[{name:"a",age:12},{name:"b",age:11},{name:"c",age:13},{name:"d",age:14}];  
                        for(var o in objectUm){ 
                            var addoption=document.createElement("option");  
                            addoption.text = objectUm[o].UM;  
                            addoption.value = objectUm[o].UM;
                            //document.getElementById("ddlUmTemp").add(addoption);
                            $("#ddlUmTemp").append(addoption);
                            if($("#ddlUmTemp").val() != '其他')
                            {
                                $("#txtUmTemp").attr("disabled","disabled");
                            }
                            else
                            {
                                $("#txtUmTemp").removeAttr("disabled"); 
                            }
                        }
                    },
                    error: function (XMLHttpRequest, textStaus, errThrown) {
                        $("#txtPrice").val('0.00000');
                        $("#txtQADDesc1").val('');
                        $("#txtQADDesc2").val('');
                        $("#hidPtUm").val('');
                        $("#hidPrice").val('0.00000');
                        $("#hidDesc1").val('');
                        $("#hidDesc2").val('');
                    }
                })
            }
            //
            $("#ddlUmTemp").change(function(){
                if($(this).val() != "其他")
                {
                    $("#txtUmTemp").val('');
                    $("#txtUmTemp").val($(this).val());
                    $("#txtUmTemp").attr("disabled","disabled");
                    search();
                }
                else
                {
                    $("#txtUmTemp").val('');
                    $("#txtUmTemp").removeAttr("disabled"); 
                }
                return false;
            });
            //查询物料详细信息
            function search()
            {
                $("#hidUm").val('');
                $("#hidQty").val('');

                if ($("#txtQAD").val() != '') 
                {
                    //供应商
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "../Ajax/QAD.ashx",
                        dataType: "html",
                        beforesend: function(){
                            $("#txtPrice").val('0.00000');
                            $("#txtQADDesc1").val('');
                            $("#txtQADDesc2").val('');
                            $("#hidPtUm").val('');
                            $("#hidPrice").val('0.00000');
                            $("#hidDesc1").val('');
                            $("#hidDesc2").val('');
                        },
                        data: "qad=" + $("#txtQAD").val() + "&type=vend&vend=" + $("#txtVend").val() + "&vendName=" + $("#txtVendName").val() + "&um=" + $("#txtUmTemp").val(),
                        success: function (result) {          
                            var vend = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVend").val(vend);
                            var vendname = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVendName").val(vendname);
                            var um = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtUmTemp").val(um);
                            var ptum = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPtUm").val(ptum);
                            $("#hidPtUm").val(ptum);
                            var price = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtPrice").val(price);
                            $("#hidPrice").val(price);
                            var desc1 = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtQADDesc1").val(desc1);
                            $("#hidDesc1").val(desc1.replace(/>/g, "&gt;").replace(/</g, "&lt;"));
                            var desc2 = result.substring(result.indexOf(":") + 1);
                            $("#txtQADDesc2").val(replace(/>/g, "&gt;").replace(/</g, "&lt;"));
                            $("#hidDesc2").val(desc2);
                            
                        },
                        error: function (XMLHttpRequest, textStaus, errThrown) {
                            $("#txtPrice").val('0.00000');
                            $("#txtQADDesc1").val('');
                            $("#txtQADDesc2").val('');
                            $("#hidPtUm").val('');
                            $("#hidPrice").val('0.00000');
                            $("#hidDesc1").val('');
                            $("#hidDesc2").val('');
                        }
                    })
                }
                else
                {
                    $("#txtPtUm").val('');
                    $("#txtPrice").val('0.00000');
                    $("#txtQADDesc1").val('');
                    $("#txtQADDesc2").val('');
                    $("#hidPtUm").val('');
                    $("#hidPrice").val('0.00000');
                    $("#hidDesc1").val('');
                    $("#hidDesc2").val('');
                }
            }
            
            function searchSupplier() {
                if ($("#txtVend").val() != '')
                {
                    //供应商
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "../Ajax/QAD.ashx",
                        dataType: "html",
                        data: "qad=" + $("#txtQAD").val() + "&type=supplier&vend=" + $("#txtVend").val() + "&vendName=" + $("#txtVendName").val() + "&um=" + $("#txtUmTemp").val(),
                        success: function (result) {
                            var code = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVend").val(code);
                            var name = result.substring(result.indexOf(":") + 1);
                            result = result.substring(result.indexOf(";") + 1);
                            $("#txtVendName").val(name);
                        },
                        error: function (XMLHttpRequest, textStaus, errThrown) {
                            $("#txtVend").val('');
                            $("#txtVendName").val('');
                        }
                    })
                }
                else
                {
                    $("#txtVend").val('');
                    $("#txtVendName").val('');
                }
            }
            $("#txtVend").blur(function(){
                search();
            });
            //当ID为txtUmTemp的对象失去焦点的时候
            $("#txtUmTemp").blur(function(){
                search();
            });
            //按钮提交触发
            $("#btnSubmit").click(function(){
                var BusDept = $("#ddlBusDept").val();
                if(BusDept == 0)
                {
                    alert("请选择业务部门");
                    return false;
                }
            });
            //按钮保存触发
            $("#btnSave").click(function(){
                var BusDept = $("#ddlBusDept").val();
                if(BusDept == 0)
                {
                    alert("请选择业务部门");
                    return false;
                }
            });
            //按钮新增触发
            $("#btnNew").click(function(){
                $("#hidUm").val('');
                $("#hidQty").val('');
                      
                var QAD = $("#txtQAD").val();
                var Desc = $("#txtDesc").val();
                var Uses = $("#txtUses").val();
                var flag = 0; 
                var UmTemp = $("#txtUmTemp").val();
                $("#hidUm").val($("#txtUmTemp").val());
                $("#hidQty").val($("#txtQtyTemp").val());
                if(QAD != '')
                {
                    if(QAD.length !=15 && QAD.length !=14)
                    {
                        alert("QAD号位数不正确");
                        return false;
                    }
                    else 
                    {                    
                        //判断是否是5-8的物料
                        var first = QAD.substring(0,1);
                        if (first <= 4) 
                        {
                            alert('零星采购只允许采购5-8的物料');
                            return false;
                        }
                    }
                }
                if(UmTemp == '')
                {
                    alert("采购单位不能为空");
                    return false;
                }
                else
                {
                    var myReg = /^[\u4e00-\u9fa5]+$/;
                    if(UmTemp.length > 2)
                    {
                        alert("单位不能超过2位");
                        return false;
                    }
                    else
                    {
                        //判断是否包含数字
                        var first = UmTemp.substring(0,1);
                        var second = UmTemp.substring(1,3);
                        if (myReg.test(first)) 
                        {
                            $("#hidUm").val('');
                        }
                        else
                        {
                            if(second != '')
                            {
                                if (myReg.test(second)) 
                                {
                                    $("#hidUm").val('');
                                }
                            }
                        }
                    }
                }
                if(Desc == '')
                {
                    alert("描述不能为空");
                    return false;
                }
                var qty = $("#txtQtyTemp").val();
                if(qty != '')
                {
                    if(isNumeric(qty) != 1)
                    {
                        alert("数量必须为数字");
                        $("#txtQtyTemp").val('');
                        return false;
                    }
                    else
                    {
                        $("#hidQty").val($("#txtQtyTemp").val());
                    }
                }
                else
                {
                    alert("数量不能为空！");
                    return false;
                }
            });
            //采购单位不能是中文
            //$("#txtUmTemp").blur(function(){
            //    var myReg = /^[\u4e00-\u9fa5]+$/;
            //    if($(this).val() != '')
            //    {
            //        if (myReg.test($(this).val())) 
            //        {
            //            alert("单位不能是中文");
            //            $(this).val('');
            //            return false;
            //        }
            //        else
            //        {
                    
            //        }
            //    }
            //});
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
        td{
            border:solid #808080; 
            border-width:0px 1px 1px 0px;
        }
        table{
            border:solid #808080; 
            border-width:1px 0px 0px 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:20px;"  width="90%" border="1" cellspacing="0" cellpadding="1" >
            <tr class="NoTrHover">
                <td style="width:200px;" class="FixedGridTD FixedGridTDLeftCorner"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
                <td style="width:200px;" class="FixedGridTD"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
            </tr>
            <tr>
                <td style="text-align:center; font-size:20px; background-color:#a9b6f7;" colspan="4">采购申请单</td>
            </tr>
            <tr>
                <td class="title">采购单号</td>
                <td>
                    <asp:Label ID="labNo" runat="server" Text=""></asp:Label>
                </td>
                <td class="title">业务部门</td>
                <td>
                    <asp:DropDownList ID="ddlBusDept" runat="server" DataTextField="departmentname" DataValueField="departmentid" CssClass="SmallTextBox5">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:center; background-color:#a9b6f7;" colspan="4">附件</td>
            </tr>
            <tr>
                <td style="text-align:center;" colspan="4">
                    <input id="filename" runat="server" style="width:580px;" name="resumename"  CssClass="SmallTextBox5"  type="file"/>
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="上传" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center;" colspan="4">
                    <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_fileName,rp_filePath,createBy,createName,createDate"
                        OnRowCommand="gvFile_RowCommand" OnRowDeleting="gvFile_RowDeleting"
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
                                    <asp:TableCell HorizontalAlign="center" Text="文件名" Width="500px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传日期" Width="320px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建人" Width="200px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="rp_FileName" HeaderText="文件名">
                                <HeaderStyle Width="420px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="420px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                <HeaderStyle Width="200px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createName" HeaderText="创建人">
                                <HeaderStyle Width="200px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="查看" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center; background-color:#a9b6f7;">物料信息查询</td>
                <td colspan="2" style="text-align:center; background-color:#a9b6f7;">物料信息</td>
            </tr>
            <tr>
                <td class="title">QAD</td>
                <td>
                    <asp:TextBox ID="txtQAD" CssClass="CCPPart" AutoComplete="Off" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="labStock" runat="server" ForeColor="red" Text="查看库存"></asp:Label>
                </td>
                <td class="title" rowspan="3">描述</td>
                <td rowspan="3">
                    <asp:TextBox ID="txtDesc" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="400px" Height="80px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">供应商</td>
                <td>
                    <asp:TextBox ID="txtVend" runat="server" AutoComplete="Off"  CssClass="Supplier"></asp:TextBox>
                </td>
            </tr>
            <tr><!--供应商名称的 edit 不可以写成false  -->
                <td class="title">供应商名称</td>
                <td>
                    <asp:TextBox ID="txtVendName" AutoComplete="Off"  CssClass="SupplierNameOutput" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">采购单位</td>
                <td>
                    <asp:DropDownList ID="ddlUmTemp" Width="70px" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlUmTemp_SelectedIndexChanged"></asp:DropDownList>
                    <asp:TextBox ID="txtUmTemp" AutoComplete="Off" Width="80px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtChangeUm" AutoComplete="Off" Visible="false" Width="80px" runat="server"></asp:TextBox>
                </td>
                <td class="title" rowspan="3">用途</td>
                <td rowspan="3">
                    <asp:TextBox ID="txtUses" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="400px" Height="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">基本单位</td>
                <td>
                    <asp:TextBox ID="txtPtUm" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">价格(含税)</td>
                <td>
                    <asp:TextBox ID="txtPrice" Enabled="false" runat="server" Text="0.00000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">描述1</td>
                <td>
                    <asp:TextBox ID="txtQADDesc1" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="title">规格</td>
                <td>
                    <asp:TextBox ID="txtFormat" AutoComplete="Off" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">描述2</td>
                <td>
                    <asp:TextBox ID="txtQADDesc2" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="title">数量</td>
                <td>
                    <asp:TextBox ID="txtQtyTemp" AutoComplete="Off" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
            
            <tr>
                <td colspan="4" style="text-align:center; background-color:#a9b6f7;">采购单明细</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_Index,rp_QAD,rp_Descript,rp_Uses,rp_Format,rp_QtyTemp,rp_Supplier,rp_SupplierName,rp_UmTemp,rp_Price,rp_QADDesc1,rp_QADDesc2,rp_status,rp_PtUm"                      
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
                            <asp:BoundField DataField="rp_UmTemp" HeaderText="采购单位">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_PtUm" HeaderText="基本单位">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Price" HeaderText="价格">
                                <HeaderStyle Width="35px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="35px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1" HtmlEncode="False" HtmlEncodeFormatString="False">
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
                            <asp:BoundField DataField="rp_QtyTemp" HeaderText="数量">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Format" HeaderText="规格">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="SmallButton2" Text="提交" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnBack_Click" /><br />
                    <b style="color:red;">提交后不允许在修改，请确保填写无误后再提交</b>
                </td>
            </tr>
             <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:TextBox ID="txtRejection" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox>

                </td>

            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidStatus" runat="server" />
        <asp:HiddenField ID="hidPlant" runat="server" />
        <asp:HiddenField ID="hidPtUm" runat="server" />
        <asp:HiddenField ID="hidPrice" runat="server" />
        <asp:HiddenField ID="hidDesc1" runat="server" />
        <asp:HiddenField ID="hidDesc2" runat="server" />
        <asp:HiddenField ID="hidQty" runat="server" />
        <asp:HiddenField ID="hidUm" runat="server" />        
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
