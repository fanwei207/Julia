<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Microsoft.ApplicationBlocks.Data" %>
<%@ Import Namespace="System.Data" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_auto_daily" CodeFile="ws_auto_daily.aspx.vb"
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
    <script> 
		<!-- 
		      var limit="0:10" /* min:sec */ 
		      if (document.images)
		      { 
		        var parselimit=limit.split(":") 
		        parselimit=parselimit[0]*60+parselimit[1]*1 
		      } 
		      
		      function beginrefresh()
		      { if (!document.images) 
		          return 
		        if (parselimit==1) 
		          <%
		            if ddl_line.SelectedIndex >= ddl_line.Items.Count-1 then  
		          %>
  		            window.location.href ="/workshop_cs/ws_auto_display2.aspx?dd=" + document.getElementById("txb_date").value + "&site=" + document.getElementById("ddl_site").value + "&cc=" + document.getElementById("ddl_cc").value + "&part=" + document.getElementById("txb_part").value ;
		          <% else %>
		            window.location.href ="/workshop/ws_auto_daily.aspx?dd=" + document.getElementById("txb_date").value + "&site=" + document.getElementById("ddl_site").value + "&cc=" + document.getElementById("ddl_cc").value + "&line=" + document.getElementById("ddl_line").value + "&part=" + document.getElementById("txb_part").value + "&in=y" ;
		          <% end if %> 
		        else
		        { parselimit-=1 
		          /*
		          curmin=Math.floor(parselimit/60) 
		          cursec=parselimit%60 
		          if (curmin!=0) 
		             curtime=curmin+"分"+cursec+"秒后重刷本页！" 
		          else 
		             curtime=cursec+"秒后重刷本页！" 
		          window.status=curtime */
		          setTimeout("beginrefresh()",1000) 
		        } 
		      } 
		       
		      window.onload=beginrefresh 
		//--> 
    </script>
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
                        公司<asp:DropDownList ID="ddl_site" runat="server" Width="120px" Enabled="false">
                            <asp:ListItem Selected="true" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 成本中心<asp:DropDownList ID="ddl_cc" runat="server" Width="120px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp; 工段线<asp:DropDownList ID="ddl_line" runat="server" Width="120px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp; 零件号<asp:TextBox ID="txb_part" runat="server" Width="110" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                            TabIndex="4"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_gexcel" runat="server" Width="40" CssClass="SmallButton3" Text="返回"
                            TabIndex="4"></asp:Button>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <script language="JavaScript" src="/js/diagram.js"></script>
        <!--
				<iframe name="fDiagram" frameBorder="0" width="0" height="220"></iframe>
				-->
        <script language="JavaScript">
//_DiagramTarget=fDiagram;
//with (_DiagramTarget.document)
{
    var num2 = new Array();
    var high=0;
    var num = new Array();
    var high2=0;
 
	   <%
       Dim sql as string 
       Dim ds As DataSet
       Dim val As Decimal
       Dim val2 As Decimal
       Dim i As Integer

       sql="SELECT dispx, qty_bad,qty from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' order by id"
       ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql)
       With ds.Tables(0)
          If .Rows.Count > 0 Then
             For i = 0 To .Rows.Count - 1
               if not isdbnull(.Rows(i).Item(1)) then 
                 val=Convert.ToDecimal(.Rows(i).Item(1))
               else
                 val=0   
               end if
               if not isdbnull(.Rows(i).Item(2)) then 
                 val2=Convert.ToDecimal(.Rows(i).Item(2))
               else
                 val2=0   
               end if
       %>
               num[<%=i%>]=<%=val%>;
               if(num[<%=i%>]>high) high=num[<%=i%>];
               num2[<%=i%>]=<%=val2%>;
               if(num2[<%=i%>]>high2) high2=num2[<%=i%>];
       <%                  
             Next
          End If
       End With
       ds.Reset()
       %>

    
	document.open();  
	var D=new Diagram();
	D.SetFrame(150, 120, 850, 260);
	if (high==0)  high=100; 
  	D.SetBorder(1,97, 0, high);
	D.SetText("分钟","数量", "次品十五分钟流量图");
	//D.XScale="%";
	//D.YScale="%";
    //	D.Draw("#FFEECC", "#663300", false);
	//D.Draw("#FFFFFF", "#663300", false, "Click here to refresh graph", "DiagramClick()");
    //D.Draw("#FFFFFF", "#663300", false);
    D.Draw("#FFEECC", "#663300", false);
    var y0=D.ScreenY(0);
    var j,y;
	var tmpmark3=0;
	
    for (j=1;j<97;j++)
    {
      tmpmark3=num[j];
      new Box(D.ScreenX(j), D.ScreenY(tmpmark3), D.ScreenX(j+1), D.ScreenY(0), "ff0000",'','','','',num[j]);
    }
	j=D.ScreenX(1)+610;
	new Bar(j, y0-10, j+230, y0+10, '', '15分钟', '#663300');

	j=D.ScreenX(1)-20;
	y0=y0-55;
	new Bar(j, y0-110, j+50, y0+10, '', '数量', '#663300');
	
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
                document.open();
                var D = new Diagram();
                D.SetFrame(150, 310, 850, 450);
                if (high2 == 0) high2 = 100;
                D.SetBorder(1, 97, 0, high2);
                D.SetText("分钟", "数量", "正品十五分钟流量图");
                //D.XScale="%";
                //D.YScale="%";
                //	D.Draw("#FFEECC", "#663300", false);
                //D.Draw("#FFFFFF", "#663300", false, "Click here to refresh graph", "DiagramClick()");
                //D.Draw("#FFFFFF", "#663300", false);
                D.Draw("#FFEECC", "#663300", false);
                var y0 = D.ScreenY(0);
                var j, y;
                var tmpmark1 = 0;

                for (j = 1; j < 97; j++) {
                    tmpmark1 = num2[j];
                    new Box(D.ScreenX(j), D.ScreenY(tmpmark1), D.ScreenX(j + 1), D.ScreenY(0), "0000ff", '', '', '', '', num2[j]);
                }
                j = D.ScreenX(1) + 610;
                new Bar(j, y0 - 10, j + 230, y0 + 10, '', '15分钟', '#663300');

                j = D.ScreenX(1) - 20;
                y0 = y0 - 55;
                new Bar(j, y0 - 110, j + 50, y0 + 10, '', '数量', '#663300');

                document.close();
                function DiagramClick() {
                }
            }
        </script>
        <script language="JavaScript">
            //_DiagramTarget=fDiagram;
            //with (_DiagramTarget.document)
            {

                document.open();
                var D = new Diagram();
                D.SetFrame(150, 500, 850, 600);
                D.SetBorder(1, 97, 0, 100);
                D.SetText("分钟", "数量", "十五分钟一次合格率");
                //D.XScale="%";
                //D.YScale="%";
                //	D.Draw("#FFEECC", "#663300", false);
                //D.Draw("#FFFFFF", "#663300", false, "Click here to refresh graph", "DiagramClick()");
                //D.Draw("#FFFFFF", "#663300", false);
                D.Draw("#FFEECC", "#663300", false);
                var y0 = D.ScreenY(0);
                var j, y;
                var tmpmark1 = 0;
                var tmpmark2 = 0;
                var tmpmark3 = 0;

                for (j = 1; j < 97; j++) {
                    tmpmark1 = tmpmark1 + num[j];
                    tmpmark2 = tmpmark2 + num2[j];

                    if (tmpmark2 == 0)
                        tmpmark3 = 0;
                    else
                        tmpmark3 = (1 - tmpmark1 / tmpmark2) * 100;

                    new Box(D.ScreenX(j), D.ScreenY(tmpmark3), D.ScreenX(j + 1), D.ScreenY(0), "00ff00", '', '', '', '', tmpmark3);
                }
                j = D.ScreenX(1) + 610;
                new Bar(j, y0 - 10, j + 230, y0 + 10, '', '15分钟', '#663300');

                j = D.ScreenX(1) - 20;
                y0 = y0 - 15;
                new Bar(j, y0 - 110, j + 50, y0 + 10, '', '数量', '#663300');

                document.close();
                function DiagramClick() {
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
