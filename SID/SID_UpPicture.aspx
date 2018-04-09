﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_UpPicture.aspx.cs" Inherits="SID_SID_UpPicture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <%-- <style type="text/css">
        #table2
        {
            width: 400px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblshipid" runat="server" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblsid" runat="server" Text="" Visible="False"></asp:Label>
                   
                </td>
            </tr>
           
          <tr>
                <td>
            <asp:GridView ID="gvSID" runat="server"  AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" DataKeyNames="SID,SID_Status,SID_ShipId" 
                       Width="980px" >
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="√" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系统货运单号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="参考" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运单号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="装箱地点" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出厂日期" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运输方式" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运往" Width="400px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="域" Width="30px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
               
                <asp:BoundField DataField="PK" HeaderText="系统货运单号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PKRef" HeaderText="参考">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nbr" HeaderText="出运单号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Site" HeaderText="装箱地点">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="出厂日期">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Via" HeaderText="运输方式">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Shipto" HeaderText="运往">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="域">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                
            </Columns>
        </asp:GridView>                  
                </td>
            </tr>
             <tr>
               <td style="vertical-align: middle; text-align: center;">
                  <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="920px"  DataKeyNames="SID_url,SID_ShipId" 
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="图片" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="备注" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="创建人" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                           
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                    </asp:TemplateField>

              
                     <asp:BoundField DataField="SID_name" HeaderText="照片">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SID_remark" HeaderText="备注">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle Width="300px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SID_createdate" HeaderText="上传时间">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                 
                </Columns>
            </asp:GridView>
                </td>
           </tr>
           </table>
           <table>
            <tr>
                <td style=" text-align:left;">
                    照片:
                   </td>
                </tr>
                <tr>
                    <td style="width: 700px; text-align:left;">
                        <input id="file1" style="width: 100%; height: 23px" type="file" size="45" name="filename1"
                            runat="server" />
                    </td>
            </tr>
            <tr>
                <td style="width: 700px; text-align:left;">
                    &nbsp;照片备注:<asp:TextBox ID="txt_tracking" runat="server" Height="200px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td style="vertical-align: middle; text-align: center;">
                    <asp:Button ID="btn_submit" runat="server" Text="Submit" 
                        CssClass="SmallButton2" onclick="btn_submit_Click" Width="50px"
                        />
                         &nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btn_back" runat="server" Text="Back"  Width="50px"
                        CssClass="SmallButton2" onclick="btn_back_Click" 
                        />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
