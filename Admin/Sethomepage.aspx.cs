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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
 

public partial class admin_homepage1 : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //权限公开：nRet = chk.securityCheck(Session("uID"), 1, Session("orgID"), 19010200)
            
            dll_MenuLoad();
            BindAllMenu();
            BindUserHomepageMenu();

        } 
    } 
    private void dll_MenuLoad()
    {

        dll_Menu.DataSource = admin_AccessApply.getRootMenuInfo();
        dll_Menu.DataBind();
        ListItem item = new ListItem("--","0");
        dll_Menu.Items.Insert(0,item);
        dll_Menu.SelectedIndex = 0;
     
        
    }

    private void BindUserHomepageMenu()
    {
        CheckBoxList_homepages.Items.Clear();
        String strSQL = @"select ahp_id,ahp_moduleID, M.name ,M.description 
                          from  adm_homepages
                         Inner Join tcpc0.dbo.Menu M on M.id = ahp_moduleID and ahp_userid = " + Convert.ToInt32(Session["uID"]);
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            ListItem item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Text = dt.Rows[i]["name"].ToString() + ":&nbsp;&nbsp;&nbsp;&nbsp;<font color=purple>" + dt.Rows[i]["description"].ToString() + "</font>";
                item.Value = dt.Rows[i]["ahp_moduleID"].ToString();
                CheckBoxList_homepages.Items.Add(item);
            }
        }
       
    }

    private void BindAllMenu()
   {
       chkBL_Menu.Items.Clear();

        int iRole = Convert.ToInt32(Session["uRole"]);
        int iUserId =  Convert.ToInt32(Session["uID"]);
        int iModuleId =  Convert.ToInt32(dll_Menu.SelectedValue);

        String strSQL = "sp_admin_selectMenuSetHomepage";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@uRoleID", iRole);
        param[1] = new SqlParameter("@userID", iUserId);
        param[2] = new SqlParameter("@ParentMenuId", iModuleId);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, param).Tables[0];
        if(dt.Rows.Count > 0) 
        {
            ListItem item;
            for( int i= 0;i< dt.Rows.Count; i++)
            {
                item = new ListItem(dt.Rows[i]["name"].ToString() + ":&nbsp;&nbsp;&nbsp;&nbsp;<font color=purple>" + dt.Rows[i]["description"].ToString()+ "</font>");
                item.Value = dt.Rows[i]["id"].ToString();
                chkBL_Menu.Items.Add(item);
            
            }
        } 
        dt.Reset();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (CheckBoxList_homepages.Items.Count >= 15)
        {
            ltlAlert.Text = "alert('  Only 12 pages at one time！  ');";
            return;
        }
        ListItem item;
        int iUserId = Convert.ToInt32(Session["uID"]);
        for( int i=0; i< chkBL_Menu.Items.Count; i++)
        {
            if (chkBL_Menu.Items[i].Selected == true)
            {
                if( CheckBoxList_homepages.Items.Count >= 15)
                {
                    BindUserHomepageMenu();
                    BindAllMenu();
                    ltlAlert.Text = "alert('  Only 15 pages at one time！  ');";
                    return;
          
                }
                String strSQL = "If Not Exists (select * from adm_homepages where ahp_userId=" + iUserId + " and ahp_moduleID=" + Convert.ToInt32(chkBL_Menu.Items[i].Value) + ")";
                       strSQL += " Begin Insert adm_homepages(ahp_userId,ahp_moduleID, ahp_createDate) Values( '" + iUserId +"', '"+ Convert.ToInt32(chkBL_Menu.Items[i].Value) +"','"+ System.DateTime.Now +"')";
                       strSQL += " select 1 end Else Select -1";
                  
               int iresult = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL));
               if (iresult >= 1)
               {
                   item = new ListItem();
                   item.Text = chkBL_Menu.Items[i].Text.ToString();
                   item.Value = chkBL_Menu.Items[i].Value.ToString();
                   CheckBoxList_homepages.Items.Add(item);
               }
            }
         }
         //BindUserHomepageMenu();
         BindAllMenu();
    }

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        int iUserId = Convert.ToInt32(Session["uID"]);
        if(CheckBoxList_homepages.Items.Count <1)
        {
            ltlAlert.Text = "alert('  No pages，no clear！  ');";
                    return;

        }
        for (int i = 0; i < CheckBoxList_homepages.Items.Count; i++)
        {
            if (CheckBoxList_homepages.Items[i].Selected == true)
            {

                String strSQL = @"Delete adm_homepages Where ahp_userId = '" + Convert.ToInt32(Session["uID"]) + "'and ahp_moduleID = "+ Convert.ToInt32(CheckBoxList_homepages.Items[i].Value);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
            }
        }
        BindUserHomepageMenu();
        BindAllMenu();

    }
    protected void dll_Menu_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllMenu();
    }
}
