<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FI_view.aspx.cs" Inherits="Supplier_FI_view" %>

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
                    }
                }
                else
                {
                    alert("Supervisor has not yet signed！");
                }
            });
           
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
               
                var _no = $("#lblNo").text();
                var _src = "/RDW/prod_Report.aspx?from=rdw&name=" + _no;
                $.window("变更申请试流单", "90%", "95%", _src, "", true);
                return false;
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

    <% Response.Write(Effectscript); %>
    <style type="text/css">
        .FixedGrid {
            border-collapse: collapse;
            table-layout: fixed;
        }

            .FixedGrid .FixedGridWidth {
                width: 100px;
                height: 25px;
                border-top: 1px solid #fff;
                border-right: 1px solid #fff;
                border-left: 1px solid #fff;
            }

            .FixedGrid .FixedGridWidthLeft {
                width: 100px;
                height: 25px;
                border-top: 1px solid #fff;
                border-right: 1px solid #fff;
                border-left: 1px solid #fff;
                border-bottom: 1px solid #fff;
            }

            .FixedGrid .FixedGridWidthright {
                width: 100px;
                height: 25px;
                border-top: 1px solid #fff;
                border-right: 1px solid #fff;
                border-left: 1px solid #fff;
                border-bottom: 1px solid #fff;
            }

            .FixedGrid .FixedGridLeft {
                width: 100px;
                text-align: center;
                border-left: 1px solid #fff;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #000;
                font-weight: bold;
                height: 100px;
            }

            .FixedGrid .FixedGridRight {
                width: 100px;
                text-align: center;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #fff;
                font-weight: bold;
            }

            .FixedGrid .FixedGridLeftbody {
                width: 100px;
                text-align: center;
                border-left: 1px solid #fff;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #000;
                font-weight: bold;
                height: 25px;
            }

            .FixedGrid .FixedGridRightbody {
                width: 100px;
                text-align: center;
                border-top: 1px solid #fff;
                border-bottom: 1px solid #fff;
                border-right: 1px solid #fff;
                font-weight: bold;
                height: 25px;
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
            .FixedGrid tr {
                border-right: 1px solid #000;
            }

            .FixedGrid td {
                text-align: left;
                word-break: break-all;
                word-wrap: break-word;
                border: 1px solid #000;
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

                    <li>
                        <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="btnBack_Click" Width="80px" />
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        <input id="hidEmailAddress" type="hidden" value="" runat="server" />
                    </li>
                </ul>
                <div id="tabs-1">

                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">

                            <td class="FixedGridWidthLeft"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidth"></td>
                            <td class="FixedGridWidthright"></td>
                        </tr>
                        <tr id="test">
                            <td class="FixedGridLeft">
                                 <asp:HiddenField ID="hidsuppno" runat="server" />
                                <asp:HiddenField ID="hidFIid" runat="server" />
                            </td>

                            <td colspan="8" style="text-align: center;">
                                <p style="font-size: 15px; font-weight: 600;">上海强凌电子有限公公司</p>
                                <p style="font-size: 15px; font-weight: 600">TCP-CHINA TECH. CO.,LTD.</p>
                                <p style="font-size: 15px; font-weight: 600">新增供应商初次验厂评价表</p>

                            </td>
                            <td class="FixedGridRight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">NO.
                            </td>
                            <td colspan="7" style="text-align: left">
                                <asp:Label ID="lblNo" runat="server" Visible="true"></asp:Label>
                                <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>

                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">供应商名称
                            </td>
                            <td colspan="4" style="text-align: left">
                                <asp:Label ID="lblname" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: right;">验厂时间
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lbldate" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">提供产品服务范围
                            </td>
                            <td colspan="7" style="text-align: left">
                                <asp:Label ID="lblserver" runat="server" Visible="true"></asp:Label>
                            </td>

                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">公司地址
                            </td>
                            <td colspan="7" style="text-align: left">
                                <asp:Label ID="lblAddress" runat="server" Visible="true"></asp:Label>
                            </td>

                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">营业执照号码
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblLicenseNO" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: right;">法人代表
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblLegalPerson" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">业务负责人
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblBusiness" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: right;">品质负责人
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblQuality" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">联系电话
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblTelephone" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: right;">传真
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblFax" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="1" style="text-align: right;">员工人数
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblnumber" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: right;">技术人员
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblTechnology" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="8" style="text-align: center;">
                                <p style="font-size: 15px; font-weight: 600;">评估报告</p>
                            </td>

                            <td class="FixedGridRightbody"></td>
                        </tr>

                        <% Response.Write(EffectTR); %>

                        <tr>
                            <td class="FixedGridLeft"></td>

                            <td colspan="4" style="text-align: left;">评价结果：
                                <br />
                                <asp:RadioButtonList ID="rbtncheck" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">合格</asp:ListItem>
                                    <asp:ListItem Value="0">不合格</asp:ListItem>
                                    <asp:ListItem Value="2">有条件合格</asp:ListItem>
                                </asp:RadioButtonList>
                                有条件合格是否按要求完善：
                                <asp:RadioButtonList ID="rbtncheckper" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td colspan="4" style="text-align: left;">总结论：
                              <asp:RadioButtonList ID="rbtnfinuse" runat="server" RepeatDirection="Horizontal">
                                  <asp:ListItem Value="1">合格</asp:ListItem>
                                  <asp:ListItem Value="0">不合格</asp:ListItem>
                              </asp:RadioButtonList>
                                核准：
                             <asp:Label ID="lblcheck" runat="server" Visible="true"></asp:Label>
                                <br />
                                <asp:Button ID="btnsave" runat="server" Text="save" CssClass="SmallButton3" OnClick="btnsave_Click" />
                            </td>
                            <td class="FixedGridRight"></td>
                        </tr>
                        <tr>
                            <td class="FixedGridLeftbody"></td>
                            <td colspan="8" style="text-align: left;">
                                <asp:HyperLink ID="hlinkExcute" runat="server" Font-Bold="False" Font-Size="12px" Font-Underline="True" Height="18px" Target="_blank">附件:</asp:HyperLink>
                                <input id="fileExcute" style="height: 23px" type="file" size="45" name="filename4"
                                    runat="server" />
                                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="SmallButton3" OnClick="btnupdate_Click" />
                            </td>
                            <td class="FixedGridRightbody"></td>
                        </tr>
                    </table>


                </div>
                <div id="tabs-2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="FI_id,FI_path,fileid" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="没有上传文件" Width="60px" HorizontalAlign="center"></asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>

                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="文件">
                                <ItemTemplate>

                                    <asp:LinkButton ID="reply" runat="server" Font-Bold="False" Font-Size="12px" CommandName="show"
                                        Font-Underline="True" Text='<%# Bind("FI_Filename") %>' Style="padding-left: 5px;"></asp:LinkButton>

                                    </td>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <asp:LinkButton ID="delete" runat="server" Font-Bold="False" Font-Size="12px" CommandName="delete"
                                        Font-Underline="True" Text="delete" Style="padding-left: 5px;"></asp:LinkButton>

                                    </td>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FI_createname" HeaderText="创建人">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FI_createdate" HeaderText="上传时间">
                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                <ItemStyle Width="120px" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </div>

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
