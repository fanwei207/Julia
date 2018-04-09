<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustPartEdit.aspx.cs" Inherits="CustPartEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
            <table cellspacing="0" cellpadding="2" bgcolor="white" border="0" style="background-image: url('../images/bg_tb17.jpg'); background-repeat: repeat-x; margin-top: 20px; height: 267px;">
                <tr>
                    <td rowspan="9" style="width: 4px; background-image: url(../images/bg_tb16.jpg); background-repeat: no-repeat; background-position: left top;"></td>
                    <td style="height: 1px; text-align: right;">&nbsp;</td>
                    <td align="Left" style="width: 112px">
                        <asp:DropDownList ID="dropDomain" runat="server" Visible="False">
                            <asp:ListItem>SZX</asp:ListItem>
                            <asp:ListItem>ZQL</asp:ListItem>
                            <asp:ListItem>YQL</asp:ListItem>
                            <asp:ListItem>HQL</asp:ListItem>
                            <asp:ListItem>ATL</asp:ListItem>
                            <asp:ListItem>ZQZ</asp:ListItem>
                            <asp:ListItem>TCP</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="Left" style="width: 213px">&nbsp;</td>
                    <td rowspan="9" style="width: 4px; background-image: url(../images/bg_tb18.jpg); background-repeat: no-repeat; background-position: right top;"></td>
                </tr>
                <tr>
                    <td style="height: 1px; text-align: right;">客户货物/发往:
                    </td>
                    <td align="Left" style="width: 112px">
                        <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="10"
                            AutoPostBack="True" OnTextChanged="txtCust_TextChanged"></asp:TextBox>
                    </td>
                    <td align="Left" style="width: 213px">
                        <asp:Label ID="lbCustName" runat="server" CssClass="smalltextbox" Width="205px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1px; text-align: right;">&nbsp;客户物料:
                    </td>
                    <td align="Left" style="width: 112px">
                        <asp:TextBox ID="txtPart" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="50" AutoPostBack="True" OnTextChanged="txtPart_TextChanged"></asp:TextBox>
                    </td>
                    <td align="Left" style="width: 213px">SKU:
                     <asp:TextBox ID="txtSku" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1px; text-align: right;">物料号:
                    </td>
                    <td align="Left" style="width: 112px">
                        <asp:TextBox ID="txtQad" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="15" AutoPostBack="True" OnTextChanged="txtQad_TextChanged"></asp:TextBox>
                    </td>
                    <td align="Left" style="width: 213px">&nbsp;&nbsp; UL:&nbsp;
                     <asp:TextBox ID="txtUL" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="50" ReadOnly="True"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td style="height: 1px; text-align: right;">&nbsp;生效日期:
                    </td>
                    <td align="Left" style="width: 112px">
                        <asp:TextBox ID="txtStdDate" runat="server" CssClass="smalltextbox Date" Width="81px"
                            MaxLength="10"></asp:TextBox>
                    </td>
                    <td style="height: 1px;">截止日期:
                
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="smalltextbox Date" Width="82px"
                        MaxLength="10"></asp:TextBox>
                        <asp:Label ID="lblckickQAD" runat="server" Text="0" Visible="false"></asp:Label>
                        <asp:Label ID="lblchickUL" runat="server" Text="" Visible="false"></asp:Label>
                    </td>


                </tr>

                <tr>
                    <td colspan="3" align="center">
                        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="cp_id,cp_start_date,cp_end_date" PageSize="5" Width="300px"
                            CssClass="GridViewStyle" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowDataBound="gvlist_RowDataBound">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="物料号" DataField="cp_part">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="生效日期" DataField="cp_start_date">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="截止日期" DataField="cp_end_date">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>


                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>

                <tr>
                    <td style="height: 1px; text-align: right;">说明:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txtComment" runat="server" CssClass="smalltextbox" Width="100%"
                            MaxLength="50"></asp:TextBox>
                        <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1px; text-align: right;">显示客户物料:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txtPartd" runat="server" CssClass="smalltextbox" Width="100%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 23px; text-align: center;">
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" Width="70px" Visible="false"
                            OnClick="btnSave_Click" />
                    </td>
                    <td style="height: 23px; text-align: center;">
                        <asp:Button ID="btnupdate" runat="server" CssClass="SmallButton2" Text="新增" Width="70px" OnClick="btnupdate_Click" />
                    </td>
                    <td style="height: 23px; text-align: center;">
                        <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" Text="返回" Width="70px" OnClick="btnback_Click" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
