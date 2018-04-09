<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_app.aspx.cs" Inherits="hr_Train_app" %>

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
            $("#HR_Skills").click(function () {
                var _formid = $("#tb_FormID").html();
                //window.open('../HR/hr_Train_Skills.aspx?appno=' + _formid + "&check=false");
                var _src = "../HR/hr_Train_Skills.aspx?appno=" + _formid + "&check=false";
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
                            <asp:TextBox ID="txt_dep" runat="server" Visible="false" Width="100px"></asp:TextBox>
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
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="联系电话:"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="tb_Phone" runat="server" style="ime-mode: disabled; width: 100px;"
                                onkeypress="numberkeypress()" />
                            <span style="color: Red">*</span>
                        </td>
<%--                        <td>
                            <asp:Label ID="Label15" runat="server" Text="听课人数:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_Count" runat="server" Width="80px" Style="ime-mode: disabled;"
                                onkeypress="numberkeypress()"></asp:TextBox>人 <span style="color: Red">*</span>
                        </td>--%>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="培训时数:"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tb_Time" runat="server" Width="99px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'
                                Style="height: 19px"></asp:TextBox>小时 <span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="培训区域:"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:CheckBoxList ID="ck_domain" runat="server" RepeatDirection="Horizontal" Height="21px" >
                                <asp:ListItem Value="0">SZX</asp:ListItem>
                                <asp:ListItem Value="1">ZQL</asp:ListItem>
                                <asp:ListItem Value="2">YQL</asp:ListItem>
                                <asp:ListItem Value="3">HQL</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="Label12" runat="server" Text="培训主题:"></asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="tb_Subject" runat="server"
                                Width="90%"></asp:TextBox>
                            <span style="color: Red">*50字以内</span
                        </td>                   
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="培训內容:"></asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="tb_Content" runat="server" TextMode="MultiLine" Style="overflow: auto"
                                Width="90%" Height="120px"></asp:TextBox>
                            <span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="培训地点:"></asp:Label>
                            
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="tb_Place" runat="server" Width="90%"></asp:TextBox>
                                <span style="color: Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="培训对象:"></asp:Label>
                            
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="txt_Object" runat="server" Width="90%"></asp:TextBox>
                                <span style="color: Red">*</span>
                        </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="培训技能:"></asp:Label>
                    </td>
                    <td colspan="8">
                        <div id="skillselect" runat="server">
                        <u id="HR_Skills">技能信息选择</u
                        </div>
       <%--                 <asp:Button ID="btn_selectskill" text="技能信息选择" runat="server" 
                            onclick="btn_selectskill_Click" />--%>
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
                                <asp:Button ID="btn_Application" runat="server" Text="提　交　申　请"
                                    OnClick="btn_Application_Click" />
                            </td>                
                    </tr>
                    <div id="trun" runat="server">
                        <tr id="tr2" runat="server">
                            <td colspan="8" align="center">
                                <u id="HR_PersonsInfo">应到人员＆实到人员信息</u>
                                &nbsp;&nbsp;&nbsp; &nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返      回"
                                    OnClick="btn_back_Click" />
                            </td>
                        </tr>
                    </div>
                
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
