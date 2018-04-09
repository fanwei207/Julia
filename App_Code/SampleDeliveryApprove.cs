using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SampleDeliveryApprove
/// </summary>
[Serializable]
public class SampleDeliveryApprove
{
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private int applyId;

    public int ApplyId
    {
        get { return applyId; }
        set { applyId = value; }
    }

    private int applyBy;

    public int ApplyBy
    {
        get { return applyBy; }
        set { applyBy = value; }
    }

    private int approveBy;

    public int ApproveBy
    {
        get { return approveBy; }
        set { approveBy = value; }
    }

    private string approver;

    public string Approver
    {
        get { return approver; }
        set { approver = value; }
    }

    private DateTime? approveDate;

    public DateTime? ApproveDate
    {
        get { return approveDate; }
        set { approveDate = value; }
    }

    private bool? approveResult;

    public bool? ApproveResult
    {
        get { return approveResult; }
        set { approveResult = value; }
    }

    private string approveNote;

    public string ApproveNote
    {
        get { return approveNote; }
        set { approveNote = value; }
    }

    public SampleDeliveryApprove()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SampleDeliveryApprove(int id)
    {
        this.id = id;
    }
}