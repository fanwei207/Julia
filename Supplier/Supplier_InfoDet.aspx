<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier_InfoDet.aspx.cs" Inherits="Supplier_Supplier_InfoDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <div align="center">
            <div id="divTabs">
                <ul>
                    <li><a href="#tabs-1">&nbsp;&nbsp;新供应商信息单&nbsp;&nbsp;</a></li>
                    <li id="liFileQualification"><a href="#tabs-2">&nbsp;&nbsp;供应商资质文件评估&nbsp;&nbsp;</a></li>
                    <li id="liSignFile"><a href="#tabs-3">&nbsp;&nbsp;签署文件&nbsp;&nbsp;</a></li>
                    <li id="liFormalFile"><a href="#tabs-4">&nbsp;&nbsp;正式合同文件&nbsp;&nbsp;</a></li>
                    <li>
                        <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" Width="80px" OnClick="btnReturn_Click" />
                        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                    </li>

                </ul>

                <div id="tabs-1">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth">
                                <asp:HiddenField ID="hidSupplierNo" runat="server" />
                                <asp:HiddenField ID="hidSupplierID" runat="server" />
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
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight" style="height: 80px;"></td>
                            <td colspan="3" class="FixedGridLeft" style="border-right: none;">
                                <img src="../images/LOGO.png" /></td>
                            <td colspan="13" class="FixedGridLeft" style="border-right: none;"><span style="font-size: 25px;">供应商信息维护</span><span style="font-size: 25px;">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbsupplierNo" runat="server"></asp:Label></span></td>
                            <td colspan="3" class="FixedGridRight" style="border-left: none;">
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" Width="80px" OnClick="btnSave_Click" /><br />
                                <br />

                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">申请单位</td>
                            <td colspan="7" style="text-align: center;">
                                <asp:Label ID="labPlant" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">申请日期</td>
                            <td colspan="6" style="text-align: center;">
                                <asp:Label ID="labDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">申请人</td>
                            <td colspan="7" style="text-align: center;">
                                <asp:Label ID="labAPPUserName" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">申请部门</td>
                            <td colspan="6" style="text-align: center;">
                                <asp:Label ID="labAPPDeptName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">供应商名称</td>
                            <td colspan="2" class="FixedGridLeft">中文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:TextBox ID="TxTChineseSupplierName" runat="server" Text="Label" CssClass="SmallButton2" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:TextBox ID="TxTEnglishSupplierName" runat="server" Text="Label" CssClass="SmallButton2" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">供应商地址</td>
                            <td colspan="2" class="FixedGridLeft">中文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:TextBox ID="txtChineseSupplierAddress" runat="server" Text="Label" CssClass="SmallButton2" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:TextBox ID="txtEnglishSupplierAddress" runat="server" Text="Label" CssClass="SmallButton2" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">联系人 </td>
                            <td style="text-align: center;" colspan="16">
                                <div style="text-align: left;">
                                    <asp:TextBox ID="txtLinkNum" runat="server" CssClass="smalltextbox" Width="80px"></asp:TextBox>
                                    <asp:TextBox ID="txtLinkName" runat="server" CssClass="smalltextbox"
                                        Width="110px"></asp:TextBox>
                                    <asp:TextBox ID="txtRole" runat="server" CssClass="smalltextbox"
                                        Width="110px"></asp:TextBox>
                                    <asp:TextBox ID="txtMobilePhone" runat="server"
                                        CssClass="smalltextbox" Width="110px"></asp:TextBox>
                                    <asp:TextBox ID="txtPhone"
                                        runat="server" CssClass="smalltextbox" Width="110px"></asp:TextBox>
                                    <asp:TextBox ID="txtEmail"
                                        runat="server" CssClass="smalltextbox" Width="110px"></asp:TextBox>
                                    <asp:HiddenField id="hidOldNo" runat="server"/>
                                    <asp:Button ID="btnAddLinkMan" runat="server" CssClass="SmallButton2" Text="添加" Width="80px" OnClick="btnAddLinkMan_Click" />
                                    <asp:Button ID="btnModifyLinkMan" runat="server" CssClass="SmallButton2" Text="编辑" Width="80px" Visible="false" OnClick="btnModifyLinkMan_Click" />
                                </div>
                                <asp:GridView ID="gvBasisInfo" runat="server" CssClass="GridViewStyle"
                                    AutoGenerateColumns="false" Width="90%" OnRowCommand="gvBasisInfo_RowCommand">
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="367px"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="类型" Width="67px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                                <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                            </asp:TableFooterRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField HeaderText="排序" DataField="linkNo">
                                            <HeaderStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="联系人" DataField="linkName">
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="职务" DataField="linkRole">
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="联系电话" DataField="linkMobilephone">
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="座机" DataField="linkPhone">
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Email" DataField="linkEmail">
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="操作">
                                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkbtnEdit" runat="server" CommandName="lkbtnEdit" CommandArgument='<%# Eval("linkNo") %>'
                                                    Text="编辑"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("linkNo") %>'
                                         Text="删除"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">经营类型</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:Label ID="labBusinesstypeID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtBusinesstype" runat="server" Text="" CssClass="SmallTextBox5" Width="90%"></asp:TextBox>
                            </td>
                            <%--<td colspan="3" class="FixedGridLeft">公司传真</td>
                            <td colspan="6" style="text-align:center;">
                                <asp:TextBox ID="txtSupplierfax" runat="server" Text="" CssClass="SmallTextBox5" Width="90%"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">供应物料类型</td>
                            <td colspan="16" style="text-align: center; font-size: 15px;">
                                <asp:Label ID="Label1" runat="server" Text="大类区分："></asp:Label>
                                <asp:Label ID="labBroadHeadingID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtBroadHeading" runat="server" Text="" CssClass="SmallTextBox5" Width="30%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="细部区分："></asp:Label>
                                <asp:Label ID="labSubDivisionID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtSubDivision" runat="server" Text="" CssClass="SmallTextBox5" Width="30%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--    <asp:Label ID="Label3" runat="server" Text="子物料："></asp:Label>
                                <asp:Label ID="labSubMaterialID" runat="server" Text="Label" Visible="false"></asp:Label>--%>
                                <%--    <asp:Label ID="labSubMaterial" runat="server" Text="Label"></asp:Label>--%>
                                <asp:Label ID="Label5" runat="server" Text=" 等级："></asp:Label>
                                <asp:Label ID="labFactoryInspectionLevelID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labFactoryInspection" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <%--  <tr class="NoTrHover">                            
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">交期</td>
                            <td colspan="7" style="text-align:center;">
                                    <asp:TextBox ID="txtDeliveryTime" runat="server" Width="80%"></asp:TextBox>
                            </td>
                             <td colspan="3" class="FixedGridLeft">账期</td>
                            <td colspan="6" style="text-align:center;">
                                     <asp:Label ID="lbTerms" runat="server" Text="Label"></asp:Label>
                            </td>
                           
                        </tr>--%>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">账期</td>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label ID="lbTerms" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">币种</td>
                            <td colspan="3" style="text-align:center">
                                <asp:Label ID="lbCurr" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td colspan="3" class="FixedGridLeft">税率</td>
                            <td colspan="3" style="text-align:center">
                                <asp:Label ID="lbTax" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="FixedGridHeight"></td>
                        </tr>               
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">备注</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:TextBox runat="server" ID="txtRemark" Width="90%" CssClass="SmallTextBox"
                                    TextMode="MultiLine" Height="100px"></asp:TextBox>
                            </td>

                        </tr>
                    </table>
                </div>
                <div id="tabs-2" style="text-align: center; margin-top: 40px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr>
                            <td colspan="8" class="FixedGridLeft" style="font-size: 30px; border: none;">供应商资质文件评估</td>
                        </tr>
                        <tr style="height: 20px;">
                            <td colspan="8"></td>
                        </tr>
                        <tr style="text-align: center; color: red;">
                            <td colspan="8">全国企业信用信息公示系统 信息核准 <u id="links">www.sgs.gov.cn</u></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td colspan="8"></td>
                        </tr>
                        <tr style="border: none;" id="trFileQualificationSecurity">
                            <td style="text-align: right;">
                                <asp:Label ID="Label20" runat="server" Text="文件类型："></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlFileType" runat="server" CssClass="txtDDLSupplier" Width="200px"
                                    DataTextField="supplier_FileType" DataValueField="supplier_FileID">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFileTypeName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="Label21" runat="server" Text="必要性："></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlNecessity" runat="server">
                                    <asp:ListItem Value="0" Text="是">是</asp:ListItem>
                                    <asp:ListItem Value="1" Text="否">否</asp:ListItem>
                                    <asp:ListItem Value="2" Text="一般">一般</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right;">有效期：
                            </td>
                            <td style="text-align: left;">
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
                            <td colspan="8" style="height: 30px;"></td>
                        </tr>
                        <tr class="NoTrHover">
                            <td colspan="8" align="center" id="tdFQ">
                                <asp:GridView ID="FAgv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
                                    DataKeyNames="supplier_FileID,supplier_FileNecessityID,supplier_FilePath,supplier_FileName,supplier_FileIsEffect,canDelete,supplier_AssetFileID"
                                    OnRowDataBound="FAgv_RowDataBound" OnRowCommand="FAgv_RowCommand">
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="文件类型" DataField="supplier_FileType">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" Height="25px" HorizontalAlign="Center" />
                                        </asp:BoundField>
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
                                        <asp:ButtonField Text="Delete" HeaderText="Delete" CommandName="DeleteFA">
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
                <div id="tabs-3" style="text-align: center;">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
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
                        <tr class="NoTrHover">
                            <td colspan="19" class="FixedGridLeft"><span style="font-size: 25px;">签署文件</span></td>
                        </tr>
                        <tr class="NoTrHover" id="trSuppDeptUpload" style="text-align: center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="19" class="FixedGridLeft">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button10" runat="server" Text="上传" CssClass="SmallButton2" OnClick="Button10_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="19" style="text-align: center;">
                                <asp:GridView ID="gvFile" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowCommand="gvFile_RowCommand"
                                    DataKeyNames="SignFile_FileStatus,SignFile_FileName,SignFile_FilePath,SignFile_FileID,canDelete" OnRowDataBound="gvFile_RowDataBound">
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
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
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
                                        <asp:ButtonField Text="Delete" HeaderText="Delete" CommandName="DeleteFA">
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
                <div id="tabs-4">
                    <table class="FixedGrid" border="0" cellpadding="0" cellspacing="0" style="width: 1000px; margin-top: -20px;">
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight FixedGridLeftCorner"></td>
                            <td class="FixedGridLeft" style="border-right: 1px solid #fff;"></td>
                            <td class="FixedGridWidth">
                                <asp:HiddenField ID="HiddenField2" runat="server" />
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
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" class="FixedGridLeft"><span style="font-size: 25px;">正式合同文件</span></td>
                        </tr>
                        <tr class="NoTrHover" style="text-align: center;">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" class="FixedGridLeft">
                                <asp:FileUpload ID="FileUpload3" runat="server" Width="300px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button3" runat="server" Text="上传" CssClass="SmallButton2" OnClick="Button3_Click" />
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="18" style="text-align: center;">
                                <asp:GridView ID="gvFormal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowCommand="gvFormal_RowCommand"
                                    DataKeyNames="FormalFile_FileName,FormalFile_FilePath,canDelete,Formal_FileID" OnRowDataBound="gvFormal_RowDataBound">
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
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                            <ControlStyle Font-Bold="False" Font-Underline="True" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>
                                        <asp:ButtonField Text="Delete" HeaderText="Delete" CommandName="DeleteGF">
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
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
