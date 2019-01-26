using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager instance;
    public static TextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TextManager>();
            }
            return instance;
        }
    }

    public GameObject TextBox;
    public GameObject Text;
    public float textSpeed;
    [SerializeField]
    private MoveableCharacter player;

    Queue<string> stringQueue = new Queue<string>();
    string currentString = "";
    string currentShowString = "";
    float lastUpdate;

    public void LoadFromFile(string file)
    {
        Debug.Log("File time");
        /*if(!File.Exists(Application.dataPath + "/" +  file))
        {
            Debug.Log("File not found" + Application.dataPath + "/" + file);
            return;
        }*/
        //string[] lines = File.ReadAllLines(file);
        StreamReader reader = new StreamReader(file);
        string content = reader.ReadToEnd();
        reader.Close();
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            QueueText(lines[i]);
        }
        DequeueText();
    }

    public void ToggleText()
    {
        TextBox.SetActive(!TextBox.activeSelf);
        //Text.SetActive(!Text.activeSelf);
        lastUpdate = Time.time;
    }

    /**
     * @breif Queue the text onto the string Queue. Does not show the string automatically, Dequeue Text needs to be called for that
     */
    public void QueueText(string text)
    {
        stringQueue.Enqueue(text);
    }

    public void DequeueText()
    {
        if (stringQueue.Count == 0)
        {
            currentString = "";
            Debug.Log("all done with text");
            player.enabled = true;
            //TextBox.SetActive(false);
            ToggleText();
            return;
        }
        currentString = stringQueue.Dequeue();
        currentShowString = "";
    }

    public void SkipTextTime()
    {
        currentShowString = currentString;
    }

    public bool isQueueEmpty()
    {
        return (stringQueue.Count == 0);
    }

    public void EnableTextBox()
    {
        TextBox.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentString != "")
        {
            if (lastUpdate + textSpeed < Time.time)
            {
                if (currentShowString.Length != currentString.Length)
                {
                    currentShowString = currentShowString + currentString[currentShowString.Length];
                    lastUpdate = Time.time;
                }
            }
            Text.GetComponent<Text>().text = currentShowString;
        }
    }
}