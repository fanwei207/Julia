<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_AddPcApply.aspx.cs" Inherits="part_NPart_AddPcApply" %>

<!DOCTYPE html>

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
                $("#gvDet input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="left">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" Font-Size="12px">QAD:</asp:Label>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQAD" CssClass="SmallTextBox5 Part" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>

                    <td>
                        <asp:Label runat="server" Font-Size="12px">物料号:</asp:Label>&nbsp;&nbsp;&nbsp;
                    </td>

                    <td>
                        <asp:TextBox runat="server" ID="txtCode" CssClass="SmallTextBox5 " Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label runat="server" Font-Size="12px">分类:</asp:Label>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="0" Text="--" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="结构件"></asp:ListItem>
                            <asp:ListItem Value="2" Text="元器件"></asp:ListItem>
                            <asp:ListItem Value="3" Text="包装"></asp:ListItem>
                            <asp:ListItem Value="4" Text="电线和辅料"></asp:ListItem>
                            <asp:ListItem Value="5" Text="产成品"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    </td>
                    <td  rowspan="2">

                        <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" Width="80px" OnClick="btnSelect_Click" />&nbsp;&nbsp;
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btnAdd" runat="server" Text="生成申请单" CssClass="SmallButton2" Width="80px" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" Width="80px" OnClick="btnReturn_Click" />&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Font-Size="12px">供应商:</asp:Label>&nbsp;&nbsp;&nbsp;

                    </td>
                    <td>
                        <asp:TextBox ID="txtvendor" runat="server" CssClass="Supplier SmallTextBox5" Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    </td>
                    <td>
                        <asp:Label runat="server" Font-Size="12px">供应商名称:</asp:Label>&nbsp;&nbsp;&nbsp;

                    </td>
                    <td>

                        <asp:TextBox ID="txtVendorName" runat="server" CssClass=" SmallTextBox5" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>数据来源：</td>
                    <td>
                        <asp:DropDownList ID="ddlSou" runat="server">
                            <asp:ListItem Value="--" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="0" Text="申请" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="-10" Text="驳回"></asp:ListItem>
                            <asp:ListItem Value="-15" Text="关闭申请"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    </td>

                </tr>
<%--                <tr>
                    <td align="right">
                    修改单位导入: &nbsp;
                </td>
                     <td colspan="2" valign="top" style="height: 28px">
                    <input id="fileUpdateUM" style="width: 468px; height: 22px" type="file" name="fileUpdateUM"
                        runat="server" />
                </td>
                      <td style=" height: 28px;">
                    <asp:Button ID="btnImportUM" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImportUM_Click"  />
                </td>
                      <td>
                        <asp:Button ID="btnModel" runat="server"  Text="下载模板" CssClass ="SmallButton2" OnClick="btnModel_Click" />
                    </td>
                      <td></td>

                </tr>--%>
                <tr>
                     <td align="right">
                    修改需求规格导入: &nbsp;
                </td>
                <td colspan="2" valign="top" style="height: 28px">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style=" height: 28px;">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImport_Click" />
                </td>
                    <td>
                        <asp:Button ID="btnDownLode" runat="server"  Text="下载源数据" CssClass ="SmallButton2" OnClick="btnDownLode_Click"/>
                    </td>
                </tr>
                 </table>
            <table>
                <tr>
                    <td colspan="10">
                        <asp:GridView ID="gvDet" runat="server"  Width="1600px"
                            AutoGenerateColumns="False" AllowPaging="true" PageSize="100"
                            CssClass="GridViewStyle GridViewRebuild" DataKeyNames="id,NPartStatus" OnRowDataBound="gvDet_RowDataBound" OnRowCommand="gvDet_RowCommand">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                            <input id="chkAll" type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server"/>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="QAD号" DataField="NPartQAD" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="部件号" DataField="code" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>
                                 <asp:BoundField HeaderText="零件类型" DataField="NPartCMMT" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                 <asp:BoundField HeaderText="单位" DataField="NPartUM" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="需求规格" DataField="NPartFormate" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="供应商" DataField="NPartVendor" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="供应商名称" DataField="ad_name" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="申请类型" DataField="NPartLevel" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                 <asp:TemplateField >
                                      <ItemStyle Width="100px" HorizontalAlign="Center" />
                                       <HeaderStyle Width ="100px" HorizontalAlign="Center"   />
                                        <HeaderTemplate>操作</HeaderTemplate>
                                    <ItemTemplate>
                                            <asp:LinkButton ID="lkbtnDelete" runat="server" CommandName="lkbDelete" 
                                                CommandArgument='<%# Eval("id") %>' Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="创建人" DataField="userName" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="创建时间" DataField="createdDate"  DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField >
                                      <ItemStyle Width="100px" HorizontalAlign="Center" />
                                       <HeaderStyle Width ="100px" HorizontalAlign="Center"   />
                                        <HeaderTemplate>数据来源</HeaderTemplate>
                                    <ItemTemplate>
                                            <asp:Label ID="lbFrom" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="驳回原因" DataField="NPartRjectReason" >
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
