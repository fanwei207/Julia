﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Part_ItemUpdateList.aspx.cs" Inherits="part_Part_ItemUpdateList" %>

<!DOCTYPE html>

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
    <div>
            <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1050px;">
                <tr class="main_top">
                    <td class="main_left"></td>
                    <td style="height: 1px">
                    申请单：<asp:TextBox ID="txt_nbr" runat="server" Width="109px"></asp:TextBox>

                         申请人：<asp:TextBox ID="txt_user" runat="server" Width="109px"></asp:TextBox>
                       
                     状态：
                        <asp:DropDownList ID="ddl_status" runat="server">
                            <asp:ListItem  Text="--"  Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem  Text="待提交"  Value="0"></asp:ListItem>
                            <asp:ListItem  Text="待确认"  Value="10"></asp:ListItem>
                            <asp:ListItem  Text="已完成"  Value="20"></asp:ListItem>
                           <asp:ListItem  Text="已取消"  Value="-10"></asp:ListItem>
                            <asp:ListItem  Text="已拒绝"  Value="-20"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="只看自己" />
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                            Text="查询" Width="50px" />
                          <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" 
                            Text="新增" Width="50px" OnClick="btnNew_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                 PageSize="30"
                CssClass="GridViewStyle GridViewRebuild" DataKeyNames="id" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowCommand="gvlist_RowCommand" >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                   <asp:BoundField HeaderText="申请单号" DataField="nbr">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
               
                    <asp:BoundField HeaderText="备注" DataField="remark">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="状态" DataField="status">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="申请人" DataField="createname">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="申请时间" DataField="createdate">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </asp:BoundField>
                      <asp:BoundField HeaderText="通过人" DataField="passName">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="通过时间" DataField="passDate">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </asp:BoundField>
                   <asp:TemplateField HeaderText ="审核">
                        <ItemTemplate>
                            <asp:LinkButton ID="ltnConfirm" Text="detail" ForeColor="Blue" Font-Size="12px" runat="server"
                                CommandName="Confirm" />
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   
                </Columns>
            </asp:GridView>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
</body>
</html>
