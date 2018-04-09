<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier_InfoView.aspx.cs" Inherits="Supplier_InfoView" %>

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
                                <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" Width="80px" OnClick="btnReturn_Click" />
                                <input id="hidTabIndex" type="hidden" value="0" runat="server" />
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
                                <asp:Label ID="labChineseSupplierName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:Label ID="labEnglishSupplierName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" rowspan="2" class="FixedGridLeft">供应商地址</td>
                            <td colspan="2" class="FixedGridLeft">中文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:Label ID="labChineseSupplierAddress" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="2" class="FixedGridLeft">英文</td>
                            <td colspan="14" style="text-align: center;">
                                <asp:Label ID="labEnglishSupplierAddress" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">联系人 </td>
                            <td style="text-align: center;" colspan="16">                     
                                <asp:GridView ID="gvBasisInfo" runat="server" CssClass="GridViewStyle"
                                    AutoGenerateColumns="false" Width="100%" OnRowCommand="gvBasisInfo_RowCommand">
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

                          

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">经营类型</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:Label ID="labBusinesstypeID" runat="server" Text="Label" Visible="false"></asp:Label>
                               
                                <asp:Label ID="labBusinesstype" runat="server" Text="Label"></asp:Label>
                            
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
                                <asp:Label ID="labBroadHeading" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="细部区分："></asp:Label>
                                <asp:Label ID="labSubDivisionID" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="labSubDivision" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <asp:Label runat="server" ID="txtRemark" Width="90%" CssClass="SmallTextBox"
                                    TextMode="MultiLine" Height="100px"></asp:Label>
                            </td>

                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">供应商资质文件评估</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:GridView ID="FAgv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" Width="100%"
                                    DataKeyNames="supplier_FileID,supplier_FileNecessityID,supplier_FilePath,supplier_FileName,supplier_FileIsEffect,canDelete,supplier_AssetFileID"
                                    OnRowDataBound="FAgv_RowDataBound" OnRowCommand="FAgv_RowCommand" >
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
                                    </Columns>
                                </asp:GridView>
                                
                            </td>

                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">签署文件</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:GridView ID="gvFile" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                     CssClass="GridViewStyle" OnRowCommand="gvFile_RowCommand"
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
                                        <asp:ButtonField Text="作废" HeaderText="作废" CommandName="NotEff" >
                                            <ControlStyle Font-Bold="False" Font-Underline="false" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:ButtonField>                                        
                                    </Columns>
                                </asp:GridView>
                                <td colspan="8" align="center" id="tdFQ">
                                    &nbsp;</td>
                            </td>

                        </tr>
                        <tr class="NoTrHover">
                            <td class="FixedGridHeight"></td>
                            <td colspan="3" class="FixedGridLeft">正式合同文件</td>
                            <td colspan="16" style="text-align: center;">
                                <asp:GridView ID="gvFormal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Width="100%" CssClass="GridViewStyle" PageSize="20" OnRowCommand="gvFormal_RowCommand"
                                    DataKeyNames="FormalFile_FileName,FormalFile_FilePath,canDelete,Formal_FileID" >
                                    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                                    <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table2" Width="90%" CellPadding="-1" CellSpacing="0" runat="server"
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

                                    </Columns>
                                </asp:GridView>
                                <td colspan="8" align="center" id="tdFQ">
                                    &nbsp;</td>
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
