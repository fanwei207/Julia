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

public partial class SID_CNT_CargoTracking : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (!this.Security["550050026"].isValid)
            {
                btn_save.Enabled = false;
            }
            txt_cntID.Text = Request.QueryString["cntID"];
            txt_Driver.Text = Request.QueryString["driverName"];
            txt_DriverPhone.Text = Request.QueryString["driverPhone"];
            txt_entry.Text = Request.QueryString["entryDate"];
            txt_leave.Text = Request.QueryString["leaveDate"];
            txt_MotorcadePhone.Text = Request.QueryString["motorcadePhone"];
            txt_PlateNbr.Text = Request.QueryString["plateNmb"];
            DataBind();
            
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        string centID = Request.Form["txt_cntID"].Trim();
        string entryDate = Request.Form["txt_entry"].Trim();
        Response.Redirect("CNT_EntryLeaveList.aspx?centID=" + centID + "&entryDate=" + entryDate);
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {

        string sql = "sp_Cnt_InsertCargoTrack";

        if(ddl_normal.SelectedValue=="-1")
        {
            ltlAlert.Text = "alert('必须选择情况是否异常！')";
            return;
        }

        if (ddl_ontime.SelectedValue == "-1")
        {
            ltlAlert.Text = "alert('必须选择是否按时到达！')";
            return;
        }

        if(txt_location.Text.Trim().Length==0)
        {
            ltlAlert.Text = "alert('车辆位置必须填写！')";
            return;
        }
        if (txt_TrackTime.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('联系时间必须填写！')";
            return;
        }
        

        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@cnt_entrydate", Request.Form["txt_entry"].Trim());
        param[1] = new SqlParameter("@cnt_id", Request.Form["txt_cntID"].Trim());
        param[2] = new SqlParameter("@tracktime", Convert.ToDateTime(txt_TrackTime.Text.Trim()));
        param[3] = new SqlParameter("@location", txt_location.Text.Trim());
        param[4] = new SqlParameter("@normal", Convert.ToInt32(ddl_normal.SelectedValue));
        param[5] = new SqlParameter("@ontime", Convert.ToInt32(ddl_ontime.SelectedValue));
        param[6] = new SqlParameter("@reValue", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;
        param[7] = new SqlParameter("@cargo_trackby", Session["uID"]);

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param);

        int re = Convert.ToInt32(param[6].Value);

        if (re == 0)
        {
            ltlAlert.Text = "alert('追踪记录添加失败！')";
        }
        else { ltlAlert.Text = "alert('追踪记录添加成功！')"; }

        DataBind();
    }
    protected void DataBind()
    {
        string sql = "sp_Cnt_SelectCargoTrack";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@cnt_entrydate", Convert.ToDateTime(Request.QueryString["entryDate"]));
        param[1] = new SqlParameter("@cnt_id", Request.QueryString["cntID"]);

        gv_track.DataSource =SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sql, param);
        
        gv_track.DataBind();
    }
}