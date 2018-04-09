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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using QADSID;


public partial class SID_SID_ShipEng : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            btnSave.Visible = false;
            btnAdd.Visible = false;
            gvShip.Columns[13].Visible = false;

            chkInspectDate.Checked = false;//为真，表示此用户有操作免检、验货日期的权限，特别是已报关时有用

            txtInspectDate.Enabled = false;
            txtInspectSite.Enabled = false;
            chkMianJian.Enabled = false;
            txt_InspMatchDate.Enabled = false;
            chkIsCabin.Enabled = false;
            btnSaveInsp.Visible = false;
            btnClearInsp.Visible = false;

            gvShip.Columns[18].Visible = false;
            gvShip.Columns[19].Visible = false;



            if (Request.QueryString["nbr"] != null && Request.QueryString["line"] != null)
            {
                //txtcreated.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-3));
                txt_SID_nbr.Text = Convert.ToString(Request.QueryString["nbr"]);
                txt_SID_line.Text = Convert.ToString(Request.QueryString["line"]);

                rad1.Checked = false;
                rad2.Checked = true;

                BindData();
            }
            else
            {

                txtcreated.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
                txtcreated1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
          
        }
    }

    protected void BindData()
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }
        if (rad3.Checked)
        {
            strRad = "3";
        }

        DataTable dt = sid.GetShip(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(), strRad,txt_SID_nbr.Text.Trim(),txt_SID_line.Text.Trim());

        gvShip.DataSource = dt;
        gvShip.DataBind();


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtPK.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('系统出运单号必须输入！');";
            return;
        }
        if (txtnbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出运单号必须输入！');";
            return;
        }
        if (txtVia.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('运输方式必须输入！');";
            return;
        }
        if (txtCtype.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('装箱类型必须输入！');";
            return;
        }
        if (txtsite.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('装箱地点必须输入！');";
            return;
        }
        if (txtShipDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出运日期必须输入！');";
            return;
        }
        else
        {
            if (!sid.IsDate(txtShipDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('出运日期应形如2009-09-09！');";
                return;
            }
        }

        if (txtOutDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出厂日期必须输入！');";
            return;
        }

        if (txtdomain.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('所在公司必须输入！');";
            return;
        }
        /*
         *  Add By Shanzm 2013-01-29
         * 
         * 首次维护出运单时，验货日期为空
         * 再次维护出运单时，如果是非免检的，此时更改出运日期的话，需要专人清空验货日期
        */
        if (chkMianJian.Checked || txtInspectDate.Text.Trim().Length > 0)
        {
            if (txtShipDate.Text.Trim() != txtOldShipDate.Text.Trim())
            {
                ltlAlert.Text = "alert('若更改出运日期，请先让相关人员清空验货日期、验货地点！');";
                return;
            }
        }

        sid.UpdateShipMstr(Convert.ToInt32(Session["uID"]), Convert.ToInt32(lblSID.Text.Trim()), txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtPKref.Text.Trim());
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int line;
        if ( string.IsNullOrEmpty(txt_SID_line.Text.Trim()))
        {
            BindData();
        }
        else
        {
            if (Int32.TryParse(txt_SID_line.Text.Trim(), out line))
            {
                if (line > 0)
                {
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('Line number must be greater than zero!');";
                }
            }
            else
            {
                ltlAlert.Text = "alert('Line number must be numeric!');";
            }

        }
       

        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";

    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            string url = "SID_shipdetailEng.aspx?DID=" + Server.UrlEncode(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + rad2.Checked.ToString() + "&rm=" + DateTime.Now;
            if (Request.QueryString["nbr"] != null && Request.QueryString["line"] != null)
            {
                url += "&nbr=" + Request.QueryString["nbr"] + "&line=" + Request.QueryString["line"];
            }
            Response.Redirect(url);

        }
        else if (e.CommandName.ToString() == "Del1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('没有删除的权限！');";
                return;
            }


            Int32 IErr = 0;

            IErr = sid.DelShipData(Convert.ToString(Session["uID"]), gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim());
            if (IErr < 0)
            {
                ltlAlert.Text = "alert('删除失败！');";
                return;
            }
            else
            {
                BindData();
            }
        }
        else if (e.CommandName.ToString() == "Edit1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('没有维护的权限！');";
                return;
            }

            int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
            txtPK.Text = gvShip.Rows[index].Cells[0].Text.Trim();
            txtPKref.Text = gvShip.Rows[index].Cells[1].Text.Trim();

            if (gvShip.Rows[index].Cells[1].Text.Trim() == "&nbsp;")
            {
                txtPKref.Text = "";
            }

            txtnbr.Text = gvShip.Rows[index].Cells[2].Text.Trim();
            txtVia.Text = gvShip.Rows[index].Cells[3].Text.Trim();
            txtCtype.Text = Server.HtmlDecode(gvShip.Rows[index].Cells[4].Text.Trim());
            txtsite.Text = gvShip.Rows[index].Cells[5].Text.Trim();

            txtShipDate.Text = gvShip.Rows[index].Cells[6].Text.Trim();
            if (gvShip.Rows[index].Cells[6].Text.Trim() == "&nbsp;")
            {
                txtShipDate.Text = "";
            }

            txtOutDate.Text = gvShip.Rows[index].Cells[7].Text.Trim();
            if (gvShip.Rows[index].Cells[7].Text.Trim() == "&nbsp;")
            {
                txtOutDate.Text = "";
            }

            txtdomain.Text = gvShip.Rows[index].Cells[15].Text.Trim();
            txtshipto.Text = gvShip.Rows[index].Cells[16].Text.Trim();
            if (gvShip.Rows[index].Cells[16].Text.Trim() == "&nbsp;")
            {
                txtshipto.Text = "";
            }

            //Add By Shanzm 2013-01-29
            txtOldShipDate.Text = txtShipDate.Text;
            if (sid.IsDate(gvShip.Rows[index].Cells[8].Text.Trim()))
            {
                txtInspectDate.Text = gvShip.Rows[index].Cells[8].Text.Trim();
            }
            else
            {
                txtInspectDate.Text = "";
            }

            //Add By Liuqj 2013-09-05
            if (gvShip.Rows[index].Cells[9].Text.Trim() == "&nbsp;")
            {
                txtInspectSite.Text = "";
            }
            else
            {
                txtInspectSite.Text = gvShip.Rows[index].Cells[9].Text.Trim();
            }

            if (sid.IsDate(gvShip.Rows[index].Cells[10].Text.Trim()))
            {
                txt_InspMatchDate.Text = gvShip.Rows[index].Cells[10].Text.Trim();
            }
            else
            {
                txt_InspMatchDate.Text = "";
            }
            if (gvShip.Rows[index].Cells[11].Text.Trim() == "是")
            {
                chkMianJian.Checked = true;
            }
            else
            {
                chkMianJian.Checked = false;
            }
            if (gvShip.Rows[index].Cells[12].Text.Trim() == "是")
            {
                chkIsCabin.Checked = true;
            }
            else
            {
                chkIsCabin.Checked = false;
            }

            lblSID.Text = gvShip.DataKeys[index].Value.ToString().Trim();
        }
        else if (e.CommandName == "Confirm1")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG1", Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "Confirm2")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG2", Session["uID"].ToString());

            BindData();
        }
    }

    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShip.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.Security["550000003"].isValid)
            {
                e.Row.Cells[18].Enabled = false;
            }

            if (this.Security["550001000"].isValid)
            {
                e.Row.Cells[19].Enabled = false;
            }
            //Add By WLW 2014-05-12
            if (!this.Security["550000005"].isValid)
            {
                e.Row.Cells[19].Enabled = false;
            }
            if (!this.Security["550000004"].isValid)
            {
                e.Row.Cells[18].Enabled = false;
            }

            //Add By Shanzm 2013-04-01
            if (chkInspectDate.Checked)
            {
                LinkButton linkEdit = (LinkButton)e.Row.FindControl("LinkButton1");
                linkEdit.Enabled = true;
            }

            e.Row.Cells[13].Text = "";
            e.Row.Cells[15].Text = "";
            gvShip.Columns[13].Visible = false;
            gvShip.Columns[15].Visible = false;
            gvShip.Columns[20].Visible = false;
        }
          

    }
    protected void rad1_CheckedChanged(object sender, EventArgs e)
    {
        gvShip.Columns[18].Visible = false;
        gvShip.Columns[19].Visible = false;

        BindData();
    }
    protected void rad2_CheckedChanged(object sender, EventArgs e)
    {
        if (rad2.Checked == true)
        {
            gvShip.Columns[18].Visible = true;
            gvShip.Columns[19].Visible = true;
        }

        BindData();
    }
    protected void rad3_CheckedChanged(object sender, EventArgs e)
    {
        gvShip.Columns[18].Visible = false;
        gvShip.Columns[19].Visible = false;

        BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPK.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('系统出运单号必须输入！');";
            return;
        }
        if (txtnbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出运单号必须输入！');";
            return;
        }
        if (txtVia.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('运输方式必须输入！');";
            return;
        }
        if (txtCtype.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('装箱类型必须输入！');";
            return;
        }
        if (txtsite.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('装箱地点必须输入！');";
            return;
        }
        if (txtShipDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出运日期必须输入！');";
            return;
        }
        else
        {
            if (!sid.IsDate(txtShipDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('出运日期应形如2009-09-09！');";
                return;
            }
        }

        if (txtOutDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出厂日期必须输入！');";
            return;
        }

        if (txtdomain.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('所在公司必须输入！');";
            return;
        }

        sid.UpdateShipMstr(Convert.ToInt32(Session["uID"]), 0, txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtPKref.Text.Trim());
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        BindData();
    }
    protected void btnSaveInsp_Click(object sender, EventArgs e)
    {
        if (lblSID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请先指定一条出运单！');";
            return;
        }

        //如果是免检的，则不用填写检验日期，否则，必填！并且在出运日期之前
        //如果没勾选，而验货日期又填写了，则自动认为是非免检的
        if (txtInspectDate.Text.Trim() != string.Empty)
        {
            chkMianJian.Checked = false;
        }

        if (!chkMianJian.Checked)
        {
            if (txtInspectDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('不是免检的产品，必须输入验货日期！');";
                return;
            }
            else
            {
                DateTime ship_date = Convert.ToDateTime(txtShipDate.Text.Trim());
                DateTime insp_date = Convert.ToDateTime(txtInspectDate.Text.Trim());

                if (insp_date > ship_date)
                {
                    ltlAlert.Text = "alert('验货日期不能晚于出运日期！');";
                    return;
                }
            }

            if (txtInspectSite.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('不是免检的产品，必须输入验货地点！');";
                return;
            }
            if (txt_InspMatchDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('不是免检的产品，必须输入预配日期！');";
                return;
            }

            if (!sid.IsDate(txt_InspMatchDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('预配日期应形如2009-09-09！');";
                return;
            }
        }

        sid.UpdateInspectInfo(Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(lblSID.Text.Trim()), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), chkMianJian.Checked, txt_InspMatchDate.Text.Trim(), chkIsCabin.Checked);
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        txtInspectDate.Text = "";
        txtInspectSite.Text = "";
        txt_InspMatchDate.Text = "";
        chkMianJian.Checked = false;
        chkIsCabin.Checked = false;
        BindData();
    }
    protected void btnClearInsp_Click(object sender, EventArgs e)
    {
        if (lblSID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请先指定一条出运单！');";
            return;
        }

        sid.ClearInspectInfo(Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(lblSID.Text.Trim()));
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        txtInspectDate.Text = "";
        txtInspectSite.Text = "";
        txt_InspMatchDate.Text = "";
        chkMianJian.Checked = true;
        chkIsCabin.Checked = false;
        BindData();
    }
    protected void chkMianJian_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMianJian.Checked)
        {
            txtInspectDate.Text = string.Empty;
            txtInspectSite.Text = string.Empty;
            txt_InspMatchDate.Text = string.Empty;
            txtInspectDate.Enabled = false;
            txtInspectSite.Enabled = false;
            txt_InspMatchDate.Enabled = false;
        }
        else
        {
            txtInspectDate.Enabled = true;
            txtInspectSite.Enabled = true;
            txt_InspMatchDate.Enabled = true;
        }
    }
}