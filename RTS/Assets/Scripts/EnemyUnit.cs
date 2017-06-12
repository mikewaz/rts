using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit
{
	void Start ()
    {
        this.enemyTag = "Player Unit";
        this.agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent.stoppingDistance = this.RadiusOfAttack;
    }
}
