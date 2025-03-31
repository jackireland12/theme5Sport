using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathScript : MonoBehaviour
{
    public Transform respawnPoint;
    public bool isDead = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        isDead = true;

        // Reset player position and velocity
        transform.position = respawnPoint.position;
        rb.velocity = Vector3.zero;

        isDead = false; // Reset the death state
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death")) // If player crashes into an obstacle
        {
            Die();
        }
    }
}
