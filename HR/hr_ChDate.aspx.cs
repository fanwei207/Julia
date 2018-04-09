using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data;

public partial class HR_hr_ChDate : BasePage
{
    private adamClass adam = new adamClass();
    private Wage.HR_Ch hr=new Wage.HR_Ch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    private void BindData()
    {
        SqlParameter[] pram = new SqlParameter[2];
        pram[0] = new SqlParameter("@year", txtYear.Text.Trim());
        pram[1] = new SqlParameter("@month", dropMonth.SelectedValue);
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_selectChDate", pram).Tables[0];
        gvlist.DataSource = dt;
        gvlist.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvlist.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvlist.EditIndex = -1;
        BindData();
    }

    protected void gvlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvlist.DataKeys[e.RowIndex].Values["id"].ToString();

        TextBox txHours = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txHours");
        if (txHours.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('工作时长不能为空!');";
            return;
        }
        else
        {
            try
            {
                decimal hours = Convert.ToDecimal(txHours.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('工作时长必须是数字!');";
                return;
            }
        }

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@hours", txHours.Text.Trim());

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_UpdateChDate", param);
        gvlist.EditIndex = -1;
        BindData();
        ltlAlert.Text = "alert('修改成功!');";
    }
    protected void btnAttendance_Click(object sender, EventArgs e)
    {
        SqlParameter[] pram = new SqlParameter[2];
        pram[0] = new SqlParameter("@year", txtYear.Text.Trim());
        pram[1] = new SqlParameter("@month", dropMonth.SelectedValue);
        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_chAttendance_jiancha", pram);
        ltlAlert.Text = "alert('生成考勤成功!');";
    }
    protected void btnSalary_Click(object sender, EventArgs e)
    {
        //try
        //{
            if (txtYear.Text.Length == 0 || Convert.ToInt32(txtYear.Text) < 1900)
            {
                string strScr = @"<script language='javascript'> alert('输入年份有误!例如:2004');form1.txtYear.focus(); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year1", strScr);
                return;
            }

            hr.DeleteSalaryDataTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0);
            int intError;

            intError = hr.CalculateTimeSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]), Convert.ToInt32(Session["Plantcode"]));

            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('生成工资出错，请重新生成');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", strScr);
                return;
            }
            ltlAlert.Text = "alert('生成工资成功!');";
        //}
        //catch
        //{
        //    string strScr = @"<script language='javascript'> alert('结算出错，请重新结算');</script>";
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strScr);
        //}
    }
    
}