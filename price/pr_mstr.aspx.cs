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
using QCProgress;

namespace TCP.Price
{
    public partial class price_pr_mstr : System.Web.UI.Page
    {
        POPrice _price = new POPrice();
        GridViewNullData ogv = new GridViewNullData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
            else
            {
                ogv.ResetGridView(gvPrice);
            }
        }

        private void GridViewBind()
        {
            DataTable _Table = _price.GetPriceList(txtPart.Text.Trim(), txtDate.Text.Trim()).Tables[0];

            String DOMAIN = String.Empty;
            String PART = String.Empty;
            String VENT = String.Empty;

            foreach (DataRow row in _Table.Rows)
            {
                if (DOMAIN == row[0].ToString().ToUpper().Trim() && PART == row[1].ToString().ToUpper().Trim() && VENT == row[2].ToString().ToUpper().Trim())
                {
                    row.Delete();
                }
                else
                {
                    DOMAIN = row[0].ToString().ToUpper().Trim();
                    PART = row[1].ToString().ToUpper().Trim();
                    VENT = row[2].ToString().ToUpper().Trim();
                }
            }

            ogv.GridViewDataBind(gvPrice, _Table);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtPart.Text.Trim().Length < 8 || txtPart.Text.Trim().Length > 14)
            {
                ltlAlert.Text = "alert('零件号的位数应该在8至14位!');";
                return;
            }

            GridViewBind();
        }


        protected void gvPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataSet _dataset = _price.GetPartInfo(e.Row.Cells[2].Text, e.Row.Cells[0].Text);

                if (_dataset != null && _dataset.Tables[0].Rows.Count > 0)
                {
                    e.Row.Cells[1].Text = _dataset.Tables[0].Rows[0][0].ToString();
                    e.Row.Cells[3].Text = _dataset.Tables[0].Rows[0][1].ToString();
                    e.Row.Cells[5].Text = _dataset.Tables[0].Rows[0][2].ToString().ToUpper();
                }

                e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToUpper();
                e.Row.Cells[6].Text = _price.GetVendInfo(e.Row.Cells[6].Text, e.Row.Cells[0].Text);
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.Split(';')[0];
            }
        }
        protected void gvPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrice.PageIndex = e.NewPageIndex;

            GridViewBind();
        }
}
}