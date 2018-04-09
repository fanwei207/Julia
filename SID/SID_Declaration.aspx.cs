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

public partial class SID_SID_Declaration : BasePage
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
            gvSID.Columns[13].Visible = false;

            string strShipNo = txtShipNo.Text.Trim();
            DataTable dt = sid.CheckEistsCuster(strShipNo);
            if (dt == null || dt.Rows.Count <= 0)
            {
                btn_edit.Enabled = true;
            }
            else
            {
                btn_edit.Enabled = false;
            }
        }
    }

    protected void BindData()
    {
        //定义参数
        int uID = Convert.ToInt32(Session["uID"]);
        string strShipNo = txtShipNo.Text.Trim();
       
        if (Request.QueryString["type"] != null)
        {

            string strRet = Request.QueryString["strRet"];
            int number = strRet.IndexOf(",");
            if (number > 0)
            {
                strRet = strRet.Substring(0, number);
            }
            SqlDataReader reader = sid.GetSIDmstr(strRet);
            while (reader.Read())
            {
                txtShipNo.Text = reader["SID_nbr"].ToString();
                txtShipDate.Text = reader["SID_shipdate"].ToString();
                txtHarbor.Text = reader["SID_shipto"].ToString();
                txtShipVia.Text = reader["SID_via"].ToString();
            }
            if (number > 0)
            {
                string shipno = txtShipNo.Text.Trim();
                txtShipNo.Text = shipno.Substring(0, shipno.Length - 1);
            }

            btnDeclare.Enabled = false;
            btnSZXinv.Enabled = false;
            btnSZXpkg.Enabled = false;
            btnZQLinv.Enabled = false;
            btnZQLpkg.Enabled = false;
            //btnQuarantine.Enabled = false;
            btnConfirm.Enabled = false;
            btnSzxOrder.Enabled = false;
            btnZqlOrder.Enabled = false;
            //btn9City.Enabled = false;
            BtnZqlDeclare.Enabled = false;
            //txtShipNo.ReadOnly = false;

            if (sid.chkSplitIsExistTemp(uID))
            {
                lblSplit.Visible = true;
                lblSplitValue.Visible = true;
                lblSplitValue.Text = string.Format("{0:#0.00}", sid.SelectSplitAmountTemp(uID));
            }
            else
            {
                lblSplit.Visible = false;
                lblSplitValue.Visible = false;
            }

            gvSID.DataSource = sid.SelectDeclarationTemp(uID);
            gvSID.DataBind();
        }
        else
        {
            if (Request.QueryString["sno"] != null)
            {
                strShipNo = Convert.ToString(Request.QueryString["sno"]);
            }
            else
            {
                strShipNo = txtShipNo.Text.Trim();
            }

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            //txtShipNo.ReadOnly = true;
            txtShipNo.Text = sdi.ShipNo;
            if (sdi.ShipNo == null)
            {
                txtShipDate.Text = "";
            }
            else
            {
                txtShipDate.Text = string.Format("{0:yyyy-MM-dd}", sdi.SDate);
            }
            txtCustomer.Text = sdi.Customer;
            txtHarbor.Text = sdi.Harbor;
            txtShipVia.Text = sdi.ShipVia;
            txtTrade.Text = sdi.Trade;
            txtConveyance.Text = sdi.Conveyance;
            txtBL.Text = sdi.BL;
            txtVerfication.Text = sdi.Verfication;
            txtContact.Text = sdi.Contact;
            txtCountry.Text = sdi.Country;
            txtTax.Text = sdi.Tax;

            if (sid.chkSplitIsExist(strShipNo))
            {
                lblSplit.Visible = true;
                lblSplitValue.Visible = true;
                lblSplitValue.Text = string.Format("{0:#0.00}", sid.SelectSplitAmount(strShipNo));
            }
            else
            {
                lblSplit.Visible = false;
                lblSplitValue.Visible = false;
            }

            gvSID.DataSource = sid.SelectDeclaration(strShipNo);
            gvSID.DataBind();

            if (gvSID.Rows.Count > 0)
            {
                if (sdi.Status == "")
                {
                    btnDeclare.Enabled = true;
                    btnSZXinv.Enabled = true;
                    btnSZXpkg.Enabled = true;
                    btnZQLinv.Enabled = true;
                    btnZQLpkg.Enabled = true;
                    //btnQuarantine.Enabled = true;
                    btnSzxOrder.Enabled = true;
                    btnZqlOrder.Enabled = true;
                    //btn9City.Enabled = true;
                    btnConfirm.Enabled = false;
                    BtnZqlDeclare.Enabled = true;
                }
                else
                {
                    btnDeclare.Enabled = false;
                    btnSZXinv.Enabled = false;
                    btnSZXpkg.Enabled = false;
                    btnZQLinv.Enabled = false;
                    btnZQLpkg.Enabled = false;
                    //btnQuarantine.Enabled = false;
                    btnSzxOrder.Enabled = false;
                    btnZqlOrder.Enabled = false;
                    //btn9City.Enabled = false;
                    BtnZqlDeclare.Enabled = false;
                    btnConfirm.Enabled = true;
                }
                btnSave.Enabled = true;
            }
            else
            {
                btnDeclare.Enabled = false;
                btnSZXinv.Enabled = false;
                btnSZXpkg.Enabled = false;
                btnZQLinv.Enabled = false;
                btnZQLpkg.Enabled = false;
                //btnQuarantine.Enabled = false;
                btnSave.Enabled = false;
                btnConfirm.Enabled = false;
                btnSzxOrder.Enabled = false;
                btnZqlOrder.Enabled = false;
                //btn9City.Enabled = false;
                BtnZqlDeclare.Enabled = false;
                //txtShipNo.ReadOnly = false;
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

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        Response.Redirect("/SID/SID_Declaration.aspx?sno=" + strShipNo + "&rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtShipNo.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('出运编号 不能为空！'); ";
            return;
        }

        if (txtShipDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('出运日期 不能为空！'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtShipDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期 格式不正确！');";
                return;
            }
        }

        //定义参数
        string strOriShipNo = string.Empty;
        string strShipNo = txtShipNo.Text.Trim();

        if (Request.QueryString["sno"] != null)
        {
            strOriShipNo = Convert.ToString(Request.QueryString["sno"]);
        }

        int Ret = 0;

        SID_DeclarationInfo sdi = new SID_DeclarationInfo();

        sdi.ShipNo = txtShipNo.Text.Trim();
        sdi.ShipDate = txtShipDate.Text.Trim();
        sdi.Customer = txtCustomer.Text.Trim();
        sdi.Harbor = txtHarbor.Text.Trim();
        sdi.ShipVia = txtShipVia.Text.Trim();
        sdi.Trade = txtTrade.Text.Trim();
        sdi.Conveyance = txtConveyance.Text.Trim();
        sdi.BL = txtBL.Text.Trim();
        sdi.Verfication = txtVerfication.Text.Trim();
        sdi.Contact = txtContact.Text.Trim();
        sdi.Country = txtCountry.Text.Trim();
        sdi.Tax = txtTax.Text.Trim();
        sdi.uID = Convert.ToInt32(Session["uID"]);

        //判断是否存在相同的出运编号
        if (strOriShipNo != strShipNo)
        {
            Ret = sid.CheckShipNo(strShipNo);
        }

        switch (Ret)
        {
            case 0:
                if (Request.ServerVariables["Http_Referer"].ToString().IndexOf("?type=") > 0)
                {
                    if (sid.InsertDeclarationHeader(sdi))
                    {
                        ltlAlert.Text = "alert('报关单保存成功！'); window.location.href='";
                        ltlAlert.Text += Request.ServerVariables["Http_Referer"].Substring(0, Request.ServerVariables["Http_Referer"].ToString().IndexOf("?type="));
                        ltlAlert.Text += "?sno=" + strShipNo + "&rm=" + DateTime.Now.ToString() + "';";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('保存数据过程中出错！'); ";
                    }
                }

                if (Request.ServerVariables["Http_Referer"].ToString().IndexOf("?sno=") > 0)
                {
                    if (sid.UpdateDeclarationHeader(sdi, strOriShipNo))
                    {
                        ltlAlert.Text = "alert('报关单保存成功！'); window.location.href='";
                        ltlAlert.Text += Request.ServerVariables["Http_Referer"].Substring(0, Request.ServerVariables["Http_Referer"].ToString().IndexOf("?sno="));
                        ltlAlert.Text += "?sno=" + strShipNo + "&rm=" + DateTime.Now.ToString() + "';";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('保存数据过程中出错！'); ";
                    }
                }
                break;

            case -1:
                ltlAlert.Text = "alert('保存数据过程中出错！'); ";
                break;

            default:
                ltlAlert.Text = "alert('系统中已经存在和" + strShipNo + "相同的出运编号！'); ";
                break;
        }
    }

    protected void btnSZXinv_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isPo = chkPO.Checked;
        strFile = "SID_szx_inv_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_inv.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxInvToExcel("SZX报关发票", strShipNo, isPo);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_inv.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxInvToExcelByNPOI("SZX报关发票", strShipNo, isPo);
        }
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnZQLinv_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();

        strFile = "SID_zql_inv_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_inv.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlInvToExcel("ZQL报关发票", strShipNo);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_inv.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlInvToExcelByNPOI("ZQL报关发票", strShipNo);
        }
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnSZXpkg_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isPkgs = chkPkgs.Checked;
        bool isNotM3 = chkNotM3.Checked;
        strFile = "SID_szx_pkl_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_pkl.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxPklToExcel("SZX装箱单", strShipNo, isPkgs, isNotM3);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_pkl.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxPklToExcelByNPOI("SZX装箱单", strShipNo, isPkgs, isNotM3);
        }
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnZQLpkg_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isNotM3 = chkNotM3.Checked;
        bool isNotPkgs = chkNotPkgs.Checked;

        strFile = "SID_zql_pkl_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_pkl.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlPklToExcel("ZQL装箱单", strShipNo, isNotM3, isNotPkgs);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_pkl.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlPklToExcelByNPOI("ZQL装箱单", strShipNo, isNotM3, isNotPkgs);
        }        
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnDeclare_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isPkgs = chkPkgs.Checked;

        strFile = "SID_declaration_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_declaration.xls"), Server.MapPath("../Excel/") + strFile);

            excel.DeclarationToExcel("报关单", strShipNo, isPkgs);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_declaration.xls"), Server.MapPath("../Excel/") + strFile);

            excel.DeclarationToExcelByNPOI("报关单", strShipNo, isPkgs);
        }   
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnQuarantine_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isPkgs = chkPkgs.Checked;

        strFile = "SID_quarantine_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_quarantine.xls"), Server.MapPath("../Excel/") + strFile);

            excel.QuarantineToExcel("检疫单", strShipNo, isPkgs);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_quarantine.xls"), Server.MapPath("../Excel/") + strFile);

            excel.QuarantineToExcelByNPOI("报关单", strShipNo, isPkgs);
        }   
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnSzxOrder_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();

        strFile = "SID_szx_order_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_order.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxOrderToExcel("SZX订单", strShipNo);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_szx_order.xls"), Server.MapPath("../Excel/") + strFile);

            excel.SzxOrderToExcelByNPOI("SZX订单", strShipNo);
        }
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnZqlOrder_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();

        strFile = "SID_zql_order_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_order.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlOrderToExcel("ZQL订单", strShipNo);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_zql_order.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlOrderToExcelByNPOI("SZX订单", strShipNo);
        }
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btn9City_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isNotPkgs = chkNotPkgs.Checked;

        strFile = "SID_9City_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_9City.xls"), Server.MapPath("../Excel/") + strFile);

            excel.NineCityToExcel("九城", strShipNo, isNotPkgs);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_9City.xls"), Server.MapPath("../Excel/") + strFile);

            excel.NineCityToExcelByNPOI("九城", strShipNo, isNotPkgs);
        }   
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
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

        sdi.Net = Convert.ToDecimal(((TextBox)gvSID.Rows[e.RowIndex].FindControl("txtNet")).Text.Trim());

        try
        {
            sdi.Amount = Convert.ToDecimal(((TextBox)gvSID.Rows[e.RowIndex].FindControl("txtAmount")).Text.Trim());
        }
        catch
        {
            sdi.Amount = 0;
            ltlAlert.Text = "alert('修正总价 只能为数字！');";
            return;
        }



        sdi.SNO = gvSID.Rows[e.RowIndex].Cells[0].Text.Trim();
        sdi.uID = Convert.ToInt32(Session["uID"]);
        sdi.ShipNo = txtShipNo.Text.Trim();

        if (Request.QueryString["type"] != null)
        {
            if (sid.UpdateDeclarationDetailTemp(sdi))
            {
                ltlAlert.Text = "alert('更新成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
                //ltlAlert.Text = "alert('更新成功！');";
                gvSID.EditIndex = -1;
            }
            else
            {
                ltlAlert.Text = "alert('更新数据过程中出错！');";
            }
            BindData();
        }

        if (Request.QueryString["sno"] != null)
        {
            if (sid.UpdateDeclarationDetail(sdi))
            {
                ltlAlert.Text = "alert('更新成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('更新数据过程中出错！');";
                gvSID.EditIndex = -1;
                BindData();
            }
        }

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //定义参数

        SID_DeclarationInfo sdi = new SID_DeclarationInfo();

        sdi.ShipNo = txtShipNo.Text.Trim();
        sdi.uID = Convert.ToInt32(Session["uID"]);

        if (sid.UpdateDeclarationStatus(sdi))
        {
            ltlAlert.Text = "alert('确认成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "&rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('确认数据过程中出错！');";
        }
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
        String strShipNo = txtShipNo.Text.Trim();

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
            e.Row.Cells[1].Text = "合计:";
            e.Row.Cells[1].Style.Remove("text-align");
            e.Row.Cells[1].Style.Add("text-align", "Right");
            e.Row.Cells[2].Text = string.Format("{0:#0}", set);
            e.Row.Cells[2].Style.Remove("text-align");
            e.Row.Cells[2].Style.Add("text-align", "Right");
            e.Row.Cells[3].Text = string.Format("{0:#0}", pcs);
            e.Row.Cells[3].Style.Remove("text-align");
            e.Row.Cells[3].Style.Add("text-align", "Right");
            e.Row.Cells[4].Text = string.Format("{0:#0}", box);
            e.Row.Cells[4].Style.Remove("text-align");
            e.Row.Cells[4].Style.Add("text-align", "Right");
            e.Row.Cells[5].Text = string.Format("{0:#0}", pkgs);
            e.Row.Cells[5].Style.Remove("text-align");
            e.Row.Cells[5].Style.Add("text-align", "Right");
            e.Row.Cells[6].Text = string.Format("{0:#0.00}", weight);
            e.Row.Cells[6].Style.Remove("text-align");
            e.Row.Cells[6].Style.Add("text-align", "Right");
            e.Row.Cells[7].Text = string.Format("{0:#0.00}", net);
            e.Row.Cells[7].Style.Remove("text-align");
            e.Row.Cells[7].Style.Add("text-align", "Right");
            e.Row.Cells[8].Text = string.Format("{0:#0.00}", volume);
            e.Row.Cells[8].Style.Remove("text-align");
            e.Row.Cells[8].Style.Add("text-align", "Right");
            e.Row.Cells[9].Text = string.Format("{0:#0.00}", fixamount);
            e.Row.Cells[9].Style.Remove("text-align");
            e.Row.Cells[9].Style.Add("text-align", "Right");
            e.Row.Cells[10].Text = string.Format("{0:#0.00}", amount);
            e.Row.Cells[10].Style.Remove("text-align");
            e.Row.Cells[10].Style.Add("text-align", "Right");
            e.Row.Cells[11].Text = string.Format("{0:#0.00}", diff);
            e.Row.Cells[11].Style.Remove("text-align");
            e.Row.Cells[11].Style.Add("text-align", "Right");
        }
    }

    protected void BtnZqlDeclare_Click(object sender, EventArgs e)
    {
        //定义参数
        string strShipNo = txtShipNo.Text.Trim();
        bool isPkgs = chkPkgs.Checked;

        strFile = "SID_declaration_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        if (chkVersion.Checked == true)
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_Zql_Declaration.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlDeclarationToExcel("报关单", strShipNo, isPkgs);
        }
        else
        {
            excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_Zql_Declaration.xls"), Server.MapPath("../Excel/") + strFile);

            excel.ZqlDeclarationToExcelByNPOI("报关单", strShipNo, isPkgs);
        }   
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        string strRet = Request.QueryString["strRet"];
        Response.Redirect("/SID/SID_Declaration_Edit.aspx?type=temp&strRet=" + strRet, true);
    }
}
