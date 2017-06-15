using System;
using UnityEngine;

public abstract class WarriorUnit : Unit
{
    [Range(1f, 100f)]
    public int Damage;

    protected int CalculateTheDamage()
    {
        return this.Damage;
    }

    protected abstract override void MainAction();
}
