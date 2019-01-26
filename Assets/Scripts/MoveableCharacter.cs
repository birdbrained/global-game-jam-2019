using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private bool facingRight;
    [SerializeField]
    private Animator ani;
    public float MoveHor { get; private set; }
    public float MoveVer { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHor = Input.GetAxis("Horizontal");
        MoveVer = Input.GetAxis("Vertical");

        if (ani != null)
        {
            HandleAnimations(MoveHor, MoveVer);
        }

        rb.velocity = new Vector3(MoveHor * speed, rb.velocity.y, MoveVer * speed);
        ChangeDirection(MoveHor);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            InteractableObject inter = other.gameObject.GetComponent<InteractableObject>();
            if (inter != null)
            {
                inter.EnableIcon();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Talk!");
                inter.QueueTextBox();
                gameObject.GetComponent<MoveableCharacter>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            InteractableObject inter = other.gameObject.GetComponent<InteractableObject>();
            if (inter != null)
            {
                inter.DisableIcon();
            }
        }
    }
}
