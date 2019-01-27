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
            //print("He DIED");
            isSelectable = false;
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
        if (IsDead)
        {
            sr.color = Color.gray;
        }
    }
    void OnMouseDown()
    {
        //Debug.Log("The mouse is down!");
        if (isSelectable)
        {
            //check if the attack was an imagined one or a normal one!
            //This damage calcuation is a test. actual combat calcuation handled by someone else
            Character character = Turnmaker.currentturn.GetComponent<Character>();
            Debug.LogWarning(character == null);
            int damage = CombatManager.Instance.CalculateDamageAmount(character.attack, defense);
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
