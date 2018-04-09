using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pcm_MadeInquiry : System.Web.UI.Page
{
    /// <summary>
    /// 从供应商列表新建
    /// 从询价单列表
    /// </summary>
    private int TempFrom
    {
        get
        {
            if (ViewState["TempFrom"] == null)
            {
                ViewState["TempFrom"] = 0;
            }
            return Convert.ToInt32(ViewState["TempFrom"]);
        }
        set
        {
            ViewState["TempFrom"] = value;
        }
    }

    PCM_price pc = new PCM_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TempFrom=Convert.ToInt32( Request["From"].ToString());
            lbVender.Text = Request["vender"].ToString();
            lbVenberName.Text = Request["venderName"].ToString();
            bind();
        
        }
    }
    protected void btnAddIM_Click(object sender, EventArgs e)
    {
        List<int> ilist = new List<int>();
        string IMID=string.Empty;
        for (int i = 0; i < gvNotInquiryList.Rows.Count; i++)
        {
            if (((CheckBox)(gvNotInquiryList.Rows[i].Cells[0].FindControl("chk"))).Checked)
            {
                int detId = Convert.ToInt32(gvNotInquiryList.DataKeys[i].Values["DetId"].ToString());

                ilist.Add(detId);
            }
        }
        if (ilist.Count == 0)
        {
            ltlAlert.Text = "alert('您未选中生成询价单的项目');";
            return;
        }

        if (pc.insertTOInquiry(ilist, lbVender.Text, lbVenberName.Text, Convert.ToInt32(Session["uID"]),out IMID ))
        {
            ltlAlert.Text = "alert('生成询价单成功');";
            //转跳到报价页面
            Response.Redirect("pcm_InquiryDet.aspx?IMID=" + IMID + "&ComeFrom=1");
        }
        else
        {
            ltlAlert.Text = "alert('生成询价单失败，请联系管理员');";
        }
        bind();

    }
    private void bind()
    {
        DataTable dt = pc.selectNotInquiryByVender(lbVender.Text);
        gvNotInquiryList.DataSource = dt;
        gvNotInquiryList.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("pcm_MadeInquiryList.aspx");
    }
    protected void gvNotInquiryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(gvNotInquiryList.DataKeys[e.Row.RowIndex].Values["isout"]))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Orange;
            }

        }
    }
}