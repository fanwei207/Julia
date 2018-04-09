//summary
//     Author :   Simon
//Create Date :   May 14 ,2009
//Description :   Maintenance the Type in basic information module for fix asset. .
//summary
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
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;


public partial class new_FixAssetType : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        }
    }

    protected void btnSaveFixType_Click(object sender, EventArgs e)
    {
        if (txtFixType.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('类型 不能为空！');";
            return;
        }

        if (txtFixLift.Text.Trim().Length > 0)
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtFixLift.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('使用年限 只能是数字！');";
                return;
            }
        }

        try
        {
            int intFixLift;
            if (txtFixLift.Text.Trim().Length == 0)
                intFixLift = 0;
            else
                intFixLift = Convert.ToInt32(txtFixLift.Text.Trim());

            GetDataTcp.SaveOrModifyType(0, adam.sqlEncode(txtFixType.Text), intFixLift, Convert.ToInt32(Session["uID"]));
            txtFixLift.Text = "";
            txtFixType.Text = "";
            gvType.DataBind();
            ltlAlert.Text = "alert('保存成功！');";
            return;
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！');";
            return;  
        }

    }

    protected void Detail_Command(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strFixtyID = string.Empty;

        //如果按钮的CommandName为DetailType时，
        if (e.CommandName.ToString() == "DetailType")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strFixtyID = gvType.DataKeys[intRow].Value.ToString();

            //页面跳转到FixAssetTypeDetail.aspx
            Response.Redirect("/new/FixAssetTypeDetail.aspx?id=" + strFixtyID + "&rm=" + DateTime.Now.ToString(), true);
        }
    }
}
