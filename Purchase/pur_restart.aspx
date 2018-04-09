<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_restart.aspx.cs" Inherits="Purchase_pur_restart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[type='checkbox'][id='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {
                $("#gvList input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 531px;
        }
        .auto-style2 {
            width: 94px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">         
        <table id="table1" cellspacing="0" cellpadding="0">
            <tr>
                <td class="auto-style1" >
                    采购单号:
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    供应商:<asp:TextBox ID="txtVend" runat="server" CssClass="SmallTextBox Supplier" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;
                    节点：<asp:DropDownList ID="dropNode" runat="server" DataTextField="Node_Name" 
                        DataValueField="Node_Id">
                    </asp:DropDownList>
                 </td>
                 <td align="left" class="auto-style2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="80"
                    OnClick="btnSearch_Click" />
                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    重启原因:
                    <asp:TextBox ID="txtReason" runat="server" CssClass="SmallTextBox" Width="399px"></asp:TextBox>
                    
                  </td>
                 <td align="left" class="auto-style2">
                     <asp:Button ID="btnRestart" runat="server" CssClass="SmallButton3" Text="重启本节点" Width="80"
                    OnClick="btnRestart_Click" />

                </td>
                <td  align="left">
                    <font color="red">重启本节点：在审批中拒绝后，需要重新审批。但不需要前置审批节点重新操作。</font>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                </td>
                <td align="left" class="auto-style2">
                   <asp:Button ID="btnRestartAll" runat="server" CssClass="SmallButton3" Text="重启前置节点" Width="80"
                    OnClick="btnRestartAll_Click" />        
                </td>
                <td  align="left">
                     <font color="red">重启前置节点：在审批中拒绝后，需要前置审批节点重新操作。</font>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ID"  OnPageIndexChanging="gvList_PageIndexChanging" Width="930px" CssClass="GridViewStyle GridViewRebuild">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input id="chkAll" type="checkbox">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" Enabled='<%# Eval("checkstatus") %>'/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="pov_nbr" HeaderText="采购单号">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="pov_vend" HeaderText="供应商">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="供应商名称">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="pov_site" HeaderText="地点">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="pov_domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="pov_ord_date" HeaderText="订货日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pov_ord_date" HeaderText="订货日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
