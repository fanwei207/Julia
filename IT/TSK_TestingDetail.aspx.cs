using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using IT;

public partial class TSK_TestingDetail : System.Web.UI.Page
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTestingDesc.Text))
        {
            ltlAlert.Text = "alert('测试方案 不能为空！');";
            return;
        }
        else if(txtTestingDesc.Text.Length < 10)
        {
            ltlAlert.Text = "alert('测试方案 至少应该10字以上！');";
            return;
        }

        if (TaskHelper.InsertTaskTesting(Request.QueryString["tskdID"], txtTestingDesc.Text
                                , Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('保存失败！');";
            return;
        }
    }
}