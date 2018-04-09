<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_PriceApply.aspx.cs" Inherits="price_pc_PriceApply" %>

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
            $("#txtQad").change(function () {
                $("#txtFormat").val("");
                $("#ddlUM op")
                $("select > option:first").attr("selected", "selected")
            })
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
                <asp:Button ID="btnSubmit" runat="server" Text="提交" Enabled="false" CssClass="SmallButton2"
                    OnClientClick="return confirm('你确定要提交吗?提交后将无法修改内容！！');" Width="81px" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnreturn1" runat="server" Text="返回" Width="81px" CssClass="SmallButton2"
                    OnClick="btnreturn1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpload" runat="server"
                        Text="批量导入" Visible="false" CssClass="SmallButton2" OnClick="btnUpload_Click" />--%></span></div>
        <table id="tbinsert" align="center" runat="server">
            <tr>
               
                <td align="center">
                    QAD号: &nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtQad" CssClass="SmallTextBox Part" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;<br/>
                   <asp:Label runat="server" ID="lbTechnicalPrice" Visible ="false">技术参考价:</asp:Label> &nbsp;&nbsp; <asp:TextBox runat="server" ID="txtTechnicalPrice" CssClass="SmallTextBox" Width="120px" Visible ="false"></asp:TextBox>
                </td>
                <td>
                    需求规格:
                </td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtFormat" Width="400px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="50px"></asp:TextBox>
                    &nbsp;&nbsp;
                </td>
                <td style="width: 190px;">
                    供应商: &nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtVender" Width="120px" CssClass="SmallTextBox Supplier"></asp:TextBox>
                    &nbsp;&nbsp;
                </td>
                <td style="width: 120px;">
                    单位;&nbsp;<asp:DropDownList ID="ddlUM" runat="server">
                        <asp:ListItem Text="EA" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="BO"></asp:ListItem>
                        <asp:ListItem Text="G"></asp:ListItem>
                        <asp:ListItem Text="KG"></asp:ListItem>
                        <asp:ListItem Text="M"></asp:ListItem>
                        <asp:ListItem Text="PC"></asp:ListItem>
                        <asp:ListItem Text="RL"></asp:ListItem>
                        <asp:ListItem Text="L"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnAddVender" CssClass="SmallButton2" Text="添加供应商"
                        OnClick="btnAddVender_Click" Width="64px" />
                  
                </td>
            </tr>
            <tr>
                <td align="right">
                    导入文件: &nbsp;
                </td>
                <td colspan="2" valign="top" style="height: 28px">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style="width: 110px; height: 28px;">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImport_Click" />
                </td>
                <td><asp:Button ID="btnExport" runat="server" Text="导出驳回部分" 
                        CssClass="SmallButton2"  Enabled="false" onclick="btnExport_Click" 
                        Width="108px"/></td>
            </tr>
            <tr>
                <td align="right">
                    下载模板:
                </td>
                <td colspan="3">
                    <a href="../docs/ApplyDetImport.xls" target="_blank"><font color="blue">导入模板</font></a>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvVender" runat="server" CssClass="GridViewStyle" Width="2000px"
            DataKeyNames="DetID,InfoFrom,status,QADNO" AutoGenerateColumns="False" OnRowCommand="gvVender_RowCommand"
            PageSize="30" AllowPaging="true" OnRowDataBound="gvVender_RowDataBound" OnPageIndexChanging="gvVender_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="部件号" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="是否指定" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="删除" Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="需求规格" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="描述1" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="描述2" Width="300px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="9"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
             <asp:TemplateField >
                <ItemTemplate>
                    <asp:CheckBox ID="chk" runat="server"/>
                </ItemTemplate>
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center"   />
            </asp:TemplateField>
                <asp:BoundField HeaderText="QAD" DataField="QADNO">
                    <HeaderStyle Width="60px" />
                    <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="部件号" DataField="itemCode">
                    <HeaderStyle Width="120px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:TemplateField HeaderText="文档">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnSelectQADDOC" runat="server" CommandName="lkbtnSelectQADDOC"
                            CommandArgument='<%# Eval("QADNO") %>' Text="view"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="单位" DataField="UM">
                    <HeaderStyle Width="30px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="vender">
                    <HeaderStyle Width="60px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="venderName">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="技术部指定">
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbIsAppoint" runat="server" Text='<%# Eval("InfoFrom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbtnDelete" CommandArgument='<%# Eval("DetId") %>'
                            Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="操作">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnUpdateDesc" runat="server" CommandName="lkbtnUpdateDesc" Visible="false"  CommandArgument='<%#  ((GridViewRow) Container).RowIndex %>'
                            Text="修改描述申请"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lkbtnAppvClose" runat="server" CommandName="lkbtnAppvClose" Visible="false"  CommandArgument='<%# Eval("DetId") %>'
                            Text="关闭申请"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="技术参考价" DataField="priceFromTechnical">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="需求规格" DataField="formate">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>

                <asp:BoundField HeaderText="详细描述" DataField="ItemDescription">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述1" DataField="desc1">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="描述2" DataField="desc2">
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
        <div align="center" id="divReject" runat="server">
            <span>驳回原因：</span> <span>&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtRejectReason"
                Width="400px" CssClass="SmallTextBox" TextMode="MultiLine" Height="50px"></asp:TextBox>
                &nbsp;&nbsp;</span> <span>
                    <asp:Button ID="btnReject" runat="server" Text="驳回" CssClass="SmallButton2" OnClick="btnReject_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnPass" runat="server" Text="通过" CssClass="SmallButton2" OnClick="btnPass_Click" /></span>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        <asp:Literal id="ltlHide" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
