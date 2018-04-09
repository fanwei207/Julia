<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Trainapp.aspx.cs" Inherits="hr_Train_app" %>

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

            $("#HR_PersonsInfo").click(function () {
                var _formid = $("#tb_FormID").html();
                var _src = "../HR/hr_Train_DeptReport.aspx?appno=" + _formid + "&check=false";
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
                        <td width="80px">
                            <asp:Label ID="Label2" runat="server" Text="申请单号:"></asp:Label>
                        </td>
                        <td width="120px">
                            <asp:Label ID="tb_FormID" runat="server" Text="申请后会自动产生"></asp:Label>
                        </td>
                        <td width="65px">
                            <asp:Label ID="Label3" runat="server" Text="部门名称:"></asp:Label>
                        </td>
                        <td width="100px">
                            <asp:Label ID="tb_DeptName" runat="server" BorderStyle="None" Width="90px" 
                                Height="16px"></asp:Label>
                        </td>
                        <td width="90px">
                            <asp:Label ID="Label4" runat="server" Text="成本中心:"></asp:Label>
                        </td>
                        <td width="115px">
                            <asp:Label ID="tb_Code" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td width="90px">
                            <asp:Label ID="lt_EntryDate" runat="server" Text="填表日期:"></asp:Label>
                        </td>
                        <td style="width: 160px">
                            <asp:Label ID="tb_AgentDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="申请人工号:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tb_AppEno" runat="server" ReadOnly="True" BorderStyle="None"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="申请人:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tb_AppName" runat="server" ReadOnly="True" BorderStyle="None"></asp:Label>
                        </td>
                        <td>
                            公 司:
                        </td>
                        <td>
                            <asp:Label ID="lb_CompanyCodeName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="培训日期:"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tb_Date" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                            <asp:DropDownList ID="ddl_Hours" runat="server" >
                            </asp:DropDownList>
                            <asp:Label ID="Label14" runat="server" Text="时" ></asp:Label>
                            <asp:DropDownList ID="ddl_Minuter" runat="server" >
                            </asp:DropDownList>
                            分 <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="主办培训部门:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Dep" runat="server" Width="100px">
                            </asp:DropDownList>
                            <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="培训讲师:"></asp:Label>
                        </td>
                        <td style="width: 150px">
                            <input type="text" id="tb_Teacher" runat="server" value="" onfocus="if(this.value=='无'){this.value='';}"
                                onblur="if(this.value==''){this.value='无'}"   style="width: 140px"/>
                                <span style="color: Red">*</span>
                        </td>
                        <td>
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
