using UnityEngine;

public class healthZombie : Health
{
    [Header("Zombie Settings")]
    public int infectionDamage = 15; // Daño por infección
    public float attackCooldown = 2f;
    private float lastAttackTime;

    public override void Damage(int damage, Health enemy)
    {
        base.Damage(damage, enemy);

        // Infecta al atacante si es humano
        if (!IsDead && enemy != null && (enemy.typeAgent == TypeAgent.Villager || enemy._UnitGame == UnitGame.Soldier))
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                enemy.Damage(infectionDamage, this);
                lastAttackTime = Time.time;
            }
        }
    }

    protected override void OnDeath()
    {
        // Efectos especiales para zombie
        Debug.Log("Zombie eliminado!");
        Destroy(gameObject, 1.5f);
    }
}