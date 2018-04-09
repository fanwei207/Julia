<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_ImportTalentList.aspx.cs" Inherits="HR_app_ImportTalentList" %>

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
        function doSelect() {
            var dom = document.all;
            var el = event.srcElement;
            if (el.id.indexOf("chkAll") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("chkUsers") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
    <table>
        <tr>
            <td><asp:TextBox ID="txtCompany" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtProcess" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtPlantcode" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtDepartmentID" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>性别</td>
            <td><asp:DropDownList ID="ddlSex" runat="server" Width="60px" CssClass="smalltextbox">
                    <asp:ListItem Text="-性别-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="男" Value="1"></asp:ListItem>
                    <asp:ListItem Text="女" Value="2"></asp:ListItem>
                    </asp:DropDownList></td>
            <td>年龄</td>
            <td>
                <asp:TextBox ID="txtAge1" runat="server" Width="50px"></asp:TextBox>-
                <asp:TextBox ID="txtAge2" runat="server" Width="50px"></asp:TextBox></td>
            <td>学历</td>
            <td>
                <asp:DropDownList ID="ddlEdu" runat="server" DataTextField="systemCodeName" DataValueField ="systemCodeID"  Width="70px"></asp:DropDownList></td>
            <td>人才渠道</td>
            <td><asp:DropDownList ID="ddlTalentList" runat="server" Width="90px" CssClass="smalltextbox">
                    <asp:ListItem Text="-人才来源-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="人才搜集" Value="1"></asp:ListItem>
                    <asp:ListItem Text="人才储备" Value="2"></asp:ListItem>
                    </asp:DropDownList></td>
            <td>
                <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="SmallButton2" 
                    onclick="btnImport_Click" /></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="查询" CssClass="SmallButton2" 
                    onclick="Button1_Click" /></td>
            <td>
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" 
                    onclick="btnBack_Click" /></td>
        </tr>
    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"  CssClass="GridViewStyle AutoPageSize" 
        OnRowCommand="gv_RowCommand" 
        DataKeyNames="id,userName,sex,sexID,education,educationID,graduateSchool,professional,place,placeID,status,fname,fpath,email,birthday">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkUsers" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="chkAll" runat="server" ForeColor="Black" AutoPostBack="False" onclick="doSelect()" />
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="姓名" DataField="userName">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="性别" DataField="sex">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="年龄" DataField="age">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="学历" DataField="education">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="毕业院校" DataField="graduateSchool">
                <HeaderStyle Width="150px" />
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="专业" DataField="professional">
                <HeaderStyle Width="150px" />
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="籍贯" DataField="place">
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
            <asp:BoundField HeaderText="人才来源" DataField="status">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="true"></asp:Literal>
    </script>
    </form>
</body>
</html>
