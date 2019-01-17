using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        if (!player)
            return;

        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.6f);
    }
}
