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

public partial class QC_qc_defect_type : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    private void GridViewBind() 
    {
        string strName = txtType.Text.Trim();
        int module = int.Parse(dropModule.SelectedValue);

        ogv.GridViewDataBind(gvDefect, oqc.GetDefectType(strName, module));
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
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvDefect.PageIndex * 20 + id;
                e.Row.Cells[0].Text = id.ToString();
            }

            DropDownList dropModule = (DropDownList)e.Row.Cells[2].FindControl("dropModule");
            dropModule.SelectedValue = gvDefect.DataKeys[e.Row.RowIndex].Values[1].ToString();
        }
    }
    protected void gvDefect_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = int.Parse(gvDefect.DataKeys[e.RowIndex].Values[0].ToString());

        FuncErrType error = oqc.DeleteDefectType(ID);

        GridViewBind();
    }
    protected void gvDefect_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvDefect.EditIndex = index;

        GridViewBind();

        DropDownList dropModule = (DropDownList)gvDefect.Rows[index].Cells[2].FindControl("dropModule");
        dropModule.SelectedValue = gvDefect.DataKeys[index].Values[1].ToString();
    }
    protected void gvDefect_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        bool bFlag = true;

        int uID = int.Parse(Session["uID"].ToString());
        int ID = int.Parse(gvDefect.DataKeys[e.RowIndex].Values[0].ToString());
        string strName = ((TextBox)gvDefect.Rows[e.RowIndex].Cells[1].Controls[1]).Text.Trim();
        DropDownList dropModule = (DropDownList)gvDefect.Rows[e.RowIndex].Cells[2].FindControl("dropModule");
        TextBox txtOrder = (TextBox)gvDefect.Rows[e.RowIndex].Cells[3].FindControl("txtOrder");

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

            FuncErrType errortype = FuncErrType.操作成功;

            errortype = oqc.ModifyDefectType(ID, dropModule.SelectedValue, strName, nOrder, uID);

            if (errortype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errortype.ToString() + "');";
            }
            
            gvDefect.EditIndex = -1;
        }

        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {        
        bool bFlag = true;

        if (txtType.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('名称不能为空!');";
            bFlag = false;
        }

        if (dropModule.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('请选择所属模块!');";
            bFlag = false;
        }

        string strName = txtType.Text.Trim();
        int pID = int.Parse(dropModule.SelectedValue);
        int uID = int.Parse(Session["uID"].ToString());

        FuncErrType error = FuncErrType.操作成功;

        if (bFlag)
        {
            error = oqc.AddDefectType(strName, pID, uID);
            if (error != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + error.ToString() + "!');";
            }
        }

        gvDefect.PageIndex = 0;

        txtType.Text = string.Empty;
        dropModule.SelectedIndex = 0;

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
}
