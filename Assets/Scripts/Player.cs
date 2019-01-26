using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int damageToDeal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damageToDeal > 0)
        {
            currHealth--;
            damageToDeal--;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        damageToDeal += CombatManager.Instance.CalculateDamageAmount(damageAmount, defense);
    }
}
