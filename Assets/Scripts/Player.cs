using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public int damageToDeal;
    [SerializeField]
    public Text characterNameText;
    [SerializeField]
    public Text imaginationText;
    [SerializeField]
    public Text healthText;
    public int playerNum;

    private string baseTextHP;
    private string baseTextIP;
    // Start is called before the first frame update
    void Start()
    {
        switch (playerNum)
        {
            case 1:
                if (GameManager.Instance.player1Created == false)
                {
                    GameManager.Instance.player1Created = true;
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            case 2:
                if (GameManager.Instance.player2Created == false)
                {
                    GameManager.Instance.player2Created = true;
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            default:
                if (GameManager.Instance.player3Created == false)
                {
                    GameManager.Instance.player3Created = true;
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
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
        if (IsDead)
        {
            damageToDeal = 0;
            currHealth = 0;
        }
        if (imaginationText != null)
        {
            imaginationText.text = "IP: " + currIP.ToString();
        }
        if (healthText != null)
        {
            healthText.text =  "HP: " + currHealth.ToString();
        }
        if (characterNameText != null)
        {
            characterNameText.text = characterName;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        Debug.Log("TakeDamage: " + damageAmount.ToString());
        int damageTaken = CombatManager.Instance.CalculateDamageAmount(damageAmount, defense);
        
        if (defending)
        {
            damageTaken=damageTaken / 2;
        }
        if (damageTaken <= 0)
        {
            damageTaken = 1;
        }

        CombatManager.Instance.logText.text = characterName + " took " + damageTaken.ToString() + " damage!";
        damageToDeal += damageTaken;
        Debug.Log("Damage to deal: " + damageToDeal.ToString());
    }
}
