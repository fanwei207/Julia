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
using RD_WorkFlow;
using System.Data.SqlClient;


public partial class RDW_UL_doc : System.Web.UI.Page
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id;
            id = Convert.ToString(Request.QueryString["mid"]);
            //txtProject.Text = rdw.selectULproject(id);
            BindData(id);
            SqlDataReader read = rdw.selectULdet(id);
            if (read.Read())
            {
                txtProject.Text = read["UL_Project"].ToString().Trim();
                txtSentBy.Text = read["UL_docSentName"].ToString().Trim();
                txtSentTo.Text = read["UL_docSentTo"].ToString().Trim();
                txtEmali.Text = read["UL_docSentEmail"].ToString().Trim();
                txtDate1.Text = read["UL_docDate"].ToString().Trim();
            }
            read.Close();
            if (txtSentBy.Text == string.Empty)
            {
                txtSentBy.Text = Convert.ToString(Session["uName"]);
            }
            if (txtDate1.Text == string.Empty)
            {
                txtDate1.Text = System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
        }
    }

    public void BindData(string id)
    {
        //gvRDW.DataSource = rdw.selectULDoc(id);
        //gvRDW.DataBind();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&id=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtSentTo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('发送人不能为空');";
            return;
        }
        else if (txtDate1.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('发送时间不能为空');";
            return;
        }
        else if (txtEmali.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('联系方式不能为空');";
            return;
        }
        else if (txtSentBy.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('收件人不能为空');";
            return;
        }
        if (rdw.updateULDoc(Convert.ToString(Request.QueryString["mid"]), txtSentTo.Text.Trim(), txtSentBy.Text.Trim(), txtDate1.Text.Trim(), txtEmali.Text.Trim()))
        {
            ltlAlert.Text = "alert('保存成功');";
        }
        else
        {
            ltlAlert.Text = "alert('保存失败');";
        }
    }
}