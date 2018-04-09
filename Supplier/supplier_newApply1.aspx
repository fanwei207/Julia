<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_newApply1.aspx.cs" Inherits="Supplier_supplier_newApply1" %>

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
    <script language="JavaScript" src="../script/test.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function(){
            
            $("#Button1").click(function(){
                var ChineseSupplierName = $("#txtChineseSupplierName").val();
                var EnglishSupplierName = $("#txtEnglishSupplierName").val();
                var ChineseSupplierAddress = $("#txtChineseSupplierAddress").val();
                var EnglishSupplierAddress = $("#txtEnglishSupplierAddress").val();                
                var SupplierUserName = $("#txtSupplierUserName").val();
                var SupplierRoleName = $("#txtSupplierRoleName").val();
                var SupplierNumber = $("#txtSupplierNumber").val();
                var SupplierPhone = $("#txtSupplierPhone").val();                
                var SupplierFax = $("#txtSupplierFax").val();
                var SupplierEmail = $("#txtSupplierEmail").val();
                var SupplierWeb = $("#txtSupplierWeb").val();
                var SuppNewReason = $("#txtSuppNewReason").val();
                
                var flg = true;
                if(ChineseSupplierName == '')
                {
                    $("#txtChineseSupplierName").focus();
                    alert('供应商中文名为空！');
                    flg = false;
                    return false;
                }
                //else if(EnglishSupplierName == '')
                //{
                //    $("#txtEnglishSupplierName").focus();
                //    alert('供应商英文名为空！');
                //    flg = false;
                //    return false;
                //}
                else if(ChineseSupplierAddress == '')
                {
                    $("#txtChineseSupplierAddress").focus();
                    alert('供应商中文地址为空！');
                    flg = false;
                    return false;
                }
                //else if(EnglishSupplierAddress == '')
                //{
                //    $("#txtEnglishSupplierAddress").focus();
                //    alert('供应商英文地址为空！');
                //    flg = false;
                //    return false;
                //}
                else if(SupplierUserName == '')
                {
                    $("#txtSupplierUserName").focus();
                    alert('供应商联系人为空！');
                    flg = false;
                    return false;
                }
                //else if(SupplierRoleName == '')
                //{
                //    $("#txtSupplierRoleName").focus();
                //    alert('供应商联系人职务为空！');
                //    flg = false;
                //    return false;
                //}
                else if(SupplierNumber == '')
                {
                    $("#txtSupplierNumber").focus();
                    alert('供应商联系电话为空！');
                    flg = false;
                    return false;
                }
                else if(SupplierPhone == '')
                {
                    $("#txtSupplierPhone").focus();
                    alert('供应商固定电话为空！');
                    flg = false;
                    return false;
                }
                //else if(SupplierFax == '')
                //{
                //    $("#txtSupplierFax").focus();
                //    alert('供应商传真为空！');
                //    flg = false;
                //    return false;
                //}
                else if(SupplierEmail == '')
                {
                    $("#txtSupplierEmail").focus();
                    alert('供应商邮箱为空！');
                    flg = false;
                    return false;
                }
                //else if(SupplierWeb == '')
                //{
                //    $("#txtSupplierWeb").focus();
                //    alert('公司网页为空！');
                //    flg = false;
                //    return false;
                //}
                else if(SuppNewReason == '')
                {
                    $("#txtSuppNewReason").focus();
                    alert('新增供应商原因为空！');
                    flg = false;
                    return false;
                }
                if(!flg)
                {
                    return false;
                }
            });
        });
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
            width:99%;
            height:30px;
        }
        .txtShortSupplier {
            width:98%;
            height:30px;
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
                <td colspan="16" class="FixedGridLeft" style="border-right:none;"><span style="font-size:25px;">新增供应商申请单</span></td>
                <td colspan="3" class="FixedGridLeft" style="border-left:none;"></td>
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
                    <asp:TextBox ID="txtChineseSupplierName" runat="server" CssClass="txtLongSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="2" class="FixedGridLeft">英文</td>
                <td colspan="14" style="text-align:center;">
                    <asp:TextBox ID="txtEnglishSupplierName" runat="server" CssClass="txtLongSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="3" rowspan="2" class="FixedGridLeft">供应商地址</td>
                <td colspan="2" class="FixedGridLeft">中文</td>
                <td colspan="14" style="text-align:center;">
                    <asp:TextBox ID="txtChineseSupplierAddress" runat="server" CssClass="txtLongSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="2" class="FixedGridLeft">英文</td>
                <td colspan="14" style="text-align:center;">
                    <asp:TextBox ID="txtEnglishSupplierAddress" runat="server" CssClass="txtLongSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="3" class="FixedGridLeft">联系人</td>
                <td colspan="2" class="FixedGridLeft">联系人</td>
                <td colspan="5" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierUserName" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
                <td colspan="3"class="FixedGridRight">职务</td>
                <td colspan="6" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierRoleName" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="3" rowspan="2" class="FixedGridLeft">联系方式</td>
                <td colspan="2" class="FixedGridLeft">联系电话</td>
                <td colspan="5" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierNumber" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
                <td colspan="3" class="FixedGridRight">固定电话</td>
                <td colspan="6" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierPhone" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">
                <td class="FixedGridHeight"></td>
                <td colspan="2" class="FixedGridLeft">传真</td>
                <td colspan="5" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierFax" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
                <td colspan="3" class="FixedGridRight">邮箱</td>
                <td colspan="6" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierEmail" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">                            
                <td class="FixedGridHeight"></td>
                <td colspan="3" class="FixedGridLeft">经营类型</td>
                <td colspan="7" style="text-align:center;">
                    <asp:DropDownList ID="ddlBusinesstype" runat="server" DataTextField="SupplieType" DataValueField="SupplieTypeID" 
                        AutoPostBack="True" OnSelectedIndexChanged="ddlBusinesstype_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="3" class="FixedGridLeft">公司网页</td>
                <td colspan="6" style="text-align:center;">
                    <asp:TextBox ID="txtSupplierWeb" runat="server" CssClass="txtShortSupplier"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">                            
                <td class="FixedGridHeight"></td>
                <td colspan="3" class="FixedGridLeft">供应物料类型</td>
                <td colspan="16" style="text-align:center;">
                    <asp:Label ID="Label1" runat="server" Text="大类区分"></asp:Label>
                    <asp:DropDownList ID="ddlBroadHeading" runat="server" CssClass="txtDDLSupplier"
                            DataTextField="BroadHeading" DataValueField="BroadHeadingID" AutoPostBack="True" OnSelectedIndexChanged="ddlBroadHeading_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="细部区分"></asp:Label>
                    <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="txtDDLSupplier"
                            DataTextField="SubDivision" DataValueField="SubDivisionID" AutoPostBack="True" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="子物料"></asp:Label>
                    <asp:DropDownList ID="ddlSubMaterial" runat="server" CssClass="txtDDLSupplier"
                            DataTextField="SubMaterial" DataValueField="SubMaterialID" AutoPostBack="True" OnSelectedIndexChanged="ddlSubMaterial_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label4" runat="server" Text="等级"></asp:Label>
                    <asp:DropDownList ID="ddlFactoryInspectionLevel" runat="server" CssClass="txtDDLSupplier"
                            DataTextField="FactoryInspection" DataValueField="FactoryInspectionLevelID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="NoTrHover">                            
                <td class="FixedGridHeight"></td>
                <td colspan="3" class="FixedGridLeft">新增供应商原因</td>
                <td colspan="16" style="text-align:center;">
                    <asp:TextBox ID="txtSuppNewReason" runat="server" Width="99%" Height="30px"></asp:TextBox>
                </td>
            </tr>
            <tr class="NoTrHover">                            
                <td class="FixedGridHeight"></td>
                <td colspan="19" style="text-align: center;">
                    <asp:Button ID="Button1" runat="server" Text="提交" CssClass="SmallButton2" OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="返回" CssClass="SmallButton2" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidSupplierID" runat="server" />
        <asp:HiddenField ID="hidSupplierStatus" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
