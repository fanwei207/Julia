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

public partial class wo2_wo2_FinCale : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindYear();
            dropYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
            BindDate();
        }
    }

    private DataTable selectWorkOrderDate(int year)
    {
        try
        {
            SqlParameter param = new SqlParameter("@year", year);
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectWorkOrderDate", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private int insertWorkOrderDate(int year, int month, int userID, string userName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@year", year);
            param[1] = new SqlParameter("@month", month);
            param[2] = new SqlParameter("@userID", userID);
            param[3] = new SqlParameter("userName", userName);
            param[4] = new SqlParameter("@retValue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_insertWorkOrderDate", param);

            return Convert.ToInt32(param[4].Value);
        }
        catch
        {
            return -1;
        }
    }

    private bool updateWorkOrderDate(int year, int month, bool isOpen, int userID, string userName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@year", year);
            param[1] = new SqlParameter("@month", month);
            param[2] = new SqlParameter("@isOpen", isOpen);
            param[3] = new SqlParameter("@userID", userID);
            param[4] = new SqlParameter("userName", userName);
            param[5] = new SqlParameter("@retValue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_updateWorkOrderDate", param);

            return Convert.ToBoolean(param[5].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool deleteWorkOrderDate(int year, int month)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@year", year);
            param[1] = new SqlParameter("@month", month);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_deleteWorkOrderDate", param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }

    private void BindYear()
    {
        for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year; i++)
        {
            dropYear.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void BindDate()
    {
        DataTable dt = selectWorkOrderDate(Convert.ToInt32(dropYear.SelectedItem.Text));
        gvlist.DataSource = dt;
        gvlist.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropMonth.SelectedItem.Value == "0")
        {
            ltlAlert.Text = "alert('请选择月份!')";
            return;
        }

        if (dropYear.SelectedItem.Text == DateTime.Now.Year.ToString() && Convert.ToInt32(dropMonth.SelectedItem.Text) > DateTime.Now.Month)
        {
            ltlAlert.Text = "alert('不能添加超过当月的记录!')";
            return;
        }

        int nRet = 0;
        nRet = insertWorkOrderDate(Convert.ToInt32(dropYear.SelectedItem.Text), Convert.ToInt32(dropMonth.SelectedItem.Text), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());

        if (nRet == 0)
        {
            ltlAlert.Text = "alert('该月记录已经存在!')";
            return;
        }
        else if (nRet == -1)
        {
            ltlAlert.Text = "alert('添加失败，请联系管理员!')";
            return;
        }
        else if (nRet == 1)
        {
            BindDate();
        }
    }

    protected void chkSinger_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSinger = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkSinger.NamingContainer;
        int year = Convert.ToInt32(gvlist.DataKeys[row.RowIndex]["wo2_year"].ToString());
        int month = Convert.ToInt32(gvlist.DataKeys[row.RowIndex]["wo2_month"].ToString());

        if(updateWorkOrderDate(year, month, chkSinger.Checked, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
        {
            BindDate();
        }
        else
        {
            ltlAlert.Text = "alert('操作失败，请联系管理员!')";
            return;
        }
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            int year = Convert.ToInt32(param[0]);
            int month = Convert.ToInt32(param[1]);

            if (deleteWorkOrderDate(year, month))
            {
                BindDate();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败，请联系管理员!')";
                return;
            }
        }
    }

    protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDate();
    }
}