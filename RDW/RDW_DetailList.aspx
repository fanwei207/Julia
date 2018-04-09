<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_DetailList.aspx.cs" Inherits="RDW_DetailList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .SmallTextBox {}
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="980" bgcolor="white" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblProject0" runat="server" Width="100px" CssClass="LabelRight" Text="Project Category:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                        <asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id"
                            Width="250px">
                        </asp:DropDownList>
                </td>
                <td valign="top" align="right" class="style3">
                    <asp:Label ID="lblStatus" runat="server" Width="86px" CssClass="LabelRight" Text="Status:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td  valign="top" align="left">
                    <asp:DropDownList ID="dropStatus" runat="server" DataTextField="SKU" DataValueField="SKU"
                        Width="150px">
                        <asp:ListItem Value="PROCESS">In Process</asp:ListItem>
                        <asp:ListItem Value="SUSPEND">Suspend</asp:ListItem>
                        <asp:ListItem Value="CLOSE">Close</asp:ListItem>
                        <asp:ListItem Value="CANCEL">Cancel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="top" align="right" class="style3">
                    <asp:Label ID="Label5" runat="server" Width="85px" CssClass="LabelRight" Text="Engineer Team:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_ET" runat="server" Width="110px">
                        <asp:ListItem Value="--" >--</asp:ListItem>
                        <asp:ListItem Value="SZX">SZX</asp:ListItem>
                        <asp:ListItem Value="ZQL">ZQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td rowspan="5" align="left" valign="top">
                    <asp:Label ID="lblPartner" runat="server" Width="80px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProject" runat="server" Width="75px" CssClass="LabelRight" Text="Project Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProject" runat="server" CssClass="SmallTextBox" Width="250px"
                        TabIndex="1" MaxLength="50" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="Label6" runat="server" Width="75px" CssClass="LabelRight" Text="Priority:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" >
                    <asp:TextBox ID="txt_priority" runat="server" CssClass="SmallTextBox" Width="150px"
                        TabIndex="1" MaxLength="50" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="Label7" runat="server" Width="75px" CssClass="LabelRight" Text="Customer:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" style="width:110px">
                    <asp:TextBox ID="txt_customer" runat="server" CssClass="SmallTextBox" Width="110px"
                        TabIndex="1" MaxLength="50" ValidationGroup="chkAll"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Width="75px" CssClass="LabelRight" Text="Project Code:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProdCode" runat="server" CssClass="SmallTextBox" Width="250px"
                        TabIndex="7" MaxLength="250" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="lbProdCode" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Width="75px" CssClass="LabelRight" Text="Lamp Type:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_lamptype" runat="server" CssClass="SmallTextBox" Width="150px"
                        TabIndex="1" MaxLength="50" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="Label8" runat="server" Width="75px" CssClass="LabelRight" Text="Tier:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" style="width:110px">
                    <asp:DropDownList ID="ddl_tier" runat="server" Width="110px">
                            <asp:ListItem Value="--" Selected="True">--</asp:ListItem>
                            <asp:ListItem Value="Innovative" >Innovative</asp:ListItem>
                            <asp:ListItem Value="Standard">Standard</asp:ListItem>
                            <asp:ListItem Value="Value">Value</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProdCode" runat="server" Width="75px" CssClass="LabelRight" Text="SKU#:"
                        Font-Bold="False" Visible="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropSKU" runat="server" DataTextField="SKU" DataValueField="SKU"
                        Width="250px" Visible="false">
                    </asp:DropDownList>
                </td>
                             
            </tr>
            <tr>
                <td align="right">PPA:</td>
                <td>
                    <asp:TextBox ID="txtPPA" runat="server" CssClass="SmallTextBox" Width="250px"
                        TabIndex="7" MaxLength="250"></asp:TextBox>
                    <asp:Label ID="lblPPAMstrID" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Width="75px" CssClass="LabelRight" Text="EStar/DLC:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddl_EStarDLC" runat="server"
                        Width="150px">
                        <asp:ListItem Value="--" >--</asp:ListItem>
                        <asp:ListItem Value="EStar">EStar</asp:ListItem>
                        <asp:ListItem Value="DLC">DLC</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProdDesc" runat="server" Width="75px" CssClass="LabelRight" Text="Description:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProdDesc" runat="server" CssClass="SmallTextBox" Width="250px"
                        TabIndex="3" MaxLength="250" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td rowspan="4" valign="top" align="right" class="style3">
                    <asp:Label ID="lblStandard" runat="server" Width="86px" CssClass="LabelRight" Text="key specification:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td rowspan="5" colspan="3" valign="top" align="left">
                    <asp:TextBox ID="txtStandard" runat="server" CssClass="SmallTextBox" Width="421px"
                        TabIndex="7" TextMode="MultiLine" MaxLength="500" Height="120px"></asp:TextBox>
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblPM" runat="server" Width="80px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>                    
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblStartDate" runat="server" Width="75px" CssClass="LabelRight" Text="Start Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="SmallTextBox EnglishDate"
                        Width="250px" TabIndex="4" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblEndDate" runat="server" Width="75px" CssClass="LabelRight" Text="End Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox EnglishDate" Width="250px"
                        TabIndex="5" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lbl_mgr" runat="server" Width="95px" CssClass="LabelRight" Text="Product Manager:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtMGR" CssClass="SmallTextBox" Width="250px" Enabled="false"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td align="right">
                    <asp:Label ID="lblMemo" runat="server" Width="75px" CssClass="LabelRight" Text="Notes:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;
                    
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMemo" runat="server" CssClass="SmallTextBox" Width="250px" TabIndex="6"
                        MaxLength="200" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    Comments:
                </td>
                <td>
                    <asp:LinkButton ID="lkbtnProjectArgue" runat="server" Text="Comments" OnClick="lkbtnProjectArgue_Click" ></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lbl_ecnCode" runat="server" Width="75px" CssClass="LabelRight" Text="ECN Code:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:LinkButton runat="server" ID="lbt_ecnCode" Text="ECN" OnClick="lbt_ecnCode_Click"></asp:LinkButton>
                    <asp:Label runat="server" ID="lbl_type" Width="0px" Visible="false"></asp:Label>
                </td>
                <td align="center" class="style3">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Save"
                        Width="50px" ValidationGroup="chkAll" CausesValidation="true" OnClick="btnSave_Click" />
                </td>
                <td align="center"width="220px" colspan="3" >
                    <asp:Button ID="btnLeader" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Viewer"
                        Width="50px" CausesValidation="false" OnClick="btnLeader_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnPM" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Leader"
                        Width="50px" CausesValidation="false" OnClick="btnPM_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btn_mgr" runat="server" CssClass="SmallButton2" TabIndex="10" Text="PM"
                        Width="50px" CausesValidation="false" OnClick="btn_mgr_Click"/>&nbsp;&nbsp;
                    <asp:Button ID="btnQAD" runat="server" CssClass="SmallButton2" TabIndex="10" Text="QAD" Visible="false"
                        Width="50px" CausesValidation="false" onclick="btnQAD_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSample" runat="server" CssClass="SmallButton2" TabIndex="10" Text="Sample"
                        Width="50px" CausesValidation="false" onclick="btnSample_Click" />&nbsp;&nbsp;
                    <asp:Button ID="txt_ppa" runat="server" CssClass="SmallButton2" TabIndex="10" Text="PPA"
                        Width="50px" CausesValidation="false" OnClick="txt_ppa_Click"  />
                    <asp:Button ID="btnStep" runat="server" CssClass="SmallButton2" TabIndex="10" Text="Step" Visible="false"
                        Width="50px" CausesValidation="false" OnClick="btnStep_Click" />&nbsp;&nbsp;
                     <asp:Button ID="btnUL" runat="server" CssClass="SmallButton2" TabIndex="10" Text="UL"
                        Width="50px" CausesValidation="false" OnClick="btnUL_Click" Visible="false" />　
                     <asp:Button ID="btnPCB" runat="server" CssClass="SmallButton2" TabIndex="10" Text="PCB"
                        Width="50px" CausesValidation="false"  OnClick="btnPCB_Click" />
                </td>
                <td align="center" class="style4">
                    <asp:Button ID="btnDoc" runat="server" CssClass="SmallButton2" TabIndex="11" Text="Doc"
                        Width="50px" CausesValidation="false" OnClick="btnDoc_Click" 
                        Height="22px" />
                </td>
                <td style="width: 15%" align="center">
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="12" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <img src="\images\green.gif" width="15" height="10" title="Approved">&nbsp;&nbsp;
                    <asp:Label ID="Green" Text="Approved" runat="server"></asp:Label>&nbsp;&nbsp;
                    <img src="\images\yellow.gif" border="0" width="15" height="10" title="Partial Approved">&nbsp;&nbsp;
                    <asp:Label ID="Yellow" Text="Partial Approved" runat="server"></asp:Label>&nbsp;&nbsp;
                    <img src="\images\red.gif" border="0" width="15" height="10" title="Expired">&nbsp;&nbsp;
                    <asp:Label ID="Label1" Text="Expired" runat="server"></asp:Label>&nbsp;&nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkClosed" runat="server" Text="已关闭" Visible="false" AutoPostBack="true" OnCheckedChanged="chkClosed_CheckedChanged"/>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" Style="overflow: auto; text-align: left;" runat="server" Width="986px"
            BorderWidth="0px" ScrollBars="Auto" Height="330px">
            <asp:GridView ID="gvRDW" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" PageSize="20" OnPreRender="gvRDW_PreRender" OnRowDataBound="gvRDW_RowDataBound"
                Width="980px" DataKeyNames="RDW_DetID,RDW_Sort,RDW_isActive,RDW_isTemp,RDW_Extra,RDW_PredtaskID,RDW_StepActEndDate" OnRowCommand="gvRDW_RowCommand"
                OnRowDeleting="gvRDW_RowDeleting">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Description" Width="370px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="StartDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="EndDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Complete" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Duration" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <%--  <asp:TableCell Text="Pre" Width="40px" HorizontalAlign="center"></asp:TableCell>--%>
                            <asp:TableCell Text="Member" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Approver" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkNo" runat="server" CommandArgument='<%# Bind("RDW_DetId") %>'
                                CommandName="myEdit" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("RDW_StepNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="RDW_StepName" HeaderText="Step Name" HtmlEncode="False">
                        <HeaderStyle Width="350px" HorizontalAlign="Center" />
                        <ItemStyle Width="350px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="Predecessor">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="PredecessorName"  >
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                    <asp:BoundField Datafield="RDW_PredtaskID" HeaderText="TaskID"  Visible="false">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_StepStartDate" HeaderText="StartDate" Visible="false">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_StepEndDate" HeaderText="EndDate">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_StepFinishDate" HeaderText="Complete">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_Duration" HeaderText="Duration" Visible="false">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Member">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkMember" runat="server" CommandArgument='<%# Bind("RDW_DetID") %>'
                                CommandName="Member" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("RDW_PartnerName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" Width="130px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approver" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkApprover" runat="server" CommandArgument='<%# Bind("RDW_DetID") %>'
                                CommandName="Approver" Font-Bold="False" Font-Size="8pt" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("RDW_Evaluater") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" Width="130px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubStep">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkSubTask" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                CommandName="SubTask" Font-Bold="false" ForeColor="Black" Text="SubStep"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detail">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetail" runat="server" Text="Detail" ForeColor="Black" CommandName="Detail"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:GridView ID="gv_oldProject" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" DataKeyNames="ID,RDW_Category,Project,ProdCode,Status,StartDate"
            Width="980px" OnPageIndexChanging="gv_oldProject_PageIndexChanging" OnRowCommand="gv_oldProject_RowCommand" >
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="NO.">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cateName" HeaderText="Project Categpry">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Project" HeaderText="Project Name">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ProdCode" HeaderText="Project Code">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EndDate" HeaderText="End Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Creater" HeaderText="Creator">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FinishDate" HeaderText="Complete Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="View" HeaderText="Detail" CommandName="Detail" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="ProdDesc" HeaderText="Description">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Memo" HeaderText="Notes">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </asp:Panel>

        <asp:CheckBox ID="chkCanEditProject" runat="server" Visible="false" />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
