using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, -5);

    void LateUpdate()
    {
        // Lock camera position behind player
        transform.position = player.position + player.rotation * offset;

        // Lock camera rotation with player
        transform.rotation = player.rotation;
    }
}
