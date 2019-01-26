using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currHealth;
    //public int damageToDeal;
    public int maxMP;
    public int currMP;
    public int attack;
    public int defense;
    public int agility;

    public bool IsDead
    {
        get
        {
            return currHealth <= 0;
        }
    }

    public abstract void TakeDamage(int damageAmount);
}
