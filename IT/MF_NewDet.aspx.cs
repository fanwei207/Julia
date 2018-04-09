using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MF;
using System.Data.SqlClient;

public partial class IT_MF_NewDet : BasePage
{
    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }
    public string _ID
    {
        get
        {
            return ViewState["ID"].ToString();
        }
        set
        {
            ViewState["ID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _parentID = Request.QueryString["id"];
            if (Request.QueryString["detid"] != null)
            {
                _ID = Request.QueryString["detid"];
                SqlDataReader read = MFHelper.selectMFdetone(_ID);
                while (read.Read())
                {
                    txtTitle.Text = read["FM_title"].ToString();
                    txtresponsible.Text = read["FM_depart"].ToString();
                    txtDecription.Text = read["FM_desc"].ToString();
                    lblstep.Text = read["FM_Step"].ToString();
                    btnsave.Text = "Save";
                }
            }
           
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (lblstep.Text == "0")
        {
            if (MFHelper.insertMFdet(_parentID, txtTitle.Text.Trim(), txtresponsible.Text.Trim(), txtDecription.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString()))
            {
                this.Alert("新增成功！");
                ltlAlert.Text = "window.close();";
            }
            else
            {
                this.Alert("新增失败！");
            }
        }
        else
        {
            if (MFHelper.updateMFdet(_parentID, txtTitle.Text.Trim(), txtresponsible.Text.Trim(), txtDecription.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(),lblstep.Text))
            {
                this.Alert("保存成功！");
                ltlAlert.Text = "window.close();";
            }
            else
            {
                this.Alert("保存失败！");
            }
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
}