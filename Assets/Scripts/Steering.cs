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
    private bool facingRight;
    [SerializeField]
    private Animator ani;
    [SerializeField]
    private MoveableCharacter mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        targetMovement = new Queue<Vector3>();
        targetObj = target.gameObject;
        lastPosition = targetObj.transform.position;
        facingRight = true;
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

        if (ani != null)
        {
            HandleAnimations(mainCharacter.MoveHor, mainCharacter.MoveVer);
        }
        ChangeDirection(mainCharacter.MoveHor);
    }

    private void HandleAnimations(float hor, float ver)
    {
        ani.SetFloat("walkSpeed", Mathf.Abs(hor) + Mathf.Abs(ver));

        if (ver > 0)
        {
            ani.SetLayerWeight(1, 1);
        }
        else
        {
            ani.SetLayerWeight(1, 0);
        }
    }

    private void ChangeDirection(float hor)
    {
        if (hor > 0 && !facingRight || hor < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
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
