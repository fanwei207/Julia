<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_FinCheckApply.aspx.cs" Inherits="price_pcm_FinCheckApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.dev.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
        <script language="javascript" type="text/javascript">
            $(function () {
                $("#btnSetPCD").click(function () {
                    var date = $("#txtPlanDate").val();
                    $(".ApplyPCD").val(date);
                })
                $("#chkAll").click(function () {
                    $("#gvDet input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"))
                })
            })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" style="text-align: left">
            <tr>
                <td align="center" colspan ="3">
                    &nbsp;

                    <asp:CheckBox ID="chk_isApproved" runat="server" Text="已审批" Enabled="false" />
                    <asp:Label ID="lbl_ApproveNote" runat="server" Text=" "></asp:Label>    &nbsp;    &nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" 
                        onclick="btnBack_Click"/>
                </td>
            </tr>
        <tr>
            <td>
                 <input id="filename" name="filename" type="file" runat="server" 
                style=" width:400px; height: 24px;"  visible="false" />
            </td>
            <td>
                <asp:Button ID="btnUpload"  runat="server" Text="上传" CssClass="SmallButton2" 
                        onclick="btnUpload_Click" visible="false" />&nbsp;&nbsp;
               </td>
             <td>
                   <asp:Button ID="btnDownExcel"  runat="server" Text="下载批量模板" Width ="80 " CssClass="SmallButton2" 
                          OnClick="btnDownExcel_Click" visible="false"/>&nbsp;&nbsp;
            </td>
        </tr>
        </table>

        <table style="text-align: center; width: 1800px;">
                        <tr>
                <td>
                        <asp:GridView ID="gvDet" runat="server" Width="1800px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle"  DataKeyNames="isout,DetId,Part,PQDetId,Vender,Price,PriceSelf,PriceDiscount,CheckPrice,IMID" 
                        EmptyDataText="No data" onrowdatabound="gvDet_RowDataBound" 
                            onrowcommand="gvDet_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="QAD号" DataField="Part" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="部件号" DataField="ItemCode" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="供应商" DataField="Vender" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="供应商名称" DataField="VenderName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                        <asp:TemplateField HeaderText="历史价格">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnHist" runat="server" CommandName="lkbtnHist" CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                            Text="历史"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:BoundField HeaderText="指定供应商" DataField="InfoFrom" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单位" DataField="UM" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="币种" DataField="Curr" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="期望价格" DataField="applyPrice" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="核价" DataField="CheckPrice" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="批准核价">
                                <ItemTemplate>
                                    <asp:Textbox ID="txtCheckPrice"  AutoPostBack="true" runat="server"  Text='<%# Eval("FinCheckPrice")%>'  CssClass="Numeric" Width="60px" OnTextChanged="txtCheckPrice_TextChanged"></asp:Textbox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="生效日期">
                                <ItemTemplate>
                                  <asp:Textbox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate")%>' CssClass="Date" Width="80px"></asp:Textbox>
                                  
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="失效日期">
                                <ItemTemplate>
                                  <asp:Textbox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate")%>' CssClass="Date" Width="80px"></asp:Textbox>
                                  
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="报核价依据">
                                <ItemTemplate>
                                   <asp:LinkButton ID="lkbBasis" CommandArgument='<%# Eval("IMID")%>' CommandName="lkbBasis" Text="凭据" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center" Font-Underline="true" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                               <asp:BoundField HeaderText="详细描述" DataField="ItemDescription" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述1" DataField="ItemDesc1" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述2" DataField="ItemDesc2" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table style="text-align: center; width: 800px;">
            <tr>
            <td align="center">
            <table id="tbApply"  style="width: 800px;" cellpadding="1" cellspacing="1" runat="server">
            <tr align="right">
                <td align="right" class="style1">
                    <asp:Label ID="Label3" runat="server" Text="下一级审批人：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    &nbsp;<asp:TextBox 
                        ID="txtApproveName" runat="server" 
                        CssClass="SmallButton2" Width="78px" 
                        Height="21px"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txt_ApproveEmail" runat="server" CssClass="SmallButton2" Width="161px"
                        Height="21px"  ></asp:TextBox>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Button ID="btn_Approver" runat="server" Text="选择审批人" OnClick="btn_Approver_Click"
                        CssClass="SmallButton2" Width="93px" Height="21px" />
                    &nbsp;
                    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" />
                </td>
                <td>
                    <asp:Label ID="lblApplyId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblApplyer" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblApplyerEmail" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="Label5" runat="server" Text="申请原因：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApplyReason" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="approveopinion" visible="false">
                <td align="right" class="style1" >
                    <asp:Label ID="Label6" runat="server" Text="审批备注：" Width="100px" CssClass="LabelRight"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:TextBox ID="txtApprOpin" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="500" TextMode="MultiLine" Height="28px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAproveDate" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td align="right" class="style1">
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_ApplySubmit" runat="server" Text="提交" CssClass="SmallButton2" Width="70px" 
                        OnClick="btn_ApplySubmit_Click"  />
                    &nbsp; &nbsp;
                    <asp:Button ID="btn_approve" runat="server" Text="通过" CssClass="SmallButton2" Width="70px" 
                        OnClick="btn_approve_Click" />&nbsp; &nbsp; &nbsp;<asp:Button 
                        ID="btn_diaApp" runat="server"
                            CssClass="SmallButton2" Text="拒绝" Width="70px" 
                        OnClick="btn_diaApp_Click" />&nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
            </td>
            
            </tr>
             <tr>
                <td align="left">
                    审批记录
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvApprove" runat="server" Width="800px" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" PageSize="20"  DataKeyNames="ApproveId" EmptyDataText="No data">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                GridLines="Vertical"  CssClass="GridViewHeaderStyle">
                                <asp:TableRow>
                                    <asp:TableCell Text="审批人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批意见" Width="350px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批结果" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="审批日期" Width="200px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow BackColor="White">
                                    <asp:TableCell Text="没有记录" Width="100px" HorizontalAlign="center" ColumnSpan="4"></asp:TableCell>
                                    
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Approver" HeaderText="审批人" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveNote" HeaderText="审批意见" HtmlEncode="False">
                                <HeaderStyle Width="350px" HorizontalAlign="Center" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveResult" HeaderText="审批结果">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveDate" HeaderText="审批日期">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>    
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
