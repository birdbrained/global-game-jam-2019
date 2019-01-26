using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public int damageToDeal;
    [SerializeField]
    private Text characterNameText;
    public string characterName;

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
        damageToDeal += CombatManager.Instance.CalculateDamageAmount(damageAmount, defense);
    }
}
