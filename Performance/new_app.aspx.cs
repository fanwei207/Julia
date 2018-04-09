using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class Performance_new_app : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //考核日期默认
            txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //绑定类型
            dropType.DataSource = this.GetPerformanceType();
            dropType.DataBind();
            dropType.Items.Insert(0, new ListItem("--请选择一项--", "0"));

            dropResult.DataSource = this.GetPerformanceResult();
            dropResult.DataBind();
            dropResult.Items.Insert(0, new ListItem("--请选择一项--", ""));

            dropDeduct.DataSource = this.GetPerformanceDeduct();
            dropDeduct.DataBind();
            dropDeduct.Items.Insert(0, new ListItem("--请选择一项--", ""));

            dropDemerit.DataSource = this.GetPerformanceDemerit();
            dropDemerit.DataBind();
            dropDemerit.Items.Insert(0, new ListItem("--请选择一项--", ""));

            dropHrResult.DataSource = this.GetPerformanceHrResult();
            dropHrResult.DataBind();
            dropHrResult.Items.Insert(0, new ListItem("--请选择一项--", ""));

            if (Request.QueryString["id"] != null)
            {
                hidID.Value = Request.QueryString["id"];

                txtDate.Enabled = false;
                dropType.Enabled = false;
                btnSelect.Enabled = false;

                btnBack.Visible = true;

                DataTable table = this.GetPerformanceByID(hidID.Value);
                if (table != null && table.Rows.Count > 0)
                {
                    txtDate.Text =  string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(table.Rows[0]["perf_date"]));
                    txtCompany.Text = table.Rows[0]["perf_domain"].ToString();
                    txtDept.Text = table.Rows[0]["perf_dept"].ToString();
                    txtRole.Text = table.Rows[0]["perf_role"].ToString();
                    hidRespUserID.Value = table.Rows[0]["perf_userID"].ToString();
                    txtRespNo.Text = table.Rows[0]["perf_userNo"].ToString();
                    txtRespName.Text = table.Rows[0]["perf_userName"].ToString();
                    txtRemarks.Text = table.Rows[0]["perf_remarks"].ToString();

                    try
                    {
                        dropType.SelectedIndex = -1;
                        dropResult.SelectedIndex = -1;
                        dropDeduct.SelectedIndex = -1;
                        dropDemerit.SelectedIndex = -1;
                        dropHrResult.SelectedIndex = -1;

                        dropType.Items.FindByText(table.Rows[0]["perf_type"].ToString()).Selected = true;
                        dropResult.Items.FindByText(table.Rows[0]["perf_result"].ToString()).Selected = true;
                        dropDeduct.Items.FindByText(table.Rows[0]["perf_deduct"].ToString()).Selected = true;
                        dropDemerit.Items.FindByText(table.Rows[0]["perf_demerit"].ToString()).Selected = true;
                        dropHrResult.Items.FindByText(table.Rows[0]["perf_hrResult"].ToString()).Selected = true;
                    }
                    catch
                    { }
                }
                else
                {
                    this.Alert("获取数据时发生错误！请返回！");
                }
            }
        }
    }

    protected DataTable GetPerformanceByID(string id)
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceById";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected DataTable GetPerformanceType()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceType";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 公司规定
    /// </summary>
    /// <returns></returns>
    protected DataTable GetPerformanceResult()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceResult";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 扣分考核
    /// </summary>
    /// <returns></returns>
    protected DataTable GetPerformanceDeduct()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceDeduct";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 结果考核
    /// </summary>
    /// <returns></returns>
    protected DataTable GetPerformanceDemerit()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceDemerit";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 人事考核
    /// </summary>
    /// <returns></returns>
    protected DataTable GetPerformanceHrResult()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceHrResult";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            this.Alert("必须输入考核的 日期！");
            return;
        }
        else if (!this.IsDate(txtDate.Text))
        {
            this.Alert("必须输入日期格式不正确！");
            return;
        }
        else if (Convert.ToDateTime(txtDate.Text) > DateTime.Now)
        {
            this.Alert("必须输入日期不能超过当前！");
            return;
        }

        if (dropType.SelectedIndex == 0)
        {
            this.Alert("必须选择一项考核的 类型！");
            return;
        }

        if (string.IsNullOrEmpty(hidRespUserID.Value))
        {
            this.Alert("必须输入考核的 责任人！");
            return;
        }

        if (dropResult.SelectedIndex == 0)
        {
            this.Alert("必须选择一项考核的 处理规定！");
            return;
        }

        try
        {
            string strSql = "sp_perf_savePerformance";
            SqlParameter[] parms = new SqlParameter[13];
            parms[0] = new SqlParameter("@id", hidID.Value);
            parms[1] = new SqlParameter("@date", txtDate.Text.Trim());
            parms[2] = new SqlParameter("@type", dropType.SelectedItem.Text);
            parms[3] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
            parms[4] = new SqlParameter("@userID", hidRespUserID.Value);
            parms[5] = new SqlParameter("@restult", dropResult.SelectedValue);
            parms[6] = new SqlParameter("@deduct", dropDeduct.SelectedValue);
            parms[7] = new SqlParameter("@demerit", dropDemerit.SelectedValue);
            parms[8] = new SqlParameter("@hrResult", dropHrResult.SelectedValue);
            parms[9] = new SqlParameter("@remarks", txtRemarks.Text.Trim());
            parms[10] = new SqlParameter("@uID", Session["uID"].ToString());
            parms[11] = new SqlParameter("@uName", Session["uName"].ToString());
            parms[12] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[12].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);

            if (Convert.ToBoolean(parms[12].Value))
            {
                this.Alert("保存成功！");
            }
            else 
            {
                this.Alert("保存失败A！");
            }
        }
        catch
        {
            this.Alert("保存失败B！");
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('new_choose.aspx?mid=','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("new_list.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}