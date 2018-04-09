using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MF;
using System.Data.SqlClient;

public partial class IT_MF_detReview : System.Web.UI.Page
{
    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }
    public string _ID
    {
        get
        {
            return ViewState["ID"].ToString();
        }
        set
        {
            ViewState["ID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _parentID = Request.QueryString["id"];

            _ID = Request.QueryString["detid"];
           
                if (Request.QueryString["sec"].Trim() == "false")
                {
                    btnsave.Visible = false;
                }
            
            Databind();

            SqlDataReader read = MFHelper.selectMFdetone(_ID);
            while (read.Read())
            {
                txtTitle.Text = read["FM_title"].ToString();
                txtresponsible.Text = read["FM_depart"].ToString();
                txtDecription.Text = read["FM_desc"].ToString();
                lblstep.Text = read["FM_Step"].ToString();
                btnsave.Text = "Save";
            }
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Databind();
    }

    public void Databind()
    {
        
        gv.DataSource = MFHelper.selectMFdetReview(_parentID, _ID);
        gv.DataBind();

      
      

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       //// txtDealID.Text = "1";
       // if (e.CommandName == "Review")
       // {
       //     int index = ((GridViewRow)(((RadioButton)e.CommandSource).Parent.Parent)).RowIndex;
       //     int fsId = Convert.ToInt32(gv.DataKeys[index].Values["FM_id"].ToString());
       //    // ltlAlert.Text = "window.showModalDialog('MF_detReview.aspx?id=" + _parentID + "&detid=" + fsId.ToString() + " &rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
       //     //ltlAlert.Text += "window.location.href = 'MF_det.aspx?id=" + _parentID + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";
       //     // Response.Redirect("MF_detReview.aspx?id=" + _parentID + "&detid=" + fsId.ToString() + " &rt=" + DateTime.Now.ToFileTime().ToString());
       // }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            RadioButton rb = (RadioButton)e.Row.FindControl("raButton");

            rb.Attributes.Add("onclick", "judge(this)");//给RadioButton添加onclick属性

        }




    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
       
        foreach (GridViewRow dr in gv.Rows) 
        {
            if (((RadioButton)dr.FindControl("raButton")).Checked) 
            {
                MFHelper.saveMFdet(((Label)dr.FindControl("lblid")).Text, Session["uID"].ToString(), Session["uName"].ToString());
                
            }
           
        }
        ltlAlert.Text = "window.close();";
    }
}