using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using RD_WorkFlow;

using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class RDW_RDW_AddDelay : System.Web.UI.Page
{
    private string ids
    {
        get
        {
            if (ViewState["ids"] == null)
            {
                ViewState["ids"] = ""
        ;
            }
            return ViewState["ids"].ToString();
        }
        set
        {
            ViewState["ids"] = value;
        }
    }
    private int aut
    {
        get
        {
            if (ViewState["aut"] == null)
            {
                ViewState["aut"] = 1;
            }
            return (int)ViewState["aut"];
        }
        set
        {
            ViewState["aut"] = value;
        }
    }
   
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            string strDID = Convert.ToString(Request.QueryString["did"]);
            string strMID = Convert.ToString(Request.QueryString["mid"]);
            string strUID = Convert.ToString(Session["uID"]);
            if (!(rdw.CheckFinishRDWDetail(strDID, strUID) || rdw.CheckEvaluateRDWDetail(strDID, strUID)))
            {
                btnAdd.Visible = false;
                btnSave.Visible = false;
                aut = 0;
            }
           

             Databind();
        }
    }
    public void Databind()
    {
       
        
        string strDID = Convert.ToString(Request.QueryString["did"]);
        gv.DataSource = rdw.SelectRDWDelay(strDID);
        gv.DataBind();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        back();
    }
    public void back()
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
      
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["did"]);
        string name = Convert.ToString(Request.QueryString["name"]);
        if (txbdate.Text.Trim() == "" || txbdate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Date cannot be empty！')";
        }
        else if (txbRemark.Text.Trim() == "" || txbRemark.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Reason cannot be empty！')";
        }
        else
        {

            int stust = rdw.InsertRDWDelay(strMID, name, txbdate.Text.Trim(), txbRemark.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            if(stust==-1)
                {
                    ltlAlert.Text = "alert('insert error！')";
                }
            else if (stust == 0)
            {
                ltlAlert.Text = "alert('Time must be more than the current time！')";
            }
            else
            {
                ltlAlert.Text = "alert('Insert success！')";
               //back();
                Databind();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //gv.DataSource = SelectPerformanceResult(txbType.Text);
        if (txbdate.Text.Trim() == "" || txbdate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Date cannot be empty！')";
        }
        else if (txbRemark.Text.Trim() == "" || txbRemark.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Reason cannot be empty！')";
        }
        else
        {
            int stutes = rdw.UpdateRDWDelay(ids, txbdate.Text, txbRemark.Text, Session["uID"].ToString(), Session["uName"].ToString());
            if (stutes==1)
            {
                ltlAlert.Text = "alert('update success！')";
                Databind();
                txbdate.Text = string.Empty;
                txbRemark.Text = string.Empty;
                btnSave.Visible = false;
                btnAdd.Visible = true;

            }
            else if (stutes == -1)
            {
                ltlAlert.Text = "alert('update error！')";
            }
            else
            {
                ltlAlert.Text = "alert('Time must be more than the current time！')";
            }
        }

      
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mydelete")
        {
            if (rdw.DeleteRDWDelay(e.CommandArgument.ToString()))
            {
                ltlAlert.Text = "alert('delete success！')";
                //string strDID = Convert.ToString(Request.QueryString["did"]);
                //string checkde = rdw.Checkdelay(strDID).Trim();
                //if (checkde == "1")
                //{
                //    btnAdd.Visible = true;
                //}
                //else
                //{
                //    btnAdd.Visible = false;
                //}
                Databind();
            }
            else
            {
                ltlAlert.Text = "alert('delete failed！')";
            }

        }
        if (e.CommandName == "myupdate")
        {
         
            ids = e.CommandArgument.ToString();
            SqlDataReader reader = rdw.SelectRDWDelay(Request.QueryString["did"], ids);
            btnSave.Visible = true;
            btnAdd.Visible = false;
            while (reader.Read())
            {
               
                txbdate.Text = string.Format("{0:yyyy-MM-dd}", reader["RDW_delaytime"]);
                txbRemark.Text = reader["RDW_delayrmk"].ToString();


            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Databind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (aut == 0)
            {
               // e.Row.Cells[3].Enabled = false;
                //e.Row.Cells[4].Enabled = false;
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = ""; 
            }
        }
    }
}