using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SID_CNT_CNTRegistrationList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txt_enrtyTime1.Text = DateTime.Now.ToString("yyyy-MM-01");
            Bind();
        }
    }
    protected void gv_cnt_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_cnt.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void gv_cnt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sql="sp_CNT_updateRegistation";

        int intRow=e.RowIndex;

        SqlParameter[] param = new SqlParameter[13];
        param[0] = new SqlParameter("@id", Convert.ToInt32(gv_cnt.DataKeys[intRow].Values[0].ToString()));
        param[1] = new SqlParameter("@plate_number",(gv_cnt.Rows[e.RowIndex].Cells[0].FindControl("txt_plateNum") as TextBox).Text.Trim());
        param[2] = new SqlParameter("@cnt_id", (gv_cnt.Rows[e.RowIndex].Cells[1].FindControl("txt_cntID") as TextBox).Text.Trim());
        param[3] = new SqlParameter("@driver_name", (gv_cnt.Rows[e.RowIndex].Cells[3].FindControl("txt_driver") as TextBox).Text.Trim());
        param[4] = new SqlParameter("@driver_IDCard",(gv_cnt.Rows[e.RowIndex].Cells[4].FindControl("txt_driverIDCard") as TextBox).Text.Trim() );
        param[5] = new SqlParameter("@driver_phone", (gv_cnt.Rows[e.RowIndex].Cells[5].FindControl("txt_driverPhone") as TextBox).Text.Trim());
        param[6] = new SqlParameter("@temporary_ID", (gv_cnt.Rows[e.RowIndex].Cells[7].FindControl("txt_temporaryID") as TextBox).Text.Trim());
        param[7] = new SqlParameter("@motorcade_phone", (gv_cnt.Rows[e.RowIndex].Cells[6].FindControl("txt_motorcadephone") as TextBox).Text.Trim());
        param[8] = new SqlParameter("@registBy", Session["uID"].ToString().Trim());
        param[9] = new SqlParameter("@registDate", Convert.ToDateTime(DateTime.Now.AddDays(1).ToString()));
        param[10] = new SqlParameter("@entryDate", Convert.ToDateTime((gv_cnt.Rows[e.RowIndex].Cells[2].FindControl("lblentryDate") as Label).Text.Trim()));
        param[11] = new SqlParameter("@remark", (gv_cnt.Rows[e.RowIndex].Cells[8].FindControl("txt_remark") as TextBox).Text.Trim());
        param[12] = new SqlParameter("@reValue",SqlDbType.Int );
        param[12].Direction = ParameterDirection.Output;

        int re=Convert.ToInt32(SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param));
        if (re == 0)
            {
                ltlAlert.Text = "alert('修改失败！')";
            }
            else
                ltlAlert.Text = "alert('修改成功！')";
        gv_cnt.EditIndex = -1;
        Bind();
    }
    protected void gv_cnt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_cnt.EditIndex = -1;
        Bind();
    }
    protected void Bind()
    {
        string sql = "sp_CNT_selectRegistation";
        string entrydate2=txt_enrtyTime2.Text.Trim();
        if(entrydate2.Length==0) entrydate2=DateTime.Now.AddDays(1).ToString();

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@entrydate1", Convert.ToDateTime(txt_enrtyTime1.Text.Trim()));
        param[1] = new SqlParameter("@entrydate2", Convert.ToDateTime(entrydate2));
        param[2] = new SqlParameter("@cnt_id", txt_cntID.Text.Trim());
        //param[3] = new SqlParameter("@ModifyID", Session["uID"].ToString().Trim());
        //param[4] = new SqlParameter("@ModifyDate", DateTime.Now.ToString());

        gv_cnt.DataSource = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sql, param);
        gv_cnt.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bind();
    }
    protected void gv_cnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_cnt.PageIndex = e.NewPageIndex;
        Bind();
    }
}