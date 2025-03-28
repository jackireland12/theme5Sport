using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiMovement : MonoBehaviour
{
    
    public float gravityStrength = 20f;
    public float acceleration = 30f;
    public float maxTurnSpeed = 50f;
    public float minTurnSpeed = 10f;
    public float edgeFriction = 5f;
    public float jumpForce = 10f;
    public float maxSpeed = 50f;
    public float groundCheckDistance = 1.5f;
    public LayerMask groundMask;
    public GameObject ramp;
    public float spawnForwardDistance = 10f;
    public float spawnDownwardDistance = 5f;
    // public float rotateSpeed = 360f;


    TrickScript ts;
    private Rigidbody rb;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ts = GetComponentInChildren<TrickScript>();
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            spawnPad();
        }
        //if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    if (!IsGrounded())
        //    {
        //        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        //    }
        //}
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
    public bool IsGrounded()
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
    
    void trick()
    {
        //if (!IsGrounded())
        //{
        //    transform.Rotate(Vector3.left*TimeCount*Time.deltaTime);
            
        //}
    }

    void spawnPad()
    {
        //Vector3 spawnPosition = transform.position + transform.forward * spawnForwardDistance + Vector3.down * spawnDownwardDistance;
        ////Vector3 spawnPosition = transform.position + new Vector3(xx,yy,zz);
        //Quaternion roataion =  Quaternion.LookRotation(spawnPosition, Vector3.up);
        //Instantiate(ramp, spawnPosition, transform.rotation ) ;
        Vector3 spawnPosition = transform.position + transform.forward * spawnForwardDistance;
        RaycastHit hit;
        if (Physics.Raycast(spawnPosition + Vector3.up * 5f, Vector3.down, out hit, 10f))
        {
            spawnPosition = hit.point; // Adjust spawn position to the ground
            Quaternion rampRotation = Quaternion.LookRotation(transform.forward, hit.normal); // Align with terrain
            Instantiate(ramp, spawnPosition, rampRotation);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (ts.attemptedTrick)
            {
                ts.crashCheack();
            }
            
            ts.attemptedTrick = false;
        }
    }


}
