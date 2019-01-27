using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    public bool player1Created = false;
    public bool player2Created = false;
    public bool player3Created = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string FormatAttackQuip(int damage, string characterName, string baseQuip)
    {
        string quip = baseQuip;
        quip = quip.Replace("<1>", damage.ToString());
        quip = quip.Replace("<2>", characterName);
        return quip;
    }
}
