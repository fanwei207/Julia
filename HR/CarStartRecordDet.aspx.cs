using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class HR_CarStartRecordDet : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] == "new")
            {
                BindCarType(Session["PlantCode"].ToString());
                BindDriver();
                txtStartKilometers.Text = GetCarStartKilometers();
                txtStartTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                hidType.Value = "new";
            }
            else if (Request["type"] == "edit")
            {
                BindCarTypeByEdit(Session["PlantCode"].ToString());
                BindDriver();
                DataTable dt = SelectCarStartRecordByID();
                hidType.Value = "edit";
                btnReceive.Visible = true;
                btnStart.Visible = false;
                ddlCarType.Enabled = false;
                ddlDriver.Enabled = false;
                txtStartKilometers.Enabled = false;
                txtCarStartReason.Enabled = false;
                #region 绑定数据
                ddlCarType.SelectedValue = dt.Rows[0]["CarNumber"].ToString();
                ddlDriver.SelectedValue = dt.Rows[0]["DriverID"].ToString();
                txtStartTime.Value = dt.Rows[0]["StartTime"].ToString();
                txtCarStartReason.Text = dt.Rows[0]["CarStartReason"].ToString();
                //txtStartKilometers.Text = dt.Rows[0]["LastKilometers"].ToString();
                txtStartKilometers.Text = dt.Rows[0]["StartKilometers"].ToString();
                hidKilometers.Value = dt.Rows[0]["LeaveKilometers"].ToString();
                //txtOverKilometers1.Text = dt.Rows[0]["LeaveKilometers"].ToString();
                #endregion
            }
        }
    }
    private DataTable SelectCarStartRecordByID()
    {
        string str = "sp_carRecord_SelectCarStartRecordByID";
        SqlParameter param = new SqlParameter("@id", Request["id"].ToString());
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    private void BindCarType(string plant)
    {
        DataTable dt = SelectCarType(plant);
        ddlCarType.DataSource = dt;
        ddlCarType.DataBind();
    }
    private void BindCarTypeByEdit(string plant)
    {
        DataTable dt = SelectCarTypeByEdit(plant);
        ddlCarType.DataSource = dt;
        ddlCarType.DataBind();
    }
    private DataTable SelectCarType(string plant)
    {
        string sql = "select CarNumber,CarNumber from CarInformation Where CarStartStatus in (1,3) And CarCurrentGround = " + plant;
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private DataTable SelectCarTypeByEdit(string plant)
    {
        string sql = "select CarNumber,CarNumber from tcpc0.dbo.carinformation";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void ddlCarType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        txtStartKilometers.Text = GetCarStartKilometers();
    }
    private string GetCarStartKilometers()
    {
        string sql = "select Kilometers from CarInformation where CarNumber = N'" + ddlCarType.SelectedValue.ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql));
    }
    private void BindDriver()
    {
        DataTable dt = SelectDriverInfor();
        ddlDriver.DataSource = dt;
        ddlDriver.DataBind();
    }
    private DataTable SelectDriverInfor()
    {
        string sql = "select DriverID,DriverName from DriverInformation Where Plant = " + Session["PlantCode"].ToString();
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../hr/carStartRecordList.aspx");
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        if (ddlCarType.SelectedValue.ToString() == "")
        {
            this.Alert("暂时无空闲车辆！");
            return;
        }
        if (txtStartTime.Value == string.Empty)
        {
            this.Alert("发车时间不能为空！");
            return;
        }
        string StartKilometers = "0.00";
        if (txtStartKilometers.Text == string.Empty)
        {
            StartKilometers = GetCarStartKilometers();
        }
        else
        {
            StartKilometers = txtStartKilometers.Text;
        }
        if (CheckCarStartRecode())
        {
            this.Alert("此车已发送！");
            return;
        }
        if (!InsertCarStartRecode(StartKilometers))
        {
            this.Alert("发车成功！");
            BindCarType(Session["PlantCode"].ToString());
            return;
        }
        else
        {
            this.Alert("发车失败，请联系管理员！");
            return;
        }
    }
    private bool CheckCarStartRecode()
    {
        string str = "sp_carRecord_CheckCarStartRecode";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@CarNumber", ddlCarType.SelectedValue.ToString());
        param[1] = new SqlParameter("@DriverID", ddlDriver.SelectedValue.ToString());
        param[2] = new SqlParameter("@StartTime", txtStartTime.Value);
        param[3] = new SqlParameter("@LastKilometers", GetCarStartKilometers());
        param[4] = new SqlParameter("@StartKilometers", txtStartKilometers.Text);
        param[5] = new SqlParameter("@uID", Session["uID"].ToString());
        param[6] = new SqlParameter("@uName", Session["uName"].ToString());
        param[7] = new SqlParameter("@Plant", Session["PlantCode"].ToString());
        param[8] = new SqlParameter("@CarStartReason", txtCarStartReason.Text);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertCarStartRecode(string StartKilometers)
    {
        string str = "sp_carRecord_InsertCarStartRecode";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@CarNumber",ddlCarType.SelectedValue.ToString());
        param[1] = new SqlParameter("@DriverID", ddlDriver.SelectedValue.ToString());
        param[2] = new SqlParameter("@StartTime", txtStartTime.Value);
        param[3] = new SqlParameter("@LastKilometers", GetCarStartKilometers());
        param[4] = new SqlParameter("@StartKilometers", StartKilometers);
        param[5] = new SqlParameter("@uID",Session["uID"].ToString());
        param[6] = new SqlParameter("@uName", Session["uName"].ToString());
        param[7] = new SqlParameter("@Plant", Session["PlantCode"].ToString());
        param[8] = new SqlParameter("@CarStartReason", txtCarStartReason.Text);
        
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        if (txtOverTime.Value == string.Empty)
        {
            this.Alert("收车时间不能为空");
            return;
        }
        string OverKilometers = string.Empty;
        if (txtOverKilometers.Text == string.Empty)
        {
            this.Alert("当前里程数不能为空");
            return;
            //OverKilometers = hidKilometers.Value;
        }
        else 
        {
            OverKilometers = txtOverKilometers.Text;
            //if (Convert.ToDecimal(hidKilometers.Value) > Convert.ToDecimal(txtOverKilometers.Text))
            //{
            //    hidKilometersStatus.Value = "1";
            //    //this.Alert("收车公里数不能比实际所跑公里数小");
            //    return;
            //}
        }
        if (!UpdateCarStartRecode(OverKilometers))
        {
            this.Alert("收车成功！");
            return;
        }
        else
        {
            this.Alert("收车失败，请联系管理员！");
            return;
        }
    }
    private bool UpdateCarStartRecode(string OverKilometers)
    {
        string str = "sp_carRecord_UpdateCarStartRecode";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", Request["id"].ToString());
        param[1] = new SqlParameter("@OverTime", txtOverTime.Value);
        param[2] = new SqlParameter("@OverKilometers", OverKilometers);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@Plant", Session["PlantCode"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    public void Alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "<script language=\"JavaScript\" type=\"text/javascript\">alert('" + msg + "');</script>");
    }
}