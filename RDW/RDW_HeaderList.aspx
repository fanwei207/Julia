<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_HeaderList.aspx.cs" Inherits="RDW_HeaderList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 163px;
        }
        .auto-style1 {
            height: 37px;
        }
        .auto-style2 {
            height: 37px;
            width: 176px;
        }
        .auto-style3 {
            width: 176px;
        }
        .auto-style4 {
            height: 37px;
            width: 159px;
        }
        .auto-style5 {
            width: 159px;
        }
        .auto-style6 {
            height: 37px;
            width: 135px;
        }
        .auto-style7 {
            height: 37px;
            width: 120px;
        }
        .auto-style8 {
            height: 37px;
            width: 75px;
        }
        .auto-style9 {
            height: 37px;
            width: 165px;
        }
        .auto-style11 {
            width: 74px;
        }
        .auto-style12 {
            height: 37px;
            width: 74px;
        }
        .Param {}
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="1200px" bgcolor="white" border="0" >
            <tr>
                 <td class="auto-style2">
                    Project Name<asp:TextBox ID="txtProject" runat="server" Width="100px" CssClass="SmallTextBox Param"
                        TabIndex="1"></asp:TextBox>
                </td>
            <td class="auto-style4">
                    Project Code<asp:TextBox ID="txtProjectCode" runat="server" Width="90px" CssClass="SmallTextBox Param"
                        TabIndex="1"></asp:TextBox>
                </td>
            <td class="auto-style7">Region<asp:DropDownList ID="ddl_region" runat="server" DataTextField="cate_region" CssClass="Param"
                        DataValueField="cate_region" Width="80px">
                </asp:DropDownList>
            </td>        
            <td class="auto-style8">Type<asp:DropDownList ID="ddl_type" runat="server"  Width="50px" 
                   style="margin-top: 0px" DataValueField="RDW_typeID"  DataTextField="RDW_typeName" CssClass="Param">
               </asp:DropDownList>
             </td>
            <td class="auto-style9">
             Project Category<asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id" CssClass="Param"
                            Width="80px">
                        </asp:DropDownList>
            </td>
                <td class="auto-style6">
                    LampType<asp:TextBox ID="txtLampType" runat="server" Width="80px" TabIndex="3"
                        CssClass="SmallTextBox Param"></asp:TextBox>
                </td>
           
            
            
            <td align="center" class="auto-style12" >
                    <asp:DropDownList ID="dropSKU" runat="server" DataTextField="SKU" DataValueField="SKU"
                        Width="0px" Visible="false" Height="0px">
                    </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Query"
                        Width="70px" OnClick="btnQuery_Click" />                    
           </td>
                <td >
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Add"
                        Width="70px" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnComments" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Import Comments"
                        Width="100px" OnClick="btnComments_Click"  />

                </td>
                
            
            </tr>
            <tr>
                <td class="auto-style3">
                    Start Date&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtStartDate" runat="server" Width="101px" TabIndex="3"
                        CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    Status&nbsp;
                    <asp:DropDownList ID="dropStatus" runat="server" DataTextField="SKU" DataValueField="SKU" CssClass="Param"
                        Width="115px"> 
                        <asp:ListItem Value="--">--</asp:ListItem>
                        <asp:ListItem Value="PROCESS" Selected="True">In Process</asp:ListItem>
                        <asp:ListItem Value="SUSPEND">Suspend</asp:ListItem>
                        <asp:ListItem Value="CLOSE">Close</asp:ListItem>
                        <asp:ListItem Value="CANCEL">Cancel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4" >
                        Keywords&nbsp;<asp:TextBox ID="txtKeyword" runat="server" Width="448px" CssClass="SmallTextBox Param"
                        TabIndex="10" ></asp:TextBox>
                </td>
                 
                <td align="center" class="auto-style11">
                        <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" 
                        TabIndex="9" Text="Export"
                        Width="70px" onclick="btnExport_Click"   />
                    
                </td>
                <td>
                    <asp:Button ID="btnExportWithQad" runat="server" CssClass="SmallButton2" 
                            TabIndex="9" Text="Export(QAD)"
                        Width="70px" onclick="btnExportWithQad_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvRDW" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvRDW_PreRender" DataKeyNames="RDW_PPAMstrID,RDW_MstrID,RDW_Project,RDW_ProdCode,RDW_OldID,RDW_Type,RDW_EcnCode"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            Width="1880px" OnRowCommand="gvRDW_RowCommand" OnRowDeleting="gvRDW_RowDeleting">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1080px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="No." Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Project Name" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Project Code" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Start Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="End Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Creator" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Complete Date" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Task" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Status" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Cancel" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Delete" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Description" Width="230px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Notes" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="NO.">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RDW_TypeName" HeaderText="Project Type">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Category" HeaderText="Project Categpry">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_priority" HeaderText="Priority">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Project" HeaderText="Project Name">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_EStarDLC" HeaderText="EStar/DLC" Visible="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_ProdCode" HeaderText="Project Code">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_LampType" HeaderText="LampType">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_PPA" HeaderText="PPA">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Stage" HeaderText="Stage">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_StartDate" HeaderText="Start Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_EndDate" HeaderText="End Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Creater" HeaderText="Creator">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_FinishDate" HeaderText="Complete Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Status" HeaderText="Status" Visible="false">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField DataTextField="RDW_Status" HeaderText="Status" CommandName="EditStatus" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="View" HeaderText="Detail" CommandName="Detail" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="View" HeaderText="PPA Files" CommandName="PPA" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="View" HeaderText="Doc" CommandName="gobom" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="View" HeaderText="QAD" CommandName="goQAD" ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField HeaderText="Cancel" Visible="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnStop" runat="server" Text="Cancel" ForeColor="Black" CommandName="Stop"
                            Font-Underline="true" CommandArgument='<%# Eval("RDW_MstrID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete"
                            Font-Underline="true" CommandArgument='<%# Eval("RDW_MstrID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RDW_ProdSKU" HeaderText="SKU#" Visible="false">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_ProdDesc" HeaderText="Description">
                    <HeaderStyle Width="470px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="470px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Memo" HeaderText="Notes">
                    <HeaderStyle Width="330px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="330px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="RDW_Remark" HeaderText="Reasons">
                    <HeaderStyle Width="330px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="330px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
