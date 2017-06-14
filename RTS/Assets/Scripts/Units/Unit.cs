using UnityEngine;
using System.Collections;

public abstract class Unit : Subject
{
    #region Fields

    protected NavMeshAgent navMeshAgent;
    protected GameObject Target;
    protected SubjectType TargetType;
    protected float DelayOfAction;

    [Range(1f, 100f)]
    public float RadiusOfAction;
    [Range(0.1f, 2f)]
    public float SpeedOfAction;
    [HideInInspector]
    public CapsuleCollider Collired;

    #endregion

    #region Private methods

    private void Start()
    {
        this.DelayOfAction = this.SpeedOfAction;
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.navMeshAgent.stoppingDistance = this.RadiusOfAction;
        this.Collired = this.GetComponent<CapsuleCollider>();
        this.SubjectType = SubjectType.Unit;
        this.ColliderType = ColliderType.CapsulaCollider;
    }

    private void Update()
    {
        SelectOperation();
    }

    private void TargetDied()
    {
        Debug.Log("Targer deid");
        this.Target = null;
    }

    private bool TurnTowardsTheTarget()
    {
        if (this.Target == null) return false;

        var gip = (this.Target.transform.position - this.transform.position).magnitude;
        var kat = this.Target.transform.position.z - this.transform.position.z;
        var ralpha = Mathf.Asin(kat / gip);
        var alpha = ralpha * (180 / Mathf.PI);        

        var s = 1f;
        alpha = alpha > 0 ? alpha : 360 - Mathf.Abs(alpha);

        var unitVector = this.transform.position;
        var targetVectory = this.Target.transform.position;

        if (unitVector.z >= targetVectory.z && unitVector.x > targetVectory.x)
        {
            //Debug.Log(1);
            alpha = alpha - 90;
            s = -1f;
        }
        if (unitVector.z >= targetVectory.z && unitVector.x < targetVectory.x)
        {
            //Debug.Log(2);
            alpha = alpha - 180;
            s = 1f;
        }
        if (unitVector.z < targetVectory.z && unitVector.x >= targetVectory.x)
        {
            //Debug.Log(3);
            alpha = 270 + alpha;
            s = -1f;
        }

        if (this.transform.rotation.eulerAngles.y < alpha - 1 || this.transform.rotation.eulerAngles.y > alpha + 1)
        {
            this.transform.Rotate(0, s, 0);
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion

    #region Public methods

    public void SetTarget(GameObject target)
    {
        this.Target = target;
        target.GetComponent<Subject>().Died += this.TargetDied;
    }

    public void SetTarget(Vector3 position)
    {
        this.navMeshAgent.SetDestination(position);
        this.TargetType = SubjectType.None;
    }


    #endregion

    #region Protected virtual methods

    protected virtual void SelectOperation()
    {
        if (this.Target == null)
            return;

        if ((this.transform.position - this.Target.transform.position).magnitude >
              this.navMeshAgent.stoppingDistance + this.GetComponent<CapsuleCollider>().radius +
              this.Target.GetComponent<CapsuleCollider>().radius)
        {
            if (this.navMeshAgent.pathEndPosition != this.Target.transform.position)
                this.navMeshAgent.SetDestination(this.Target.transform.position);
        }
        else
        {
            this.navMeshAgent.ResetPath();
            if(TurnTowardsTheTarget())
                MainAction();
        }
    }

    #endregion

    #region Protected methods

    protected abstract void MainAction();

    #endregion
}
