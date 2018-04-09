<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calendarModi.aspx.cs" Inherits="wsline_wl_calendarModi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
    <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1050px">
            <tr>
                <td colspan="2" style="height: 23px">
                    &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True">
                        <asp:ListItem Selected="false" Value="1">上海强凌 SZX</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                        <asp:ListItem Selected="true" Value="5">扬州强凌 YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">淮安强凌 HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="11">上海天灿宝 TCB</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; &nbsp;&nbsp; 成本中心<asp:DropDownList ID="ddl_cc" runat="server" Width="98px"
                        AutoPostBack="false">
                    </asp:DropDownList>
                    &nbsp; 员工类型<asp:DropDownList ID="ddl_type" runat="server" Width="50px" AutoPostBack="false">
                        <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="394">A类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="395">B类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="396">C类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="397">D类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="398">E类</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3"
                        CssClass="SmallTextBox"></asp:TextBox>
                    &nbsp;&nbsp; 考勤日期<asp:TextBox ID="txb_year" runat="server" Width="125px" TabIndex="3"
                        CssClass="SmallTextBox"></asp:TextBox>
                    &nbsp;&nbsp; 考勤类型<asp:DropDownList ID="ddl_atten" runat="server" Width="50px" AutoPostBack="false">
                        <asp:ListItem Selected="true" Value="a">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="I">上班</asp:ListItem>
                        <asp:ListItem Selected="false" Value="O">下班</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; 补漏类型<asp:DropDownList ID="ddl_company" runat="server" Width="50px" AutoPostBack="false">
                        <asp:ListItem Selected="true" Value="2">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="0">私事</asp:ListItem>
                        <asp:ListItem Selected="false" Value="1">公事</asp:ListItem>
                    </asp:DropDownList>
                     &nbsp;&nbsp; 请假时长(小时)<asp:TextBox  ID="septime"  runat="server" Width="30" TabIndex="3"
                        CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 420px; height: 22px">
                </td>
                <td style="height: 22px">
                    <asp:Button ID="btn_search" runat="server" Width="40" CssClass="SmallButton2" Text="查询"
                        TabIndex="4" onclick="btn_search_Click"></asp:Button>
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btn_add" runat="server" Width="40" CssClass="SmallButton2" Text="增加"
                        TabIndex="4" onclick="btn_add_Click"></asp:Button>
                    &nbsp;&nbsp; &nbsp;<asp:Button ID="btn_clear" runat="server" Width="40" CssClass="SmallButton2"
                        Text="清除" TabIndex="4" onclick="btn_clear_Click"></asp:Button>
                    &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btn_exportExcel" runat="server" Width="40"
                        CssClass="SmallButton2" Text="导出" TabIndex="4" 
                        onclick="btn_exportExcel_Click" />&nbsp; &nbsp;
                    <asp:Button ID="btn_exportExcel2" runat="server" Width="94px" CssClass="SmallButton2"
                        Text="导出包含工段" TabIndex="4" ToolTip="夏天当温度超过35度，需为当天出勤人员分工段发盐汽水" 
                        onclick="btn_exportExcel2_Click" />
                &nbsp;<asp:Button ID="btn_updateCC" runat="server" Width="94px" CssClass="SmallButton2"
                        Text="更新成本中心" TabIndex="4" ToolTip="夏天当温度超过35度，需为当天出勤人员分工段发盐汽水" 
                        onclick="btn_updateCC_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="Server" Width="900px" AutoGenerateColumns="false"
        CssClass="GridViewStyle GridViewRebuild"    PageSize="20" AllowPaging="True" 
            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowcancelingedit="GridView1_RowCancelingEdit" 
            onrowupdating="GridView1_RowUpdating"   DataKeyNames="AttendanceID,C_ID,AttendanceTime"
            onrowcommand="GridView1_RowCommand" >
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:table ID="table1" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                 CssClass="GridViewHeaderStyle" GridLines="Vertical" >
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="50" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="员工类型" Width="50" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="考勤日期" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="考勤类型" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="是否补漏" Width="50" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="补漏类型" Width="50" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="考勤号" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="修改者" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="修改日期" Width="70" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="打卡机号" Width="50" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:table>
            </EmptyDataTemplate>
            <Columns>
            <asp:BoundField Visible="false" DataField="AttendanceID" ReadOnly="true"/>
            <asp:BoundField Visible="false" DataField="C_ID" ReadOnly="true"/>
            <asp:BoundField Visible="false" DataField="U_ID" ReadOnly="true" />
             <asp:TemplateField HeaderText="成本中心"> 
                <ItemTemplate> 
                <asp:Label ID="lblCenter" runat="server" Text='<%# Bind("Center") %>'></asp:Label> 
                </ItemTemplate> 
                <EditItemTemplate >
                <asp:TextBox ID="txtCenter" Width="60px" runat="server" Text='<%# Bind("Center")%>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工号"> 
                <ItemTemplate> 
                <asp:Label ID="AttendanceUserNo" runat="server" Text='<%# Bind("AttendanceUserNo") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="40px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="姓名" > 
                <ItemTemplate> 
                <asp:Label ID="AttendanceUserName" runat="server" Text='<%# Bind("AttendanceUserName") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="40px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工类型" > 
                <ItemTemplate> 
                <asp:Label ID="lblUserType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label> 
                </ItemTemplate> 
                <EditItemTemplate >
                <asp:TextBox ID="txtUserType" Width="60px" runat="server" Text='<%# Bind("UserType")%>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="考勤日期" > 
                <ItemTemplate> 
                <asp:Label ID="AttendanceTime" runat="server" Text='<%# Bind("AttendanceTime","{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="120px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="考勤类型" > 
                <ItemTemplate> 
                <asp:Label ID="lblAttendanceType" runat="server" Text='<%# Bind("AttendanceType") %>'></asp:Label> 
                </ItemTemplate> 
                <EditItemTemplate >
                <asp:TextBox ID="txtAttendanceType" Width="60px" runat="server" Text='<%# Bind("AttendanceType")%>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否补漏" > 
                <ItemTemplate> 
                <asp:Label ID="lblIsComp" runat="server" Text='<%# Bind("IsComp") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="60px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" /> 
            </asp:TemplateField>
             <asp:TemplateField HeaderText="补漏类型" > 
                <ItemTemplate> 
                <asp:Label ID="lblComp" runat="server" Text='<%# Bind("Comp") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="60px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="考勤号" > 
                <ItemTemplate> 
                <asp:Label ID="lblAttendenceNo" runat="server" Text='<%# Bind("AttendenceNo") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="70px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" /> 
            </asp:TemplateField>
           <asp:TemplateField HeaderText="修改者" > 
                <ItemTemplate> 
                <asp:Label ID="lblModi" runat="server" Text='<%# Bind("Modi") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="60px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修改日期" > 
                <ItemTemplate> 
                <asp:Label ID="lblModiDate" runat="server" Text='<%# Bind("ModiDate","{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="80px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" /> 
            </asp:TemplateField>
            
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" 
            EditText="<u>调整</u>" UpdateText="<u>保存</u>">
                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="打卡机号" > 
                <ItemTemplate> 
                <asp:Label ID="lblSensor" runat="server" Text='<%# Bind("Sensor") %>'></asp:Label> 
                </ItemTemplate> 
                <HeaderStyle HorizontalAlign="Center" Width="80px" /> 
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" /> 
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
