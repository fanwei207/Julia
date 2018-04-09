<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddDelay.aspx.cs" Inherits="RDW_RDW_AddDelay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">
    <div align="center">
    
    <table style="width: 730px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
             
                <td align="left" style="width: 534px">
                 Planned finish date：
                   <asp:TextBox ID="txbdate" runat="server" Width="154px" CssClass="SmallTextBox Date"
                        MaxLength="30"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="Refresh" CssClass="SmallButton3" 
                        Width="50px" onclick="btnSearch_Click"
                        />
                    &nbsp;
                </td>
             </tr>
             <tr>
              <td align="left" style="width: 534px">
                 The reason for the delay：
                   <asp:TextBox ID="txbRemark" runat="server" Width="339px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="SmallButton3" 
                        Width="50px" onclick="btnAdd_Click"
                         />
                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="SmallButton3" Visible="false"
                        Width="50px" onclick="btnSave_Click"  />
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="SmallButton3"
                        Width="50px" onclick="btnback_Click"  />
                </td>
             </tr>
         </table>
         <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="730px" 
            onpageindexchanging="gv_PageIndexChanging" onrowcommand="gv_RowCommand" onrowdatabound="gv_RowDataBound" 
            
            >
           <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />

            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="730px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="Step Name" Width="80px"></asp:TableCell>
                     
                        <asp:TableCell HorizontalAlign="center" Text="Planned finish date" Width="300px"></asp:TableCell>
                       
                        <asp:TableCell HorizontalAlign="center" Text="reason" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="没有延时记录" ColumnSpan="5"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>

                 <asp:BoundField HeaderText="Step Name  " DataField="RDW_StepName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Planned finish date  " DataField="RDW_delaytime" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
               
                <asp:BoundField HeaderText="reason" DataField="RDW_delayrmk" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="left" Width="300px" />
                </asp:BoundField>

               <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnshowdetupde" Text="<u>Edit</u>"  Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("RDW_DelayId") %>' CommandName="myupdate" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnshowdetdele" Text="<u>Delete</u>"  Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("RDW_DelayId") %>' CommandName="mydelete" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
               
               
            </Columns>
        </asp:GridView>
    </div>
      <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </form>
</body>
</html>
