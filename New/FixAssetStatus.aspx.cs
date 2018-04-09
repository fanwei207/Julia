//summary
//     Author :   Simon
//Create Date :   May 12 ,2009
//Description :   Maintenance the Status in basic information module for fix asset. .
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

public partial class new_FixAssetStatus :BasePage
{
     adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnSaveStatus_Click(object sender, EventArgs e)
    {
        if (txtFixstatus.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('状态 不能为空！');";
            return;
        }
        
        GetDataTcp.SaveOrModifiedStatus(0, txtFixstatus.Text, Convert.ToInt32(Session["uID"]));
        txtFixstatus.Text = "";
        ltlAlert.Text = "alert('保存成功！');Form1.txtFixstatus.focus();";
        return;
         
        gvStatus.DataBind();
    }
}
