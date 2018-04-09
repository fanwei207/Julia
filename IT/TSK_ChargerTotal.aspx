<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_ChargerTotal.aspx.cs" Inherits="IT_TSK_ChargerTotal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">
    <div align="left">
        <table style="width: 1020px">
            <tr>
                <td style="width: 80px">
                    任务时间：
                </td>
                <td align="left">
                    <%-- <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    --<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>--%>年<asp:TextBox ID="txb_year" runat="server" Width="50"
                        TabIndex="3" Height="22" MaxLength="4" Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                    月<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22" MaxLength="2"
                        Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                &nbsp;&nbsp;
                    任务人：<asp:DropDownList ID="dropTracker" runat="server" Width="100px" DataTextField="userName"
                        DataValueField="userID" CssClass="Param">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkNotDis" runat="server" Checked="True" Text="仅未完成" 
                        CssClass="Param" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" 
                        onclick="btnSearch_Click"  />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" 
                        onclick="btnBack_Click"  />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            PageSize="20" AllowPaging="True" Width="2800px" 
            onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound"
            >
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
           
            
        </asp:GridView>
    </div>
    </form>
     <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

