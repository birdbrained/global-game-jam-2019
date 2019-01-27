using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackID
{
    BasicAttack = 0,
    KnuckleSandwich = 1,
    RoughHousing = 2,
    Recover = 3,
    Fireball = 4,
    Protect = 5,
    Wallop = 6
}

public class Attack : MonoBehaviour
{
    [SerializeField]
    protected string attackName = "My Attack";
    [SerializeField]
    protected string attackLaunchString = "<1> attacks!";
    [SerializeField]
    protected AttackID attackID;
    [SerializeField]
    private ParticleSystem particleSys;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string FormattedAttackLaunchString(string owner)
    {
        string newString = attackLaunchString;
        newString = newString.Replace("<1>", owner);
        return newString;
    }

    public void PerformAttack()
    {

    }
}
