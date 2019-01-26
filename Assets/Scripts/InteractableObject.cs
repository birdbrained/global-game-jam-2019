using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private string dialogueFile = "Assets/Text/file.txt";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("talk to me plz!");
        }
    }*/

    public void EnableIcon()
    {
        icon.SetActive(true);
    }

    public void DisableIcon()
    {
        icon.SetActive(false);
    }

    public void QueueTextBox()
    {
        TextManager.Instance.EnableTextBox();
        TextManager.Instance.LoadFromFile(dialogueFile);
    }
}
