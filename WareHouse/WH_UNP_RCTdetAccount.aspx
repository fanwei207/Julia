<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_UNP_RCTdetAccount.aspx.cs" Inherits="WH_UNP_RCTdet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>计划外入库入账</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <base target="_self">
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="Center">
            <table width="980px" id="tb">
                <tr align="left">
                    <td align="left" width="60px">申请人
                    </td>
                    <td width="180px">
                        <asp:TextBox runat="server" ID="txt_applyer" Width="100px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="80px">领用部门</td>
                    <td width="150px">
                        <asp:TextBox runat="server" ID="txt_department" Width="150px" ReadOnly="true"></asp:TextBox>
                        <input type="hidden" id="hd_domain" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btn_update" runat="server" Text="入账" OnClick="btn_update_Click" Width="69px" Height="36px" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_cancel" runat="server" Text="退回" Width="69px" Height="36px" OnClick="btn_cancel_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_submit" runat="server" Text="确认" Width="69px" Height="36px" OnClick="btn_submit_Click" Visible="false"/>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_back" runat="server" Text="取消" Width="69px" Height="36px" OnClick="btn_back_Click" Visible="false"/>
                    </td>
                </tr>
                <tr align="left">

                    <td>地点</td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_site" Width="100px" ReadOnly="true"></asp:TextBox></td>
                    <td align="left">单据号</td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_no" Width="150px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Literal ID="lt_type" runat="server" Text="项目类型" Visible="false"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_projType" Width="100px" ReadOnly="true"></asp:TextBox></td>
                    <td>申请日期</td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_date" Width="150px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="lt_reason" runat="server" Text="退回理由" Visible="false"></asp:Literal>
                    </td>
                    <td colspan="4">
                        <asp:TextBox runat="server" ID="txt_reason" Width="600px" TextMode="MultiLine" Visible="false" MaxLength="200"></asp:TextBox>
                    </td>             
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" ShowFooter="true" Width="980px" AutoGenerateColumns="False" DataKeyNames ="wh_part,rowNum" 
                 CssClass="GridViewStyle" PageSize="20" AllowPaging="True" Height="28px" >
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
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码">
                        <ItemTemplate>
                            <asp:Label ID="lblPart" Text='<%# Eval("wh_part") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="品名 规格 型号">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" Text='<%# Eval("wh_desc") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <asp:Label ID="lblptum" Text='<%# Eval("wh_pt_um") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库位">
                        <ItemTemplate>
                            <asp:Label ID="lblloc" Text='<%# Eval("wh_pt_loc") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="需求数量">
                        <ItemTemplate>
                            <asp:Label ID="lbldemandQty" Text='<%# Eval("wh_demandQty") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实发数量">
                        <ItemTemplate>
                            <asp:Label ID="lblactualQty" Text='<%# Eval("wh_actualQty") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblremark" Text='<%# Eval("wh_remark") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="210px" />
                        <ItemStyle HorizontalAlign="Left" Width="210px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
