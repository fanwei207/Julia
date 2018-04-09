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

public partial class SID_SID_List : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

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
        string strPK = txtSysPKNo.Text.Trim();
        string strRef = txtSysPKRef.Text.Trim();
        string strNbr = txtShipNo.Text.Trim();
        string strDomain = txtDomain.Text.Trim();

        //Add By Shanzm 2011.02.14
        string strShipDate1 = txtShipDate1.Text.Trim();
        string strShipDate2 = txtShipDate2.Text.Trim();

        gvSID.DataSource = sid.SelectSIDList(strPK, strRef, strNbr, strDomain, strShipDate1, strShipDate2);
        gvSID.DataBind();
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
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

        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //定义参数
        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
       
        bool Ret = false;
        if (strRet.Length != 0)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);
             //int number = strRet.IndexOf(",");//-1
             //strRet = strRet.Substring(0, number);
            if (!sid.UpdateCustPrice(strRet, Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alert('CUST为C0000035绑定价格存在为零情况，请联系计划部！');";
                return;
            }

            Ret = false;

            Ret = sid.chkPriceIsZero(strRet);

            if (Ret)
            {
                Ret = false;

                Ret = sid.chkQAIsEmpty(strRet);

                if (Ret)
                {
                    Ret = false;

                    Ret = sid.InsertDeclarationTemp(strRet, struID);

                    if (Ret)
                    {
                        Response.Redirect("/SID/SID_Declaration.aspx?type=temp&strRet=" + strRet, true);
                    }
                    else
                    {
                        ltlAlert.Text = "alert('数据处理过程中出错，请重试！');";
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('选择要做报关的单据中有必须填写商检号而未填的数据！');";
                }
            }
            else
            {
                ltlAlert.Text = "alert('选择要做报关的单据中有0价格的数据！');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('没有选择要做报关的单据！');";
        }
    }

    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox) gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
            }
        }
        return strSelect;
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
    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
        }
    }
}
