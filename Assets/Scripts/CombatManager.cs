using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
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

    public int CalculateDamageAmount(int attackersAttack, int defenersDefense)
    {
        int baseDamage = 2 * attackersAttack - defenersDefense;
        int damage25percent = (int)(baseDamage * 0.25f);
        if (Random.value < 0.5f)
        {
            damage25percent *= -1;
        }
        return baseDamage + damage25percent;
    }
}
