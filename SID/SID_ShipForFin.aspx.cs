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


public partial class SID_ShipForFin : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            txtPK.Text = Request["pk"];
            txtnbr.Text = Request["nbr"];
            if(string.IsNullOrEmpty(Request["createdate"]))
            {
                txtcreated.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            }
            else
            {
                txtcreated.Text = Request["createdate"];
            }
            if (string.IsNullOrEmpty(Request["createdate"]))
            {
                txtcreated1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
            else
            {
                txtcreated1.Text = Request["createdate1"];
            }
            if (!string.IsNullOrEmpty(Request["outdate"]))
            {
                txtShipDate.Text = Request["outdate"];
            }
            if (!string.IsNullOrEmpty(Request["outdate1"]))
            {
                txtShipDate1.Text = Request["outdate1"];
            }
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = sid.GetShip(txtPK.Text.Trim(), txtnbr.Text.Trim(), "", txtShipDate.Text.Trim(), txtShipDate1.Text.Trim(), txtsite.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(), ddl_type.SelectedValue);
        gvShip.DataSource = dt;
        gvShip.DataBind();


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
          if (gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
          {
              ltlAlert.Text = "alert('�����ǿ��У�');";
              return;
          }
          Response.Redirect("SID_shipdetailForFin.aspx?DID=" + Server.UrlEncode(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&pk=" + txtPK.Text.Trim() + "&nbr=" + txtnbr.Text.Trim() + "&createdate=" + txtcreated.Text.Trim() + "&createdate1=" + txtcreated1.Text.Trim() + "&outdate=" + txtShipDate.Text.Trim() + "&outdate1=" + txtShipDate1.Text.Trim() + "&RAD=" + true + "&rm=" + DateTime.Now);
        }
        else if (e.CommandName == "Confirm")
        {
            sid.ConfirmShipFINInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG2", Session["uID"].ToString());

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
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (this.Security["550000003"].isValid)
            //{
            //    //e.Row.Cells[24].Enabled = false;
            //}
        }

    }
    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        //DataTable dt = sid.SidExportExcel(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(), strRad);
        //if (dt.Rows.Count <= 0)
        //{
        //    this.Alert("������ѯ���ݣ�");
        //    return;
        //}

        //string title = "80^<b>���˵���</b>~^110^<b>ϵͳ���˵���</b>~^70^<b>��������</b>~^120^<b>��������</b>~^80^<b>װ��ص�</b>~^110^<b>���</b>~^110^<b>���ϱ���</b>~^200^<b>�ͻ�����</b>~^70^<b>��������</b>~^70^<b>����ֻ��</b>~^70^<b>����</b>~^150^<b>��ע</b>~^100^<b>���۵���</b>~^40^<b>�к�</b>~^100^<b>ATL������</b>~^100^<b>TCP������</b>~^120^<b>�깤����</b>~^120^<b>����ִ�����</b>~^";
        //this.ExportExcel(title, dt, false);
    }
    protected void btn_exportexcelByTemp_Click(object sender, EventArgs e)
    {
        System.Data.DataTable dt = sid.GetSidNbrVar(txtnbr.Text.Trim(), false);
        if (dt == null)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }
        else if (dt.Rows.Count <= 0)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }
        string strFile = string.Empty;
        SIDExcel.SIDExcel excel = null;
        //����EXCEL

        //�������
        string strShipNo = txtnbr.Text.Trim();
        //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        strFile = "SID_" + strShipNo + ".xls";
        excel = new SIDExcel.SIDExcel(Server.MapPath("/docs/SID_Ship_DetZQZ.xls"), Server.MapPath("../Excel/") + strFile);
        excel.SidDetZQZToExcelNewByNPOI("���˵�", strShipNo, Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(Session["PlantCode"]));
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedValue == "0")
        {
            gvShip.Columns[14].Visible = true;
        }
        else
        {
            gvShip.Columns[14].Visible = false;
        }
        BindData();

    }
}
