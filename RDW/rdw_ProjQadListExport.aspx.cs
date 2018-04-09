using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using RD_WorkFlow;

public partial class RDW_rdw_ProjQadListExport : System.Web.UI.Page
{
    string mid = "";
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {  
            if (Request.QueryString["pqmid"] != null)
            {
                mid = Request.QueryString["pqmid"].ToString();
            }

        PIMasteryRow("<b>Project Category</b>", "<b>Project</b>", "<b>Project Code</b>", "<b>QAD</b>", "<b>QAD Desc</b>");
     
        BindData();

        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AddHeader("content-disposition", "attachment; filename=ProjectQADLIst" + System.DateTime.Now.ToString() + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
       
        }
    }
    private void BindData()
    {
        DataTable dt;
        mid = Request.QueryString["pqmid"].ToString();

        dt = rdw.GetApplyQadList(mid);
        
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PIMasteryRow(dt.Rows[i]["cate_code"].ToString(), dt.Rows[i]["RDW_Project"].ToString(), dt.Rows[i]["RDW_ProdCode"].ToString(), dt.Rows[i]["qad"].ToString(), dt.Rows[i]["pt_desc"].ToString());
            }
        }
    }



    private void PIMasteryRow(string category, string projName, string projCode, string QAD, string QADDesc)
    {
        TableRow row = new TableRow();
        row.BackColor = System.Drawing.Color.White;
        row.HorizontalAlign = HorizontalAlign.Left;
        row.BorderWidth = new Unit(0);

        TableCell cell1 = new TableCell();
        if (string.IsNullOrEmpty(category) )
        {
            cell1.Text = ""; 
        }
        else
        {
            cell1.Text = category; 
        }
         cell1.Width = new Unit(120);
         row.Cells.Add(cell1);

         TableCell cell2 = new TableCell();
         if (string.IsNullOrEmpty(projName) )
         {
             cell2.Text = "";
         }
         else
         {
             cell2.Text = projName;
         }
         cell2.Width = new Unit(300);
         row.Cells.Add(cell2);

         TableCell cell3 = new TableCell();
         if (string.IsNullOrEmpty(projCode))
         {
             cell3.Text = "";
         }
         else
         {
             cell3.Text = projCode;
         }
         cell3.Width = new Unit(100);
         row.Cells.Add(cell3);

         TableCell cell4 = new TableCell();
         if (string.IsNullOrEmpty(QAD) )
         {
             cell4.Text = "";
         }
         else
         {
             //cell4.Text = QAD
             cell4.Text = string.Format("{000}", QAD);
         }
         cell4.Width = new Unit(80);  
         row.Cells.Add(cell4);

         TableCell cell5 = new TableCell();
         if (string.IsNullOrEmpty(QADDesc) )
         {
             cell5.Text = "";
         }
         else
         {
             cell5.Text = QADDesc;
         }
         cell5.Width = new Unit(300);
         row.Cells.Add(cell5);

         exl.Rows.Add(row);


    }
     
}