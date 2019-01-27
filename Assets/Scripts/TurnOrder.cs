using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder : MonoBehaviour
{
    public GameObject currentturn;

    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> allentities  = new List<GameObject>();

    public GameObject[] livingchars;
    public Queue<GameObject> Order = new Queue<GameObject>();


    public Text currentPlayer;

    private int livefriend;
    private int liveenemy;
    private static int count;
    public bool win;
    private bool playerturn;
    private bool canDrawNewTurn = true;

    public CombatManager combat;
    private static TurnOrder instance;
    public static TurnOrder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TurnOrder>();
            }
            return instance;
        }
    }
    private void Start()
    {
        //Takes all characters and enemies on the screen
        
        allentities = chargrab();
        //Assembles a queue for the order in combat
        Order = charOrder(allentities);
        livefriend = counttag("Character", allentities, count);
        liveenemy = counttag("Enemy", allentities, count);
        livingchars = new GameObject[livefriend];
        currentturn = Order.Dequeue();

            //Remove
            /*
            while ((livefriend > 0) && (liveenemy > 0))
            {
                currentturn = Order.Dequeue();
                if (currentturn.GetComponent<Character>().defending == true)
                {
                    currentturn.GetComponent<Character>().defending = false;
                }
                if ((currentturn.GetComponent<Character>().IsDead == true)&&(currentturn.tag=="Character"))
                {
                    Order.Enqueue(currentturn);
                }
                else {
                    while (acted != true)
                    {
                    }
                    Order.Enqueue(currentturn);
                    acted = false;
                    livefriend = counttag("Character", allentities, count);
                    liveenemy = counttag("Enemy", allentities, count);
                }
            }
            if (livefriend==0)
            {
                win = false;
            }
<<<<<<< HEAD
            else if (liveenemy == 0)
            {
                win = true;
=======
            else {
                //acted refers to an animation, set to true when animation is finished
                while (acted != true)
                {
                    //wait for animation to finish
                }
                Order.Enqueue(currentturn);
                acted = false;
                livefriend = counttag("Character", allentities, count);
                liveenemy = counttag("Enemy", allentities, count);
>>>>>>> 224dd5e7bb8fd66e019b21b1f04137fd414e2244
            }
            */
        }
    
    void Update()
    {
        if ((livefriend > 0) && (liveenemy > 0))
        {
            if (currentturn.tag == "Character" && canDrawNewTurn)
            {
                //Updating UI
                currentPlayer.text = "Current Player: " + currentturn.GetComponent<Character>().characterName;

                playerturn = true;
                canDrawNewTurn = false;
            }
            if (currentturn.GetComponent<Character>().defending == true)
            {
                currentturn.GetComponent<Character>().defending = false;
            }
            if ((currentturn.GetComponent<Character>().IsDead == true) && (currentturn.tag == "Character"))
            {
                Order.Enqueue(currentturn);
                Debug.Log("Character Down");
                currentturn = Order.Dequeue();
            }
            else if ((currentturn.GetComponent<Character>().IsDead == false) && (currentturn.tag == "Enemy") && canDrawNewTurn)
            {
                //Updating UI
                currentPlayer.text = "The enemy strikes!";

                enemyattack(characters);
                Order.Enqueue(currentturn);
                livefriend = counttag("Character", allentities, count);
                liveenemy = counttag("Enemy", allentities, count);
                Debug.Log("Enemy Up");
                currentturn = Order.Dequeue();
            }
            else
            {
                if (playerturn)
                {
                    //Debug.Log("Make your move");
                    
                }
                else
                {
                    Order.Enqueue(currentturn);
                    livefriend = counttag("Character", allentities, count);
                    liveenemy = counttag("Enemy", allentities, count);
                    Debug.Log("Player Moved");
                    currentturn = Order.Dequeue();
                    canDrawNewTurn = true;
                }
            }
        }
        if (livefriend == 0)
        {
            win = false;
        }
        else if (liveenemy == 0)
        {
            win = true;
        }
    }
    
    private List<GameObject> chargrab()
    {
       // Debug.Log("Did is this the issue?");
        GameObject[] charArray = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject charr in charArray)
        {
            characters.Add(charr);
            allentities.Add(charr);
        }
       // Debug.Log("Did is this the issue?: Enemy");
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
            allentities.Add(enemy);
        }
       // Debug.Log("Did is this the issue?: Characters");
        return allentities;
    }
    
    /*public void ActedFinished()
    {
        acted = true;
    }*/

    private Queue<GameObject> charOrder(List<GameObject> allentities)
    {
        int fastest=0;
        List<GameObject> shredlist = new List<GameObject>();
        foreach (GameObject i in allentities)
        {
            shredlist.Add(i);
        }
        
        GameObject currentfastest = null;
        int maxCount = shredlist.Count;
        for (int ii = 0; ii < maxCount; ii++)
        {
            fastest = 0;
            Debug.Log(shredlist.Count);
            foreach (GameObject i in shredlist)
            {
                //Debug.Log(i.name + " " + i.GetComponent<Character>().agility + " " + fastest);
                if (i.GetComponent<Character>().agility > fastest)
                {
                   // Debug.Log("its faster");
                    fastest = i.GetComponent<Character>().agility;
                    currentfastest = i;
                }
               // else Debug.Log("not faster")
;
            }
            Order.Enqueue(currentfastest);
            shredlist.Remove(currentfastest);
        }
        return Order;
    }
    public int counttag(string tag, List<GameObject> all,int count)
    {
        foreach (GameObject i in all)
        {
            if (i.tag == tag)
            {
                count += 1;
            }
        }
        return count;
    }
    public void enemyattack(List<GameObject> characters)
    {
        int counts = 0;
        int j = 0;
        foreach(GameObject i in characters)
        {
            Debug.Log(i.name);
            if (i.GetComponent<Character>().currHealth > 0)
            {
                livingchars[counts] = i;
                counts++;
            }
        }
        int attackvictim = Random.Range(0, counts-1);
        livingchars[attackvictim].GetComponent<Character>().TakeDamage(combat.CalculateDamageAmount(currentturn.GetComponent<Character>().attack, livingchars[attackvictim].GetComponent<Character>().defense));
    }
    public void playerturns()
    {
        playerturn = false;
        canDrawNewTurn = false;
    }
    
}
