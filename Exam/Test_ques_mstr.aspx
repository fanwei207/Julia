<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_ques_mstr.aspx.cs" Inherits="Test_Test_ques_mstr" %>

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
        <div align="left">
            <table cellspacing="0" cellpadding="0" style="width: 1100px;">
                
                <tr>
                    
                    <td style="text-align: right; width: 50px;">标题</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_title" runat="server" Width="100px" CssClass="Param"></asp:TextBox></td>
                   
                    <td>类型

                    </td>
                    <td>
                            <asp:DropDownList ID="ddlStatu" runat="server" Width="108px" CssClass="Param" DataTextField="type_name" DataValueField ="type_id">
                            
                        </asp:DropDownList>
                       
                    </td>   
                      <td>所属模块

                    </td>
                    <td>
                              <asp:DropDownList ID="ddl_category" runat="server" DataTextField="category_name" DataValueField ="category_id"
                            Width="100px">
                        </asp:DropDownList>
                    </td>   
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                        <asp:Button ID="btnnew" runat="server" Text="新增" CssClass="SmallButton2" OnClick="btnnew_Click"/>   
                    </td>   
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="ques_type_id,ques_id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1100px">
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
                   
                    <asp:BoundField HeaderText="类型" DataField="type_name" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="题目" DataField="ques_title" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="答案" DataField="ques_answer" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>

                  <%--  <asp:BoundField HeaderText="开始时间" DataField="ques_startdate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="截止时间" DataField="ques_enddate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="报表">
                        <ItemStyle HorizontalAlign="Center" Width="40" Font-Underline="True" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDetail" runat="server" Text='Detail'
                                CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="40"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="取消"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="close" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>Cancel</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>


                  

                    <asp:BoundField HeaderText="创建人" DataField="ques_createname" ReadOnly="True"><%--9--%>
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="创建时间" DataField="ques_createdate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
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