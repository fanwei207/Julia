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
//using Microsoft.Office.Interop.Excel;
using System.IO;
using adamFuncs;
using QADSID;
using System.Data.SqlClient;

public partial class SID_Declaration_Edit : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();
    string strFile = string.Empty;
    ExcelHelper.ExcelHelper excel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        int uID = Convert.ToInt32(Session["uID"]);

        if (Request.QueryString["type"] != null)
        {

            string strRet = Request.QueryString["strRet"];

            gvSID.DataSource = sid.SelectDeclarationEditTemp(uID);
            gvSID.DataBind();
        }
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }


    protected void gvSID_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSID.EditIndex = -1;
        BindData();
    }

    protected void gvSID_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSID.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void gvSID_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //定义参数
        SID_DeclarationInfo sdi = new SID_DeclarationInfo();

        //sdi.Weight = Convert.ToDecimal(((TextBox)gvSID.Rows[e.RowIndex].FindControl("txtWeight")).Text.Trim());

        try
        {
            sdi.New_Price = Convert.ToDecimal(((TextBox)gvSID.Rows[e.RowIndex].FindControl("txtNewPrice")).Text.Trim());
        }
        catch
        {
            sdi.New_Price = 0;
            ltlAlert.Text = "alert('修正价格 只能为数字！');";
            return;
        }


        sdi.SNO = gvSID.Rows[e.RowIndex].Cells[2].Text.Trim();
        //sdi.SNO = gvSID.Rows[e.RowIndex].Cells[0].Text.Trim();
        sdi.SID_DID = Convert.ToInt32(gvSID.DataKeys[e.RowIndex].Values["sid_did"].ToString());//Rows[e.RowIndex].Cells[0].Text.Trim();
        sdi.uID = Convert.ToInt32(Session["uID"]);

        if (Request.QueryString["type"] != null)
        {
            if (sid.UpdateDeclarationDetailEditTemp(sdi))
            {
                if (sdi.New_Price == 0)
                {
                    //ltlAlert.Text = "alert('更新成功,但价格为零(0)！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
                    ltlAlert.Text = "alert('更新成功,但价格为零(0)！');";
                    gvSID.EditIndex = -1;
                }
                else
                {
                    //ltlAlert.Text = "alert('更新成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
                    ltlAlert.Text = "alert('更新成功！');";
                    gvSID.EditIndex = -1;
                }
            }
            else
            {
                ltlAlert.Text = "alert('更新数据过程中出错！');";
            }
            BindData();
        }

        //if (Request.QueryString["sno"] != null)
        //{
        //    if (sid.UpdateDeclarationDetail(sdi))
        //    {
        //        ltlAlert.Text = "alert('更新成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
        //    }
        //    else
        //    {
        //        ltlAlert.Text = "alert('更新数据过程中出错！');";
        //        gvSID.EditIndex = -1;
        //        BindData();
        //    }
        //}

    }

    //定义套数合计,只数合计,箱数合计,件数合计,毛重合计,净重合计,体积合计,价值合计,修正价值合计,差异合计
    private int set = 0;
    private int pcs = 0;
    private int box = 0;
    private int pkgs = 0;
    private decimal weight = 0;
    private decimal net = 0;
    private decimal volume = 0;
    private decimal fixamount = 0;
    private decimal amount = 0;
    private decimal diff = 0;

    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //定义参数
        bool Ret = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SID_DeclarationInfo sdi = (SID_DeclarationInfo)e.Row.DataItem;
            set += Convert.ToInt32(sdi.QtySet);
            pcs += Convert.ToInt32(sdi.QtyPcs);
            box += Convert.ToInt32(sdi.QtyBox);
            pkgs += Convert.ToInt32(sdi.QtyPkgs);
            weight += Convert.ToDecimal(sdi.Weight);
            net += Convert.ToDecimal(sdi.Net);
            volume += Convert.ToDecimal(sdi.Volume);
            fixamount += Convert.ToDecimal(sdi.FixAmount);
            amount += Convert.ToDecimal(sdi.Amount);
            diff += Convert.ToDecimal(sdi.Diff);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "合计:";
            e.Row.Cells[3].Style.Remove("text-align");
            e.Row.Cells[3].Style.Add("text-align", "Right");
            e.Row.Cells[4].Text = string.Format("{0:#0}", set);
            e.Row.Cells[4].Style.Remove("text-align");
            e.Row.Cells[4].Style.Add("text-align", "Right");
            e.Row.Cells[5].Text = string.Format("{0:#0}", pcs);
            e.Row.Cells[5].Style.Remove("text-align");
            e.Row.Cells[5].Style.Add("text-align", "Right");
            e.Row.Cells[6].Text = string.Format("{0:#0}", box);
            e.Row.Cells[6].Style.Remove("text-align");
            e.Row.Cells[6].Style.Add("text-align", "Right");
            e.Row.Cells[7].Text = string.Format("{0:#0}", pkgs);
            e.Row.Cells[7].Style.Remove("text-align");
            e.Row.Cells[7].Style.Add("text-align", "Right");
            e.Row.Cells[8].Text = string.Format("{0:#0.00}", weight);
            e.Row.Cells[8].Style.Remove("text-align");
            e.Row.Cells[8].Style.Add("text-align", "Right");
            e.Row.Cells[9].Text = string.Format("{0:#0.00}", net);
            e.Row.Cells[9].Style.Remove("text-align");
            e.Row.Cells[9].Style.Add("text-align", "Right");
            e.Row.Cells[10].Text = string.Format("{0:#0.00}", volume);
            e.Row.Cells[10].Style.Remove("text-align");
            e.Row.Cells[10].Style.Add("text-align", "Right");
            e.Row.Cells[13].Text = string.Format("{0:#0.00}", fixamount);
            e.Row.Cells[13].Style.Remove("text-align");
            e.Row.Cells[13].Style.Add("text-align", "Right");
            //e.Row.Cells[12].Text = string.Format("{0:#0.00}", amount);
            //e.Row.Cells[12].Style.Remove("text-align");
            //e.Row.Cells[12].Style.Add("text-align", "Right");
            e.Row.Cells[14].Text = string.Format("{0:#0.00}", diff);
            e.Row.Cells[14].Style.Remove("text-align");
            e.Row.Cells[14].Style.Add("text-align", "Right");
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        string strRet = Request.QueryString["strRet"];
        Response.Redirect("/SID/SID_Declaration.aspx?type=temp&strRet=" + strRet, true);
    }
}
