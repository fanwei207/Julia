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

public partial class HR_CarInformation : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["PlantCode"].ToString();
            BindCarInformation();
        }
    }
    private void BindCarInformation()
    {
        DataTable dt = SelectCarInformation();
        gvCarInformation.DataSource = dt;
        gvCarInformation.DataBind();
    }
    private DataTable SelectCarInformation()
    {
        string sql = "sp_carRecord_SelectCarInformation";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Plant", ddlPlant.SelectedValue);
        param[1] = new SqlParameter("@CarNumber", txtCarNumber.Text);
        param[2] = new SqlParameter("@CarType", ddlCarType.SelectedItem.ToString());
        param[3] = new SqlParameter("@CarStartType", ddlCarStartType.SelectedValue.ToString());
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sql, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindCarInformation();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (ddlPlant.SelectedValue.ToString() == "0")
        {
            this.Alert("请选择公司！");
            return;
        }
        if (txtCarNumber.Text == string.Empty)
        {
            this.Alert("请填写车号！");
            return;
        }
        if (ddlCarType.SelectedValue.ToString() == "0")
        {
            this.Alert("请选择车辆类型！");
            return;
        }
        if (CheckExistsCarInformation())
        {
            this.Alert("已存在此条信息，请重新填写！");
            return;
        }
        if (InsertCarInformation())
        {
            this.Alert("新增失败，请联系管理员！");
            return;
        }
        else
        {
            this.Alert("新增成功！");
            BindCarInformation();
        }
    }
    private bool CheckExistsCarInformation()
    {
        string str = "sp_carRecord_CheckExistsCarInformation";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Plant", ddlPlant.SelectedValue);
        param[1] = new SqlParameter("@CarNumber", txtCarNumber.Text);
        param[2] = new SqlParameter("@CarType", ddlCarType.SelectedItem.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertCarInformation()
    {
        string str = "sp_carRecord_InsertCarInformation";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Plant", ddlPlant.SelectedValue);
        param[1] = new SqlParameter("@CarNumber", txtCarNumber.Text);
        param[2] = new SqlParameter("@CarType", ddlCarType.SelectedItem.ToString());
        param[3] = new SqlParameter("@uID",Session["uID"].ToString());
        param[4] = new SqlParameter("@uName",Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvCarInformation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gvCarInformation.DataKeys[e.RowIndex].Values["id"].ToString();
        string sql = "Delete  From CarInformation Where id = '" + ID + "'";
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@id", ID);
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, sql);

        BindCarInformation();
    }
    protected void gvCarInformation_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string Kilometers = ((TextBox)gvCarInformation.Rows[e.RowIndex].Cells[3].FindControl("txtKilometers")).Text.ToString().Trim();
        string plantcode = gvCarInformation.DataKeys[e.RowIndex].Values[1].ToString();
        string carnumber = gvCarInformation.DataKeys[e.RowIndex].Values[2].ToString();


        DropDownList ddlCarCurrentGround = (DropDownList)this.gvCarInformation.Rows[e.RowIndex].Cells[4].FindControl("ddlCarCurrentGround");

        string Plants = ddlCarCurrentGround.SelectedValue;

        //写日志
        if (!updateCarInfor(Kilometers, plantcode, carnumber, Plants))
        {
            gvCarInformation.EditIndex = -1;
            BindCarInformation();
        }
        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
    }
    private bool updateCarInfor(string Kilometers, string plantcode, string carnumber, string Plants)
    {
        string str = "sp_carRecord_UpdateCarInfor";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Kilometers", Kilometers);
        param[1] = new SqlParameter("@plantcode", plantcode);
        param[2] = new SqlParameter("@carnumber", carnumber);
        param[3] = new SqlParameter("@Plants", Plants);
        
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvCarInformation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCarInformation.EditIndex = e.NewEditIndex;
        BindCarInformation();
    }
    protected void gvCarInformation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCarInformation.EditIndex = -1;
        BindCarInformation();
    }
    protected void gvCarInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        //    if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarCurrentGround"]) == 1)
        //    {
        //        e.Row.Cells[4].Text = "上海强凌";
        //    }
        //    else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarCurrentGround"]) == 2)
        //    {
        //        e.Row.Cells[4].Text = "镇江强凌";
        //    }
        //    else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarCurrentGround"]) == 5)
        //    {
        //        e.Row.Cells[4].Text = "扬州强凌";
        //    }
        //    else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarCurrentGround"]) == 8)
        //    {
        //        e.Row.Cells[4].Text = "淮安强凌";
        //    }
        //    else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarCurrentGround"]) == 11)
        //    {
        //        e.Row.Cells[4].Text = "上海天灿宝";
        //    }

            if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarStartStatus"]) == 1)
            {
                e.Row.Cells[5].Text = "待发车";
            }
            else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarStartStatus"]) == 2)
            {
                e.Row.Cells[5].Text = "使用中";
            }
            else if (Convert.ToInt32(gvCarInformation.DataKeys[e.Row.RowIndex].Values["CarStartStatus"]) == 3)
            {
                e.Row.Cells[5].Text = "待收车";
            }
        }
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCarInformation();
    }
    protected void ddlCarStartType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCarInformation();
    }
}