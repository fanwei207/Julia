using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for WorkOrderInfor
/// </summary>
namespace WOrder
{
    public class WorkOrderInfor
    {
        public WorkOrderInfor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _startDate;
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }


        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

      
        private string _Center;
        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

   
        private string _Order;
        public string Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        private string _OrderID;
        public string OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private string  _Part;
        public string Part
        {
            get { return _Part; }
            set { _Part = value; }
        }

        private string _PartDesc;
        public string PartDesc
        {
            get { return _PartDesc; }
            set { _PartDesc = value; }
        }

        private string _Tec;
        public string Tec
        {
            get { return _Tec; }
            set { _Tec = value; }
        }

        private string _Line;
        public string Line
        {
            get { return _Line; }
            set { _Line = value; }
        }

        private int _WorkOrderID;
        public int WorkOrderID
        {
            get { return _WorkOrderID; }
            set { _WorkOrderID = value; }
        }
    }
}
