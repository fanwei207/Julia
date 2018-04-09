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

public partial class Performance_Forum_AddType : System.Web.UI.Page
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string typeName = txtTypeName.Text.Trim();
        if (string.Empty.Equals(typeName))
        {
            ltlAlert.Text = "alert('类型名不能为空');";
            return;
        }
        int flag = this.addType(Convert.ToInt32(Session["uID"]), typeName);
        if (flag == 1)
        {
            ltlAlert.Text = "alert('添加类型成功');";
            ltlAlert.Text += "window.close();";
        }
        if (flag == -1)
        {
            ltlAlert.Text = "alert('添加类型失败，该类型已存在');";
        }
        if (flag == 0)
        {
            ltlAlert.Text = "alert('添加类型失败');";
        }
        

    }

    private int addType(int uID, string typeName)
    {
        string sqlstr = "sp_Forum_addType";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@typeName",typeName)
        
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
}