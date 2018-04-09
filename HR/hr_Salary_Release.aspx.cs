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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using Wage;
/// <summary>
/// 离职人员工资暂扣
/// </summary>
public partial class hr_Salary_Leaver : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            dropDeptBind();
        }
    }

    protected void GridViewBind()
    {
        btnRelease.Enabled = IsLeaverSalaryLocked(txtYear.Text, txtMonth.Text);
        btnCancelRelease.Enabled = IsLeaverSalaryLocked(txtYear.Text, txtMonth.Text);
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text);
            param[2] = new SqlParameter("@userNo", txtUserNo.Text);
            param[3] = new SqlParameter("@deptNo", dropDept.SelectedValue);
            gv.DataSource = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_selectSalaryRelease", param);
            gv.DataBind();
        }
        catch
        {
            this.Alert("列表获取失败！刷新后重新操作一次！");
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (gv.DataKeys[e.Row.RowIndex].Values["releaseDate"].ToString().Length > 0)
        //    {
        //        e.Row.Cells[5].Text = string.Empty;
        //    }
        //}
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!this.IsDate(txtYear.Text + "-" + txtMonth.Text + "-1"))
        {
            this.Alert("请正确填写年月！");
        }

        GridViewBind();
    }
    protected void btnRelease_Click(object sender, EventArgs e)
    {
        if (!this.IsDate(txtYearR.Text + "-" + txtMonthR.Text + "-1"))
        {
            this.Alert("请正确填写发放年月！");
            return;
        }
        else
        {
            if (Convert.ToDateTime(txtYearR.Text + "-" + txtMonthR.Text + "-1") < Convert.ToDateTime(txtYear.Text + "-" + txtMonth.Text + "-1"))
            {
                this.Alert("发放年月不能早于结算年月！");
                return;
            }
        }

        if (dropReleaseType.SelectedIndex == 0)
        {
            this.Alert("必须选择一项 发放方式！");
            return;
        }

        bool allSucces = true;

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = row.FindControl("chkSingle") as CheckBox;
            if (chk.Checked)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@year", txtYear.Text);
                    param[1] = new SqlParameter("@month", txtMonth.Text);
                    param[2] = new SqlParameter("@userID", gv.DataKeys[row.RowIndex].Values["userID"].ToString());
                    param[3] = new SqlParameter("@relYear", txtYearR.Text);
                    param[4] = new SqlParameter("@relMonth", txtMonthR.Text);
                    param[5] = new SqlParameter("@relType", dropReleaseType.SelectedValue);
                    param[6] = new SqlParameter("@uID", Session["uID"].ToString());
                    param[7] = new SqlParameter("@uName", Session["uName"].ToString());
                    param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[8].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_releaseSalaryLeaver", param);

                    allSucces = Convert.ToBoolean(param[8].Value);
                }
                catch
                {
                    allSucces = false;
                }
            }
        }

        if (!allSucces)
        {
            this.Alert("部分人员未成功发放！请重新发放一次！");
        }
        else
        {
            this.Alert("暂存工资发放成功！");
        }

        GridViewBind();
    }

    protected void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "-1");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }
    protected void btnCancelRelease_Click(object sender, EventArgs e)
    {
        
        bool allSucces = true;

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = row.FindControl("chkSingle") as CheckBox;
            if (chk.Checked)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@year", txtYear.Text);
                    param[1] = new SqlParameter("@month", txtMonth.Text);
                    param[2] = new SqlParameter("@userID", gv.DataKeys[row.RowIndex].Values["userID"].ToString());
                    param[3] = new SqlParameter("@uID", Session["uID"].ToString());
                    param[4] = new SqlParameter("@uName", Session["uName"].ToString());
                    param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[5].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_canceReleaseSalaryLeaver", param);

                    allSucces = Convert.ToBoolean(param[5].Value);
                }
                catch
                {
                    allSucces = false;
                }
            }
        }

        if (!allSucces)
        {
            this.Alert("部分人员取消失败！请重新取消一次！");
        }
        else
        {
            this.Alert("暂存工资取消发放成功！");
        }

        GridViewBind();
    }

    /// <summary>
    /// 检验指定年月的暂存工资是否被锁定
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    protected bool IsLeaverSalaryLocked(string year, string month)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_isSalaryLeaverLocked", param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }
}
