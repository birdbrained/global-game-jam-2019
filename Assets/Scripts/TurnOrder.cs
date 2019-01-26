using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    public GameObject currentturn;

    public GameObject[] characters;
    public GameObject[] enemies;
    public GameObject[] allentities;

    public Queue<GameObject> Order;

    private int livefriend;
    private int liveenemy;
    private static int count;
    private bool acted;
    private void Start()
    {
        allentities = chargrab();
        Order = charOrder(allentities);
        livefriend = counttag("Character", allentities, count);
        liveenemy = counttag("Enemy", allentities, count);
        while ((livefriend > 0) && (liveenemy > 0))
        {
            currentturn = Order.Dequeue();
            
            Order.Enqueue(currentturn);
            acted = false;
        }
        
    }


    private GameObject[] chargrab()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
        enemies= GameObject.FindGameObjectsWithTag("Enemy");
        characters.CopyTo(allentities, 0);
        enemies.CopyTo(allentities, enemies.Length);
       
        return allentities;
    }
    private Queue<GameObject> charOrder(GameObject[] allentities)
    {
        int fastest=0;
        List<GameObject> shredlist = new List<GameObject>();
        foreach (GameObject i in allentities)
        {
            shredlist.Add(i);
        }
        
        GameObject currentfastest = null;
        while (shredlist.Count > 0)
        {
            foreach (GameObject i in shredlist)
            {
                if (i.GetComponent<Character>().agility> fastest)
                {
                    fastest = i.GetComponent<Character>().agility;
                    currentfastest = i;
                }

            }
            Order.Enqueue(currentfastest);
            shredlist.Remove(currentfastest);
        }
        return Order;
    }
    public int counttag(string tag, GameObject[] all,int count)
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
