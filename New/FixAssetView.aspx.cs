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

public partial class new_FixAssetView : BasePage
{
    adamClass adam = new adamClass(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListItem item;
            item = new ListItem("--");
            item.Value = "0";
            dropType.Items.Add(item);
            dropEntity.Items.Add(item);

            DataTable dtDropDown = GetDataTcp.GetEntityFixAsset();
            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropEntity.Items.Add(item);
                }
            }

            dtDropDown = null;
            dtDropDown = GetDataTcp.GetTypeFixAsset();
            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropType.Items.Add(item);
                }
            }

            dtDropDown = null;

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        txtAssetNo.Focus();

        gvAsset.DataBind();
    }

    protected void MyRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            try
            {
                int intIndex = int.Parse(e.CommandArgument.ToString());
                string AssetNo = gvAsset.DataKeys[intIndex].Value.ToString();

                if (AssetNo.IndexOf("+") < 0)
                {
                    Response.Redirect("FixAssetMaintenance.aspx?pageMode=100103116&AssetNo=" + Server.UrlEncode(AssetNo.Replace("+","%2B")));
                }
                else
                {
                    Response.Redirect("FixIncMaintenance.aspx?pageMode=100103117&AssetNo=" + Server.UrlEncode(AssetNo.Replace("+", "%2B")));
                }
            }
            catch
            {
                return;
            }
        }
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex >= 0)
        {
            dropDetailType.Enabled = true;
            DataTable typeDetail = GetDataTcp.selectTypeDetailFixAsset(Convert.ToInt32(dropType.SelectedValue)).Tables[0];
            if (typeDetail.Rows.Count > 0)
            {
                int i;
                ListItem item;
                dropDetailType.Items.Clear();

                for (i = 0; i < typeDetail.Rows.Count; i++)
                {
                    item = new ListItem(typeDetail.Rows[i].ItemArray[1].ToString());
                    item.Value = typeDetail.Rows[i].ItemArray[0].ToString();
                    dropDetailType.Items.Add(item);
                }
                dropDetailType.Items.Insert(0, new ListItem("--", "0"));
            }
            else
            {
                dropDetailType.Items.Clear();
                dropDetailType.Items.Insert(0, new ListItem("--", "0"));
            }
        }
    }
}
