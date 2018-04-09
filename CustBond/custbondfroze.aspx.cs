using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
/*using OEAppServer;*/

public partial class CustBond_custbondfroze : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year - 1, 1, 1);
            date.Value = d1.ToString("yyyy-MM-dd");
            date1.Value = now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
    }  /*数据由前台AJAX取*/

    /*
    protected void custbondrun()
    {
            AppServer run = new AppServer();
            String dstring = "-";
            dstring += "\n" + "d:\bond -";
            dstring += ".";
            try
            {
                if (run.batchcim("szx", dstring,".9.23",false,0,""))
                {
                    ltlAlert.Text = "alert('运算已开始')";
                }
                else
                {
                     ltlAlert.Text = "";
                }

            }
            catch (Exception ex)
            {
                 ltlAlert.Text = "alert(" + ex.ToString() + ")";
            }
        
    }*/ //远程调用运行 计算程序

}