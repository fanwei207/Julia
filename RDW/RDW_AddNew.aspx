<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddNew.aspx.cs" Inherits="RDW_RDW_AddNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
            <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0" runat="server">
                <tr>
                    <td align="right">Project Type:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList  DataValueField="RDW_typeID"  DataTextField="RDW_typeName"
                            ID="ddl_type" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged" >                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="oldprodname" >
                    <td align="right">Old Project Name:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txt_oldProdName" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" MaxLength="100" ></asp:TextBox>
                    </td>
                </tr>
                <tr id="oldprodcode">
                    <td align="right">Old Project Code:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txt_oldProdCode" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" MaxLength="100" OnTextChanged="txt_oldProdCode_TextChanged" AutoPostBack="true" ></asp:TextBox>
                        <asp:Label runat="server" Visible="false" ID="lbl_oldmID"></asp:Label>
                    </td>
                </tr>
                <tr id="ecnCode">
                    <td align="right">ECN Code:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txt_ecnCode" runat="server" CssClass="SmallTextBox" Width="350px" 
                            TabIndex="1" MaxLength="100" OnTextChanged="txt_ecnCode_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">Project Category:
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id"
                            Width="250px" OnSelectedIndexChanged="dropCatetory_SelectedIndexChanged" AutoPostBack="true"  >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblProject" runat="server" Width="100px" CssClass="LabelRight" Text="Project Name:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txtProject" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" MaxLength="100" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblProdCode" runat="server" Width="100px" CssClass="LabelRight" Text="Project Code:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txtProdCode" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" ValidationGroup="chkAll" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">PPA:</td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtPPA" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblProdDesc" runat="server" CssClass="LabelRight" Text="Product Description:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtProdDesc" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="3" MaxLength="250" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_lampType" runat="server" Width="100px" CssClass="LabelRight" Text="Lamp Type:"
                            Font-Bold="False" ></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox runat="server" ID="txt_lampType" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_ET" runat="server" Width="100px" CssClass="LabelRight" Text="Engineer Team:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList ID="ddl_ET" runat="server" Width="350px">
                            <asp:ListItem Value="--" >--</asp:ListItem>
                            <asp:ListItem Value="SQL" Selected="True">SQL</asp:ListItem>
                            <asp:ListItem Value="ZQL">ZQL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_EStarDLC" runat="server" Width="100px" CssClass="LabelRight" Text="EStar/DLC:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList ID="ddl_EStarDLC" runat="server" Width="350px">
                            <asp:ListItem Value="--" >--</asp:ListItem>
                            <asp:ListItem Value="EStar" Selected="True">EStar</asp:ListItem>
                            <asp:ListItem Value="DLC">DLC</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_customer" runat="server" Width="100px" CssClass="LabelRight" Text="Customer:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        
                        <asp:TextBox runat="server" ID="txt_customer" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_priority" runat="server" Width="100px" CssClass="LabelRight" Text="Priority:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox runat="server" ID="txt_priority" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_tier" runat="server" Width="100px" CssClass="LabelRight" Text="Tier:"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList ID="ddl_tier" runat="server" Width="350px">
                            <asp:ListItem Value="--" Selected="True">--</asp:ListItem>
                            <asp:ListItem Value="Innovative" >Innovative</asp:ListItem>
                            <asp:ListItem Value="Standard">Standard</asp:ListItem>
                            <asp:ListItem Value="Value">Value</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>



                <tr>
                    <td align="right">
                        <asp:Label ID="lblProdSKU" runat="server" Width="100px" CssClass="LabelRight" Text="SKU#:"
                            Font-Bold="False" Visible="false"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:DropDownList ID="dropSKU" runat="server" DataTextField="SKU" DataValueField="SKU"
                            Width="350px" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblModel" runat="server" Width="100px" CssClass="LabelRight" Text="UL Model NO.:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtModel" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStartDate" runat="server" Width="100px" CssClass="LabelRight" Text="Start Date:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="SmallTextBox EnglishDate"
                            Width="350px" TabIndex="4" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblEndDate" runat="server" CssClass="LabelRight" Font-Bold="False"
                            Text="End Date:" Width="100px"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox EnglishDate" onkeydown="event.returnValue=false;"
                            onpaste="return false;" TabIndex="4" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Project Template:"
                            Width="100px"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:DropDownList ID="dropTemplate" runat="server" DataTextField="RDW_Project" DataValueField="RDW_MstrID"
                            Width="350px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStandard" runat="server" Width="100px" CssClass="LabelRight" Text="key specification:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtStandard" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="5" TextMode="MultiLine" MaxLength="500" Height="223px"></asp:TextBox>
                    </td>
                </tr>



                <tr style="display: none;">
                    <td align="right" >
                        <asp:Label ID="lblLEDJXL" runat="server" Width="100px" CssClass="LabelRight" Text="LEDJXL:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtLEDJXL" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td align="right">
                        <asp:Label ID="lblLEDLV" runat="server" Width="100px" CssClass="LabelRight" Text="LEDLV:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtLEDLV" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td align="right">
                        <asp:Label ID="lblDriverJXL" runat="server" Width="100px" CssClass="LabelRight" Text="DriverJXL:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtDriverJXL" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td align="right">
                        <asp:Label ID="lblDriverLv" runat="server" Width="100px" CssClass="LabelRight" Text="DriverLv:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtDriverLv" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMemo" runat="server" Width="100px" CssClass="LabelRight" Text="Notes:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtMemo" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="6"
                            MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                            Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnSave_Click" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Cancel"
                            Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
