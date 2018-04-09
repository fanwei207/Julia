<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Train_Report.aspx.cs" Inherits="hr_Train_Report" %>

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
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".IT_uLog").click(function () {

                var _FormID = $(this).attr("tb_FormID");
                var _src = "../IT/hr_Train_DeptReport.aspx?appno=" + _FormID + "&check=false";
                $.window("任务明细", 1300, 600, _src);
            })

        })
        

    </script>
    </script>
</head>
<body>
     <form runat="server">
       <div align="center">
        <table cellspacing="0" cellpadding="0" style="width: 1080px;">
            <tr>
                <td style=" width:30px;">
                    公司:
                </td>
                <td style=" width:100px;">
                    <asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style=" width:30px;">
                    日期:
                </td>
                <td style=" width:100px;">
                    <asp:TextBox ID="txt_StartDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:30px;" >
                    --
                </td>
                <td style=" width:100px;">
                    <asp:TextBox ID="txt_EndDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:50px;">
                    申请人:
                </td>
                <td style=" width:100px;">
                    <asp:TextBox ID="txt_User" CssClass="Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:30px;">
                    部门:
                </td>
                <td style=" width:100px;">
                    <asp:DropDownList ID="ddl_Dep" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td  style=" width:80px;">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btn_export" runat="server" Text="导出" CssClass="SmallButton3" OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_TrainReport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1080px" PageSize="20" DataKeyNames="train_index,train_AppNo" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" 
            OnRowCommand="gv_RowCommand">
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
                <asp:BoundField HeaderText="申请单号" DataField="train_AppNo" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="公司" DataField="train_Companye" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="培训主题" DataField="train_Subject" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="340px" />
                    <ItemStyle HorizontalAlign="Left" Width="340px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="培训时间" DataField="train_dTime" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}" >
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="培训单位" DataField="train_DepName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="讲师" DataField="train_Teacher" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="听课人数" DataField="train_EntriesNumber" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="培训时数" DataField="train_TrainTime" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="联系方式" DataField="train_Phone" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建人" DataField="train_AppName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkDetail" runat="server" CommandName="Detail"><u>明细</u></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
    </div>
        </form>
</body>
</html>
