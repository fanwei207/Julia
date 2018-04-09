<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_newApply.aspx.cs" Inherits="Supplier_supplier_newApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        
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
                });
            var url=window.location.href;//获取当前的URL
            url=url.replace(/[^a-z0-9]/gi,"");//用正则清除字符串中的所有非字母和数字的内容
            if($.cookie(url)=="" || $.cookie(url)==null){
                
                if ($("#hidTabIndex").val() == '')
                {
                    $("#hidTabIndex").val(0);
                }
            }
            var _index = $("#hidTabIndex").val();

            var $tabs = $("#divTabs").tabs({ active: _index });

            //CSS权限
            var FileQualificationSecurity = $("#hidFileQualificationSecurity").val();
            var FileQualificationOpinionSecurity = $("#hidFileQualificationOpinionSecurity").val();
            var LeaderSecurity = $("#hidLeaderSecurity").val();
            var ManageSecurity = $("#hidManageSecurity").val();
            var SuppOpinionSecurity = $("#hidSuppOpinionSecurity").val();
            var LawOpinionSecurity = $("#hidLawOpinionSecurity").val();
            var FinanceOpinionSecurity = $("#hidFinanceOpinionSecurity").val();
            var FormalSecurity = $("#hidFormalFileSecurity").val();
            var FISecurity = $("#hidFISecurity").val();
            var SignFileSecurity = $("#hidSignFileSecurity").val();
            if(FileQualificationSecurity == '0')
            {
                $("#trFileQualificationSecurity").hide();
            }
            if(SuppOpinionSecurity == '0')
            {
                $("#trSuppDeptUpload").hide();
            }
            //获取HID的值
            var LeaderIsAgree = $("#hidLeaderIsAgree").val();
            var SignFileIsAgree = $("#hidSignFileIsAgree").val();
            var FLIsAgree = $("#hidFLIsAgree").val();
            var SuppDept = $("#hidSuppDept").val();
            var LawDept = $("#hidLawDept").val();
            var FinanceDept = $("#hidFinanceDept").val();
            var flStatus = $("#hidFLStatus").val();
            var manageIsAgree = $("#hidManageIsAgree").val();
            var username = $("#hidUserName").val();
            var _username = $("#labLeaderUsername").text();
            var deptname = $("#hidDeptName").val();
            var _labSuppDeptName= $("#labSuppDeptName").text();
            var labSuppDeptName= $("#labAPPDeptName").text();
            var SupplierStatus = $("#hidSupplierStatus").val();
            var IsFL = $("#hidIsFL").val();
            var SupplierNumSecurity = $("#hidSupplierNumSecurity").val();
            
            //主管签字
            $("#newReason").dblclick(function () {
                $("#hidTabIndex").val(0);
                if(LeaderSecurity == '0')
                {

                    alert("您没有部门主管签字的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == "1")
                        {

                            alert("主管签字已同意");
                            return;
                        }
                        else if(LeaderIsAgree == "2")
                        {
                            alert("主管签字已拒绝");
                            return;
                        }
                        else
                        {
                            var _src = "../Supplier/supplier_SignOfLeader.aspx?type=Leader&typeID=0&no=" + $("#hidSupplierID").val();
                            $.window("主管签字", 550, 300, _src,"",true);
                        }
                    }
                }
            })
            $("#SuppSignFile").dblclick(function () {
                $("#hidTabIndex").val(0);
                if(FileQualificationOpinionSecurity == '0')
                {
                    alert("您没有供应商资质文件评估的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == "0")
                        {
                            alert("主管还未签字");
                            return;
                        }
                        else if(SignFileIsAgree == "1")
                        {
                            alert("供应商资质文审核已同意");
                            return false;
                        }
                        else if(SignFileIsAgree == "2")
                        {
                            alert("供应商资质文审核已拒绝");
                            return false;
                        }
                        else
                        {
                            var _src = "../Supplier/supplier_SignOfLeader.aspx?level=" + $("#hidLevelID").val() + "&type=SignFile&typeID=0&no=" + $("#hidSupplierID").val();
                            $.window("供应商资质文件评估", 550, 300, _src,"",true);
                        }
                    }
                }
            });
            $("#FLOpinion").dblclick(function () {
                $("#hidTabIndex").val(0);
                if(FISecurity == '0')
                {
                    alert("您没有填写验厂意见的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == "0")
                        {
                            alert("主管还未签字");
                            return;
                        }
                        else
                        {
                            if(SignFileIsAgree == "0")
                            {
                                alert("供应商资质文件还未审核");
                                return false;
                            }
                            else if(FLIsAgree == "1")
                            {
                                alert("验厂意见已同意");
                                return false;
                            }
                            else if(FLIsAgree == "2")
                            {
                                alert("验厂意见已拒绝");
                                return false;
                            }
                            else
                            {
                                if(IsFL == '0')
                                {
                                    alert('还未决定是否验厂！');
                                    return false;
                                }
                                else if(IsFL == '1')//要验厂
                                {
                                    if(flStatus != '2')
                                    {
                                        alert('验厂还未完成！');
                                        return false;
                                    }
                                    else
                                    {
                                        var _src = "../Supplier/supplier_SignOfLeader.aspx?type=FL&typeID=0&no=" + $("#hidSupplierID").val();
                                        $.window("验厂意见", 550, 300, _src,"",true);
                                    }
                                }
                                else if(IsFL == '2')//要验厂
                                {
                                    var _src = "../Supplier/supplier_SignOfLeader.aspx?type=FL&typeID=0&no=" + $("#hidSupplierID").val();
                                    $.window("验厂意见", 550, 300, _src,"",true);
                                }
                            }
                        }
                    }
                }
            });
            $("#SuppDeptOpinion").dblclick(function () {
                $("#hidTabIndex").val(2);
                if(SuppOpinionSecurity == '0')
                {
                    alert("您没有填写供应商开发部意见的权限");
                    return false;
                }
                else
                {
                    if(flStatus == '0')
                    {
                        if(IsFL == '1')
                        {
                            alert("验厂还未结束");
                            return false;
                        }
                    }
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if (manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                        else if(SuppDept == '1')
                        {
                            alert("供应商开发部意见已提交");
                            return false;
                        }
                        else if(SuppDept == '2')
                        {
                            alert("供应商开发部意见已拒绝");
                            return false;
                        }
                        else
                        {
                            var _src = "../Supplier/supplier_SignOfLeader1.aspx?type=SuppDept&typeID=1&no=" + $("#hidSupplierID").val()
                                +"&successEmailTo=" + $("#hidLawPersonEmail").val()+"&FailEmailTo=" + $("#hidCreatedEmail").val() ;
                            $.window("供应商开发部意见", 550, 300, _src,"",true);
                        }
                    }
                }
            });
            $("#LawDeptOpinion").dblclick(function () {
                $("#hidTabIndex").val(2);
                if(LawOpinionSecurity == '0')
                {
                    alert("您没有填写法务部意见的权限");
                    return false;
                }
                else
                {
                    if(flStatus == '0')
                    {
                        if(IsFL == '1')
                        {
                            alert("验厂还未结束");
                            return false;
                        }
                        else if (SuppDept == '0')
                        {
                            alert("供应商开发部意见还未填写确定");
                            return false;
                        }
                        else if (SuppDept == '2')
                        {
                            alert("供应商开发部意见已拒绝");
                            return false;
                        }
                    }
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                        else if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                        else if(LawDept == '1')
                        {
                            alert("法务部意见已提交");
                            return false;
                        }
                        else if(LawDept == '2')
                        {
                            alert("法务部意见已拒绝");
                            return false;
                        }
                        else
                        {
                            var _src = "../Supplier/supplier_SignOfLeader1.aspx?type=LawDept&typeID=1&no=" + $("#hidSupplierID").val()
                             +"&successEmailTo=" + $("#hidFinPersonEmail").val()+"&FailEmailTo=" + $("#hidSupplierPersonEmail").val() ;
                            $.window("法务部意见", 550, 300, _src,"",true);
                        }
                    }
                }
            });
            $("#FinanceDeptOpinion").dblclick(function () {
                $("#hidTabIndex").val(2);
                if(FinanceOpinionSecurity == '0')
                {
                    alert("您没有填写财务部意见的权限");
                    return false;
                }
                else
                {
                    if(flStatus == '0')
                    {
                        if(IsFL == '1')
                        {
                            alert("验厂还未结束");
                            return false;
                        }
                        else if (SuppDept == '0')
                        {
                            alert("供应商开发部意见还未填写确定");
                            return false;
                        }
                        else if (SuppDept == '2')
                        {
                            alert("供应商开发部意见已拒绝");
                            return false;
                        }
                        else if (LawDept == '0')
                        {
                            alert("法务部意见还未填写确定");
                            return false;
                        }
                        else if (LawDept == '2')
                        {
                            alert("法务部意见已驳回");
                            return false;
                        }
                    }
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                        else if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                        else if(FinanceDept == '1')
                        {
                            alert("财务部意见已提交");
                            return false;
                        }
                        else if(FinanceDept == '2')
                        {
                            alert("财务部意见已拒绝");
                            return false;
                        }
                        else
                        {
                            var _src = "../Supplier/supplier_SignOfLeader1.aspx?type=FinanceDept&typeID=1&no=" + $("#hidSupplierID").val()
                            +"&successEmailTo=" + $("#hidManagerEmail").val()+"&FailEmailTo=" + $("#hidSupplierPersonEmail").val() ;
                            $.window("财务部意见", 550, 300, _src,"",true);
                        }
                    }
                }
            });
            $("#ManageOpinion").dblclick(function () {
                $("#hidTabIndex").val(0);
                if(ManageSecurity == '0')
                {
                    alert("您没有填写总经理意见的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == "0")
                        {
                            alert("主管还未签字");
                            return false;
                        }
                        else
                        {
                            if(SignFileIsAgree == "0")
                            {
                                alert("供应商资质文件还未审核");
                                return false;
                            }
                            if(flStatus == '0')
                            {
                                if(IsFL == '1')
                                {
                                    alert("验厂还未结束");
                                    return false;
                                }
                            }
                            if(FLIsAgree == "0")
                            {
                                alert("验厂意见还未确认");
                                return false;
                            }
                            else
                            {
                                if(SuppDept != '1' || LawDept != '1' || FinanceDept != '1')
                                {
                                    alert('供应商开发部、法务部或财务部还未签字通过');
                                    return false;
                                }
                                else if(manageIsAgree == '1')
                                {
                                    alert("总经理意见已提交");
                                    return false;
                                }
                                else if(manageIsAgree == '2')
                                {
                                    alert("总经理意见已拒绝");
                                    return false;
                                }
                                else
                                {
                                    var _src = "../Supplier/supplier_SignOfLeader.aspx?type=Manage&typeID=0&no=" + $("#hidSupplierID").val();
                                    $.window("总经理意见", 550, 300, _src,"",true);
                                }
                            }
                        }
                    }
                }
            });
            $("#trSupplierNum").dblclick(function(){
                $("#hidTabIndex").val(0);
                if(SupplierStatus == '1')
                {
                    alert("此申请单已提交，不允许任何操作");
                    return false;
                }
                else
                {
                    if(LeaderIsAgree == "0")
                    {
                        alert("主管还未签字");
                        return false;
                    }
                    else
                    {
                        if(SignFileIsAgree == "0")
                        {
                            alert("供应商资质文件还未审核");
                            return false;
                        }
                        else
                        {
                            if(flStatus == '0')
                            {
                                if(IsFL == '1')
                                {
                                    alert("验厂还未结束");
                                    return false;
                                }
                            }
                            if(FLIsAgree == "0")
                            {
                                alert("验厂意见还未确认");
                                return false;
                            }
                            if(SuppDept != '1' || LawDept != '1' || FinanceDept != '1')
                            {
                                alert('供应商开发部、法务部或财务部还未签字通过');
                                return false;
                            }
                            else
                            {
                                if(manageIsAgree == '0')
                                {
                                    alert('总经理还未签字');
                                    return false;
                                }
                                if(SupplierNumSecurity == '0')
                                {
                                    alert('您没有填写供应商代码的权限');
                                    return false;
                                }
                                else
                                {
                                    var _src = "../Supplier/supplier_SignOfLeader.aspx?type=SupplierNum&typeID=0&no=" + $("#hidSupplierID").val();
                                    $.window("供应商代码", 550, 300, _src,"",true);
                                }
                            }
                        }
                    }
                }
            });

            $("#links").click(function(){                
                var _src = "http://www.sgs.gov.cn";
                //用户当前浏览器的高度和宽度的90%
                $.window("全国企业信用信息公示系统", window.screen.width * 0.9, window.screen.height * 0.9, _src,"",false);
            });
            $("#txtFileTypeName").hide();
            $("#ddlFileType").change(function(){
                var fileType = $("#ddlFileType").val();
                if (fileType == 'c22f72aa-cc32-4653-b399-aadd39ab63a7')
                {
                    $("#txtFileTypeName").show();
                }
                else
                {
                    $("#txtFileTypeName").hide();
                }
            });
            $("#liFileQualification").click(function(){
                if (LeaderIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("主管还未签字");
                    return;
                }
            });
            $("#liSignFile").click(function(){
                if (LeaderIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("主管还未签字");
                    return;
                }
                else if (SignFileIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("供应商资质文件评估还未审核");
                    return;
                }
                else if(IsFL == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert('还未决定是否验厂！');
                    return false;
                }
            });
            $("#liFormalFile").click(function(){
                if (LeaderIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("主管还未签字");
                    return;
                }
                else if (SignFileIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("供应商资质文件评估还未审核");
                    return;
                }
                else if(IsFL == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert('还未决定是否验厂！');
                    return false;
                }
                else if(SuppDept == '2')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert('供应商开发部已拒绝此申请单');
                    return false;
                
                }
                else if (manageIsAgree == '0')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("总经理还未签字");
                    return;
                }
                else if (manageIsAgree == '2')
                {
                    var _index = $("#hidTabIndex").val();

                    var $tabs = $("#divTabs").tabs({ active: _index });
                    alert("总经理已拒绝此申请单");
                    return;
                }
            });
            //按钮判断
            $("#btnIsFI").click(function(){
                if(SupplierStatus == '1')
                {
                    alert("此申请单已提交，不允许任何操作");
                    return false;
                }
                else
                {
                    if(LeaderIsAgree == '0')
                    {
                        alert('主管还未签字！');
                        return false;
                    }
                    else if(SignFileIsAgree == "0")
                    {
                        alert("供应商资质文件还未审核");
                        return false;
                    }
                }
            });
            $("#btnIsNotFI").click(function(){
                if(SupplierStatus == '1')
                {
                    alert("此申请单已提交，不允许任何操作");
                    return false;
                }
                else
                {
                    if(LeaderIsAgree == '0')
                    {
                        alert('主管还未签字！');
                        return false;
                    }
                    else if(SignFileIsAgree == "0")
                    {
                        alert("供应商资质文件还未审核");
                        return false;
                    }
                }
            });
            $("#btnLeaderYes").click(function(){
                if(LeaderSecurity == '0')
                {
                    alert("您没有部门主管签字按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        //if(labSuppDeptName.indexOf(deptname) < 0)
                        //{ 
                        //    alert("您不在" + _labSuppDeptName + ",所以没有权限操作此按钮");
                        //    return false;
                        //}
                    }
                }
            });
            $("#btnLeaderNo").click(function(){
                if(LeaderSecurity == '0')
                {
                    alert("您没有部门主管签字按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(labSuppDeptName.indexOf(deptname) < 0)
                        { 
                            alert("您不在" + _labSuppDeptName + ",所以没有权限操作此按钮");
                            return false;
                        }
                    }
                }
            });
            $("#btnSignFileYes").click(function(){
                if(FileQualificationOpinionSecurity == '0')
                {
                    alert("您没有供应商资质文件评估按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                    }
                }
            });
            $("#btnSignFileNo").click(function(){
                if(FileQualificationOpinionSecurity == '0')
                {
                    alert("您没有供应商资质文件评估按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                    }
                }
            });
            $("#btnFLYes").click(function(){
                if(FISecurity == '0')
                {
                    alert("您没有验厂意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                        else if(SignFileIsAgree == "0")
                        {
                            alert("供应商资质文件还未审核");
                            return false;
                        }
                        else if(IsFL == '0')
                        {
                            alert('还未决定是否验厂！');
                            return false;
                        }
                        else if(IsFL == '1')
                        {
                            if(flStatus != '2')
                            {
                                alert('验厂还未完成！');
                                return false;
                            }
                        }
                        //待商议
                        //else if(IsFL == '2')
                        //{
                        //    if(SuppDept != '1' && LawDept != '1' &&FinanceDept != '1')
                        //    {
                        //        alert('文件签署流程还未完成！');
                        //        return false;
                        //    }
                        //}
                    }
                }
            });
            $("#btnFLNo").click(function(){
                if(FISecurity == '0')
                {
                    alert("您没有验厂意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                        else if(SignFileIsAgree == "0")
                        {
                            alert("供应商资质文件还未审核");
                            return false;
                        }
                        else if(IsFL == '0')
                        {
                            alert('还未决定是否验厂！');
                            return false;
                        }
                        else if(IsFL == '1')
                        {
                            if(flStatus != '2')
                            {
                                alert('验厂还未完成！');
                                return false;
                            }
                        }
                    }
                }
            });
            $("#btnManageYes").click(function(){
                if(ManageSecurity == '0')
                {
                    alert("您没有填写总经理意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                        else if(SignFileIsAgree == "0")
                        {
                            alert("供应商资质文件还未审核");
                            return false;
                        }
                        else if(IsFL == '0')
                        {
                            alert("还未确定是否要验厂");
                            return false;
                        }
                        else if(IsFL == '1')
                        {
                            if(flStatus == '0')
                            {
                                alert("验厂还未结束");
                                return false;
                            }                        
                        }
                        else if(FLIsAgree == "0")
                        {
                            alert("供应商开发部验厂意见还未确认");
                            return false;
                        }
                        else if(SuppDept != '1' || LawDept != '1' || FinanceDept != '1')
                        {
                            alert('供应商开发部、法务部或财务部还未签字通过');
                            return false;
                        }
                    }
                }
            });
            $("#btnManageNo").click(function(){
                if(ManageSecurity == '0')
                {
                    alert("您没有填写总经理意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(LeaderIsAgree == '0')
                        {
                            alert('主管还未签字！');
                            return false;
                        }
                        else if(SignFileIsAgree == "0")
                        {
                            alert("供应商资质文件还未审核");
                            return false;
                        }
                        else if(IsFL == '0')
                        {
                            alert("还未确定是否要验厂");
                            return false;
                        }
                        else if(IsFL == '1')
                        {
                            if(flStatus == '0')
                            {
                                alert("验厂还未结束");
                                return false;
                            }                        
                        }
                        else if(FLIsAgree == "0")
                        {
                            alert("供应商开发部验厂意见还未确认");
                            return false;
                        }
                        else if(SuppDept != '1' || LawDept != '1' || FinanceDept != '1')
                        {
                            alert('供应商开发部、法务部或财务部还未签字通过');
                            return false;
                        }
                    }
                }
            });
            $("#btnSuppDeptYes").click(function(){
                if(SuppOpinionSecurity == '0')
                {
                    alert("您没有填写供应商开发部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                    }
                }
            });
            $("#btnSuppDeptNo").click(function(){
                if(SuppOpinionSecurity == '0')
                {
                    alert("您没有填写供应商开发部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                    }
                }
            });
            $("#btnLawDeptYes").click(function(){
                if(LawOpinionSecurity == '0')
                {
                    alert("您没有填写法务部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                        else if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                    }
                }
            });
            $("#btnLawDeptNo").click(function(){
                if(LawOpinionSecurity == '0')
                {
                    alert("您没有填写法务部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                        else if(manageIsAgree == '2')
                        {
                            alert("总经理已拒绝此申请单");
                            return false;
                        }
                    }
                }
            });
            $("#btnFinanceDeptYes").click(function(){
                if(FinanceOpinionSecurity == '0')
                {
                    alert("您没有填写财务部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                    }
                }
            });
            $("#btnFinanceDeptNo").click(function(){
                if(FinanceOpinionSecurity == '0')
                {
                    alert("您没有填写财务部意见按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                    else
                    {
                        if(SuppDept == '2')
                        {
                            alert('供应商开发部已拒绝此申请单');
                            return false;
                        }
                    }
                }
            });
            $("#btnSupplierNum").click(function(){
                if(SupplierStatus == '1')
                {
                    alert("此申请单已提交，不允许任何操作");
                    return false;
                }
                else
                {
                    if(LeaderIsAgree == '0')
                    {
                        alert('主管还未签字！');
                        return false;
                    }
                    else if(SignFileIsAgree == "0")
                    {
                        alert("供应商资质文件还未审核");
                        return false;
                    }
                    if(IsFL == '1')
                    {
                        if(flStatus != '2')
                        {
                            alert("验厂还未结束");
                            return false;
                        }
                    } 
                    if(IsFL == '0')
                    {
                        alert("还未进行验厂表决");
                        return false;
                    }
                    if(FLIsAgree == "0")
                    {
                        alert("供应商开发部验厂意见还未确认");
                        return false;
                    }
                    if($("#labSupplierNum").text() == '')
                    {
                        alert("财务部还未填写供应商代码");
                        return false;
                    }
                }
            });
            $("#Button3").click(function(){
                if(FormalSecurity == '0')
                {
                    alert("您没有上传正式合同按钮的权限");
                    return false;
                }
            });
            $("#Button10").click(function(){
                if(SignFileSecurity == '0')
                {
                    alert("您没有上传签署文件按钮的权限");
                    return false;
                }
                else
                {
                    if(SupplierStatus == '1')
                    {
                        alert("此申请单已提交，不允许任何操作");
                        return false;
                    }
                }
            });
            $("#btnFileSubmit").click(function(){
                var _index = $(this).find('div').val();
                $("#hidTabIndex").val(_index)
                //var $tabs = $("#divTabs").tabs({ active: _index }); 
                if(FileQualificationSecurity == '0')
                {
                    alert("您没有上传供应商资质文件评估按钮的权限");
                    return false;
                }
                //else
                //{
                //    $.ajax({
                //        type:"Post",
                //        url: "../supplier_newApply.aspx?&no=" + $("#hidSupplierID").val(),
                //        // data: "{'token':'ajax'}",// 使用这种方式竟然无法传递参数，各位有知道原因的告诉一下啊。
                //        data:"type=file",
                //        success: function (data) {
                //            $("#tdFQ").text(data); 
                //        }
                //    });
                //}
            });
            //弹窗
            $("#linkFI").click(function(){
                var src = "../supplier/FI_view.aspx?suppno=" + $("#hidSupplierID").val();
                $.window("验厂详单", window.screen.width * 0.9, window.screen.height * 0.9, src,"",false);
            });
        })
        //输入验证
        //function validate(){
         
        //    if($.trim($('#txtCurr').val()!= 'rmb')||$.trim($('#txtCurr').val() != 'usd'))
        //    {
        //        alert("币种，请输入rmb/usd");
        //        return false;
        //    }
        //    if($.trim($('#txtPaymentDays').val() != '0')||$.trim($('#txtPaymentDays').val() != '30')|| $.trim($('#txtPaymentDays').val() != '60')|| $.trim($('#txtPaymentDays').val() != '90'))
        //    {
        //        alert("账期，请输入0/30/60/90");
        //        return false;
        //    }
        //    if(!System.Text.RegularExpressions.Regex.IsMatch(txtTax.Text.Trim(), "^\\d+$"))
        //    {
        //        alert("税率，请输入数字");
        //        return  false;
        //    }
        //}

    </script>
    <style type="text/css">
        .FixedGrid {
            border-collapse: collapse;
            table-layout: fixed;
        }

            .FixedGrid .FixedGridWidth {
                width: 50px;
                height: 25px;
                border-top: 1px solid #fff;
                border-right: 1px solid #fff;
            }

            .FixedGrid .FixedGridHeight {
                width: 1px;
                height: 25px;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #fff;
            }

            .FixedGrid .FixedGridLeftCorner {
                width: 1px;
                border-left: 1px solid #fff;
                border-bottom: 1px solid #000;
            }

            .FixedGrid .FixedGridLeft {
                width: 100px;
                height:40px;
                text-align: center;
                border-left: 1px solid #fff;
                border-right: 1px solid #000;
                font-weight: bold;
            }

            .FixedGrid .FixedGridRight {
                width: 100px;
                text-align: center;
                border-right: 1px solid #000;
                font-weight: bold;
            }

            .FixedGrid tr {
                border-right: 1px solid #000;
            }

            .FixedGrid td {
                text-align: left;
                word-break: break-all;
                word-wrap: break-word;
                border: 1px solid #000;
            }
        .txtLongSupplier {
            width:500px;
        }
        .txtShortSupplier {
            width:200px;
        }
        .txtDDLSupplier {
            width:150px;
        }
        u {
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="center">
            <div id="divTabs">
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;新供应商信息单&nbsp;&nbsp;</a></li>
                    <li id="liFileQualification"><a href="#tabs-2">&nbsp;&nbsp;供应商资质文件评估&nbsp;&nbsp;</a></li>
                    <li id="liSignFile"><a href="#tabs-3">&nbsp;&nbsp;签署文件&nbsp;&nbsp;</a></li>
                    <li id="liFormalFile"><a href="#tabs-4">&nbsp;&nbsp;正式合同文件&nbsp;&nbsp;</a></li>
                    <li>
                        <asp:Button ID="Button2" runat="server" Text="返回" CssClass="SmallButton2" OnClick="Button1_Click" />
                    </li>
                    <li>
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                    </li>
                </ul>
                <div id="tabs-1">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth"><asp:HiddenField ID="hidAgreeAuth" runat="server" /></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridRight" style="border-right: 1px solid #fff; width:150px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight" style="height:80px;"></td>
                            <td colspan="3" class="FixedGridLeft" style="border-right:none;"><img src="../images/LOGO.png" /></td>
                            <td colspan="13" class="FixedGridLeft" style="border-right:none;"><span style="font-size:25px;">新增供应商申请单</span></td>
                            <td colspan="3" class="FixedGridRight" style="border-left:none;"></td>
                        </tr>
                        <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">申请单位</td>
                            <td colspan="7" style="text-align:center;">
                                <asp:Label ID="labPlant" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">申请日期</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">申请人</td>
                            <td colspan="7" style="text-align:center;">
                                <asp:Label ID="labAPPUserName" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">申请部门</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labAPPDeptName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">供应商名称</td>
                            <td colspan="2" class="FixedGridLeft">中文</td>
                            <td colspan="14" style="text-align:center;">
                                <asp:Label ID="labChineseSupplierName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align:center;">
                                <asp:Label ID="labEnglishSupplierName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">供应商地址</td>
                            <td colspan="2" class="FixedGridLeft">中文</td>
                            <td colspan="14" style="text-align:center;">
                                <asp:Label ID="labChineseSupplierAddress" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align:center;">
                                <asp:Label ID="labEnglishSupplierAddress" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">联系人</td>
                            <td colspan="2" class="FixedGridLeft">联系人</td>
                            <td colspan="5" style="text-align:center;">
                                <asp:Label ID="labSupplierUserName" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3"class="FixedGridRight">职务</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labSupplierRoleName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">联系方式</td>
                            <td colspan="2" class="FixedGridLeft">联系电话</td>
                            <td colspan="5" style="text-align:center;">
                                <asp:Label ID="labSupplierNumber" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridRight">固定电话</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labSupplierPhone" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">传真</td>
                            <td colspan="5" style="text-align:center;">
                                <asp:Label ID="labSupplierFax" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridRight">邮箱</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labSupplierEmail" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">经营类型</td>
                            <td colspan="7" style="text-align:center;">
                                <asp:Label ID="labBusinesstypeID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labBusinesstype" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">公司网页</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:Label ID="labSupplierWeb" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">供应物料类型</td>
                            <td colspan="16" style="text-align:center; font-size:15px;">
                                <asp:Label ID="Label1" runat="server" Text="大类区分："></asp:Label>
                                <asp:Label ID="labBroadHeadingID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labBroadHeading" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="细部区分："></asp:Label>
                                <asp:Label ID="labSubDivisionID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labSubDivision" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="子物料："></asp:Label>
                                <asp:Label ID="labSubMaterialID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labSubMaterial" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text=" 等级："></asp:Label>
                                <asp:Label ID="labFactoryInspectionLevelID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labFactoryInspection" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">新增供应商原因</td>
                            <td colspan="16" style="text-align:center;">
                                <asp:Label ID="labSuppNewReason" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover" id="newReason">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">主管签字</td>
                            <td colspan="14" id="tdLeaderOpinion" runat="server" style="height:80px;"></td>
                            <td colspan="2" rowspan="2" style="text-align:center;">                                
                                <asp:Label ID="labLeaderUsername" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnLeaderYes" runat="server" Text="同意" CssClass="SmallButton2" OnClick="btnLeaderYes_Click" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnLeaderNo" runat="server" Text="拒绝" CssClass="SmallButton2" OnClick="btnLeaderNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover" id="SuppSignFile">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="3" class="FixedGridLeft" style="text-align:center">供应商资质文件评估</td>
                            <td style="text-align:center"><strong>币种:</strong></td>
                            <td colspan="3" style="text-align:center;">    
                                <asp:textbox style="text-align:center;" ID="txtCurr" runat="server" Text="" Width="100%" BorderStyle="None" ></asp:textbox>                             
                            </td>
                            <td style="text-align:center"><strong>税率:</strong></td>
                            <td colspan="3" style="text-align:center;">    
                                <asp:textbox style="text-align:center;" ID="txtTax" runat="server" Text="" Width="100%" BorderStyle="None"></asp:textbox>                         
                            </td>
                            <td style="text-align:center"><strong>账期:</strong></td>
                            <td colspan="3" style="text-align:center;">    
                                <asp:textbox style="text-align:center;" ID="txtPaymentDays" runat="server" Text="" Width="100%" BorderStyle="None"></asp:textbox>
                            </td>
                            <td colspan="2" style="text-align: center;">                               
                                <asp:Button ID="btnSave" runat="server"  Text="保存" CssClass="SmallButton2" OnClick="btnSave_Click" Height="21px" Width="100%"/>
                            </td>           
                            
                            <td colspan="2" rowspan="3" style="text-align:center;">                                
                                <asp:Label ID="labSuppDeptName" runat="server" Text="供应商开发部"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>                                                 
                            <td colspan="14" id="tdSignFileOpinion" runat="server" style="height:80px;"></td>             
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnSignFileYes" runat="server" Text="同意" CssClass="SmallButton2" OnClick="btnSignFileYes_Click" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnSignFileNo" runat="server" Text="拒绝" CssClass="SmallButton2" OnClick="btnSignFileNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">是否验厂</td>
                            <td colspan="16" style="text-align:center;">
                                <asp:Button ID="btnIsFI" runat="server" Text="验厂" CssClass="SmallButton2" OnClick="btnIsFI_Click" />
                                <asp:Button ID="btnIsNotFI" runat="server" Text="不验厂" CssClass="SmallButton2" OnClick="btnIsNotFI_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover" id="FLOpinion">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">验厂意见</td>
                            <td colspan="16" id="tdFLOpinion" runat="server" style="height:80px;">
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="12" id="tdFIlink" runat="server">
                                <asp:Button ID="linkFIStatus" runat="server" Text="验厂查看" CssClass="SmallButton2" OnClick="linkFIStatus_Click" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnFLYes" runat="server" Text="同意" CssClass="SmallButton2" OnClick="btnFLYes_Click" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnFLNo" runat="server" Text="拒绝" CssClass="SmallButton2" OnClick="btnFLNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="3" class="FixedGridLeft">签署文件</td>
                            <td colspan="2" style="height:130px;" class="FixedGridLeft">供应商开发部</td>
                            <td colspan="14" id="tdSuppDeptOpinion" runat="server"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" style="height:130px;" class="FixedGridLeft">法务部</td>
                            <td colspan="14" id="tdLawDeptOpinion" runat="server" ></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" style="height:130px;" class="FixedGridLeft">财务部</td>
                            <td colspan="14" id="tdFinanceDeptOpinion" runat="server"></td>
                        </tr>
                        <tr class="NoTrHover" id="ManageOpinion">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">总经理意见</td>
                            <td colspan="16" id="tdManageOpinion" runat="server" style="height:80px;">
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="12">
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnManageYes" runat="server" Text="同意" CssClass="SmallButton2" OnClick="btnManageYes_Click" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnManageNo" runat="server" Text="拒绝" CssClass="SmallButton2" OnClick="btnManageNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover" id="trSupplierNum">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">供应商代码</td>
                            <td colspan="12" id="td1" runat="server" style="text-align:center; height:30px;">
                                <asp:Label ID="labSupplierNum" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnSupplierNum" runat="server" Text="提交" CssClass="SmallButton2" OnClick="btnSupplierNum_Click" />
                            </td>
                            <td colspan="2" style="text-align:center;">                                
                                <asp:Label ID="Label4" runat="server" Text="财务部"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="19" style="text-align: center;">
                                <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" Text="返回" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tabs-2" style="text-align:center; margin-top:40px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr>
                            <td colspan="8" class="FixedGridLeft" style="font-size:30px; border:none;">供应商资质文件评估</td>
                        </tr>
                        <tr style="height:20px;">
                            <td colspan="8"></td>
                        </tr>
                        <tr style="text-align:center; color:red;">
                            <td colspan="8">全国企业信用信息公示系统 信息核准 <u id="links">www.sgs.gov.cn</u></td>
                        </tr>
                        <tr style="height:20px;">
                            <td colspan="8"></td>
                        </tr>
                        <tr style="border:none;" id="trFileQualificationSecurity">
                            <td style="text-align:right;">
                                <asp:Label ID="Label20" runat="server" Text="文件类型："></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlFileType" runat="server" CssClass="txtDDLSupplier" Width="200px"
                                     DataTextField="supplier_FileType" DataValueField="supplier_FileID">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFileTypeName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="Label21" runat="server" Text="必要性："></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlNecessity" runat="server">
                                    <asp:ListItem Value="0" Text="是">是</asp:ListItem>
                                    <asp:ListItem Value="1" Text="否">否</asp:ListItem>
                                    <asp:ListItem Value="2" Text="一般">一般</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right;">
                                有效期：
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtEffectDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" Width="250px" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnFileSubmit" runat="server" Text="提交" OnClick="btnFileSubmit_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height:30px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td colspan="8" align="center" id="tdFQ">
                                <asp:GridView ID="FQgv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
                                DataKeyNames="supplier_FileID,supplier_FileNecessityID,supplier_FilePath,supplier_FileName,supplier_FileIsEffect" 
                                OnRowDataBound="FQgv_RowDataBound" OnRowCommand="FQgv_RowCommand">
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                     <asp:TemplateField HeaderText="文件类型" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="200px" Height="25px" Font-Underline="false"/>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbldownload" CssClass="no" runat="server" Text='<%# Eval("supplier_FileType") %>'
                                             CommandName="Download" CommandArgument='<%# Eval("supplier_FileType") %>' style="TEXT-DECORATION:none">
                                                
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ControlStyle Font-Underline="true"/>
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="文件类型" DataField="supplier_FileType">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField HeaderText="必要性" DataField="supplier_FileNecessity">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="附件名称" DataField="supplier_FileName">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="有效期" DataField="supplier_FileEffectDate">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                        <ControlStyle Font-Bold="False" Font-Underline="True" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                                    </asp:ButtonField>                            
                                </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tabs-3" style="text-align:center;">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth"><asp:HiddenField ID="HiddenField1" runat="server" /></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridRight" style="border-right: 1px solid #fff; width:150px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td colspan="19" class="FixedGridLeft"><span style="font-size:25px;">签署文件</span></td>
                        </tr>
                        <tr class="NoTrHover" id="trSuppDeptUpload" style="text-align:center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="19" class="FixedGridLeft">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button10" runat="server" Text="上传" CssClass="SmallButton2" OnClick="Button10_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="19" style="text-align:center;">
                                <asp:GridView ID="gvFile" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowCommand="gvFile_RowCommand"
                                    DataKeyNames="SignFile_FileStatus,SignFile_FileName,SignFile_FilePath,SignFile_FileID" OnRowDataBound="gvFile_RowDataBound">
                                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                                    <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell Text="文件名" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="上传者" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="上传日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="SignFile_FileName" HeaderText="文件名">
                                            <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                            <ItemStyle Width="540px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createName" HeaderText="上传者">
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>
                                        <asp:ButtonField Text="作废" HeaderText="作废" CommandName="NotEff">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr class="NoTrHover" id="SuppDeptOpinion">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">
                                供应商开发部意见
                            </td>
                            <td colspan="16" id="tdSuppDeptOpinion1" runat="server" style="height:80px;"></td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnSuppDeptYes" runat="server" CssClass="SmallButton2" Text="提交" OnClick="btnSuppDeptYes_Click" />
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnSuppDeptNo" runat="server" CssClass="SmallButton2" Text="拒绝" OnClick="btnSuppDeptNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;" id="LawDeptOpinion">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">
                                法务部意见
                            </td>
                            <td colspan="16" id="tdLawDeptOpinion1" runat="server" style="height:80px;">
                            </td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnLawDeptYes" runat="server" CssClass="SmallButton2" Text="同意" OnClick="btnLawDeptYes_Click" />
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnLawDeptNo" runat="server" CssClass="SmallButton2" Text="驳回" OnClick="btnLawDeptNo_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;" id="FinanceDeptOpinion">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">
                                财务部意见
                            </td>
                            <td colspan="16" id="tdFinanceDeptOpinion1" runat="server" style="height:80px;">
                                
                            </td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="10" >
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnFinanceDeptYes" runat="server" CssClass="SmallButton2" Text="同意" OnClick="btnFinanceDeptYes_Click" />
                            </td>
                            <td colspan="3" style="text-align:center;">
                                <asp:Button ID="btnFinanceDeptNo" runat="server" CssClass="SmallButton2" Text="驳回" OnClick="btnFinanceDeptNo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tabs-4">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth"><asp:HiddenField ID="HiddenField2" runat="server" /></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridRight" style="border-right: 1px solid #fff; width:150px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" class="FixedGridLeft"><span style="font-size:25px;">正式合同文件</span></td>
                        </tr>
                        <tr class="NoTrHover" style="text-align:center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" class="FixedGridLeft">
                                <asp:FileUpload ID="FileUpload3" runat="server" Width="300px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button3" runat="server" Text="上传" CssClass="SmallButton2" OnClick="Button3_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" style="text-align:center;">
                                <asp:GridView ID="gvFormal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowCommand="gvFormal_RowCommand"
                                    DataKeyNames="FormalFile_FileName,FormalFile_FilePath">
                                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                                    <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell Text="文件名" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="上传者" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="上传日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                                <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="FormalFile_FileName" HeaderText="文件名">
                                            <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                            <ItemStyle Width="540px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createName" HeaderText="上传者">
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hidSupplierID" runat="server" />
                <asp:HiddenField ID="hidLeaderIsAgree" runat="server" />
                <asp:HiddenField ID="hidSignFileIsAgree" runat="server" />
                                
                <asp:HiddenField ID="hidFLStatus" runat="server" />
                
                <asp:HiddenField ID="hidIsFL" runat="server" />
                <asp:HiddenField ID="hidFLIsAgree" runat="server" />
                
                <asp:HiddenField ID="hidSuppDept" runat="server" />
                <asp:HiddenField ID="hidLawDept" runat="server" />
                <asp:HiddenField ID="hidFinanceDept" runat="server" />                
                <asp:HiddenField ID="hidManageIsAgree" runat="server" />
                
                <asp:HiddenField ID="hidUserName" runat="server" />
                <asp:HiddenField ID="hidDeptName" runat="server" />
                <asp:HiddenField ID="hidSupplierStatus" runat="server" />
                
                <asp:HiddenField ID="hidFileQualificationSecurity" runat="server" />
                <asp:HiddenField ID="hidFileQualificationOpinionSecurity" runat="server" />
                <asp:HiddenField ID="hidLeaderSecurity" runat="server" />
                <asp:HiddenField ID="hidManageSecurity" runat="server" />
                <asp:HiddenField ID="hidSuppOpinionSecurity" runat="server" />
                <asp:HiddenField ID="hidLawOpinionSecurity" runat="server" />
                <asp:HiddenField ID="hidFinanceOpinionSecurity" runat="server" />
                <asp:HiddenField ID="hidFormalFileSecurity" runat="server" />
                <asp:HiddenField ID="hidFISecurity" runat="server" />
                <asp:HiddenField ID="hidSignFileSecurity" runat="server" />
                <asp:HiddenField ID="hidSupplierNumSecurity" runat="server" />
                                
                <asp:HiddenField ID="hidLevelID" runat="server" />

                <asp:HiddenField ID="hidCreatedID" runat="server" />
                <asp:HiddenField ID="hidSupplierPersonEmail" runat="server" />
                <asp:HiddenField ID="hidLawPersonEmail" runat="server" />
                <asp:HiddenField ID="hidFinPersonEmail" runat="server" />
                <asp:HiddenField ID="hidManagerEmail" runat="server" />
                <asp:HiddenField ID="hidSupplierEditEmail" runat="server" />
                <asp:HiddenField ID ="hidCreatedEmail" runat="server" />
             </div>
        </div>    
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
