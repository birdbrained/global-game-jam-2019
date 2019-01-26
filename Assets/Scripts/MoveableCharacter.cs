using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHor * speed, rb.velocity.y, moveVer * speed);
    }
}
