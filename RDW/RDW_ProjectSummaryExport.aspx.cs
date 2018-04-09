using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
 

public partial class RDW_ProjectSummaryExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Put user code to initialize the page here
        PIMasteryRow("<b>Project</b>", "<b>Project Code</b>", "<b>Project Desc</b>", "<b>Project StartDate</b>", "<b>Project Createddate</b>", "<b>Project CreatedName</b>",
             "<b>Current TaskId</b>", "<b>Current StepName</b>", "<b>Step StartDate</b>", "<b>Step Description</b>",
             "<b>SKU#</b>", "<b>Product Category</b>", "<b>Lumens</b>", "<b>Voltage</b>", "<b>Wattage</b>", "<b>BeamAngle</b>", "<b>CCT</b>", "<b>CRT</b>", "<b>LPW</b>", "<b>Driver Type</b>",
             "<b>STKorMTO</b>", "<b>UPC</b>", "<b>UL</b>", "<b>Sku Createtor</b>", "<b>Notes</b>");
       
        BindData(); 
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AddHeader("content-disposition", "attachment; filename=ProjectSummary"+ System.DateTime.Now.ToString()+".xls");
        Response.ContentType = "application/vnd.ms-excel";
         
    } 
private void BindData()
{
    string strProj = Request.QueryString["proj"].ToString();
    string strProjcode = Request.QueryString["projcode"].ToString();
    string strSku = Request.QueryString["sku"].ToString();
    string strStart = Request.QueryString["date1"].ToString();

    string strSQL = "sp_RDW_SelectProjectSummary"; 
    SqlParameter[] param = new SqlParameter[7];
    param[0] = new SqlParameter("@proj", strProj);
    param[1] = new SqlParameter("@prod", strProjcode);
    param[2] = new SqlParameter("@sku", strSku);
    param[3] = new SqlParameter("@start", strStart);
    param[4] = new SqlParameter("@status", "PROCESS");
    param[5] = new SqlParameter("@uid", Convert.ToString(Session["uID"]));
    param[6] = new SqlParameter("@viewall", true);

    DataTable dt = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strSQL, param).Tables[0];
    for (int i = 0; i < dt.Rows.Count; i++)
    {
        string _Project = dt.Rows[i]["RDW_Project"].ToString();
        string _ProjectCode = dt.Rows[i]["RDW_ProdCode"].ToString();
        string _ProjectDesc = dt.Rows[i]["RDW_ProdDesc"].ToString();
        string _ProjStartDate = dt.Rows[i]["RDW_StartDate"].ToString();
        string _ProjCreatedDate = dt.Rows[i]["RDW_CreatedDate"].ToString();
        string _ProjCreatedName = dt.Rows[i]["CreatedName"].ToString();
        string _ProjStepNO = dt.Rows[i]["RDW_TaskID"].ToString();
        string _ProjStepName = dt.Rows[i]["StepName"].ToString();
        string _StepStartDate = dt.Rows[i]["RDW_StartDate"].ToString();
        string _StepDesc = dt.Rows[i]["RDW_StepDesc"].ToString();
        string _SKU = dt.Rows[i]["RDW_ProdSKU"].ToString();
        string _Category = dt.Rows[i]["ProductCategory"].ToString();
        string _Lumens = dt.Rows[i]["Lumens"].ToString();
        string _Voltage = dt.Rows[i]["Voltage"].ToString();
        string _Wattage = dt.Rows[i]["Wattage"].ToString();
        string _BeamAngle = dt.Rows[i]["BeamAngle"].ToString();
        string _CCT = dt.Rows[i]["CCT"].ToString();
        string _CRI = dt.Rows[i]["CRI"].ToString();
        string _DriverType = dt.Rows[i]["DriverType"].ToString();
        string _LEDChipType = dt.Rows[i]["LEDChipType"].ToString();
        string _STKorMTO = dt.Rows[i]["STKorMTO"].ToString();
        string _UPC = dt.Rows[i]["UPC"].ToString();
        string _UL = dt.Rows[i]["UL"].ToString();
        string _CreateUser = dt.Rows[i]["CreateUser"].ToString();
        string _Notes = dt.Rows[i]["rdw_memo"].ToString();
        PIMasteryRow(_Project, _ProjectCode, _ProjectDesc,_ProjStartDate, _ProjCreatedDate, _ProjCreatedName, _ProjStepNO, _ProjStepName, _StepStartDate, _StepDesc,
                _SKU, _Category, _Lumens, _Voltage, _Wattage, _BeamAngle, _CCT, _CRI, _DriverType, _LEDChipType, _STKorMTO, _UPC, _UL, _CreateUser,_Notes);

    }

}

private void PIMasteryRow(string _Project, string _ProjectCode, string _ProjectDesc, string _ProjStartDate, string _ProjCreatedDate, string _ProjCreatedName, string _ProjStepNO, string _ProjStepName, string _StepStartDate, string _StepDesc, 
    string _SKU, string _Category, string _Lumens, string _Voltage, string _Wattage, string _BeamAngle, string _CCT, string _CRI, string _DriverType, string _LEDChipType,
    string _STKorMTO, string _UPC, string _UL, string _CreateUser,string _Notes)
{
    TableRow row = new TableRow();
    row.BackColor = System.Drawing.Color.White;
    row.HorizontalAlign = HorizontalAlign.Left;
    row.BorderWidth = new Unit(0);

    TableCell cell = new TableCell(); 
    if (string.IsNullOrEmpty(_Project))
    {
        cell.Text = "";
    }
    else
    {
        cell.Text = _Project;
    }
    cell.Width = new Unit(150);
    row.Cells.Add(cell);

    TableCell cell2= new TableCell();
    if (string.IsNullOrEmpty(_ProjectCode))
    {
        cell2.Text = "";
    }
    else
    {
        cell2.Text = _ProjectCode;
    }
    cell2.Width = new Unit(120);
    row.Cells.Add(cell2);

    TableCell cell3 = new TableCell();
    if (string.IsNullOrEmpty(_ProjectDesc))
    {
        cell3.Text = "";
    }
    else
    {
        cell3.Text = _ProjectDesc;
    }
    cell3.Width = new Unit(200);
    row.Cells.Add(cell3);

    TableCell cell4_1 = new TableCell();
    if (string.IsNullOrEmpty(_ProjStartDate))
    {
        cell4_1.Text = "";
    }
    else
    {
        cell4_1.Text = string.Format("{0:yyyy-MM-dd}", _ProjStartDate);
    }
    cell4_1.Width = new Unit(120);
    row.Cells.Add(cell4_1);

    TableCell cell4 = new TableCell();
    if (string.IsNullOrEmpty(_ProjCreatedDate))
    {
        cell4.Text = "";
    }
    else
    {
        cell4.Text = string.Format("{0:yyyy-MM-dd HH:MM:ss}", _ProjCreatedDate);
    }
    cell4.Width = new Unit(120); 
    row.Cells.Add(cell4); 
     
    TableCell cell5 = new TableCell();
    if (string.IsNullOrEmpty(_ProjCreatedName))
    {
        cell5.Text = "";
    }
    else
    {
        cell5.Text = _ProjCreatedName;
    }
    cell5.Width = new Unit(100);
    row.Cells.Add(cell5);

    TableCell cell6 = new TableCell();
    if (string.IsNullOrEmpty(_ProjStepNO))
    {
        cell6.Text = "";
    }
    else
    {
        cell6.Text = _ProjStepNO;
    }
    cell6.Width = new Unit(80);
    row.Cells.Add(cell6);

    TableCell cell7 = new TableCell();
    if (string.IsNullOrEmpty(_ProjStepName))
    {
        cell7.Text = "";
    }
    else
    {
        cell7.Text = _ProjStepName;
    }
    cell7.Width = new Unit(160);
    row.Cells.Add(cell7);
     
    TableCell cell8 = new TableCell();
    if (string.IsNullOrEmpty(_StepStartDate))
    {
        cell8.Text = "";
    }
    else
    {
        cell8.Text = string.Format( "{0:yyyy-MM-dd HH:MM:ss}",_StepStartDate);
    }
    cell8.Width = new Unit(100);
    row.Cells.Add(cell8);

    TableCell cell9 = new TableCell();
    if (string.IsNullOrEmpty(_StepDesc))
    {
        cell9.Text = "";
    }
    else
    {
        cell9.Text = _StepDesc;
    }
    cell9.Width = new Unit(200);
    row.Cells.Add(cell9);

    TableCell cell10 = new TableCell();
    if (string.IsNullOrEmpty(_SKU))
    {
        cell10.Text = "";
    }
    else
    {
        cell10.Text = _SKU;
    }
    cell10.Width = new Unit(100);
    row.Cells.Add(cell10);
   
    TableCell cell11 = new TableCell();
    if (string.IsNullOrEmpty(_Category))
    {
        cell11.Text = "";
    }
    else
    {
        cell11.Text = _Category;
    }
    cell11.Width = new Unit(100);
    row.Cells.Add(cell11);

    TableCell cell12 = new TableCell();
    if (string.IsNullOrEmpty(_Lumens))
    {
        cell12.Text = "";
    }
    else
    {
        cell12.Text = _Lumens;
    }
    cell12.Width = new Unit(100);
    row.Cells.Add(cell12);

    TableCell cell13 = new TableCell();
    if (string.IsNullOrEmpty(_Voltage))
    {
        cell13.Text = "";
    }
    else
    {
        cell13.Text = _Voltage;
    }
    cell13.Width = new Unit(100);
    row.Cells.Add(cell13);

    TableCell cell14 = new TableCell();
    if (string.IsNullOrEmpty(_Wattage))
    {
        cell14.Text = "";
    }
    else
    {
        cell14.Text = _Wattage;
    }
    cell14.Width = new Unit(100);
    row.Cells.Add(cell14);
     
    TableCell cell15 = new TableCell();
    if (string.IsNullOrEmpty(_BeamAngle))
    {
        cell15.Text = "";
    }
    else
    {
        cell15.Text = _BeamAngle;
    }
    cell15.Width = new Unit(100);
    row.Cells.Add(cell15);

    TableCell cell16 = new TableCell();
    if (string.IsNullOrEmpty(_CCT))
    {
        cell16.Text = "";
    }
    else
    {
        cell16.Text = _CCT;
    }
    cell16.Width = new Unit(100);
    row.Cells.Add(cell16);

    TableCell cell17 = new TableCell();
    if (string.IsNullOrEmpty(_CRI))
    {
        cell17.Text = "";
    }
    else
    {
        cell17.Text = _CRI;
    }
    cell17.Width = new Unit(100);
    row.Cells.Add(cell17);
   
    TableCell cell18 = new TableCell();
    if (string.IsNullOrEmpty(_DriverType))
    {
        cell18.Text = "";
    }
    else
    {
        cell18.Text = _DriverType;
    }
    cell18.Width = new Unit(100);
    row.Cells.Add(cell18);

    TableCell cell19 = new TableCell();
    if (string.IsNullOrEmpty(_LEDChipType))
    {
        cell19.Text = "";
    }
    else
    {
        cell19.Text = _LEDChipType;
    }
    cell19.Width = new Unit(100);
    row.Cells.Add(cell19);
   
    TableCell cell20= new TableCell();
    if (string.IsNullOrEmpty(_STKorMTO))
    {
        cell20.Text = "";
    }
    else
    {
        cell20.Text = _STKorMTO;
    }
    cell20.Width = new Unit(100);
    row.Cells.Add(cell20);

    TableCell cell21 = new TableCell();
    if (string.IsNullOrEmpty(_UPC))
    {
        cell21.Text = "";
    }
    else
    {
        cell21.Text = _UPC;
    }
    cell21.Width = new Unit(100);
    row.Cells.Add(cell21);
    //   string _UL, string _CreateUser)
    TableCell cell22 = new TableCell();
    if (string.IsNullOrEmpty(_UL))
    {
        cell22.Text = "";
    }
    else
    {
        cell22.Text = _UL;
    }
    cell22.Width = new Unit(100);
    row.Cells.Add(cell22);

    TableCell cell23 = new TableCell();
    if (string.IsNullOrEmpty(_CreateUser))
    {
        cell23.Text = "";
    }
    else
    {
        cell23.Text = _CreateUser;
    }
    cell23.Width = new Unit(100);
    row.Cells.Add(cell23);

    TableCell cell24 = new TableCell();
    if (string.IsNullOrEmpty(_Notes))
    {
        cell24.Text = "";
    }
    else
    {
        cell24.Text = _Notes;
    }
    cell24.Width = new Unit(100);
    row.Cells.Add(cell24); 

    exl.Rows.Add(row);
}

 
}