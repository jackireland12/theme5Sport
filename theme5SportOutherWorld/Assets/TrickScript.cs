using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickScript : MonoBehaviour
{
    skiMovement sk;
    public float rotateSpeed ;
    public float trickRotation = 0f;
    public float forwardRotate;
    public float backRotate;
    public float leftRotate;
    public float rightRotate;
    public float upRotate;
    public float downRotate;
    public float trickScore;
    public bool attemptedTrick;

    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponentInParent<skiMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sk.IsGrounded())
        {
            rotateSpeed = 200 * Time.deltaTime;
            if (Input.GetKey(KeyCode.J))
            {
                transform.Rotate(Vector3.forward * rotateSpeed);
                forwardRotate += rotateSpeed;
                trickRotation += rotateSpeed;
                attemptedTrick = true;
            }
            if (Input.GetKey(KeyCode.L))
            {
                transform.Rotate(Vector3.back * rotateSpeed);
                backRotate += rotateSpeed;
                trickRotation += rotateSpeed;
                attemptedTrick = true;
            }
            if (Input.GetKey(KeyCode.I))
            {
                transform.Rotate(Vector3.right * rotateSpeed);
                rightRotate += rotateSpeed;
                trickRotation += rotateSpeed;
                attemptedTrick = true;
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.Rotate(Vector3.left * rotateSpeed);
                leftRotate += rotateSpeed;
                trickRotation += rotateSpeed;
                attemptedTrick = true;
            }
            if (Input.GetKey(KeyCode.O))
            {
                transform.Rotate(Vector3.up * rotateSpeed);
                upRotate += rotateSpeed;
                trickRotation += (rotateSpeed/3);
                attemptedTrick = true;
            }
            if(Input.GetKey(KeyCode.P))
            {
                transform.Rotate(Vector3.down * rotateSpeed);
                downRotate += rotateSpeed;
                trickRotation += (rotateSpeed/3);
                attemptedTrick = true;
            }
        }

        
        if(sk.IsGrounded()) { transform.Rotate(0, 0, 0); }
            
        
    }
    public int trickCombo = 0;
    public void crashCheack() 
    {
        
        
        // Get player's rotation
        Vector3 rotation = transform.eulerAngles;

        // Convert to -180 to 180 range for better checking
        float tiltX = (rotation.x > 180) ? rotation.x - 360 : rotation.x;
        float tiltZ = (rotation.z > 180) ? rotation.z - 360 : rotation.z;


        // Define a safe landing range
        float maxTilt = 27f;  // Allow slight tilt

        if (Mathf.Abs(tiltX) > maxTilt || Mathf.Abs(tiltZ) > maxTilt)
        {
           Debug.Log("CRASH! You landed too tilted!");
           crash();
                
        }
        else
        {
            Debug.Log("Perfect landing!");
            if (trickRotation >= 720)
            {
                trickCombo++;
                trickScore += (trickRotation * trickCombo);
                
            }
            else
            {
                trickScore += trickRotation;
                
                trickCombo = 0;
            }
            trickRotation = 0;

        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
    void crash()
    {

    }
}
