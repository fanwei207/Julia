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

public partial class QC_qc_defect_item : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDefect.Text = Request.QueryString["def"].ToString();
            lblID.Text = Request.QueryString["ID"].ToString();
            int pID = int.Parse(Request.QueryString["pID"].ToString());
            
            ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (int.Parse(Request.QueryString["pID"].ToString()) == 3)
            Response.Redirect("qc_defect.aspx?pID=" + Request.QueryString["pID"].ToString());
        else
            Response.Redirect("qc_process_procedure.aspx");
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        bool bFlag = true;

        if (txtName.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('名称不能为空!');";
            bFlag = false;
        }

        int pID = int.Parse(Request.QueryString["pID"].ToString());
        int uID = int.Parse(Session["uID"].ToString());
        string strName = txtName.Text.Trim();


        FuncErrType errtype = FuncErrType.操作成功;

        if (bFlag)
        {
            errtype = oqc.AddDefectItem(lblID.Text.Trim(), pID, strName, uID);

            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            }
        }

        gvItem.EditIndex = -1;

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void gvItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvItem.EditIndex = -1;
        int pID = int.Parse(Request.QueryString["pID"].ToString());

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvItem.PageIndex * 20 + id;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }
    protected void gvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int defID = int.Parse(gvItem.DataKeys[e.RowIndex].Values[0].ToString());
        int pID = int.Parse(Request.QueryString["pID"].ToString());

        string strMsg = "";
        FuncErrType errtype = FuncErrType.操作成功;

        errtype = oqc.DeleteDefectItem(defID, ref strMsg);

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void gvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        bool bFlag = true;
        int pID = int.Parse(Request.QueryString["pID"].ToString());

        TextBox txtOrder = (TextBox)gvItem.Rows[e.RowIndex].Cells[2].FindControl("txtOrder");
        TextBox txtState = (TextBox)gvItem.Rows[e.RowIndex].Cells[3].FindControl("txtState");

        if (txtOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('排序不能为空!');";
            bFlag = false;
        }
        else 
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtOrder.Text.Trim())) 
            {
                ltlAlert.Text = "alert('排序格式不对!');";
                bFlag = false;
            }
        }

        if (bFlag)
        {
            int nOrder = int.Parse(txtOrder.Text.Trim());

            FuncErrType errtype = FuncErrType.操作成功;

            int ID = int.Parse(gvItem.DataKeys[e.RowIndex].Values[0].ToString());
            int defID = int.Parse(lblID.Text);
            string strName = ((TextBox)gvItem.Rows[e.RowIndex].Cells[1].Controls[1]).Text.Trim();

            errtype = oqc.ModifyDefectItem(ID, defID, pID, strName, nOrder, int.Parse(Session["uID"].ToString()), txtState.Text.Trim());

            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
                return;
            }

            gvItem.EditIndex = -1;
        }

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void gvItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvItem.EditIndex = e.NewEditIndex;

        int pID = int.Parse(Request.QueryString["pID"].ToString());

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItem.PageIndex = e.NewPageIndex;

        int pID = int.Parse(Request.QueryString["pID"].ToString());

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
    protected void chkTcp_CheckedChanged(object sender, EventArgs e)
    {
        Int32 nIndex = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;

        Int32 dItemID = Int32.Parse(gvItem.DataKeys[nIndex].Values[0].ToString());
        oqc.SetDefectItemTcpValue(dItemID, ((CheckBox)sender).Checked);

        int pID = int.Parse(Request.QueryString["pID"].ToString());

        ogv.GridViewDataBind(gvItem, oqc.GetDefectItem(int.Parse(lblID.Text.Trim()), pID));
    }
}
