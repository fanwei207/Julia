<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetReservation.aspx.cs" Inherits="New_FixAssetReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" border="0" >
            <tr style="height:50px">
                        <td align="center">
                        上次输入日期:<asp:label ID="lblLastInputDate" runat="server" Width="190px"></asp:label>
                        </td>
                        <td style="width:80px;"></td>                   
                        <td align="center">
                         上次折旧截止月份:<asp:label ID="lblLastVouDate" runat="server" Width="130px"></asp:label>
                        </td>                   
            </tr>
            <tr style="height:50px">                
                        <td align="center">
                        输入日期:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtInputDate" runat="server" Width="120px" CssClass="SmallTextBox Date"></asp:TextBox>
                        </td>
                        <td style="width:100px;"></td>
                        <td align="center">
                        折旧截止月份:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVouDate" runat="server" Width="130px"></asp:TextBox>
                        </td>                           
               </tr>
            <tr style="height:100px"> 
                <td style="text-align:center;color:red;font-size:small;" colspan="3">
                    <asp:Button ID="btnReserve" runat="server" Text="预定" CssClass="SmallButton3" OnClick="btnReserve_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;备注:预定成功后，下一天可以导
                </td>
            </tr>
        </table>
    </div>
    </form>
     <div align="center" >
        <table id="reserveInfo" runat="server" cellpadding="0" cellspacing="0" border="1" >
            <tr>
                        <td align="center" style="width:150px;">
                        预定人id
                        </td>                   
                        <td align="center" style="width:150px;">
                        预订人姓名
                        </td>                                                           
                        <td align="center" style="width:150px;">
                        预定输入日期
                        </td>
                        <td align="center" style="width:150px;">
                        预定折旧截止月份
                        </td>  
                        <td align="center" style="width:150px;">
                        预定日期
                        </td>              
               </tr>
            <tr>
                <td align="center">
                    <asp:label ID="lbluID" runat="server" ></asp:label>
                </td>
                <td align="center">
                    <asp:label ID="lbluName"  runat="server" ></asp:label>
                </td>
                <td align="center">
                    <asp:label ID="lblinputDate"  runat="server"></asp:label>
                </td>
                <td align="center">
                    <asp:label ID="lblvouDate" runat="server"></asp:label>
                </td>
                <td align="center">
                    <asp:label ID="lbldate" runat="server"></asp:label>
                </td>                                                                                
            </tr>
            </table>
    </div>
        <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
