using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public int damageToDeal;
    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Text imaginationText;
    [SerializeField]
    private Text healthText;

    private string baseTextHP;
    private string baseTextIP;

    //This is a debug Timer for Enemies. Makes them "randomly" attack.



    // Start is called before the first frame update
    void Start()
    {
        if (characterNameText != null)
        {
            characterNameText.text = characterName;
            baseTextHP = healthText.text;
            baseTextIP = imaginationText.text;
            imaginationText.text = imaginationText.text + " " + this.maxHealth.ToString();
            healthText.text = healthText.text + " " + this.maxIP.ToString();
            
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Time.time);

        if (damageToDeal > 0 && Time.time % 0.5 == 0)
        {
            currHealth--;
            damageToDeal--;
        }
        imaginationText.text = baseTextHP + " " + this.currHealth.ToString();
        healthText.text = baseTextIP + " " + this.currIP.ToString();
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
