using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    public GameObject currentturn;

    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> allentities  = new List<GameObject>();

    public Queue<GameObject> Order = new Queue<GameObject>();

    private int livefriend;
    private int liveenemy;
    private static int count;
    public bool win;
    private bool acted;
    
    private void Start()
    {
        //Takes all characters and enemies on the screen
        
        allentities = chargrab();
        //Assembles a queue for the order in combat
        Order = charOrder(allentities);
        livefriend = counttag("Character", allentities, count);
        liveenemy = counttag("Enemy", allentities, count);
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
                //acted refers to an animation, set to true when animation is finished
                while (acted != true)
                {
                    //wait for animation to finish
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
        else if (liveenemy == 0)
        {
            win = true;
        }
        */
    }
    
    void Update()
    {
        if ((livefriend > 0) && (liveenemy > 0))
        {
            currentturn = Order.Dequeue();
            if (currentturn.GetComponent<Character>().defending == true)
            {
                currentturn.GetComponent<Character>().defending = false;
            }
            if ((currentturn.GetComponent<Character>().IsDead == true) && (currentturn.tag == "Character"))
            {
                Order.Enqueue(currentturn);
            }
            else
            {
                /*
                while (acted != true)
                {
                }
                */
                Order.Enqueue(currentturn);
                acted = false;
                livefriend = counttag("Character", allentities, count);
                liveenemy = counttag("Enemy", allentities, count);
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
        Debug.Log("Did is this the issue?");
        GameObject[] charArray = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject charr in charArray)
        {
            characters.Add(charr);
            allentities.Add(charr);
        }
        Debug.Log("Did is this the issue?: Enemy");
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
            allentities.Add(enemy);
        }
        Debug.Log("Did is this the issue?: Characters");
        return allentities;
    }
    
    public void ActedFinished()
    {
        acted = true;
    }

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
            //fastest = 0;
            Debug.Log(shredlist.Count);
            foreach (GameObject i in shredlist)
            {
                Debug.Log(i.name + " " + i.GetComponent<Character>().agility + " " + fastest);
                if (i.GetComponent<Character>().agility > fastest)
                {
                    Debug.Log("its faster");
                    fastest = i.GetComponent<Character>().agility;
                    currentfastest = i;
                }
                else Debug.Log("not faster")
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
    
}
