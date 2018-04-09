<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustComplaint_SheetDetial.aspx.cs" Inherits="EDI_CustComplaint_SheetDetial" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户投诉-明细</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("table tr").hover(
                function () {
                    if (!$(this).hasClass("NoTrHover")) {
                        $(this).css("background-color", "#E1FCCE");
                    }
                },
                function () {
                    if (!$(this).hasClass("NoTrHover")) {
                        $(this).css("background-color", "#fff");
                    }
                }
                );
            
            //获取传递参数的信息
            var no = $("#hidNo").val();
            var cust = $("#hidCustomer").val();
            var order = $("#hidOrder").val();

            var param = "&no=" + no + "&cust=" + cust + "&order=" + order;
            //获取当前用户的信息
            var uName = $("#hiduName").val();
            var uID = $("#hiduID").val();
            var DeptSecurity = $("#hidDeptSecurity").val();
            var DeptStatus = $("#hidDeptStatus").val();
            var createBy = $("#hidcreateBy").val();
            //获取财务权限
            var finance = $("#labFinance").text();
            var financeID = $("#hidFinanceID").val();
            //获取售后服务权限
            var afterSaleService = $("#labAfterSaleService").text();
            var afterSaleServiceID = $("#hidAfterSaleServiceID").val();
            //获取讨论权限
            var talk = $("#labTalk").text();
            var talkID = $("#hidTalkID").val();

            //获取处理方案权限
            var opinion = $("#labOpinion").text();
            var opinionID = $("#hidOpinionID").val();
            //获取最终结论权限
            var finalOpin = $("#labFinalOpin").text();
            var finalOpinID = $("#hidFinalOpinID").val();
            var staus = $("#hidStaus").val();
            //基本信息
            $("#btnDeptYes").click(function(){
                if(DeptSecurity == "2")
                {
                    alert('您没有此按钮操作的权限');
                    return false;
                }
                else
                {
                    if(uID == createBy)
                    {
                        alert('创建人不能审批自己创建的表单');
                        return false;
                    }
                }
            });
            $("#btnDeptNo").click(function(){
                if(DeptSecurity == "2")
                {
                    alert('您没有此按钮操作的权限');
                    return false;
                }
                else
                {
                    if(uID == createBy)
                    {
                        alert('创建人不能审批自己创建的表单');
                        return false;
                    }
                }
            });
            $(".dept").dblclick(function(){
                if(DeptSecurity == "2")
                {
                    alert('您没有部门主管操作的权限');
                    return false;
                }
                else
                {
                    if(staus=="1")
                    {
                        alert('表单已完结');
                        return false;
                    }
                    else
                    {
                        var _src = '../rmInspection/CustComplaint_Message.aspx?dept=dept' + param;
                        $.window("部门主管", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            //财务
            $("#btnFinYes").click(function(){
                if (financeID.indexOf(";" + uID + ";") < 0) {
                    alert('您没有财务同意按钮操作的权限');
                    return false;
                }
                else
                {
                    if(DeptStatus == "0")
                    {
                        alert('主管还未确认');
                        return false;
                    }
                    else if(DeptStatus == "2")
                    {
                        alert('表单已被拒绝');
                        return false;
                    }
                }
            });
            $("#btnFinNo").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (financeID.indexOf(";" + uID + ";") < 0) 
                    {
                        alert('您没有财务拒绝按钮操作的权限');
                        return false;
                    }
                }
            });
            $(".finance").dblclick(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (financeID.indexOf(";" + uID + ";") >= 0) 
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else
                        {
                            var _src = '../rmInspection/CustComplaint_Message.aspx?dept=finance' + param;
                            $.window("财务", "80%", "80%", _src, "", true);
                            return false;
                        }
                    }
                    else 
                    {
                        alert('您没有财务操作的权限');
                        return false;
                    }
                }
            });
            //售后服务
            $("#btnAfterSaleServiceYes").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (afterSaleServiceID.indexOf(";" + uID + ";") < 0) 
                    {
                        alert('您没有售后服务同意按钮操作的权限');
                        return false;
                    }
                }
            });
            $("#btnAfterSaleServiceNo").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (afterSaleServiceID.indexOf(";" + uID + ";") < 0) 
                    {
                        alert('您没有售后服务拒绝按钮操作的权限');
                        return false;
                    }
                }
            });
            $("#btnEdit1").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if(staus=="1")
                    {
                        alert('表单已完结');
                        return false;
                    }
                    else
                    {
                        if (afterSaleServiceID.indexOf(";" + uID + ";") < 0) 
                        {
                            alert('您没有售后服务工厂修改按钮操作的权限');
                            return false;
                        }
                    }
                }
            });
            $(".afterSaleService").dblclick(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (afterSaleServiceID.indexOf(";" + uID + ";") >= 0) 
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else
                        {
                            var _src = '../rmInspection/CustComplaint_Message.aspx?dept=afterSaleService' + param;
                            $.window("售后服务", "80%", "80%", _src, "", true);
                            return false;
                        }
                    }
                    else 
                    {
                        alert('您没有售后服务操作的权限');
                        return false;
                    }
                }
            });
            //讨论
            $(".talk").dblclick(function(){ 
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (talkID.indexOf(";" + uID + ";") >= 0) 
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else
                        {
                            var _src = '../rmInspection/CustComplaint_Message.aspx?dept=talk' + param;
                            $.window("讨论区", "80%", "80%", _src, "", true);
                            return false;
                        }
                    }
                    else 
                    {
                        alert('您没有讨论区操作的权限');
                        return false;
                    }
                }
            });
            $(".talkuser").dblclick(function(){
                if (afterSaleServiceID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有添加人的权限');
                    return false;
                }
                else
                {
                    var _src = '../rmInspection/CustComplaint_TalkUsers.aspx?dept=talk' + param;
                    $.window("讨论人员", "80%", "80%", _src, "", true);
                    return false;
                }
            });
            //处理方案
            $(".opinion").dblclick(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    /*
                    if(staus=="1")
                    {
                        alert('表单已完结');
                        return false;
                    }
                    else
                    {
                        var _src = '../rmInspection/CustComplaint_Message.aspx?dept=opinion' + param;
                        $.window("意见反馈", "80%", "80%", _src, "", true);
                        return false;
                    }
                    */
                    if (opinionID.indexOf(";" + uID + ";") >= 0) 
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else
                        {
                            var _src = '../rmInspection/CustComplaint_Message.aspx?dept=opinion' + param;
                            $.window("意见反馈", "80%", "80%", _src, "", true);
                            return false;
                        }
                    }
                    else 
                    {
                        alert('您没有处理方案操作的权限');
                        return false;
                    }
                }
            });
            //出运明细
            $("#btnSidEdit").click(function(){
                if (createBy.indexOf(uID) < 0) 
                {
                    alert('您不是创建人，没有权限');
                    return false;
                }
            });
            //最终结论
            $("#btnEdit").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (finalOpinID.indexOf(";" + uID + ";") < 0) 
                    {
                        alert('您没有最终结论责任人修改按钮操作的权限');
                        return false;
                    }
                    else
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else if($("#hidFinance").val() == 0)
                        {
                            alert('财务还未同意');
                            return false;
                        }
                        else if($("#hidAfterSaleService").val() == 0)
                        {
                            alert('质量判定还未同意');
                            return false;
                        }

                    }
                }
            });
            $("#btnFinsh").click(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (finalOpinID.indexOf(";" + uID + ";") < 0) 
                    {
                        alert('您没有完结按钮操作的权限');
                        return false;
                    }
                    else
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else if($("#hidFinance").val() == 0)
                        {
                            alert('财务还未同意');
                            return false;
                        }
                        else if($("#hidAfterSaleService").val() == 0)
                        {
                            alert('质量判定还未同意');
                            return false;
                        }

                    }
                }
            });
            $(".finalOpinion").dblclick(function(){
                if(DeptStatus == "0")
                {
                    alert('主管还未确认');
                    return false;
                }
                else if(DeptStatus == "2")
                {
                    alert('表单已被拒绝');
                    return false;
                }
                else
                {
                    if (finalOpinID.indexOf(";" + uID + ";") >= 0) 
                    {
                        if(staus=="1")
                        {
                            alert('表单已完结');
                            return false;
                        }
                        else
                        {
                            var _src = '../rmInspection/CustComplaint_Message.aspx?dept=finalOpinion' + param;
                            $.window("最终结论", "80%", "80%", _src, "", true);
                            return false;
                        }
                    }
                    else 
                    {
                        alert('您没有最终结论操作的权限');
                        return false;
                    }
                }
            });
        })
    </script>
    <style type="text/css">
        .FixedGridTD {
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
        }

        table td {
            text-align: left;
            word-break: break-all;
            word-wrap: break-word;
        }
        .auto-style1 {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:10px;">
        <table border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse; width: 1000px; table-layout: fixed; margin-top:-20px;">
            <tr class="NoTrHover">
                <td class="FixedGridTD FixedGridTDLeftCorner"></td>
                <td class="auto-style1"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD"></td>
                <td class="FixedGridTD FixedGridRight"></td>
            </tr>

            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft" rowspan="9">投诉单信息
                    <br />      <br />
                    <asp:Button ID="btnExportExcel"  runat="server" Text="导出excel" Width="80px" CssClass="SmallButton2" OnClick="btnExportExcel_Click"/>
                </td>
                <td colspan="2">投诉单号</td>
                <td colspan="2">
                    <asp:Label ID="labCustCompNo" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="2">客户代码</td>
                <td colspan="2">
                    <asp:Label ID="labCust" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="2">客户名称</td>
                <td colspan="2">
                    <asp:Label ID="labCustName" runat="server" Text=""></asp:Label>
                </td>
                <td class="FixedGridRight" rowspan="9">
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="2">原订单</td>
                <td colspan="2">
                    <asp:Label ID="labOrder" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="2">Due Date</td>
                <td colspan="2">
                    <asp:Label ID="labDueDate" runat="server" Text="Label"></asp:Label>
                </td>
                <td colspan="2">Date Code</td>
                <td colspan="2">
                    <asp:Label ID="labReqDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD">
                <td colspan="2" rowspan="2">问题描述</td>
                <td colspan="10" rowspan="2">
                    <asp:Label ID="labDescribe" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD">
                <td colspan="2">附件</td>
                <td colspan="10" id="tdFile" runat="server">
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="2" rowspan="3">赔付明细</td>
                <td>1、赔款</td>
                <td colspan="9">
                    <asp:Label ID="labMoney" runat="server" style="margin-left:30px;" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td>2、赔料</td>
                <td colspan="9" style="text-align:center;">
                    <asp:GridView ID="gvPart" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,Payment_Qty
                        ,Payment_Price,Payment_Total,createBy,createName,createDate"                      
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
                                    <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料号" Width="220px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单价" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="共计" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="poLine" HeaderText="原订单行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_Site" HeaderText="原出运地">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Part" HeaderText="物料号">
                                <HeaderStyle Width="95px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="95px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Description" HeaderText="描述">
                                <HeaderStyle Width="125px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="125px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Qty" HeaderText="数量">
                                <HeaderStyle Width="30px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Price" HeaderText="单价">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Total" HeaderText="共计">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DetReqDate" HeaderText="Req Date">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DetDueDate" HeaderText="Due Date">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td>3、退换货</td>
                <td colspan="9">
                    <asp:GridView ID="gvGoods" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,Payment_Qty
                        ,Payment_Price,Payment_Total,createBy,createName,createDate,Payment_DateCode"                      
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
                                    <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料号" Width="220px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单价" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="共计" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="poLine" HeaderText="原订单行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_Site" HeaderText="原出运地">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Part" HeaderText="物料号">
                                <HeaderStyle Width="95px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="95px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Description" HeaderText="描述">
                                <HeaderStyle Width="125px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="125px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Qty" HeaderText="数量">
                                <HeaderStyle Width="30px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Price" HeaderText="单价">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Total" HeaderText="共计">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DateCode" HeaderText="DateCode">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="2">赔付总计</td>
                <td colspan="10">
                    <asp:Label ID="labPaymentTotal" runat="server" style="margin-left:30px; color:#f00;" Text="Label"></asp:Label>
                </td>
            </tr>


            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="4">部门主管</td>
                <td colspan="12" rowspan="3" class="dept" runat="server" id="tdDept">
                </td>
                <td class="FixedGridRight dept" rowspan="4">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>           
            <tr>
                <td class="FixedGridTD">
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3" align="center">
                    <asp:Button ID="btnDeptYes" runat="server" CssClass="SmallButton3" Text="同意" Width="90" OnClick="btnDeptYes_Click" />
                    <asp:Button ID="btnDeptNo" runat="server" CssClass="SmallButton3" Text="驳回" Width="90" OnClick="btnDeptNo_Click" />
                </td>
            </tr>


            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="5">讨论</td>
                <td align="center" colspan="12" rowspan="5" class="talk" runat="server" id="tdTalk"></td>
                <td class="FixedGridRight talkuser" rowspan="5">
                    <asp:Label ID="labTalk" runat="server" Text=""></asp:Label>
                    <asp:Label ID="labTalkEmail" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidTalkID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>



            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="4">财务</td>
                <td colspan="12" rowspan="3" class="finance" runat="server" id="tdFinance">
                </td>
                <td class="FixedGridRight finance" rowspan="4">
                    <asp:Label ID="labFinance" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidFinanceID" runat="server" />
                </td>
            </tr>           
            <tr>
                <td class="FixedGridTD">
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3" align="center">
                    <asp:Button ID="btnFinYes" runat="server" Text="同意" CssClass="SmallButton3" Width="90" OnClick="btnFinYes_Click" />
                    <asp:Button ID="btnFinNo" runat="server" Text="驳回" CssClass="SmallButton3" Width="90" OnClick="btnFinNo_Click" />
                </td>
            </tr>



            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="5">质量判定</td>
                <td align="center">工厂</td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlFactory" Enabled="false" runat="server">
                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="上海强凌电子有限公司"></asp:ListItem>
                        <asp:ListItem Value="2" Text="镇江强凌电子有限公司"></asp:ListItem>
                        <asp:ListItem Value="5" Text="扬州强凌电子有限公司"></asp:ListItem>
                        <asp:ListItem Value="8" Text="淮安强凌电子有限公司"></asp:ListItem>
                        <asp:ListItem Value="100" Text="其他"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="5"></td>
                <td colspan="3">
                    <asp:Button ID="btnEdit1" runat="server" Text="修改" Width="90" CssClass="SmallButton3" OnClick="btnEdit1_Click" />
                    <asp:Button ID="btnSave1" runat="server" Text="保存" Width="90" CssClass="SmallButton3" Visible="false" OnClick="btnSave1_Click" />
                </td>
                <td class="FixedGridRight afterSaleService" rowspan="5">
                    <asp:Label ID="labAfterSaleService" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidAfterSaleServiceID" runat="server" />
                </td>
            </tr>           
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="12" rowspan="3" class="afterSaleService" runat="server" id="tdAfterSaleService"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3" align="center">
                    <asp:Button ID="btnAfterSaleServiceYes" runat="server" Text="同意" Width="90px" CssClass="SmallButton3" OnClick="btnAfterSaleServiceYes_Click" />
                    <asp:Button ID="btnAfterSaleServiceNo" runat="server" Text="驳回" Width="90" CssClass="SmallButton3" OnClick="btnAfterSaleServiceNo_Click" />
                </td>
            </tr>


            



            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="5">处理方案</td>
                <td align="center" colspan="12" rowspan="5" class="opinion" runat="server" id="tdOpinion"></td>
                <td class="FixedGridRight opinion" rowspan="5">
                    <asp:Label ID="labOpinion" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidOpinionID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>


            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft" rowspan="2">出运明细</td>
                <td align="center">出运费用</td>
                <td colspan="4">
                    <asp:TextBox ID="txtSidMoney" CssClass="txtResponsiblePerson" runat="server" Width="240" Enabled="false"></asp:TextBox>
                </td>
                <td colspan="4"></td>
                <td colspan="3">
                    <asp:Button ID="btnSidEdit" runat="server" Text="修改" Width="90" CssClass="SmallButton3" OnClick="btnSidEdit_Click" />
                    <asp:Button ID="btnSidSave" runat="server" Text="保存" Width="90" CssClass="SmallButton3" Visible="false" OnClick="btnSidSave_Click"/>
                </td>
                <td class="FixedGridRight" rowspan="2">
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td align="center" style="text-align:center;" colspan="12" runat="server">
                    <asp:GridView ID="gvSID" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames=""                      
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
                                    <asp:TableCell HorizontalAlign="center" Text="出运单号" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料号" Width="220px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="出运数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="出运日期" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="SID_nbr" HeaderText="出运单号">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sid_so_line" HeaderText="行号">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sid_qad" HeaderText="物料号">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_qty_set" HeaderText="出运数量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_shipdate" HeaderText="出运日期">
                                <HeaderStyle Width="80px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="auto-style1" rowspan="5">结案意见</td>
                <td align="center">责任方</td>
                <td colspan="4">
                    <asp:TextBox ID="txtResponsiblePerson" CssClass="txtResponsiblePerson" runat="server" Width="240" Enabled="false"></asp:TextBox>
                </td>
                <td>赔付金额</td>
                <td colspan="3">
                    <asp:Label ID="labPayment" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Button ID="btnEdit" runat="server" Text="修改" Width="90" CssClass="SmallButton3" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" Width="90" CssClass="SmallButton3" Visible="false" OnClick="btnSave_Click" />
                </td>
                <td class="FixedGridRight finalOpinion" rowspan="5">
                    <asp:Label ID="labFinalOpin" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidFinalOpinID" runat="server" />
                </td>
            </tr>           
            <tr>
                <td class="FixedGridTD"></td>
                <td rowspan="3" colspan="12" class="finalOpinion" runat="server" id="tdFinalOpinion"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3">
                    <asp:Button ID="btnFinsh" runat="server" Text="完结" CssClass="SmallButton3" Width="100" OnClick="btnFinsh_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidDeptSecurity" runat="server" />
    <asp:HiddenField ID="hidDeptStatus" runat="server" />
    <asp:HiddenField ID="hidDeptStatusBy" runat="server" />
    <asp:HiddenField ID="hidDeptStatusName" runat="server" />
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidNo" runat="server" />
    <asp:HiddenField ID="hidCustomer" runat="server" />
    <asp:HiddenField ID="hidCustomerName" runat="server" />
    <asp:HiddenField ID="hidOrder" runat="server" />
    <asp:HiddenField ID="hidDescribe" runat="server" />
    <asp:HiddenField ID="hidFinance" runat="server" />
    <asp:HiddenField ID="hidFinanceBy" runat="server" />
    <asp:HiddenField ID="hidFinanceName" runat="server" />
    <asp:HiddenField ID="hidAfterSaleService" runat="server" />
    <asp:HiddenField ID="hidAfterSaleServiceBy" runat="server" />
    <asp:HiddenField ID="hidAfterSaleServiceName" runat="server" />
    <asp:HiddenField ID="hidFactory" runat="server" />
    <asp:HiddenField ID="hidResponsiblePerson" runat="server" />
    <asp:HiddenField ID="hidPayment" runat="server" />
    <asp:HiddenField ID="hidStaus" runat="server" />
    <asp:HiddenField ID="hidDetModeifyBy" runat="server" />
    <asp:HiddenField ID="hidDetModeifyName" runat="server" />
    <asp:HiddenField ID="hidcreateBy" runat="server" />
    <asp:HiddenField ID="hidcreateName" runat="server" />
    <asp:HiddenField ID="hiduID" runat="server" />
    <asp:HiddenField ID="hiduName" runat="server" />
    <asp:HiddenField ID="hidDateCode" runat="server" />
    <asp:HiddenField ID="hidDueDate" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
