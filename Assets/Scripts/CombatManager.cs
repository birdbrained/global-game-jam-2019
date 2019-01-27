using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    //IF DOING TURN ORDER, PULL FROM GIT HUB TO UPDATE THE SCRIPTS!!!!
    //GameObject currentchar;
    public GameObject combatMenu;
    public bool isAttacking;
    private static CombatManager instance;
    private static TurnOrder turnMaster;
    public static CombatManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatManager>();
                turnMaster = FindObjectOfType<TurnOrder>();
            }
            return instance;
        }
    }
    public Text logText;

    // Start is called before the first frame update
    void Start()
    {
        //Should grab and instnatiate the characters in party
        //currentchar = turnMaster.currentturn;
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
    //This section of the Manager is meant mainly for UI! - Dono
    //test function for ui selection
    public void HighlightAllEnemies()
    {
        //Only trigger when its players turn!!!
        //The following below is all broken, but leaving it here in case it is actually useful
        //Debug.Log(turnMaster.currentturn.GetComponent<Character>().characterName);
        /*
        if (turnMaster.currentturn.GetComponent<Character>().characterName == GameObject.Find("PlayerTest").GetComponent<Player>().characterName)
        {
            Debug.Log("its " + currentchar.GetComponent<Player>().characterName + "'s turn!");
        }
        else
        {
            Debug.Log("its someone else's turn");
        }
        */
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            //enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            enemy.isSelectable = true;
        }
        this.isAttacking = true;
    }
    //Disables previous menu and enables a new menu
    public void SwitchImagineMenu(GameObject newMenu){
        GameObject oldMenu = GameObject.Find("Main Combat Menu");
        if(oldMenu == null)
        {
            combatMenu.SetActive(true);
            newMenu.SetActive(false);
        }
        else {
            oldMenu.SetActive(false);
            newMenu.SetActive(true);
        }
     
    }
    //Outline for imagine attacks, and there variants per character
    public void imagineMove()
    {
        //If the current character is a X party member, then ....
    }





    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void ChangeLogText(string s)
    {
        if (logText != null)
        {
            logText.text = s;
        }
    }
   
}


