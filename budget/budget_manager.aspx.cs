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
using BudgetProcess;

public partial class budget_manager : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    bool nRet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUser.Text = Budget.getBudgetUser(Convert.ToString(Request.QueryString["id"]), Convert.ToString(Request.QueryString["type"]));
            txtUserID.Text = "," + Budget.getBudgetUserID(Convert.ToString(Request.QueryString["id"]), Convert.ToString(Request.QueryString["type"])) + ",";
            txtUserID.Text = txtUserID.Text.Trim().Replace(",,", ",");
            ddlPlant.DataSource = Budget.getPlant();
            ddlPlant.DataBind();
            ddlPlant.SelectedValue = Convert.ToString(Session["PlantCode"]);
            loadDepartment();
        }
    }

    protected void loadDepartment()
    {
        if (ddlDept.Items.Count > 0) ddlDept.Items.Clear();

        string strsql = "Select 0 As departmentID, '--' As name Union Select departmentID, name From tcpc" + ddlPlant.SelectedValue + ".dbo.departments where issalary=1 order by name ";
        
        ddlDept.DataSource = Budget.getDept(strsql);
        ddlDept.DataBind();
        //ddlDept.SelectedIndex = 0;
    }

    protected void loadUser()
    {
        string struserID;
        DataSet dst;
        //ListItem item;
        if (chkUser.Items.Count > 0) chkUser.Items.Clear();

        struserID = "," + Budget.getBudgetUserID(Convert.ToString(Request.QueryString["id"]), Convert.ToString(Request.QueryString["type"])) + ",";
        struserID = struserID.Replace(",,", ",");

        if (ddlDept.SelectedIndex > 0)
        {
            dst = Budget.getUser(Convert.ToInt32(ddlPlant.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(Session["orgID"]));

            if (dst.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for (i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
                {
                    chkUser.Items.Add(new ListItem(dst.Tables[0].Rows[i].ItemArray[1].ToString().Trim(), dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim()));

                    if(Request.QueryString["type"] == "0")
                    {
                        if (struserID == "," + dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim() + ",")
                            chkUser.Items[i].Selected = true;
                    }
                    else
                    {
                        if (struserID.IndexOf("," + dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim() + ",", 0) >= 0)
                            chkUser.Items[i].Selected = true;
                    }                    
                }
            }
        }    
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        switch (Request.QueryString["type"])
        {
            case "0":
                if (chkUserSelected() == false)
                {
                    ltlAlert.Text = "alert('主管只能选择一位!');";
                    return;
                }
                chkUserSave(Convert.ToInt32(Request.QueryString["id"]),"0");
                break;

            case "1":
                chkUserSave(Convert.ToInt32(Request.QueryString["id"]), "1");
                break;

            case "2":
                chkUserSave(Convert.ToInt32(Request.QueryString["id"]), "2");
                break;

            default:
                break;
        }
        ltlAlert.Text = "alert('保存成功!'); window.close(); window.opener.location='/budget/budget_cc.aspx?rm=" + DateTime.Now + "';";
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

    protected bool chkUserSelected()
    {
        int cnt = 0;
        if (txtUserID.Text.Trim().Length > 0)
        {
            string struserid = txtUserID.Text.Trim().Substring(1);
            while (struserid.IndexOf(",") > 0)
            {
                struserid = struserid.Substring(struserid.IndexOf(",") + 1);
                cnt += 1;
            }
        }
        
        if (cnt > 1) return false;
        else return true;
    }

    protected void chkUserSave(int ccid, string strtype)
    {
        int i,pos;
        string struser = txtUser.Text.Trim();
        string struserid = txtUserID.Text.Trim();
        if (struserid != "")
        {
            struserid = struserid.Substring(1, struserid.Length - 2);
            struser = struser.Substring(1, struser.Length - 2);
        }
        Budget.updateBudgetUser(ccid, struserid, struser, strtype);
    }

    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;

        int i, pos;
        string struser = "";
        string struserid = "";
        for (i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkUser.Items[i].Selected == false)
            {
                txtUserID.Text = txtUserID.Text.Trim().Replace("," + chkUser.Items[i].Value + ",", ",");
                pos = chkUser.Items[i].Text.Trim().IndexOf("~");
                txtUser.Text = txtUser.Text.Replace("," + chkUser.Items[i].Text.Substring(pos + 1) + ",", ",");

                if (txtUserID.Text.Trim().Replace(",", "").Length <= 0) txtUserID.Text = "";

                if (txtUser.Text.Trim().Replace(",", "").Length <= 0) txtUser.Text = "";
            }
            else
            {
                pos = chkUser.Items[i].Text.Trim().IndexOf("~");
                if (txtUser.Text.IndexOf("," + chkUser.Items[i].Text.Substring(pos + 1) + ",") == -1)
                {
                    txtUser.Text = txtUser.Text + "," + chkUser.Items[i].Text.Substring(pos + 1) + ",";
                    txtUser.Text = txtUser.Text.Replace(",,", ",");
                    txtUserID.Text = txtUserID.Text + "," + chkUser.Items[i].Value + ",";
                    txtUserID.Text = txtUserID.Text.Replace(",,", ",");
                }
            } 

        }

    }
}