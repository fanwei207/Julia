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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using PIInfo;

public partial class IT_Pi_ShowPrice : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    PI pi = new PI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        gv.DataSource = pi.showPriceList(txtcust.Text.Trim(),txtQAD.Text.Trim(),txtshipto.Text.Trim(),txtCrtDate1.Text.Trim(),txtCrtDate2.Text.Trim());
        gv.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }


    ///// <summary>
    ///// 查询价格
    ///// </summary>
    ///// <param name="Pi_Cust"></param>
    ///// <param name="Pi_QAD"></param>
    ///// <param name="Pi_ShipTo"></param>
    ///// <returns></returns>
    //public DataTable showPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo)
    //{
    //    try
    //    {
    //        string strSQL = "sp_Pi_select_PriceList";
    //        SqlParameter[] parm = new SqlParameter[12];
    //        parm[0] = new SqlParameter("@cust", Pi_Cust);
    //        parm[1] = new SqlParameter("@QAD", Pi_QAD);
    //        parm[2] = new SqlParameter("@shipto", Pi_ShipTo);
    //        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }

    //}

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindGridView();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
         BindGridView();
         String Currency = ((Label)gv.Rows[e.NewEditIndex].Cells[11].FindControl("lblHis")).Text.Trim();
         ((DropDownList)gv.Rows[e.NewEditIndex].Cells[11].FindControl("ddlHis")).Items.FindByText(Currency).Selected = true;


    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String id = gv.DataKeys[e.RowIndex].Values[0].ToString();
        String strCurrency = gv.Rows[e.RowIndex].Cells[3].Text.Trim();
        String strUM = gv.Rows[e.RowIndex].Cells[4].Text.Trim();
        String strStartDate = gv.Rows[e.RowIndex].Cells[6].Text.ToString().Trim();
        String strEndDate = gv.Rows[e.RowIndex].Cells[7].Text.ToString().Trim();
        String txtprice1 = gv.Rows[e.RowIndex].Cells[8].Text.ToString().Trim();
        String txtprice2 = gv.Rows[e.RowIndex].Cells[9].Text.ToString().Trim();
        String txtprice3 = gv.Rows[e.RowIndex].Cells[10].Text.ToString().Trim();
        String txtRemark = gv.Rows[e.RowIndex].Cells[17].Text.ToString().Trim();
        String txtCust = gv.Rows[e.RowIndex].Cells[0].Text.Trim();
        String txtQad = gv.Rows[e.RowIndex].Cells[1].Text.Trim();
        String txtcust2 = gv.Rows[e.RowIndex].Cells[2].Text.Trim();
        String txtHis = ((DropDownList)gv.Rows[e.RowIndex].Cells[11].FindControl("ddlHis")).SelectedValue.ToString().Trim();
           
       
        //IErr = sid.UpdateShipDetail(Convert.ToString(Session["uID"]), strDID, strSNO, strBox, strQa, strWo, strweight, strvolume, strprice, strprice1, strmemo, strQtyset, strQtypcs, strPkgs, strPtype, strTcpPo);
        if (pi.UpdatePriceList(id, strCurrency, strUM, strStartDate, strEndDate, txtprice1, txtprice2, txtprice3, txtRemark, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"]), txtHis))
        {
            gv.EditIndex = -1;
            BindGridView();
        }
      
        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
        
    }



    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (pi.DeletePriceList(gv.DataKeys[e.RowIndex].Values[0].ToString(), Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        {
            BindGridView();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
       
    }
}