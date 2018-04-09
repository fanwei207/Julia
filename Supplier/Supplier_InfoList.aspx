<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier_InfoList.aspx.cs" Inherits="Supplier_Supplier_InfoList" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
              <td>供应商</td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="SmallTextBox Supplier" Width="80px"></asp:TextBox>
                </td>
                <td>供应商名称</td>
                <td>
                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSelect_Click"/>
                </td>
                 <td>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="导出" OnClick="btnExport_Click"/>
                </td>
                    </tr>
                    </table>
         <asp:GridView ID="gvList" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="GridViewStyle" 
         PageSize="25" Width="2000px"
            DataKeyNames="ad_addr,supplierID" OnRowCommand="gvList_RowCommand" OnPageIndexChanging="gvList_PageIndexChanging"   >
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:TemplateField HeaderText="公司编号（供应商代码）" Visible="true">
                <ItemStyle HorizontalAlign="Center" width="120" Font-Underline="true"/>
                <ItemTemplate>
                    <asp:LinkButton ID="linkSupp" CssClass="no" runat="server" Text='<%# Eval("ad_addr") %>'
                     CommandName="view" CommandArgument='<%# Eval("ad_addr") %>'>

                    </asp:LinkButton>
                </ItemTemplate>
                <ControlStyle Font-Underline="true"/>
                <HeaderStyle HorizontalAlign="Center" Width="110"/>
            </asp:TemplateField>
             <%--<asp:BoundField HeaderText="公司编号（供应商代码）" DataField="ad_addr">
                <HeaderStyle Width="240px" />
                <ItemStyle Width="240px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>--%>
            <asp:BoundField HeaderText="供应商中文名" DataField="supplierName">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商英文名" DataField="supplierEnglish">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商中文地址" DataField="suppChineseAddress">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
          <asp:BoundField HeaderText="供应商英文地址" DataField="suppEnglishAddress">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:BoundField HeaderText="供应商存在域" DataField="ad_domain">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系人1" DataField="linkName">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="职务1" DataField="linkRole">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系电话1" DataField="linkPhone">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系手机1" DataField="linkMobilephone">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系邮箱1" DataField="linkEmail">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:BoundField HeaderText="联系人2" DataField="linkName1">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="职务2" DataField="linkRole1">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系电话2" DataField="linkPhone1">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系手机2" DataField="linkMobilephone1">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系邮箱2" DataField="linkEmail1">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:BoundField HeaderText="联系人3" DataField="linkName2">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="职务3" DataField="linkRole2">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系电话3" DataField="linkPhone2">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系手机3" DataField="linkMobilePhone2">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系邮箱3" DataField="linkEmail2">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
                        <asp:BoundField HeaderText="币种" DataField="vd_curr">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
               <asp:BoundField HeaderText="税率" DataField="ad_taxc">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
                        <asp:BoundField HeaderText="账期" DataField="vd_cr_terms">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
                        <asp:BoundField HeaderText="大类区分" DataField="BroadHeading">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
                        <asp:BoundField HeaderText="细部区分" DataField="SubDivision">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
                       <%-- <asp:BoundField HeaderText="子物料" DataField="SubMaterial">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>--%>
                        <asp:BoundField HeaderText="验厂等级" DataField="FactoryInspection">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="经营类型" DataField="SupplieType">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="申请人" DataField="applyName">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="申请部门" DataField="applyDepartment">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
        <%--    <asp:BoundField HeaderText="申请日期" DataField="applyDate">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>--%>
             <asp:BoundField HeaderText="申请公司" DataField="domain">
                <HeaderStyle Width="240px" />
                <ItemStyle Width="240px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="代码生成日期" DataField="supplierCodeCreateDate">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="备注" DataField="Remark">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>

             <asp:TemplateField HeaderText="修改">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbModifiy" runat="server" CommandName="lkbModifiy" CommandArgument='<%# Eval("ad_addr") %>'
                                    Text="修改"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

        </Columns>
        </asp:GridView>
    </div>
    </form>
     <script type="text/javascript">
         <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
