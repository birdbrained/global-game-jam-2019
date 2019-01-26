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
    public bool wasImagined;
    public bool wasAttacked;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsDead)
        {
            //play animation here
            print("He DIED");
            Destroy(this);
        }
        if (isSelectable)
        {
            sr.color = Color.yellow;
        }
        else
        {
            sr.color = Color.white;
        }
    }
    void OnMouseDown()
    {
        Debug.Log("The mouse is down!");
        if (isSelectable)
        {
            //check if the attack was an imagined one or a normal one!
            //This damage calcuation is a test. actual combat calcuation handled by someone else
            int damage = CombatManager.Instance.CalculateDamageAmount(5, 1);
            this.TakeDamage(damage);
            //print(damage + " was dealt to the enemy");
            CombatManager.Instance.logText.text = damage.ToString() + " was dealt to " + characterName + "!";
            CombatManager.Instance.isAttacking = false;
            
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
