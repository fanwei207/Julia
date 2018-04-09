<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderRoom.aspx.cs" Inherits="OrderRoom" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function datelink() 
        {
            document.getElementById("tb_EndDate").value = document.getElementById("tb_OrderDate").value;
        }
    </script>
</head>
<body>
    <div align="center">
    <form id="form1" runat="server">
    <div class="AppMainContent">
        <table class="TB_AppPage">
            <caption>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </caption>
            <tr>
                <td>&nbsp</td>
            </tr>
            <tr>
                <td width="60">
                    <asp:Label ID="lt_FormID" runat="server" Text="预订单号：" 
                        ></asp:Label>
                </td>
                <td width="90">
                    <asp:Label ID="tb_FormID" runat="server" Text="申请会自动产生"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="申&nbsp;&nbsp;请&nbsp;&nbsp;人：" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="tb_AppName" runat="server"></asp:Label>
                </td>
                <td width="60">
                    <asp:Label ID="Label3" runat="server" Text="部门名称："></asp:Label>
                </td>
                <td width="60">
                    <asp:Label ID="tb_DeptName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="联系方式：" 
                        meta:resourcekey="Label8Resource2"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_AppExtNo" Width="120px" runat="server" onbeforepaste="PastValidate();"></asp:TextBox>
                    <span style="color: Red; font-size: 10.0pt;">*</span>
                </td>

            </tr>
            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="使用人数："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_MeettingMemberNum" runat="server" Width="60px" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " ></asp:TextBox>(人)
                    <span style="color: Red; font-size: 10.0pt;">*</span>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" 
                        Text="事&amp;nbsp&amp;nbsp&amp;nbsp&amp;nbsp由："></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="tb_Reason" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox>
                    <span style="color: Red; font-size: 10.0pt;">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="借用物品：" ></asp:Label>
                </td>
                <td colspan="7">
                    <asp:TextBox ID="tb_BorrowThings" runat="server" Width="93%" 
                        TextMode="MultiLine"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="其它描述:" ></asp:Label>
                </td>
                <td colspan="7">
                    <asp:TextBox ID="tb_otherDes" runat="server" Width="93%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                        <hr style="border: 0; border-top: 2px solid #DE5C2F; height: 2px;" />
                </td>
            </tr>
             </div>
            <tr>
                <td>
                    会议室地点：
                </td>
                <td colspan="7">
                 <asp:RadioButtonList ID="rbl_CompanyCode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
                        OnSelectedIndexChanged="rbl_CompanyCode_SelectedIndexChanged" >
                                        </asp:RadioButtonList>
                </td>          
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="会议室编号"></asp:Label>：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_RoomID" runat="server" Height="23px" Width="90px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_RoomID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="分机号码："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="tb_Phone" runat="server" ForeColor="#9900FF"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server"  Text="坐&amp;nbsp位&amp;nbsp数："></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="tb_SeatCount" runat="server" ForeColor="#9900FF"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="有无投影仪："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="rbtl_HasProjector" runat="server" Font-Bold="True" 
                        ForeColor="#9900FF"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" 
                        Text="电&amp;nbsp&amp;nbsp&amp;nbsp&amp;nbsp&amp;nbsp脑："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="rbtl_HasComputer" runat="server"  Font-Bold="True" 
                        ForeColor="#9900FF"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="有无白板："></asp:Label>
                   
                </td>
                <td colspan="3">
                    <asp:Label ID="rbtl_HasWhiteBoard" runat="server"  Font-Bold="True"
                        ForeColor="#9900FF"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" 
                        Text="白&amp;nbsp&amp;nbsp板&amp;nbsp&amp;nbsp 笔："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="tb_WBPan" runat="server" ForeColor="#9900FF"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="饮&amp;nbsp水&amp;nbsp机："></asp:Label>
                </td>
                <td colspan="1">
                    <asp:Label ID="rbtl_HasEatWaterMa" runat="server" Font-Bold="True"
                        ForeColor="#9900FF"></asp:Label>
                </td>
               
                <td>
                    <asp:Label ID="Label2" runat="server" Text="ConCall机：" 
                        ></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="rbtl_HaveConCall" runat="server" Font-Bold="True"
                        ForeColor="#9900FF"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="Label22" runat="server" Text="预订开始时间：" 
                        meta:resourcekey="Label22Resource2"></asp:Label>
                    <asp:TextBox ID="tb_OrderDate"  CssClass="SmallTextBox Date"
                        runat="server"   Width="80px"></asp:TextBox>
                        
                    <asp:DropDownList ID="ddl_BeginHour" runat="server">
                    </asp:DropDownList>
                   
                    <asp:DropDownList ID="ddl_BeginMin" runat="server" 
                        meta:resourcekey="ddl_BeginMinResource2">
                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">
                    <asp:Label ID="Label23" runat="server" Text="预订结束时间：" 
                        meta:resourcekey="Label23Resource2"></asp:Label>
                    <asp:TextBox ID="tb_EndDate"  CssClass="SmallTextBox Date" runat="server" 
                        Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddl_EndHour" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddl_EndMin" runat="server" >
                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtn_IsFullDay" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="连续时间" Value="true" Selected="True" 
                            meta:resourcekey="ListItemResource12"></asp:ListItem>
                        <asp:ListItem Text="指定区段" Value="false"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                    <td colspan="8">
                        <span id="Span2" style="color: Red;" runat="server">注:申请时间区间说明;申请8:00～9:00；为8:01～8:59;不包含9:00～9:59</span>
                    </td>
                </tr>
            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
            <div id='div_orderedinfo' runat="server">
                <tr>
                    <td colspan="8">
                        <asp:Literal ID="lit_ShowInfos" runat="server" ></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:RadioButtonList ID="rbtn"  runat="server" RepeatDirection="Horizontal"  RepeatColumns="7" AutoPostBack="True"
                            OnSelectedIndexChanged="rbtn_SelectedIndexChanged"  >
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                    </td>
                </tr>
           
                <tr>
                    <td colspan="8">
                        <span id="Span1" style="color: Red;" runat="server">注:背景色为已预订.</span>
                    </td>
                </tr>
                <tr>
                <td colspan="8" align="center">
                    <asp:Button ID="btn_App" runat="server" Text="发送申请" OnClientClick="show()" 
                        OnClick="btn_App_Click"/>
                </td>
            </tr>
             

                 <tr>
                <td colspan="8">
                
                    <asp:Panel ID="pn_ShowOders" runat="server" Width="100%"  
                        meta:resourcekey="pn_ShowOdersResource2">
                        <font color="red">&nbsp;&nbsp;<asp:Label ID="Label24" runat="server" Text="注" ></asp:Label>：
                            <asp:Label ID="Label25" runat="server" Text="当前会议室预订记录"></asp:Label>....</font>
                        <asp:GridView ID="gdv_MroomOrderList" runat="server" Width="99%" AutoGenerateColumns="False"
                            CssClass="GridViewStyle">
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="MR_FormId" HeaderText="订单号" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="120px" HorizontalAlign ="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mr_Deptname" HeaderText="部门" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Mr_APPNAME" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Mr_APPExtNo" HeaderText="联系方式" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="MR_begintime"  ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="开始时间" >
                                <HeaderStyle Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MR_endtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="结束时间">
                                <HeaderStyle Width="160px" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                        </asp:GridView>
                    </asp:Panel>
                    
                </td>
            </tr>
                 <tr>
                <td colspan="8">
                    <font color="red">
                        <asp:Label ID="Label27" runat="server" Text="注：‘连续时间’表示从开始时间，到结束时间 这一区间全部预订！"></asp:Label><br />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label28" runat="server" Text="‘指定区段’表示在指定日期內，预订每天的这一时间段 "></asp:Label>！&nbsp;
                    </font>
                </td>
            </tr>
            </div>
        </table>
   
    </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
