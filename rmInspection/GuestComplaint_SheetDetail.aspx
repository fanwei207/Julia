<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_SheetDetail.aspx.cs" Inherits="rmInspection_GuestComplaint_SheetList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户投诉-投诉单列表明细</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
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

            $("#preResultContent").dblclick(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    var _no = $("#hidGuestComplaintNo").val();
                    var _moduleId = 6;
                    var _src = "/rmInspection/GuestComplaint_Conent.aspx?no=" + _no + "&moduleId=" + _moduleId;
                    $.window("review", "70%", "80%", _src, "", true);
                }
                else
                {
                    alert("不允许进行此操作");
                }
            });

            $("#managerAprContent").dblclick(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                if(!$("#managerAgree").prop("disabled"))
                {
                    var _no = $("#hidGuestComplaintNo").val();
                    var _moduleId = 3;
                    var _src = "/rmInspection/GuestComplaint_Conent.aspx?no=" + _no + "&moduleId=" + _moduleId;
                    $.window("review", "70%", "80%", _src, "", true);
                }
                else
                {
                    alert("不允许进行此操作");
                }
            });  
            
            $("#executorContent").dblclick(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                else if(!$("#managerAgree").prop("disabled"))
                {
                    alert("经理未审批！");
                    return false;
                }
                else if(!$("#feedbackAgree").prop("disabled"))
                {
                    alert("反馈客户未审批！");
                    return false;
                }else if(!$("#btnFinishOrder").prop("disabled"))
                {
                    var _no = $("#hidGuestComplaintNo").val();
                    var _moduleId = 1;
                    var _src = "/rmInspection/GuestComplaint_Conent.aspx?no=" + _no + "&moduleId=" + _moduleId;
                    $.window("review", "70%", "80%", _src, "", true);
                } 
                else
                {
                    alert("不允许进行此操作");
                }
            });  
         
            $("#feedbackContent").dblclick(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                else if(!$("#managerAgree").prop("disabled"))
                {
                    alert("经理未审批！");
                    return false;
                }else if(!$("#feedbackAgree").prop("disabled"))
                {
                    var _no = $("#hidGuestComplaintNo").val();
                    var _moduleId = 2;
                    var _src = "/rmInspection/GuestComplaint_Conent.aspx?no=" + _no + "&moduleId=" + _moduleId;
                    $.window("review", "70%", "80%", _src, "", true);
                }  
                else
                {
                    alert("不允许进行此操作");
                }
            });                
            //$("#gv1").dblclick(function(){
            //    if(!$("#btnPreAgree").prop("disabled")&& !$("#btnPreNotAgree").prop("disabled"))
            //    {
            //        alert("不允许进行此操作");
            //    }  
            //});
            $("#btnApproachAgree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
            });

            $("#btnApproachDisagree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
            });

            $("#managerAgree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
            });

            $("#managerDisagree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
            });

            $("#feedbackAgree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                else if(!$("#managerAgree").prop("disabled"))
                {
                    alert("经理未审批！");
                    return false;
                }
            });

            $("#feedbackDisagree").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                else if(!$("#managerAgree").prop("disabled"))
                {
                    alert("经理未审批！");
                    return false;
                }
            });

            $("#btnFinishOrder").click(function(){
                if(!$("#btnPreAgree").prop("disabled"))
                {
                    alert("未形成初判结果！");
                    return false;
                }
                else if(!$("#btnFinAgree").prop("disabled"))
                {
                    alert("财务未审批！");
                    return false;
                }
                else if(!$("#btnApproachAgree").prop("disabled"))
                {
                    alert("未决定赔偿方式！");
                    return false;
                }
                else if(!$("#managerAgree").prop("disabled"))
                {
                    alert("经理未审批！");
                    return false;
                }
                else if(!$("#feedbackAgree").prop("disabled"))
                {
                    alert("反馈客户未审批！");
                    return false;
                }
            });
            
            //$("#ecnProcess").click(function(){
            //    window.open("../Docs/GuestComp.pdf", "_blank");
            //    return false;
            //});
                                                
        })

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <div id="divTabs">
                <asp:Label runat="server">顾客投诉处理表：SQL-8.5-006</asp:Label>
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;FORM&nbsp;&nbsp;</a></li>
                    <li><a href="#tabs-2">&nbsp;&nbsp;REVIEW&nbsp;&nbsp;</a></li>
                    <%--<li><a id="ecnProcess" href="#">&nbsp;PROCESS&nbsp;</a></li>--%>
                    <li>
                        <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" Width="80px" />
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        <input id="hidEmailAddress" type="hidden" value="" runat="server" />
                    </li>
                </ul>
                <div id="tabs-1" style="margin-top: 10px;" align="center">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth">
                                <asp:HiddenField ID="hidGuestComplaintNo" runat="server" />
                            </td>
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
                            <td class="FixedGridRight" style="border-right: 1px solid #fff; width: 150px;"></td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="10">客诉申请单</td>
                            <td colspan="13"></td>
                            <td colspan="3" rowspan="10">
                                <asp:Label ID="applyList" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="FixedGridRight">客户号:</td>
                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblGuestNo" runat="server"></asp:Label>
                            </td>
                            <td colspan="7" class="FixedGridRight" style="text-align: center">客户需求的解决方式:</td>
                        </tr>
                        <tr>
                            <td colspan="2" class="FixedGridRight" rowspan="2">客户名称:</td>
                            <td colspan="4" rowspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblGuestName" runat="server"></asp:Label>
                            </td>
                            <td colspan="7" rowspan="5" style="text-align: left">
                                <asp:Label ID="lblApproach" runat="server"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="FixedGridRight">客户等级:</td>
                            <td style="text-align: center;" colspan="4">
                                <asp:Label ID="lblGuestLevel" runat="server" Width="150px"> </asp:Label>
                            </td>
                            <%--  <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </td>--%>
                            <td colspan="7" class="FixedGridWidth"></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="FixedGridRight">严重等级:</td>
                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblSeverity" runat="server"> </asp:Label>
                            </td>
                            <td colspan="7" class="FixedGridWidth"></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="FixedGridRight">客诉接收日期:</td>
                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblReceivedDate" runat="server"></asp:Label>
                            </td>
                            <td colspan="7" class="FixedGridWidth"></td>
                        </tr>
                        <tr>
                            <td colspan="2">上传文件：</td>
                            <td colspan="11">
                                <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    DataKeyNames="ID,GuestComp_No,GuestComp_FileName,GuestComp_FilePath,createBy,createName,createDate"
                                    OnRowCommand="gvFile_RowCommand" AllowPaging="False">
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
                                                <asp:TableCell HorizontalAlign="center" Text="文件名" Width="240px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="上传日期" Width="170px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="创建人" Width="120px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="查看" Width="50px"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="GuestComp_FileName" HeaderText="文件名">
                                            <HeaderStyle Width="240px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="240px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                            <HeaderStyle Width="170px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="170px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createName" HeaderText="创建人">
                                            <HeaderStyle Width="120px" HorizontalAlign="center" Font-Bold="False" />
                                            <ItemStyle Width="120px" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:ButtonField Text="View" HeaderText="查看" CommandName="View">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2">导入文件：</td>
                            <td colspan="11" rowspan="2">
                                <asp:GridView ID="gvImport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    DataKeyNames="FileID,CustPo,PoLine,CustPart,UM,Qad,Price,Currency,Location,Qty,DateCode,FOB,CreateBy,CreateName,CreateDate"
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
                                                <asp:TableCell HorizontalAlign="center" Text="订单号" Width="110px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="物料号" Width="110px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="单位" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="QAD" Width="110px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="价格" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="币种" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="单位" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="位置" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="数量" Width="30px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="周期章" Width="110px"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="CustPo" HeaderText="订单号">
                                            <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PoLine" HeaderText="行号">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustPart" HeaderText="物料号">
                                            <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UM" HeaderText="单位">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qad" HeaderText="QAD">
                                            <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Price" HeaderText="价格">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Currency" HeaderText="币种">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location" HeaderText="位置">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="数量">
                                            <HeaderStyle Width="30px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DateCode" HeaderText="周期章">
                                            <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FOB" HeaderText="FOB价">
                                            <HeaderStyle Width="40px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="40px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreateDate" HeaderText="上传日期">
                                            <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreateName" HeaderText="创建人">
                                            <HeaderStyle Width="60px" HorizontalAlign="Left" Font-Bold="False" />
                                            <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <%--<tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>--%>
                        <tr>
                            <td colspan="3" rowspan="5">客诉初判结果</td>
                            <td colspan="13" rowspan="4" id="preResultContent" runat="server"></td>
                            <td colspan="3" rowspan="5">
                                <asp:Label ID="preResult" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridWidth">责任方：</td>
                            <td colspan="6">
                                <asp:CheckBoxList ID="radDuty" runat="server" RepeatDirection="Horizontal" DataTextField="DutyName" DataValueField="DutyID" CellPadding="10" CellSpacing="1" Font-Bold="False" Enabled="true" RepeatLayout="Flow" AutoPostBack="false" OnSelectedIndexChanged="radDuty_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnPreAgree" runat="server" Text="同意" CssClass="SmallButton3" Width="150px" OnClick="btnPreAgree_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnPreNotAgree" runat="server" Text="不同意" CssClass="SmallButton3" Width="150px" OnClick="btnPreNotAgree_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><strong>责任方</strong></td>
                            <td colspan="13" style="text-align: center;"><strong>描述</strong></td>
                            <td colspan="3"><strong>负责人</strong></td>
                        </tr>
                        <% Response.Write(EffectTR); %>
                        <tr>
                            <td colspan="3" rowspan="7">财务填写方案相关金额</td>
                            <td colspan="13" rowspan="6" id="financeContent" runat="server">
                                <asp:GridView ID="gvFinance" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" OnRowDataBound="gvFinance_RowDataBound" OnRowCancelingEdit="gvFinance_RowCancelingEdit"
                                    OnRowEditing="gvFinance_RowEditing"
                                    Width="620px" DataKeyNames="ID,FinanceApproachID,FinanceApproachName,FinancePrice" OnRowUpdating="gvFinance_RowUpdating">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table1" Width="510px" CellPadding="-1" CellSpacing="0" runat="server"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell Text="解决方式" Width="340px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                                <asp:TableCell Text="价格" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="FinanceApproachName" HeaderText="解决方式" ReadOnly="true">
                                            <HeaderStyle Width="340px" HorizontalAlign="Center" Font-Bold="False" />
                                            <ItemStyle Width="340px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FinancePrice" HeaderText="价格">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowEditButton="True">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td colspan="3" rowspan="7">
                                <asp:Label ID="financeAproach" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridLeftCorner"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridWidth"></td>
                            <td colspan="6"></td>
                            <td colspan="3">
                                <asp:Button ID="btnFinAgree" runat="server" Text="同意" CssClass="SmallButton3" Width="150px" OnClick="btnFinAgree_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnFinDisagree" runat="server" Text="不同意" CssClass="SmallButton3" Width="150px" OnClick="btnFinDisagree_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="6">决定赔偿方式</td>
                            <td colspan="13" rowspan="5" id="decideAproachContent">
                                <asp:GridView ID="gvDecide" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" OnRowDataBound="gvDecide_RowDataBound"
                                    Width="620px" DataKeyNames="ID,FinanceApproachID,FinanceApproachName,FinancePrice">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table1" Width="510px" CellPadding="-1" CellSpacing="0" runat="server"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell Text="解决方式" Width="340px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                                <asp:TableCell Text="价格" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FinanceApproachName" HeaderText="解决方式" ReadOnly="true">
                                            <HeaderStyle Width="340px" HorizontalAlign="Center" Font-Bold="False" />
                                            <ItemStyle Width="340px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FinancePrice" HeaderText="价格">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td colspan="3" rowspan="6">
                                <asp:Label ID="decideAproach" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridLeftCorner"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridWidth"></td>
                            <td colspan="6"></td>
                            <td colspan="3">
                                <asp:Button ID="btnApproachAgree" runat="server" Text="同意" CssClass="SmallButton3" Width="150px" OnClick="btnApproachAgree_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnApproachDisagree" runat="server" Text="不同意" CssClass="SmallButton3" Width="150px" OnClick="btnApproachDisagree_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="6">总经理审批</td>
                            <td colspan="13" rowspan="5" id="managerAprContent" runat="server"></td>
                            <td colspan="3" rowspan="6">
                                <asp:Label ID="managerApr" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridLeftCorner"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridWidth"></td>
                            <td colspan="6"></td>
                            <td colspan="3">
                                <asp:Button ID="managerAgree" runat="server" Text="同意" CssClass="SmallButton3" Width="150px" OnClick="managerAgree_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="managerDisagree" runat="server" Text="不同意" CssClass="SmallButton3" Width="150px" OnClick="managerDisagree_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="7">反馈客户</td>
                            <td colspan="2">计划出运单号</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNo" runat="server" Width="90px" CssClass="SmallTextBox5"></asp:TextBox>
                            </td>
                            <td>物料号</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtPartNo" runat="server" Width="90px" CssClass="SmallTextBox5"></asp:TextBox>
                            </td>
                            <td>数量</td>
                            <td>
                                <asp:TextBox ID="txtNum" runat="server" Width="40px" CssClass="SmallTextBox5"></asp:TextBox>
                            </td>
                            <td colspan="2">出运日期</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox5 Date" Width="90px"></asp:TextBox>
                            </td>

                            <td colspan="3" rowspan="7">
                                <asp:Label ID="feedback" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="3" rowspan="7">反馈客户</td>
                            <td>导入文件</td>
                            <td colspan="3" valign="top" style="height: 28px">
                                <input id="file1" style="width: 468px; height: 22px" type="file" name="filename"
                                    runat="server" />
                            </td>
                            <td colspan="4">
                                <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                                    Text="导入" OnClick="btnImport_Click" />
                            </td>
                            <td colspan="2">下载模板</td>
                            <td colspan="3" align="left">
                                <asp:LinkButton ID="lkbModle" runat="server" Text="导入模板" Font-Underline="true" CommandName="down" OnClick="lkbModle_Click"></asp:LinkButton>
                            </td>
                                <td colspan="3" rowspan="7">
                                <asp:Label ID="feedback" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="13" rowspan="5" id="feedbackContent" runat="server"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridLeftCorner"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridWidth"></td>
                            <td colspan="6"></td>
                            <td colspan="3">
                                <asp:Button ID="feedbackAgree" runat="server" Text="同意" CssClass="SmallButton3" Width="150px" OnClick="feedbackAgree_Click" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="feedbackDisagree" runat="server" Text="不同意" CssClass="SmallButton3" Width="150px" OnClick="feedbackDisagree_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="6">执行方</td>
                            <td colspan="13" rowspan="5" id="executorContent" runat="server"></td>
                            <td colspan="3" rowspan="6">
                                <asp:Label ID="executor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridHeight"></td>
                        </tr>
                        <tr>
                            <td colspan="13" class="FixedGridLeftCorner"></td>
                        </tr>
                        <tr>
                            <td colspan="2">实际金额</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtActualPrice" runat="server" Width="90px" CssClass="SmallTextBox5" Enabled="true"></asp:TextBox>
                            </td>
                            <td colspan="3" style="text-align: center">执行完成时间</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtFinishedDate" runat="server" Width="90px" CssClass="SmallTextBox5 Date"></asp:TextBox>
                            </td>
                            <td colspan="4">
                                <asp:Button ID="btnFinishOrder" runat="server" Text="结单" CssClass="SmallButton3" Width="150px" OnClick="btnFinishOrder_Click" />
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
                            <asp:Table ID="Table3" Width="800px" CellPadding="11" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="责任方" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="DutyName" HeaderText="责任方">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ResDesc" HeaderText="描述">
                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                <ItemStyle Width="400px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ResponsiblePersonName" HeaderText="负责人">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <asp:HiddenField ID="hidLblID" runat="server" />
        </div>
        <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>       
        </script>
    </form>
</body>
</html>
