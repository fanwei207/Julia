using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class product_m5_mstr_closeReason : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Text = Request["no"].ToString();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (txtReason.Text == string.Empty)
        {
            this.Alert("请填写关闭原因！");
            return;
        }
        if (!closeM5Mstr(Label1.Text, txtReason.Text))
        {
            this.Alert("项目变更关闭失败，请联系管理员！");
            return;
        }
        else
        {
            this.Alert("项目变更关闭成功！");
            return;
        }
    }
    private bool closeM5Mstr(string no, string reason)
    {
        try
        {
            string str = "sp_m5_closeM5Mstr";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@reason", reason);
            param[1] = new SqlParameter("@no", no);
            param[2] = new SqlParameter("@uID", Session["uID"]);
            param[3] = new SqlParameter("@uName", Session["uName"]);
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
            return Convert.ToBoolean(param[4].Value);
        }
        catch
        {
            return false;
        }
    }
}






