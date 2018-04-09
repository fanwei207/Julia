<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Part_ItemUpdateNew.aspx.cs" Inherits="part_Part_ItemUpdateNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

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
        .style1
        {
            width: 60px;
        }
        .auto-style1 {
            width: 60px;
            height: 20px;
        }
        .auto-style2 {
            height: 20px;
        }
    </style>
     <style type="text/css">
        .title{
            background-color:#efefef;
        }
        .tdclass {
            width:130px;
            border-top:0px solid #ffffff;
            border-left:0px solid #ffffff;
            border-right:0px solid #ffffff;
        }
        /*.FixedGridTD {
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
            border:1px solid #fff;
        }

        table td {            
            width: 100px;
            height: 25px;
            border:1px solid #000;
            text-align: left;
            word-break: break-all;
            word-wrap: break-word;

        }
        .auto-style1 {
            width: 100px;
        }*/
        .FixedGridTD {
            width: 50px;
            height: -10px;
            border-top:none;
            border-right:none;
            border-left:none;
        }
        .NoTrHover {
            border-top:none;
            border-right:none;
            border-left:none;
        }
        .FixedGridTDLeftCorner {
         width:1px;
         border-left:1px solid #fff;
         border-bottom:1px solid #000;
        }
         .auto-style3 {
             height: 29px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
         <table style="margin-top:20px;"  width="90%" border="1" cellspacing="0" cellpadding="1" >
            <tr class="NoTrHover">
                <td style="width:200px;" class="FixedGridTD FixedGridTDLeftCorner"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
                <td style="width:200px;" class="FixedGridTD"></td>
                <td style="width:250px;" class="FixedGridTD"></td>
            </tr>
            <tr>
                <td style="text-align:center; font-size:20px; background-color:#7da7f2;" colspan="4">部件/产品目录修改</td>
            </tr>
            <tr>

                <td class="style1" align="right">
                    申&nbsp;&nbsp;请&nbsp;&nbsp;单：
                </td>
                <td class="style1">
                    <asp:TextBox ID="txt_nbr" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </td>
                <td class="style3">
                   <asp:TextBox ID="txt_status" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                   <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
                </td>
                 <tr>
                <td class="auto-style1" align="right">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td colspan="3" class="auto-style2">
                    <asp:TextBox ID="txtRmks" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
               
            </tr>
              <td align="left" colspan ="4">
                    部件号/产品号
                    <asp:TextBox ID="txtPartCode" runat="server" Width="153px" MaxLength="50" 
                        CssClass="SmallTextBox">请输入部件号/产品号</asp:TextBox>&nbsp;
                    QAD号<asp:TextBox ID="txtQad" runat="server" Width="95px" MaxLength="50" 
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;描述
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 
                    <asp:RadioButton ID="radNormal" runat="server" Text="使用" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" Text="试用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" Text="停用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                         <asp:Button ID="BtnQuery" runat="server" CssClass="SmallButton3" Text="导出" 
                        Width="48px" OnClick="BtnQuery_Click"></asp:Button>
                </td>
             <tr>
                 <td colspan ="4">
                     导入：
                     <input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                       &nbsp;&nbsp;<asp:Button ID="btnRouting" runat="server" 
                                onclick="btnRouting_Click" Text="Import" CssClass="SmallButton2" />
                 </td>
             </tr>
           
          
           
             
      
              <tr>
                <td style="text-align:center; background-color:#7da7f2;" colspan="4">修改明细</td>
            </tr>
                <tr>
                <td style="text-align:center;" colspan="4">

                        <asp:GridView ID="gv_product" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_product_PageIndexChanging"
                        PageSize="8" Width="800px" EnableModelValidation="True" DataKeyNames="id" OnRowDataBound="gv_product_RowDataBound" OnRowDeleting="gv_product_RowDeleting" OnRowCommand="gv_product_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                          
                            <asp:BoundField DataField="QAD" HeaderText="QAD" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                           
                           
                            <asp:BoundField DataField="part" HeaderText="物料号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                              <asp:BoundField DataField="partNEW" HeaderText="物料号（新）" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="describe" HeaderText="描述" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="describeNEW" HeaderText="描述（新）" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            
                              <asp:BoundField DataField="desc1" HeaderText="QAD描述1" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="desc1NEW" HeaderText="QAD描述1（新）" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            
                               <asp:BoundField DataField="desc2" HeaderText="QAD描述2" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="desc2NEW" HeaderText="QAD描述2（新）" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>


                             <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:CommandField>


                        </Columns>
                    </asp:GridView>

                    </td>
</tr>
              <tr id="trReason" runat="server">
                <td class="title" align="right">
                    驳回意见：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" runat="server"  Width="598px" Height="50px" TextMode="MultiLine" Wrap="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trApply" runat="server">
                <td colspan="5" align="center">
                    
                  
                    <asp:Button ID="btn_Submit" runat="server" Text="提交" Width="53px" OnClick="btn_Submit_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Cancel_Click" OnClientClick="return confirm('确定取消该申请单');" 
                        Text="取消" Width="56px" />
                </td>
            </tr>
            <tr id="trReview" runat="server">
                <td colspan="5" align="center" class="auto-style3">
                    <asp:Button ID="btn_Confirm" runat="server" Text="通过" Width="53px" OnClick="btn_Confirm_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Reject" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Reject_Click" OnClientClick="return confirm('确定驳回该申请单');" 
                        Text="驳回" Width="56px" />&nbsp; &nbsp;
                    <asp:Button ID="btn_Back1" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="left">
                   
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                
                </td>
            </tr>
            <tr>
               <td align="left">
                   
                </td>
                <td align="right">

                    <asp:Button OnClick ="likbtn_Click" ID ="btnExport" Visible="false" runat="server"  CssClass ="SmallButton2" Text ="导出"/>

            

                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    &nbsp;</td>
            </tr>
        </table>
         <asp:HiddenField  ID="hid_CreatedBy" runat="server"/>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>