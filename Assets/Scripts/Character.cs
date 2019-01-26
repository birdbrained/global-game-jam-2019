﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    //public int damageToDeal;
    public int maxMP;
    public int currMP;
    public int attack;
    public int defense;
    public int agility;

    public abstract void TakeDamage(int damageAmount);
}
