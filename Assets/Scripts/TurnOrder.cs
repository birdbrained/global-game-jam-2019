using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnOrder : MonoBehaviour
{
    public GameObject currentturn;
    [SerializeField]
    private GameObject playerCommandButtonPanel;

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
    private bool canRunCoro = true;

    [SerializeField]
    private GameObject[] characterIcons = new GameObject[3];

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
    [Header("Player Text Fields")]
    [SerializeField]
    private Text player1NameText;
    [SerializeField]
    private Text player1HealthText;
    [SerializeField]
    private Text player1MagicText;
    [SerializeField]
    private Text player2NameText;
    [SerializeField]
    private Text player2HealthText;
    [SerializeField]
    private Text player2MagicText;
    [SerializeField]
    private Text player3NameText;
    [SerializeField]
    private Text player3HealthText;
    [SerializeField]
    private Text player3MagicText;

    private bool battleOver = false;

    private void Start()
    {
        //Takes all characters and enemies on the screen
        battleOver = false;
        allentities = chargrab();
        SetupPlayerUIStuff();
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
        //Debug.Log("live enemies: " + liveenemy.ToString());
        CheckIfEnemyDead();

        if (currentturn.tag == "Character")
        {
            if (playerCommandButtonPanel != null)
            {
                playerCommandButtonPanel.SetActive(true);
            }
            if (currentturn.name == "Player1")
            {
                characterIcons[0].SetActive(true);
            }
            else
            {
                characterIcons[0].SetActive(false);
            }
            if (currentturn.name == "Player2")
            {
                characterIcons[1].SetActive(true);
            }
            else
            {
                characterIcons[1].SetActive(false);
            }
            if (currentturn.name == "Player3")
            {
                characterIcons[2].SetActive(true);
            }
            else
            {
                characterIcons[2].SetActive(false);
            }
        }
        else
        {
            foreach (GameObject icon in characterIcons)
            {
                icon.SetActive(false);
            }
            if (playerCommandButtonPanel != null)
            {
                playerCommandButtonPanel.SetActive(false);
            }
        }

        while (currentturn.GetComponent<Character>().IsDead && currentturn.tag == "Enemy")
        {
            currentturn = Order.Dequeue();
        }

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
                canDrawNewTurn = true;
                canRunCoro = true;
                currentturn = Order.Dequeue();
            }
            else if ((currentturn.GetComponent<Character>().IsDead == false) && (currentturn.tag == "Enemy") && canDrawNewTurn)
            {
                if (canRunCoro)
                {
                    enemyattack(characters);
                    Order.Enqueue(currentturn);
                    livefriend = counttag("Character", allentities, count);
                    liveenemy = counttag("Enemy", allentities, count);
                    Debug.Log("Enemy Up");
                    currentPlayer.text = "The enemy strikes!";
                    StartCoroutine(waitforflagreset());
                }

                /*enemyattack(characters);
                Order.Enqueue(currentturn);
                livefriend = counttag("Character", allentities, count);
                liveenemy = counttag("Enemy", allentities, count);
                Debug.Log("Enemy Up");
                currentturn = Order.Dequeue();*/

            }
            else
            {
                if (playerturn==true)
                {
                    //Debug.Log("Make your move");
                    CombatManager.Instance.ChangeLogText("It's " + currentturn.GetComponent<Character>().characterName + "'s turn.");
                    if (playerCommandButtonPanel != null)
                    {
                        playerCommandButtonPanel.SetActive(true);
                    }
                }
                else
                {
                    if (playerCommandButtonPanel != null)
                    {
                        playerCommandButtonPanel.SetActive(false);
                    }
                    if (canRunCoro)
                    {
                        Order.Enqueue(currentturn);
                        livefriend = counttag("Character", allentities, count);
                        liveenemy = counttag("Enemy", allentities, count);
                        Debug.Log("Player Moved");
                        StartCoroutine(waitforflagreset());
                    }
                    

                }
            }
            /*if (canDrawNewTurn)
            {
                currentturn = Order.Dequeue();
                
            }*/
        }
        if (livefriend == 0)
        {
            win = false;
            CombatManager.Instance.ChangeLogText("You lost...");
            battleOver = true;
            foreach (GameObject player in characters)
            {
                Character c = player.GetComponent<Character>();
                c.currHealth = c.maxHealth;
                c.currIP = c.maxIP;
            }
            if (playerCommandButtonPanel != null)
            {
                playerCommandButtonPanel.SetActive(false);
            }
            StartCoroutine(TransitionBackToOverworld());
        }
        else if (liveenemy == 0)
        {
            win = true;
            CombatManager.Instance.ChangeLogText("ayy lmao you win!");
            if (playerCommandButtonPanel != null)
            {
                playerCommandButtonPanel.SetActive(false);
            }
            if (battleOver == false)
            {
                //start corotunie to switch back to overworld
                //maybe heal players?
                battleOver = true;
                foreach (GameObject player in characters)
                {
                    Character c = player.GetComponent<Character>();
                    c.currHealth = c.maxHealth;
                    c.currIP = c.maxIP;
                }
                StartCoroutine(TransitionBackToOverworld());
            }
        }
    }

    public void CheckIfEnemyDead()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Character enemy = enemies[i].GetComponent<Character>();
            if (enemy.IsDead)
            {
                enemies.Remove(enemies[i]);
                i--;
            }
        }
    }

    private IEnumerator waitforflagreset()
    {
        canRunCoro = false;
        yield return new WaitForSeconds(3f);
        canDrawNewTurn = true;
        canRunCoro = true;
        currentturn = Order.Dequeue();
    }

    private IEnumerator TransitionBackToOverworld()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Overworld");
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
            if (i.tag == tag && !i.GetComponent<Character>().IsDead)
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
        int attackvictim = Random.Range(0, counts);
        //CHECK if person is not dead, otherwise choose another player
        Character attackee = livingchars[attackvictim].GetComponent<Character>();
        attackee.TakeDamage(currentturn.GetComponent<Character>().attack);/*(combat.CalculateDamageAmount(currentturn.GetComponent<Character>().attack, attackee.defense));*/
    }
    public void playerturns()
    {
        playerturn = false;
    }

    private void SetupPlayerUIStuff()
    {
        Player player1 = GameObject.Find("Player1").GetComponent<Player>();
        Player player2 = GameObject.Find("Player2").GetComponent<Player>();
        Player player3 = GameObject.Find("Player3").GetComponent<Player>();

        player1.characterNameText = player1NameText;
        player1.healthText = player1HealthText;
        player1.imaginationText = player1MagicText;
        player2.characterNameText = player2NameText;
        player2.healthText = player2HealthText;
        player2.imaginationText = player2MagicText;
        player3.characterNameText = player3NameText;
        player3.healthText = player3HealthText;
        player3.imaginationText = player3MagicText;
    }
    
}
