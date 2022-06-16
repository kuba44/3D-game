using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Skrypt od poruszania siê gracza
//Krzysiek
public class PlayerController : MonoBehaviour
{
    Rigidbody RB;
    public float Speed;
    
    public bool IsGrounded;
    public LayerMask GroundMask;
    
    float Horizontal;
    float Vertical;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move();
        
    }

    private void move()
    {
        IsGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1.7f, transform.position.z), 0.5f, GroundMask);

        Horizontal = Input.GetAxis("Horizontal") * Speed;
        Vertical = Input.GetAxis("Vertical") * Speed;

        Vector3 MovePosition = transform.right * Horizontal + transform.forward * Vertical;
        Vector3 NewMovePosition = new Vector3(MovePosition.x, RB.velocity.y, MovePosition.z);

        RB.velocity = NewMovePosition;

    }

  
}