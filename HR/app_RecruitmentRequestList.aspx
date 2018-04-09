<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_RecruitmentRequestList.aspx.cs" Inherits="HR_hr_RecruitmentRequestList" %>

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
//        $(function () {
//            $('#checkPower').click(function () {

//                //alert('123');
//                var _src = "../HR/app_RecruitmentRequestListDet.aspx";
//                $.window("招聘申请明细", 850, 800, _src);
//            })
        //        })                                                                                                                                                                                                                                                           
       

        $(function () {
            $(".GridViewRowStyle").dblclick(function () {

                var App_Company = $(this).find("td:eq(0)").html();
                var App_Date = $(this).find("td:eq(1)").html();
                var App_userName = $(this).find("td:eq(2)").html();
                var App_department = $(this).find("td:eq(3)").html();
                var App_Process = $(this).find("td:eq(4)").html();
                var _src = "../HR/app_RecruitmentRequestListDet.aspx?App_Company=" + App_Company + "&App_Date=" + App_Date +
                            "&App_userName=" + App_userName + "&App_department=" + App_department + "&App_Process=" + App_Process;
                $.window("招聘申请明细", 650, 570, _src);

//                var _tskNbr = $(this).find("td:eq(1)").html();
//                var _src = "../IT/TSK_TaskView.aspx?tskNbr=" + _tskNbr;
//                $.window("任务明细", 650, 600, _src);
            })
            $(".GridViewAlternatingRowStyle").dblclick(function () {
                var App_Company = $(this).find("td:eq(0)").html();
                var App_Date = $(this).find("td:eq(1)").html();
                var App_userName = $(this).find("td:eq(2)").html();
                var App_department = $(this).find("td:eq(3)").html();
                var App_Process = $(this).find("td:eq(4)").html();
                var _src = "../HR/app_RecruitmentRequestListDet.aspx?App_Company=" + App_Company + "&App_Date=" + App_Date +
                            "&App_userName=" + App_userName + "&App_department=" + App_department + "&App_Process=" + App_Process;
                $.window("招聘申请明细", 650, 570, _src);

                   
//                var _tskNbr = $(this).find("td:eq(1)").html();
//                var _src = "../IT/TSK_TaskView.aspx?tskNbr=" + _tskNbr;
//                $.window("任务明细", 650, 600, _src);
            })
        })  
    </script>
    <style type ="text/css">
        body { font-size:15px;}
    </style>
</head>
<body>
    <div  align="Center" style="margin-top:20px;">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>申请日期</td>
            <td>
                <asp:TextBox ID="txtAppDate" runat="server"  CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="labCompany" runat="server" Text="所属公司"></asp:Label></td>
            <td><asp:DropDownList ID="ddlCompany" runat="server"  Width="80px" 
                    DataTextField="plantCode" DataValueField="plantID" 
                    onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="labDepartment" runat="server" Text="部门"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server"  Width="80px" 
                    DataTextField="name" DataValueField="departmentID" 
                    AutoPostBack="True"></asp:DropDownList></td>
            <%--<td>岗位</td>
            <td>
                <asp:DropDownList ID="ddlProcess" runat="server"  Width="80px" 
                    DataTextField="roleName" DataValueField="roleID"></asp:DropDownList></td>--%>
            <td>状态</td>
            <td><asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                <asp:ListItem Text="--状态--" Value="4"></asp:ListItem>
                <asp:ListItem Text="审核中" Value="0"></asp:ListItem>
                <asp:ListItem Text="招聘中" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" 
                    onclick="btnSearch_Click" /></td>
        </tr>
    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" 
    OnRowCommand="gv_RowCommand" DataKeyNames="App_ProcID,App_id,App_Company,App_Date,App_userName
    ,App_department,App_Process,App_StatusID,App_Status,App_plantCode,App_departmentID,App_roleID,APP_NewProc"
    OnRowDataBound="gv_RowDataBound" onrowdeleting="gv_RowDeleting">
    <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
    <RowStyle CssClass="GridViewRowStyle" />
    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
    <PagerStyle CssClass="GridViewPagerStyle" />
    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
    <HeaderStyle CssClass="GridViewHeaderStyle" />
    <EmptyDataTemplate>
        <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
            GridLines="Vertical">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="center" Text="所属公司" Width="50px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="70px"></asp:TableCell>
			    <asp:TableCell HorizontalAlign="center" Text="申请人" Width="60px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Text="部门" Width="80px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Text="招聘岗位" Width="80px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Text="招聘人数" Width="50px"></asp:TableCell>                        
                <asp:TableCell HorizontalAlign="center" Text="岗位人数" Width="50px"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </EmptyDataTemplate>
    <Columns>
        <asp:BoundField DataField="App_Company" HeaderText="所属公司">
            <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_Date" HeaderText="申请日期">
            <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="70px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_userName" HeaderText="申请人">
            <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="60px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_department" HeaderText="部门">
            <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_Process" HeaderText="招聘岗位">
            <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_Num" HeaderText="招聘人数">
            <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_PostNum" HeaderText="岗位人数">
            <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_Status" HeaderText="状态">
            <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="App_ResumeQty" HeaderText="简历数量">
            <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="岗位审核">
            <ItemTemplate>
                <asp:LinkButton ID="linkReview" Text="<u>审核</u>" ForeColor="Black" Font-Underline="True"
                    Font-Size="12px" runat="server"
                    CommandName="review" />
            </ItemTemplate>
            <HeaderStyle Width="60px" Font-Bold="False" />
            <ItemStyle Width="60px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="上传简历">
            <ItemTemplate>
                <asp:LinkButton ID="linkresume" Text="<u>简历</u>" ForeColor="Black" Font-Underline="True"
                    Font-Size="12px" runat="server"
                    CommandName="Resume"/>
            </ItemTemplate>
            <HeaderStyle Width="60px" Font-Bold="False" />
            <ItemStyle Width="60px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
            <HeaderStyle Width="40px" HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <ControlStyle Font-Bold="False" Font-Size="12px" />
        </asp:CommandField>  
        <asp:TemplateField HeaderText="结束">
            <ItemTemplate>
                <asp:LinkButton ID="linkover" Text="<u>招聘结束</u>" ForeColor="Black" Font-Underline="True"
                    Font-Size="12px" runat="server"
                    CommandName="AppOver"/>
            </ItemTemplate>
            <HeaderStyle Width="60px" Font-Bold="False" />
            <ItemStyle Width="60px" HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
    </div>
</body>
</html>
