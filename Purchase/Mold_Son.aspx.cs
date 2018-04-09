using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Purchase_Mold_Son : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bindTxtVend();
            BindGridView();
             //, new SqlParameter("@mold_Nbr" , Request.QueryString["fk0"])
             // , new SqlParameter("@mold_Vend" , Request.QueryString["fk1"])
        }
    }

    private void bindTxtVend()
    {
        string sqlstr = "SELECT  Mold_Vend FROM dbo.Mold_mstr WHERE Mold_ID = '" + Request.QueryString["pk0"] + "'";

        txt_vend.Text = SqlHelper.ExecuteScalar(strConn, CommandType.Text, sqlstr).ToString();
    }

    protected override void BindGridView()
    {
        string sql = "sp_mold_selectSonByMoldId";
        SqlParameter[] param = new SqlParameter[]
        {
              new SqlParameter("@mstrID", Request.QueryString["pk0"])
        }; 
            
   

        gvInfo.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gvInfo.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int flag = 0;
        try
        {
            flag = Convert.ToInt32(txt_weighting.Text.Trim());

            if (flag <= 0)
            {
                this.Alert("权只能是大于0的整数");
                return;
            }
        }
        catch
        {
            this.Alert("权只能是大于0的整数");
            return;
        }
        

        string sqlstr = "sp_mold_insertSonByMoldId";

        SqlParameter[] param = new SqlParameter[]{
          new SqlParameter("@mstrID", Request.QueryString["pk0"])
          , new SqlParameter("@mold_Nbr" , txt_Nbr.Text.Trim())
          , new SqlParameter("@mold_Vend" , txt_vend.Text.Trim())
          , new SqlParameter("@uID" , Session["uID"].ToString())
          , new SqlParameter("@uName" , Session["uName"].ToString())
          , new SqlParameter("@weighting" , flag)
            
        };

        string msg = SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();

        if (msg.Equals("添加成功！"))
        {
            this.Alert(msg);
            BindGridView();
        }
        else
        {
            this.Alert(msg);
        }


       
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string id = gvInfo.DataKeys[intRow].Values["id"].ToString();

        string sqlstr = "sp_mold_deleteSonByID";

        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@id",id)
            , new SqlParameter("@uID" , Session["uID"].ToString())
            , new SqlParameter("@uName" , Session["uName"].ToString())
        };

        string msg = SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();

        if (msg.Equals("删除成功！"))
        {
            this.Alert(msg);
            BindGridView();
        }
        else
        {
            this.Alert(msg);
        }

    }
}