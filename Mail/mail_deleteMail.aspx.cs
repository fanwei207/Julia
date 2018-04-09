using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
public partial class Mail_mail_deleteMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-30));
            txtEdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-29));
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DateTime bDate;
        DateTime eDate;
        DataTable bodyPath;
        DataTable attPath;
        ArrayList mails = new ArrayList();
        try {
            bDate=Convert.ToDateTime(txtBdate.Text);
            eDate=Convert.ToDateTime(txtEdate.Text);
            if (bDate > eDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('开始日期不能大于结束日期！');", true);
                return;
            }
            MailHelper.GetMailPath(bDate, eDate, out bodyPath, out attPath);
            if (bodyPath != null && bodyPath.Rows.Count > 0)
            {
                foreach (DataRow dr in bodyPath.Rows)
                {
                    if (File.Exists(Server.MapPath(dr.ItemArray[0].ToString())))
                            File.Delete(Server.MapPath(dr.ItemArray[0].ToString()));
                    mails.Add(dr.ItemArray[1].ToString());
                }
            }
            if (mails.Count > 0)
            {
                MailHelper.DeleteServerMail(mails);
            }
            if (attPath != null && attPath.Rows.Count > 0)
            {
                foreach (DataRow dr in attPath.Rows)
                {
                    if (File.Exists(Server.MapPath(dr.ItemArray[0].ToString())))
                        File.Delete(Server.MapPath(dr.ItemArray[0].ToString()));
                }
            }
            MailHelper.DeleteMail(bDate, eDate);
        }
        catch(FormatException fe)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(),"alert","alert('输入日期格式错误！');",true);
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("mail_oprating.aspx");
    }
}