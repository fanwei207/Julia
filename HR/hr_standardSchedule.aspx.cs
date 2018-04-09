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

public partial class HR_hr_standardSchedule : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropStatus.Items.Add(new ListItem("全部", "0"));
            dropStatus.Items.Add(new ListItem("在职", "1"));
            dropStatus.Items.Add(new ListItem("离职", "2"));
            dropStatus.SelectedIndex = 1;

            BindDeparts();
            BindTypes();
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@departID", dropDeparts.SelectedValue);
            param[1] = new SqlParameter("@userType", dropTypes.SelectedValue);
            param[2] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
            param[3] = new SqlParameter("@status", dropStatus.SelectedValue);

            DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectUserSchedules", param);
            gvUserSchedules.DataSource = ds;
            gvUserSchedules.DataBind();

            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
            ds.Dispose();
        }
        catch
        {
            return;
        }
    }

    protected void BindTypes()
    {
        string strSQL = "select systemCodeID, systemCodeName from SystemCode where systemCodeTypeID = 42";
        dropTypes.Items.Clear();
        dropTypes.Items.Add(new ListItem(" -- ", "0"));
        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropTypes.Items.Add(new ListItem(reader["systemCodeName"].ToString(), reader["systemCodeID"].ToString()));
            }
            reader.Dispose();
        }
        catch
        {
            return;
        }
    }

    protected void BindDeparts()
    {
        string strSQL = "select departmentID, name from Departments where isSalary = 1And active = 1 order by departmentID";
        dropDeparts.Items.Clear();
        dropDeparts.Items.Add(new ListItem(" -- ", "0"));
        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropDeparts.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
            }
            reader.Dispose();
        }
        catch
        {
            return;
        }
    }

    protected void gvUserSchedules_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvUserSchedules_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserSchedules.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected bool IsTime(string val)
    {
        try
        {
            val = Convert.ToDateTime(val).ToString("HH:mm:ss");
            if (val == "00:00:00")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strErrorMsg = string.Empty;
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else if (txbUserNo.Text.Trim() == string.Empty)
        {
            strErrorMsg += "工号为空；\\n  ";
        }
        if (txbAtWork.Text.Trim() == string.Empty)
        {
            strErrorMsg += "上班时间为空；\\n  ";
        }
        if (txbAtWork.Text.Trim() != string.Empty && !IsTime(txbAtWork.Text.Trim()))
        {
            strErrorMsg += "上班时间格式不对；\\n  ";
        }
        if (txbOffWork.Text.Trim() != string.Empty && IsTime(txbAtWork.Text.Trim()) && IsTime(txbOffWork.Text.Trim()) && Convert.ToDateTime(txbAtWork.Text.Trim()) >= Convert.ToDateTime(txbOffWork.Text.Trim()))
        {
            strErrorMsg += "上班时间等于或晚于下班时间；\\n  ";
        }

        if (strErrorMsg != string.Empty)
        {
            ltlAlert.Text = "alert('请处理以下错误：\\n  " + strErrorMsg + "')";
        }
        else
        {
            string retValue = string.Empty;
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
                param[1] = new SqlParameter("@atWork", txbAtWork.Text.Trim());
                param[2] = new SqlParameter("@offWork", txbOffWork.Text.Trim());
                param[3] = new SqlParameter("@createdBy", Session["uID"].ToString());
                param[4] = new SqlParameter("@retValue", SqlDbType.NVarChar, 200);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_insertUserSchedule", param);

                retValue = param[4].Value.ToString();
            }
            catch
            {
                retValue = "-1";
            }

            if (retValue == "1")
            {
                ltlAlert.Text = "alert('新增成功！')";
            }
            else if (retValue == "0")
            {
                ltlAlert.Text = "alert('新增失败！请重试！')";
            }
            else if (retValue == "-1")
            {
                ltlAlert.Text = "alert('新增异常！请联系管理员！')";
            }
            else if (retValue != string.Empty)
            {
                ltlAlert.Text = "alert('" + retValue + "')";
            }

            dropDeparts.SelectedValue = "0";
            dropTypes.SelectedValue = "0";
            txbUserNo.Text = string.Empty;
            txbAtWork.Text = string.Empty;
            txbOffWork.Text = string.Empty;
            BindData();
        }
    }

    protected void gvUserSchedules_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        bool isSuccess = false;
        if (e.CommandName == "myDelete")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", e.CommandArgument.ToString());
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_deleteUserSchedule", param);
                isSuccess = Convert.ToBoolean(param[1].Value);
            }
            catch
            {
                isSuccess = false;
            }
            if (isSuccess)
            {
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！')";
            }
        }
    }
    protected void gvUserSchedules_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUserSchedules.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvUserSchedules_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUserSchedules.EditIndex = -1;
        BindData();
    }
    protected void gvUserSchedules_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvUserSchedules.DataKeys[e.RowIndex].Values["id"].ToString();
        TextBox txtIn = gvUserSchedules.Rows[e.RowIndex].FindControl("txtIn") as TextBox;
        TextBox txtOut = gvUserSchedules.Rows[e.RowIndex].FindControl("txtOut") as TextBox;

        if (string.IsNullOrEmpty(txtIn.Text))
        {
            this.Alert("上班时间不能为空！");
            return;
        }
        else if (!IsTime(txtIn.Text))
        {
            this.Alert("上班时间 正确格式是:07：00！");
            return;
        }

        if (!string.IsNullOrEmpty(txtOut.Text))
        {
            if (!IsTime(txtOut.Text))
            {
                this.Alert("下班时间 正确格式是:17：00！");
                return;
            }
        }

        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@inTime", txtIn.Text.Trim());
            param[2] = new SqlParameter("@outTime", txtOut.Text.Trim());
            param[3] = new SqlParameter("@uID", Session["uID"].ToString());
            param[4] = new SqlParameter("@uName", Session["uName"].ToString());
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_updateUserSchedule", param);

            if (!Convert.ToBoolean(param[5].Value))
            {
                this.Alert("数据保存失败！请联系管理员！");
                return;
            }
        }
        catch
        {
            this.Alert("数据操作失败！请联系管理员！");
            return;
        }

        gvUserSchedules.EditIndex = -1;
        BindData();
    }
}
