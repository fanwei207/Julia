<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_PriceApply.aspx.cs" Inherits="price_pcm_PriceApply" %>

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
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#txtQad").change(function () {
                $("#txtFormat").val("");
                $("#ddlUM op")
                $("select > option:first").attr("selected", "selected")
            })
            

            var mY_url = window.location.search;
            mY_url = mY_url.substr(1,(mY_url.length-1));
            var arr_url = mY_url.split("&");
            var arr_url1 = arr_url[1].split("=");
            
            
          
            $("#txtVender").focus(function(){
                if(($("#txtFormat").val().length==0||$("#txtFormat").val() == ""))
                {
                    alert("需求规格不可为空！");
                    return;
                
                }
                if(arr_url1[1] == 0  )
                {
                    var QAD = $("#txtQad").val();
                    var c = parseInt(QAD);
                    if(QAD.length == 14)
                    {
                        //alert("pcm_venderListView.aspx?QAD=" + $("#txtQad").val() +"&PQID="+ $("#lbPQID").text()
                        //    + "&formate=" + $("#txtFormat").val());
                        window.showModalDialog("pcm_venderListView.aspx?QAD=" + $("#txtQad").val() +"&PQID="+$("#lbPQID").text()
                            + "&formate=" + $("#txtFormat").val(), window, 'dialogHeight: 400px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');
                        window.location.href = "pcm_PriceApply.aspx?PQID=" +$("#lbPQID").text()  + "&Status=" + "0" ;
                        $("#txtQad").focus();
                    }
                }
                });
            
        })
      
    </script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <h2>
    </h2>
    <div align="left">
        <div align="left" class="MainContent_top">
            <span>申请号：&nbsp;
                <asp:Label ID="lbPQID" runat="server"></asp:Label>&nbsp;&nbsp;</span> <span>申请人：&nbsp;&nbsp;
                    <asp:Label ID="lbApplyBy" runat="server"></asp:Label>&nbsp;&nbsp;</span>
            <span>申请日期：&nbsp;&nbsp;<asp:Label runat="server" ID="lbApplyDate"></asp:Label>
            </span>&nbsp;&nbsp;&nbsp;&nbsp;申请单状态：<asp:Label ID="lbStatus" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <span>&nbsp;&nbsp;&nbsp;&nbsp; 分类：&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="0" Text="--" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="结构件"></asp:ListItem>
                            <asp:ListItem Value="2" Text="元器件"></asp:ListItem>
                            <asp:ListItem Value="3" Text="包装"></asp:ListItem>
                            <asp:ListItem Value="4" Text="电线和辅料"></asp:ListItem>
                            <asp:ListItem Value="5" Text="产成品"></asp:ListItem>
                        </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="确认" Enabled="false" CssClass="SmallButton2"
                    OnClientClick="return confirm('你确定要确认吗?确认后将无法修改内容！！');" Width="81px" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnreturn1" runat="server" Text="返回" Width="81px" CssClass="SmallButton2"
                    OnClick="btnreturn1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpload" runat="server"
                        Text="批量导入" Visible="false" CssClass="SmallButton2" OnClick="btnUpload_Click" />--%></span></div>
        <table id="out" align="center" width="1000px">
           
            <tr>
                <td align="right">
                    导入文件: &nbsp;
                </td>
                <td colspan="2" valign="top" style="height: 20px">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style="width: 300px; height: 20px;">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImport_Click" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnBasis" runat="server" CssClass="SmallButton2" onclick="btnBasis_Click" Text="上传及查看凭据列表" Width="100px" />
                </td>
                <td><asp:Button ID="btnExport" runat="server" Text="导出驳回部分" 
                        CssClass="SmallButton2"  Enabled="false" onclick="btnExport_Click" 
                        Width="108px"/></td>
            </tr>
            <tr>
                <td align="right">
                    下载模板:
                </td>
                <td colspan="4">
                   <a href="../docs/ModifyApplyDetImport.xls" target="_blank"><font color="blue">导入模板</font></a>
                     <%--<asp:LinkButton ID="lkbtnDownList" runat="server" Text="下载模板" OnClick="lkbtnDownList_Click"></asp:LinkButton>--%>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvVender" runat="server" CssClass="GridViewStyle" Width="2400px"
            DataKeyNames="DetId,InfoFrom,status,Part,Vender,UM" AutoGenerateColumns="False" OnRowCommand="gvVender_RowCommand"
            PageSize="30" AllowPaging="true" OnRowDataBound="gvVender_RowDataBound" OnPageIndexChanging="gvVender_PageIndexChanging">
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
             <asp:TemplateField >
                <ItemTemplate>
                    <asp:CheckBox ID="chk" runat="server" /><%--OnCheckedChanged="chk_OnCheckedChanged"--%>
                </ItemTemplate>
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center"   />
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
                <asp:TemplateField HeaderText="删除">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("DetId") %>'
                            Text="删除"></asp:LinkButton>
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
                <asp:BoundField HeaderText="驳回原因" DataField="ItemDesc2">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                  <asp:TemplateField HeaderText="apply状态" Visible="false">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                       <asp:Label ID="lbDetStatue" runat="server" Text='<%# Eval("canReject") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div align="center" id="divReject" runat="server" visible="false">
            <span>驳回原因：</span> <span>&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtRejectReason"
                Width="400px" CssClass="SmallTextBox" TextMode="MultiLine" Height="50px"></asp:TextBox>
                &nbsp;&nbsp;</span> <span>
                    <asp:Button ID="btnReject" runat="server" Text="驳回" CssClass="SmallButton2" OnClick="btnReject_Click" />&nbsp;&nbsp;
                   </span>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        <asp:Literal id="ltlHide" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
