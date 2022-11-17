using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float zRange;
    private Animator anim;
    Rigidbody rgBody = null;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        rgBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float veritcalInput = Input.GetAxis("Vertical");

        anim.SetInteger("Walk", 0);

        Vector3 movementDirection = new Vector3(horizontalInput, 0, veritcalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("jump", true);
                ySpeed = jumpSpeed;
                
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
    }
    
   
}

