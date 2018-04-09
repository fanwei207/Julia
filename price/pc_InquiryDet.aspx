<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_InquiryDet.aspx.cs" Inherits="price_pc_InquiryDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <base target="_self">
     <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div id="divTable">
        <table width="1000px">
            <tr>
                <td>询价单号：&nbsp;&nbsp;<asp:Label ID="lbIMID" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                <td>供应商：&nbsp;&nbsp;<asp:Label ID="lbVender" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                <td style=" width:250px;">供应商名称：&nbsp;&nbsp;<asp:Label ID="lbVenderName" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                <td rowspan="3"> <asp:Button ID="btnBasis"  runat="server" Text="上传及查看凭据列表" CssClass="SmallButton2" 
                        Width="100px" onclick="btnBasis_Click"/></td>
                  <td rowspan="3">币种:&nbsp;&nbsp;<asp:DropDownList ID="ddlcurr" runat="server" AutoPostBack="true" Enabled="false"  >
                      <asp:ListItem Value="1" Text="RMB" ></asp:ListItem>
                        <asp:ListItem Value="2" Text="USD"></asp:ListItem>
                        <asp:ListItem Value="3" Text="EUR"></asp:ListItem>
                        <asp:ListItem Value="4" Text="HKD"></asp:ListItem>
                      </asp:DropDownList></td>
                    <td rowspan="3"> <asp:Button ID="btnMakeInquiry"  runat="server" Text="导出询价单" 
                            CssClass="SmallButton2" onclick="btnMakeInquiry_Click"/></td>
            </tr>
            <tr>
              <td>状态：&nbsp;&nbsp;<asp:Label ID="lbStatus" runat="server"></asp:Label>&nbsp;&nbsp;</td>
               <td>建单人：&nbsp;&nbsp;<asp:Label ID="lbCreate" runat="server"></asp:Label>&nbsp;&nbsp;</td>
               <td>建单时间：&nbsp;&nbsp;<asp:Label ID="lbCreateDate" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                 <%--<input id="filename" name="filename" type="file" runat="server" 
                style=" width:400px; height: 24px;" /></td>
                <td> <asp:Button ID="btnUpload"  runat="server" Text="上传" CssClass="SmallButton2" 
                        onclick="btnUpload_Click"/>&nbsp;&nbsp;--%>
               
            </tr>
            <tr>
            <td>供应商电话：&nbsp;&nbsp;<asp:Label ID="lbVenderPhone" runat="server"></asp:Label></td>
             <td colspan="2" align="left">供应商邮箱：&nbsp;&nbsp;<asp:Label ID="lbVenderEmail" runat="server"></asp:Label></td>
              
            
            </tr>
            <tr>
                  <td colspan="2">
                <input id="filename" name="filename" type="file" runat="server" 
                style=" width:400px; height: 24px;" /></td>
                <td> <asp:Button ID="btnUpload"  runat="server" Text="上传" CssClass="SmallButton2" 
                        onclick="btnUpload_Click"/>&nbsp;&nbsp;
                </td>
                <td colspan="3">    <asp:Button  ID="btnQuotation" runat="server" Text="报价" CssClass="SmallButton2" 
                        onclick="btnQuotation_Click"/>&nbsp;&nbsp;&nbsp;
                     <asp:Button  ID="btnSave" runat="server" Text="核价" CssClass="SmallButton2" 
                        onclick="btnSave_Click"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button  ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" 
                        onclick="btnReturn_Click"/>&nbsp;&nbsp;&nbsp;</td>
            
            </tr>
              <tr>
             
                <td  align="right" >下载模板:</td>
                <td > <a href="../docs/priceDetImport.xls" target="_blank"><font color="blue">导入模板</font></a></td>
            </tr>
        </table>
        </div>
        <div><asp:GridView ID="gvQuotationAndCalculation" runat="server" 
                    CssClass="GridViewStyle GridViewRebuild" AutoGenerateColumns="false" 
                       DataKeyNames="applyDetID,status,Part,PriceFinish,isout" 
                    onrowcommand="gvQuotationAndCalculation_RowCommand" 
                    onrowdatabound="gvQuotationAndCalculation_RowDataBound"  Width="1800px" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
             <asp:BoundField HeaderText="QAD" DataField="Part" >
                        <HeaderStyle Width="100px" />
                         <ItemStyle HorizontalAlign="Center"  />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="部件号" DataField="ItemCode" >
                        <HeaderStyle Width="100px" />
                        
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="文档">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnSelectQADDOC" runat="server" CommandName="lkbtnSelectQADDOC" CommandArgument='<%# Eval("Part") %>'
                            Text="view"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                   
                     <asp:BoundField HeaderText="单位" DataField="UM" >
                        <HeaderStyle Width="30px" />
                         <ItemStyle HorizontalAlign="Center"  />
                    </asp:BoundField>
                   <asp:TemplateField HeaderText="报价" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center"   />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="price" runat="server" CssClass="SmallTextBox Numeric" Text='<%# Eval("price") %>'  Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="自报价" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="priceSelf" runat="server" CssClass="SmallTextBox Numeric" Text='<%# Eval("priceSelf") %>' Width="80px"></asp:TextBox>
                    </ItemTemplate>
                         </asp:TemplateField>
                    <asp:TemplateField HeaderText="报价备注" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="PriceBasis" runat="server" CssClass="SmallTextBox " Text='<%# Eval("PriceBasis") %>' Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="折扣方式" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="priceDiscount" runat="server" CssClass="SmallTextBox " Text='<%# Eval("priceDiscount") %>' Width="200px" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="核价" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="checkPrice" runat="server" CssClass="SmallTextBox Numeric" Text='<%# Eval("checkPrice") %>'  Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="核价备注" >
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                       <asp:TextBox ID="FinCheckPriceBasis" runat="server" CssClass="SmallTextBox " Text='<%# Eval("FinCheckPriceBasis") %>' Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ApplyDetStatue"  Visible="false">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True"  />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                    <asp:Label id="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="技术部指定" >
                    <ControlStyle Font-Bold="False" Font-Size="12px"  />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"  /> 
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                    <asp:Label id="lbInfoFrom" runat="server" Text='<%# Eval("InfoFrom") %>' Font-Underline="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="取消" >
                    <ControlStyle Font-Bold="False" Font-Size="12px"  />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"  />
                    <ItemStyle HorizontalAlign="Center"  Font-Underline="true" />
                    <ItemTemplate>
                    <asp:LinkButton id="lkbtnCancel" runat="server"  CommandName="lkbtnCancel" CommandArgument='<%# Eval("applyDetID") %>' ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField HeaderText="技术参考价" DataField="priceFromTechnical">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="需求规格" DataField="Formate" >
                        <HeaderStyle Width="300px" />
                    </asp:BoundField>
                   <asp:BoundField HeaderText="详细描述" DataField="ItemDescription" >
                        <HeaderStyle Width="500px" />
                    </asp:BoundField>
                <asp:BoundField HeaderText="描述1" DataField="ItemDesc1" >
                        <HeaderStyle Width="300px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="描述2" DataField="ItemDesc2" >
                        <HeaderStyle Width="300px" />
                    </asp:BoundField>
                 <asp:BoundField HeaderText="100详细描述" DataField="Description" >
                        <HeaderStyle Width="500px" />
                    </asp:BoundField>
                <asp:BoundField HeaderText="100描述1" DataField="item_qad_desc1" >
                        <HeaderStyle Width="300px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="100描述2" DataField="item_qad_desc2" >
                        <HeaderStyle Width="300px" />
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="取消状态"  Visible="false">
                    <ControlStyle Font-Bold="False" Font-Size="12px"  />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"  />
                    <ItemStyle HorizontalAlign="Center"  />
                    <ItemTemplate>
                        <asp:Label id="lbIsCancel" runat="server" Text='<%# Eval("isCancel") %>' Font-Underline="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
            
            </Columns>
            
            </asp:GridView></div>
        <div>                </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
