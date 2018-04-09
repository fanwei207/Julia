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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class SID_PackingNbrCombine : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    SID_Packing pack = new SID_Packing();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtShipNo.Text = Request["nbr"];
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strNbr = txtShipNo.Text.Trim();
        gvPacking.DataSource = pack.SelectSIDNbrCombineList(strNbr);
        gvPacking.DataBind();
    }

    protected void gvPacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPacking.PageIndex = e.NewPageIndex;
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
        BindData();
    }

    protected string chkShipno()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                string str = gvPacking.Rows[i].Cells[4].Text.ToString().Trim();
                if (string.IsNullOrEmpty(gvPacking.Rows[i].Cells[4].Text.ToString().Trim()) || gvPacking.Rows[i].Cells[4].Text.ToString().Trim() == "&nbsp;")
                {
                    return strSelect = "0";
                }
            }
        }
        return strSelect;
    }

    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";
        int j = 0;

        //判断是否有选择
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvPacking.DataKeys[i].Value.ToString() + ",";
                j += 1;
            }
            if (j > 1)
            {
                strSelect = "0";
                return strSelect;
            }
        }
        return strSelect;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
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

    protected void gvPacking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
        }
    }

    protected void gvPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        string nbr = "";
        for (int i = 0; i < gvPacking.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvPacking.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                //strSelect = strSelect + gvPacking.DataKeys[i].Value.ToString() + ",";
                nbr = nbr + gvPacking.DataKeys[i]["Nbr"].ToString().Trim() + ",";
            }
        }
        //strSelect = strSelect.Substring(0, strSelect.Length - 1);
        txt_CombineNbr.Text = txt_CombineNbr.Text + nbr;
        string[] str = txt_CombineNbr.Text.Split(',');
        txtShipNo.Text = str[1];
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("../sid/SID_PackingList1.aspx?id=0&nbr=" + Request["nbr"] + "&rm=" + DateTime.Now.ToString());
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("../sid/SID_PackingList1.aspx?id=0&nbrCombine=" + "1" + "&nbr=" + Request["nbr"]+ "&rm=" + DateTime.Now.ToString());
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string nbr = txt_CombineNbr.Text;
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请选择要合并的出运单号!");
            return;
        }
        int uID = Convert.ToInt32(Session["uID"]);
        string id = pack.GetPackingNbrNo(Request["nbr"]); //DateTime.Now.ToString("yyyyMMddHHmmss");
        if (pack.PackingCombineNbrDelete(id, uID, Request["nbr"], Request["nbrCombine"]) != 0)
        {
            this.Alert("删除记录失败，请联系管理员!");
            return;
        }
        string[] str = nbr.Split(',');
        foreach (string i in str)
        {
            if (!string.IsNullOrEmpty(i.ToString()))
            {
                int ret = pack.PackingCombineNbr(i.ToString(), uID, id);
                if (ret != 0)
                {
                    if (pack.PackingCombineNbrDelete(id, uID, Request["nbr"], "0") != 0)
                    {
                        this.Alert("删除记录失败，请联系管理员!");
                        return;
                    }
                    this.Alert("出运单号已存在其他组合，请确认!");
                    return;
                }
            }
        }
        BindData();
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        txt_CombineNbr.Text = "";
    }
}
