using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;

public partial class HR_CarStartRecordList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["PlantCode"].ToString();
            BindCarNumber();
            BindDriver();
            BindgvCarStartReacord();
        }
    }
    private void BindgvCarStartReacord()
    {
        DataTable dt = SelectCarStartRecode();
        gvCarStartReacord.DataSource = dt;
        gvCarStartReacord.DataBind();
    }
    private DataTable SelectCarStartRecode()
    {
        string str = "sp_carRecord_SelectCarStartRecode";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@CarNumber", ddlCarNumber.SelectedValue.ToString());
        param[2] = new SqlParameter("@Driver",ddlDriver.SelectedValue.ToString());
        param[3] = new SqlParameter("@CarStartStatus",ddlCarStartStatus.SelectedValue.ToString());
        param[4] = new SqlParameter("@StartDate", txtDate.Text);
        param[5] = new SqlParameter("@CarStartType", ddlCarStartType.SelectedValue.ToString());
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
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
        BindgvCarStartReacord();
    }
    private void BindCarNumber()
    {
        DataTable dt = SelectCarNumber();
        ddlCarNumber.DataSource = dt;
        ddlCarNumber.DataBind();
        ddlCarNumber.Items.Insert(0,new ListItem("全部", "0"));
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindgvCarStartReacord();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../hr/CarStartRecordDet.aspx?type=new");
    }
    protected void gvCarStartReacord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gvCarStartReacord.DataKeys[e.RowIndex].Values["id"].ToString();

        //写入历史表
        string str = "sp_carRecord_InsertCarStartRecordHis";
        string sql = "Delete  From CarStartRecord Where id = '" + ID + "'";
        SqlParameter[] Param = new SqlParameter[10];
        Param[0] = new SqlParameter("@id", ID);
        Param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        Param[2] = new SqlParameter("@uName", Session["uName"].ToString());

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, Param);
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, sql);

        BindgvCarStartReacord();
    }
    protected void gvCarStartReacord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["CarRecordStatus"]) == 1)
            {
                e.Row.Cells[4].Text = "运载中";
                if (SelectExistsCatStartRecord(Convert.ToString(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["id"])))
                {
                    e.Row.Cells[13].Text = "";
                }
            }
            else if (Convert.ToInt32(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["CarRecordStatus"]) == 2)
            {
                e.Row.Cells[4].Text = "已结束";
                e.Row.Cells[12].Text = "";
                e.Row.Cells[13].Text = "";
            }

            if (Convert.ToInt32(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["CarStartType"]) == 1)
            {
                e.Row.Cells[14].Text = "";
            }
            else if (Convert.ToInt32(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["CarStartType"]) == 2)
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
            }
            else if (Convert.ToInt32(gvCarStartReacord.DataKeys[e.Row.RowIndex].Values["CarStartType"]) == 3)
            {
                e.Row.Cells[14].Text = "已拒绝";
            }
        }
    }
    private bool SelectExistsCatStartRecord(string id)
    {
        string str = "sp_carRecord_CheckExistsCarUseRecord";
        SqlParameter param = new SqlParameter("@id", id);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvCarStartReacord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "Receive")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("../hr/CarStartRecordDet.aspx?type=edit&id=" + gvCarStartReacord.DataKeys[intRow].Values["id"].ToString().Trim());
        }
        if (e.CommandName == "Yes")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (!ReviewCarStartType("1", gvCarStartReacord.DataKeys[index].Values["id"].ToString()))
            {
                BindgvCarStartReacord();
            }
        }
        if (e.CommandName == "No")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (!ReviewCarStartType("3", gvCarStartReacord.DataKeys[index].Values["id"].ToString()))
            {
                BindgvCarStartReacord();
            }
        }
    }
    private bool ReviewCarStartType(string type, string id)
    {
        string str = "sp_carRecord_ReviewCarStartType";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@id", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void ddlCarNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvCarStartReacord();
    }
    protected void ddlDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvCarStartReacord();
    }
    protected void ddlCarStartStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvCarStartReacord();
    }
    protected void ddlCarStartType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvCarStartReacord();
    }
    protected void gvCarStartReacord_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCarStartReacord.EditIndex = e.NewEditIndex;
        BindgvCarStartReacord();
    }
    protected void gvCarStartReacord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCarStartReacord.EditIndex = -1;
        BindgvCarStartReacord();
    }
    protected void gvCarStartReacord_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string StartKilometers = ((TextBox)gvCarStartReacord.Rows[e.RowIndex].Cells[6].FindControl("txtStartKilometers")).Text.ToString().Trim();
        string OverKilometers = ((TextBox)gvCarStartReacord.Rows[e.RowIndex].Cells[7].FindControl("txtOverKilometers")).Text.ToString().Trim();
        string CarStartReason = ((TextBox)gvCarStartReacord.Rows[e.RowIndex].Cells[9].FindControl("txtCarStartReason")).Text.ToString().Trim();
        string id = gvCarStartReacord.DataKeys[e.RowIndex].Values[0].ToString();
        if(OverKilometers == "")
        {
            OverKilometers = "null";
        }
        //SqlParameter[] param = new SqlParameter[10];
        //param[0] = new SqlParameter("@id", id);
        //param[0] = new SqlParameter("@id", StartKilometers);
        //param[0] = new SqlParameter("@id", id);
        string sql = "Update CarStartRecord Set StartKilometers = " + StartKilometers + ", OverKilometers = " + OverKilometers + ", CarStartReason = N'" + CarStartReason + "' where id = '" + id + "'";
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, sql);

        gvCarStartReacord.EditIndex = -1;
        BindgvCarStartReacord();
    }
    protected void gvCarStartReacord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCarStartReacord.PageIndex = e.NewPageIndex;
        BindgvCarStartReacord();
    }
}