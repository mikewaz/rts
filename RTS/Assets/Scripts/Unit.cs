using UnityEngine;

public class Unit : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;

    // Use this for initialization
    void Start()
    {
        this._agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 target)
    {
        this._agent.SetDestination(target);
    }

    public void SetTarget(GameObject target)
    {
        this._agent.SetDestination(target.transform.position);
    }

    public void TurnOffSelection()
    {
        this.transform.FindChild("Selected").gameObject.SetActive(false);
    }
}
