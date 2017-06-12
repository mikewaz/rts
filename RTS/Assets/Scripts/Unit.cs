using UnityEngine;

public class Unit : MonoBehaviour
{
    [Range(0.5f, 100f)]
    public float RadiusOfAttack;
    public int Health;

    GameObject _target;
    bool _isArived;
    protected NavMeshAgent agent;
    protected string enemyTag;


    void Update()
    {
        if (this.agent.remainingDistance <= this.RadiusOfAttack)
        {
            if (this._target != null)
            {
                if (this._target.tag == enemyTag)
                    Attack();
                else
                    this._target = null;
            }
        }

        if (this._target != null)
        {
            this.agent.SetDestination(this._target.transform.position);
        }
    }


    public void SetTarget(Vector3 target)
    {
        this.agent.SetDestination(target);
    }


    public void SetTarget(GameObject target)
    {
        this._isArived = false;
        this._target = target.gameObject;
        this.agent.SetDestination(target.transform.position);
    }


    private void Attack()
    {

    }

}
