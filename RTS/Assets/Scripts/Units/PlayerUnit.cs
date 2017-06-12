using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit
{
    void Start()
    {
        this.enemyTag = "Enemy's Unit";
        this.agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent.stoppingDistance = this.RadiusOfAttack + this.transform.GetComponent<CapsuleCollider>().radius;
    }

    public void TurnOffSelection()
    {
        this.transform.FindChild("Selected").gameObject.SetActive(false);
    }
}
