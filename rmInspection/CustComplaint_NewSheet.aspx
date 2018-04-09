<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustComplaint_NewSheet.aspx.cs" Inherits="EDI_CustComplaint_NewSheet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客户投诉-新建投诉单</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquerysession.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btnSubmit").click(function () {
                var customer = $("#txtCustomer").val();
                var order = $("#txtOrder").val();
                var problemContent = $("#txtProblemContent").val();
                if (customer == '')
                {
                    alert('客户不能为空');
                    return false;
                }
                if (order == '') {
                    alert('订单号不能为空');
                    return false;
                }
                if (problemContent == '') {
                    alert('问题描述不能为空');
                    return false;
                }
            });
            $("#btnNew").click(function(){
                var line = $("#txtLine").val();
                var part = $("#txtPart").val();
                var qty = $("#txtQty").val();
                var DateCode = $("#txtDateCode").val();
                var dueDate = $("#txtDueDate").val();
                var poline = $("#txtLine").val();var customer = $("#txtCustomer").val();
                var order = $("#txtOrder").val();
                var problemContent = $("#txtProblemContent").val();
                if (customer == '')
                {
                    alert('客户不能为空');
                    return false;
                }
                if (order == '') {
                    alert('订单号不能为空');
                    return false;
                }
                if (problemContent == '') {
                    alert('问题描述不能为空');
                    return false;
                }
                //if(line == "")
                //{
                //    alert("行号不能为空");
                //    return false;
                //}
                //else
                //{
                //    if(isNaN(line))
                //    {
                //        alert('行号为数字');
                //        $("#txtLine").val('');
                //        return false;
                //    }
                //}
                if($("#txtOrder").val() == '')
                {
                    alert("原订单号为空，不能新增赔料信息");
                    return false;
                }
                if(poline == "原订单行号")
                {
                    alert("原订单行号不能为空");
                    return false;
                }
                else
                {
                    if(isNaN(poline))
                    {
                        alert('行号为数字');
                        $("#txtLine").val('');
                        return false;
                    }
                }
                if(part == "物料号")
                {
                    alert("物料号不能为空");
                    return false;
                }
                if(qty == "数量")
                {
                    alert("数量不能为空");
                    return false;
                }
                else
                {
                    if(isNaN(qty))
                    {
                        alert('数量必须为数字');
                        $("#txtQty").val('数量');
                        return false;
                    }
                }
                if($("#txtDetReqDate").val() == "")
                {
                    alert("Req Date不能为空");
                    return false;
                }
                if($("#txtDetDueDate").val() == "")
                {
                    alert("Due Date不能为空");
                    return false;
                }
            });
            $("#btnNew1").click(function(){
                var line = $("#txtLine1").val();
                var part = $("#txtPart1").val();
                var qty = $("#txtQty1").val();
                var DateCode = $("#txtDateCode1").val();
                var dueDate = $("#txtDueDate1").val();
                var poline = $("#txtLine1").val();var customer = $("#txtCustomer").val();
                var order = $("#txtOrder").val();
                var problemContent = $("#txtProblemContent").val();
                if (customer == '')
                {
                    alert('客户不能为空');
                    return false;
                }
                if (order == '') {
                    alert('订单号不能为空');
                    return false;
                }
                if (problemContent == '') {
                    alert('问题描述不能为空');
                    return false;
                }
                //if(line == "")
                //{
                //    alert("行号不能为空");
                //    return false;
                //}
                //else
                //{
                //    if(isNaN(line))
                //    {
                //        alert('行号为数字');
                //        $("#txtLine").val('');
                //        return false;
                //    }
                //}
                if($("#txtOrder").val() == '')
                {
                    alert("原订单号为空，不能新增赔料信息");
                    return false;
                }
                if(poline == "原订单行号")
                {
                    alert("原订单行号不能为空");
                    return false;
                }
                else
                {
                    if(isNaN(poline))
                    {
                        alert('行号为数字');
                        $("#txtLine1").val('');
                        return false;
                    }
                }
                if(part == "物料号")
                {
                    alert("物料号不能为空");
                    return false;
                }
                if(qty == "数量")
                {
                    alert("数量不能为空");
                    return false;
                }
                else
                {
                    if(isNaN(qty))
                    {
                        alert('数量必须为数字');
                        $("#txtQty1").val('数量');
                        return false;
                    }
                }
                if($("#txtGoodsDateCode").val() == "")
                {
                    alert("退换货产品的Date Code不能为空");
                    return false;
                }
            });
            //End part
            $("#txtLine").focus(function(){
                if($("#txtLine").val() == '原订单行号')
                {
                    $(this).val('');
                }
            });
            $("#txtLine").blur(function(){
                if($("#txtLine").val() == '')
                {
                    $(this).val('原订单行号');
                }
            });

            
            $("#txtPart").focus(function(){
                if($("#txtPart").val() == '物料号')
                {
                    $(this).val('');
                }
            });
            $("#txtPart").blur(function(){
                if($("#txtPart").val() == '')
                {
                    $(this).val('物料号');
                }
            });

            
            $("#txtQty").focus(function(){
                if($("#txtQty").val() == '数量')
                {
                    $(this).val('');
                }
            });
            $("#txtQty").blur(function(){
                if($("#txtQty").val() == '')
                {
                    $(this).val('数量');
                }
            });
            
            //退换货
            $("#txtLine1").focus(function(){
                if($("#txtLine1").val() == '原订单行号')
                {
                    $(this).val('');
                }
            });
            $("#txtLine1").blur(function(){
                if($("#txtLine1").val() == '')
                {
                    $(this).val('原订单行号');
                }
            });

            
            $("#txtPart1").focus(function(){
                if($("#txtPart1").val() == '物料号')
                {
                    $(this).val('');
                }
            });
            $("#txtPart1").blur(function(){
                if($("#txtPart1").val() == '')
                {
                    $(this).val('物料号');
                }
            });

            
            $("#txtQty1").focus(function(){
                if($("#txtQty1").val() == '数量')
                {
                    $(this).val('');
                }
            });
            $("#txtQty1").blur(function(){
                if($("#txtQty1").val() == '')
                {
                    $(this).val('数量');
                }
            });
            $("#ddlOrders").bind("change", function() {
                //alert($("#ddlOrder").val());
                //$("#two").attr("class","divClass");
                if($("#ddlOrder").val() == '1')
                {
                    if ($("#ddlOrder").hasClass("CCPSo")==$("#ddlOrder").is(".CCPSo"))
                    {
                        alert('有CSS1');
                        $("#ddlOrder").removeClass("CCPSo")
                        $("#ddlOrder").attr("class","CCPOrder");
                    }
                }
                else if($("#ddlOrder").val() == '2')
                {
                    //alert('有CSS');
                    if ($("#ddlOrder").hasClass("CCPOrder")==$("#ddlOrder").is(".CCPOrder"))
                    {
                        alert('有CSS2');
                        $("#ddlOrder").removeClass("CCPOrder")
                        $("#ddlOrder").attr("class","CCPSo");
                    }
                }
                return false;
            });
            /*
            $("#txtDetReqDate").focus(function(){
                if($("#txtDetReqDate").val() == 'Req Date')
                {
                    $(this).val('');
                }
            });
            $("#txtDetReqDate").blur(function(){
                if($("#txtDetReqDate").val() == '')
                {
                    $(this).val('Req Date');
                }
            });

            
            $("#txtDetDueDate").focus(function(){
                if($("#txtDetDueDate").val() == 'Due Date')
                {
                    $(this).val('');
                }
            });
            $("#txtDetDueDate").blur(function(){
                if($("#txtDetDueDate").val() == '')
                {
                    $(this).val('Due Date');
                }
            });
            */
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td style="width:60px;">投诉单号</td>
                <td style="width:100px;">
                    <asp:Label ID="labCustComplaintNo" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:30px;">客户</td>
                <td>
                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox5 Customer"></asp:TextBox>
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                        <asp:ListItem Text="原订单" Value="1"></asp:ListItem>
                        <asp:ListItem Text="销售单" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox5 CCPOrder"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Due Date</td>
                <td>
                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="SmallTextBox5 Date"></asp:TextBox>
                </td>
                <td>Date Code</td>
                <td>
                    <asp:TextBox ID="txtDateCode" runat="server" CssClass="smalltextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>问题描述</td>
                <td colspan="5">
                    <asp:TextBox ID="txtProblemContent" runat="server" CssClass="SmallTextBox5" TextMode="MultiLine" Width="650px" Height="150px" MaxLength="2000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>上　　传</td>
                <td colspan="5">
                    <input id="filename" runat="server" style="width:580px;" name="resumename"  CssClass="SmallTextBox"  type="file"/>
                    <asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton3" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,CustComp_No,CustComp_FileName,CustComp_FilePath,createBy,createName,createDate"
                        OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting"
                        AllowPaging="False" PageSize="20">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名" Width="300px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传日期" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建人" Width="120px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="CustComp_FileName" HeaderText="文件名">
                                <HeaderStyle Width="240px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="240px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                <HeaderStyle Width="170px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="170px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createName" HeaderText="创建人">
                                <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="查看" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height:10px;"></td>
            </tr>
            <tr>
                <td>
                    <%--<input runat="server" class="money" type="checkbox" id="chkMoney" value="赔款">赔款 --%>
                    <input type="checkbox" runat="server" id="chkMoney" name="chkMoney" value="赔款">赔款
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">金额 <asp:TextBox ID="txtMoney" runat="server"></asp:TextBox>($)
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPart" runat="server" Text="赔料" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:TextBox ID="txtLine" runat="server" CssClass="smalltextbox" Text="原订单行号" Enabled="true" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txtPart" runat="server" CssClass="smalltextbox CCPPart" Text="物料号" Width="260px"></asp:TextBox>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="smalltextbox" Text="数量" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtDetReqDate" runat="server" CssClass="smalltextbox Date" Width="90px"></asp:TextBox>
                    <asp:TextBox ID="txtDetDueDate" runat="server" CssClass="smalltextbox Date" Width="90px"></asp:TextBox>
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton3" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:GridView ID="gvPart" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,Payment_Qty
                        ,Payment_Price,Payment_Total,createBy,createName,createDate,Payment_DetDueDate"
                        OnRowDeleting="gvPart_RowDeleting" OnRowDataBound="gvPart_RowDataBound"                       
                        AllowPaging="False" PageSize="20">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="原订单行号" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料号" Width="180px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="描述" Width="110px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Req Date" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单价" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="共计" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="poLine" HeaderText="原订单行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_Site" HeaderText="出运地">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Part" HeaderText="物料号">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Description" HeaderText="描述">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Qty" HeaderText="数量">
                                <HeaderStyle Width="30px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DetReqDate" HeaderText="Req Date">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DetDueDate" HeaderText="Due Date">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Price" HeaderText="单价">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Total" HeaderText="共计">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            
            <tr>
                <td colspan="6">
                    <asp:CheckBox ID="chkGoods" runat="server" Text="退换货" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:TextBox ID="txtLine1" runat="server" CssClass="smalltextbox" Text="原订单行号" Enabled="true" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txtPart1" runat="server" CssClass="smalltextbox CCPPart" Text="物料号" Width="260px"></asp:TextBox>
                    <asp:TextBox ID="txtQty1" runat="server" CssClass="smalltextbox" Text="数量" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtGoodsDateCode" runat="server" CssClass="smalltextbox" Width="90px"></asp:TextBox>
                    <asp:Button ID="btnNew1" runat="server" CssClass="SmallButton3" Text="新增" OnClick="btnNew1_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:GridView ID="gvGoods" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,Payment_Qty
                        ,Payment_Price,Payment_Total,createBy,createName,createDate,Payment_DetDueDate,Payment_DateCode"
                         OnRowDeleting="gvGoods_RowDeleting"  OnRowDataBound="gvGoods_RowDataBound"                       
                        AllowPaging="False" PageSize="20">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="原订单行号" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料号" Width="180px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="描述" Width="110px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="数量" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="DateCode" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单价" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="共计" Width="60px"></asp:TableCell>
                                    
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="poLine" HeaderText="原订单行号">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SID_Site" HeaderText="出运地">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Part" HeaderText="物料号">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Description" HeaderText="描述">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Qty" HeaderText="数量">
                                <HeaderStyle Width="30px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_DateCode" HeaderText="DateCode">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Price" HeaderText="单价">
                                <HeaderStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Payment_Total" HeaderText="共计">
                                <HeaderStyle Width="60px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton3" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="SmallButton3" Visible="false" OnClick="btnEdit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidCustCompNo" runat="server" />
        <asp:HiddenField ID="hidMoneyStatus" runat="server" />
        <asp:HiddenField ID="hidGoodsStatus" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidType" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
