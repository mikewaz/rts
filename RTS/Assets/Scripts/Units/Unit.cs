using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Fields

    [Range(0.5f, 100f)]
    public float RadiusOfAttack;
    [Range(0.3f, 2f)]
    public float AttackSpeed;
    public int Health;

    GameObject _target;
    float delayAttack;
    float stopDistance;
    protected NavMeshAgent agent;
    protected string enemyTag;

    #endregion


    void Start()
    {
        this.delayAttack = this.AttackSpeed;
    }


    void Update()
    {
        if (this._target == null) return;

        if (this.agent.remainingDistance <= this.RadiusOfAttack + this._target.GetComponent<CapsuleCollider>().radius)
        {
            if (this._target.tag == this.enemyTag)
            {
                Attack();
                return;
            }
        }

        this.agent.SetDestination(this._target.transform.position);
    }


    public void SetTarget(Vector3 target)
    {
        this.agent.SetDestination(target);
    }


    public void SetTarget(GameObject target)
    {
        this._target = target.gameObject;
        this.agent.SetDestination(target.transform.position);
    }


    public void ChangeAttackRadius(float newRadius)
    {
        this.agent.stoppingDistance = newRadius;
        this.RadiusOfAttack = newRadius;
    }


    private void Attack()
    {
        if (this.delayAttack >= this.AttackSpeed)
        {
            this.delayAttack = 0;
        }
        else
        {
            this.delayAttack += Time.deltaTime;
        }
    }
}