<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipDetailEng.aspx.cs" Inherits="SID_SID_ShipDetailEng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table style="width: 1000px;" cellspacing="0" cellpadding="4" class="table05">
            <tr>
                <td>
                    Systen No.:<asp:Label ID="lblPK" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    Reference:<asp:Label ID="lblPKref" runat="server"
                        Width="100px"></asp:Label>
                </td>
                <td>
                    Waybill No.:<asp:Label ID="lblnbr" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    Transport:<asp:Label ID="lblVia" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Packing Place:<asp:Label ID="lblsite" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    Shipment Date:<asp:Label ID="lblShipDate" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    Manufacture Date:<asp:Label ID="lblOutDate" runat="server" Width="110px"></asp:Label>
                </td>
                <td>
                    Container Type:<asp:Label ID="lblCtype" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total Containers:<asp:Label ID="lblBox" runat="server"></asp:Label>
                </td>
                <td>
                    Total Volume:<asp:Label ID="lblVolume" runat="server"></asp:Label>
                </td>
                <td>
                    Gross weight:<asp:Label ID="lblWeight" runat="server"></asp:Label>
                </td>
                <td>
                    Total Price:<asp:Label ID="lblPrice" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Shipped:<asp:Label ID="lblshipto" runat="server"
                        Width="280px"></asp:Label>
                </td>
                <td>
                    The total number of pieces:<asp:Label ID="lblPkgs" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="增加" Width="60px" CssClass="SmallButton3"
                        OnClick="btnAdd_Click" Visible="False" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="60px" CssClass="SmallButton3"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShipdetail" runat="server" AutoGenerateColumns="False" DataKeyNames="DID,SID_org_con"
            Width="1760px" OnRowCancelingEdit="gvShipdetail_RowCancelingEdit" OnRowDeleting="gvShipdetail_RowDeleting"
            OnRowEditing="gvShipdetail_RowEditing" OnRowUpdating="gvShipdetail_RowUpdating"
            OnRowCommand="gvShipdetail_RowCommand" OnRowDataBound="gvShipdetail_RowDataBound"
            CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="no" HeaderText="No." ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Series">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSNO" runat="server" CssClass="SmallTextBox" Text='<%# Bind("SNO") %>'
                            Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <%#Eval("SNO")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QAD" HeaderText="Material Code" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Shipped Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQtyset" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_set") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qty_set")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Boxes Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtbox" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_box") %>'
                            Width="50px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <%#Eval("qty_box")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Inspection No.">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtqa" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qa") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qa")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="so_nbr" HeaderText="Sales Order" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="so_line" HeaderText="Line number" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Batch No.">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtwo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("wo") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("wo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TCP Orders">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtpo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("po") %>'
                            Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <%#Eval("po")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cust_part" HeaderText="Customer Material" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Weight">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtweight" runat="server" CssClass="SmallTextBox" Text='<%# Bind("weight") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("weight")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Volume">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtvolume" runat="server" CssClass="SmallTextBox" Text='<%# Bind("volume") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("volume")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtprice" runat="server" CssClass="SmallTextBox" Text='<%# Bind("price") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("price")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price1">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtprice1" runat="server" CssClass="SmallTextBox" Text='<%# Bind("price1") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("price1")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price Type">
                    <EditItemTemplate>
                        <asp:Label ID="lblptype" Text='<%# Bind("ptype") %>' runat="server" Visible="false"></asp:Label>
                        <asp:DropDownList ID="drpPtype" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem>SETS</asp:ListItem>
                            <asp:ListItem>PCS</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("ptype")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="Set">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtpkgs" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pkgs") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("pkgs")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pcs">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtqtypcs" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_pcs") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qty_pcs")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fedx" HeaderText="Fedex" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fob" HeaderText="Customer Order" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Adds" Text="<u>详细</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:ButtonField>
                <asp:BoundField DataField="atl" HeaderText="ATL Order" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Remarks">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtmemo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("memo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemTemplate>
                        <%#Eval("memo")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
