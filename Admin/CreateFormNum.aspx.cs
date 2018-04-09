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

public partial class CreateFormNum : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindYear();
            BindTypes();
            GridViewBind();
            this.gv.Columns[0].Visible = false;
        }
    }

    private void BindTypes()
    {
        try
        {
            string strName = "sp_form_selecttype";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@formid", ddl_formtype.SelectedValue == "" || ddl_formtype.SelectedValue == "--请选择--" ? "0" : ddl_formtype.SelectedValue);
            param[1] = new SqlParameter("@formyear", Convert.ToInt32(ddl_year.SelectedValue));

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            ddl_formtype.DataSource = ds;
            ddl_formtype.DataBind();
            ddl_formtype.Items.Insert(0, "--请选择--");
            ddl_formtype.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取名称列表失败!");
        }
    }

    private void BindYear()
    {
        try
        {
            ddl_year.Items.Add(System.DateTime.Now.ToString("yyyy"));
            ddl_year.Items.Add(System.DateTime.Now.AddYears(1).ToString("yyyy"));
        }
        catch
        {
            this.Alert("获取名称列表失败!");
        }
    }

    private void GridViewBind() 
    {
        try
        {
            string strName = "sp_form_selectNumlist";
            SqlParameter[] param = new SqlParameter[1];
            if (ddl_formtype.SelectedIndex == 0)
            {
                param[0] = new SqlParameter("@typeid", "0");
            }
            else
            {
                param[0] = new SqlParameter("@typeid", ddl_formtype.SelectedValue);
            }

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    private bool checkNoteIsExist(string type)
    {
        try
        {
            string strName = "sp_vc_checkVendCompCateIsExist";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@name", type);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strName = "sp_vc_deleteVendCompCate";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }

    protected void btn_createformnum_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        DataTable dt1 = null;
        DataTable dt2 = null;
        string formnum = "";
        string lastformnum = "";
        try
        {
            string strName = "sp_form_createnum";
            string strtype = ddl_formtype.SelectedValue;
            string stryear = ddl_year.SelectedValue;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@formid", strtype);
            param[1] = new SqlParameter("@formyear", stryear);

            dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];

            if (dt.Rows.Count > 0)
            {
                formnum = dt.Rows[0]["form_id"].ToString();
                lastformnum = dt.Rows[0]["form_lastnum"].ToString();
            }
        }
        catch
        {
            this.Alert("获取名称列表失败!");
        }
        try
        {
            string strCheck = "sp_form_CheckNumExists";
            SqlParameter[] param1 = new SqlParameter[1];
            param1[0] = new SqlParameter("@formnum", formnum);

            dt1 = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strCheck, param1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                this.Alert("生成单号已存在,请联系管理员!");
                return;
            }
        }
        catch
        {
            this.Alert("生成单号验证失败,请联系管理员!");
        }

        if (string.IsNullOrEmpty(lastformnum))
        {
            this.Alert("生成单号编码失败,请联系管理员!");
            return;
        }
        try
        {
            string strInsert = "sp_form_InsertNum";
            SqlParameter[] param2 = new SqlParameter[5];
            param2[0] = new SqlParameter("@typeid", ddl_formtype.SelectedValue);
            param2[1] = new SqlParameter("@formnum", formnum);
            param2[2] = new SqlParameter("@formlastnum", lastformnum);
            param2[3] = new SqlParameter("@uID", Session["uID"]);
            param2[4] = new SqlParameter("@uName", Session["uName"]);

            int result = (int)SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strInsert, param2);
            if (result > 0)
            {
                lb_formnum.Text = formnum;
                this.Alert("生成单号成功!\\n" +"单号:"+ formnum);
            }
            else
            {
                this.Alert("生成单号保存失败!");
                return;
            }
        }
        catch
        {
            this.Alert("生成单号保存失败,请联系管理员!");
        }
        GridViewBind();
        
    }
    protected void ddl_formtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTypes();
    }
}
