<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_supplierApplyToManager.aspx.cs" Inherits="price_pcm_supplierApplyToManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
   <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[name$='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {
                $("#gvVender input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })

            var divReason =  $("#divReject");

            divReason.hover(function(){},function(){
                outDivReject();
            });

            var txtCloseReason = $("#txtCloseReason");
            txtCloseReason.load(txtval());
            txtCloseReason.click(
            function () {
                txtCloseReason.val() == "请输入驳回原因：";
                txtcl();

            }
            );


            function txtval() {
                if (txtCloseReason.val() == "") {

                    txtCloseReason.val("请输入关闭原因：");
                    txtCloseReason.attr("color", "#AAAAAA");

                }
            }
            function txtcl() {

                txtCloseReason.val("");

            }

        })
    </script>
     <script language="JavaScript" type="text/javascript">

         function beginReject()
         {
            
             var chkList = $(".chkList>input")
             var flagIsCheck = false;
             chkList.each(function(){
                 if(this.checked)
                 {
                     flagIsCheck = true;
                 }
             })

                 
             if(flagIsCheck)
             {
                 $("#divReject").show();
             
                 var txtCloseReason = $("#txtCloseReason");
                 txtCloseReason.text("请输入驳回原因：");
                 $("#txtCloseReason")[0].focus();
             }
             else
             {
                 alert("您没有选择驳回的申请");
             }
            
         }

         function outDivReject()
         {
             $("#divReject").hide();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
               QAD号<asp:TextBox ID="txtPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            供应商:<asp:TextBox ID="txtVender" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
             供应商名：<asp:TextBox ID="txtVenderName" runat="server"  CssClass="SmallTextBox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;

             <asp:Button ID="btnSelect" runat="server" Text="查询"  CssClass="SmallButton2"
                     Width="81px" OnClick="btnSelect_Click"   />&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnSubmit" runat="server" Text="确认"  CssClass="SmallButton2"
                    OnClientClick="return confirm('你确定要确认吗?！！');" Width="81px" OnClick="btnSubmit_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btnReject" type="button" class="SmallButton2"  value="驳回"  onclick ="beginReject()" style="width:81px"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    <div>
                <asp:GridView ID="gvVender" runat="server" CssClass="GridViewStyle" Width="2400px"
            DataKeyNames="DetId" AutoGenerateColumns="False" OnRowCommand="gvVender_RowCommand" 
             >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="2400px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="部件号" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="文档" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="币种" Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="单位" Width="40px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="系统单位" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="转换因子" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="删除" Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD价格最小值" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD原价格" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD价格最大值" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="是否可抵扣" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="税率" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="原价格的开始时间" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="原价格的关闭时间" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请价格" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请是否可抵扣" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请税率" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请价格的开始时间" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请价格的关闭时间" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="详细描述" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="描述1" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="描述2" Width="300px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="26"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                        <input id="chkAll" type="checkbox">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk" runat="server"  CssClass="chkList"/>
                </ItemTemplate>
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
                <asp:BoundField HeaderText="QAD" DataField="Part">
                    <HeaderStyle Width="60px" />
                    <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="部件号" DataField="ItemCode">
                    <HeaderStyle Width="120px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:TemplateField HeaderText="文档">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnSelectQADDOC" runat="server" CommandName="lkbtnSelectQADDOC"
                            CommandArgument='<%# Eval("Part") %>' Text="view"></asp:LinkButton>
                    </ItemTemplate>
                  
                </asp:TemplateField>
                <asp:BoundField HeaderText="供应商" DataField="Vender">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="VenderName">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                    <asp:BoundField HeaderText="币种" DataField="Curr">
                    <HeaderStyle Width="30px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="单位" DataField="UM">
                    <HeaderStyle Width="40px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="系统单位" DataField="ptum">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="转换因子" DataField="changeFor">
                    <HeaderStyle Width="100" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:TemplateField HeaderText="相关依据">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnBasis" runat="server" CommandName="lkbtnBasis" CommandArgument='<%# Eval("DetId") %>'
                            Text="查看"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:BoundField HeaderText="QAD最小价格" DataField="qadMinPrice">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="QAD原价格" DataField="oldPrice">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="QAD最大价格" DataField="qadMaxPrice">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="是否可抵扣" DataField="isDeductible">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="税率" DataField="taxes">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="原价格的开始时间" DataField="oldPriceStarDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="原价格的关闭时间" DataField="oldPriceEndDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="申请价格" DataField="applyPrice">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="申请是否可抵扣" DataField="applyIsDeductible">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="申请税率" DataField="applyTaxes">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="申请价格的开始时间" DataField="applyPriceStarDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="100px" /> 
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="申请价格的关闭时间" DataField="applyPriceEndDate" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="100px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="CheckPriceBasis">
                    <HeaderStyle Width="100px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="详细描述" DataField="ItemDescription">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述1" DataField="ItemDesc1">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述2" DataField="ItemDesc2">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                 
            </Columns>
        </asp:GridView>
        <div id="divReject" style="width:40%;height:60%;position:absolute;display:none;top:10%;left:20%;background-color:white" align="center" >
                 <asp:TextBox runat="server" ID="txtCloseReason" Width="100%" CssClass="SmallTextBox"
                    TextMode="MultiLine" Height="80%"></asp:TextBox>
            <br />
            <asp:Button ID="btnRejectTow" runat="server" Text="驳回" CssClass="SmallButton2" OnClientClick="return confirm('你确定要驳回吗?！！');" Width="80px" OnClick="btnRejectTow_Click"  />
        </div>
    </div>
    </form>
     <script type="text/javascript">
         <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
