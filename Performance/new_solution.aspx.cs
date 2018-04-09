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
            hidID.Value = Request.QueryString["ID"];

            try
            {
                dropPlant.SelectedIndex = -1;

                dropPlant.Items.FindByText(Request.QueryString["domain"]).Selected = true;
            }
            catch
            {
                dropPlant.SelectedIndex = 0;
            }

            dropDept.DataSource = this.GetDepartments();
            dropDept.DataBind();
            dropDept.Items.Insert(0, new ListItem("--选择部门--", ""));

            //初始化
            if (hidID.Value.Length > 0)
            {
                DataTable table = this.GetPerformanceById(hidID.Value);

                if (table != null && table.Rows.Count > 0)
                {
                    try
                    {
                        dropDept.SelectedIndex = -1;
                        dropDept.Items.FindByText(table.Rows[0]["perf_dept"].ToString()).Selected = true;
                    }
                    catch
                    {
                        ;
                    }

                    txtRespNo.Text = table.Rows[0]["perf_relNo"].ToString();
                    txtRespName.Text = table.Rows[0]["perf_relName"].ToString();
                    hidDeptName.Value = table.Rows[0]["perf_relDept"].ToString();
                    hidRoleName.Value = table.Rows[0]["perf_relRole"].ToString();

                    txtCause.Text = table.Rows[0]["perf_cause"].ToString();
                    txtSolution.Text = table.Rows[0]["perf_solution"].ToString();
                }
            }
        }
    }

    /// <summary>
    /// 验证责任人是否正确。可以是工号，也可以是姓名。同名的，报错
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    protected Boolean CheckRespUser(string user)
    {
        try
        {
            string strSql = "sp_perf_checkRespUser";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@user", user);
            parms[1] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
            parms[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);

            return Convert.ToBoolean(parms[2].Value);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证责任人是否正确。可以是工号，也可以是姓名。同名的，报错
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    protected DataTable GetDepartments()
    {
        try
        {
            string strSql = "sp_perf_selectDepartment";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected DataTable GetPerformanceById(string id)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (dropPlant.SelectedIndex == 0 || hidID.Value == "")
        {
            this.Alert("数据非法！请返回重新操作！");
            return;
        }

        if (string.IsNullOrEmpty(txtRespNo.Text))
        {
            this.Alert("必须输入考核的 实际责任人！");
            return;
        }

        if (!this.CheckRespUser(txtRespNo.Text))
        {
            this.Alert("输入的实际责任人不存在，或重名！");
            return;
        }

        try
        {
            string strSql = "sp_perf_completePerformance";
            SqlParameter[] parms = new SqlParameter[11];
            parms[0] = new SqlParameter("@id", hidID.Value);
            parms[1] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
            parms[2] = new SqlParameter("@deptID", dropDept.SelectedValue);
            parms[3] = new SqlParameter("@deptName", hidDeptName.Value);
            parms[4] = new SqlParameter("@roleName", hidRoleName.Value);
            parms[5] = new SqlParameter("@userNo", txtRespNo.Text.Trim());
            parms[6] = new SqlParameter("@cause", txtCause.Text.Trim());
            parms[7] = new SqlParameter("@solution", txtSolution.Text.Trim());
            parms[8] = new SqlParameter("@uID", Session["uID"].ToString());
            parms[9] = new SqlParameter("@uName", Session["uName"].ToString());
            parms[10] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parms);

            if (Convert.ToBoolean(parms[10].Value))
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

        txtRespNo.Text = string.Empty;
        txtRespName.Text = string.Empty;
        hidDeptName.Value = string.Empty;
        hidRoleName.Value = string.Empty;
    }

    protected void txtRespNo_TextChanged(object sender, EventArgs e)
    {
        if (dropPlant.SelectedIndex == 0)
        {
            this.Alert("请选择一项 公司");
            return;
        }

        if (dropDept.SelectedIndex == 0)
        {
            this.Alert("请选择一项 部门");
            return;
        }

        try
        {
            string strSql = "sp_perf_selectRespUser";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@user", txtRespNo.Text.Trim());
            parms[1] = new SqlParameter("@deptID", dropDept.SelectedValue);
            parms[2] = new SqlParameter("@plantCode", dropPlant.SelectedValue);

            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];

            if (table.Rows.Count > 0)
            {
                txtRespNo.Text = table.Rows[0]["userNo"].ToString();
                txtRespName.Text = table.Rows[0]["userName"].ToString();
                hidDeptName.Value = table.Rows[0]["deptName"].ToString();
                hidRoleName.Value = table.Rows[0]["roleName"].ToString();
            }
            else
            {
                txtRespNo.Text = string.Empty;
                txtRespName.Text = string.Empty;
                hidDeptName.Value = string.Empty;
                hidRoleName.Value = string.Empty;

                this.Alert("工号或姓名填写错误！");
            }
        }
        catch
        {
            txtRespNo.Text = string.Empty;
            txtRespName.Text = string.Empty;
            hidDeptName.Value = string.Empty;
            hidRoleName.Value = string.Empty;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("new_list.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}