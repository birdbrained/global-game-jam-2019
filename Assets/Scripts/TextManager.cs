using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;   

public class TextManager : MonoBehaviour
{
    public GameObject TextBox;
    public GameObject Text;
    public float textSpeed;

    Queue<string> stringQueue = new Queue<string>();
    string currentString = "";
    string currentShowString = "";
    float lastUpdate;

    public void LoadFromFile(string file)
    {
        /*if(!File.Exists(Application.dataPath + "/" +  file))
        {
            Debug.Log("File not found" + Application.dataPath + "/" + file);
            return;
        }*/
        //string[] lines = File.ReadAllLines(file);
        StreamReader reader = new StreamReader("Assets/Text/SampleText.txt");
        string content = reader.ReadToEnd();
        string[] lines = content.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            QueueText(lines[i]);
        }
        DequeueText();
    }

    public void ToggleText()
    {
        TextBox.SetActive(!TextBox.activeSelf);
        Text.SetActive(!Text.activeSelf);
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
        if(stringQueue.Count == 0)
        {
            currentString = "";
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentString != "")
        {
            if(lastUpdate + textSpeed < Time.time)
            {
                if(currentShowString.Length != currentString.Length)
                {
                    currentShowString = currentShowString + currentString[currentShowString.Length];
                    lastUpdate = Time.time;
                }
            }
            Text.GetComponent<Text>().text = currentShowString;
        }
    }
}
