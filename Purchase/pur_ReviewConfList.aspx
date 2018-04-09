<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ReviewConfList.aspx.cs" Inherits="plan_so_ReviewConf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">    
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td>供应商代码：</td>
                <td>
                    <asp:TextBox ID="txtSupp" runat="server" Width="100px" CssClass="SmallTextBox Supplier"></asp:TextBox></td>
                <td><asp:Button ID="btnReach" runat="server" Text="查询"  CssClass="SmallButton2" 
                        onclick="btnReach_Click"/></td>
                <td><asp:Button ID="btnAdd" runat="server" Text="添加"  CssClass="SmallButton2" 
                        onclick="btnAdd_Click"/></td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
        PageSize="20" OnPageIndexChanging="gv_PageIndexChanging"
        DataKeyNames="id,ad_addr,node1,name1,node2,name2,Node_Id3,Node_Name,isShow,money,ad_name"
        OnRowDataBound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" OnRowCommand="gv_RowCommand" AllowPaging="True" >
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="供应商代码" DataField="ad_addr">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商名称" DataField="ad_name">
                <HeaderStyle Width="200px" />
                <ItemStyle Width="200px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="节点1" DataField="name1">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="节点2" DataField="name2">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="节点3" DataField="Node_Name">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="是否显示">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Bind("isShow") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                </asp:TemplateField>
            <%--<asp:BoundField HeaderText="是否显示" DataField="isShow">
                <HeaderStyle Width="30px" />
                <ItemStyle Width="30px" HorizontalAlign="Center" />
            </asp:BoundField>--%>
            <asp:BoundField HeaderText="金额" DataField="money">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="审核配置">
                <ItemTemplate>
                    <asp:LinkButton ID="linkConf" runat="server" CommandName="conf" Font-Bold="False"
                        Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>配置</u></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <%--<asp:BoundField HeaderText="年龄" DataField="ages">
                <HeaderStyle Width="30px" />
                <ItemStyle Width="30px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="简历">
                <ItemTemplate>
                    <asp:LinkButton ID="linkDownLoad" runat="server" CommandName="DownLoad" Font-Bold="False"
                        Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>预览</u></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="应聘岗位" DataField="process">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="状态" DataField="status">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="面试通知">
                <ItemTemplate>
                    <asp:LinkButton ID="linkInterview" runat="server" CommandName="interview"><u>面试</u></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Width="70px"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="录用通知">
                <ItemTemplate>
                    <asp:LinkButton ID="linkEmail" runat="server" CommandName="Email"><u>邮件</u></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Width="70px"/>
            </asp:TemplateField>
            <asp:BoundField HeaderText="面试时间" DataField="interviewTime">
                <HeaderStyle Width="110px" />
                <ItemStyle Width="110px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="应试时间" DataField="examinationTime">
                <HeaderStyle Width="70px" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="人员入职">
                <ItemTemplate>
                    <asp:LinkButton ID="linkEmploy" runat="server" CommandName="employ1"><u>入库建档</u></asp:LinkButton>                       
                </ItemTemplate>
                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>--%>
            <%--<asp:TemplateField HeaderText="人员录用test">
                <ItemTemplate>
                    <asp:LinkButton ID="linkEmploy1" runat="server" CommandName="employ"><u>录用</u></asp:LinkButton>                       
                </ItemTemplate>
                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>--%>
        </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
