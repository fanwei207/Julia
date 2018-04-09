<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_detail.aspx.cs" Inherits="m5_new" %>

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

            var _index = $("#hidTabIndex").val();

            var $tabs = $("#divTabs").tabs({ active: _index });

            $("#chkValid").click(function(){
                if(!$("#chkIsApprove").prop("checked"))
                {
                    alert("Supervisor has not yet signed！");
                }
            });

            $("#chkNotValid").click(function(){
                if(!$("#chkIsApprove").prop("checked"))
                {
                    alert("Supervisor has not yet signed！");
                }
            });

            $("#gv1").dblclick(function(){
                if($("#chkIsApprove").prop("checked"))
                {
                    if($("#chkValid").prop("checked") || $("#chkNotValid").prop("checked"))
                    {
                        alert("The operation is not accepted");
                    } else{
                        var _no = $("#lblNo").text();
                        var _src = "/product/m5_effect_det.aspx?no=" + _no ;
                        $.window("review", "70%", "80%", _src, "", true);
                        //window.showModalDialog('m5_effect_det.aspx?no=' + _no, window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');
                        //window.location.href = "m5_detail.aspx?index=1&no=" + _no;
                    }
                }
                else
                {
                    alert("Supervisor has not yet signed！");
                }
            });


            ///如果申请与安规相关
            var isAboutSafety = $(".isAboutSafety");
            var lbSafety = $("#lbisChangeSafety").text();
           
            if(lbSafety == 'YES')
            {
                isAboutSafety.css("background","red")
            }
            else
            {
                isAboutSafety.css("background","#fff")
            }

           
            //$("#gv1").children().children().each(function(){
            //    if($(this).children().eq(0).text() == "Responsibility")
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        $(this).click(function (){
                      
            //            if($("#chkIsApprove").prop("checked"))
            //            {
            //                if($("#chkValid").prop("checked") || $("#chkNotValid").prop("checked"))
            //                {
            //                    alert("The operation is not accepted");
            //                } else{
            //                    var _descEn = $(this).children().eq(0).text();
            //                    var _no = $("#lblNo").text();
            //                    //$.window("Opinion",1000,1500,"../m5_effect_det.aspx?no=" + _no + "&descEn="+ _descEn,"", true);
            //                    var _src = "/product/m5_effect_det.aspx?no=" + _no + "&descEn="+ _descEn;
            //                    $.window("变更申请单关闭原因", "70%", "80%", _src, "", true);
            //                    return false;
            //                    //window.showModalDialog('m5_effect_det.aspx?no=' + _no + "&descEn="+ _descEn , window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');
            //                    //window.location.href = "m5_detail.aspx?index=1&no=" + _no;
            //                }
            //            }
            //            else
            //            {
            //                alert("Supervisor has not yet signed！");
            //            }
                    
            //        });

            //    }
            //});
         

            $("#gv2").dblclick(function(){
                if($("#chkIsApprove").prop("checked"))
                {
                    if($("#chkValid").prop("checked"))
                    {
                        if($("#chkAgree").prop("checked") || $("#chkNotAgree").prop("checked"))
                        {
                            alert("The operation is not accepted");
                        } else{
                            var _no = $("#lblNo").text();
                            window.showModalDialog('m5_valid_det.aspx?no=' + _no, window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');
                            window.location.href = "m5_detail.aspx?index=2&no=" + _no;
                        }
                    }
                    else
                    {
                        alert("The operation is not accepted");
                    }
                } else
                {
                    alert("Supervisor has not yet signed！");
                }
            });

            $("#ecnProcess").click(function(){
                window.open("../Docs/ECN.pdf", "_blank");
                return false;
            });

            //指定执行人
            $("#uExcutor").click(function(){
                if($("#chkAgree").prop("checked") || $("#chkNotAgree").prop("checked"))
                {
                    alert("The operation is not accepted");
                } else{
                    if($("#btnAgree").prop("disabled")){
                        alert("The operation is not accepted");
                    } else{
                        var _no = $("#lblNo").text();
                        var _src = "../product/m5_excutor.aspx?no=" + _no;

                        $.window("指定执行人", 800, 600, _src, "", true);
                    }
                }
            });
            //end $("#uExcutor")
            $("#agree").dblclick(function(){
                var _no = $("#lblNo").text();
                //var _need = $("#hidNeed").val();
                var _agree = $("#hidAgree").val();
                var _appr = $("#hidAppr").val();
                var _effdate = $("#labEffDate").text();
                var _uexcutor = $("#labUExcutor").text();
                var _emailAddress = $("#hidEmailAddress").val();
                var _agreeAuth = $("#hidAgreeAuth").val();
                if(_agreeAuth.toLowerCase() == "true" ? false :true)
                {
                    alert("The operation is not accepted");
                    return false;
                
                }
                if(_appr == 0)
                {
                    alert("The operation is not accepted2");
                    return false;
                }
                else if(_appr == 2)
                {
                    alert("The operation is not accepted3");
                    return false;
                }
                //if(_need == 0)
                //{
                //    alert("The operation is not accepted4");
                //    return false;
                //}
                //else if(_need == 2)
                //{
                //    alert("The operation is not accepted5");
                //    return false;
                //}
                if(_agree == 1)
                {
                    alert("The operation is not accepted6");
                    return false;
                }
                else
                {
                    var _src = "/product/m5_mstrMsg.aspx?type=agree&no=" + _no + "&emailTo=" + _emailAddress + "&effdate=" + _effdate + "&uexcutor=" + _uexcutor;
                    $.window("Message", "80%", "80%", _src, "", true);
                    return false;
                }
            })
            $("#btnProdApp").click(function () {
                //if (typeof ($.confirm_retValue) == "undefined" ||
                //    (typeof ($.confirm_retValue) != "undefined" && $.confirm_retValue)) {
                //    var _no = $(this).parent().parent().find(".no").html();
                //    var _src = "/product/m5_appNew.aspx?no=" + _no;
                //    $.window("变更申请试流单", "50%", "50%", _src, "", true);
                //    return false;
                //}
                var _no = $("#lblNo").text();
                //var _src = "/product/m5_appNew.aspx";
                var _src = "/RDW/prod_Report.aspx?from=rdw&name=" + _no;
                $.window("变更申请试流单", "90%", "95%", _src, "", true);
                return false;
            });

            
            $(".changeDetails").dblclick(function(){

                var _no = $("#lblNo").text();
                var _model = $("#lbModel").text();
                    if($("#hidReasonChange").val() == "1")
                    {
                        var _src = "/product/m5_modifiedApply.aspx?no=" + _no +"&model="+_model ;
                        $.window("Message", "60%", "80%", _src, "", true);
                        return false;
                    }
                    else
                    {
                        alert("Modification conditions are not satisfied");
                        return false;
                    }
                });
            


            $("#agreeMsg").dblclick(function(){
                var _no = $("#lblNo").text();
               // var _need = $("#hidNeed").val();
                var _agree = $("#hidAgree").val();
                var _appr = $("#hidAppr").val();
                var _effdate = $("#labEffDate").text();
                var _uexcutor = $("#labUExcutor").text();
                var _emailAddress = $("#hidEmailAddress").val();
                var _agreeAuth = $("#hidAgreeAuth").val();
                if(_agreeAuth.toLowerCase() == "true" ? false :true)
                {
                    alert("The operation is not accepted");
                    return false;
                
                }
                if(_appr == 0)
                {
                    alert("The operation is not accepted");
                    return false;
                }
                else if(_appr == 2)
                {
                    alert("The operation is not accepted");
                    return false;
                }
                //if(_need == 0)
                //{
                //    alert("The operation is not accepted");
                //    return false;
                //}
                //else if(_need == 2)
                //{
                //    alert("The operation is not accepted");
                //    return false;
                //}
                if(_agree == 1)
                {
                    alert("The operation is not accepted");
                    return false;
                }
                else
                {
                    var _src = "/product/m5_mstrMsg.aspx?type=agree&no=" + _no + "&emailTo=" + _emailAddress + "&effdate=" + _effdate + "&uexcutor=" + _uexcutor;
                    $.window("Message", "80%", "80%", _src, "", true);
                    return false;
                }
            })
        })
    
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
        .auto-style20 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <div id="divTabs">
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;FORM&nbsp;&nbsp;</a></li>
                    <li><a href="#tabs-2">&nbsp;&nbsp;REVIEW&nbsp;&nbsp;</a></li>
               <%--     <li><a href="#tabs-3">&nbsp;&nbsp;VERIFICATION &nbsp;&nbsp;</a></li>--%>
                    <li><a id="ecnProcess" href="#">&nbsp;PROCESS&nbsp;</a></li>
                    <li>
                        <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="btnBack_Click" Width="80px" />
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        <input id="hidEmailAddress" type="hidden" value="" runat="server" />
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
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>No.</strong></td>
                            <td colspan="10">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblNo" runat="server" Text="ECN-1504-13"></asp:Label>
                            </td>
                            <td colspan="2"><strong>Applicant</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lblCreateName" runat="server" Font-Bold="False"></asp:Label>
                                <input id="hidCreateBy" runat="server" type="hidden" />
                            </td>
                            <td class="FixedGridRight" rowspan="4">
                                <asp:CheckBox ID="chkIsApprove" Style="display: none;" runat="server" />
                                <asp:CheckBox ID="chkIsAgree" Style="display: none;" runat="server" /></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>Type</strong></td>
                            <td colspan="10">
                                <asp:CheckBoxList ID="radProject" runat="server" RepeatDirection="Horizontal" DataTextField="m5p_projectEn" DataValueField="m5p_id" CellPadding="10" CellSpacing="1" Font-Bold="False" Enabled="False" RepeatLayout="Flow">
                                    <asp:ListItem>人员</asp:ListItem>
                                    <asp:ListItem>设备</asp:ListItem>
                                    <asp:ListItem>物料</asp:ListItem>
                                    <asp:ListItem>工艺</asp:ListItem>
                                    <asp:ListItem>环境</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td colspan="2"><strong>Marketing</strong></td>
                            <td colspan="4">
                                <asp:Label ID="lblMarket" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                             <td class="FixedGridHeight" style="height: 26px"></td>
                            <td class="FixedGridLeft" style="height: 26px"><strong>Model</strong></td>
                            <td colspan="10" class="auto-style20"><asp:Label ID="lbModel" runat="server"></asp:Label></td>
                            <td colspan="2" class="auto-style20"><strong>Level</strong></td>
                            <td colspan="4" class="auto-style20"><asp:Label ID="lbLevel" runat="server"></asp:Label></td>


                        </tr>
                               <tr class="NoTrHover isAboutSafety">
                             <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"><strong>About Safety</strong></td>
                            <td colspan="16"><asp:Label ID="lbisChangeSafety" runat="server"></asp:Label></td>
                        


                        </tr>
                        <tr class="NoTrHover changeDetails">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Reason</strong
                                </td>
                            <td colspan="16" rowspan="6" ID="txtReason" runat="server">
                               <%-- <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="500" BorderStyle="None" ReadOnly="True"></asp:TextBox>--%>
                            </td>
                            <td class="FixedGridRight" rowspan="6"></td>
                        </tr>

                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"> <asp:HiddenField ID="hidReasonChange"  runat="server"/></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                         <%--   <td colspan="16">
                               
                                <asp:HyperLink ID="hlinkReason" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                            </td>--%>
                        </tr>
                        <tr class="changeDetails">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="6"><strong>Details</strong></td>
                            <td colspan="16" rowspan="6" ID="txtDesc" runat="server" >
                             <%--   <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="500" BorderStyle="None" ReadOnly="True"></asp:TextBox>--%>
                            </td>
                            <td class="FixedGridRight" rowspan="6"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                          <%--  <td colspan="16">
                                <asp:HyperLink ID="hlinkDesc" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Supervisor</strong></td>
                            <td colspan="16" rowspan="5">
                                <asp:TextBox ID="txtApprMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD"></asp:TextBox>
                            </td>
                            <td class="FixedGridRight" rowspan="7"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="16">
                                <asp:HyperLink ID="hlinkManager" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                                <input id="fileManager" style="width: 90%; height: 23px" type="file" size="45" name="filename2"
                                    runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10"></td>
                            <td colspan="3">
                                <asp:Button ID="btnDone" runat="server" Text="Agree" CssClass="SmallButton3" OnClick="btnDone_Click" Width="150px" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnNotDone" runat="server" Text="Disagree" CssClass="SmallButton3" OnClick="btnNotDone_Click" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" colspan="18"><strong>
                                Each dept. is to check the  effects caused by the Change and confirm whether agree to the Change</strong></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" colspan="4"><strong>Responsibility</strong></td>
                            <td colspan="13" style="text-align: center;"><strong>Comment</strong></td>
                            <td class="FixedGridRight"><strong>Charged By</strong></td>
                        </tr>
                        <% Response.Write(EffectTR); %>
<%--                        <tr> 
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Notice</strong></td>
                            <td colspan="17" rowspan="5">
                                <asp:TextBox ID="txtValidMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD"></asp:TextBox>
                            </td>
                            <%--<td class="FixedGridRight" rowspan="7">
                               <%-- <asp:Button ID="btnProdApp" runat="server" Text="ProdApp" CssClass="SmallButton3" Visible ="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                           <td colspan="17">
                                <asp:HyperLink ID="hlinkValid" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">Attachment:</asp:HyperLink>
                                <input id="fileValid" style="width: 90%; height: 23px" type="file" size="45" name="filename3"
                                    runat="server" /></td>
                        </tr>--%>
                           <tr id="agree">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7">Notice</td>
                            <td colspan="2"><strong>Executed Date:</strong></td>
                            <td colspan="4">    
                                <asp:Label ID="labEffDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblExcutor" runat="server" Text="Executed By:"></asp:Label></td>
                            <td colspan="8">                                
                                <asp:Label ID="labUExcutor" runat="server" Text=""></asp:Label>
                               <asp:HiddenField id="hidUExcutorEmail" runat="server"/>
                            </td>
                           <td align="center"  class="FixedGridLeft" id="tdAgree" rowspan="6"  runat="server" style="word-break:break-all; word-wrap:break-word;border-bottom:none;border-top:inherit;border-left:inherit;border-right:inherit"></td>
                        </tr>
                        <tr id="agreeMsg">
                            <td class="FixedGridHeight"></td>
                            <td colspan="16" rowspan="5" id="tdAgreeMsg" runat="server">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight" style="height: 25px"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                                <asp:Label ID="lbAboutBoom" runat="server"  ></asp:Label>
                                <asp:CheckBox ID="chkValid" runat="server" Text="是" Enabled="False" Style="display: none;" />
                                <asp:CheckBox ID="chkNotValid" runat="server" Text="否" Enabled="False" Style="display: none;" />
                                <%--<asp:Label ID="labProd" runat="server" Text=""></asp:Label>--%>
                            </td>
                            <td colspan="3" >
                                <asp:Button ID="btnValid" runat="server" Text="Agree" CssClass="SmallButton3" Width="150px" OnClick="btnValid_Click" Enabled="False" />
                            </td>
                            <td colspan="3" >
                                <asp:Button ID="btnNotValid" runat="server" Text="DisAgree" CssClass="SmallButton3" OnClick="btnNotValid_Click" Width="150px" Enabled="False" />
                            </td>
                            <td style="border-bottom:none;border-top:none"></td>
                        </tr>
<%--                                 <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" colspan="4"><strong>Department</strong></td>
                            <td colspan="13" style="text-align: center;"><strong>Comment</strong></td>
                            <td class="FixedGridRight"><strong>Charge By</strong></td>
                        </tr>
                         <% Response.Write(ValidTR); %>
                        <tr id="agree">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Agreement(Y/N)</strong></td>
                            <td colspan="2"><strong>Effective Date:</strong></td>
                            <td colspan="4">    
                                <asp:Label ID="labEffDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblExcutor" runat="server" Text="Executed By:"></asp:Label></td>
                            <td colspan="8">                                
                                <asp:Label ID="labUExcutor" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="FixedGridRight" rowspan="7"></td>
                        </tr>
                        <tr id="agreeMsg">
                            <td class="FixedGridHeight"></td>
                            <td colspan="16" rowspan="5" id="tdAgreeMsg" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight" style="height: 25px"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                                <asp:CheckBox ID="chkAgree" runat="server" Text="是" Style="display: none;" />
                                <asp:CheckBox ID="chkNotAgree" runat="server" Text="否" Style="display: none;" /></td>
                            <td colspan="3">
                                <asp:Button ID="btnAgree" runat="server" Text="YES" CssClass="SmallButton3" OnClick="btnAgree_Click" Width="150px" Enabled="False" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnNotAgree" runat="server" Text="NO" CssClass="SmallButton3" OnClick="btnNotAgree_Click" Width="150px" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Result</strong></td>
                            <td colspan="16" rowspan="5">
                                <asp:TextBox ID="txtExcuteMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD"></asp:TextBox>
                            </td>
                            <td class="FixedGridRight" rowspan="7">
                                <%
                                    foreach (DataRow row in this.GetExecutor(lblNo.Text).Rows)
                                    {
                                        if (Session["uID"].ToString() == row["m5xu_userid"].ToString())
                                        {
                                            chkAccessExcute.Checked = true;
                                        }

                                        Response.Write(row["m5xu_userName"].ToString() + " - " + row["englishName"].ToString() + "<br />");
                                    }

                                    btnExcute.Enabled = chkIsExcute.Checked ? false : chkAccessExcute.Checked;
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="16">
                                <asp:HyperLink ID="hlinkExcute" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">附件:</asp:HyperLink>
                                <input id="fileExcute" style="width: 90%; height: 23px" type="file" size="45" name="filename4"
                                    runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                                <asp:CheckBox ID="chkAccessExcute" Style="display: none;" runat="server" />
                                <asp:CheckBox ID="chkIsExcute" Style="display: none;" runat="server" />
                                </td>
                            <td colspan="3">
                                <asp:Button ID="btnPowerExecute" runat="server" Text="Confirm" CssClass="SmallButton3"  Width="150px" Enabled="False" OnClick="btnPowerExecute_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnExcute" runat="server" Text="Result Confirm" CssClass="SmallButton3" OnClick="btnExcute_Click" Width="155px" Enabled="False" />
                            </td>
                        </tr>--%>
                        <tr style="display: none;">
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="FixedGridRight" style="border-bottom:none;border-top:none"></td>
                        </tr>
                      
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td class="FixedGridLeft" rowspan="7"><strong>Result</strong></td>
                            <td colspan="16" rowspan="5"  >
                                <div  style="border-bottom:inherit">ECN Project : <asp:HyperLink ID="hlRDW" runat="server"></asp:HyperLink></div>  
                                <asp:TextBox ID="txtExcuteMsg" runat="server" TextMode="MultiLine" Width="99%" Height="150px"
                                    MaxLength="300" BorderStyle="None" BackColor="#DDFFDD"></asp:TextBox>

                            </td>
                            <td class="FixedGridRight" rowspan="7">
                                <%
                                    foreach (DataRow row in this.GetExecutor(lblNo.Text).Rows)
                                    {
                                        if (Session["uID"].ToString() == row["m5xu_userid"].ToString())
                                        {
                                            chkAccessExcute.Checked = true;
                                        }

                                        Response.Write(row["m5xu_userName"].ToString() + " - " + row["englishName"].ToString() + "<br />");
                                    }

                                    btnExcute.Enabled = chkValid.Checked && (chkIsExcute.Checked ? false : chkAccessExcute.Checked);
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="16">
                                <asp:HyperLink ID="hlinkExcute" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">附件:</asp:HyperLink>
                                <input id="fileExcute" style="width: 90%; height: 23px" type="file" size="45" name="filename4"
                                    runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="FixedGridHeight"></td>
                            <td colspan="10">
                                <asp:CheckBox ID="chkAccessExcute" Style="display: none;" runat="server" />
                                <asp:CheckBox ID="chkIsExcute" Style="display: none;" runat="server" />
                                </td>
                            <td colspan="3">
                                <%--<asp:Button ID="btnPowerExecute" runat="server" Text="Confirm" CssClass="SmallButton3"  Width="150px" Enabled="False" OnClick="btnPowerExecute_Click" />--%>
                                <asp:Button ID="btnExcute" runat="server" Text="Result Confirm" CssClass="SmallButton3" OnClick="btnExcute_Click" Width="155px" Enabled="False" />
                            </td>
                            <td colspan="3">
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tabs-2">
                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15" OnRowDataBound="gv1_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" Height="50px" VerticalAlign="Top" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="各单位责任" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="m5e_descEn" HeaderText="Responsibility">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Comment">
                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                <ItemStyle Width="400px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Charged By">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            <%--    <div id="tabs-3">
                    <asp:GridView ID="gv2" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15" OnRowDataBound="gv2_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" Height="50px" VerticalAlign="Top" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="各单位责任" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="m5v_descEn" HeaderText="Responsibility">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Comment">
                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                <ItemStyle Width="400px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Charged by">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>--%>
            </div>
        </div>
        <asp:HiddenField ID="hidNeed" runat="server" />
        <asp:HiddenField ID="hidAgree" runat="server" />
        <asp:HiddenField ID="hidAppr" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
     
       
    </script>
</body>
</html>
