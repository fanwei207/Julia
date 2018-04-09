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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using QADSID;

public partial class SID_SID_shipdetail : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
            DataBindHeader();
        }
    }

    protected void DataBindHeader()
    {
        DataTable dt = sid.GetShipDetail1(Convert.ToString(Request.QueryString["DID"]));
        lblPK.Text = dt.Rows[0].ItemArray[0].ToString();
        lblnbr.Text = dt.Rows[0].ItemArray[1].ToString();
        lblOutDate.Text = dt.Rows[0].ItemArray[2].ToString();
        lblVia.Text = dt.Rows[0].ItemArray[3].ToString();
        lblCtype.Text = dt.Rows[0].ItemArray[4].ToString();
        lblShipDate.Text = dt.Rows[0].ItemArray[5].ToString();
        lblshipto.Text = dt.Rows[0].ItemArray[6].ToString();
        lblsite.Text = dt.Rows[0].ItemArray[7].ToString();
        lblPKref.Text = dt.Rows[0].ItemArray[8].ToString();
        lblWeight.Text = dt.Rows[0].ItemArray[10].ToString();
        lblVolume.Text = dt.Rows[0].ItemArray[11].ToString();
        lblBox.Text = dt.Rows[0].ItemArray[12].ToString();
        lblPkgs.Text = dt.Rows[0].ItemArray[13].ToString();
        lblPrice.Text = dt.Rows[0].ItemArray[14].ToString();
    }

    protected void DataBind()
    {
        DataTable dt = sid.GetShipDetail(Convert.ToString(Request.QueryString["DID"]), Request.QueryString["RAD"].ToString());
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
        }
        gvShipdetail.DataSource = dt;
        gvShipdetail.DataBind();
        dt.Dispose();
    }

    protected void gvShipdetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvShipdetail.EditIndex = -1;
        DataBind();
    }

    protected void gvShipdetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 IErr = 0;
        IErr = sid.DelShipDetail(Convert.ToString(Session["uID"]), gvShipdetail.DataKeys[e.RowIndex].Values[0].ToString());
        if (IErr < 0)
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
        else
        {
            DataBind();
        }

    }

    protected void gvShipdetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvShipdetail.EditIndex = e.NewEditIndex;
        DataBind();

        String ptype = ((Label)gvShipdetail.Rows[e.NewEditIndex].Cells[12].FindControl("lblptype")).Text.Trim();
        if (ptype == "")
        {
            ptype = "--";
        }
        ((DropDownList)gvShipdetail.Rows[e.NewEditIndex].Cells[1].FindControl("drpPtype")).Items.FindByText(ptype).Selected = true;

    }

    protected void gvShipdetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //gvShipdetail.Rows[e.RowIndex].Cells[13].FindControl("txtprice").Visible = false;
        //gvShipdetail.Rows[e.RowIndex].Cells[14].FindControl("txtprice1").Visible = false;
        String strDID = gvShipdetail.DataKeys[e.RowIndex].Values[0].ToString();
        String strSNO = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[1].FindControl("txtSNO")).Text.ToString().Trim();
        String strBox = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[4].FindControl("txtbox")).Text.ToString().Trim();
        String strQa = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[5].FindControl("txtqa")).Text.ToString().Trim();
        String strWo = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[8].FindControl("txtWo")).Text.ToString().Trim();
        String strweight = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[11].FindControl("txtweight")).Text.ToString().Trim();
        String strvolume = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[12].FindControl("txtvolume")).Text.ToString().Trim();
        String strprice = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[13].FindControl("txtprice")).Text.ToString().Trim();
        String strprice1 = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[14].FindControl("txtprice1")).Text.ToString().Trim();
        String strmemo = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[22].FindControl("txtmemo")).Text.ToString().Trim();
        String strQtyset = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[3].FindControl("txtQtyset")).Text.ToString().Trim();
        String strQtypcs = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[17].FindControl("txtqtypcs")).Text.ToString().Trim();
        String strPkgs = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[18].FindControl("txtpkgs")).Text.ToString().Trim();
        String strPtype = ((DropDownList)gvShipdetail.Rows[e.RowIndex].Cells[14].FindControl("drpPtype")).SelectedValue.ToString();
        String strTcpPo = ((TextBox)gvShipdetail.Rows[e.RowIndex].Cells[9].FindControl("txtpo")).Text.ToString().Trim();

        #region 验证
        if (strSNO.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('系列 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strSNO);
            }
            catch
            {
                ltlAlert.Text = "alert('系列 必须是数字！');";
                return;
            }
        }

        if (strQtyset.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('出运套数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strQtyset);
            }
            catch
            {
                ltlAlert.Text = "alert('出运套数 必须是数字！');";
                return;
            }
        }

        if (strBox.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('箱数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strBox);
            }
            catch
            {
                ltlAlert.Text = "alert('箱数 必须是数字！');";
                return;
            }
        }

        if (strweight.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('重量 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strweight);
            }
            catch
            {
                ltlAlert.Text = "alert('重量 必须是数字！');";
                return;
            }
        }

        if (strvolume.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('体积 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strvolume);
            }
            catch
            {
                ltlAlert.Text = "alert('体积 必须是数字！');";
                return;
            }
        }

        if (strPkgs.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('件数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strPkgs);
            }
            catch
            {
                ltlAlert.Text = "alert('件数 必须是数字！');";
                return;
            }
        }

        if (strQtypcs.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('只数 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strQtypcs);
            }
            catch
            {
                ltlAlert.Text = "alert('只数 必须是数字！');";
                return;
            }
        }

        if (strprice.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strprice);
            }
            catch
            {
                ltlAlert.Text = "alert('价格 必须是数字！');";
                return;
            }
        }

        if (strprice1.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(strprice1);
            }
            catch
            {
                ltlAlert.Text = "alert('价格1 必须是数字！');";
                return;
            }
        }
        #endregion

        if (strprice.Trim() == "")
        {
            strprice = "0";
        }

        if (strprice1.Trim() == "")
        {
            strprice1 = "0";
        }

        Int32 IErr = 0;
        IErr = sid.UpdateShipDetail(Convert.ToString(Session["uID"]), strDID, strSNO, strBox, strQa, strWo, strweight, strvolume, strprice, strprice1, strmemo, strQtyset, strQtypcs, strPkgs, strPtype, strTcpPo);
        gvShipdetail.EditIndex = -1;

        if (IErr < 0)
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
        else
        {
            DataBind();
            DataBindHeader();
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SID_shipdetailAdd.aspx?DID=" + Convert.ToString(Request.QueryString["DID"]) + "&rm=" + DateTime.Now.ToString());
    }

    protected void gvShipdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Adds")
        {
            // ltlAlert.Text = "window.open('SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";
            Response.Redirect("SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&DID_ori=" + Convert.ToString(Request.QueryString["DID"]) + "&rm=" + DateTime.Now.ToString());
        }
    }

    protected void gvShipdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[13].Enabled = false;
            e.Row.Cells[14].Enabled = false;
            bool bSid = false;
            if (gvShipdetail.DataKeys[e.Row.RowIndex].Values[1].ToString() != string.Empty)
                bSid = Convert.ToBoolean(gvShipdetail.DataKeys[e.Row.RowIndex].Values[1].ToString());

            if (!bSid && Convert.ToBoolean(Request.QueryString["RAD"].ToString()))
            {
                e.Row.Cells[16].Enabled = false;
                e.Row.Cells[17].Enabled = false;
                e.Row.Cells[22].Enabled = false;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_ship.aspx?rt=" + DateTime.Now.ToString());
    }
}
