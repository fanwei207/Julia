<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_ResponsibleParty.aspx.cs" Inherits="rmInspection_GuestComplaint_ResponsibleParty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客诉-责任方信息维护</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" width="900px" bgcolor="white" border="0">
            <tr> 
                <td>
                    <asp:Label ID="lblDuty" runat="server" Width="55px" CssClass="LabelRight" Text="责任方:"
                        Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtDuty" runat="server" Width="50px" TabIndex="1" MaxLength="30"
                        CssClass="smallTextBox"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lblId" runat="server" Text="Label" Visible="false"></asp:Label>                                       
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Width="55px" CssClass="LabelRight" Text="责任人:"
                        Font-Bold="False"></asp:Label>                   
                    <asp:TextBox ID="txtResponsiblePerson" runat="server"  Height="20px" 
                        Width="150px"></asp:TextBox>
                    <asp:Button ID="btnResponsiblePerson" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnResponsiblePerson_Click" Text="选择" />
                    <asp:HiddenField ID="lblResponsiblePersonId" runat="server" />
                </td>
<%--                <td>
                    <asp:Label ID="lblEndtime" runat="server" Width="55px" CssClass="LabelRight" Text="截止时间:"
                        Font-Bold="False"></asp:Label>                   
                    <asp:TextBox ID="txtEndtime" runat="server"  Height="20px" Width="150px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="查询"
                        Width="40px" OnClick="btnQuery_Click" />&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="新增"
                        Width="40px" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="gv" runat="server" Width="920px" CssClass="GridViewStyle AutoPageSize" 
            AllowPaging="True" PageSize="25" DataKeyField="ID"  AutoGenerateColumns="False" 
            onitemcommand="gv_ItemCommand" onpageindexchanged="gv_PageIndexChanged">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="DutyName" ReadOnly="True" HeaderText="<b>责任方</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ResponsiblePersonName" ReadOnly="True" HeaderText="<b>责任人</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="responsiblePersonId" HeaderText="<b>责任人</b>" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CreatedName" ReadOnly="True" HeaderText="<b>创建者</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Createdtime" HeaderText="<b>创建时间</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Endtime" visible="false" HeaderText="<b>截止时间</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" CommandName="UpdateBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>取消</u>" CommandName="CancelBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
    </div>
     <script type="text/javascript">
         <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
