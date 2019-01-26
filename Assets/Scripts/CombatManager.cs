using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    //IF DOING TURN ORDER, PULL FROM GIT HUB TO UPDATE THE SCRIPTS!!!!
    GameObject currentchar;
    public bool isAttacking;
    private static CombatManager instance;
    public static CombatManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatManager>();
            }
            return instance;
        }
    }
    public Text logText;

    // Start is called before the first frame update
    void Start()
    {
        //Should grab and instnatiate the characters in party
    }

    // Update is called once per frame
    void Update()
    {
        //Should handle switching characters
        //Should check the status of characters

        //Checks if the player is currently not in the attacking stage
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (this.isAttacking == false)
        {
            foreach (Enemy enemy in enemies)
            {
                //enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                enemy.isSelectable = false;
            }
        }
    }

    public int CalculateDamageAmount(int attackersAttack, int defendersDefense)
    {
        
        int baseDamage = 2 * attackersAttack - defendersDefense;
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }
       
        return baseDamage + damage25percent;
    }
        
    public bool Defend(bool defending)
    {
        return true;
    }
    public bool requiredIP(int ipcost, int charmp)
    {
        if (ipcost - charmp < 0)
            return true;
        else
        {
            return false;
        }
    }
    public int KnuckleSandwich(int attackersAttack, int defendersDefense,Random rnd, GameObject User)
    {
        int ipcost = 10;
        User.GetComponent<Character>().currIP -= ipcost;
        int baseDamage = (2 * attackersAttack - defendersDefense);
        int damage25percent = (int)(baseDamage * 0.25f) + Random.Range(1, attackersAttack);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }
        return baseDamage + damage25percent;
    }
    public int RoughHousing(int attackersAttack, int defendersDefense, Random rnd, GameObject User)
    {
        int ipcost = 5;
        User.GetComponent<Character>().currIP -= ipcost;
        int baseDamage = (2 * attackersAttack - defendersDefense);
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }
        return (int)((baseDamage + damage25percent)*1.5f);
    }
    public int Recover(int totalhealth, GameObject User)
    {
        int ipcost = 20;
        User.GetComponent<Character>().currIP -= ipcost;
        int Randompercent = Random.Range(10, 25);
        int healamount = (int)(totalhealth * ((Randompercent)/100f));
        return healamount;
    }
    public int Fireball(int attackersAttack, int defendersDefense, GameObject User)
    {
        int ipcost = 20;
        User.GetComponent<Character>().currIP -= ipcost;
        int baseDamage = 2 * attackersAttack - defendersDefense;
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }

        return baseDamage + damage25percent;
    }
    public void Protect(GameObject Target, GameObject User)
    {
        int ipcost = 5;
        User.GetComponent<Character>().currIP -= ipcost;
        Target.GetComponent<Character>().defending = true;
    }
    public int Wallop(int attackersAttack, int defendersDefense, GameObject User)
    {
        int ipcost = 10;
        User.GetComponent<Character>().currIP -= ipcost;
        int baseDamage = 2 * attackersAttack - defendersDefense;
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }

        return (int)(((baseDamage + damage25percent)*1.5f));
    }

    //test function for ui selection
    public void HighlightAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            //enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            enemy.isSelectable = true;
        }
        this.isAttacking = true;
    }

    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
    }
   
}


