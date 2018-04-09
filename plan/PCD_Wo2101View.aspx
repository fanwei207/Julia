<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_Wo2101View.aspx.cs" Inherits="plan_PCD_Wo2101View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 1px;
        }
    </style>
</head>
<body>
        <div align="center">
        <form id="form1" runat="server">
            <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 750px;">
                <tr class="main_top">
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="超期" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                          
                        工单号：
                    <asp:TextBox ID="txtWoNbr" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                         工单ID：
                    <asp:TextBox ID="txtWoLot" runat="server" CssClass="smalltextbox Param" Width="104px"></asp:TextBox>
                       物料号：
                        <asp:TextBox
                            ID="txtPart" runat="server" CssClass="smalltextbox Param" Width="122px"></asp:TextBox>
                        
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                            Text="查询" Width="50px" />
                        <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" OnClick="btnExcel_Click"
                            Text="Excel" Width="50px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                 PageSize="30"
                CssClass="GridViewStyle GridViewRebuild" DataKeyNames="wo_nbr,wo_lot" OnPageIndexChanging="gvlist_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="工单号" DataField="wo_nbr">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ID" DataField="wo_lot">
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QAD号" DataField="wo_part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="域" DataField="wo_domain">
                        <HeaderStyle Width="30px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="地点" DataField="wo_site">
                        <HeaderStyle Width="30px" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField HeaderText="截止日期" DataField="wo_due_date" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PCD" DataField="pcd" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="工帽，E型电感" DataField="StrCol11" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="电阻，发光管" DataField="StrCol12" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="二极管" DataField="StrCol13"  DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="电容，环形电感，保险丝" DataField="StrCol14" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="印制板" DataField="StrCol16" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="集成电路，三极管，场效应管" DataField="StrCol17" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
