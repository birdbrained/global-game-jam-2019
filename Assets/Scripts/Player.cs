﻿using System.Collections;
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
        if (damageToDeal > 0 && Time.time % 0.5 == 0)
        {
            currHealth--;
            damageToDeal--;
        }
        if (imaginationText != null)
        {
            imaginationText.text = baseTextIP + " " + this.currIP.ToString();
        }
        if (healthText != null)
        {
            healthText.text =  baseTextHP + " " + this.currHealth.ToString();
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        int damageTaken = CombatManager.Instance.CalculateDamageAmount(damageAmount, defense);
        CombatManager.Instance.logText.text = Mathf.Abs(damageTaken).ToString() + " was taken by " + characterName + "!";
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
