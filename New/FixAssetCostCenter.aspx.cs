//summary
//     Author :   Simon
//Create Date :   May 15 ,2009
//Description :   Maintenance the Cost Center in basic information module for fix asset. .
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

public partial class new_FixAssetCostCenter : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //Populates Fix Asset Entity Drop Down.             
            DataTable dtDrop = GetDataTcp.GetEntityFixAsset();
            ListItem item;
            item = new ListItem("--");
            item.Value = "0";
            dropEntity.Items.Add(item);
            //bind the dropdown
            if (dtDrop.Rows.Count > 0)
            {
                for (int i = 0; i < dtDrop.Rows.Count; i++)
                {
                    item = new ListItem(dtDrop.Rows[i][1].ToString());
                    item.Value = dtDrop.Rows[i][0].ToString();
                    dropEntity.Items.Add(item);
                }
                dropEntity.SelectedIndex = 0;
            }
        }
    }


    protected void btnSaveCostCenter_Click(object sender, EventArgs e)
    {
        if (dropEntity.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 会计单位！');";
            return;
        }

        if (txtCostCenter.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('名称 不能为空！');";
            return;
        }

        try
        {
            GetDataTcp.SaveOrModifyCostCenter(Convert.ToInt32(lblCostCenter.Text.Trim()), Convert.ToInt32(dropEntity.SelectedValue), txtCostCenter.Text.Trim(), adam.sqlEncode(txtCenterDes.Text.Trim()), Convert.ToInt32(Session["uID"]));
            dropEntity.SelectedIndex = 0;
            txtCenterDes.Text = "";
            txtCostCenter.Text = "";
            lblCostCenter.Text = "0";
            gvCostCenter.DataBind(); 
            ltlAlert.Text = "alert('保存成功！');";
            return;

            dropEntity.Focus(); 
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！');";
            return;
        }
    }

    protected void MyRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            try
            {
                int intIndex = Convert.ToInt32(e.CommandArgument.ToString());
                Label lblDomain =(Label)gvCostCenter.Rows[intIndex].FindControl("lblViewVisible");
                dropEntity.Text = lblDomain.Text;

                txtCostCenter.Text = gvCostCenter.Rows[intIndex].Cells[2].Text;
                txtCenterDes.Text = (gvCostCenter.Rows[intIndex].Cells[3].Text == "&nbsp;") ? "" : gvCostCenter.Rows[intIndex].Cells[3].Text;
                lblCostCenter.Text = gvCostCenter.DataKeys[intIndex].Value.ToString ();
                gvCostCenter.DataBind();
              
            }

            catch
            { 
                return;
            }
        }
    }
}
