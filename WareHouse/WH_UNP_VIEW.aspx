<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_UNP_VIEW.aspx.cs" Inherits="WH_UNP_VIEW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.WareHouse.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table width="960px" id="tb">
                <tr align="left">
                    <td align="left" width="50px">单据号</td>
                    <td width="120px">
                        <asp:TextBox runat="server" ID="txt_no" Width="120px"></asp:TextBox>
                    </td>
                    <td width="60px">项目类型</td>
                    <td Width="100px">
                        <asp:DropDownList runat="server" ID="ddl_projType" Width="100px" AutoPostBack="true"
                            DataValueField="unp_acct_code" DataTextField="unp_acct" 
                            onselectedindexchanged="ddl_projType_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td Width="30px">地点</td>
                    <td Width="80px">
                        <asp:DropDownList runat="server" ID="ddl_site" Width="80px" AutoPostBack="true"
                            onselectedindexchanged="ddl_site_SelectedIndexChanged">
                            <asp:ListItem Value="1000">1000</asp:ListItem>
                            <asp:ListItem Value="2000">2000</asp:ListItem>
                            <asp:ListItem Value="5000">5000</asp:ListItem>
                            <asp:ListItem Value="8000">8000</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="30px">
                        类别
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddl_type_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="UNP-ISS">UNP-ISS</asp:ListItem>
                            <asp:ListItem Value="UNP-RCT">UNP-RCT</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="190px">
                        <asp:RadioButtonList ID="rd_type" runat="server" RepeatDirection="Horizontal"  AutoPostBack="true"
                            onselectedindexchanged="rd_type_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">未提交</asp:ListItem>
                            <asp:ListItem Value="1">已提交</asp:ListItem>
                            <asp:ListItem Value="-1">被退回</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="60px">
                        <asp:Button ID="btn_find" runat="server" Text="查找" onclick="btn_find_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btn_exportexcel" runat="server" Text="导出" onclick="btn_exportexcel_Click" />
                    </td>                      
                </tr>
                <tr>
                    <td align="left" width="50px">申请人
                    </td>
                    <td width="120px">
                        <asp:TextBox runat="server" ID="txt_applyer" Width="120px"></asp:TextBox>
                    </td>
                    <td width="60px">领用部门</td>
                    <td width="100px">
                        <asp:DropDownList runat="server" ID="ddl_department" Width="100px" AutoPostBack="true"
                            onselectedindexchanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                        <input type="hidden" id="hd_domain" runat="server" />
                    </td>
                    <td width="60px">申请日期</td>
                    <td width="190px" colspan="3">
                        <asp:TextBox runat="server" ID="txt_startdate" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>~
                        <asp:TextBox runat="server" ID="txt_enddate" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        加工单<asp:TextBox ID="txt_nbr" runat="server" Width="80px"></asp:TextBox>
                        ID<asp:TextBox ID="txt_ID" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" Width="1080px" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" DataKeyNames ="wh_nbr,wh_type" 
                OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging" CssClass="GridViewStyle GridViewRebuild"     OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="lblLine" Text='<%# Eval("rowNum") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单据号">
                        <ItemTemplate>
                            <asp:Label ID="lblPart" Text='<%# Eval("wh_nbr") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类别">
                        <ItemTemplate>
                            <asp:Label ID="lbltype" Text='<%# Eval("wh_type") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目类型">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" Text='<%# Eval("wh_projName") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="地点">
                        <ItemTemplate>
                            <asp:Label ID="lblptum" Text='<%# Eval("wh_site") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请人">
                        <ItemTemplate>
                            <asp:Label ID="lblloc" Text='<%# Eval("createName") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="领用部门">
                        <ItemTemplate>
                            <asp:Label ID="lbldemandQty" Text='<%# Eval("wh_departmentName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="事由">
                        <ItemTemplate>
                            <asp:Label ID="lblreason" Text='<%# Eval("wh_reason") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="340px" />
                        <ItemStyle HorizontalAlign="Left" Width="340px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="退回原因">
                        <ItemTemplate>
                            <asp:Label ID="lblbackreason" Text='<%# Eval("wh_backreason") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="340px" />
                        <ItemStyle HorizontalAlign="Left" Width="340px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请日期">
                        <ItemTemplate>
                            <asp:Label ID="lblactualQty" Text='<%# Eval("wh_Date") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详情">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDet" CommandName="Det" runat="server" Width="40px">明细</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ControlStyle Font-Underline="True" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" CommandName="Delete" runat="server" Width="40px">删除</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ControlStyle Font-Underline="True" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </form>
</body>
</html>
