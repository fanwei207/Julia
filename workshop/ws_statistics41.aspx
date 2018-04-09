<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Microsoft.ApplicationBlocks.Data " %>
<%@ Import Namespace="System.Data" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_statistics41" CodeFile="ws_statistics41.aspx.vb"
    EnableEventValidation="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <asp:Panel ID="Panel2" runat="server" Width="980px" HorizontalAlign="Left" BorderWidth="1px"
            BorderColor="Black" Height="30px">
            <table id="table1" cellspacing="0" cellpadding="0" width="980">
                <tr>
                    <td>
                        &nbsp;日期<asp:TextBox ID="txb_date" runat="server" Width="80" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        公司<asp:DropDownList ID="ddl_site" runat="server" Width="120px" AutoPostBack="True">
                            <asp:ListItem Selected="true" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 成本中心<asp:DropDownList ID="ddl_cc" runat="server" Width="120px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp; 工段线<asp:DropDownList ID="ddl_line" runat="server" Width="120px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp; 零件号<asp:TextBox ID="txb_part" runat="server" Width="110" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        <asp:DropDownList ID="ddl_type" runat="server" Width="50px">
                            <asp:ListItem Selected="true" Value="1">小时</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">天</asp:ListItem>
                            <asp:ListItem Selected="false" Value="3">周</asp:ListItem>
                            <asp:ListItem Selected="false" Value="4">月</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">年</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                            TabIndex="4"></asp:Button>&nbsp;
                        <asp:Button ID="btn_back" runat="server" Width="40" CssClass="SmallButton3" Text="返回"
                            TabIndex="4"></asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <script language="JavaScript" src="/js/diagram.js"></script>
        <!--<iframe name="fDiagram" frameBorder="0" width="0" height="220"></iframe>-->
        <script language="JavaScript" type="text/javascript">
//_DiagramTarget=fDiagram;
//with (_DiagramTarget.document)
{
    var num = new Array();
    var high=100;
 
	   <%
       Dim sql as string 
       Dim ds As DataSet
       Dim val As decimal
       Dim i As Integer

       sql="SELECT dispx, case when isnull(qty,0)=0 then 0 else (1 - qty_bad / qty) * 100 end from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' order by id"
       ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql)
       With ds.Tables(0)
          If .Rows.Count > 0 Then
             For i = 0 To .Rows.Count - 1
               if not isdbnull(.Rows(i).Item(1)) then 
                 val=Convert.ToDecimal(.Rows(i).Item(1))
               else
                 val=0   
               end if
       %>
               num[<%=i%>]=<%=val%>;
               if(num[<%=i%>]>high) num[<%=i%>]=high;
       <%                  
             Next
          End If
       End With
       ds.Reset()
       %>

    
	document.open();  
	var D=new Diagram();
		D.SetFrame(150, 120, 850, 320);
	D.SetBorder(1,13, 0, high);
	D.SetText("<%=ddl_type.SelectedItem.text%>","合格率", "一次合格率");
	//D.XScale="%";
	//D.YScale="%";
    //	D.Draw("#FFEECC", "#663300", false);
	//D.Draw("#FFFFFF", "#663300", false, "Click here to refresh graph", "DiagramClick()");
    //D.Draw("#FFFFFF", "#663300", false);
    D.Draw("#FFEECC", "#663300", false);
    var y0=D.ScreenY(0);
    var j,y;
    var tmpt=0;
    var tmpmark0=0;
	var tmpmark1=0;
	
    for (j=1;j<13;j++)
    {
      tmpmark0=tmpmark1;
      tmpmark1=num[j];
      //new Line(D.ScreenX(j), D.ScreenY(tmpmark0), D.ScreenX(j+1), D.ScreenY(tmpmark1), "ff0000", 2,num[j]);
      new Box(D.ScreenX(j+1), D.ScreenY(tmpmark1), D.ScreenX(j+2), D.ScreenY(0), "ff0000",'','','','',num[j]);
    }
	j=D.ScreenX(1)+610;
	new Bar(j, y0-10, j+220, y0+10, '', '<%=ddl_type.SelectedItem.text%>', '#663300');
	j=D.ScreenX(1)-20;
	y0=y0-110;
	new Bar(j, y0-110, j+50, y0+10, '', '合格率', '#663300');
	
    document.close();
    function DiagramClick()
    { 
    }
 }
        </script>
        <script language="JavaScript">
//_DiagramTarget=fDiagram;
//with (_DiagramTarget.document)
{
    var num = new Array();
    var high=0;
 
	   <%
     

         sql="SELECT dispx, isnull(qty,0) from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' order by id"
       ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql)
       With ds.Tables(0)
          If .Rows.Count > 0 Then
             For i = 0 To .Rows.Count - 1
               if not isdbnull(.Rows(i).Item(1)) then 
                 val=Convert.ToDecimal(.Rows(i).Item(1))
               else
                 val=0   
               end if
       %>
               num[<%=i%>]=<%=val%>;
               if(num[<%=i%>]>high) high=num[<%=i%>];
       <%                  
             Next
          End If
       End With
       ds.Reset()
       %>

    
	document.open();  
	var D=new Diagram();
	D.SetFrame(150, 370, 850, 670);
	D.SetBorder(1,13, 0, high);
	D.SetText("<%=ddl_type.SelectedItem.text%>","流量", "流水线流量");
	//D.XScale="%";
	//D.YScale="%";
    //	D.Draw("#FFEECC", "#663300", false);
	//D.Draw("#FFFFFF", "#663300", false, "Click here to refresh graph", "DiagramClick()");
    //D.Draw("#FFFFFF", "#663300", false);
    D.Draw("#FFEECC", "#663300", false);
    var y0=D.ScreenY(0);
    var j,y;
    var tmpt=0;
    var tmpmark0=0;
	var tmpmark1=0;
	
    for (j=1;j<13;j++)
    {
      tmpmark0=tmpmark1;
      tmpmark1=num[j];
      //new Line(D.ScreenX(j), D.ScreenY(tmpmark0), D.ScreenX(j+1), D.ScreenY(tmpmark1), "0000ff", 2,num[j]);
      new Box(D.ScreenX(j+1), D.ScreenY(tmpmark1), D.ScreenX(j+2), D.ScreenY(0), "0000ff",'','','','',num[j]);
    }
	j=D.ScreenX(1)+610;
	new Bar(j, y0-10, j+220, y0+10, '', '<%=ddl_type.SelectedItem.text%>', '#663300');
	j=D.ScreenX(1)-20;
	y0=y0-210;
	new Bar(j, y0-110, j+50, y0+10, '', '流量', '#663300');
	
    document.close();
    function DiagramClick()
    { 
    }
 }
        </script>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
