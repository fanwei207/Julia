<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_DetailEdit.aspx.cs" Inherits="RDW_DetailEdit" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function(){
        
            $("#btnTrack").click(function(){
                //
                var _mid = $("#hidMID").val();
                var _did = $("#hidDID").val();          
                var _projName = $("#lblProjectData").text();         
                var _projCode = $("#lblProdCodeData").text();
                var _src = "/RDW/prod_Report.aspx?from=rdw&code=" + _projCode + "&mid=" +　_mid　+ "&did=" +　_did + "&name=" + _projName;
                $.window("样品试流申请单列表", "95%", "90%", _src, "", true);

                return false;
            });
            //end btnTrack
            $("#btnTest").click(function(){
                //
                var _mid = $("#hidMID").val();
                var _did = $("#hidDID").val();          
                var _projName = $("#lblProjectData").text();         
                var _projCode = $("#lblProdCodeData").text();
                var _src = "/RDW/Test_Report.aspx?from=rdw&projectcode=" + _projCode + "&mid=" +　_mid　+ "&did=" +　_did + "&projectname=" + _projName;
                $.window("可靠性测试项目列表", "95%", "90%", _src, "", true);

                return false;
            });

        })

    </script>
    
    <style type="text/css">
        .style1
        {
            width: 84px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <asp:HiddenField ID="hidMID" runat="server" />
            <asp:HiddenField ID="hidDID" runat="server" />
            <asp:HiddenField ID="hidStepTitle" runat="server" />
        <table cellspacing="2" cellpadding="2" width="800px" bgcolor="white" border="0">
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProject" runat="server" Width="100px" CssClass="LabelRight" Text="Project Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblProjectData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style1">
                    <asp:Label ID="lblProdCode" runat="server" Width="100px" CssClass="LabelRight" Text="Product Code:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblProdCodeData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                    <asp:Label ID="lblprojStatus" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProdDesc" runat="server" Width="100px" CssClass="LabelRight" Text="Description:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:Label ID="lblProdDescData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblStartDate" runat="server" Width="100px" CssClass="LabelRight" Text="Product Start Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblStartDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
               <td align="right" class="style1">
                    <asp:Label ID="lblEndDate" runat="server" Width="100px" CssClass="LabelRight" Text="Product End Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblEndDateData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblStepName" runat="server" Width="100px" CssClass="LabelRight" Text="Step Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:Label ID="lblStepNameData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblStepDesc" runat="server" Width="100px" CssClass="LabelRight" Text="Step Description:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:TextBox ID="txtStepData" runat="server" CssClass="TextLeft" TextMode="MultiLine"
                        Width="500px" BorderWidth="0px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblStepEnd" runat="server" Width="100px" CssClass="LabelRight" Text="Step End Date:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblStepEndData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right" class="style1">
                    <asp:Label ID="lblStepStart" runat="server" Width="100px" CssClass="LabelRight" Text="Step Start Date:"
                        Font-Bold="false" Visible="false"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="lblStepStartData" runat="server" CssClass="LabelLeft" Font-Bold="false" Visible="false"></asp:Label>
                </td>

            </tr>
            <tr id="upload" runat="server">
                <td align="right" class="style1">
                    <asp:Label ID="lblUpload" runat="server" Width="100px" CssClass="LabelRight" Text="File Upload:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="5">
                    &nbsp;<Upload:InputFile ID="fileAttachFile" runat="server" Width="400px" />
                    (*Max 100M)</td>
                <td align="center">
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="Upload" OnClick="btnUpload_Click"
                        Width="60px"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="BtnDoc" runat="server" CssClass="SmallButton2" Text="From Docs" 
                        Width="60px" onclick="BtnDoc_Click"></asp:Button>        
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    File Support: </td>
                <td align="left" colspan="7">
                    .txt, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .pdf, .eml, .ai, .dwg, .pcb, 
                    .pcbdoc, .p, .sch, .schdoc, .rar, .zip, .jpg, .jpeg, .png, .gif, .bmp, .chm</td>
            </tr>
            <tr id="notes" runat="server">
                <td align="right" class="style1">
                    <asp:Label ID="lblNotes" runat="server" Width="100px" CssClass="LabelRight" Text="Notes:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="SmallTextBox" Width="520px" MaxLength="500"
                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="lblMemo" runat="server" CssClass="LabelLeft" Text="Max(500)" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr id="trCancelReason" runat="server">
                <td align="right" class="style1">
                    <asp:Label ID="Label1" runat="server" Width="100px" CssClass="LabelRight" Text="Cancel Finish Reason:"
                        Font-Bold="False" Height="40px"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="SmallTextBox" Width="520px"
                        MaxLength="180" TextMode="MultiLine" Height="42px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" CssClass="LabelLeft" Text="Max(200)" Font-Bold="false"></asp:Label>
                </td>
            </tr>
             <tr >
                 <td align="right" class="style1">
               
                        <asp:Label ID="lbldelaydateshow" runat="server" Width="100px" CssClass="LabelRight" Text="Delay Date:"
                            Font-Bold="False"  Visible="False"></asp:Label>
                        
                    </td>
                    <td  class="style1" colspan="2">
                        <asp:Label ID="lbldelaydate" runat="server" Width="100px" CssClass="LabelLift" Text="Delay Date"
                            Font-Bold="False"  Visible="False"></asp:Label>
                    </td>
                    <td  align="right" class="style1">                   
                      <asp:Label ID="lbldelayreasonshow" runat="server" Width="100px" 
                             CssClass="LabelRight" Text="Delay Reason:"
                            Font-Bold="False"  Visible="False"></asp:Label>
                    </td>
                    <td  class="style1" colspan="3">
                            <asp:Label ID="lbldelayreason" runat="server" Width="100px" 
                             CssClass="LabelLift" Text="Delay Reason"
                            Font-Bold="False"  Visible="False"></asp:Label>
                    </td>
                     <td align="center" class="style1">
                
                        <asp:Button ID="btndelay" runat="server" CssClass="SmallButton2" Text="delay"
                        Width="70px" onclick="btndelay_Click" Visible="False"  />
                    </td>
             </tr>
            <tr align="center">
                <td class="style1">
                    <asp:LinkButton ID="lnkMessage" runat="server" Font-Underline="True" OnClick="lnkMessage_Click">Show Message List</asp:LinkButton>
                </td>
                <td style="width: 84px" align="center">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="Save" Width="50px"
                        OnClick="btnSave_Click" />
                </td>
                <td style="width: 84px" align="center">
                    <asp:Button ID="btnFinish" runat="server" CssClass="SmallButton2" Text="Member Finish"
                        Width="85px" OnClick="btnFinish_Click" />&nbsp;
                    <asp:Button ID="btnCancelFinish" runat="server" CssClass="SmallButton2" Text="Cancel Finish"
                        Width="85px" OnClick="btnCancelFinish_Click" />
                </td>
                <td style="width: 84px" align="center">
                    <asp:Button ID="btn_disApprove" runat="server" CssClass="SmallButton2" Text="DisApprove" 
                    Width="85px" OnClick="btn_disApprove_Click" Visible="false"  />
                </td>
                <td style="width: 84px" align="center">
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="Back" Width="50px"
                        OnClick="btnBack_Click" />
                </td>           
                <td style="width: 84px" align="center">
                       <asp:Button ID="btn_Doc" runat="server" CssClass="SmallButton2" Text="Doc" Width="50px" OnClick="btn_Doc_Click"
                        />
                </td>
                 <td align="center" class="style1">
                    
                    
                 <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" Visible="false"/>
                </td>
                <td>
                   <asp:Button ID="btnSample" runat="server" CssClass="SmallButton2" 
                        Text="Link Sample" Width="80px" onclick="btnSample_Click" />
                   <asp:Button ID="btnTrack" runat="server" CssClass="SmallButton2" 
                        Text="Track" Width="80px" OnClick="btnTrack_Click" />
                   <asp:Button ID="btnTest" runat="server" CssClass="SmallButton2" 
                        Text="Test" Width="80px" OnClick="btnTest_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="8" valign="top" align="center">
                    <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowDataBound="gvUpload_RowDataBound"
                        OnRowCommand="gvUpload_RowCommand" OnRowDeleting="gvUpload_RowDeleting" 
                        DataKeyNames="RDW_DocsID,RDW_DetID,RDW_Path,RDW_UploaderID,RDW_PhysicalName,RDW_TransferStatus,RDW_fromDocID,RDW_FileName">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Attach File Name" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Upload Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Delete" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text=""   Width="50px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="RDW_FileName" HeaderText="Attach File Name">
                                <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                <ItemStyle Width="540px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_Uploader" HeaderText="Upload User">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RDW_UploadDate" HeaderText="Upload Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Delete">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkTransfer" runat="server" CommandName="Transfer" Font-Underline="true"
                                        CommandArgument='<%# Eval("RDW_DocsID") + "," + Eval("RDW_TransferStatus") + "," + Eval("RDW_DetID") %>' ForeColor="Black"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td colspan="8" valign="top" align="center">
            <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        CssClass="GridViewStyle" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="1" DataKeyNames="id" GridLines="Vertical" Height="20px" PageSize="5" Width="800px" OnRowCommand="gvlist_RowCommand">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="bos_docfileName" HeaderText="FileName">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docfileDescs" HeaderText="File Description">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docUploadby" HeaderText="Uploader">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_docUploadDate" HeaderText="UploadTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                            </asp:BoundField>
                    
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkstep" runat="server" CommandArgument='<%# Eval("bos_vend") + "," + Eval("bos_docId") + "," + Eval("bos_path") + "," + Eval("bos_transferStatus") %>'
                                        CommandName="download" Font-Size="12px" Font-Underline="true" ForeColor="Black"
                                        Text="View"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                    
                        </Columns>
                    </asp:GridView>
            </td>
            </tr>
            <tr>
                <td colspan="8" valign="top" align="center">
                    <asp:GridView ID="gvMessage" runat="server" AllowPaging="False" AllowSorting="True"
                        AutoGenerateColumns="False" Width="800px" CssClass="GridViewStyle">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="true" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Message List" Width="800px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="RDW_Message" HeaderText="Message List" HtmlEncode="false">
                                <HeaderStyle Width="800px" HorizontalAlign="Center" />
                                <ItemStyle Width="800px" HorizontalAlign="Left" Wrap="true"/>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
