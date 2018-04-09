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

public partial class HR_CarFuelList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["PlantCode"].ToString();
            BindCarNumber();
            BindDriver();
            BindFuelRecord();
        }
    }
    private void BindCarNumber()
    {
        DataTable dt = SelectCarNumber();
        ddlCarNumber.DataSource = dt;
        ddlCarNumber.DataBind();
        ddlCarNumber.Items.Insert(0, new ListItem("全部", "0"));
    }
    private DataTable SelectCarNumber()
    {
        string sql = string.Empty;
        if (ddlPlant.SelectedValue.ToString() == "0")
        {
            sql = "select CarNumber,CarNumber from tcpc0.dbo.carinformation";
        }
        else
        {
            sql = "select CarNumber,CarNumber from tcpc0.dbo.carinformation where CarCurrentGround = '" + ddlPlant.SelectedValue.ToString() + "'";
        }
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private void BindDriver()
    {
        DataTable dt = SelectDriverInfor();
        ddlDriver.DataSource = dt;
        ddlDriver.DataBind();
        ddlDriver.Items.Insert(0, new ListItem("全部", "0"));
    }
    private DataTable SelectDriverInfor()
    {
        string sql = string.Empty;
        if (ddlPlant.SelectedValue == "0")
        {
            sql = "select DriverID,DriverName from DriverInformation";
        }
        else
        {
            sql = "select DriverID,DriverName from DriverInformation Where Plant = '" + ddlPlant.SelectedValue.ToString() + "'";
        }
        //string sql = "select DriverID,DriverName from DriverInformation Where Plant = " + hidPlant.Value;
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCarNumber();
        BindDriver();
        BindFuelRecord();
    }
    private void BindFuelRecord()
    {
        DataTable dt = SelectFuelRecord();
        gvFuelRecord.DataSource = dt;
        gvFuelRecord.DataBind();
    }
    private DataTable SelectFuelRecord()
    {
        string str = "sp_carRecord_SelectFuelRecord";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@CarNumber", ddlCarNumber.SelectedValue.ToString());
        param[2] = new SqlParameter("@Driver", ddlDriver.SelectedValue.ToString());
        param[3] = new SqlParameter("@StartDate", txtDate.Text);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gvFuelRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gvFuelRecord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 0)
            {
                e.Row.Cells[0].Text = "SQL";
            }
            else if (Convert.ToInt32(gvFuelRecord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 1)
            {
                e.Row.Cells[0].Text = "ZQL";
            }
            else if (Convert.ToInt32(gvFuelRecord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 2)
            {
                e.Row.Cells[0].Text = "YQL";
            }
            else if (Convert.ToInt32(gvFuelRecord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 3)
            {
                e.Row.Cells[0].Text = "HQL";
            }
            else if (Convert.ToInt32(gvFuelRecord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 3)
            {
                e.Row.Cells[0].Text = "TCB";
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindFuelRecord();
    }
    protected void ddlCarNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFuelRecord();
    }
    protected void ddlDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFuelRecord();
    }
}