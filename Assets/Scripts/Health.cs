using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeAgent { Villager, Zombie, Golem }
public enum UnitGame { Zombie, Soldier, None }

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public Image HealthBarLocal;
    public int health = 100;
    public int healthMax = 100;
    public bool IsDead => health <= 0;

    [Header("References")]
    public Transform AimOffset;
    public Health HurtingMe;
    public TypeAgent typeAgent;
    public List<TypeAgent> typeAgentAllies = new List<TypeAgent>();
    public UnitGame _UnitGame;
    public bool IsCantView = true;

    public void Damage(int damage, Health enemy)
    {
        if (IsDead) return;
        health = Mathf.Max(health - damage, 0);
        UpdateHealthBar();
        if (health <= 0) OnDeath();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, healthMax);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (HealthBarLocal)
            HealthBarLocal.fillAmount = (float)health / healthMax;
    }

    private void OnDeath()
    {
        Debug.Log(gameObject.name + " ha muerto!");
        Destroy(gameObject, 2f);
    }
}