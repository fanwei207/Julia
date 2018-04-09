<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_View.aspx.cs" Inherits="plan_PCD_View" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
            <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1100px;">
                <tr class="main_top">
                    <td class="main_left"></td>
                    <td style="height: 1px">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="超期" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    类型
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Text="--" Value="--"></asp:ListItem>
                            <asp:ListItem Text="订单" Value="订单"></asp:ListItem>
                            <asp:ListItem Text="备货" Value="备货"></asp:ListItem>
                            <asp:ListItem Text="样品" Value="样品"></asp:ListItem>
                        </asp:DropDownList>
                          
                        订单号：
                    <asp:TextBox ID="txtpoNbr" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                         行号：
                    <asp:TextBox ID="txtLine" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                           客户号：
                    <asp:TextBox ID="txtcode" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                        客户物料：
                        <asp:TextBox
                            ID="txtPart" runat="server" CssClass="smalltextbox Param" Width="122px"></asp:TextBox>
                       物料号：
                        <asp:TextBox
                            ID="txtQAD" runat="server" CssClass="smalltextbox Param" Width="122px"></asp:TextBox>
                        
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                            Text="查询" Width="50px" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" OnClick="btnExcel_Click"
                            Text="Excel" Width="50px" />
                    </td>
                    <td class="main_right"></td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                 PageSize="30"
                CssClass="GridViewStyle GridViewRebuild" DataKeyNames="poNbr,poLine,wo_nbr,wo_lot" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowCommand="gvlist_RowCommand" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                   <asp:BoundField HeaderText="类型" DataField="type">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="订单号" DataField="poNbr">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="行号" DataField="poLine">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                   
                    <asp:BoundField HeaderText="客户号" DataField="cusCode">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="客户名称" DataField="CustName">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="客户物料" DataField="partNbr">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QAD号" DataField="part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="销售单" DataField="sod_nbr">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="工单号" DataField="wo_nbr">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ID" DataField="wo_lot">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="订单日期" DataField="PoRecDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="客户PCD" DataField="planDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="运算PCD" DataField="actPlanDate" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="塑料件，透镜" DataField="jinshujian" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="金属件，其他" DataField="suliaojian" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="散热器" DataField="sanreqi"  DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="结构件，灯罩" DataField="jiegoujian" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="包装" DataField="baozhuang" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="COB" DataField="COB" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText ="线路板">
                        <ItemTemplate>
                            <asp:LinkButton ID="ltnxianluban" Text='<%# Bind("xianluban") %>' ForeColor="Blue" Font-Size="12px" runat="server"
                                CommandName="xianluban" />
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText ="芯片板">
                        <ItemTemplate>
                            <asp:LinkButton ID="ltnxinpianban" Text='<%# Bind("xinpianban") %>' ForeColor="Blue" Font-Size="12px" runat="server"
                                CommandName="xinpianban" />
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>