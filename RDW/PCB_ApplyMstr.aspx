<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCB_ApplyMstr.aspx.cs" Inherits="RDW_PCB_ApplyMstr" %>

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
    <form id="form1" runat="server" align="center"> 
    <div>
          <asp:DropDownList ID="ddlDomain" runat="server">
             <asp:ListItem Value="--" Text="--" Selected="True"></asp:ListItem>
              <asp:ListItem Value="SZX" Text="SZX"></asp:ListItem>
              <asp:ListItem Value="ZQL" Text="ZQL"></asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        <label>项目号</label>&nbsp;&nbsp;
        <asp:TextBox ID ="txtProjectNo" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <label>PCB品名</label>&nbsp;&nbsp;
        <asp:TextBox ID ="txtProductName" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <label>PCB编号</label>&nbsp;&nbsp;
        <asp:TextBox ID ="txtPCBNo" runat="server" CssClass="SmallTextBox5" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <label>状态</label>&nbsp;&nbsp;
        <asp:DropDownList ID="ddlStatus" runat="server">
              <asp:ListItem Value="全部" Text="全部"></asp:ListItem>
              <asp:ListItem Value="已创建" Text="已创建"></asp:ListItem>
              <asp:ListItem Value="已提交" Text="已提交"></asp:ListItem>
              <asp:ListItem Value="已删除" Text="已关闭"></asp:ListItem>
             <asp:ListItem Value="已驳回" Text="已驳回"></asp:ListItem>
             <asp:ListItem Value="已生成打样单" Text="已生成打样单"></asp:ListItem>

        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
        
      
        <asp:Button ID="btnSelect" runat="server"  Text="查询" OnClick="btnSelect_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server"  Text="新增" OnClick="benAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnReturn" runat="server"  Text="返回" OnClick="btnReturn_Click" Visible="false" />
    </div>
     <div>
         <asp:GridView ID="gvApplyList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                        AllowPaging="true" PageSize="25" 
                        DataKeyNames="PCB_ID,PCB_LAYOUTStatus"
                         Width="1800px" OnPageIndexChanging="gvApplyList_PageIndexChanging" OnRowCommand="gvApplyList_RowCommand" OnRowDataBound="gvApplyList_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                         <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="707px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="9"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="PCB品名" DataField="PCB_ProductName" >
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="PCB编号" DataField="PCB_No" >
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="项目编号" DataField="PCB_ProjectCode">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="状态" DataField="PCB_LAYOUTStatus">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="创建人" DataField="createdName">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            
                            <asp:BoundField HeaderText="创建日期" DataField="createdDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                              <asp:BoundField HeaderText="交付日期" DataField="PCB_SampleDeliveryDate" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnList" CommandName="lkbtnList" CommandArgument=' <%# Eval("PCB_ID") %>'
                                        Text="明细" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                              
                            <asp:TemplateField HeaderText="关闭" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="lkbtnDelete" CommandArgument=' <%# Eval("PCB_ID") %>'
                                        Text="关闭" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="样品数量" DataField="PCB_Num">
                                <HeaderStyle Width="50px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="外形尺寸	" DataField="PCB_Size">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="PCB厚度" DataField="PCB_Thickness">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField> 
                            <asp:BoundField HeaderText="层数	" DataField="PCB_Ply">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="PCB材质" DataField="PCB_Material">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="PCB处理	" DataField="PCB_Machining">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="制程要求" DataField="PCB_Requirment">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="防焊漆" DataField="PCB_SolderResistPaint">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="LAYOUT依据" DataField="PCB_LAYontBasis">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="丝印颜色	" DataField="PCB_ScreenPrintingColour">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="铜箔厚度" DataField="PCB_CopperFoil">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="新器件封装	" DataField="PCB_Package">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="安规" DataField="PCB_Safety">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>

                            <asp:BoundField HeaderText="备注	" DataField="PCB_Remark">
                                <HeaderStyle Width="300px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
    
                        </Columns>
                    </asp:GridView>

     </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
