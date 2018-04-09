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
using BudgetProcess;

public partial class bg_User : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        { 
            string strType = Convert.ToString(Request.QueryString["type"]);

            if (strType == null)
            {
                ltlAlert.Text = " window.close();";
            }

            ddlPlant.DataSource = Budget.getPlant();
            ddlPlant.DataBind();
            ddlPlant.SelectedValue = Convert.ToString(Session["PlantCode"]);

            loadDepartment();
        }
    }

    protected void loadDepartment()
    {
        if (ddlDept.Items.Count > 0) ddlDept.Items.Clear();

        string strsql = " Select 0 As departmentID, '--' As name Union Select departmentID, name From tcpc" + ddlPlant.SelectedValue;
        strsql += ".dbo.departments Where issalary=1 Order By name ";

        ddlDept.DataSource = Budget.getDept(strsql);
        ddlDept.DataBind();
        //ddlDept.SelectedIndex = 0;
    }

    protected void loadUser()
    {
        string struserID;
        DataSet dst;
        
        if (chkUser.Items.Count > 0) chkUser.Items.Clear();

        if (ddlDept.SelectedIndex > 0)
        {
            dst = Budget.getUserEmail(Convert.ToInt32(ddlPlant.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(Session["orgID"]));

            if (dst.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
                {
                    chkUser.Items.Add(new ListItem(dst.Tables[0].Rows[i].ItemArray[1].ToString().Trim(), dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim()));
                }
            }
        }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepartment();
        loadUser();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string strType = Convert.ToString(Request.QueryString["type"]);

        if (chkUserSelected() == false)
        {
            if (strType == "0")
                ltlAlert.Text = "alert('选择的提交人只能有一位!');";
            else
                ltlAlert.Text = "alert('选择的提交人至少有一位!');";
            return;
        }

        txtUserID.Text = txtUserID.Text.Substring(1, txtUserID.Text.Trim().Length - 2);
        txtUserEmail.Text = txtUserEmail.Text.Trim().Length == 0? "" : txtUserEmail.Text.Substring(1, txtUserEmail.Text.Trim().Length - 2);
        txtUser.Text = txtUser.Text.Substring(1, txtUser.Text.Trim().Length - 2);

        if (strType == "0")
        {
            ltlAlert.Text = " window.opener.document.getElementById('txtReceiptID').value=document.getElementById('txtUserID').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtReceiptEmail').value=document.getElementById('txtUserEmail').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtReceipt').value=document.getElementById('txtUser').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtReceiptValue').value=document.getElementById('txtUser').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtCopyTo').value=window.opener.document.getElementById('txtCopyToValue').value;";
            ltlAlert.Text += " window.close();";
        }

        if (strType == "1")
        {
            ltlAlert.Text = " window.opener.document.getElementById('txtCopyToID').value=document.getElementById('txtUserID').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtCopyToEmail').value=document.getElementById('txtUserEmail').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtCopyTo').value=document.getElementById('txtUser').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtCopyToValue').value=document.getElementById('txtUser').value;";
            ltlAlert.Text += " window.opener.document.getElementById('txtReceipt').value=window.opener.document.getElementById('txtReceiptValue').value;";
            ltlAlert.Text += " window.close();";
        }
    }

    protected bool chkUserSelected()
    {
        int cnt = 0;
        bool isOK = false;
        string strType = Convert.ToString(Request.QueryString["type"]);

        if (txtUserID.Text.Trim().Length > 0)
        {
            string struserid = txtUserID.Text.Trim().Substring(1);
            while (struserid.IndexOf(",") > 0)
            {
                struserid = struserid.Substring(struserid.IndexOf(",") + 1);
                cnt += 1;
            }
        }

        if (strType == "0")
        {
            if (cnt != 1) isOK = false;
            else isOK =  true;
        }
        if (strType == "1")
        {
            if (cnt < 1) isOK = false;
            else isOK = true;
        }

        return isOK;
    }

    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;

        int i, pos;
        string struser = "";
        string struserid = "";
        string struseremail = "";
        for (i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkUser.Items[i].Selected == false)
            {
                txtUserID.Text = txtUserID.Text.Trim().Replace("," + chkUser.Items[i].Value + ",", ",");
                pos = chkUser.Items[i].Text.Trim().IndexOf("~");
                txtUser.Text = txtUser.Text.Replace("," + chkUser.Items[i].Text.Substring(0, pos) + ",", ",");
                pos = chkUser.Items[i].Text.Trim().IndexOf("^");
                txtUserEmail.Text = txtUserEmail.Text.Replace("," + chkUser.Items[i].Text.Substring(pos + 1) + ",", ",");

                if (txtUserID.Text.Trim().Replace(",", "").Length <= 0) txtUserID.Text = "";

                if (txtUser.Text.Trim().Replace(",", "").Length <= 0) txtUser.Text = "";

                if (txtUserEmail.Text.Trim().Replace(",", "").Length <= 0) txtUserEmail.Text = "";
            }
            else
            {
                pos = chkUser.Items[i].Text.Trim().IndexOf("~");
                if (txtUser.Text.IndexOf("," + chkUser.Items[i].Text.Substring(0, pos) + ",") == -1)
                {
                    txtUser.Text = txtUser.Text + "," + chkUser.Items[i].Text.Substring(0, pos) + ",";
                    txtUser.Text = txtUser.Text.Replace(",,", ",");
                    txtUserID.Text = txtUserID.Text + "," + chkUser.Items[i].Value + ",";
                    txtUserID.Text = txtUserID.Text.Replace(",,", ",");
                    pos = chkUser.Items[i].Text.Trim().IndexOf("^");
                    txtUserEmail.Text = txtUserEmail.Text + "," + chkUser.Items[i].Text.Substring(pos + 1) + ",";
                    txtUserEmail.Text = txtUserEmail.Text.Replace(",,", ",");
                }
            }
        }
    }
}
