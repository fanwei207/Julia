<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCB_ApplyDet.aspx.cs" Inherits="RDW_PCB_ApplyDet" %>

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
    <script language="JavaScript" type="text/javascript">
        $(function () {
            var txtPending = $("#txtUserNoInput");
            txtPending.load(txtval());
            txtPending.click(
            function () {
                txtPending.val() == "No. OR Name";
                txtcl();

            }
            );

            function txtval() {
                if (txtPending.val() == "") {

                    txtPending.val("No. OR Name");
                    txtPending.attr("color", "#AAAAAA");

                }
            }
            function txtcl() {

                txtPending.val("");

            }

         


        })
        
        
        $(function(){



            if($("#chklMaterial_4").prop("checked") )
            {
                $("#txtMaterial").show();
            }
            else
            {
                $("#txtMaterial").hide();
        
            }

            if($("#chklMachining_2").prop("checked") )
            {
                $("#txtMachining").show();
            }
            else
            {
                $("#txtMachining").hide();
        
            }

            if($("#chklSoderResistPaint_4").prop("checked") )
            {
                $("#txtSoderResistPaint").show();
            }
            else
            {
                $("#txtSoderResistPaint").hide();
                
            }
            if($("#chklLAYOUTBasis_2").prop("checked") )
            {
                $("#txtLAYOUTBasis").show();
            }
            else
            {
                $("#txtLAYOUTBasis").hide();
        
            }

            if($("#chklCopperFoil_3").prop("checked") )
            {
                $("#txtCopperFoil").show();
            }
            else
            {
                $("#txtCopperFoil").hide();
        
            }

            if($("#chklPackage_3").prop("checked") )
            {
                $("#txtPackage").show();
            }
            else
            {
                $("#txtPackage").hide();
        
            }

            if($("#chklSafety_3").prop("checked") )
            {
                $("#txtSafety").show();
            }
            else
            {
                $("#txtSafety").hide();
        
            }
            
        })
            
        function CheckBoxList_Click(sender) 
        { 
                
            var container = sender.parentNode; 
            if(container.tagName.toUpperCase() == "TD") { // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局 
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input /> 
            } 
            var chkList = container.getElementsByTagName("input"); 
            var senderState = sender.checked; 
            for(var i=0; i<chkList.length;i++) { 
                chkList[i].checked = false; 
            } 
            sender.checked = senderState; 
            
        } 

        function checkBoxListForOther(sender)
        {
            var container = sender.parentNode; 
            if(container.tagName.toUpperCase() == "TD") { // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局 
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input /> 
            } 
            var chkList = container.getElementsByTagName("input"); 
            var thisID = $(sender).prop("id");
            var no =thisID.substr(thisID.lastIndexOf("_")+1);
                
            var txt = $(sender).parent().parent().children(".other")

            if(chkList.length-1 ==no)
            {
                txt.show();
            }
            else
            {
                txt.hide();
            }

            if(!sender.checked)
            {
                txt.hide();
            }

        }
       
     
          
        
    </script>



     <style type="text/css">
         td {
              border-left: 1px solid #000;
              border-top: 1px solid #000;
              margin-left:0px;
         }
         table {
             border-right: 1px solid #000;
             border-bottom: 1px solid #000;
         }

          </style>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <h4>PCB LAYOUT申请单</h4>
            <table style="width: 800px;">
                <tr style="height: 25px;">
                    <td style="width=150px;">
                        <label>PCB品名</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductName" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                    </td>
                    <td style="width=150px">
                        <label>PCB编号</label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtPCBOldNo" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <label>项目编号</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectNo" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                    </td>
                    <%--<td>
                        <label>线路依据</label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtLineBasis" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <label>样品数量</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNum" runat="server" BorderStyle="None" Width="300px" CssClass="Number"></asp:TextBox>
                    </td>
                    <td>
                        <label>创建日期</label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lbCreatedDate" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>交付日期</label>
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtSampleDeliveryDate" runat="server" BorderStyle="None" Width="300px" CssClass="Date"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                       <label style="color: red;">*单面板3天 铝基板3天 双面板7天</label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <label>
                            外形尺寸
                        </label>
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtSize" runat="server" Width="250px"></asp:TextBox>
                        <div>
                            <input id="fileSize" name="filename" type="file" runat="server"
                                style="width: 400px; height: 24px;" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton2" OnClick="btnUpload_Click" OnClientClick="return confirm('上传会自动保存内容');" />
                        </div>
                        <div>
                             <asp:GridView ID="gvSize" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false" DataKeyNames="PCBI_ID,PCBI_URL,PCBI_FileName" OnPageIndexChanging="gvSize_PageIndexChanging" OnRowCommand="gvSize_RowCommand" >
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="PCBI_FileName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("PCBI_URL") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("PCBI_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                        </div>
                    </td>

                </tr>
                <tr>
                    <td>
                        <label>
                            PCB厚度
                        </label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtThickness" runat="server" BorderStyle="None" Width="650px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <label>
                            层数
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklply" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="1 Layer" Value="1 Layer" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="2 Layer" Value="2 Layer" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="4 Layer" Value="4 Layer" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="8 Layer" Value="8 Layer" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="16 Layer" Value="16 Layer" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                        </asp:CheckBoxList>

                    </td>

                </tr>
                <tr>
                    <td>
                        <label>
                            PCB材质
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklMaterial" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Text="FR4" Value="FR4" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="CEM1" Value="CEM1" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="CEM3" Value="CEM3" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="铝基板" Value="铝基板" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="other"></asp:TextBox>

                    </td>

                </tr>

                <tr>
                    <td>
                        <label>
                            PCB处理
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklMachining" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="全面喷锡" Value="全面喷锡" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="OSP抗氧化体层" Value="OSP抗氧化体层" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtMachining" runat="server" CssClass="other"></asp:TextBox>

                    </td>

                </tr>

                <tr>
                    <td>
                        <label>
                            制程要求
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklRequirment" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="无铅" Value="无铅" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="有铅" Value="有铅" onclick="CheckBoxList_Click(this)"></asp:ListItem>

                        </asp:CheckBoxList>


                    </td>

                </tr>

                <tr>
                    <td>
                        <label>
                            防焊漆
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklSoderResistPaint" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="绿色" Value="绿色" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="黄色" Value="黄色" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="蓝色" Value="蓝色" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="白色" Value="白色" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtSoderResistPaint" runat="server" CssClass="other"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td>
                        <label>
                            LAYOUT依据
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklLAYOUTBasis" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="原理图" Value="原理图" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="提供样板" Value="提供样板" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtLAYOUTBasis" runat="server" CssClass="other"></asp:TextBox>
                        <div>
                             <input id="fileLAYOUTBasis" name="filename" type="file" runat="server"
                                style="width: 400px; height: 24px;" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUploadLAYOUT" runat="server" Text="上传" CssClass="SmallButton2" OnClick="btnUploadLAYOUT_Click" OnClientClick="return confirm('上传会自动保存内容');" />
                        </div>
                        <div>
                             <asp:GridView ID="gvLAYOUTBasis" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false"  DataKeyNames="PCBI_ID,PCBI_URL,PCBI_FileName" OnPageIndexChanging="gvLAYOUTBasis_PageIndexChanging" OnRowCommand="gvLAYOUTBasis_RowCommand">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="PCBI_FileName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("PCBI_URL") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("PCBI_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                        
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                        </div>

                    </td>

                </tr>
                             <tr>
                    <td>
                        <label>
                            丝印颜色
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklScreenParintingColour" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Text="白色" Value="白色" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="黑色" Value="黑色" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                             <asp:ListItem Text="黄色" Value="黄色" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                            <asp:ListItem Text="无" Value="无" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                        </asp:CheckBoxList>


                    </td>

                </tr>

                           <tr>
                    <td>
                        <label>
                            铜箔厚度
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklCopperFoil" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="0.5 OZ" Value="0.5 OZ" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="1 OZ" Value="1 OZ" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="2 OZ" Value="2 OZ" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtCopperFoil" runat="server" CssClass="other"></asp:TextBox>

                    </td>

                </tr>

                
                           <tr>
                    <td>
                        <label>
                            新器件封装
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklPackage" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="提供规格书" Value="提供规格书" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="提供样板" Value="提供样板" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="无新器件" Value="无新器件" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtPackage" runat="server" CssClass="other"></asp:TextBox>

                          <div>
                             <input id="filePackage" name="filename" type="file" runat="server"
                                style="width: 400px; height: 24px;" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUploadPackage" runat="server" Text="上传" CssClass="SmallButton2" OnClick="btnUploadPackage_Click" OnClientClick="return confirm('上传会自动保存内容');" />
                        </div>
                        <div>
                             <asp:GridView ID="gvPackage" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false" DataKeyNames="PCBI_ID,PCBI_URL,PCBI_FileName" OnPageIndexChanging="gvPackage_PageIndexChanging" OnRowCommand="gvPackage_RowCommand" >
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="PCBI_FileName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("PCBI_URL") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("PCBI_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                        </div>
                    </td>

                </tr>
                 <tr>
                    <td>
                        <label>
                            安规
                        </label>
                    </td>
                    <td colspan="3" align="left">

                        <asp:CheckBoxList ID="chklSafety" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="日本" Value="日本" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="北美" Value="北美" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="欧洲" Value="欧洲" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他" onclick="CheckBoxList_Click(this);checkBoxListForOther(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="txtSafety" runat="server" CssClass="other"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td><label>LAYOUT</label></td>
                    <td colspan ="3">
                        <div>
                             <input id="fileLayoutSelf" name="filename" type="file" runat="server"
                                style="width: 400px; height: 24px;" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUploadLayoutSelf" runat="server" Text="上传" CssClass="SmallButton2"  OnClientClick="return confirm('上传会自动保存内容');" OnClick="btnUploadLayoutSelf_Click" />
                        </div>
                        <div>
                             <asp:GridView ID="gvLayout" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="false" DataKeyNames="PCBI_ID,PCBI_URL,PCBI_FileName" OnPageIndexChanging="gvPackage_PageIndexChanging" OnRowCommand="gvPackage_RowCommand" >
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="显示" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="上传人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="文件名称" DataField="PCBI_FileName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="显示">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("PCBI_URL") %>'
                                        Text="显示"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtndelete" runat="server" CommandName="lkbtndelete" CommandArgument='<%# Eval("PCBI_ID") %>'
                                        Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="上传人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="上传时间" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td><label>备注</label></td>
                    <td colspan="3" > 
                        <asp:TextBox runat="server" ID="txtRemark" Width="600px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="50px"></asp:TextBox>

                    </td>

                </tr>
                <tr runat="server" id ="applyBtn" align="center" >
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server"  CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server"  CssClass="SmallButton2" Text="提交" OnClick="btnSubmit_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReturn" runat="server"  CssClass="SmallButton2" Text="返回" OnClick="btnReturn_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <hr />
           
            <div id="divReject" runat="server">
                  <h4 >驳回信息 </h4>
                        &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtReject" Width="400px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="80px"></asp:TextBox>
                        <div>
                        <asp:Button ID="btnReject" runat="server"  CssClass="SmallButton2" Text="驳回"  Width="80px" OnClick="btnReject_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                            </div>
                    
            </div>
            <hr />
           
            <table runat="server" id="tableSample" style="width:800px">
                <tr>
                    <td align="center" colspan="6">
                         <h4 id="hSampTitle" runat="server">打样单</h4>
                    </td>
                </tr>
                <tr>
                        <td style="width=150px;">
                        <label>PCB编号</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNo" runat="server" BorderStyle="None" Width="130px"></asp:TextBox>
                    </td>
                    <td style="width=150px">
                        <label>PCB版本</label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtVar" runat="server" BorderStyle="None" Width="130px"></asp:TextBox>
                    </td>
                   <td style="width=150px">
                        <label>需求日期</label>
                    </td>
                     <td >
                        <asp:TextBox ID="txtNeedDate" runat="server" CssClass="Date" BorderStyle="None" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width=150px">
                        <label>制图人</label>
                    </td>
                    <td>
                        <asp:Label ID="lbDrawingName" runat="server"></asp:Label>
                    </td>
                    <td colspan="4">
                         <asp:TextBox ID="txtUserNoInput" runat="server" Width="90px" CssClass="User"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserNo" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserNoOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserName" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserNameOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserDept" runat="server" Width="100px" BackColor="#CCCCCC" CssClass="UserDeptOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserRole" runat="server" Width="100px" BackColor="#CCCCCC" CssClass="UserRoleOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserDomain" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserDomainOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" 
                        onpaste="return false;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                         <asp:GridView ID="gv_bos_mstr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" 
                        
                        PageSize="20" DataKeyNames="bos_nbr,bos_vendIsConfirm, bos_receiptIsConfirm,bos_isSendEmail,bos_isCanceled,bos_vend,bos_completeDate,bos_createddate"
                        Width="800px" OnPageIndexChanging="gv_bos_mstr_PageIndexChanging" OnRowCommand="gv_bos_mstr_RowCommand" OnRowDataBound="gv_bos_mstr_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                             <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="800px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名称" Width="300px"></asp:TableCell>
                                    
                                    <asp:TableCell HorizontalAlign="center" Text="打样单" Width="70px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="备注" Width="380px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="生成日期" Width="70px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="完工时间" Width="70px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="供应商" Width="70px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="收货" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="状态" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="详细" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="邮件" Width="50px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="12"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="bos_nbr" HeaderText="打样单">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vend" HeaderText="供应商">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vendName" HeaderText="供应商名称">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_rmks" HeaderText="备注">
                                <HeaderStyle HorizontalAlign="Center" Width="380px" />
                                <ItemStyle HorizontalAlign="Left" Width="380px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_createddate" HeaderText="生成日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_completeDate" HeaderText="完工时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vendIsConfirm" HeaderText="供应商确认">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_receiptIsConfirm" HeaderText="收货">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                              <asp:BoundField DataField="bos_isCanceled" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="详细">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDetail" runat="server" CommandName="Detail" Font-Underline="True"
                                        CommandArgument='<%# Eval("bos_nbr") %>' ForeColor="Black">详细</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="邮件">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkEmail" runat="server" CommandName="SendDetail" Font-Underline="True"
                                        OnClientClick="return confirm('确定通知供应商已建立此打样单');" CommandArgument='<%# Container.DataItemIndex %>'
                                        ForeColor="Black">发送</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                         </td>
                </tr>

                <tr>
                    <td  align="center" colspan="6">
                        <div runat="server" id="SampBtn">
                        <asp:Button ID="btnSeveS" runat="server"  CssClass="SmallButton2" Text="保存" OnClientClick="alert('保存不会影响申请部分信息');" OnClick="btnSeveS_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnSemp" runat="server"  CssClass="SmallButton2" Text="生成打样单" OnClick="btnSemp_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReturnS" runat="server"  CssClass="SmallButton2" Text="返回" OnClick="btnReturnS_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;
                   
                        </div>
                             </td>
                </tr>

            </table>
            <asp:HiddenField ID="hidCreatedBy" runat="server" />
            <asp:HiddenField ID="hidCreatedName" runat="server" />
            <asp:HiddenField ID="hidCreatedDate" runat="server" />

        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
