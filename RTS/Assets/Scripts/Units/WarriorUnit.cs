using System;
using UnityEngine;

public class WarriorUnit : Unit
{
    [Range(1f, 100f)]
    public int Damage;

    protected override void MainAction()
    {
        this.DelayOfAction += Time.deltaTime;
        if (this.DelayOfAction >= this.SpeedOfAction)
        {
            this.DelayOfAction = 0;
            var damage = CalculateTheDamage();
            this.Target.GetComponent<Subject>().MakeDamage(damage);
        }
    }

    private int CalculateTheDamage()
    {
        Debug.Log("Attack");
        return this.Damage;
    }
}
