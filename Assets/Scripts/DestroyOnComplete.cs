using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnComplete : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] systems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (ParticleSystem ps in systems)
        {
            if (!ps.isPlaying)
            {
                count++;
            }
        }
        if (count == systems.Length)
        {
            Destroy(gameObject);
        }
    }
}
