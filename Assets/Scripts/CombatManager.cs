using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    GameObject currentchar;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public int KnuckleSandwich(int attackersAttack, int defendersDefense,Random rnd, GameObject User)
    {
        int baseDamage = (2 * attackersAttack - defendersDefense);
        int damage25percent = (int)(baseDamage * 0.25f) + Random.Range(1, attackersAttack);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
            User.GetComponent<Character>().currMP -= 10;
        }
        return baseDamage + damage25percent;
    }
    public int RoughHousing(int attackersAttack, int defendersDefense, Random rnd, GameObject User)
    {
        User.GetComponent<Character>().currMP -= 5;
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
        User.GetComponent<Character>().currMP -= 20;
        int Randompercent = Random.Range(10, 25);
        int healamount = (int)(totalhealth * ((Randompercent)/100f));
        return healamount;
    }
    public int Fireball(int attackersAttack, int defendersDefense, GameObject User)
    {
        User.GetComponent<Character>().currMP -= 20;
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
        Target.GetComponent<Character>().defending = true;
        User.GetComponent<Character>().currMP -= 5;
    }
    public int Wallop(int attackersAttack, int defendersDefense, GameObject User)
    {
        User.GetComponent<Character>().currMP -= 10;
        int baseDamage = 2 * attackersAttack - defendersDefense;
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }

        return (int)(((baseDamage + damage25percent)*1.5f));
    }
   
}


