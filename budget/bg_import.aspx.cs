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
using BudgetProcess;
using QCProgress;
using adamFuncs;

namespace BudgetProcess
{
    public partial class budget_bg_import : BasePage
    {
        adamClass chk = new adamClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        protected void uploadBtnExcel_ServerClick(object sender, EventArgs e)
        {
            FileOperate fop = new FileOperate(filenameExcel, Server.MapPath("/import"), 102400);
            fop.SectionLimit = Section.XLS;
            string strMsg = "";
            int intMsg = -1;
            DataTable dt = null;

            if (!fop.SaveFileToServer(ref strMsg))
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
            }
            else
            {
                fop.ImportBudgetExcel(Session["uID"].ToString().Trim(),ref intMsg, ref dt);
                if (intMsg == -1)
                {
                    ltlAlert.Text = "alert('数据导入失败!')";
                    return;
                }
                else if (intMsg == -2)
                {
                    ltlAlert.Text = "alert('Excel模版不正确!')";
                    return;
                }
                else 
                {
                    if (dt.Rows.Count > 0)
                    {
                        FileOperate.ExportToExcel(dt);
                    }

                    ltlAlert.Text = "alert('数据导入成功!')";
                }
            }
        }
}
}
