using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public int damageToDeal;
    [SerializeField]
    private Text characterNameText;

    // Start is called before the first frame update
    void Start()
    {
        characterNameText.text = characterName;
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
        int damageTaken = CombatManager.Instance.CalculateDamageAmount(damageAmount, defense);
        if (defending)
        {
            damageTaken=damageTaken / 2;
        }
        if (damageTaken <= 0)
        {
            damageTaken = 1;
        }
        damageToDeal += damageTaken;
    }
}
