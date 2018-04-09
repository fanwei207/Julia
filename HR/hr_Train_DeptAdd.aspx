<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_DeptAdd.aspx.cs" Inherits="hr_Train_DeptAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
            //  var $=document.getElementById;
            //控制只能輸入數字
            function numberkeypress() 
            {
                if ((event.keyCode < 48 || event.keyCode > 57)) 
                {
                    event.keyCode = 0;
                }
            }
    </script>
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form runat="server">
    <div class="MainDiv">
        <br />
        <br />
        <div class="MainTable" align="center">
<%--            <h2>
                <asp:Label ID="Label1" runat="server" Text="培训提报"></asp:Label>
                培训申请
            </h2>--%>
        </div>
        <div class="MainTable">
            <%-- <form id="Form1" method="post" runat="server">--%>
            <div class="AppMainContent">
                <table class="TB_AppPage" width="850">
                    <tr>
                        <td colspan="8">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label30" runat="server" Text="所在部门:" ></asp:Label>
                            <asp:DropDownList ID="ddl_deptbyperson" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddl_deptbyperson_SelectedIndexChanged" >
                            </asp:DropDownList>
                            &nbsp; 
                            <asp:Label ID="Label31" runat="server" Text="员工工号:"></asp:Label>
                            <asp:TextBox ID="tb_EmpNo" runat="server" Width="63px"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btn_Query" runat="server" Text="查  询" OnClick="btn_Query_Click"/>
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp
                            <asp:Button ID="btn_save" runat="server" Text="保  存" OnClick="btn_save_Click"/>
                            <span style="color: #CC3300">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp
                            <asp:Button ID="Button1" runat="server" Text="返      回"
                                OnClick="btn_back_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="style1">    
                        <asp:Label ID="Label34" runat="server" ForeColor="Red" Text="提示:提交的上课人数不能大于提报或经HR确认过的人数." ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                     <td colspan="6">
                        <div align="left" style="height: 14px">
                            <asp:Label ID="Label33" runat="server" Text="选中的员工:"></asp:Label>
                        </div>
                        <br />
                        <div style="width: 565px; height: 342px; overflow: auto">
                            <asp:GridView ID="dgv_Right" runat="server" Width="533px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cb_ChoiceAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_ChoiceAll_CheckedChanged"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="loginName" HeaderText="工号"/>
                                <asp:BoundField DataField="userName" HeaderText="员工姓名"/>
                                <asp:BoundField DataField="department" HeaderText="所在部门"/>
                                <asp:BoundField DataField="departmentID" HeaderText="部门编号"/>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         </asp:GridView>
                       </div>
                    </td>
                   </tr

                    <tr>
                        <td colspan="8" style="color: #CC6600">
                            <asp:Label ID="Label20" runat="server" Text="提示：培训提报需在培训当天提报，且在开课前至少提前一个小时提报，未提报就开课，HR将不予认可！"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
<%--            <tr>
                <div id="Div1" runat="server" align="center">
                    <td align="center" colspan="8">
                        <asp:Button ID="btn_UpRecord" runat="server" Visible="false" Text="录入培训详细信息"/>
                    </td>
                </div>
            </tr>--%>
            
        </div>
            <input id="hid_formid" type="hidden" runat="server" />
        </form>
    </div>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
