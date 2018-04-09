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
using RD_WorkFlow;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class RDW_AddTemplate : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                lbID.Text = Request.QueryString["id"].ToString();

                RDW_Header header = rdw.SelectTepmlateMstr(lbID.Text);
                txtProject.Text = header.RDW_Project;
                txtProdDesc.Text = header.RDW_Memo;

                if (Request.QueryString["cp"] == "y")
                {
                    btnSave.Text = "Copy";
                    copyflag.Visible = true;
                    txtTempCopyfrom.Text = header.RDW_Project;
                    txtProject.Text = string.Empty;
                    txtProdDesc.Text = string.Empty;

                }
                else
                {
                    if (header.RDW_CreatedBy == Convert.ToInt32(Session["uID"]) || Convert.ToInt32(Session["uRole"]) == 1)
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                }
            }
            else
            {
                lbID.Text = "0";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProject.Text == string.Empty)
        {
            ltlAlert.Text = "alert('Project name is required.'); ";
            return;
        }


        //定义参数
        RDW_Header rh = new RDW_Header();

        rh.RDW_Project = txtProject.Text.Trim();
        //rh.RDW_ProdCode = dropSKU.SelectedValue;
        rh.RDW_ProdCode = "";
        rh.RDW_Memo = txtProdDesc.Text.Trim();
        rh.RDW_CreatedBy = Convert.ToInt32(Session["uID"]);

        if (Request.QueryString["cp"] == "y")
        {
            //try
            //{
            int intNewID = 0;
            string strName = "sp_RDW_CopyTemplate";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@proj", txtProject.Text);
            parm[1] = new SqlParameter("@memo", txtProdDesc.Text);
            parm[2] = new SqlParameter("@id", Convert.ToInt32(lbID.Text));
            parm[3] = new SqlParameter("@uid", Convert.ToInt32(Session["uID"]));

            intNewID = Convert.ToInt32(SqlHelper.ExecuteScalar(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm));
            if (intNewID > 0)
            {
                ltlAlert.Text = "window.location.href='/RDW/RDW_Template.aspx?id=" + intNewID.ToString() + "&rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('Failed to copy template. Unique project name is required.'); ";
            }
        }
        else
        {
            if (Convert.ToInt32(lbID.Text) > 0)
            {
                rh.RDW_MstrID = Convert.ToInt32(lbID.Text);
                rdw.UpdateTemplateMstr(rh);

                ltlAlert.Text = "window.location.href='/RDW/RDW_Template.aspx?rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                int intNewID = Convert.ToInt32(rdw.InsertTemplateMstr(rh));

                if (intNewID > 0)
                {
                    ltlAlert.Text = "window.location.href='/RDW/RDW_Template.aspx?id=" + intNewID.ToString() + "&rm=" + DateTime.Now.ToString() + "';";
                }
                else
                {
                    ltlAlert.Text = "alert('Failed to create new template.'); ";
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_Template.aspx?rm=" + DateTime.Now.ToString(), true);
    }
    
}
