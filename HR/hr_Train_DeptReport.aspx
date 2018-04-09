<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_DeptReport.aspx.cs" Inherits="hr_Train_DeptReport" %>

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
                        <td colspan="6" align="center">
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp
                            <asp:Button ID="btn_save" runat="server" Text="人员维护" onclick="btn_save_Click" />
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp
                            <asp:Button ID="btn_check" runat="server" Text="实到人员确认" OnClick="btn_check_Click"/>
                            <span style="color: #CC3300">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp
                <%--            <asp:Button ID="Button1" runat="server" Text="返      回"
                                OnClick="btn_back_Click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="style1">    
                        <asp:Label ID="Label34" runat="server" ForeColor="Red" Text="提示:提交的上课人数不能大于提报或经HR确认过的人数." ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <div style="height: 368px; width: 650px; overflow: auto; float: left;" align="left" class=" divCSS"> 
                                <asp:Label ID="Label32" runat="server" Text="员工选择"></asp:Label>:<br />
                                <br />
                                <asp:GridView ID="dgv_Left" runat="server" Width="633px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="选择">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cb_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="cb_ALL_CheckedChanged"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server"/>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="loginName" HeaderText="员工工号">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="userName" HeaderText="员工姓名">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="department" HeaderText="所在部门">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="departmentID" HeaderText="部门编号">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
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
        </div>
            <input id="hid_formid" type="hidden" runat="server" />
        </form>
    </div>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".HR_uAdd").click(function () {
                var _FormID = $(this).attr("hid_formid");
                var _src = "../HR/hr_Train_DeptAdd.aspx?appno=" + _FormID + "&check=false";
                $.window("人员维护", 1300, 600, _src);
            })

        })
    </script>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
