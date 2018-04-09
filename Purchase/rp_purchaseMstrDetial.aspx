<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseMstrDetial.aspx.cs" Inherits="Purchase_rp_purchaseMstrDetial" %>

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
            
            $(".GridViewRowStyle").dblclick(function () {
                var qad = $(this).find("td:eq(0)").text().trim();//.html();
                var plantCode = $("#hidPlant").val();
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
                var param = 'qad=' + qad + '&site=' + site
                var _src = '../Purchase/RP_ld_search.aspx?' + param;
                $.window("查看库存", "80%", "80%", _src, "", false);
                return false;
            })
            $(".GridViewAlternatingRowStyle").dblclick(function () {
                var qad = $(this).find("td:eq(0)").text().trim();
                var plantCode = $("#hidPlant").val();
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
                var param = 'qad=' + qad + '&site=' + site
                var _src = '../Purchase/RP_ld_search.aspx?' + param;
                $.window("查看库存", "80%", "80%", _src, "", false);
                return false;
            });

            var ID = $("#hidID").val();
            var No = $("#labNo").text();
            var BusDept = $("#ddlBusDept").val();
            var param = "&No=" + No + "&ID=" + ID + "&BusDept=" + BusDept;
            //绑定状态
            var DeptStatus = $("#hidDeptStatus").val();
            var DeptStatusBy = $("#hidDeptStatusBy").val();
            var DeptStatusName = $("#hidDeptStatusName").val();
            var BusDeptUserStatus = $("#hidBusDeptUserStatus").val();
            var BusDeptUserStatusBy = $("#hidBusDeptUserStatusBy").val();
            var BusDeptUserStatusName = $("#hidBusDeptUserStatusName").val();
            var BusDeptStatus = $("#hidBusDeptStatus").val();
            var BusDeptStatusBy = $("#hidBusDeptStatusBy").val();
            var BusDeptStatusName = $("#hidBusDeptStatusName").val();
            var EquipmentStatus = $("#hidEquipmentStatus").val();
            var EquipmentStatusBy = $("#hidEquipmentStatusBy").val();
            var EquipmentStatusName = $("#hidEquipmentStatusName").val();
            var SupplierStatus = $("#hidSupplierStatus").val();
            var SupplierStatusBy = $("#hidSupplierStatusBy").val();
            var SupplierStatusName = $("#hidSupplierStatusName").val();
            var CEOStatus = $("#hidCEOStatus").val();
            var CEOStatusBy = $("#hidCEOStatusBy").val();
            var CEOStatusName = $("#hidCEOStatusName").val();
            var Status = $("#hidStatus").val();
            //绑定权限
            var hidBusDeptUserID = $("#hidBusDeptUserID").val();
            var hidBusDeptID = $("#hidBusDeptID").val();
            var hidEquipmentID = $("#hidEquipmentID").val();
            var hidSupplierID = $("#hidSupplierID").val();
            var hasQad = $("#hidHasQad").val();
            var hidCEOID = $("#hidCEOID").val();
            var hasCEO = $("#hasCEO").val();
            //获取当前用户的信息
            var uName = $("#hiduName").val();
            var uID = $("#hiduID").val();
            //获取创建者的信息
            var createBy = $("#hidCreateBy").val();
            //获取部门主管信息
            var DeptID = $("#hidDeptID").val();
            //
            $(".vend").dblclick(function(){ 
                if (hidSupplierID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有供应商开发部的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，不能进行操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，不能进行操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行操作");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseSupList.aspx?ID=' + ID;
                        $.window("供应商询价查询", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $(".busDeptUser").dblclick(function(){
                if (hidBusDeptUserID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务部门的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，不能进行操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行操作");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseBusDeptUserList.aspx?ID=' + ID;
                        $.window("业务部查询", "90%", "90%", _src, "", true);
                        return false;
                    }
                }
            });
            $(".cssEquipment").dblclick(function(){
                if (hidEquipmentID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有设备部的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，不能进行操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行操作");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseEquipment.aspx?ID=' + ID;
                        $.window("设备部", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            //$(".purchase").dblclick(function(){                
            //    var _src = '../Purchase/rp_purchaseList.aspx?ID=' + ID;
            //    $.window("采购明细查询", "80%", "80%", _src, "", true);
            //    return false;
            //});
            //部门会签
            $(".dept").dblclick(function(){
                if (DeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有部门主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行留言");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，不能进行留言");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("部门主管已签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=dept' + param;
                        $.window("部门会签", "80%", "80%", _src, "", true);
                        return false;
                    }
                    //if (createBy.indexOf(uID) >= 0) 
                    //{
                    //    alert('创建人不能操作自己创建的表单');
                    //    return false;
                    //}
                    //else
                    //{
                    //}
                }
            });
            $("#btnDeptYes").click(function () {
                if (DeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有部门主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("部门主管已签字，按钮不能操作");
                        return false;
                    }
                    //if (createBy.indexOf(uID) >= 0) 
                    //{
                    //    alert('创建人不能操作自己创建的表单');
                    //    return false;
                    //}
                    //else
                    //{
                    //}
                }
            });
            $("#btnDeptBack").click(function () {
                if (DeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有部门主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("部门主管已签字，按钮不能操作");
                        return false;
                    }
                    //if (createBy.indexOf(uID) >= 0) 
                    //{
                    //    alert('创建人不能操作自己创建的表单');
                    //    return false;
                    //}
                    //else
                    //{
                    //}
                }
            });

            //业务部门
            $(".cbusDeptUser").dblclick(function(){
                if (hidBusDeptUserID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务部门的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行留言");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，不能进行留言");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=busDeptUser' + param;
                        $.window("业务部门", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $("#btnBusDeptUserSubmit").click(function(){
                if (hidBusDeptUserID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务部门的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            $("#btnBusDeptUserBack").click(function(){
                if (hidBusDeptUserID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务部门的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务部门已提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            //业务主管
            $(".busDept").dblclick(function(){
                if (hidBusDeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行留言");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行留言");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=busDept' + param;
                        $.window("业务主管", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $("#btnBusDeptYes").click(function(){
                if (hidBusDeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            $("#btnBusDeptBack").click(function(){
                if (hidBusDeptID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("业务主管已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            //设备部
            $(".equipment").dblclick(function(){
                if (hidEquipmentID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，不能进行留言");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行留言");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=equipment' + param;
                        $.window("设备部", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $("#btnEquipmentYes").click(function(){
                if (hidEquipmentID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            $("#btnEquipmentBack").click(function(){
                if (hidEquipmentID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有业务主管的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("设备部已同意，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            //供应商开发部
            $(".supplier").dblclick(function(){
                if (hidSupplierID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有供应商开发部的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行留言");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=supplier' + param;
                        $.window("供应商", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $("#btnSupplierBack").click(function(){
                if (hidSupplierID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有供应商开发部的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '60')
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad == 1)
                    {
                        alert("供应商开发部已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            //副总会签
            $(".ceo").dblclick(function(){
                if (hidCEOID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有副总的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，不能进行留言");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("供应商开发部还未签字，不能进行留言");
                        return false;
                    }
                    if(hasQad != 1)
                    {
                        alert("供应商开发部还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，不能进行留言");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，不能进行留言");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，不能进行留言");
                        return false;
                    }
                    else
                    {
                        var _src = '../Purchase/rp_purchaseMessage.aspx?dept=ceo' + param;
                        $.window("副总", "80%", "80%", _src, "", true);
                        return false;
                    }
                }
            });
            $("#btnCEOYse").click(function(){
                if (hidCEOID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有副总的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("供应商开发部还未签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad != 1)
                    {
                        alert("供应商开发部还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
            $("#btnCEOBack").click(function(){
                if (hidCEOID.indexOf(";" + uID + ";") < 0) 
                {
                    alert('您没有副总的权限');
                    return false;
                }
                else
                {
                    if(Status == '70')
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(hasCEO == 1)
                    {
                        alert("副总已签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '50')
                    {
                        alert("供应商开发部还未签字，按钮不能操作");
                        return false;
                    }
                    if(hasQad != 1)
                    {
                        alert("供应商开发部还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '40')
                    {
                        alert("设备部还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '30')
                    {
                        alert("业务主管还未签字，按钮不能操作");
                        return false;
                    }
                    if(Status == '20')
                    {
                        alert("业务部门还未提交，按钮不能操作");
                        return false;
                    }
                    if(Status == '10')
                    {
                        alert("部门主管还未签字，按钮不能操作");
                        return false;
                    }
                }
            });
        });
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
        .title{
            background-color:#efefef;
            /*color:#8e90ee;*/
        }
        .auto-style2 {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:10px;">
        <table border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse; width: 1100px; table-layout: fixed; margin-top:-20px;">
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
                <td class="FixedGridLeft title" rowspan="3">采购单信息</td>
                <td class="title">采购单号</td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="labNo" runat="server" Text=""></asp:Label>
                </td>
                <td class="title">业务部门</td>
                <td colspan="2" style="text-align:center;">                    
                    <asp:DropDownList ID="ddlBusDept" runat="server" Enabled="false" DataTextField="departmentname" DataValueField="departmentid" CssClass="SmallTextBox5">
                    </asp:DropDownList>
                </td>
                <td class="title">申请人</td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="labCreate" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="title">申请部门</td>
                <td colspan="2" style="text-align:center;">
                    <asp:Label ID="labDeptName" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="createDeptID" runat="server" />
                </td>
                <td class="FixedGridRight" rowspan="3">
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="12" runat="server" id="tdFile"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="12">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,rp_No,rp_Index,rp_QAD,rp_Descript,rp_Uses,rp_Format,rp_Qty,rp_Supplier,rp_SupplierName,rp_Um,rp_Price,rp_QADDesc1,rp_QADDesc2,rp_status"                      
                        AllowPaging="False" PageSize="20" OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
                         OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound">
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
                            <asp:TemplateField HeaderText="QAD" >
                                <EditItemTemplate>
                                <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox CCPPart cssQAD"  Text='<%# Bind("rp_QAD") %>'
                                    Width="65px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="65px" />
                                <ItemTemplate>
                                    <%#Eval("rp_QAD")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtVend" runat="server" CssClass="SmallTextBox Supplier cssVend" Text='<%# Bind("rp_Supplier") %>'
                                    Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <%#Eval("rp_Supplier")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商名称">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtVendName" runat="server" CssClass="SmallTextBox cssVendName" Text='<%# Bind("rp_SupplierName") %>'
                                    Width="95px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="95px" />
                                <ItemTemplate>
                                    <%#Eval("rp_SupplierName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_Um" HeaderText="单位" ReadOnly="True">
                                <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="单位">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtUm" runat="server" CssClass="SmallTextBox cssUm" Text='<%# Bind("rp_Um") %>'
                                    Width="30px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <%#Eval("rp_Um")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rp_Qty" HeaderText="数量" ReadOnly="True">
                                <HeaderStyle Width="40px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="rp_Price" HeaderText="价格" ReadOnly="True">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="价格">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="SmallTextBox cssPrice" Text='<%# Bind("rp_Price") %>'
                                    Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <%#Eval("rp_Price")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="物料描述1">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtQADDesc1" runat="server" CssClass="SmallTextBox cssQADDesc1" Text='<%# Bind("rp_QADDesc1") %>'
                                    Width="90px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <%#Eval("rp_QADDesc1")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_QADDesc2" HeaderText="物料描述2" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="物料描述2">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtQADDesc2" runat="server" CssClass="SmallTextBox cssQADDesc2" Text='<%# Bind("rp_QADDesc2") %>'
                                    Width="90px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <%#Eval("rp_QADDesc2")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rp_Uses" HeaderText="用途" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Descript" HeaderText="描述" ReadOnly="True">
                                <HeaderStyle Width="90px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="规格" >
                                <EditItemTemplate>
                                <asp:TextBox ID="txtFormat" runat="server" CssClass="SmallTextBox" Text='<%# Bind("rp_Format") %>'
                                    Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <%#Eval("rp_Format")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>



            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">部门会签</td>
                <td colspan="12" rowspan="3" class="dept" runat="server" id="tdDept"></td>
                <td class="FixedGridRight" rowspan="4">
                    <asp:HiddenField ID="hidDeptID" runat="server" />
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
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <asp:Button ID="btnDeptYes" CssClass="SmallButton2" runat="server" Width="90" Text="同意" OnClick="btnDeptYes_Click" />&nbsp;
                    <asp:Button ID="btnDeptBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnDeptBack_Click" />
                </td>
            </tr>



            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">业务部门</td>
                <td colspan="12" rowspan="3" class="cbusDeptUser" runat="server" id="tdBusDeptuser"></td>
                <td class="FixedGridRight busDeptUser" rowspan="4">
                    <asp:Label ID="labBusDeptUser" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidBusDeptUserID" runat="server" />
                    <asp:HiddenField ID="hidBusDeptUserEmail" runat="server" />
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
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <asp:Button ID="btnBusDeptUserSubmit" CssClass="SmallButton2" runat="server" Width="90" Text="提交" OnClick="btnBusDeptUserSubmit_Click" />&nbsp;
                    <asp:Button ID="btnBusDeptUserBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnBusDeptUserBack_Click" />
                </td>
            </tr>


            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">业务主管</td>
                <td colspan="12" rowspan="3" class="busDept" runat="server" id="tdBusDept"></td>
                <td class="FixedGridRight" rowspan="4">
                    <asp:Label ID="labBusDept" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidBusDeptID" runat="server" />
                    <asp:HiddenField ID="hidBusDeptEmail" runat="server" />
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
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <asp:Button ID="btnBusDeptYes" CssClass="SmallButton2" runat="server" Width="90" Text="同意" OnClick="btnBusDeptYes_Click" />&nbsp;
                    <asp:Button ID="btnBusDeptBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnBusDeptBack_Click" />
                </td>
            </tr>




            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">设备部</td>
                <td colspan="12" rowspan="3" class="equipment" runat="server" id="tdEquipment"></td>
                <td class="FixedGridRight cssEquipment" rowspan="4">
                    <asp:Label ID="labEquipment" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="hidEquipmentID" runat="server" />
                    <asp:HiddenField ID="hidEquipmentEmail" runat="server" />
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
                <td colspan="9" class="auto-style2"></td>
                <td colspan="3" style="text-align:center;" class="auto-style2">
                    <asp:Button ID="btnEquipmentYes" CssClass="SmallButton2" runat="server" Width="90" Text="同意" OnClick="btnEquipmentYes_Click" />&nbsp;
                    <asp:Button ID="btnEquipmentBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnEquipmentBack_Click" />
                </td>
            </tr>


            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">供应商开发部</td>
                <td colspan="12" rowspan="3" class="supplier" runat="server" id="tdSupplier"></td>
                <td class="FixedGridRight vend" rowspan="4">
                    <asp:Label ID="labSupplier" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="hidSupplierID" runat="server" />
                    <asp:HiddenField ID="hidSupplierEmail" runat="server" />
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
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <%--<asp:Button ID="btnSupplierYes" CssClass="SmallButton2" runat="server" Width="90" Text="同意" />--%>&nbsp;
                    <asp:Button ID="btnSupplierBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnSupplierBack_Click" />
                </td>
            </tr>



            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft title" rowspan="4">副总会签</td>
                <td colspan="12" rowspan="3" class="ceo" runat="server" id="tdCEO"></td>
                <td class="FixedGridRight" rowspan="4">
                    <asp:Label ID="labCEO" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="hidCEOID" runat="server" />
                    <asp:HiddenField ID="hidCEOEmail" runat="server" />
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
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <asp:Button ID="btnCEOYse" CssClass="SmallButton2" runat="server" Width="90" Text="同意" OnClick="btnCEOYse_Click" />&nbsp;
                    <asp:Button ID="btnCEOBack" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" OnClick="btnCEOBack_Click" />
                </td>
            </tr>



            
<%--            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft" rowspan="3">采购部</td>
                <td colspan="12" rowspan="3" class="purchase"></td>
                <td class="FixedGridRight purchase" rowspan="3">
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>--%>
            <%--<tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3" style="text-align:center;">
                    <asp:Button ID="Button11" CssClass="SmallButton2" runat="server" Width="90" Text="同意" />&nbsp;
                    <asp:Button ID="Button12" CssClass="SmallButton2" runat="server" Width="90" Text="驳回" />
                </td>
            </tr>--%>

        </table>
    
    </div>
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidCreateBy" runat="server" />
        <asp:HiddenField ID="hidCreateEmail" runat="server" />
        <asp:HiddenField ID="hidCreatePlant" runat="server" />
        <asp:HiddenField ID="hidNo" runat="server" />
        <asp:HiddenField ID="hidDeptStatus" runat="server" />
        <asp:HiddenField ID="hidDeptStatusBy" runat="server" />
        <asp:HiddenField ID="hidDeptStatusName" runat="server" />
        <asp:HiddenField ID="hidBusDeptStatus" runat="server" />
        <asp:HiddenField ID="hidBusDeptStatusBy" runat="server" />
        <asp:HiddenField ID="hidBusDeptStatusName" runat="server" />
        <asp:HiddenField ID="hidBusDeptUserStatus" runat="server" />
        <asp:HiddenField ID="hidBusDeptUserStatusBy" runat="server" />
        <asp:HiddenField ID="hidBusDeptUserStatusName" runat="server" />
        <asp:HiddenField ID="hidEquipmentStatus" runat="server" />
        <asp:HiddenField ID="hidEquipmentStatusBy" runat="server" />
        <asp:HiddenField ID="hidEquipmentStatusName" runat="server" />
        <asp:HiddenField ID="hidSupplierStatus" runat="server" />
        <asp:HiddenField ID="hidSupplierStatusBy" runat="server" />
        <asp:HiddenField ID="hidSupplierStatusName" runat="server" />
        <asp:HiddenField ID="hidCEOStatus" runat="server" />
        <asp:HiddenField ID="hidCEOStatusBy" runat="server" />
        <asp:HiddenField ID="hidCEOStatusName" runat="server" />
        <asp:HiddenField ID="hidStatus" runat="server" />
        <asp:HiddenField ID="hiduID" runat="server" />
        <asp:HiddenField ID="hiduName" runat="server" />
        <asp:HiddenField ID="hidEmail" runat="server" />
        <asp:HiddenField ID="hidPlant" runat="server" />
        <asp:HiddenField ID="hidHasQad" runat="server" />
        <asp:HiddenField ID="hasCEO" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
