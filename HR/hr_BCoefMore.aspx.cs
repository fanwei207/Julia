using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class hr_BCoefMore : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserNo.Text = Request.QueryString["userNo"];
            txtUserName.Text = Server.UrlDecode(Request.QueryString["userName"]);
            txtCoef.Text = Server.UrlDecode(Request.QueryString["coef"]);

            hidDept.Value = Server.UrlDecode(Request.QueryString["dept"]);
            hidWorkShop.Value = Server.UrlDecode(Request.QueryString["line"]);

            BindDept();

            try
            {
                dropDept.SelectedIndex = -1;
                dropDept.Items.FindByText(hidDept.Value).Selected = true;
                BindWorkShop(Server.UrlDecode(Request.QueryString["dept"]));
            }
            catch
            {
                ;
            }

            BindData();
        }

    }

    protected void BindDept()
    {
        try
        {
            string strSql = " SELECT departmentID, Name";
            strSql += " From Departments";
            strSql += " Where isSalary='1'";
            strSql += " Order By departmentID";

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strSql);

            dropDept.DataSource = ds;
            dropDept.DataBind();

            dropDept.Items.Insert(0, new ListItem("--", "0"));
        }
        catch
        {
            ltlAlert.Text = "alert('获取部门失败！请联系管理员！');";
        }
    }

    protected void BindWorkShop(string dept)
    {
        try
        {
            string strSql = " SELECT w.id, w.name";
            strSql += " From Workshop w";
            strSql += " Inner Join departments d ON w.departmentID = d.departmentID";
            strSql += " Where d.name=N'" + dept + "'";
            strSql += "      And w.workshopID Is Null";
            strSql += " Order By w.code  ";

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strSql);

            dropWorkShop.DataSource = ds;
            dropWorkShop.DataBind();

            dropWorkShop.Items.Insert(0, new ListItem("--", "0"));
        }
        catch
        {
            ltlAlert.Text = "alert('获取工段失败！请联系管理员！');";
        }
    }

    protected void BindData()
    {
        try
        {
            string strSql = "sp_hr_selectBCoefMore";

            SqlParameter[] parmArray = new SqlParameter[2];
            parmArray[0] = new SqlParameter("@userNo", txtUserNo.Text.Trim());
            parmArray[1] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

            gvlist.DataSource = ds;
            gvlist.DataBind();

            ds.Dispose();
        }
        catch
        {
            ltlAlert.Text = "alert('获取数据失败！请联系管理员！');";
        }
    }

    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = " Update hr_BCoefMore";
            strSql += " Set bcf_isDel = 1 ";
            strSql += "     , bcf_deleteBy = " + Session["uID"].ToString();
            strSql += "     , bcf_deleteName = N'" + Session["uName"].ToString() + "'";
            strSql += "     , bcf_deleteDate = GetDate() ";
            strSql += " Where bcf_id = " + gvlist.DataKeys[e.RowIndex].Values["bcf_id"].ToString();

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败！');";
        }

        BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (dropWorkShop.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 工段！');";
            return;
        }
        //else
        //{
        //    if (hidDept.Value == dropDept.SelectedItem.Text && hidWorkShop.Value == dropWorkShop.SelectedItem.Text)
        //    {
        //        ltlAlert.Text = "alert('自身的工段无需添加！见帮助文档！');";
        //        return;
        //    }
        //}

        if (txtCoef.Text.Length == 0)
        {
            ltlAlert.Text = "alert('系数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                decimal _dc = Convert.ToDecimal(txtCoef.Text);

                if (_dc < 0)
                {
                    ltlAlert.Text = "alert('系数 不能小于0！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('系数 必须是大于0的数字！');";
                return;
            }
        }

        try
        {
            string strSql = "sp_hr_saveBCoefMore";

            SqlParameter[] parmArray = new SqlParameter[7];
            parmArray[0] = new SqlParameter("@userNo", txtUserNo.Text.Trim());
            parmArray[1] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
            parmArray[2] = new SqlParameter("@deptID", dropDept.SelectedValue);
            parmArray[3] = new SqlParameter("@workShopID", dropWorkShop.SelectedValue);
            parmArray[4] = new SqlParameter("@coef", txtCoef.Text);
            parmArray[5] = new SqlParameter("@uID", Session["uID"].ToString());
            parmArray[6] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！请关闭后重新操作一次！');";
            return;
        }

        BindData();
    }
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDept.SelectedIndex != 0)
        {
            BindWorkShop(dropDept.SelectedItem.Text);
        }
        else
        {
            dropWorkShop.Items.Clear();
            dropWorkShop.Items.Insert(0, new ListItem("--", "0"));
        }
    }
}