using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToSpawn : MonoBehaviour
{
    private Transform player;
    public Transform spawn;

    void Start()
    {
        player = GetComponent<Transform>();
        player.position = spawn.position;
    }
    
}
