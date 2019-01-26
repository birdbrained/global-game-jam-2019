﻿using System.Collections;
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
        
    }

    public void ActedFinished()
    {
        acted = true;
    }

    private GameObject[] chargrab()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
        enemies= GameObject.FindGameObjectsWithTag("Enemy");
        characters.CopyTo(allentities, 0);
        enemies.CopyTo(allentities, characters.Length);
       
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
