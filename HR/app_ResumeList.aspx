<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_ResumeList.aspx.cs" Inherits="HR_app_ResumeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $('#checkPower').click(function () {
                var _src = "../HR/app_CheckReviewPower1.aspx";
                $.window("拟录用人员审批权限表", 650, 400, _src);
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr style="width:760px;">
                <td style="width:100%;">公司：<asp:TextBox ID="txtCop" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtPlantcode" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;部门：<asp:TextBox ID="txtDepart" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtDepartID" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;岗位：<asp:TextBox ID="txtProc" runat="server"  Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtProcID" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;<asp:Button ID="butBack" runat="server"  Text="返回" CssClass="SmallButton2" onclick="butBack_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="butUpResume" runat="server" Text="上传简历" CssClass="SmallButton2" onclick="butUpResume_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnImport" runat="server" Text="人才库导入" 
                        CssClass="SmallButton2" onclick="btnImport_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="butSendEmail" runat="server" Text="发送邮件" CssClass="SmallButton2" onclick="butSendEmail_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;<u id="checkPower" style="color: blue; cursor: pointer;">查看拟录用人员审批权限表</u></td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" CssClass="GridViewStyle AutoPageSize" AutoGenerateColumns="False"
        OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" DataKeyNames="fpath,id,status,userName,examinationTime">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
                <asp:BoundField HeaderText="姓名" DataField="userName">
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
                <asp:BoundField HeaderText="状态" DataField="status">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="面试">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkResumeYse" runat="server" CommandName="ResumeYse"><u>同意</u></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="linkResumeNo" runat="server" CommandName="ResumeNo"><u>否决</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部门意见">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEmploy" runat="server" CommandName="Employ"><u>聘用</u></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="linkUnEmploy" runat="server" CommandName="UnEmploy"><u>弃用</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px"/>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="公司录用">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkInto" runat="server" CommandName="GetInto"><u>同意</u></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="linkOutTo" runat="server" CommandName="GetOutTo"><u>拒绝</u></asp:LinkButton>                    
                    </ItemTemplate>
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="人才储备">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkReserve" runat="server" CommandName="Reserve"><u>储备</u></asp:LinkButton>                       
                    </ItemTemplate>
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="true"></asp:Literal>
    </script>
    <%--<center><p><u id="checkPower" style="color: blue; cursor: pointer;">查看拟录用人员审批权限表</u></p></center>--%>
    </form>
</body>
</html>
