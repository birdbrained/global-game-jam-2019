using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomBattleBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundObj;
    //private Material backgroundObjMaterial;
    [SerializeField]
    private Material[] backgroundMaterials;

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundObj != null)
        {
            //backgroundObjMaterial = backgroundObj.GetComponent<Material>();
            int rando = Random.Range(0, backgroundMaterials.Length);
            backgroundObj.GetComponent<Renderer>().material = backgroundMaterials[rando];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
