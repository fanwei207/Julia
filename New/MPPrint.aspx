<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPPrint.aspx.cs" Inherits="new_MPPrint" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function AllAreaExcel() {
            try {
                var curTbl = document.getElementById("tbPrint");
                var oXL = new ActiveXObject("Excel.Application");
                var oWB = oXL.Workbooks.Add();
                var oSheet = oWB.ActiveSheet;
            } catch (e) {
                alert("创建excel对象失败，请确认已经安装了excel软件!");
                return false;
            }
            var sel = document.body.createTextRange();
            sel.moveToElementText(curTbl);
            sel.select();
            sel.execCommand("Copy");
            oSheet.Paste();
            oXL.Visible = true;

        }  
    </script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <%--<uc1:top id="Top1" runat="server"></uc1:top>--%>
        <br />
        <br />
        <table id="tbPrint" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server" style="font-size: 10pt; height: 193px;" border="1">
            <tr style="height: 23px">
                <td style="height: 36px" colspan="4" align="center">
                    <b>零星采购单</b>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 30px; width: 135px;" align="right">
                    <asp:Label ID="lblDeptName" runat="server" Text="申请部门："></asp:Label>
                </td>
                <td style="height: 30px; width: 230px;" align="Left">
                    <asp:Label ID="lblDept" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 30px; width: 79px;" align="right">
                    <asp:Label ID="lblTypeName" runat="server" Text="零件分类："></asp:Label>
                </td>
                <td style="height: 30px" align="Left">
                    <asp:Label ID="lblType" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 30px; width: 135px;" align="right">
                    <asp:Label ID="lblSPName" runat="server" Text="供应商："></asp:Label>
                </td>
                <td colspan="3" style="height: 30px" align="Left">
                    <asp:Label ID="lblSP" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 30px; width: 135px;" align="right">
                    <asp:Label ID="lblPartName" runat="server" Text=" 零件描述 ："></asp:Label>
                </td>
                <td colspan="3" style="height: 30px" align="Left">
                    <asp:Label ID="lblPart" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 29px; width: 135px;" align="right">
                    <asp:Label ID="lblQtyName" runat="server" Text="数量："></asp:Label>
                </td>
                <td style="height: 29px; width: 230px;" align="Left">
                    <asp:Label ID="lblQuantity" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 29px; width: 79px;" align="right">
                    <asp:Label ID="lblPName" runat="server" Text="单价："></asp:Label>
                </td>
                <td style="height: 29px" align="Left">
                    <asp:Label ID="lblprice" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 29px; width: 135px;" align="right">
                    <asp:Label ID="lblName" runat="server" Text="总价："></asp:Label>
                </td>
                <td colspan="3" style="height: 36px" align="Left">
                    <asp:Label ID="lbltotal" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="Table1" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server" style="font-size: 10pt;">
            <tr style="height: 23px">
                <td colspan="4" style="height: 36px">
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 29px; width: 84px;" align="right">
                    <asp:Label ID="lblAName" runat="server" Text="申请人："></asp:Label>
                </td>
                <td style="height: 26px; width: 230px;" align="Left">
                    <asp:Label ID="lblApper" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 29px; width: 79px;" align="right">
                    <asp:Label ID="lblCName" runat="server" Text=" 审核人："></asp:Label>
                </td>
                <td style="height: 26px" align="Left">
                    <asp:Label ID="lblConform" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        </form>
    </div>
</body>
<script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
</script>
</html>
