<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_exam_view.aspx.cs" Inherits="Test_Test_exam_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
 
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table cellspacing="0" cellpadding="0" style="width: 1100px;">
                
                <tr>
                    
                    <td style="text-align: right; width: 50px;">试卷</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtname" runat="server" Width="100px" CssClass="Param"></asp:TextBox></td>
                    <td style="width: 80px;">开始时间</td>
                    <td>
                        <asp:TextBox ID="txtstartdate" runat="server" Width="100px" CssClass="Param Date"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">结束时间</td>
                    <td>
                        <asp:TextBox ID="txtenddate" runat="server" Width="100px" CssClass="Param Date"></asp:TextBox></td>

                  
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" Width="34px" />
                        <asp:Button ID="btnnew" runat="server" Text="新增" CssClass="SmallButton3" OnClick="btnnew_Click"/>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="exam_id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1000px">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Width="100%" HorizontalAlign="center">无数据</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                 
                    <asp:BoundField HeaderText="名称" DataField="exam_name" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                   
                    <asp:BoundField HeaderText="开始时间" DataField="exam_startdate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="结束时间" DataField="exam_enddate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="报表">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="考试人员">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkperson" runat="server" Text='Detail'
                                CommandName="person"></asp:LinkButton>
                             <asp:LinkButton ID="linkADD" runat="server" Text='添加'
                                CommandName="add"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>

                   <%-- <asp:TemplateField HeaderText="取消">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>--%>

                    <asp:BoundField HeaderText="考试时间" DataField="exam_alltime" ReadOnly="True"><%--11--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                   

                    <asp:BoundField HeaderText="创建人" DataField="createname" ReadOnly="True"><%--9--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建时间" DataField="createtime" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>