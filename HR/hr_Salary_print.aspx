<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Salary_print.aspx.cs"
    Inherits="HR_hr_Salary_print" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <form id="form1" runat="server">
        <br />
        <br />
        <table width="860px" cellspacing="0" cellpadding="0">
            <tr>
                <td align="Left">
                    ���&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; �·�&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="100px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="status" Checked="true" Text="ȫ��"/>
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="status" Text="��ְ"/>
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="status" Text="��ְ"/>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="160px" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropWorkshop" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkshop_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropWorkgroup" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td>
                    �ù�����&nbsp;<asp:DropDownList ID="dropWork" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                <td>
                    ��������&nbsp;<asp:DropDownList ID="dropInsurance" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 30px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="btnPrint" runat="server" CssClass="SmallButton2" Width="100px" Text="��ӡ"
                        OnClick="btnPrint_Click" />
                </td>

            </tr>
            <tr>
                <td colspan="3">
                    <font color="red">ע:1����ְ��ָԱ����ְ���ڴӵ���1��������14�յ���Ա<br/>
                     &nbsp; &nbsp; &nbsp;2����ѡ��ȫ��ʱ�����ӡȫ����Ա�Ĺ���<br/>
                     &nbsp; &nbsp; &nbsp;3����ѡ����ְ����ְʱ���Ὣ��ְ��Ա����ְ��Ա�Ĺ��ʷֿ���ӡ</font>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
