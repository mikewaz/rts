using UnityEngine;

public abstract class Subject : MonoBehaviour {

    public int Health;
    [HideInInspector]
    public ColliderType ColliderType;
    [HideInInspector]
    public delegate void dDie();
    [HideInInspector]
    public event dDie Died;
    protected SubjectType SubjectType;


    public void MakeDamage(int damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Died();
        Destroy(this.gameObject);
    }
}

public enum SubjectType
{
    None,
    Unit,
    Building
}

public enum ColliderType
{
    CapsulaCollider,
    BoxCollired
}

public enum UnitScriptType
{
    MeleeWarriorUnit
}