using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private static EnemyCreator instance;
    public static EnemyCreator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyCreator>();
            }
            return instance;
        }
    }

    public Sprite[] enemySprites;
    public string[] enemyNames;
    public int[] enemyMaxHPs;
    public int[] enemyMaxIPs;
    public int[] enemyAttacks;
    public int[] enemyDefenses;
    public int[] enemyAgilities;
    public string[] enemyAttackQuips;
    public ParticleSystem[] enemyAttackPSs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeEnemy(Enemy enemy)
    {
        int index = Random.Range(0, enemySprites.Length);
        enemy.sr.sprite = enemySprites[index];
        enemy.characterName = enemyNames[index];
        enemy.maxHealth = enemyMaxHPs[index];
        enemy.currHealth = enemy.maxHealth;
        enemy.maxIP = enemyMaxIPs[index];
        enemy.currIP = enemyMaxIPs[index];
        enemy.attack = enemyAttacks[index];
        enemy.defense = enemyDefenses[index];
        enemy.agility = enemyAgilities[index];
        enemy.attackQuip = enemyAttackQuips[index];
        enemy.attackPS = enemyAttackPSs[index];
    }
}
