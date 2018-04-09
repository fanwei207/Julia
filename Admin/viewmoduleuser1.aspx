<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewmoduleuser1.aspx.cs" Inherits="Admin_viewmoduleuser1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script  language="JavaScript" type="text/javascript">
        $(function () {
            $("body", parent.parent.document).find("#divLoading").hide();
        })
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
    <style type="text/css">
        #d1{ float:left; width:250px; height:550px; border:1px solid #99CCFF; overflow:auto; position:relative; margin-left:30px;}
        #d3{ float:left; width:450px; height:550px; border:1px solid #99CCFF; overflow:auto; position:relative; margin-left:10px;}
        #treeT{border:0px solid #b8d2f0;text-align:center}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="d1">
        <asp:TreeView ID="treeT" runat="server" ExpandDepth="0" SelectAction="Expand" CssClass="GridViewStyle"
            onselectednodechanged="treeT_SelectedNodeChanged" Font-Size="Large" EnableClientScript="False"></asp:TreeView>
    </div>
        <div id="d3" align="center">        
        <table>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="菜单:"></asp:Label></td>
                <td><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:Label ID="Label4" runat="server" Text="菜单号:"></asp:Label></td>
                <td><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:Button ID="Button1" runat="server" Text="回收"  CssClass="SmallButton3" 
                        onclick="Button1_Click1" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
        DataKeyNames="plantCode,moduleID,userID,userName,userNo,departmentID,name,isleave"
        onrowdeleting="gv_RowDeleting" OnRowDataBound="gv_RowDataBound"
         CssClass="GridViewStyle GridViewReBuild" >
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
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
            <asp:BoundField DataField="userName" HeaderText="名字">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="plantCode" HeaderText="公司">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="部门">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>回收 </u>">
                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <asp:BoundField DataField="userNo" HeaderText="工号">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px"  Height="25px" HorizontalAlign="Center" />
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
