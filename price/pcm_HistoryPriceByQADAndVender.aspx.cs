using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_HistoryPriceByQADAndVender : System.Web.UI.Page
{
    PCM_price pc = new PCM_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    private void bind()
    {
        string QAD = Request["QAD"].ToString();
        string vender = Request["vender"].ToString();
        
        gvPcMstr.DataSource = pc.selectHistoryPriceByQADAndVender(QAD, vender);
        gvPcMstr.DataBind();
    
    }
}