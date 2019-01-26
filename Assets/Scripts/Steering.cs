using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField]
    private Rigidbody target;
    [SerializeField]
    private int numFrames = 10;

    private GameObject targetObj;
    private Vector3 lastPosition;
    private Queue<Vector3> targetMovement;

    // Start is called before the first frame update
    void Start()
    {
        targetMovement = new Queue<Vector3>();
        targetObj = target.gameObject;
        lastPosition = targetObj.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //lastPosition = player.transform.position;
        if (lastPosition != target.transform.position)
        {
            targetMovement.Enqueue(target.transform.position);
        }
        lastPosition = target.transform.position;

        if (targetMovement.Count > numFrames)
        {
            transform.position = targetMovement.Dequeue();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}
