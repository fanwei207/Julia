using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SampleDeliveryApply
/// </summary>
[Serializable]
public class SampleDeliveryApply
{
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private int mstrId;

    public int MstrId
    {
        get { return mstrId; }
        set { mstrId = value; }
    }

    private int applyBy;

    public int ApplyBy
    {
        get { return applyBy; }
        set { applyBy = value; }
    }

    private string applyer;

    public string Applyer
    {
        get { return applyer; }
        set { applyer = value; }
    }

    private DateTime applyDate;

    public DateTime ApplyDate
    {
        get { return applyDate; }
        set { applyDate = value; }
    }

    private string reason;

    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }

    private bool? approveResult;

    public bool? ApproveResult
    {
        get { return approveResult; }
        set { approveResult = value; }
    }

    private DateTime? approveDate;

    public DateTime? ApproveDate
    {
        get { return approveDate; }
        set { approveDate = value; }
    }

    private int currentApproveBy;

    public int CurrentApproveBy
    {
        get { return currentApproveBy; }
        set { currentApproveBy = value; }
    }

    private string currentApprover;

    public string CurrentApprover
    {
        get { return currentApprover; }
        set { currentApprover = value; }
    }


    public SampleDeliveryApply(int id)
    {
        this.id = id;
    }

	public SampleDeliveryApply()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}