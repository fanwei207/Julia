<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Microsoft.ApplicationBlocks.Data" %>
<%@ Import Namespace="System.Data" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_anal" CodeFile="ws_anal.aspx.vb"
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
                        &nbsp;����<asp:TextBox ID="txb_date" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox><asp:TextBox
                            ID="txb_date2" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox>--<asp:TextBox
                                ID="txb_date3" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox><asp:TextBox
                                    ID="txb_date4" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        <asp:DropDownList ID="ddl_site" runat="server" Width="100px">
                            <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">��ǿ�� ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">����ǿ�� YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">�Ϻ����� SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddl_cc" runat="server" Width="120px">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddl_line" runat="server" Width="120px">
                        </asp:DropDownList>
                        &nbsp; �����<asp:TextBox ID="txb_part" runat="server" Width="110" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        <asp:DropDownList ID="ddl_type" runat="server" Width="50px">
                            <asp:ListItem Selected="true" Value="1">Сʱ</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="3">��</asp:ListItem>
                            <asp:ListItem Selected="false" Value="4">��</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="ˢ��"
                            TabIndex="4"></asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <script language="JavaScript" src="/js/diagram.js"></script>
        <!--<iframe name="fDiagram" frameBorder="0" width="0" height="220"></iframe>-->
        <script language="JavaScript">
//_DiagramTarget=fDiagram;
//with (_DiagramTarget.document)
{
    var num = new Array();
    var num2 = new Array();
    var high=100;
 
	   <%
       Dim sql as string 
       Dim ds As DataSet
       Dim val As decimal
       Dim i As Integer
       

       sql="SELECT dispx, case when isnull(qty,0)=0 then 0 else (1 - qty_bad / qty) * 100 end from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is not null and line is null order by id"
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
               num[<%=i%>]=<%=val%> ;
               if(num[<%=i%>]>high) num[<%=i%>]=high;
               if(num[<%=i%>]<0) num[<%=i%>]=0;
       <%                  
             Next
          End If
       End With
       ds.Reset()

       sql="SELECT dispx, case when isnull(qty,0)=0 then 0 else (1 - qty_bad / qty) * 100 end from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is not null and line is not null order by id"
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
               num2[<%=i%>]=<%=val%> ;
               if(num2[<%=i%>]>high) num2[<%=i%>]=high;
               if(num2[<%=i%>]<0) num2[<%=i%>]=0;
       <%                  
             Next
          End If
       End With
       ds.Reset()

       %>

    
	document.open();  
	var D=new Diagram();
	D.SetFrame(150, 120, 850, 320);
	D.SetBorder(0,24, 0, high);
	D.SetText("<%=ddl_type.SelectedItem.text%>","�ϸ���", "���ڷ�Χ��һ���и�Сʱ��ƽ���ϸ�������ͼ");
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
	var tmpmark2=0;
	var tmpmark3=0;
	
    for (j=0;j<24;j++)
    {
      tmpmark2=tmpmark3;
      tmpmark3=num2[j];
      new Line(D.ScreenX(j), D.ScreenY(tmpmark2), D.ScreenX(j+1), D.ScreenY(tmpmark3), "ff0000", 2,num2[j]);
      tmpmark0=tmpmark1;
      tmpmark1=num[j];
      new Line(D.ScreenX(j), D.ScreenY(tmpmark0), D.ScreenX(j+1), D.ScreenY(tmpmark1), "0000ff", 2,num[j]);
    }
	j=D.ScreenX(0)+610;
	new Bar(j, y0-10, j+220, y0+10, '', 'Сʱ', '#663300');

	j=D.ScreenX(0)-20;
	y0=y0-110;
	new Bar(j, y0-110, j+50, y0+10, '', '�ϸ���', '#663300');
	
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
    var num2 = new Array();
    var high=0;
 
	   <%

       sql="SELECT dispx, qty / case when isOK=0 then 1 else isOK end  from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is not null and line is null order by id"
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
               num[<%=i%>]=<%=val%> ;
               if(num[<%=i%>]>high) high=num[<%=i%>];
       <%                  
             Next
          End If
       End With
       ds.Reset()
       
       sql="SELECT dispx, qty / case when isOK=0 then 1 else isOK end  from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is not null and line is not null order by id"
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
               num2[<%=i%>]=<%=val%> ;
               if(num2[<%=i%>]>high) high=num2[<%=i%>];
       <%                  
             Next
          End If
       End With
       ds.Reset()
       %>

    
	document.open();  
	var D=new Diagram();
	D.SetFrame(150, 370, 850, 570);
	D.SetBorder(0,24, 0, high);
	D.SetText("<%=ddl_type.SelectedItem.text%>","����", "���ڷ�Χ��һ���и�Сʱ��ƽ����������ͼ");
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
    var tmpmark2=0;
	var tmpmark3=0;
	
    for (j=0;j<24;j++)
    {
      tmpmark2=tmpmark3;
      tmpmark3=num2[j];
      new Line(D.ScreenX(j), D.ScreenY(tmpmark2), D.ScreenX(j+1), D.ScreenY(tmpmark3), "ff0000", 2,num2[j]);
      tmpmark0=tmpmark1;
      tmpmark1=num[j];
      new Line(D.ScreenX(j), D.ScreenY(tmpmark0), D.ScreenX(j+1), D.ScreenY(tmpmark1), "0000ff", 2,num[j]);
    }
	j=D.ScreenX(0)+610;
	new Bar(j, y0-10, j+220, y0+10, '', 'Сʱ', '#663300');

	j=D.ScreenX(0)-20;
	y0=y0-110;
	new Bar(j, y0-110, j+50, y0+10, '', '����', '#663300');
	
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
