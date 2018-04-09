<%@ Page Language="C#" AutoEventWireup="true" CodeFile="perf_punish.aspx.cs" Inherits="Performance_perf_punish" %>

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
                    
                    <td style="text-align: right; width: 50px;">工号</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_NO" runat="server" Width="100px" CssClass="Param"></asp:TextBox>

                    </td>
                   
                    <td>姓名

                    </td>
                    <td>
                          <asp:TextBox ID="txt_name" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                       
                    </td>   
                      <td>日期

                    </td>
                    <td>
                              <asp:TextBox ID="txt_stret" runat="server" Width="100px" CssClass="Param Date"></asp:TextBox>-
                         <asp:TextBox ID="txt_end" runat="server" Width="100px" CssClass="Param Date"></asp:TextBox>
                    </td>   
                     <td>状态

                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server">
                            <asp:ListItem Value="2">--</asp:ListItem>
                              <asp:ListItem Value="0" Selected="True">未处理</asp:ListItem>
                              <asp:ListItem Value="1">已处理</asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                    <td rowspan="2" style="text-align:left;width:200px;" >
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                       <asp:Button ID="btnnew" runat="server" Text="新增" CssClass="SmallButton2" OnClick="btnnew_Click"/>
                    </td>   
                </tr>
            </table>
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                OnRowCommand="gv_RowCommand" DataKeyNames="doneby,donename,pref_id" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" Width="1100px">
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
                   
                    <asp:BoundField HeaderText="类型" DataField="perf_type" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="原因" DataField="perf_caues" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="处分" DataField="perf_duty" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>

                   
                     <asp:BoundField HeaderText="姓名" DataField="duty_uname" ReadOnly="True">
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
                 

                    <asp:TemplateField HeaderText="确认处理"><%--17--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkClose" CssClass="close" runat="server" CommandName="done" Font-Bold="False"
                                Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>处理</u></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>


                  

                    <asp:BoundField HeaderText="创建时间" DataField="createdate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
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