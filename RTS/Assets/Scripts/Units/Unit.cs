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

    #endregion

    #region Public methods

    public void SetTarget(GameObject target)
    {
        this.Target = target;
        target.GetComponent<Subject>().Died += this.TargetDied;
    }

    private void TargetDied()
    {
        Debug.Log("Targer deid");
        this.Target = null;
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
            MainAction();
        }
    }

    #endregion

    #region Protected methods

    protected abstract void MainAction();

    #endregion
}
