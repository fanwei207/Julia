<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ProjLog.aspx.cs" Inherits="RDW_ProjLog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table width="1000px" cellspacing="2" cellpadding="2" bgcolor="white" border="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="LabelRight" Text="Project Category:" Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left"><asp:DropDownList ID="dropCatetory" runat="server" DataTextField="cate_name" DataValueField="cate_id" CssClass="Param"
                        Width="200px">
                    </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblProject" runat="server" CssClass="LabelRight" Text="Project" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtProject" runat="server" Width="100px" TabIndex="1" CssClass="Param"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_step" runat="server" Width="70px" CssClass="LabelRight" 
                            Font-Bold="False" Text="Step Name:"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:DropDownList runat="server" ID="ddl_step" Width="210px" DataTextField="RDW_StepName" DataValueField="RDW_Code" CssClass="Param"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbl_status" runat="server" Width="70px"  CssClass="LabelRight" Text="Step Status "
                            Font-Bold="False"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server" CssClass="Param">
                            <asp:ListItem Value="-1" Selected="True">ALL</asp:ListItem>
                            <asp:ListItem Value="2">Finished</asp:ListItem>
                            <asp:ListItem Value="0" >Unfinished</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                    <td>
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                            Width="40px" OnClick="btnQuery_Click" />&nbsp; &nbsp;
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProdCode" runat="server" CssClass="LabelRight" Text="Message" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMessage" runat="server" TabIndex="1" Width="200px" CssClass="Param"></asp:TextBox>&nbsp;
                        
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Creater"
                            Width="46px"></asp:Label>
                        </td>
                    <td>
                        <asp:TextBox ID="txtCreater" runat="server" TabIndex="3" Width="100px" CssClass="Param"></asp:TextBox>&nbsp;
                        
                    
                    </td>
                    <td>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px"  CssClass="LabelRight" Text="Create Date"
                            Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtStartDate" runat="server" Width="100px" TabIndex="3" CssClass="EnglishDate Param"></asp:TextBox>-<asp:TextBox
                            ID="txtEndDate" runat="server" TabIndex="3" Width="100px" CssClass="EnglishDate Param"></asp:TextBox>
                    </td>
                    
                    <td>
                        <asp:Label ID="Label3" runat="server" Width="70px" CssClass="LabelRight" 
                            Font-Bold="False" Text="Read:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Read" runat="server" CssClass="Param">
                            <asp:ListItem Value="-1">--</asp:ListItem>
                            <asp:ListItem Value="1">have read</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">unread</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    
                    <td>
                        <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Excel"
                        Width="40px" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvRDW" runat="server" AllowPaging="True"
                AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle GridViewRebuild" PageSize="22" OnPreRender="gvRDW_PreRender" OnPageIndexChanging="gvRDW_PageIndexChanging"
                Width="1800px" OnRowCommand="gvRDW_RowCommand"
                DataKeyNames="RDW_MstrID,RDW_DetID" OnRowDataBound="gvRDW_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>

                    <asp:TemplateField HeaderText="Project">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Pro" runat="Server" Text='<%#Eval("RDW_Project") %>'
                                CommandName="Project" Font-Underline="true"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RDW_MstrID" HeaderText="RDW_MstrID" Visible="false">
                        <HeaderStyle Width="400px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="400px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rdw_message" HeaderText="Message">
                        <HeaderStyle Width="400px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="400px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDw_createname" HeaderText="Creator">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rdw_createdate" HeaderText="Create Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_TaskID" HeaderText="StepNO.">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Step Name">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_StepName" runat="Server" Text='<%#Eval("rdw_stepname")%>'
                                CommandName="StepName" Font-Underline="true"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="rdw_stepdesc" HeaderText="Step Desc">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rdw_prodcode" HeaderText="Project Code">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rdw_proddesc" HeaderText="Project Desc">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rdw_prodsku" HeaderText="SKU#">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
