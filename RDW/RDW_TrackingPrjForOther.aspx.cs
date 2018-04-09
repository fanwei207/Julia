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
public partial class RDW_RDW_TrackingPrjForOther : BasePage
{
    RDW rdw = new RDW();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
        string mstrId = "65F2C4C5-DD6C-4456-AAB7-A1293557B2D9";
        string strSql = "sp_RDW_SelectProjectByTemplate";
        SqlParameter[] parm = new SqlParameter[9];
        parm[0] = new SqlParameter("@prod", txt_prod.Text.Trim());
        parm[1] = new SqlParameter("@prodcode", txt_code.Text.Trim());
        parm[2] = new SqlParameter("@start", txt_startDate.Text.Trim());
        parm[3] = new SqlParameter("@status", ddl_Status.SelectedValue);
        parm[4] = new SqlParameter("@cateid", ddl_Category.SelectedValue);
        parm[5] = new SqlParameter("@region", ddl_region.SelectedValue);
        parm[6] = new SqlParameter("@plantCode", Convert.ToInt32(Session["plantCode"]));
        parm[7] = new SqlParameter("@mstrId", mstrId);
        parm[8] = new SqlParameter("@LampType", txtLampType.Text.Trim());

        DataTable dt_data = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];

        gv.Columns.Clear();
        BoundField col1 = new BoundField();
        col1.DataField = "cate_name";
        col1.HeaderText = "Project Category";
        gv.Columns.Add(col1);
        BoundField col2 = new BoundField();
        col2.DataField = "prodName";
        col2.HeaderText = "PPA";
        gv.Columns.Add(col2);
        BoundField col3 = new BoundField();
        col3.DataField = "prodCode";
        col3.HeaderText = "LNA";
        gv.Columns.Add(col3);
        BoundField col4 = new BoundField();
        col4.DataField = "prodDesc";
        col4.HeaderText = "Description";
        gv.Columns.Add(col4);
        BoundField col5 = new BoundField();
        col5.DataField = "RDW_EndDate";
        col5.HeaderText = "Est. Completion";
        gv.Columns.Add(col5);
        col5 = new BoundField();
        col5.DataField = "RDW_MgrName";
        col5.HeaderText = "Product Manager";
        gv.Columns.Add(col5);
        col5 = new BoundField();
        col5.DataField = "EE";
        col5.HeaderText = "EE";
        gv.Columns.Add(col5);
        col5 = new BoundField();
        col5.DataField = "ME";
        col5.HeaderText = "ME";
        gv.Columns.Add(col5);
        BoundField col6 = new BoundField();
        col6.DataField = "RDW_Status";
        col6.HeaderText = "Status";
        gv.Columns.Add(col6);
        string sql = "sp_RDW_SelectStandardStep";
        SqlParameter[] parm1 = new SqlParameter[1];
        parm1[0] = new SqlParameter("@mstrId", mstrId);
        DataTable dt_code = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, parm1).Tables[0];
        foreach (DataRow row in dt_code.Rows)
        {
            BoundField col = new BoundField();
            col.DataField = row["RDW_Code"].ToString();
            col.HeaderText = row["RDW_StepTitle"].ToString();
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        int uID = int.Parse(Session["uID"].ToString());
        string mstrId = "65F2C4C5-DD6C-4456-AAB7-A1293557B2D9";
        string strFloder = Server.MapPath("/docs/RDW_ProjectStandardStepOther.xlsx");
        string strImport = "RDW_ProjectStandardStep" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        RDW_Excel _rdwExcel = new RDW_Excel(strFloder, Server.MapPath("../Excel/") + strImport);
        _rdwExcel.ProjectTracking(txt_prod.Text.Trim(), txt_code.Text.Trim(), ddl_Status.SelectedValue, ddl_Category.SelectedValue, txt_startDate.Text.Trim(), ddl_region.SelectedValue, Convert.ToInt32(Session["plantCode"]), mstrId);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i;
            for (i = 8; i < gv.Columns.Count; i++)
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
}