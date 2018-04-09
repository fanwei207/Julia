using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Mold_RelateQad :BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        string mstrID = Request.QueryString["pk0"];

        if (txt_Qad.Text.Trim().Length == 0)
        {
            this.Alert("QAD号不能为空！");
            return;
        }
        else if (!CheckQad(txt_Qad.Text.Trim()))
        {
            this.Alert("不存在此QAD号！");
            return;
        }
        else
        {
            string sql = "sp_mold_insertRelatedQad";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@mstrID", Request.QueryString["pk0"]);
            param[1] = new SqlParameter("@Qad", txt_Qad.Text.Trim());
            param[2] = new SqlParameter("@createBy ", Convert.ToInt32(Session["uID"]));
            param[3] = new SqlParameter("@createName", Session["uName"].ToString());
            param[4] = new SqlParameter("@reValue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.Output;

                string msg =SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param).ToString();

                if (param[4].Value.ToString() != "1")
                {
                    this.Alert(msg);
                    return;
                }
           
           
            BindGridView();
        }
    }
    protected bool CheckQad(string Qad)
    {
        string sql = "sp_mold_CheckQad";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@plantCode", Convert.ToInt32(Session["PlantCode"]));
        param[1] = new SqlParameter("@Qad", Qad);

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];

        if (dt.Rows.Count > 0) return true;
        else return false;

    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string id = gv.DataKeys[intRow].Values["id"].ToString();
        string sql = "sp_mold_deleteRelatedQad";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);
        if (Convert.ToInt32(param[1].Value) != 1)
        {
            this.Alert("删除失败！");
            return;
        }
        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected override void BindGridView()
    {
        string sql = "sp_mold_selectRelatedQad";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrID", Request.QueryString["pk0"]);
      
        gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv.DataBind();
    }  
}