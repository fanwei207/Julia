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

public partial class Purchase_Mold_ChangeStatus : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["IntStatus"] == "1")
            {
                btn_change.Text = "转为停用";
            }
        }
    }
    protected void btn_change_Click(object sender, EventArgs e)
    {
        if(txt_reason.Text.Trim().Length == 0)
        {
            this.Alert("理由必填！");
            return;
        }
        string sql = "sp_mold_changeStatus";
        try
        {
            int type = 1;
            if (Request.QueryString["IntStatus"] == "1")
                type = 0;
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@detailNbr", Request.QueryString["detID"]);            
            param[1] = new SqlParameter("@changeType", type);
            param[2] = new SqlParameter("@createName", Session["uName"].ToString());
            param[3] = new SqlParameter("@createBy ", Convert.ToInt32(Session["uID"]));
            param[4] = new SqlParameter("@reason", txt_reason.Text.Trim());
            param[5] = new SqlParameter("@retValue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);
        }
        catch
        {
            this.Alert("转换失败！");
            
        }
        this.Alert("转换成功！");
        btn_change.Enabled = false;
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mold_AllLists.aspx");
    }
}