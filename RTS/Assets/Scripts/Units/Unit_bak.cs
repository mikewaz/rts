using UnityEngine;

public abstract class Unit_bak : MonoBehaviour
{
    #region Fields

    [Range(1, 2000)]
    public int Health;
    [HideInInspector]
    public float capsulaRadius;

    public int Player;
    float stopDistance;
    protected NavMeshAgent agent;
    protected GameObject target;
    protected TargetType targetType;

    #endregion


    #region Private

    void Start()
    {
        this.stopDistance = this.GetComponent<CapsuleCollider>().radius;
    }


    void Update()
    {
        if (this.target == null) return;

        SelectOperation();
    }

    #endregion

    #region Public

    public void SetTarget(Vector3 target)
    {
        this.agent.SetDestination(target);
    }


    public void SetTarget(GameObject target)
    {
        this.target = target.gameObject;
        this.agent.SetDestination(target.transform.position);
        SelectTargetType(this.target);
    }


    public virtual void ChangeActionRadius(float newRadius)
    {
        this.agent.stoppingDistance = newRadius;
    }

    #endregion

    #region

    protected void SelectTargetType(GameObject targetType)
    {
        switch (targetType.tag)
        {
            case "Enemy's Unit":
                this.targetType = TargetType.Unit;
                break;
            default:
            case "Untagged":
                this.targetType = TargetType.Terrain;
                break;
        }
    }

    #endregion

    #region Protected Abstract

    protected abstract void Attack();

    protected abstract void SelectOperation();

    protected abstract void StartConfigurations();

    protected abstract void MakeHit(GameObject targetType);

    protected abstract void Kill();

    #endregion
}