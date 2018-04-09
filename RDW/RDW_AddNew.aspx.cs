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

public partial class RDW_RDW_AddNew : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_type.DataSource = rdw.SelectProjType();
            ddl_type.DataBind();

            oldprodname.Visible = false;
            oldprodcode.Visible = false;
            ecnCode.Visible = false;
            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));

            SKUHelper skuHelper = new SKUHelper();

            dropSKU.DataSource = skuHelper.Items(string.Empty);
            dropSKU.DataBind();
            dropSKU.Items.Insert(0, new ListItem("--", "--"));
            
            dropTemplate.DataSource = rdw.GetTemplateByType(0, Convert.ToInt32(dropCatetory.SelectedValue));
            dropTemplate.DataBind();

            //创建人必须要有英文名。默认情况下，非US、EU域的人员的英文名如果为空的话，则直接去中文名；故这里只要中、英文名相等，则表示没有英文名
            if (Convert.ToInt32(Session["PlantCode"]) < 90 && Session["uName"].ToString().ToLower() == Session["eName"].ToString().ToLower())
            {
                btnSave.Enabled = false;
                ltlAlert.Text = "alert('你还没有英文名。请通知人事添加！');";
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(ddl_type.SelectedValue.ToString() == "ECN")
        {
            string oldprodCode = txt_oldProdCode.Text.Trim();
            string oldprodName = txt_oldProdName.Text.Trim();
            if (rdw.IsProject(oldprodName, oldprodCode) == false)
            {
                ltlAlert.Text = "alert('This Project does not exist!'); ";
                return;
            }
            if (txt_ecnCode.Text.Trim().Length > 0)
            {
                if (rdw.IsEnabledEcnCode(txt_ecnCode.Text.Trim()) == "2")
                {
                    ltlAlert.Text = "alert('ECN Code does not exist or the ECN is not completed!'); ";
                    return;
                }
                else if (rdw.IsEnabledEcnCode(txt_ecnCode.Text.Trim()) == "3")
                {
                    ltlAlert.Text = "alert('Thie ECN Code has been used!'); ";
                    return;
                }
            }

            
        }

        if (txtPPA.Text == string.Empty)
        {
            ltlAlert.Text = "alert('The PPA could not be empty!'); ";
            return;
        }
        else
        {
            int ppaflag =  rdw.CheckExistsPPA(txtPPA.Text);
            if (ppaflag == 0)
            {
                ltlAlert.Text = "alert('PPA is invalid!'); ";
                return;
            }
            else if (ppaflag == 2)
            {
                ltlAlert.Text = "alert('PPA did not pass approval!'); ";
                return;
            }
        }

        if (dropCatetory.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('The Project Category could not be empty!'); ";
            return;
        }
        if (txtProject.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Project Name could not be empty!'); ";
            return;
        }

        if (txtStartDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Start Date could not be empty!'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('The format of Start Date must be a DateTime value!'); ";
                return;
            }
        }
        if (txtEndDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Project EndDate could not be empty!'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());

                DateTime _startdate = Convert.ToDateTime(txtStartDate.Text.Trim());
                if (_startdate > _dt)
                {
                    ltlAlert.Text = "alert('Start Date should not be greater than End Date!'); ";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('The format of End Date must be a DateTime value!'); ";
                return;
            }
        }
        

        if (txtProdCode.Text.Trim() != string.Empty)
        {
            if (rdw.CheckIsHavethisProdCode(txtProdCode.Text.Trim().ToString()))
            {
                if (dropCatetory.SelectedIndex > 0)
                {
                    ltlAlert.Text = "alert('Please contact System administrator to maintain the Project category serial number,because this Project code already exists!'); ";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('Please rename project code,because this Project code already exists!'); ";
                    return;
                }
            }
        }
        //marked by shanzm 2015-05-18：立项之前，尚无UL号
        //if (txtModel.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The Model NO. could not be empty!'); ";
        //    return;
        //}
        //if (txtLEDJXL.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The LEDJXL could not be empty!'); ";
        //    return;
        //}
        //if (txtDriverJXL.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The DriverJXL could not be empty!'); ";
        //    return;
        //}
        //定义参数
        RDW_Header rh = new RDW_Header();

        rh.RDW_Category = dropCatetory.SelectedValue;
        rh.RDW_Project = txtProject.Text.Trim();
        rh.RDW_ProdCode = txtProdCode.Text.Trim();
        rh.RDW_ProdSKU = dropSKU.SelectedValue;
        rh.RDW_ProdDesc = txtProdDesc.Text.Trim();
        rh.RDW_StartDate = txtStartDate.Text.Trim();
        rh.RDW_EndDate = txtEndDate.Text.Trim();
        rh.RDW_Standard = txtStandard.Text.Trim();
        rh.RDW_Memo = txtMemo.Text.Trim();
        rh.RDW_CreatedBy = Convert.ToInt32(Session["uID"]);
        rh.RDW_Template = Convert.ToInt32(dropTemplate.SelectedValue);
        rh.RDW_Type = ddl_type.SelectedValue;
        rh.RDW_Customer = txt_customer.Text.Trim();
        rh.RDW_LampType = txt_lampType.Text.Trim();
        rh.RDW_Priority = txt_priority.Text.Trim();
        rh.RDW_EngineerTeam = ddl_ET.SelectedValue;
        rh.RDW_Tier = ddl_tier.SelectedValue;
        rh.RDW_EStarDLC = ddl_EStarDLC.SelectedValue;
        if (lbl_oldmID.Text.Trim().Length > 0)
        {
            rh.RDW_OldID = Convert.ToInt32(lbl_oldmID.Text);
            Session["oldmID"] = lbl_oldmID.Text;
        }            
        else
        {
            rh.RDW_OldID = 0;
            Session["oldmID"] = "0";
        }
            rh.RDW_OldID = 0;
        rh.RDW_EcnCode = txt_ecnCode.Text.Trim();
        rh.RDW_PPA = txtPPA.Text.Trim();

        if (!rdw.insertUL(txtProject.Text.Trim(), txtModel.Text.Trim(), txtLEDJXL.Text.Trim(), txtLEDLV.Text.Trim(), txtDriverJXL.Text.Trim(), txtDriverLv.Text.Trim(), Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        {
            ltlAlert.Text = "alert('Add data error or model name already exists!'); ";
            return;
        }

        int intNewID = Convert.ToInt32(rdw.InsertRDWHeader(rh));
        int tempID = Convert.ToInt32(rh.RDW_Template);


        if (intNewID > 0)
        {
            //rdw.InitDet_Ptr_Mbr(intNewID,tempID);
            ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + intNewID.ToString() + "&ecnCode=" + txt_ecnCode.Text.Trim() + "&oldmID=" + lbl_oldmID.Text.Trim() + "&rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('Add data error or project name already exists!'); ";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_HeaderList.aspx?rm=" + DateTime.Now.ToString(), true);
    }
    
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddl_type.SelectedValue.ToString() != "1")
        {
            txt_oldProdName.Text = "";
            txt_oldProdCode.Text = "";
            oldprodname.Visible = false;            
            oldprodcode.Visible = false;
            ecnCode.Visible = false;            
        }
        else
        {
            oldprodname.Visible = true;
            oldprodcode.Visible = true;
            ecnCode.Visible = true;
        }

        if (ddl_type.SelectedValue.ToString() == "2")
        {
            txtProdCode.Enabled = true;
        }
        else
        {
            txtProdCode.Enabled = false;
        }
        dropTemplate.DataSource = rdw.GetTemplateByType(Convert.ToInt32(ddl_type.SelectedValue), Convert.ToInt32(dropCatetory.SelectedValue));
        dropTemplate.DataBind();
    }
    protected void txt_oldProdCode_TextChanged(object sender, EventArgs e)
    {
        string oldprodCode = txt_oldProdCode.Text.Trim();
        string oldprodName = txt_oldProdName.Text.Trim();
        string oldprodCode1 = oldprodCode;
        string oldprodName1 = oldprodName;
        if( txt_oldProdCode.Text.Trim().IndexOf("-") >=0 )
        {
            oldprodCode1 = txt_oldProdCode.Text.Trim().Substring(0, oldprodCode.IndexOf("-"));
            oldprodName1 = txt_oldProdName.Text.Trim().Substring(0, oldprodName.IndexOf("-"));
        }
        
        
        if (txt_oldProdName.Text.Trim() != "" && txt_oldProdCode.Text.Trim() != "")
        {
            if(rdw.IsProject( oldprodName1, oldprodCode1 ) == true )
            {
                txtProdCode.Text = rdw.GetNewProdEcnCode(oldprodName1, oldprodCode1);
                lbl_oldmID.Text = rdw.GetOldProdID(oldprodName1, oldprodCode1);
                dropCatetory.SelectedValue = rdw.GetOldCategory(oldprodName, oldprodCode);
            }
            else
            {
                ltlAlert.Text = "alert('This Project does not exist!'); ";
                return;
            }
            
        }
                   
    }
    protected void txt_ecnCode_TextChanged(object sender, EventArgs e)
    {
        if( rdw.IsEnabledEcnCode(txt_ecnCode.Text.Trim()) == "2" )
        {
            ltlAlert.Text = "alert('ECN Code does not exist or the ECN is not completed!'); ";
            return;
        }
        else if (rdw.IsEnabledEcnCode(txt_ecnCode.Text.Trim()) == "3")
        {
            ltlAlert.Text = "alert('Thie ECN Code has been used!'); ";
            return;
        }
        
    }
    protected void dropCatetory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCatetory.SelectedIndex > 0 && ddl_type.SelectedValue == "0")
        {
            int cateid = Convert.ToInt32(dropCatetory.SelectedValue);
            int RDW_CreatedBy = Convert.ToInt32(Session["uID"]);
            string newProjectName = rdw.getProjectCodebyCateCode(cateid, RDW_CreatedBy);
            txtProdCode.Text = newProjectName;
            txtProdCode.ReadOnly = true;
            dropTemplate.DataSource = rdw.GetTemplateByType(Convert.ToInt32(ddl_type.SelectedValue), Convert.ToInt32(dropCatetory.SelectedValue));
            dropTemplate.DataBind();
        }
        else
        {
            txtProdCode.ReadOnly = false;
            dropTemplate.DataSource = rdw.GetTemplateByType(Convert.ToInt32(ddl_type.SelectedValue), Convert.ToInt32(dropCatetory.SelectedValue));
            dropTemplate.DataBind();
        }
    }
}
