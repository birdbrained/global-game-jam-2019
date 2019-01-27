using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Description : MonoBehaviour, IPointerEnterHandler
{
    public Text descript;
    public string baseText;
    private bool onButton;
    public void OnPointerEnter(PointerEventData eventData)
    {
        descript.text = "This is a rollover test!";
        onButton = true;
    }
    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
    // Start is called before the first frame update
    void Start()
    {
        baseText = descript.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onButton)
        {
          //  descript.text = baseText;
        }
        //
    }
}
