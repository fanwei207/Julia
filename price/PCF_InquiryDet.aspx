<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCF_InquiryDet.aspx.cs" Inherits="price_PCF_InquiryDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        td {
            width: 80px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        //$(function(){
        //    //$('#gvInfo > ');
        //    //gvInfo$ctl02$chkDeductib
        //    //gvInfo$ctl02$txtTaxes

        //    var checkBoxList= $('[id^="gvInfo_"][id*="_chkDeductib"]')

        //    checkBoxList.each(function (){
                
        //        changeEnable(this);
            
        //    });

        //    function changeEnable(obj)
        //    {
        //        $(obj).click(function(){
        //            var Taxes = $(this).parent().parent().parent().children().children(".Taxes");
        //            Taxes.each(function(){
        //                $(this).prop("disabled",!$(obj).prop("checked"));
                       
        //            });
                    
        //        });
        //    };

        //})
        </script>
     <script language="JavaScript" type="text/javascript">
         $(function(){

             var divamt = $("#min")

             divamt.hide();

             //var checkPriceList= $('[id^="gvInfo_"][id*="_checkPrice"]');

             //divamt.hover(function(){
             //    divamt.show();
             //},function(){
                 
             //});
             //flag = 1 鼠标在div上
             //flag = 2 鼠标在checkprice上
             //flag = 0 鼠标没有点击过
             
             //运行方法
             function enterAmtAndMin()
             {
                 var txtAllMinAmt = divamt.has('[id^="txt"]');
                 
                 var txtMinList =  txtAllMinAmt.has('[id*="Min"]');


                 var minval = 1;

                 var minstr = '';
                 
                 txtAllMinAmt

                 txtMinList.each(function(){
                     var val = parseInt($(this).val());
                     if($(this).val() =="")
                     {
                         val = 0
                     }
                     if(val == 'NaN')
                     {
                         alert("您输入的存在不是正整数");
                         return
                     }


                    
                 })

                 //验证amt
                 var txtAmtList =  txtAllMinAmt.has('[id*="Price"]');
             }

             divamt.hover(function(){
                 
                 divamt.show();

             },function(){
              
                 //setTimeout(function(){
                     
                 //},5000);
                 moveOutDiv(divamt);
                 
             });

             //当移出的时候执行
             function moveOutDiv(objCheckprice)
             {

                     var txtAmtMinList = $(".clearTable input");
                     txtAmtMinList.each(function(){
                         $(this).val("");
                     });
                     $("#txtMin1").val("1");
                     divamt.hide();

                 
             }

             var checkPriceList =  $(".amtTd");
             checkPriceList.each(function(){
          
                 $(this).dblclick(function(){
                     
                     toShow(this);
                    

                  
                
                 });
            
                 //$("#btnCommit").dblclick(toShow(this));
                 //展示下方div的方法
                 function toShow(objCheckprice){
                
                     
                     var offclick = GetElementPoint(objCheckprice)
                     var beginX = offclick["x"];
                     var beginY = offclick["y"];
                  
                     divamt.css("left",beginX+83 + "px"); 
                     divamt.css("top",beginY+ "px");

                     divamt.show();
                     $("#hidCheckPriceIDName").html(objCheckprice.id);
                     
                     //unbind("click") 移除事件
                      
                 

                    
                 };
                 function GetElementPoint(object)
                 {
                     var x=0,y=0;
                     while(object.offsetParent)
                     {
         
                         x+=object.offsetLeft;
                         y+=object.offsetTop;
                         object=object.offsetParent;
                     }
                     x+=object.offsetLeft;
                     y+=object.offsetTop;
                     return {'x':x,'y':y};
                 }

           
        
             });
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <table>
                <tr>
                    <td align="right">
                        <label>询价单</label>
                    </td>
                    <td>
                        <asp:Label ID="lbIMID" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <label>供应商</label>
                    </td>
                    <td>
                        <asp:Label ID="lbVender" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <label>供应商名称</label>
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="lbVenderName" runat="server"></asp:Label>
                    </td>
                    <td rowspan="2" style="width: 120px">
                        <asp:Button ID="btnUploadBasis" runat="server" Text="上传依据" Width="100px" CssClass="SmallButton2" OnClick="btnUploadBasis_Click" />

                    </td>

                    <td rowspan="2">
                        <asp:Button ID="btnInquiry" runat="server" Text="导出询价单" Width="100px" CssClass="SmallButton2" OnClick="btnInquiry_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label>状态</label>
                    </td>
                    <td>
                        <asp:Label ID="lbStutas" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <label>建单人</label>
                    </td>
                    <td>
                        <asp:Label ID="lbCreate" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <label>建单时间</label>
                    </td>
                    <td>
                        <asp:Label ID="lbCreateDate" runat="server"></asp:Label>
                        
                    </td>
                </tr>
<%--                <tr>
                    <td align="right">
                        <label>供应商电话</label>
                    </td>
                    <td>
                        <asp:Label ID="lbTelephone" runat="server"></asp:Label>
                    </td>

                </tr>--%>
                <tr>
                    <td></td>
                    <td colspan="4">
                        <input id="filename" name="filename" type="file" runat="server"
                            style="width: 400px; height: 24px;" />
                        <br />
                        <asp:LinkButton ID="lkbtnDownList" runat="server" Text="下载模板" OnClick="lkbtnDownList_Click"></asp:LinkButton>
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btnUpload" runat="server" Text="批量上传核价数据" CssClass="SmallButton2"
                            OnClick="btnUpload_Click" Width="150px" />&nbsp;&nbsp;
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="核价" CssClass="SmallButton2"
                            OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2"
                        OnClick="btnReturn_Click" />&nbsp;&nbsp;&nbsp;

                    </td>

                </tr>
            </table>
            <div>
                <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    Width="1600px" DataKeyNames="PCF_ID,PCF_sourceID,PCF_states,PCF_part,PCF_isDeductible" OnRowDataBound="gvInfo_RowDataBound" OnRowCommand="gvInfo_RowCommand">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="QAD" DataField="PCF_part">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="相关信息">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtnSelectApplyDOC" runat="server" CommandName="lkbtnSelectApplyDOC" CommandArgument='<%# Eval("PCF_sourceID") %>'
                                    Text="view"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="基本单位" DataField="PCF_ptum">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="采购单位" DataField="PCF_um">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="转换因子" DataField="PCF_changeFor">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="报价">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="Price" runat="server" CssClass="SmallTextBox price" Text='<%# Eval("PCF_price") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="核价">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="checkPrice" runat="server" CssClass="SmallTextBox price amtTd" Text='<%# Eval("PCF_checkPrice") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="折扣表">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbAmt" runat="server" Text='<%# Eval("PCF_amt") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="最小值">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbMin" runat="server" Text='<%# Eval("PCF_min") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="核价备注">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="FinCheckPriceBasis" runat="server" CssClass="SmallTextBox " Text='<%# Eval("PCF_checkPriceBasis") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="取消">
                            <ControlStyle Font-Bold="False" Font-Size="12px" />
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Font-Underline="true" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtnCancel" runat="server" CommandName="lkbtnCancel" CommandArgument='<%# Eval("PCF_ID") %>' Text="取消"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="税率（%）">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTaxes" runat="server" CssClass="SmallTextBox Taxes" Text='<%# Eval("PCF_taxes") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可抵扣">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDeductible" runat="server"   Width="30px"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="描述" DataField="PCF_desc">
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="用途" DataField="PCF_purpose">
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="规格" DataField="PCF_format">
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="描述1" DataField="PCF_desc1">
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="描述2" DataField="PCF_desc2">
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>
                    </Columns>


                </asp:GridView>
            </div>
            <div id="min" style="position: absolute; width: 550px; height: 170px;background-color:#dee;display: none">
                <div id="hidCheckPriceIDName" style="display: none"></div>
                <table class ="clearTable">
                    <tr>
                        <th>
                            <label>最小值</label>
                        </th>
                        <th>
                            <label>价格</label>
                        </th>
                        <th>
                            <label>最小值</label>
                        </th>
                        <th>
                            <label>价格</label>
                        </th>
                        <th>
                            <label>最小值</label>
                        </th>
                        <th>
                            <label>价格</label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMin1" runat="server" TabIndex="1" CssClas="SmallTextBox " Width="80px">1</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice1" runat="server" TabIndex="2" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin6" runat="server" TabIndex="11" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice6" runat="server" TabIndex="12" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin11" runat="server" TabIndex="21" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice11" runat="server" TabIndex="22" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMin2" runat="server" TabIndex="3" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice2" runat="server" TabIndex="4" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin7" runat="server" TabIndex="13" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice7" runat="server" TabIndex="14" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin12" runat="server" TabIndex="23" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice12" runat="server" TabIndex="24" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMin3" runat="server" TabIndex="5" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice3" runat="server" TabIndex="6" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin8" runat="server" TabIndex="15" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice8" runat="server" TabIndex="16" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin13" runat="server" TabIndex="25" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice13" runat="server" TabIndex="26" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMin4" runat="server" TabIndex="7" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice4" runat="server" TabIndex="8" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin9" runat="server" TabIndex="17" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice9" runat="server" TabIndex="18" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin14" runat="server" TabIndex="27" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice14" runat="server" TabIndex="28" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMin5" runat="server" TabIndex="9" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice5" runat="server" TabIndex="10" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin10" runat="server" TabIndex="19" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice10" runat="server" TabIndex="20" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMin15" runat="server" TabIndex="29" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice15" runat="server" TabIndex="30" CssClas="SmallTextBox" Width="80px">0</asp:TextBox>
                        </td>

                    </tr>
                </table>
                <input id="btnCommit" type="button" value ="确认" class ="SmallButton2" onclick="enterAmtAndMin()" style="width:80px" />
                
            </div>
            
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

   
</body>
</html>
