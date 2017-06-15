using UnityEngine;

public class MeleeWarrior : WarriorUnit
{
    protected override void MainAction()
    {
        this.DelayOfAction += Time.deltaTime;

        if (this.DelayOfAction < this.SpeedOfAction) return;

        this.DelayOfAction = 0;
        var damage = CalculateTheDamage();
        this.Target.GetComponent<Subject>().MakeDamage(damage);
    }
}
