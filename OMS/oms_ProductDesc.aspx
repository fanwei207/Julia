<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_ProductDesc.aspx.cs" Inherits="OMS_oms_ProductDesc" %>

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
      <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    Item Number
                </td>
                <td align="left">
                    <asp:TextBox ID="txtItemNumber" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    UPC number
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUPCNumber" runat="server" CssClass="SmallTextBox" Width="122px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
               
                <td align="left">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" Text="Query" Visible="True"
                        Width="60px" OnClick="btnQuery_Click" /> 
                       &nbsp;
                    <asp:Button ID="btnCreate" runat="server" CssClass="SmallButton2" Text="Create" Visible="True"
                        Width="60px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="25" Width="958px" AllowPaging="True"  DataKeyNames="Id,Item_Number" 
            onrowcommand="gvProduct_RowCommand" 
            onrowdatabound="gvProduct_RowDataBound" 
            onpageindexchanging="gvProduct_PageIndexChanging" 
            onrowdeleting="gvProduct_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Item_Number" HeaderText="Item Number">
                    <HeaderStyle Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="UPC_Number" HeaderText="UPC Number">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="Wattage"  HeaderText="Wattage">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Equiv" HeaderText="Equiv">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Lumens"  HeaderText="Lumens">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LPW" HeaderText="LPW">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="CBCPest" HeaderText="CBCPest.">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="BeamAngle" HeaderText="BeamAngle">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="CCT" HeaderText="CCT">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="CRI" HeaderText="CRI">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="MOL" HeaderText="MOL">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Dia" HeaderText="Dia">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="IP" HeaderText="IP">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                 <asp:BoundField DataField="MP" HeaderText="MP">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="List" HeaderText="List">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="A4 Price">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                 <asp:BoundField DataField="STK/MTO" HeaderText="Price">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="LM79/Life" HeaderText="STK/MTO">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="A4" HeaderText="LM79 / Life">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="IES" HeaderText="IES">
                    <HeaderStyle Width="50px" />
                 </asp:BoundField>
               <asp:BoundField DataField="UL" HeaderText="UL ">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="LDL" HeaderText="LDL ">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="EnergyStar" HeaderText="Energy Star">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Model#" HeaderText="Model #">
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="CautionStatement" HeaderText="Caution Statement">
                    <HeaderStyle Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="CountryOfOrigin" HeaderText="Country of Origin">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="pf" HeaderText="pf">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="DateCode" HeaderText="Date Code">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="UL_File#" HeaderText="UL File #">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="UL_Control#" HeaderText="UL Control#">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="UL_Group" HeaderText="UL Group">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Voltage" HeaderText="Voltage">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Frequency" HeaderText="Frequency">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Amperage" HeaderText="Amperage">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                 <%--  <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>--%>
<%--                  <asp:TemplateField>
                     <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" Text="delete" ForeColor="Black" Font-Underline="false" OnClientClick="return confirm('Are you sure you want to delete?');"
                            Font-Size="11px" runat="server"  CommandName="productDelete"  CommandArgument='<%# Bind("Id") %>'/>
                    </ItemTemplate>
                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma,Arial" Font-Size="8pt"
                        ForeColor="White" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="11px" />
                </asp:TemplateField>--%>
               <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
               </asp:CommandField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
