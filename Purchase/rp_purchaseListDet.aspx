<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseListDet.aspx.cs" Inherits="Purchase_rp_purchaseListDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function(){
            $(".cssCancel").click(function(){
                var ID = $(this).parent().parent().find("td").eq(15).html();
                var param = 'ID=' + ID;                
                var _src = '../Purchase/rp_purchaseListDetCancel.aspx?' + param;
                $.window("采购单查询-拒绝采购", "40%", "40%", _src, "", true);
                return false;
            });
            $(".cssCan").click(function(){
                alert($(this).parent().prev().html());
                return false;
            });
        });
    </script>
    <style type="text/css">
        .hidden { display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td align="left" width="40px">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td>供应商</td>
                <td>
                    <asp:TextBox ID="txtVend" CssClass="Supplier" Enabled="false" runat="server"></asp:TextBox>
                </td>
                 <td>QAD</td>
                <td>
                    <asp:TextBox ID="txtQAD" CssClass="Supplier" Enabled="true" runat="server"></asp:TextBox>
                </td>
                 <td>采购单号</td>
                <td>
                    <asp:TextBox ID="txtPurNbr" CssClass="Supplier" Enabled="true" runat="server"></asp:TextBox>
                </td>
                <td>状态</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Text="-全部-" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="已采购" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未采购" Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNewPur" runat="server" CssClass="SmallButton2" Visible="false" Text="生成采购单" OnClick="btnNewPur_Click" />
                    <asp:Button ID="btnNewWeb" runat="server" CssClass="SmallButton3" Visible="false" Text="生成网购单" OnClick="btnNewWeb_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="12">                    
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="MID,ID,rp_supplier,rp_supplierName,rp_QAD,qty,rp_Um,rp_price,rp_QADDesc1,rp_QADDesc2,pur_Nbr,pur_Line"                      
                        AllowPaging="true" PageSize="20" OnRowCommand="gv_RowCommand" OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
                         OnRowUpdating="gv_RowUpdating" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
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
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="130px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="采购数量" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="单位" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="价格" Width="350px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述1" Width="120px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="物料描述2" Width="120px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rp_Supplier" HeaderText="供应商" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_SupplierName" HeaderText="供应商名称" ReadOnly="True">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="QAD" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="130px" Font-Underline="True" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkQAD" CssClass="no" runat="server" Text='<%# Bind("rp_QAD") %>'
                                        CommandName="ViewEdit"></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="rp_QAD" HeaderText="QAD" ReadOnly="True">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="qty" HeaderText="采购数量" ReadOnly="True">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Um" HeaderText="单位" ReadOnly="True">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Price" HeaderText="价格" ReadOnly="True">
                                <HeaderStyle Width="35px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="35px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc1" HeaderText="物料描述1" ReadOnly="True">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_QADDesc2" HeaderText="物料描述2" ReadOnly="True">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="rp_Uses" HeaderText="用途">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Descript" HeaderText="描述">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                              <asp:BoundField DataField="rp_BusinessDeptName" HeaderText="采购部门">
                                <HeaderStyle Width="120px" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="采购单号">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox nbr" Text='<%# Bind("pur_Nbr") %>'
                                    Width="95px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="95px" />
                                <ItemTemplate>
                                    <%#Eval("pur_Nbr")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="采购单行号">
                                <EditItemTemplate>
                                <asp:TextBox ID="txtLine" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pur_Line") %>'
                                    Width="95px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="95px" />
                                <ItemTemplate>
                                    <%#Eval("pur_Line")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ControlStyle Font-Bold="False" Font-Size="12px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle Width="10px" CssClass="hidden" HorizontalAlign="Right" Font-Bold="False" />
                                <ItemStyle Width="10px" CssClass="hidden" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="Cancel" runat="server" CausesValidation="False" CssClass="SmallButton3 cssCan"
                                        Text="取消" CommandName="cancel"/>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" Height="40px" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="70px" Font-Underline="True" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkC" CssClass="cssCancel" runat="server" Text='拒绝'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidXXMstrID" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
