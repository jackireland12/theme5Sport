using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickScript : MonoBehaviour
{
    skiMovement sk;
    public float rotateSpeed = 360f;

    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponentInParent<skiMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!sk.IsGrounded())
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

            }
        }
        if(sk.IsGrounded()) { transform.Rotate(0, 0, 0); }
            
        
    }
}
