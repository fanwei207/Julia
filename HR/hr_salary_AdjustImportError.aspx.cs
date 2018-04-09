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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class HR_hr_salary_AdjustImportError : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = GetAdjustImportError(Convert.ToInt32(Session["uID"]));
            if (ds != null)
            {
                Session["EXHeader"] = "未导入计件工资调整";
                Session["EXTitle"] = ds.Tables[0].Rows[0]["EXTitle"].ToString();
                Session["EXSQL"] = ds.Tables[0].Rows[0]["EXSQL"].ToString();

                Response.Redirect("../public/exportexcel.aspx?ymd=hhmmss&rt=" + DateTime.Now.ToString());
            }
        }
    }

    protected DataSet GetAdjustImportError(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", uID);
            return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectAdjustImportError", param);
        }
        catch
        {
            return null;
        }
    }
}
