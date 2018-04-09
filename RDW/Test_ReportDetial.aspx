<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_ReportDetial.aspx.cs" Inherits="RDW_Test_ReportDetial" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可行性测试项目-明细</title>
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
            //end 鼠标经过变色

            //获取传递参数的值
            var id = $("#hidID").val();
            var no = $("#hidNo").val();
            var name = $("#hidProjName").val();
            var code = $("#hidCode").val();
            var mid = $("#hidmid").val();
            var did = $("#hiddid").val();
            var testPlan = $("#labTestPlan").text();
            var createBy = $("#hidCreateBy").val();
            var createName = $("#hidCreateName").val();
            //获取被分配的测试人员的信息（即权限）
            var testUser = $("#labTestUserName").text();
            var testUserID = $("#hidTestUserID").val();
            var uID = $("#hiduID").val();
            var uName = $("#hiduName").val();
            //获取项目经理信息
            var manageUser = $("#labProjectManage").text();
            var manageUserID = $("#hidProjectManageID").val();
            //获取整个项目相关人员的信息（即权限）
            var PowerName = $("#hidPowerName").val();
            //获取效果确认权限
            var effectConfirm = $("#hidEffectConfirm").val();

            //拼接的参数
            var param = '&id=' + id + '&no=' + no + '&name=' + name + '&code=' + code + '&mid=' + mid + '&did=' + did + '&planDate=' + testPlan + '&createBy=' + createBy + '&createName=' + createName;
            //选择人员
            $(".person").dblclick(function () {
                if (manageUserID.indexOf(";" + uID + ";") >= 0) {
                    var _src = '../RDW/conn_choose2.aspx?' + param;
                    $.window("可靠性测试人员选择", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有分配测试人员的权限');
                    return false;
                }
            });
            //end选择人员

            //测试计划
            $(".testPlan").dblclick(function () {
                if (manageUserID.indexOf(";" + uID + ";") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=testPlan' + param;
                    $.window("测试计划", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有分配测试人员的权限');
                    return false;
                }
            });
            //原因分析
            $(".analysisReason").dblclick(function () {
                if (testUserID.indexOf("," + uID + ",") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=analysisReason' + param;
                    $.window("原因分析", "80%", "80%", _src, "", true);
                    return false;
                }
                else
                {
                    alert('您没有原因分析的权限');
                    return false;
                }
            });
            //临时解决方案
            $(".tempSolve").dblclick(function () {
                if (testUserID.indexOf("," + uID + ",") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=tempSolve' + param;
                    $.window("临时解决方案", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有临时解决方案的权限');
                    return false;
                }
            });
            //临时行动
            $(".tempAction").dblclick(function () {
                if (testUserID.indexOf("," + uID + ",") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=tempAction' + param;
                    $.window("临时行动", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有临时行动的权限');
                    return false;
                }
            });
            //根本解决方案
            $(".realSolve").dblclick(function () {
                if (testUserID.indexOf("," + uID + ",") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=realSolve' + param;
                    $.window("根本解决方案", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有根本解决方案的权限');
                    return false;
                }
            });
            //根本行动
            $(".realAction").dblclick(function () {
                if (testUserID.indexOf("," + uID + ",") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=realAction' + param;
                    $.window("根本行动", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有根本行动的权限');
                    return false;
                }
            }); 
            //效果确认
            $(".effectConfirm").dblclick(function () {
                if (effectConfirm.indexOf(";" + uID + ";") >= 0) {
                    var _src = '../RDW/Test_Message.aspx?dept=effectConfirm' + param;
                    $.window("效果确认", "80%", "80%", _src, "", true);
                    return false;
                }
                else {
                    alert('您没有效果确认的权限');
                    return false;
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
                <td class="FixedGridTD FixedGridRight"></td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft" rowspan="3">测试单信息</td>
                <td colspan="3">跟踪单号:</td>
                <td colspan="3">
                    <asp:Label ID="labNo" runat="server" Text="" Enabled="false"></asp:Label>
                </td>
                <td colspan="3">项目代码:</td>
                <td colspan="3">
                    <asp:Label ID="labCode" runat="server" Text=""></asp:Label>
                </td>
                <td class="FixedGridRight"rowspan="3" align="center">
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" Width="80px" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridTD"></td>
                <td colspan="3">分类</td>
                <td colspan="3">
                    <asp:Label ID="labType" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="3">失效时间</td>
                <td colspan="3">
                    <asp:Label ID="labFailureTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridTD"></td>
                <td colspan="3" style="height:40px;">问题内容</td>
                <td id="tdFileByProc" runat="server" colspan="9">                    
                    <asp:Label ID="labProblemContent" runat="server" Text=""></asp:Label>
                </td>
            </tr>


            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft testPlan" rowspan="4">跟踪计划</td>
                <td colspan="3" class="person">负责人</td>
                <td colspan="3" class="person">
                    <asp:Label ID="labTestUserName" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidTestUserID" runat="server" />
                </td>
                <td colspan="3" class="testPlan">计划完成时间</td>
                <td colspan="3" class="testPlan">
                    <asp:Label ID="labTestPlan" runat="server" Text=""></asp:Label>
                </td>
                <td class="FixedGridRight testPlan" rowspan="4">
                    <asp:Label ID="labProjectManage" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidProjectManageID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>                    
                <td id="tdMsgAndFileByTestPlan" class="testPlan" runat="server"  colspan="12" rowspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>



            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft analysisReason" rowspan="3">原因分析</td>                
                <td id="tdMsgAndFileByAnalysisReason" class="analysisReason" runat="server" colspan="12" rowspan="3"></td>
                <td class="FixedGridRight" rowspan="15">
                    <asp:Label ID="labUserPower" runat="server"></asp:Label>
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
                <td class="FixedGridLeft tempSolve" rowspan="3">临时解决方案</td>                
                <td id="tdMsgAndFileByTempSolve" class="tempSolve" runat="server" colspan="12" rowspan="3"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft tempAction" rowspan="3">临时行动</td>                
                <td id="tdMsgAndFileByTempAction" class="tempAction" runat="server" colspan="12" rowspan="3"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft realSolve" rowspan="3">根本解决方案</td>                
                <td id="tdMsgAndFileByRealSolve" class="realSolve" runat="server" colspan="12" rowspan="3"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>

            
            <tr>
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft realAction" rowspan="3">根本行动</td>                
                <td id="tdMsgAndFileByRealAction" class="realAction" runat="server" colspan="12" rowspan="3"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>

            
            <tr class="effectConfirm">
                <td class="FixedGridTD"></td>
                <td class="FixedGridLeft" rowspan="3">效果确认</td>                
                <td id="tdMsgAndFileByEffectConfirm" runat="server" colspan="12" rowspan="2"></td>
                <td class="FixedGridRight" rowspan="3">
                    <asp:Label ID="labEffectConfirm" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidEffectConfirm" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
            </tr>
            <tr>
                <td class="FixedGridTD"></td>
                <td colspan="9"></td>
                <td colspan="3">
                    <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" CssClass="SmallButton3" />
                    <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" CssClass="SmallButton3" />
                </td>
            </tr>
        </table>
    </div>        
        <asp:HiddenField ID="hidNo" runat="server" />
        <asp:HiddenField ID="hidCode" runat="server" />
        <asp:HiddenField ID="hidProjName" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidmid" runat="server" />
        <asp:HiddenField ID="hiddid" runat="server" />
        <asp:HiddenField ID="hiduID" runat="server" />
        <asp:HiddenField ID="hiduName" runat="server" />
        <asp:HiddenField ID="hidPowerName" runat="server" />
        <asp:HiddenField ID="hidCreateBy" runat="server" />
        <asp:HiddenField ID="hidCreateName" runat="server" />
    </form>
</body>
</html>
