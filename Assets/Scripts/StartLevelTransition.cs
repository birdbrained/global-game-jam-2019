using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTransition : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private string nextLevel = "";
    private bool canTrans = true;

    // Start is called before the first frame update
    void Start()
    {
        canTrans = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Transition transition = FindObjectOfType<Transition>();
            if (transition != null && canTrans)
            {
                transition.ExecuteTransition(nextLevel);
                canTrans = false;
            }
        }
    }
}
