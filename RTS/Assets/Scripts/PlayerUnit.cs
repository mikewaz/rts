using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit
{
    void Start()
    {
        this.enemyTag = "Enemy";
        this.agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent.stoppingDistance = this.RadiusOfAttack;
    }

    public void TurnOffSelection()
    {
        this.transform.FindChild("Selected").gameObject.SetActive(false);
    }
}
