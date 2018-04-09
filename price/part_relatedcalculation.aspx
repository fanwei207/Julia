<%@ Page Language="C#" AutoEventWireup="true" CodeFile="part_relatedcalculation.aspx.cs"
    Inherits="part_relatedcalculation" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .TextBox_E
        {
            font-weight: normal;
            font-size: 9pt;
            font-family: Tahoma, Arial;
            text-align: left;
            text-decoration: none;
            border: 1px solid rgb(126, 157, 185);
            background-color: rgb(245,245,245);
        }
        
        .TextBox_A
        {
            border: 1px solid rgb(126, 157, 185);
            font: 11px;
            padding: 1px;
            background: #ffffff;
            -moz-background-clip: -moz-initial;
            -moz-background-origin: -moz-initial;
            -moz-background-inline-policy: -moz-initial;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 637px; height: 55px;" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr style="height: auto">
                <td style="height: 15px; width: 91px; text-align: left;" valign="baseline">
                    物料号
                </td>
                <td style="height: 15px; width: 138px;" valign="top">
                    <asp:TextBox ID="txtpt_part" runat="server" OnTextChanged="txtpt_part_TextChanged"
                        AutoPostBack="true" ToolTip="请输入要计算的物料号，如11051500000041" BackColor="White" CssClass="TextBox_A"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 89px;">
                </td>
                <td style="height: 15px; width: 66px;" valign="baseline">
                    物料描述
                </td>
                <td style="width: 275px; height: 15px;">
                    <asp:TextBox ID="txtpt_desc1" runat="server" CssClass="TextBox_E" Width="152px" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtpt_desc2" runat="server" CssClass="TextBox_E" Width="152px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 91px;">
                    请输入数量
                </td>
                <td style="height: 22px; width: 138px;">
                    <asp:TextBox ID="txtnumber" runat="server" BackColor="White" ToolTip="请输入需计算的数量"
                        AutoPostBack="false" CssClass="TextBox_A"></asp:TextBox>
                </td>
                <td style="height: 22px; width: 89px;">
                </td>
                <td style="width: 66px; height: 22px;">
                </td>
                <td style="height: 22px; width: 275px;">
                </td>
            </tr>
            <tr>
                <td style="height: 2px; width: 91px;" colspan="5">
                </td>
            </tr>
            <tr>
                <td style="height: 21px; width: 91px;" valign="baseline">
                    单套箱数
                </td>
                <td style="height: 21px; width: 138px;">
                    <asp:TextBox ID="txtunitpt_size" runat="server" ReadOnly="true" CssClass="TextBox_E"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 89px;">
                </td>
                <td style="height: 21px; width: 66px;" valign="baseline">
                    合计箱数
                </td>
                <td style="width: 275px; height: 21px;">
                    <asp:TextBox ID="txtsumpt_size" runat="server" ReadOnly="true" CssClass="TextBox_E"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 21px; width: 91px;" valign="baseline">
                    单套重量
                </td>
                <td style="height: 21px; width: 138px;">
                    <asp:TextBox ID="txtuintpt_ship_wt" runat="server" CssClass="TextBox_E" ReadOnly="true"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 89px;">
                </td>
                <td style="height: 21px; width: 66px;" valign="baseline">
                    合计重量
                </td>
                <td style="width: 275px; height: 21px;">
                    <asp:TextBox ID="txtsumpt_ship_wt" runat="server" CssClass="TextBox_E" ReadOnly="true"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 18px; width: 91px;" valign="baseline">
                    单套体积
                </td>
                <td style="height: 18px; width: 138px;">
                    <asp:TextBox ID="txtunitpt_net_wt" runat="server" ReadOnly="true" CssClass="TextBox_E"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 89px;">
                </td>
                <td style="height: 18px; width: 66px;" valign="baseline">
                    合计体积
                </td>
                <td style="width: 275px; height: 18px;">
                    <asp:TextBox ID="txtsumpt_net_wt" runat="server" ReadOnly="true" CssClass="TextBox_E"
                        ForeColor="DimGray" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td colspan="5" style="height: 29px">
                    <asp:Button ID="btnunit_cal" runat="server" Text="开始计算" Width="200px" OnClick="btnunit_cal_Click"
                        CssClass="smallbutton2" />
                </td>
            </tr>
            <tr style="text-align: justify">
                <td colspan="5" style="height: 21px">
                </td>
            </tr>
            <tr style="text-align: justify">
                <td style="height: 26px;" colspan="5">
                    若您需要一次得到多行物料的箱数、重量和体积，请按模板格式导入Excel：
                </td>
            </tr>
            <tr>
                <td style="height: 25px; width: 91px;">
                    文件名
                </td>
                <td style="height: 25px" colspan="4">
                    <input type="file" id="excelfilepath" name="filepath" runat="server" style="width: 458px" />
                </td>
            </tr>
            <tr style="text-align: center">
                <td style="height: 37px;" colspan="5">
                    <asp:Button ID="btnbatch_cal" runat="server" Text="导入Excel文件" Width="140px" OnClick="btnbatch_cal_Click"
                        CssClass="smallbutton2" />
                    <asp:Button ID="btn_exportexcel" runat="server" Width="140px" OnClick="btn_exportexcel_Click"
                        Text="导出结果" CssClass="smallbutton2" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px; width: 91px;" colspan="5">
                </td>
            </tr>
            <tr>
                <td style="width: 380px; height: 15px;" colspan="5" align="left">
                    Excel文件模板格式如下：
                    <table style="width: 327px; background-color: aliceblue; border-bottom: black 1px solid;
                        border-left: black 1px solid; border-top: black 1px solid; border-right: black 1px solid;"
                        id="TABLE3" border="1">
                        <tr>
                            <td style="width: 120px; height: 15px;">
                                物料号（QAD号）
                            </td>
                            <td style="width: 120px; height: 15px;">
                                输入的数量
                            </td>
                        </tr>
                        <tr>
                            <td>
                                11051500000040
                            </td>
                            <td>
                                23
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                                11051500000041
                            </td>
                            <td style="height: 15px">
                                10
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                                ......
                            </td>
                            <td style="height: 15px">
                                ......
                            </td>
                        </tr>
                    </table>
                    <span style="color: silver;">提示:制作Excel模板时首行请填写列名,因数据从第二行开始计算。</span>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript"> 
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
