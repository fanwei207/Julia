<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supp_FatocryInspection_New.aspx.cs" Inherits="Supplier_Supp_FatocryInspection_New" %>


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
    <form id="form1" runat="server">
          <asp:HiddenField ID="hidsuppno" runat="server" />
            <asp:HiddenField ID="hidFIid" runat="server" />
        <div align="center">
            <table style="width: 600px">
                <tr>
                    <td align="left" colspan="4">NO.<asp:Label ID="lblNo" runat="server" Visible="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left;">类型：</td>
                    <td align="left" colspan="3">

                        <%--<asp:DropDownList ID="ddlType" runat="server" DataTextField="type" DataValueField="type_id"></asp:DropDownList>--%>
                        <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal" DataTextField="type" DataValueField ="type_id">
                           <%-- <asp:ListItem Selected="True">新增供应商验厂</asp:ListItem>
                            <asp:ListItem>周期验厂</asp:ListItem>--%>

                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">供应商编码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtvent" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; height: 15px;">等级：
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtlevel" runat="server" CssClass="SmallTextBox5" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">供应商名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 80px; text-align: left;">验厂时间：</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtStdDate" CssClass="Date Param" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">提供产品或服务范围：
                    </td>
                    <td style="text-align: left; height: 15px;" colspan="3">
                        <asp:TextBox ID="txtService" runat="server" CssClass="SmallTextBox5" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">公司地址：
                    </td>
                    <td style="text-align: left; height: 15px;" colspan="3">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallTextBox5" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">营业执照号码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtLicenseNO" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 80px; text-align: left;">法人代表：</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtLegalPerson" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">业务负责人：
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusiness" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 80px; text-align: left;">品质负责人：</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtQuality" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">联系电话：
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 80px; text-align: left;">传真：</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtFax" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">员工人数：
                    </td>
                    <td>
                        <asp:TextBox ID="txtnumber" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 80px; text-align: left;">技术人员：</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:TextBox ID="txtTechnology" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">
                        <asp:Button ID="btnDone" runat="server" Text="提交" CssClass="SmallButton3" OnClick="btnDone_Click" />
                        <asp:Button ID="btnback" runat="server" Text="返回" CssClass="SmallButton3" OnClick="btnback_Click" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 15px;" colspan="4"></td>
                </tr>


            </table>
            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15" OnRowDataBound="gv1_RowDataBound" OnRowCommand="gv1_RowCommand" DataKeyNames="departmentID,plantID,Uid,UName,cbkcheck">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="各单位责任" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="意见" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="责任人" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Select" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                        <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Company" HeaderText="公司">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="部门" DataField="departmentName">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="是否去实地" DataField="FI_real">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="验厂人">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkperson" ForeColor="Blue" Font-Underline="true" Text='<%# Bind("FI_Member") %>'
                                Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Reviewer">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                        <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
  
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
