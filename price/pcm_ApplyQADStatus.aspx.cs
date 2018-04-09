using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pcm_ApplyQADStatus : System.Web.UI.Page
{
    PCM_price pc = new PCM_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }

    private void bind()
    {
        string QAD = txtQAD.Text;
        string vender = txtVender.Text;
        string venderName = txtVenderName.Text;
        string status = ddlStatus.SelectedItem.Value;
        DataTable dt = pc.selectApplyQADStatus(Convert.ToInt32(Session["uID"]),QAD,vender,venderName,status);
        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ("1".Equals(((Label)e.Row.Cells[4].FindControl("lbIsAppoint")).Text))
            {
                ((Label)e.Row.Cells[4].FindControl("lbIsAppoint")).Text = "是";
            }
            else 
            {
                ((Label)e.Row.Cells[4].FindControl("lbIsAppoint")).Text = "否";
            }
            Label lbstatue = ((Label)e.Row.Cells[5].FindControl("lbStatue"));
            if ("1".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已提交";
            }
            else if ("2".Equals(lbstatue.Text))
            {
                lbstatue.Text = "正在询价";
            }
            else if ("3".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已报价";
            }
            else if ("4".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已核价";
            }
            else if ("5".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已提交申请";
            }
            else if ("6".Equals(lbstatue.Text))
            {
                lbstatue.Text = "财务核价审批通过";
            }
            else if ("-1".Equals(lbstatue.Text))
            {
                lbstatue.Text = "驳回";
            }
            else if ("-2".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已取消";
            }
            else if ("-3".Equals(lbstatue.Text))
            {
                lbstatue.Text = "已关闭（财务）";
            }

            //if ("1".Equals(((Label)e.Row.Cells[6].FindControl("lbQuoted")).Text))
            //{
            //    ((Label)e.Row.Cells[6].FindControl("lbQuoted")).Text = "已忽略";
            //}
            //else if (string.Empty.Equals(((Label)e.Row.Cells[6].FindControl("lbQuoted")).Text))
            //{
            //    ((Label)e.Row.Cells[6].FindControl("lbQuoted")).Text = "";
            //}
            //else
            //{
            //    ((Label)e.Row.Cells[6].FindControl("lbQuoted")).Text = "已处理";
            //}


            //if ("1".Equals(((Label)e.Row.Cells[7].FindControl("lbCheck")).Text))
            //{
            //    ((Label)e.Row.Cells[7].FindControl("lbCheck")).Text = "已忽略";
            //}
            //else if (string.Empty.Equals(((Label)e.Row.Cells[7].FindControl("lbQuoted")).Text))
            //{
            //    ((Label)e.Row.Cells[7].FindControl("lbCheck")).Text = "";
            //}
            //else
            //{
            //    ((Label)e.Row.Cells[7].FindControl("lbCheck")).Text = "已处理";
            //}

        }
    }
}