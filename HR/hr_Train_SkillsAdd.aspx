<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_SkillsAdd.aspx.cs" Inherits="hr_Train_SkillsAdd" %>

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
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="技能类别:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_SkillType" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btn_SkillSave" runat="server" Text="保  存"
                                OnClick="btn_SkillSave_Click" />                                &nbsp;&nbsp;
               <%--                 <asp:Button ID="btn_back" runat="server" Text="返      回"
                                    OnClick="btn_back_Click" />       --%>                   
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="培训技能:"></asp:Label>
                    </td>
                    <td>                    
                        <asp:TextBox ID="tb_Skillname" runat="server"  Width="360px"></asp:TextBox>
                    </td>
                </tr>
                    <tr>
                        <td colspan="3">
                             <asp:GridView ID="gv_skilklist" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    Width="630px"  OnRowDeleting="gv_skilklist_RowDeleting" OnRowDataBound="gv_skilklist_RowDataBound"
                                    PageSize="20" DataKeyNames="train_id" AllowPaging="True" >
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="序号" DataField="train_index" ReadOnly="True">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle HorizontalAlign="Right" Width="40px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="培训技能">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("train_SkillName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                                                    Text='<%# Bind("train_SkillName") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="400px" />
                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="创建人" DataField="train_CreatedName" ReadOnly="True">
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                        <%--                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                            EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>--%>
                                        <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                </asp:GridView>
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
