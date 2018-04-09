using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;

public partial class part_NPart_addNPartType : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindSupplieType();
            ddlBroadHeading.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddlSubDivision.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddlSubMaterial.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddllevel.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            initDDLLevel();
            txtISEnable();
        }
    }

    

    private void initDDLLevel()
    {
        ddllevel.DataTextField = "FactoryInspection";
        ddllevel.DataValueField = "FactoryInspectionLevelID";
        ddllevel.DataSource = daoDDLLevel();
        ddllevel.DataBind();

        ddllevel.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }

    private DataTable daoDDLLevel()
    {
        string sqlstr = "SELECT DISTINCT FactoryInspection,FactoryInspectionLevelID FROM dbo.NPart_Part where ISNULL(isDelete,0) = 0  ORDER BY FactoryInspection";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sqlstr).Tables[0];
    }

    private void bindSupplieType()
    {
        ddlSupplieType.DataTextField = "SupplieType";
        ddlSupplieType.DataValueField = "SupplieTypeID";
        ddlSupplieType.DataSource = initSupplieType();
        ddlSupplieType.DataBind();

        ddlSupplieType.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }

    private DataTable initSupplieType()
    {
        string sql = "SELECT DISTINCT  SupplieTypeID,SupplieType from dbo.NPart_Part where SupplieTypeID is not null";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

    protected void ddlSupplieType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBroadHeading.Items.Clear();
        ddlSubDivision.Items.Clear();
        ddlSubMaterial.Items.Clear();
        ddlSubDivision.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
        ddlSubMaterial.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));

        ddlBroadHeading.DataTextField = "BroadHeading";
        ddlBroadHeading.DataValueField = "BroadHeadingID";
        ddlBroadHeading.DataSource = bindDDLBroadHeading();
        ddlBroadHeading.DataBind();
        ddlBroadHeading.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
        txtISEnable();
        
    }

    private DataTable bindDDLBroadHeading()
    {
        string sqlstr = "SELECT DISTINCT  BroadHeading, BroadHeadingID FROM dbo.NPart_Part  WHERE BroadHeadingID is not null and  ISNULL(isDelete,0) = 0 and SupplieTypeID = '" + ddlSupplieType.SelectedValue + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sqlstr).Tables[0];
    }
    protected void ddlBroadHeading_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubDivision.Items.Clear();
        ddlSubMaterial.Items.Clear();
        ddlSubMaterial.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));

        ddlSubDivision.DataTextField = "SubDivision";
        ddlSubDivision.DataValueField = "SubDivisionID";
        ddlSubDivision.DataSource = bindDDLSubDivision();
        ddlSubDivision.DataBind();
        ddlSubDivision.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
        txtISEnable();
       
    }

    private DataTable bindDDLSubDivision()
    {
        string sqlstr = "SELECT DISTINCT  SubDivision,SubDivisionID FROM dbo.NPart_Part  WHERE SubDivisionID is not null and  ISNULL(isDelete,0) = 0 and  BroadHeadingID = '" + ddlBroadHeading.SelectedValue + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sqlstr).Tables[0];
    }


    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubMaterial.Items.Clear();

        ddlSubMaterial.DataTextField = "SubMaterial";
        ddlSubMaterial.DataValueField = "SubMaterialID";
        ddlSubMaterial.DataSource = bindDDLSubMaterial();
        ddlSubMaterial.DataBind();
        ddlSubMaterial.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
        txtISEnable();
        
    }

    private DataTable bindDDLSubMaterial()
    {
        string sqlstr = "SELECT DISTINCT  SubMaterial,SubMaterialID FROM dbo.NPart_Part  WHERE SubMaterialID  is not null and  ISNULL(isDelete,0) = 0 and  SubDivisionID = '" + ddlSubDivision.SelectedValue + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sqlstr).Tables[0];
    }
    protected void ddlSubMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtISEnable();
    }

    protected void ddllevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtISEnable();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../part/NPart_partManager.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddllevel.SelectedValue == "00000000-0000-0000-0000-000000000000" && txtlevel.Text.Trim().Equals(string.Empty))
        {
            ltlAlert.Text = "alert('等级必须填写或者选择');";
            return;
        }
        if (ddlSupplieType.SelectedValue == "00000000-0000-0000-0000-000000000000" && txtSupplieType.Text.Trim().Equals(string.Empty))
        {
            ltlAlert.Text = "alert('供应商类型必须填写或者选择');";
            return;
        }
        if (ddlBroadHeading.SelectedValue == "00000000-0000-0000-0000-000000000000" && txtBroadHeading.Text.Trim().Equals(string.Empty))
        {
            ltlAlert.Text = "alert('大区分类必须填写或者选择');";
            return;
        }

        int flag = addNPart();

        if (flag == 1)
        {
            ltlAlert.Text = "alert('添加成功');";

            ddlSupplieType.Items.Clear();
            ddlBroadHeading.Items.Clear();
            ddlSubDivision.Items.Clear();
            ddlSubMaterial.Items.Clear();

            bindSupplieType();
            ddlBroadHeading.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddlSubDivision.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddlSubMaterial.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            ddllevel.Items.Add(new ListItem("--", "00000000-0000-0000-0000-000000000000"));
            initDDLLevel();
            txtISEnable();

        }
        else if (flag == 2)
        {
            ltlAlert.Text = "alert('您添加的类型已存在');";
        }
        else if (flag == 0)
        {
            ltlAlert.Text = "alert('添加失败，请联系管理员');";
        }
        else if (flag == 3)
        {
            ltlAlert.Text = "alert('供应商类型已经存在');";
        }
        else if (flag == 4)
        {
            ltlAlert.Text = "alert('大区分类已存在');";
        }
        else if (flag == 5)
        {
            ltlAlert.Text = "alert('细部区分已存在');";
        }
        else if (flag == 6)
        {
            ltlAlert.Text = "alert('子物料已存在');";
        }
    }

    private int addNPart()
    {
        string sqlstr = "sp_NPart_addPartType";

        SqlParameter[] param = new SqlParameter[]{
            new  SqlParameter("@SupplieTypeID",ddlSupplieType.SelectedValue)
            , new SqlParameter("@SupplieType",txtSupplieType.Text.Trim())
            , new SqlParameter("@BroadHeadingID", ddlBroadHeading.SelectedValue)
            , new SqlParameter("@BroadHeading", txtBroadHeading.Text.Trim())
            , new SqlParameter("@SubDivisionID", ddlSubDivision.SelectedValue)
            , new SqlParameter("@SubDivision", txtSubDivision.Text.Trim())
            , new SqlParameter("@SubMaterialID",ddlSubMaterial.SelectedValue)
            , new SqlParameter("@SubMaterial",txtSubMaterial.Text.Trim())
            , new SqlParameter("@LevelID", ddllevel.SelectedValue)
            , new SqlParameter("@Level",txtlevel.Text.Trim())
            , new SqlParameter("@uID", Session["uID"].ToString())
        
        };

        return Convert.ToInt32( SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }


    private void txtISEnable()
    {
        if (ddlSupplieType.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            txtSupplieType.Text = "";
            txtSupplieType.Enabled = true;
        }
        else
        {
            txtSupplieType.Text = "";
            txtSupplieType.Enabled = false;
        }

        if (ddlBroadHeading.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            txtBroadHeading.Text = "";
            txtBroadHeading.Enabled = true;
        }
        else
        {
            txtBroadHeading.Text = "";
            txtBroadHeading.Enabled = false;
        }
        if (ddlSubDivision.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            txtSubDivision.Text = "";
            txtSubDivision.Enabled = true;
        }
        else
        {
            txtSubDivision.Text = "";
            txtSubDivision.Enabled = false;
        }
        if (ddlSubMaterial.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            txtSubMaterial.Text = "";
            txtSubMaterial.Enabled = true;
        }
        else
        {
            txtSubMaterial.Text = "";
            txtSubMaterial.Enabled = false;
        }
        if (ddllevel.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            txtlevel.Text = "";
            txtlevel.Enabled = true;
        }
        else
        {
            txtlevel.Text = "";
            txtlevel.Enabled = false;
        }
    }
}