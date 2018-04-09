using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IT;

public partial class IT_TSK_Devp : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int _n = DateTime.Now.Year;
            while (_n >= 2014)
            {
                dropYear.Items.Insert(0, new ListItem(_n.ToString(), _n.ToString()));

                _n--;
            }

            _n = 12;
            while (_n > 0)
            {
                dropMonth.Items.Insert(0, new ListItem(_n.ToString(), _n.ToString()));

                _n--;
            }

            dropMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;

            bind();
        }
    }
    public void bind()
    {
        DataTable table = TaskHelper.SelectTaskGannt(dropYear.SelectedValue, dropMonth.SelectedValue);
     DataTable tablefinish = TaskHelper.SelectTaskfinishGannt(dropYear.SelectedValue, dropMonth.SelectedValue);
            if (table == null)
            {
                this.Alert("无法获取未结任务！");
            }
            else
            {
                divGannt.InnerHtml = GenerateGannt(table, tablefinish);
            }
    }
    protected string GenerateGannt(DataTable table, DataTable tablefinish)
    {
        DateTime _stdDate = Convert.ToDateTime(dropYear.SelectedValue + "-" + dropMonth.SelectedValue + "-1");
        DateTime _endDate = _stdDate.AddMonths(1);

        string _html = "<table class=\"TaskGannt\" cellpadding=\"0\" cellspacing=\"0\">";
        _html += "<thead>";
        _html += "  <tr>";
        _html += "      <td>SUN 日</td>";
        _html += "      <td>MON 一</td>";
        _html += "      <td>TUE 二</td>";
        _html += "      <td>WED 三</td>";
        _html += "      <td>THU 四</td>";
        _html += "      <td>FRI 五</td>";
        _html += "      <td>SAT 六</td>";
        _html += "  </tr>";
        _html += "</thead>";
        _html += "<tbody>";

        _html += "<tr>";
        while (_stdDate < _endDate)
        {
            //第一天，周几之前要留白
            //最后一天，周几之后要留白
            if (_stdDate.Day == 1)
            {
                for (int i = 0; i < (int)_stdDate.DayOfWeek; i++)
                {
                    _html += "<td class=\"GanntEmpty\"></td>";
                }
            }

            _html += "<td>";
            _html += "  <div>" + _stdDate.Day.ToString() ;
            foreach (DataRow row in tablefinish.Select("tskd_day = " + _stdDate.Day.ToString()))
            {
                _html += "<div></div>";
            }

            _html += "</div>";

            foreach (DataRow row in table.Select("tskd_day = " + _stdDate.Day.ToString()))
            {
                //前两个正常显示，第三个显示More...
                if (Convert.ToInt32(row["tskd_index"]) == 3)
                {
                    _html += "<span><u>More...</u></span>";
                }
                else
                {
                    _html += "<span>" + row["tskd_index"].ToString() + "、" + row["tskd_desc"].ToString() + "</span>";
                }
            }

            _html += "</td>";

            if (_stdDate.AddDays(1) == _endDate)
            {
                for (int i = (int)_stdDate.DayOfWeek; i < (int)DayOfWeek.Saturday; i++)
                {
                    _html += "<td class=\"GanntEmpty\"></td>";
                }
            }

            if (_stdDate.DayOfWeek == DayOfWeek.Saturday)
            {
                //周六封行
                _html += "</tr><tr>";
            }

            _stdDate = _stdDate.AddDays(1);
        }

        _html += "</tr>";
        _html += "</tbody>";
        _html += "</table>";

        return _html;
    }
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
}