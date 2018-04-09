<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_InterviewList.aspx.cs" Inherits="HR_app_InterviewList" %>

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
                <%--<td>学历</td>
                <td><asp:DropDownList ID="ddlDegree" runat="server" Width="100px" CssClass="smalltextbox">
                    <asp:ListItem Text="--学历--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="高中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="大专" Value="2"></asp:ListItem>
                    <asp:ListItem Text="本科" Value="3"></asp:ListItem>
                    <asp:ListItem Text="硕士" Value="4"></asp:ListItem>
                    <asp:ListItem Text="博士" Value="4"></asp:ListItem>
                    </asp:DropDownList></td>--%>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlCompany" runat="server"  Width="80px" 
                    DataTextField="plantCode" DataValueField="plantID" 
                    onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>部门</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server"  Width="80px" 
                    DataTextField="name" DataValueField="departmentID" 
                    AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>面试时间</td>
                <td><asp:TextBox ID="txtInterviewDate" runat="server"  CssClass="SmallTextBox Date" Width="100px"></asp:TextBox></td>
                <td>状态</td>
                <td><asp:DropDownList ID="ddlStatus" runat="server" Width="100px" CssClass="smalltextbox">
                    <asp:ListItem Text="--状态--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="同意面试" Value="1"></asp:ListItem>
                    <asp:ListItem Text="同意聘用" Value="3"></asp:ListItem>
                    <asp:ListItem Text="拒绝聘用" Value="4"></asp:ListItem>
                    <asp:ListItem Text="同意录用" Value="5"></asp:ListItem>
                    <asp:ListItem Text="拒绝录用" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询"  CssClass="SmallButton2" onclick="btnQuery_Click"/>
                </td>
            </tr>
        </table>

        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
        DataKeyNames="plantcode,isSendEmployEmail,interviewTime,fpath,status,examinationTime,appid,id,email,username,sex,company,department,process"
        OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:BoundField HeaderText="姓名" DataField="username">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="籍贯" DataField="place">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="学历" DataField="education">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="毕业院校" DataField="graduateSchool">
                <HeaderStyle Width="150px" />
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="专业" DataField="professional">
                <HeaderStyle Width="150px" />
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="性别" DataField="sex">
                <HeaderStyle Width="30px" />
                <ItemStyle Width="30px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="年龄" DataField="ages">
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
            </asp:TemplateField>
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
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="true"></asp:Literal>
    </script>
    </form>
</body>
</html>
