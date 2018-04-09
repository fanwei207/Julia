<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wh_unpRct.aspx.cs" Inherits="WareHouse_Wh_unpRct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $(".GridViewHeaderStyle").after($(".GridViewFooterStyle"));
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table width="980px" id="tb">
                <tr align="left">
                    <td align="left" width="60px">申请人</td>
                    <td width="120px">
                        <asp:TextBox runat="server" ID="txt_applyer" Width="100px"></asp:TextBox>
                    </td>
                    <td width="60px">领用部门</td>
                    <td width="120px">
                        <asp:DropDownList runat="server" ID="ddl_department" Width="150px" Enabled="false"></asp:DropDownList>
                        <input type="hidden" id="hd_domain" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr align="left">

                    <td>地点</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_site" Width="120px">
                            <asp:ListItem Value="1000">1000</asp:ListItem>
                            <asp:ListItem Value="2000">2000</asp:ListItem>
                            <asp:ListItem Value="5000">5000</asp:ListItem>
                            <asp:ListItem Value="8000">8000</asp:ListItem>
                        </asp:DropDownList></td>
                    <td align="left">单据号</td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_no" Width="120px" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr align="left">
                    <td>项目类型</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_projType" Width="150px" AutoPostBack="true" 
                            DataValueField="unp_acct_code" DataTextField="unp_acct" 
                            onselectedindexchanged="ddl_projType_SelectedIndexChanged"></asp:DropDownList></td>
                    <td>申请日期</td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_date" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        事由:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_reason" TextMode="MultiLine" runat="server" Width="700px" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" ShowFooter="true" Width="700px" AutoGenerateColumns="False" DataKeyNames ="wh_part,wh_mstrID,rowNum"
                 CssClass="GridViewStyle" PageSize="20" AllowPaging="True" Height="28px" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting">
                <RowStyle CssClass="GridViewRowStyle"/>
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="True" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                     <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="lblLine" Text='<%# Eval("rowNum") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLine" Text='' Width="30px" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码">
                        <ItemTemplate>
                            <asp:Label ID="lblPart" Text='<%# Eval("wh_part") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPart" CssClass="Part" Text='' Width="120px" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="品名 规格 型号">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" Text='<%# Eval("wh_desc") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDesc" Text='' runat="server" Width="250px" CssClass="PartDesc" onfocus="this.blur()"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <asp:Label ID="lblptum" Text='<%# Eval("wh_pt_um") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddl_um" runat="server" Width="50px" DataSource='<%# ddlbind()%>' DataValueField="code_value" DataTextField="code_value" >
                                <asp:ListItem Value="EA">EA</asp:ListItem>
                                <asp:ListItem Value="BL">BL</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库位">
                        <ItemTemplate>
                            <asp:Label ID="lblloc" Text='<%# Eval("wh_pt_loc") %>' runat="server" MaxLength="8"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtloc"  Text='' runat="server" Width="80px" MaxLength="8"  CssClass="PartLoc"  onfocus="this.blur()"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="应收数量">
                        <ItemTemplate>
                            <asp:Label ID="lbldemandQty" Text='<%# Eval("wh_demandQty") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtdemandQty"  Text='' runat="server" Width="70px"  onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")' MaxLength="10"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实收数量">
                        <ItemTemplate>
                            <asp:Label ID="lblactualQty" Text='<%# Eval("wh_actualQty") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtactualQty"  Text='' runat="server" Width="70px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")' MaxLength="10"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblremark" Text='<%# Eval("wh_remark") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtremark"  Text='' runat="server" Width="230px"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="230px" />
                        <ItemStyle HorizontalAlign="Left" Width="230px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" CommandName="Delete" runat="server" Width="40px">删除</asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="linkAddCust" CommandName="AddLine" runat="server" Width="40px">新增</asp:LinkButton>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <tr>
                <td>
                        <asp:Button ID="btn_submit" Text="提交" runat="server" Width="60px" 
                onclick="btn_submit_Click" />
                </td>&nbsp;&nbsp;&nbsp;
                <td align="Left">
                    <asp:Label runat="server" ID="lbl_flag" Visible="false"></asp:Label>
                    <asp:Button runat="server" ID="btn_continue" Width="70px" Text="新建申请" OnClick="btn_continue_Click" />
                       &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcel" runat="server" Text="导出" 
                        onclick="btn_exportexcel_Click" />
                </td>          
            </tr>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
