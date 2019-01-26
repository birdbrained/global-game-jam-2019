using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public string characterName;
    public int maxHealth;
    public int currHealth;
    public int attack;
    public int defense;
    public int agility;
    public bool isSelectable;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectable)
        {
            sr.color = Color.yellow;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        //check if dead
    }
    public bool IsDead
    {
        get
        {
            return currHealth <= 0;
        }
    }
}
