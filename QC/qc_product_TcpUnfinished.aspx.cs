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
using QCProgress;

public partial class QC_qc_product_unfinished : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("100103234", "TCP��Ʒ�������");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));

            txtDate2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

            if (!this.Security["100103234"].isValid)
            {
                gvProduct.Columns[9].Visible = false;
            }
            GridViewBind();
        }
    }

    private void GridViewBind()
    {

        DataSet _dataset = oqc.GetProductTCPUnfinished(txtOrder.Text.Trim(),
                                            txtID.Text.Trim(),
                                            txtDate.Text.Trim(),
                                            txtDate2.Text.Trim(),
                                            int.Parse(Session["PlantCode"].ToString()));
       
        ogv.GridViewDataBind(gvProduct, _dataset.Tables[0]);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "free")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string order = gvProduct.DataKeys[index]["wo_nbr"].ToString().Trim();
            string id = gvProduct.DataKeys[index]["wo_lot"].ToString().Trim();

            FuncErrType error = FuncErrType.�����ɹ�;
            error = oqc.AddProduct("", "", DateTime.Now.ToString("yyyy-MM-dd"), "", "", "", id, order, "", 0, "", true, int.Parse(Session["uID"].ToString().Trim()), "���", "", "", false, "", true);
            ltlAlert.Text = "alert('" + error + "')";
            this.GridViewBind();

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet _dataset = oqc.GetProductTCPUnfinished(txtOrder.Text.Trim(),
                                            txtID.Text.Trim(),
                                            txtDate.Text.Trim(),
                                            txtDate2.Text.Trim(),
                                            int.Parse(Session["PlantCode"].ToString()));
        string EXTitle = "<b>�ӹ���</b>~^<b>�к�</b>~^120^<b>���Ϻ�</b>~^<b>��ֹ����</b>~^"
                          + "<b>��������</b>~^<b>�깤����</b>~^<b>��������</b>~^<b>�ص�</b>~^<b>״̬</b>~^";
        ExportExcel(EXTitle, _dataset.Tables[0], false);
    }
}
