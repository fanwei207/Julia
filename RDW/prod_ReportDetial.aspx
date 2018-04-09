<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prod_ReportDetial.aspx.cs" Inherits="RDW_prod_ReportDetial" %>

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

            $("#btnAnalysis").click(function () {
                var _no = $(this).parent().parent().find(".no").html();   
                var _procNo = $("#hidNo").val();    //跟踪单号
                var _code = $("#hidCode").val();    //部件号
                var _proj = $("#hidProjName").val();    //项目名称
                var _mid = $("#hidmid").val();  //
                var _did = $("#hiddid").val();  //
                var _procStatus = $("#hidProcStatus").val();    //工艺
                var _typeStatus = $("#hidTypeStatus").val();    //试流单状态
                var _prodStatus = $("#hidProdStatus").val();    //试流
                var _labuser = $("#labProcUser").text();   //步骤对应的人员
                var _src = "/RDW/prod_reportAnalysis.aspx?dept=proc&no=" + _procNo + "&code=" + _code + "&name=" + _proj + "&mid=" + _mid + "&did=" + _did + "&procStatus=" + _procStatus + "&typeStatus=" + _typeStatus + "&prodStatus=" + _prodStatus + "&username=" + _labuser;
                $.window("试流标准分析", "80%", "80%", _src, "", true);
                return false;
            });
            //endAnalysis
            $("#btnProcYes").click(function(){
                var _procStatus = $("#hidProcStatus").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_prodStatus != 0)
                {
                    alert("试流结果已留言或已确认，按钮不能进行操作");
                    return false;
                }
                if(_procStatus == 0)
                {
                    $("#btnProcYes").attr("disabled", true); 
                    alert("工艺确认还未留言！");
                    return false;
                }
            });
            $("#btnProcNo").click(function(){
                var _procStatus = $("#hidProcStatus").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_prodStatus != 0)
                {
                    alert("试流结果已留言或已确认，按钮不能进行操作");
                    return false;
                }
                if(_procStatus == 0)
                {
                    $("#btnProcNo").attr("disabled", true); 
                    alert("工艺确认还未留言！");
                    return false;
                }
            });
            $("#btnPassYes").click(function(){
                var _prodStatus = $("#hidProdStatus").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _PlanDate = $("#hidPlanDate").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _existsFlowRecord = $("#hidExistsFlowRecord").val();


                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，按钮不可操作");
                    return false;
                }
                if(_existsFlowRecord == 1)
                {
                    alert("未进行试流标准分析，按钮不可操作");
                    return false;
                }
                if(_procStatus == 1 || _procStatus == 2)
                {
                    if(_PlanDate == "")//PCD不能为空
                    {
                        alert("PCD为空，试流结果不可操作！");
                        return false;
                    }
                    if (_prodStatus == 1 || _prodStatus == 2)
                    { 
                        $("#btnPassYes").attr("disabled", true);
                        alert("试流结果已确定");
                        return false;
                    }
                    else if(_purStatus == 0 || _planStatus == 0)
                    {
                        $("#btnPassYes").attr("disabled", true);
                        alert("计划情况或采购情况还未留言，按钮不可操作");
                        return false;
                    }
                    if(_prodStatus != 3)
                    {
                        $("#btnPassYes").attr("disabled", true);
                        alert("试流结果还未留言，按钮不可操作");
                        return false;
                    }
                }
                else
                {
                    $("#btnPassYes").attr("disabled", true);
                    alert("工艺还未确定");
                    return false;
                }
            });
            $("#btnPassNo").click(function(){
                var _prodStatus = $("#hidProdStatus").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _PlanDate = $("#hidPlanDate").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _existsFlowRecord = $("#hidExistsFlowRecord").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，按钮不可操作");
                    return false;
                }
                if(_existsFlowRecord == 1)
                {
                    alert("未进行试流标准分析，按钮不可操作");
                    return false;
                }
                if(_procStatus == 1 || _procStatus == 2)
                {
                    if(_PlanDate == "")//PCD不能为空
                    {
                        alert("PCD为空，试流结果不可操作！");
                        return false;
                    }
                    if (_prodStatus == 1 || _prodStatus == 2)
                    { 
                        $("#btnPassNo").attr("disabled", true);
                        alert("试流结果已确定");
                        return false;
                    }
                    else if(_purStatus == 0 || _planStatus == 0)
                    {
                        $("#btnPassNo").attr("disabled", true);
                        alert("计划情况或采购情况还未留言，按钮不可操作");
                        return false;
                    }
                    if(_prodStatus != 3)
                    {
                        $("#btnPassNo").attr("disabled", true);
                        alert("试流结果还未留言，按钮不可操作");
                        return false;
                    }
                }
                else
                {
                    $("#btnPassNo").attr("disabled", true);
                    alert("工艺还未确定");
                    return false;
                }
            });
            //endBtn
            $("#proc").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidProcUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                //没有权限不能进行留言
                if(_labuser.indexOf(";"+_user+";") >= 0 )
                { 
                    if (_prodStatus == 1 || _prodStatus == 2 || _prodStatus == 3) //1通过、2不通过、3试流结果已留言
                    {
                        alert("试流结果已留言或  已确认，不能继续留言"); 
                        return false;
                    }
                    else
                    {
                        var _src = "/RDW/prod_Massage.aspx?dept=proc&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                        $.window("工艺确认", "80%", "80%", _src, "", true);
                        return false;                   
                    }
                }
                else
                {
                    alert('您没有工艺确认及留言权限');
                    return false;
                }
            });
            //endproc
            $("#purchase").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidPurUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_labuser.indexOf(";"+_user+";") >= 0 )
                {
                    if(_procStatus == 2)
                    {
                        alert("工艺不可试流，不能进行留言");
                        return false;
                    }
                    if(_prodStatus == 1 || _prodStatus == 2)
                    {
                        alert("试流结果已确定，采购情况不能留言"); 
                        return false;
                    }
                    else if(_prodStatus == 3)
                    {
                        alert("试流结果已留言，采购情况不能留言"); 
                        return false;
                    }
                    else
                    {
                        var _src = "/RDW/prod_Massage.aspx?dept=purchase&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                        $.window("采购情况", "80%", "80%", _src, "", true);                    
                        return false;
                    }
                }                
                else
                {
                    alert('您没有采购情况留言权限');
                    return false;
                }
            });
            //endpurchase

            
            $("#ceshi").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduID").val();
                var _uid=$("#hiduID").val();
                var _labuser = $("#hidPowerNameByCeshi").val();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_labuser.indexOf(";"+_uid+";") >= 0 )
                {
                    var _src = "/RDW/prod_Massage.aspx?dept=ceshi&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                    $.window("样品发出", "80%", "80%", _src, "", true);
                    return false;

                }             
                else
                {
                    alert('您没有样品发出留言权限');
                    return false;
                }
            });
            //endpurchase





            $("#plan").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _planDate = $("#hidPlanDate").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidPlanUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_labuser.indexOf(";"+_user+";") >= 0 )
                {
                    if(_procStatus == 2)
                    {
                        alert("工艺不可试流，不能进行留言");
                        return false;
                    }
                    if(_prodStatus == 1 || _prodStatus == 2)
                    {
                        alert("试流结果已确定，计划情况不能留言"); 
                        return false;
                    }
                    else if(_prodStatus == 3)
                    {
                        alert("试流结果已留言，计划情况不能留言"); 
                        return false;
                    }
                    else
                    {
                        var _src = "/RDW/prod_Massage.aspx?dept=plan&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&planDate=" + _planDate + "&mid=" + _mid + "&did=" + _did;
                        $.window("计划情况", "80%", "80%", _src, "", true);
                        return false;
                    }
                }                
                else
                {
                    alert('您没有计划情况留言权限');
                    return false;
                }
            }); 
            $("#planMag").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _planDate = $("#hidPlanDate").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidPlanUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_labuser.indexOf(";"+_user+";") >= 0 )
                {
                    if(_procStatus == 2)
                    {
                        alert("工艺不可试流，不能进行留言");
                        return false;
                    }
                    if(_prodStatus == 1 || _prodStatus == 2)
                    {
                        alert("试流结果已确定，计划情况不能留言"); 
                        return false;
                    }
                    else if(_prodStatus == 3)
                    {
                        alert("试流结果已留言，计划情况不能留言"); 
                        return false;
                    }
                    else
                    {
                        var _src = "/RDW/prod_Massage.aspx?dept=plan&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&planDate=" + _planDate + "&mid=" + _mid + "&did=" + _did;
                        $.window("计划情况", "80%", "80%", _src, "", true);
                        return false;
                    }
                }                
                else
                {
                    alert('您没有计划情况留言权限');
                    return false;
                }
            });
            //endplan
            $("#product").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _PlanDate = $("#hidPlanDate").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidProdUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _existsFlowRecord = $("#hidExistsFlowRecord").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，试流结果不能留言");
                    return false;
                }
                if(_labuser.indexOf(";"+_user+";") >= 0)
                {
                    if (_procStatus == 1 || _procStatus == 2)
                    {
                        if(_existsFlowRecord == 1)
                        {
                            alert("未进行试流标准分析，试流结果不能留言");
                            return false;
                        }
                        if(_prodStatus == 1 || _prodStatus == 2)
                        {
                            alert("试流结果已确认，试流结果不能留言");
                            return false;
                        }
                        else
                        {
                            if(_purStatus == 0 || _planStatus == 0)
                            {
                                alert("计划情况或采购情况还未留言，试流结果不能留言");
                                return false;
                            }
                            else
                            {
                                if(_PlanDate == "")
                                {
                                    alert("PCD为空，试流结果不能留言");
                                    return false;
                                }
                                else
                                {
                                    var _src = "/RDW/prod_Massage.aspx?dept=product&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                                    $.window("试流结果", "80%", "80%", _src, "", true);
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        alert("工艺还未确认，试流结果不能留言");   
                        return false;                 
                    }
                }                
                else
                {
                    alert('您没有试流结果确认及留言权限');
                    return false;
                }
            });
            $("#productMag").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _PlanDate = $("#hidPlanDate").val();
                var _user = $("#hiduID").val();
                var _labuser = $("#hidProdUser").val();
                var _typeStatus = $("#hidTypeStatus").val();
                var _existsFlowRecord = $("#hidExistsFlowRecord").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，试流结果不能留言");
                    return false;
                }
                if(_labuser.indexOf(";"+_user+";") >= 0)
                {
                    if (_procStatus == 1 || _procStatus == 2)
                    {
                        if(_existsFlowRecord == 1)
                        {
                            alert("未进行试流标准分析，试流结果不能留言");
                            return false;
                        }
                        if(_prodStatus == 1 || _prodStatus == 2)
                        {
                            alert("试流结果已确认，试流结果不能留言");
                            return false;
                        }
                        else
                        {
                            if(_purStatus == 0 || _planStatus == 0)
                            {
                                alert("计划情况或采购情况还未留言，试流结果不能留言");
                                return false;
                            }
                            else
                            {
                                if(_PlanDate == "")
                                {
                                    alert("PCD为空，试流结果不能留言");
                                    return false;
                                }
                                else
                                {
                                    var _src = "/RDW/prod_Massage.aspx?dept=product&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                                    $.window("试流结果", "80%", "80%", _src, "", true);
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        alert("工艺还未确认，试流结果不能留言");   
                        return false;                 
                    }
                }                
                else
                {
                    alert('您没有试流结果确认及留言权限');
                    return false;
                }
            });
            //endproduct              
            $("#procAnaly").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduName").val();
                var _userid = $("#hiduID").val();
                var _labuser = $("#labProcAnalyUser").text();
                var _labusers = $("#hidPowerName").val();
                var _typeStatus = $("#hidTypeStatus").val();

                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，试流分析及解决方案不能留言");
                    return false;
                }
                //if(_labuser.indexOf(_user) >= 0)
                //{
                //    if (_procStatus == 1 || _procStatus == 2)
                //    {
                //        if(_prodStatus == 1 || _prodStatus == 2)
                //        {
                //            var _src = "/RDW/prod_Massage.aspx?dept=procAnaly&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                //            $.window("试流分析及解决方案", "80%", "80%", _src, "", true);
                //            return false;
                //        }
                //        else
                //        {
                //            alert("试流结果还未确定，试流分析及解决方案不能留言"); 
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        alert("工艺还未确认，试流分析及解决方案不能留言"); 
                //        return false;                   
                //    }
                //}
                //else 
                if(_labusers.indexOf(";"+_userid+";") >= 0)
                {
                    if (_procStatus == 1 || _procStatus == 2)
                    {
                        if(_prodStatus == 1 || _prodStatus == 2)
                        {
                            var _src = "/RDW/prod_Massage.aspx?dept=procAnaly&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                            $.window("试流分析及解决方案", "80%", "80%", _src, "", true);
                            return false;
                        }
                        else
                        {
                            alert("试流结果还未确定，试流分析及解决方案不能留言"); 
                            return false;
                        }
                    }
                    else
                    {
                        alert("工艺还未确认，试流分析及解决方案不能留言"); 
                        return false;                   
                    }
                }
                else
                {
                    alert('您没有试流分析及解决方案留言权限');
                    return false;
                }
            });
            //endprocAnaly
            $("#procSolve").dblclick(function () {
                var _no = $("#hidNo").val();
                var _code = $("#hidCode").val();
                var _proj = $("#hidProjName").val();
                var _mid = $("#hidmid").val();
                var _did = $("#hiddid").val();
                var _procStatus = $("#hidProcStatus").val();
                var _purStatus = $("#hidPurStatus").val();
                var _planStatus = $("#hidPlanStatus").val();
                var _prodStatus = $("#hidProdStatus").val();
                var _procAnalyStatus = $("#hidProcAnalyStatus").val();
                var _procSolveStatus = $("#hidProcSolveStatus").val();
                var _user = $("#hiduName").val();
                var _userid = $("#hiduID").val();
                var _labusers = $("#hidPowerName").val()
                var _labuser = $("#labProcSolveUser").text();
                var _typeStatus = $("#hidTypeStatus").val();
                if(_typeStatus == 5)
                {
                    alert("试流单已取消，不能进行留言及操作");
                    return false;
                }
                if(_procStatus == 2)
                {
                    alert("工艺不可试流，试流结果确认不能留言");
                    return false;
                }
                //if(_labuser.indexOf(_user) >= 0)
                //{
                //    if (_procStatus == 1 || _procStatus == 2)
                //    {
                //        if(_prodStatus == 1 || _prodStatus == 2)
                //        {
                //            var _src = "/RDW/prod_Massage.aspx?dept=procSolve&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                //            $.window("试流结果确认", "80%", "80%", _src, "", true);
                //            return false;
                //        }
                //        else
                //        {
                //            alert("试流结果还未确定，试流结果确认不能留言");  
                //            return false;            
                //        }

                //    }
                //    else
                //    {
                //        alert("工艺还未确认，试流结果确认不能留言");  
                //        return false;                  
                //    }
                //}
                //else 
                if(_labusers.indexOf(";"+_userid+";") >= 0)
                {
                    if (_procStatus == 1 || _procStatus == 2)
                    {
                        if(_prodStatus == 1 || _prodStatus == 2)
                        {
                            var _src = "/RDW/prod_Massage.aspx?dept=procSolve&no=" + _no + "&code=" + _code + "&proj=" + _proj + "&mid=" + _mid + "&did=" + _did;
                            $.window("试流结果确认", "80%", "80%", _src, "", true);
                            return false;
                        }
                        else
                        {
                            alert("试流结果还未确定，试流结果确认不能留言");  
                            return false;            
                        }

                    }
                    else
                    {
                        alert("工艺还未确认，试流结果确认不能留言");  
                        return false;                  
                    }
                }
                else
                {
                    alert('您没有试流结果确认留言权限');
                    return false;
                }
            });
            //endprocSolve
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
    </style>
    </head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse; width: 1000px; table-layout: fixed; margin-top:-20px;">
                <tr class="NoTrHover">
                    <td class="FixedGridTD FixedGridTDLeftCorner"></td>
                    <td class="FixedGridTD FixedGridLeft"></td>
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
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridTD FixedGridRight"></td>
                </tr>
                <tr class="NoTrHover">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="3">试流单信息</td>
                    <td colspan="2">跟踪单号:</td>
                    <td colspan="3">
                        <asp:Label ID="labNo" runat="server" Text="" Enabled="false"></asp:Label>
                    </td>
                    <td colspan="2">项目代码:</td>
                    <td colspan="3">
                        <asp:Label ID="labCode" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="2">项目名称:</td>
                    <td colspan="4">
                        <asp:Label ID="labProjectName" runat="server" Text=""></asp:Label></td>
                    <td class="FixedGridRight" rowspan="3" align="center">
                        <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" Width="80px" OnClick="btnBack_Click"  />
                    </td>
                </tr>
                <tr class="NoTrHover">
                    <td class="FixedGridTD"></td>
                    <td colspan="2">QAD:</td>
                    <td colspan="3">
                        <asp:Label ID="labQAD" runat="server" Text=""></asp:Label></td>
                    <td colspan="2">PCB:</td>
                    <td colspan="3">
                        <asp:Label ID="labPCB" runat="server" Text=""></asp:Label></td>
                    <td colspan="2">截止日期:</td>
                    <td colspan="4">
                        <asp:Label ID="labEndDate" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr class="NoTrHover">
                    <td class="FixedGridTD"></td>
                    <td colspan="2">附件:</td>
                    <td id="tdFileByProc" runat="server" colspan="8"></td>
                       <td colspan="2">类型:</td>
                    <td colspan="4">
                        <asp:Label ID="lbltype" runat="server" Text=""></asp:Label></td>
                </tr>

                <tr id="ceshi">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="3">样品发出</td>
                    <td id="tdceshi" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="3">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>

                <tr id="proc">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="4">工艺确认</td>
                    <td id="tdMsgAndFileByProc" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="4">
                        <asp:Label ID="labProcUser" runat="server"></asp:Label>
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
                    <td colspan="9">
                        <asp:Label ID="labShowProc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnAnalysis" runat="server" CssClass="SmallButton3" Text="试流标准分析" Width="80px" />
                    </td>
                    <td></td>
                    <td colspan="4">
                        <asp:Button ID="btnProcYes" runat="server" CssClass="SmallButton3" Width="80px" Text="可试流" OnClick="btnProcYes_Click" />
                        <asp:Button ID="btnProcNo" runat="server" CssClass="SmallButton3" Width="80px" Text="不试流" OnClick="btnProcNo_Click" />
                    </td>
                </tr>


                




                <tr id="purchase">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="3">采购情况</td>
                    <td id="tdMsgAndFileByPur" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="3">
                        <asp:Label ID="labPurUser" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr id="planMag">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="4">计划情况</td>
                    <td colspan="2">PCD:</td>
                    <td colspan="14">
                        <asp:Label ID="labPlanDate" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="FixedGridRight" rowspan="4">
                        <asp:Label ID="labPlanUser" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="plan">
                    <td class="FixedGridTD"></td>                    
                    <td id="tdMsgAndFileByPlan" runat="server"  colspan="16" rowspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr id="product">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="4">试流结果</td>
                    <td id="tdMsgAndFileByProduct" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="4">
                        <asp:Label ID="labProdUser" runat="server"></asp:Label>
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
                    <td colspan="12">
                        <asp:Label ID="labShowProd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:Button ID="btnPassYes" runat="server" CssClass="SmallButton3" Width="80px" Text="通过" OnClick="btnPassYes_Click" />
                        <asp:Button ID="btnPassNo" runat="server" CssClass="SmallButton3" Width="80px" Text="不通过" OnClick="btnPassNo_Click" />
                    </td>
                </tr>
                <tr id="procAnaly">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="3">试流分析及解决方案</td>
                    <td id="tdMsgAndFileByProcAnaly" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="3">
                        <asp:Label ID="labProcAnalyUser" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr id="procSolve">
                    <td class="FixedGridTD"></td>
                    <td class="FixedGridLeft" rowspan="3">试流结果确认</td>
                    <td id="tdMsgAndFileByProcSolve" runat="server" colspan="16" rowspan="3"></td>
                    <td class="FixedGridRight" rowspan="3">
                        <asp:Label ID="labProcSolveUser" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>
                <tr>
                    <td class="FixedGridTD"></td>
                </tr>

            </table>
            <asp:HiddenField ID="hidNo" runat="server" />
            <asp:HiddenField ID="hidCode" runat="server" />
            <asp:HiddenField ID="hidProjName" runat="server" />
            <asp:HiddenField ID="hidQAD" runat="server" />
            <asp:HiddenField ID="hidPCB" runat="server" />
            <asp:HiddenField ID="hidPlanDate" runat="server" />
            <asp:HiddenField ID="hidEndDate" runat="server" />
            <asp:HiddenField ID="hidmid" runat="server" />
            <asp:HiddenField ID="hiddid" runat="server" />

            <asp:HiddenField ID="hidProcStatus" runat="server" />
            <asp:HiddenField ID="hidPurStatus" runat="server" />
            <asp:HiddenField ID="hidPlanStatus" runat="server" />
            <asp:HiddenField ID="hidProdStatus" runat="server" />
            <asp:HiddenField ID="hidProcAnalyStatus" runat="server" />
            <asp:HiddenField ID="hidProcSolveStatus" runat="server" />

            
            <asp:HiddenField ID="hiduName" runat="server" />
            <asp:HiddenField ID="hidTypeStatus" runat="server" />
            
            <asp:HiddenField ID="hidExistsFlowRecord" runat="server" />
            <asp:HiddenField ID="hiduID" runat="server" />
            <asp:HiddenField ID="hiduserid" runat="server" />

            
            <asp:HiddenField ID="hidPowerNameByCeshi" runat="server" />
            <asp:HiddenField ID="hidPowerName" runat="server" />
            
            <asp:HiddenField ID="hidProcUser" runat="server" />
            <asp:HiddenField ID="hidPlanUser" runat="server" />
            <asp:HiddenField ID="hidPurUser" runat="server" />
            <asp:HiddenField ID="hidProdUser" runat="server" />
            <asp:HiddenField ID="hidprodid" runat="server" />
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
