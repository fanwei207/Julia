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
using adamFuncs;
using QADSID;


public partial class SID_SID_ShipPicture : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    SID sid = new SID();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


   
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtShipDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtShipDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期格式不正确！');";
                return;
            }
        }

        if (txtShipDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d2 = Convert.ToDateTime(txtShipDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期格式不正确！');";
                return;
            }
        }
        if (txtpicDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d3 = Convert.ToDateTime(txtpicDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('照片日期格式不正确！');";
                return;
            }
        }
        if (txtpicDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d4 = Convert.ToDateTime(txtpicDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('照片日期格式不正确！');";
                return;
            }
        }
        BindData();
    }
    protected void BindData()
    {
        //定义参数
        string strPK = txtSysPKNo.Text.Trim();
        string strRef = txtSysPKRef.Text.Trim();
        string strNbr = txtShipNo.Text.Trim();
        string strDomain = txtDomain.Text.Trim();

        //Add By Shanzm 2011.02.14
        string strShipDate1 = txtShipDate1.Text.Trim();
        string strShipDate2 = txtShipDate2.Text.Trim();
        string site = txtsite.Text.Trim().Replace("*", "%");
        string outdate = txtoutdate.Text.Trim().Replace("*", "%");

        gvSID.DataSource = sid.SelectSIDListpicture(strPK, strRef, strNbr, strDomain, strShipDate1, strShipDate2, ddlstatus.SelectedValue, txtpicDate1.Text.Trim(), txtpicDate2.Text.Trim(), site, outdate);
        gvSID.DataBind();
    }
    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
            }
        }
        return strSelect;
    }
    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
            int index = e.Row.RowIndex;
            string status = gvSID.DataKeys[index].Values["SID_Status"].ToString().Trim();
            if (status == "true")
            {

                ((LinkButton)e.Row.Cells[10].FindControl("lnkstatus")).Text = "查看";
                ((CheckBox)e.Row.Cells[0].FindControl("chk_Select")).Visible = false;

                // ((LinkButton)e.Row.Cells[3].FindControl("close")).Font.Underline = false;

                // return;

            }
            else
            {
                ((LinkButton)e.Row.Cells[10].FindControl("lnkstatus")).Visible = false;
            }

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
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //定义参数
        string time = DateTime.Now.ToFileTime().ToString();
        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
        string[] strsid;
        bool Ret = false;
        if (strRet.Length != 0)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);

            Ret = false;

            Ret = sid.chkPicture(strRet);

            if (Ret)
            {
                Ret = false;

                strsid = strRet.Split(',');
                foreach (var item in strsid)
                {
                    sid.insertPicture(time, item, Session["uID"].ToString());
                }
                Response.Redirect("SID_UpPicture.aspx?from=new&shipid=" + time + "&sid=" + strRet + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else
            {
                ltlAlert.Text = "alert('所选订单有已上传照片！');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('没有选择需要上传照片的订单！');";
        }
    }
    protected void gvSID_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "show")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int SId = Convert.ToInt32(gvSID.DataKeys[index].Values["SID"].ToString());
            Response.Redirect("SID_ShowPicture.aspx?from=new&sid=" + SId + "&rt=" + DateTime.Now.ToFileTime().ToString());
         
        }
    }
}