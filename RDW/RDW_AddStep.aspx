<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddStep.aspx.cs" Inherits="RDW_AddStep" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $( function () {
            
            var _index = $("#hidTabIndex").val();

            var $tabs = $("#divTabs").tabs({ active: _index });

           $("#chb_all").click(function(){
               $("#<%=ckb_projectStep.ClientID %> input[type=checkbox]").prop("checked", $(this).prop("checked"));
               
            });
            
        })

    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div align="center">
            <div id="divTabs">
                <div id="head" runat="server">
                    <ul>
                        <li><a href="#tabs1">&nbsp;&nbsp;Add/Copy Step&nbsp;&nbsp;</a></li>
                        <li><a href="#tabs2">&nbsp;&nbsp;Copy Steps&nbsp;&nbsp;</a></li>
                        <li>
                            <input id="hidTabIndex" type="hidden" value="0" runat="server" />
                        </li>
                    </ul>
                </div>
                <div id="tabs1">
                    <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0">
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lblprojectname" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Project Name:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:TextBox ID="txbprojectname" runat="server" CssClass="SmallTextBox"
                                    Width="350px" TabIndex="1" MaxLength="80" AutoPostBack="true"
                                    OnTextChanged="txbprojectname_TextChanged"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lblprojectcode" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Project Code:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:TextBox ID="txbprojectcode" runat="server" CssClass="SmallTextBox"
                                    Width="350px" TabIndex="1" MaxLength="80" AutoPostBack="true"
                                    OnTextChanged="txbprojectcode_TextChanged"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lblstep" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Copy From Step:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:DropDownList ID="ddlprojectstep" runat="server" Height="16px" Width="249px" DataTextField="StepDesc" DataValueField="RDW_DetID">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="Label3" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Place Before:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddlSiblingStep" runat="server" Height="16px" Width="249px" DataTextField="StepDesc" DataValueField="RDW_DetID">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr id="trPredecessor" runat="server">
                            <td align="right" class="style1">
                                <asp:Label ID="Label4" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Predecessor:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="dropPredecessor" runat="server" Height="16px" Width="249px" DataTextField="StepDesc" DataValueField="RDW_DetID"
                                    AutoPostBack="true" OnSelectedIndexChanged="dropPredecessor_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lblDescription" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Step Name:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <textarea id="txtDescription" class="SmallTextBox"
                                    style="height: 210px; width: 490px;" runat="server" cols="20" name="S1"
                                    rows="1"></textarea><br />
                                (1000 Characters)</td>
                        </tr>
                        <tr style="display:none">
                            <td align="right" class="style1">
                                <asp:Label ID="lblDuration" runat="server" Width="100px" CssClass="LabelRight" Text="Duration :"
                                    Font-Bold="False"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtDuration" runat="server" CssClass="SmallTextBox" Width="46px"
                                    TabIndex="3" MaxLength="250" AutoPostBack="true" OnTextChanged="txtDuration_TextChanged"></asp:TextBox>(Days)
                            </td>
                        </tr>
                        <tr  style="display:none">
                            <td align="right" class="style1">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Start Date :"
                                    Width="100px"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="SmallTextBox EnglishDate" MaxLength="10"
                                    TabIndex="1" Width="150px" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelRight" Font-Bold="False" Text="End Date :"
                                    Width="100px"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox EnglishDate" MaxLength="10"
                                    TabIndex="1" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lblexterStep" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Extra Step:"
                                    Width="100px"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:CheckBox ID="ckbExtra" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style2">
                                <asp:Label ID="lblComments" runat="server" CssClass="LabelRight" Font-Bold="False"
                                    Text="Description:" Width="100px"></asp:Label>
                            </td>
                            <td align="left" colspan="2" style="height: 24px">
                                <asp:TextBox ID="txtComments" runat="server" CssClass="SmallTextBox" Height="86px"
                                    TabIndex="1" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1"></td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lbMID" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1"></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                                    Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnSave_Click" />
                                &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tabs2" runat="server">
                    <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0">
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lbl_projectname" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Project Name:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:TextBox ID="txb_projectname" runat="server" CssClass="SmallTextBox"
                                    Width="350px" TabIndex="1" MaxLength="80" AutoPostBack="true"
                                    OnTextChanged="txb_projectname_TextChanged"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lbl_projectcode" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Project Code:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:TextBox ID="txb_projectcode" runat="server" CssClass="SmallTextBox"
                                    Width="350px" TabIndex="1" MaxLength="80" AutoPostBack="true"
                                    OnTextChanged="txb_projectcode_TextChanged"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="lbl_step" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Copy From Step:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="Left" colspan="2">
                                <asp:CheckBoxList runat="server" ID="ckb_projectStep" DataTextField="StepDesc" DataValueField="RDW_DetID" Width="249px"></asp:CheckBoxList>
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td align="right" class="style1"></td>
                            <td align="Left" colspan="3">
                                <asp:CheckBox runat="server" Text="全选" ID="chb_all" />
                            </td>
                        </tr>
                        <tr id="trStepStep" runat="server" >
                            <td align="right" class="style1">
                                <asp:Label ID="Label8" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Place Before:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddl_placebefore" runat="server" Height="16px" Width="249px" DataTextField="StepDesc" DataValueField="RDW_DetID">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr id="tr1" runat="server" visible="false">
                            <td align="right" class="style1">
                                <asp:Label ID="Label9" runat="server" Width="106px" CssClass="LabelRight"
                                    Text="Predecessor:" Font-Bold="False"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddl_Predecessor" runat="server" Height="16px" Width="249px" DataTextField="StepDesc" DataValueField="RDW_DetID"
                                    AutoPostBack="true" OnSelectedIndexChanged="dropPredecessor_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1"></td>
                            <td>
                                <asp:Button ID="btn_Save" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                                    Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btn_Save_Click" />
                                &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                                    Width="50px" CausesValidation="false" OnClick="btnBack_Click" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
