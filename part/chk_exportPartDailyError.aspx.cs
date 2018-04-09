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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class part_chk_exportPartDailyError : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strGenerateDate = Request.QueryString["generateDate"];
        DataTable dt = GetPartDailyError(Session["uID"].ToString(), strGenerateDate).Tables[0];

        if (dt != null && dt.Rows.Count > 0)
        {
            Session["EXHeader"] = "盘点结果导入失败数据";
            Session["EXTitle"] = dt.Rows[0]["EXTitle"].ToString();
            Session["EXSQL"] = dt.Rows[0]["EXSQL"].ToString();

            Response.Redirect("../public/exportexcel.aspx?ymd=hhmmss&rt=" + DateTime.Now.ToString());
        }
    }

    protected DataSet GetPartDailyError(string uID, string generateDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy",uID);
            param[1] = new SqlParameter("@generateDate",generateDate);

            return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_selectPartDailyError", param);
        }
        catch
        {
            return null;
        }
    }
}
