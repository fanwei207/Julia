<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Shipcheck.aspx.cs" Inherits="SID_SID_ShipX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
         <table id="table2" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                
                <td>
                系统单号<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
               </td>
               <td align="left">
                 出运单号<asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
               </td>
               
                
                <td>
                    出厂日期<asp:TextBox ID="txtOutDate" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    -<asp:TextBox ID="txtOutDate2" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="left">
                    运&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;往<asp:TextBox ID="txtshipto" runat="server"
                        CssClass="SmallTextBox" Width="280px"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
               
               
                <td>
                    验货地点<asp:TextBox ID="txtInspectSite" runat="server" CssClass="SmallTextBox" 
                        Width="100px"></asp:TextBox>
                </td>
              
              <td>
                    所在公司<asp:TextBox ID="txtdomain" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            <td class="style1">
                    验货日期<asp:TextBox ID="txtInspectDate" runat="server" CssClass="SmallTextBox Date"
                          onpaste="return false;"
                        Width="100px"></asp:TextBox>-
                        <asp:TextBox ID="txtInspectDate2" runat="server" CssClass="SmallTextBox Date"
                          onpaste="return false;"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    装箱地点<asp:TextBox ID="txtsite" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
         
        </table>
        <table id="table1" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                <td width="720px" align="left" colspan="3">
                        出运日期<asp:TextBox ID="txtdate1" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>-
                         <asp:TextBox ID="txtdate2" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>
                        创建日期<asp:TextBox ID="txt_createdate1" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>-
                         <asp:TextBox ID="txt_createdate2" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>
                           <asp:RadioButton ID="rad1" runat="server" Text="未报关" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" oncheckedchanged="rad1_CheckedChanged" ></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="已报关" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" oncheckedchanged="rad2_CheckedChanged" ></asp:RadioButton>&nbsp;
                   
                </td>
<%--            </tr>
            <tr> --%>
                <td align="left">
                  <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="40"
                        OnClick="btnSearch_Click" />
                                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcel" runat="server" CssClass="SmallButton3" 
                        Text="导出" Width="40px"
                        OnClick="btn_exportexcel_Click" />
                    &nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcelbytotal" runat="server" CssClass="SmallButton3" 
                        Text="导出已确认" Width="60px"
                        OnClick="btn_exportexcelbytotal_Click" />
                    &nbsp;
                    <asp:Button ID="btn_exportexcelbywo" runat="server" CssClass="SmallButton3" 
                        Text="导出(工单)" Width="60px"
                        OnClick="btn_exportexcelbywo_Click" />
                    &nbsp;                                             
                    </td>
               
                <td style="height: 20px">
                   
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShip" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ID,nbr" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="1130px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="PK" HeaderText="系统单号">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PKref" HeaderText="参考">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="nbr" HeaderText="出运单号">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="via" HeaderText="运输方式">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ctype" HeaderText="集装箱型">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="site" HeaderText="装箱地点">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipdate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="outdate" HeaderText="出厂日期">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_date" HeaderText="验货日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_site" HeaderText="验货地点">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_matchdate" HeaderText="预配日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="mj" HeaderText="免检">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                 <asp:BoundField DataField="IsCabin" HeaderText="配舱">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                

              <asp:BoundField DataField="sid_finsheddate" HeaderText="完工时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                 <asp:BoundField DataField="checkeddate" HeaderText="货物抵达时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                 <asp:ButtonField CommandName="Detail1" Text="<u>详细</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>

                <asp:TemplateField HeaderText="送货确认">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org3_con") %>' Text='<%# Eval("SID_org3_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="报关">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
