using Microsoft.ApplicationBlocks.Data;
using QCProgress;
using RD_WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class RDW_RDW_TrackingPrjByTamplate : BasePage
{
    RDW rdw = new RDW();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ddl_Category.DataSource = rdw.SelectProjectCategory(string.Empty);
            ddl_Category.DataBind();
            ddl_Category.Items.Insert(0, new ListItem("--", "0"));

            ddl_region.DataSource = rdw.SelectProjectRegion(string.Empty);
            ddl_region.DataBind();
            ddl_region.Items.Insert(0, new ListItem("--", "--"));
            BindGridView();
        }
    }
    protected void BindGridView()
    {
        string mstrId = "AF54E532-1C2A-4457-B7A8-5A11E5862A8D";
        string strSql = "sp_RDW_SelectProjectByTemplates";

        string line = txtLineNo.Text.Trim();
        int lineint = 0;

        if(!int.TryParse(line,out lineint))
        {
              ltlAlert.Text = "alert('Line must number');";
                return;
        }
        else
        {
            if(lineint <= 0)
            {
                ltlAlert.Text = "alert('Line must greater than 0');";
                return;
            }
        }

      

        SqlParameter[] parm = new SqlParameter[10];
        parm[0] = new SqlParameter("@prod", txt_prod.Text.Trim());
        parm[1] = new SqlParameter("@prodcode", txt_code.Text.Trim());
        parm[2] = new SqlParameter("@start", txt_startDate.Text.Trim());
        parm[3] = new SqlParameter("@status", ddl_Status.SelectedValue);
        parm[4] = new SqlParameter("@cateid", ddl_Category.SelectedValue);
        parm[5] = new SqlParameter("@region", ddl_region.SelectedValue);
        parm[6] = new SqlParameter("@plantCode", Convert.ToInt32(Session["plantCode"]));
        parm[7] = new SqlParameter("@mstrId", mstrId);
        parm[8] = new SqlParameter("@LampType", txtLampType.Text.Trim());
        parm[9] = new SqlParameter("@Line", lineint);
        DataTable dt_data = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];

        gv.Columns.Clear();
        BoundField col1 = new BoundField();
        col1.DataField = "Type";
        col1.HeaderText = "Type";
        gv.Columns.Add(col1);
        BoundField col2 = new BoundField();
        col2.DataField = "Engineering Team";
        col2.HeaderText = "Engineering Team";
        gv.Columns.Add(col2);
        BoundField col3 = new BoundField();
        col3.DataField = "Priority";
        col3.HeaderText = "Priority";
        gv.Columns.Add(col3);
        BoundField col4 = new BoundField();
        col4.DataField = "PPA";
        col4.HeaderText = "PPA";
        gv.Columns.Add(col4);
        BoundField col5 = new BoundField();
        col5.DataField = "LNA";
        col5.HeaderText = "LNA";
        gv.Columns.Add(col5);




        BoundField col6 = new BoundField();
        col6.DataField = "Description";
        col6.HeaderText = "Description";
        gv.Columns.Add(col6);
        BoundField col7 = new BoundField();
        col7.DataField = "Comments / Status";
        col7.HeaderText = "Comments / Status";
        gv.Columns.Add(col7);
        BoundField col8 = new BoundField();
        col8.DataField = "Product Manager";
        col8.HeaderText = "Product Manager";
        gv.Columns.Add(col8);
        BoundField col9 = new BoundField();
        col9.DataField = "R&DProjectLeader";
        col9.HeaderText = "R&D Project Leader";
        gv.Columns.Add(col9);
        BoundField col10 = new BoundField();
        col10.DataField = "Required Availability Date";
        col10.HeaderText = "Required Availability Date";
        gv.Columns.Add(col10);

        BoundField col11 = new BoundField();
        col11.DataField = "Target Cost FOB Shanghai";
        col11.HeaderText = "Target Cost FOB Shanghai";
        gv.Columns.Add(col11);
        BoundField col12 = new BoundField();
        col12.DataField = "ESTAR / DLC";
        col12.HeaderText = "ESTAR/DLC";
        gv.Columns.Add(col12);
        BoundField col13 = new BoundField();
        col13.DataField = "ANSI";
        col13.HeaderText = "ANSI";
        gv.Columns.Add(col13);
        BoundField col14 = new BoundField();
        col14.DataField = "Voltage";
        col14.HeaderText = "Voltage";
        gv.Columns.Add(col14);
        BoundField col15 = new BoundField();
        col15.DataField = "Power Factor";
        col15.HeaderText = "Power Factor";
        gv.Columns.Add(col15);

        BoundField col16 = new BoundField();
        col16.DataField = "Dim / ND";
        col16.HeaderText = "Dim / ND";
        gv.Columns.Add(col16);
        BoundField col17 = new BoundField();
        col17.DataField = "Base Type";
        col17.HeaderText = "Base Type";
        gv.Columns.Add(col17);
        BoundField col18 = new BoundField();
        col18.DataField = "Lumens";
        col18.HeaderText = "Lumens";
        gv.Columns.Add(col18);
        BoundField col19 = new BoundField();
        col19.DataField = "CBCP";
        col19.HeaderText = "CBCP";
        gv.Columns.Add(col19);
        BoundField col20 = new BoundField();
        col20.DataField = "Light Distribution";
        col20.HeaderText = "Light Distribution";
        gv.Columns.Add(col20);

        BoundField col21 = new BoundField();
        col21.DataField = "MinCCT";
        col21.HeaderText = "Min CCT";
        gv.Columns.Add(col21);
        BoundField col22 = new BoundField();
        col22.DataField = "MinCRI";
        col22.HeaderText = "Min CRI";
        gv.Columns.Add(col22);
        BoundField col23 = new BoundField();
        col23.DataField = "Customer";
        col23.HeaderText = "Customer";
        gv.Columns.Add(col23);
        BoundField col24 = new BoundField();
        col24.DataField = "Region";
        col24.HeaderText = "Region";
        gv.Columns.Add(col24);
        BoundField col25 = new BoundField();
        col25.DataField = "Status";
        col25.HeaderText = "Status";
        gv.Columns.Add(col25);
        BoundField col26 = new BoundField();
        col26.DataField = "Stage";
        col26.HeaderText = "Stage";
        gv.Columns.Add(col26);

        string sql = "sp_RDW_SelectStandardStep";
        SqlParameter[] parm1 = new SqlParameter[1];
        parm1[0] = new SqlParameter("@mstrId", mstrId);
        DataTable dt_code = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, parm1).Tables[0];
        foreach (DataRow row in dt_code.Rows)
        {
            BoundField col = new BoundField();
            col.DataField = row["RDW_Code"].ToString();
            col.HeaderText = row["RDW_StepName"].ToString();
            gv.Columns.Add(col);
        }
        gv.AllowPaging = true;
        gv.PageIndexChanging += new GridViewPageEventHandler(gv_PageIndexChanging);
        gv.DataSource = dt_data;
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    protected void bnt_query_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i;
            for (i = 25; i < gv.Columns.Count; i++)
            {
                if (e.Row.Cells[i].Text.IndexOf("EXPIRE") > 0)
                {
                    e.Row.Cells[i].BackColor = Color.Red;
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("EXPIRE", "");
                }
                else if (e.Row.Cells[i].Text.Length > 9)
                {
                    e.Row.Cells[i].BackColor = Color.LightGreen;
                }
            }
        }
    }
    protected void btn_export1_Click(object sender, EventArgs e)
    {

        string line = txtLineNo.Text.Trim();
        int lineint = 0;

        if (!int.TryParse(line, out lineint))
        {
            ltlAlert.Text = "alert('Line must number');";
            return;
        }
        else
        {
            if (lineint <= 0)
            {
                ltlAlert.Text = "alert('Line must greater than 0');";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());
        string mstrId = "AF54E532-1C2A-4457-B7A8-5A11E5862A8D";
        string strFloder = Server.MapPath("/docs/RDW_ProjectStandardStep1.xlsx");
        string strImport = "RDW_ProjectStandardSteps" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        RDW_Excel _rdwExcel = new RDW_Excel(strFloder, Server.MapPath("../Excel/") + strImport);
        _rdwExcel.ProjectTrackings(txt_prod.Text.Trim(), txt_code.Text.Trim(), ddl_Status.SelectedValue, ddl_Category.SelectedValue, txt_startDate.Text.Trim(), ddl_region.SelectedValue, Convert.ToInt32(Session["plantCode"]), mstrId, lineint, txtLampType.Text.Trim());

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }


    private void UploadFile()
    {
        String strSQL = "";

        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strImporter = Convert.ToString(Session["uID"]);
        bool blInsert;
        blInsert = true;

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('File upload failed.');";
                return;
            }

        }

    }
}