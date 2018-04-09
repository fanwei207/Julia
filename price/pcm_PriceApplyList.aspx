 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_PriceApplyList.aspx.cs"
    Inherits="price_pcm_PriceApplyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <h2>
    </h2>
    <div align="center">
        <table style="width: 750px">
            <tr style="height: 25px;">
                <td style="width: 650px">
                    <table style="width: 900px">
                        <tr>
                            <td>
                                QAD： <asp:TextBox ID="txtQAD" runat="server"   CssClass="SmallTextBox" Width="100px"></asp:TextBox>&nbsp; 申请人：
                                &nbsp;
                                <asp:TextBox ID="txtApplyBy" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>

                            </td>
                            <td rowspan="2">
                            类型：
                           
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="-1" Text="全部" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="结构件"></asp:ListItem>
                            <asp:ListItem Value="2" Text="元器件"></asp:ListItem>
                            <asp:ListItem Value="3" Text="包装"></asp:ListItem>
                            <asp:ListItem Value="4" Text="电线和辅料"></asp:ListItem>
                            <asp:ListItem Value="5" Text="产成品"></asp:ListItem>
                        </asp:DropDownList>
                &nbsp;&nbsp;
                </td>
                          <td rowspan="2"> 状态：  &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="0" Text="未提交" ></asp:ListItem>
                                    <asp:ListItem Value="1"   Text="已提交"></asp:ListItem>
                                    <asp:ListItem Value="-1"  Text="驳回"></asp:ListItem>
                                    <asp:ListItem Value="2"  Text="已通过"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="全部" ></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                                <td  rowspan="2" align="right" style=" width:150px">
                    <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSelect_Click" Width="45px" />&nbsp;&nbsp;<asp:Button
                        ID="btnAddApply" runat="server" Text="申请修改报价" CssClass="SmallButton2" OnClick="btnAddApply_Click" Width="82px" />
                </td>
                            
                        <td rowspan="2" style=" width:120px">
                            <asp:Button ID="btnAddApplyOut" runat="server"  Text ="申请委外修改报价" CssClass =" SmallButton2" Width="100px" OnClick="btnAddApplyOut_Click" />
                        </td>
                 
                        
                                </tr>
                        <tr>
                            <td>
                                申请日期：&nbsp; &nbsp;
                                <asp:TextBox ID="txtStarDate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                                ---
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        
                      
                    </table>
                </td>
              
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvApplyList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                        AllowPaging="true" PageSize="25" OnPageIndexChanging="gvApplyList_PageIndexChanging"
                        DataKeyNames="PQID,Status,ApplyBy,Company,ApplyDate,AppliByID,isOut " OnRowCommand="gvApplyList_RowCommand"
                        OnRowDataBound="gvApplyList_RowDataBound">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                         <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="707px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="申请号" Width="67px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请人ID" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="" Width="50px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="状态" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="删除" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="提交时间" Width="100px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="9"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="申请号" DataField="PQID" Visible="true">
                                <HeaderStyle Width="67px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="公司" DataField="Company">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请日期" DataField="ApplyDate">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请人" DataField="ApplyBy">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请部门" DataField="departmentName">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="零件类型" DataField="applyType">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle  HorizontalAlign="Center"/>
                            </asp:BoundField>
                              <asp:BoundField HeaderText="申请人ID" DataField="AppliByID" Visible="false">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnList" CommandName="lkbtnList" CommandArgument=' <%# Eval("PQID") %>'
                                        Text="明细" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="状态" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px"  />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                  <asp:Label ID="lbStatus" runat="server" text=' <%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="lkbtnDelete" CommandArgument=' <%# Eval("PQID") %>'
                                        Text="删除" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField HeaderText="提交时间" DataField="submitDate">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                              <asp:TemplateField HeaderText="是否委外" Visible="true">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                  <asp:Label  id="lbIsOut" runat ="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%--添加驳回按钮--%>
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
