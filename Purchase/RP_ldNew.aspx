<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_ldNew.aspx.cs" Inherits="Purchase_RP_ldNew" %>

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
                <td style="text-align:center; font-size:20px; background-color:#7da7f2;" colspan="4">领料申请单</td>
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
            </tr>
            <tr>
                <td class="style1" align="right">
                    申请域：   
                </td>
                <td class="style1">
                   
                          <asp:Label id ="lbPlandCode"  runat="server"></asp:Label>
                   
                </td>
                <td align="right">
                    业务部门：   
                </td>
                <td class="style3">
                     <asp:DropDownList ID="ddldepartment" runat="server" DataTextField="departmentname" DataValueField ="departmentid">
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="style1" align="right">
                申请人：   
                </td>
                <td class="style1">
                   
                     <asp:TextBox ID="txtcreate" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                   
                </td>
                <td align="right">
                    申请人部门：
                </td>
                <td class="style3">
                       <asp:TextBox ID="txtcreatedep" runat="server" Width="149px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" align="right">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td colspan="1" class="auto-style2">
                    <asp:TextBox ID="txtRmks" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td  align="right">
                    成本中心：
                </td>
                <td colspan="1" class="auto-style2">
                    <asp:DropDownList ID="ddlCost" runat="server" DataTextField="cc_desc" DataValueField ="cc_ctr">
                    </asp:DropDownList>
                </td>
            </tr>

           
              <tr  id="trUpload5" runat="server">
                <td style="text-align:center; background-color:#7da7f2;" colspan="4">领料申请</td>
            </tr>
            <tr id="trUpload" runat="server">
                <td align="right" class="style1">
                    批量导入：
                </td>
                <td colspan="2" valign="top" width="500" class="auto-style1">
                    <input id="filename1" style="width: 492px; height: 22px" type="file" size="45" name="filename1"
                        runat="server">
                </td>
                 <td align="center">
                    <asp:Button ID="btn_Upload" runat="server" Text="导入申请明细" Width="117px" CssClass="SmallButton2"
                        OnClick="btn_Upload_Click" />
                </td>

            </tr>
             <tr id="trUpload1" runat="server">
                <td align="right" class="style1">
                    <font size="3">下载模板：</font>
                      <label id="here" onclick="submit();">
                        <a href="/docs/领料申请导入.xlsx" target="blank"><font color="blue">领料申请导入</font></a></label>
                </td>
                <td align="left" colspan="3" width="135">
                  
                </td>
               
            </tr>
             <tr id="trUpload3" runat="server">
                 <td class="title">行号：</td>
                <td>
                     <asp:TextBox ID="txtline" runat="server"></asp:TextBox>
                </td>
                <td class="title">QAD：</td>
                <td >
                     <asp:TextBox ID="txtqad" CssClass="CCPPart" runat="server"></asp:TextBox>   <asp:LinkButton ID="btnld" runat="server" OnClick="btnld_Click">查看库存</asp:LinkButton>
                </td>
                </tr>

            <tr  id="trUpload2" runat="server">
                <td class="title">数量：</td>
                <td>
                     <asp:TextBox ID="txtnum" runat="server"></asp:TextBox>
                </td>
                <td class="title">地点：</td>
                <td >
                      <asp:TextBox ID="txtsite" runat="server"></asp:TextBox>
                </td>
                
            </tr>
             
            <tr  id="tr1" runat="server">
                <td class="title">账户：</td>
                <td>
                    <asp:DropDownList ID="ddlZH" runat="server" Enabled="false">
                        <asp:ListItem Selected="True">--</asp:ListItem>
                        <asp:ListItem>制造费用-低耗品</asp:ListItem>
                        <asp:ListItem>制造费用-劳动保护费</asp:ListItem>
                        <asp:ListItem>制造费用-办公费</asp:ListItem>
                        <asp:ListItem>管理费用-低耗品/劳保</asp:ListItem>
                        <asp:ListItem>管理费用-办公费</asp:ListItem>
                        <asp:ListItem>管理费用-办公费(总部)</asp:ListItem>
                        <asp:ListItem>销售费用-低耗品/劳保</asp:ListItem>
                        <asp:ListItem>销售费用-办公费</asp:ListItem>
                        <asp:ListItem>研发费用-低耗品/劳保</asp:ListItem>
                        <asp:ListItem>研发费用-办公费</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="title"></td>
                <td >
                      
                </td>
                
            </tr>
               <tr  id="trUpload4" runat="server">
                <td colspan="4" style="text-align:center;">
                     <asp:Button ID="btnsaveline" runat="server" Text="保存行" CssClass="SmallButton2" OnClick="btnsaveline_Click"/>
                </td>
            </tr>
              <tr>
                <td style="text-align:center; background-color:#7da7f2;" colspan="4">领料单明细</td>
            </tr>
                <tr>
                <td style="text-align:center;" colspan="4">

                        <asp:GridView ID="gv_product" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_product_PageIndexChanging"
                        PageSize="8" Width="800px" EnableModelValidation="True" DataKeyNames="id,QAD,site" OnRowDataBound="gv_product_RowDataBound" OnRowDeleting="gv_product_RowDeleting" OnRowCommand="gv_product_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="line" HeaderText="行号" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QAD" HeaderText="QAD" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                           
                           
                            <asp:BoundField DataField="description" HeaderText="描述" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num" HeaderText="数量" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                              <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkdetail" Text="查看库存" ForeColor="Black" Font-Size="12px" Font-Underline="true" runat="server"
                                    CommandArgument='<%# Eval("QAD") + "," + Eval("site")%>' CommandName="det" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
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
                    <asp:Button ID="btn_Save" runat="server" Text="保存" Width="53px" OnClick="btn_Save_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Submit" runat="server" Text="提交" Width="53px" OnClick="btn_Submit_Click"
                        CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="SmallButton2" 
                        OnClick="btn_Cancel_Click" OnClientClick="return confirm('确定取消该申请单');" 
                        Text="取消" Width="56px" />&nbsp; &nbsp;
                    <asp:Button ID="btnget" runat="server" Text="打印领料单" Width="56px" CssClass="SmallButton2" Visible="false" OnClick="btnget_Click"
                         />&nbsp; &nbsp;
                    <asp:Button ID="btn_Back" runat="server" Text="返回" Width="56px" CssClass="SmallButton2"
                        OnClick="btn_Back_Click" />
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