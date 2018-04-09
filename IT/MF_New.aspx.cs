using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MF;

public partial class IT_MF_show : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RBLAuthorize.RepeatLayout = RepeatLayout.Table;
            RBLAuthorize.RepeatDirection = RepeatDirection.Horizontal;
            RBLAuthorize.RepeatColumns = 2;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string keywords;
        if (txtTitle.Text == string.Empty)
        {
            this.Alert("标题不能为空！");
        }
        if (txtDecription.Text == string.Empty)
        {
            this.Alert("描述不能为空！");
        }
        if (txtkey1.Text == string.Empty)
        {
            this.Alert("关键字不能为空！");
        }
        keywords = txtkey1.Text.Trim();
        if (txtkey2.Text != string.Empty)
        {
            keywords +=","+ txtkey2.Text;
        }
        if (txtkey3.Text != string.Empty)
        {
            keywords += "," + txtkey3.Text;
        }
        if (txtkey4.Text != string.Empty)
        {
            keywords += "," + txtkey4.Text;
        }

        if (MFHelper.insertMFmstr(txtTitle.Text.Trim(), txtDecription.Text.Trim(), RBLAuthorize.SelectedValue, keywords, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("新增成功！");
        }
        else
        {
            this.Alert("新增失败！");
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("MF_mstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}