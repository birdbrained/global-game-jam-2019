using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public bool isSelectable;
    public bool wasImagined;
    public bool wasAttacked;

    public SpriteRenderer sr;
    private Rigidbody rb;
    [SerializeField]
    private ParticleSystem deathPS1;
    [SerializeField]
    private ParticleSystem deathPS2;
    [SerializeField]
    public ParticleSystem attackPS { get; private set; }

    public string attackQuip = "<1> damage was dealt to <2>!";
    [SerializeField]
    private bool isRandomEnemy = true;

    TurnOrder Turnmaker;
    private bool canPlayDeathAnimation = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        Turnmaker = FindObjectOfType<TurnOrder>();
        if (isRandomEnemy)
        {
            EnemyCreator.Instance.RandomizeEnemy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.IsDead)
        {
            //play animation here
            //print("He DIED");
            isSelectable = false;
            if (canPlayDeathAnimation)
            {
                canPlayDeathAnimation = false;
                StartCoroutine(AddAFuckTonOfForce());
            }
            //This is NOT how you get you should handle death. Use the queue system established by turn order!
            //Destroy(this);
        }
        if (isSelectable)
        {
            sr.color = Color.yellow;
        }
        else
        {
            sr.color = Color.white;
        }
        if (IsDead)
        {
            sr.color = Color.gray;
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("The mouse is down!");
        if (isSelectable)
        {
            //check if the attack was an imagined one or a normal one!
            //This damage calcuation is a test. actual combat calcuation handled by someone else
            Character character = Turnmaker.currentturn.GetComponent<Character>();
            Debug.LogWarning(character == null);
            int damage = CombatManager.Instance.CalculateDamageAmount(character.attack, defense);
            this.TakeDamage(damage);
            //print(damage + " was dealt to the enemy");
            CombatManager.Instance.logText.text = damage.ToString() + " damage was dealt to " + characterName + "!";
            //CombatManager.Instance.logText.text = FormatAttackQuip(damage, characterName);
            CombatManager.Instance.isAttacking = false;
            Turnmaker.playerturns();
            
        }
    }

    public IEnumerator AddAFuckTonOfForce()
    {
        rb.AddForce(new Vector3(-10f, 7f, 1f), ForceMode.Impulse);
        rb.AddTorque(Vector3.forward * 5);
        if (deathPS1 != null)
        {
            deathPS1.Play();
        }
        if (deathPS2 != null)
        {
            deathPS2.Play();
        }
        yield return new WaitForSeconds(3f);
        //Destroy(gameObject);
    }

    public override void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        //check if dead
    }
}
