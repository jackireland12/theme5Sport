using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiMovement : MonoBehaviour
{
    //Rigidbody rb;
    //public float gravityStrength;
    //public float acceleration;
    //public float maxTurnSpeed;

    //public float minTurnSpeed;
    //public float topSpeed;
    //public LayerMask groundMask;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    //void ApplyGravityAndMovement()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, groundMask))
    //    {
    //        Vector3 slopeNormal = hit.normal;
    //        Vector3 gravityDir = -slopeNormal; // Adjust gravity to follow slope
    //        rb.AddForce(gravityDir * gravityStrength, ForceMode.Acceleration);

    //        // Get the slope direction
    //        Vector3 slopeDirection = Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;

    //        // Apply force along slope direction
    //        rb.AddForce(slopeDirection * acceleration, ForceMode.Acceleration);
    //    }
    //}

    //void ApplySlopeMovement()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, groundMask))
    //    {
    //        Vector3 slopeNormal = hit.normal;
    //        Vector3 slopeDirection = Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;

    //        // Apply force to move the player down
    //        rb.AddForce(slopeDirection * acceleration, ForceMode.Acceleration);
    //    }
    //}

    //void Turn(float turnInput)
    //{
    //    float turnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, rb.velocity.magnitude / topSpeed);
    //    transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    //}

    //public float gravityStrength = 20f;
    //public float acceleration = 30f;
    //public float maxTurnSpeed = 50f;
    //public float minTurnSpeed = 10f;
    //public float edgeFriction = 5f;
    //public float jumpForce = 10f;
    //public float groundCheckDistance = 1.5f;
    //public LayerMask groundMask;

    //private Rigidbody rb;
    //private float turnInput;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.freezeRotation = true;
    //}

    //void Update()
    //{
    //    // Get player input
    //    turnInput = Input.GetAxis("Horizontal");

    //    // Jumping
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Jump();
    //    }
    //}
    public float gravityStrength = 20f;
    public float acceleration = 30f;
    public float maxTurnSpeed = 50f;
    public float minTurnSpeed = 10f;
    public float edgeFriction = 5f;
    public float jumpForce = 10f;
    public float maxSpeed = 50f;
    public float groundCheckDistance = 1.5f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get player input
        turnInput = Input.GetAxis("Horizontal");

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        ApplyGravityAndSlope();
        ApplySlopeMovement();
        Turn(turnInput);
        ApplyFriction();
        LimitSpeed();
    }

    void ApplyGravityAndSlope()
    {

        
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundMask))
        {
            Vector3 slopeNormal = hit.normal;
            Vector3 gravityDirection = -slopeNormal; // Pull skier down the slope

            rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
        }
        else
        {
            
            float fallMultiplier = rb.velocity.y < 0 ? 1.5f : 1f;
            rb.AddForce(Vector3.down * gravityStrength * fallMultiplier, ForceMode.Acceleration);
        }
    }

    void ApplySlopeMovement()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundMask))
        {
            Vector3 slopeNormal = hit.normal;
            Vector3 slopeDirection = Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;

            rb.AddForce(slopeDirection * acceleration, ForceMode.Acceleration);
        }
    }

    void Turn(float turnInput)
    {
        float turnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, rb.velocity.magnitude / 50f);
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }

    void ApplyFriction()
    {
        Vector3 lateralVelocity = Vector3.Project(rb.velocity, transform.right);
        rb.AddForce(-lateralVelocity * edgeFriction, ForceMode.Acceleration);
    }
    void LimitSpeed()
    {
        //  Check speed without affecting vertical movement
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            //  Clamp speed while keeping direction
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


}
