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
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using WO2Group;

public partial class wo2_groupdetail : BasePage
{
    adamClass chk = new adamClass();
    WO2 wo2 = new WO2();
    string[] strUser;
    string[] strMOP;
    string[] strSOP;

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            LoadGroup();
            LoadMOP();
            LoadSOP();
            LoadDepartment();
            LoadWorkShop();
            LoadWorkType();
            LoadUser();
        }
    }

    protected void LoadGroup()
    {
        if (ddlGroup.Items.Count > 0) ddlGroup.Items.Clear();

        ddlGroup.DataSource = wo2.SelectGroupInfo();
        ddlGroup.DataBind();
        
    }

    protected void LoadMOP()
    {
        if (ddlMOP.Items.Count > 0) ddlMOP.Items.Clear();

        ddlMOP.DataSource = wo2.SelectMOPInfo();
        ddlMOP.DataBind();
    }

    protected void LoadSOP()
    {
        if (ddlSOP.Items.Count > 0) ddlSOP.Items.Clear();

        //定义参数
        int intMopProc = 0;
        if(ddlMOP.SelectedIndex != 0)
        {
            strMOP = ddlMOP.SelectedItem.Text.Split(',');
            intMopProc= Convert.ToInt32(strMOP[0]);
        }

        ddlSOP.DataSource = wo2.SelectSOPInfo(intMopProc);
        ddlSOP.DataBind();
    }

    protected void LoadDepartment()
    {
        if (ddlDept.Items.Count > 0) ddlDept.Items.Clear();

        ddlDept.DataSource = wo2.SelectDepartmentInfo();
        ddlDept.DataBind();
    }

    protected void LoadWorkShop()
    {
        ddlWorkShop.Items.Clear();

        //定义参数
        int intDeptID = 0;
        if(ddlDept.SelectedIndex != 0)
        {
            intDeptID = Convert.ToInt32(ddlDept.SelectedValue.ToString());
        }

        ddlWorkShop.DataSource = wo2.SelectWorkShopInfo(intDeptID);
        ddlWorkShop.DataBind();
    }

    protected void LoadWorkType()
    {
        ddlWorkType.Items.Clear();

        //定义参数
        int intWSID = 0;
        if(ddlWorkShop.SelectedIndex != 0)
        {
            intWSID = Convert.ToInt32(ddlWorkShop.SelectedValue.ToString());
        }

        ddlWorkType.DataSource = wo2.SelectWorkTypeInfo(intWSID);
        ddlWorkType.DataBind();
    }

    protected void LoadUser()
    {
        chkUser.Items.Clear();

        //定义参数
        int intDeptID = Convert.ToInt32(ddlDept.SelectedValue.ToString());
        int intWSID = Convert.ToInt32(ddlWorkShop.SelectedValue.ToString());
        int intWTID = Convert.ToInt32(ddlWorkType.SelectedValue.ToString());
        int intGroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());
        int intPlantID = Convert.ToInt32(Session["PlantCode"]);

        DataTable dtb = wo2.SelectUserInfo(intDeptID, intWSID, intWTID, intPlantID);
        int i = 0;

        for (i = 0; i < dtb.Rows.Count; i++)
        {
            ListItem item = new ListItem(dtb.Rows[i].ItemArray[1].ToString().Trim(), dtb.Rows[i].ItemArray[0].ToString().Trim());
            chkUser.Items.Add(item);

            if (wo2.IsUserChecked(intGroupID, Convert.ToInt32(dtb.Rows[i].ItemArray[0].ToString().Trim()),Convert.ToInt32(ddlMOP.SelectedValue),Convert.ToInt32(ddlSOP.SelectedValue ) ))
            {
                chkUser.Items[i].Selected = true;
            }
            else
            {
                chkUser.Items[i].Selected = false;
            }
        }
    }

    protected void BindData()
    {
        //定义参数
        //int intGroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());

        //SqlDataReader reader = wo2.SelectGroupDetailInfo(intGroupID);

        //while (reader.Read())
        //{
        //    ddlMOP.SelectedValue = reader["MopID"].ToString().Trim();
        //    LoadSOP();
        //    ddlSOP.SelectedValue = reader["SopID"].ToString().Trim();
        //}   

        //reader.Close();
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMOP();                                                                              
        LoadUser();
        BindData();
    }

    protected void ddlMOP_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSOP();
        LoadUser();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkShop();
        LoadWorkType();
        LoadUser();
    }

    protected void ddlWorkShop_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkType();
        LoadUser();
    }

    protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        if (rblCheck.SelectedIndex == 0)
        {
            for (i = 0; i < chkUser.Items.Count; i++)
            {
                chkUser.Items[i].Selected = true;
            }
        }

        if (rblCheck.SelectedIndex == 1)
        {
            for (i = 0; i < chkUser.Items.Count; i++)
            {
                chkUser.Items[i].Selected = false;
            }
        }

        rblCheck.ClearSelection();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        bool haveError = false;

        //定义参数
        int intGroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());
        int intUserID = 0;
        string strUserNo = string.Empty;
        string strUserName = string.Empty;
        int intMOPID = Convert.ToInt32(ddlMOP.SelectedValue.ToString());
        string strMOPProc = string.Empty;
        string strMOPName = string.Empty;
        int intSOPID = Convert.ToInt32(ddlSOP.SelectedValue.ToString());
        string strSOPProc = string.Empty;
        string strSOPName = string.Empty;
        decimal SopRate = 0.0M;
        bool isChecked = false;
        int intUID = Convert.ToInt32(Session["uID"]);

        int i = 0;

        if (intGroupID == 0)
        {
            ltlAlert.Text = "alert('请选择用户组！');";
            return;
        }
        if (intMOPID == 0)
        {
            ltlAlert.Text = "alert('请选择工序！');";
            return;
        }
        if (intSOPID == 0)
        {
            ltlAlert.Text = "alert('请选择岗位！');";
            return;
        }
        strMOP = ddlMOP.SelectedItem.Text.Split(',');
        strMOPProc = strMOP[0];
        strMOPName = strMOP[1];

        strSOP = ddlSOP.SelectedItem.Text.Split(',');
        strSOPProc = strSOP[0];
        strSOPName = strSOP[1];
        SopRate = Convert.ToDecimal(strSOP[2]);

        for (i = 0; i < chkUser.Items.Count; i++)
        {
            strUser = chkUser.Items[i].Text.Split('~');
            intUserID = Convert.ToInt32(chkUser.Items[i].Value.ToString());
            strUserNo = strUser[1];
            strUserName = strUser[0];

            isChecked = chkUser.Items[i].Selected;

            wo2.UpdateGroupDetail(intGroupID, intUserID, strUserNo, strUserName, intMOPID, strMOPProc, strMOPName, intSOPID, strSOPProc, strSOPName, SopRate, isChecked, intUID);
        }

        ltlAlert.Text = "alert('保存/更新成功！');";
    }

    protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }
    protected void ddlSOP_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }
}
