<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_Skills.aspx.cs" Inherits="hr_Train_Skills" %>

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
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $("#HR_SkillsAdd").click(function () {
                var _formid = $("#tb_FormID").html();
                var _src = "../HR/hr_Train_SkillsAdd.aspx?appno=" + _formid + "&check=false";
                $.window("人员维护", 1300, 600, _src);
            })
            $("#HR_SkillsTypeAdd").click(function () {
                var _formid = $("#tb_FormID").html();
                var _src = "../HR/hr_Train_SkillsTypeAdd.aspx?appno=" + _formid + "&check=false";
                $.window("人员维护", 1300, 600, _src);
            })
        })
    </script>
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
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="技能类别:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_SkillType" runat="server" Width="100px" AutoPostBack="true"
                                onselectedindexchanged="ddl_SkillType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_find" runat="server" Text="查    询"
                                OnClick="btn_find_Click" />
                                 &nbsp;&nbsp;
                                <asp:Button ID="btn_Check" runat="server" Text="确     认"
                                    OnClick="btn_Check_Click" />
                                &nbsp;&nbsp;
                <%--                <asp:Button ID="btn_back" runat="server" Text="返      回"
                                    OnClick="btn_back_Click" />--%>
                                &nbsp;&nbsp;
                                <u id="HR_SkillsTypeAdd">新增技能类别信息</u>
                                &nbsp;&nbsp;
                                <u id="HR_SkillsAdd">新增技能信息</u>
                        </td>
                        <td>

                        </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="培训技能:"></asp:Label>
                    </td>
                    <td>                    
                        <asp:CheckBoxList ID="CBList" runat="server" RepeatColumns='<%# repeateColumn %>' RepeatDirection="Horizontal" Height="21px" Width="100%">
                        </asp:CheckBoxList>
                        <asp:CheckBox ID="cb_Other" runat="server" onclick="checkShow()" Text="其他" ToolTip="其他"/>
                        <asp:TextBox ID="tb_Other" runat="server" Width="120px" Visible = "false" Style="display: none"></asp:TextBox>
                        <span style="color: #CC3300; display: none" runat="server" Visible = "false" id="sp_warn">
                        <asp:Label ID="Label10" Visible = "false" runat="server" Text="（多個課程名請用'/';隔開，如安全講座/消防常識）" >
                        </asp:Label></span>
                        <%--<span style="color:Red" >*</span>--%>
                        <asp:TextBox ID="tb_coursename" runat="server"  Width="360px"></asp:TextBox>
                        <span style="color:Red">*</span>
                    </td>
                </tr>

                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                            <td align="center" colspan="8">
                  

                            </td>                                      
                    </tr>
                </table>
            </div>
        </div>
        <input id="hid_formid" type="hidden" runat="server"/>
        </form>

    </div>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
