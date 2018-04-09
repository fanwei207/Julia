using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SampleDeliveryDetail
/// </summary>
[Serializable]
public class SampleDeliveryDetail
{
    private int id;
    public int Id
    {
        get 
        {
            return id;
        }
    }

    private int mstrId;
    public int MstrId
    {
        get
        {
            return mstrId;
        }
    }

    private string partCode;
    public string PartCode
    {
        get
        {
            return partCode;
        }
        set
        {
            partCode = value;
        }
    }

    private string qadNo;
    public string QadNo
    {
        get
        {
            return qadNo;
        }
        set
        {
            qadNo = value;
        }
    }

    private decimal quantity;
    public decimal Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
        }
    }

    private string remarks;
    public string Remarks
    {
        get
        {
            return remarks;
        }
        set
        {
            remarks = value;
        }
    }

    private int checkedby;
    public int CheckedBy
    {
        set
        {
            checkedby = value;
        }
        get
        {
            return checkedby;
        }
    }

    private string checker;
    public string Checker
    {
        get
        {
            return checker;
        }
        set
        {
            checker = value;
        }
    }

    private DateTime? checkedDate;
    public DateTime? CheckedDate
    {
        get
        {
            return checkedDate;
        }
        set
        {
            checkedDate = value;
        }
    }

    private bool? checkResult;
    public bool? CheckResult
    {
        get
        {
            return checkResult;
        }
        set
        {
            checkResult = value;
        }
    }

    private string checkRemarks;
    public string CheckRemarks
    {
        get
        {
            return checkRemarks;
        }
        set
        {
            checkRemarks = value;
        }
    }
    

	public SampleDeliveryDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SampleDeliveryDetail(int id)
    {
        this.id = id;
    }

    public SampleDeliveryDetail(int id, int mstrId)
    {
        this.id = id;
        this.mstrId = id;
    }
}