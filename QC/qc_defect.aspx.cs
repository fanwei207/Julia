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

public partial class QC_qc_defect : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropModule.DataSource = oqc.GetInspectModule(1);
            dropModule.DataBind();
            dropModule.Items.Insert(0, new ListItem("--", "0"));
            dropModule.SelectedIndex = 2;

            dropType.DataSource = oqc.GetDefectType(string.Empty, int.Parse(dropModule.SelectedValue));
            dropType.DataBind();
            dropType.Items.Insert(0,new ListItem("--","0"));

            if (Request.QueryString["pID"] != null && Request.QueryString["pID"].ToString() != string.Empty) 
            {
                dropModule.Items.FindByValue(Request.QueryString["pID"].ToString()).Selected = true;
            }

            dropModule.Focus();

            GridViewBind();
        }
        else 
        {
            ogv.ResetGridView(gvDefect);
        }
    }

    private void GridViewBind() 
    {
        int pID = 3;
        int tID = int.Parse(dropType.SelectedValue);
        string strName = txtName.Text.Trim();

        ogv.GridViewDataBind(gvDefect, oqc.GetDefect(pID, tID, strName));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool bFlag = true;

        if (txtName.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('名称不能为空!');";
            bFlag = false;
        }

        if (dropType.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('请选择一项类别!');";
            bFlag = false;
        }

        if (bFlag)
        {
            FuncErrType errtype = FuncErrType.操作成功;
            errtype = oqc.AddDefect(txtName.Text.Trim(), int.Parse(dropType.SelectedValue), int.Parse(Session["uID"].ToString()));

            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
                return;
            }
        }

        gvDefect.EditIndex = -1;

        gvDefect.PageIndex = 0;

        txtName.Text = string.Empty;
        dropType.SelectedIndex = 0;

        GridViewBind();
    }
    protected void gvDefect_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDefect.EditIndex = -1;

        GridViewBind();
    }
    protected void gvDefect_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvDefect.PageIndex * 20 + id;
                e.Row.Cells[0].Text = id.ToString();
            }

            DropDownList dType = (DropDownList)e.Row.Cells[2].FindControl("dType");

            dType.DataSource = oqc.GetDefectType(string.Empty,int.Parse(dropModule.SelectedValue));
            dType.DataBind();

            dType.Items.Insert(0, new ListItem("--","0"));

            dType.SelectedValue = gvDefect.DataKeys[e.Row.RowIndex].Values[1].ToString();

            dType.Enabled = false;
        }
    }
    protected void gvDefect_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int defID = int.Parse(gvDefect.DataKeys[e.RowIndex].Values[0].ToString());

        string strMsg = "";
        FuncErrType errtype = FuncErrType.操作成功;

        errtype = oqc.DeleteDefect(defID, ref strMsg);

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        GridViewBind();
    }
    protected void gvDefect_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        FuncErrType errtype = FuncErrType.操作成功;

        int ID = int.Parse(gvDefect.DataKeys[e.RowIndex].Values[0].ToString());
        string pID = dropModule.SelectedValue;
        string strName = ((TextBox)gvDefect.Rows[e.RowIndex].Cells[1].Controls[1]).Text.Trim();
        DropDownList dType = (DropDownList)gvDefect.Rows[e.RowIndex].Cells[2].FindControl("dType");
        TextBox txtOrder = (TextBox)gvDefect.Rows[e.RowIndex].Cells[4].FindControl("txtOrder");

        if (strName == string.Empty) 
        {
            ltlAlert.Text = "alert('名称不能为空');";
            return;
        }

        if (dType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类别');";
            return;
        }

        if (txtOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('排序不能为空!');";
            return;
        }
        else 
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtOrder.Text.Trim()))
            {
                ltlAlert.Text = "alert('排序格式不对!');";
                return;
            }
        }

        int nOrder = int.Parse(txtOrder.Text.Trim());

        errtype = oqc.ModifyDefect(ID, pID, int.Parse(dType.SelectedValue), strName, nOrder,int.Parse(Session["uID"].ToString()));

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        gvDefect.EditIndex = -1;

        GridViewBind();
    }
    protected void gvDefect_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvDefect.EditIndex = index;

        GridViewBind();

        DropDownList dType = (DropDownList)gvDefect.Rows[index].Cells[2].FindControl("dType");

        dType.Enabled = true;
    }
    protected void dropModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropModule.Focus();

        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gvDefect_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDefect.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(dropType.SelectedIndex != 0)
         GridViewBind();
    }
}
