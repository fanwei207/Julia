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

public partial class new_FixAssetHistView : BasePage
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
    protected void gvAsset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[11].Text.Trim() == "True")
                e.Row.Cells[11].Text = "ÐÞ¸Ä";
            else if (e.Row.Cells[11].Text.Trim() == "False")
                e.Row.Cells[11].Text = "É¾³ý";

           
            e.Row.Attributes.Add("onclick", "window.showModalDialog('FixAssetHistViewDialog.aspx?assetno=" + Server.UrlDecode(gvAsset.DataKeys[e.Row.RowIndex].Value.ToString().Trim()) + "',null,'dialogWidth=920px;dialogHeight=500px');");
        }
    }
}
