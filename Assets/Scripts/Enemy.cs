using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public bool isSelectable;
    public bool wasImagined;
    public bool wasAttacked;

    private SpriteRenderer sr;

    TurnOrder Turnmaker;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Turnmaker = FindObjectOfType<TurnOrder>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.IsDead)
        {
            //play animation here
            print("He DIED");
            //This is NOT how you get you should handle death. Use the queue system established by turn order!
            //Destroy(this);
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
        //Debug.Log("The mouse is down!");
        if (isSelectable)
        {
            //check if the attack was an imagined one or a normal one!
            //This damage calcuation is a test. actual combat calcuation handled by someone else
            int damage = CombatManager.Instance.CalculateDamageAmount(5, 1);
            this.TakeDamage(damage);
            //print(damage + " was dealt to the enemy");
            CombatManager.Instance.logText.text = damage.ToString() + " was dealt to " + characterName + "!";
            CombatManager.Instance.isAttacking = false;
            Turnmaker.playerturns();
            
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        //check if dead
    }
}
