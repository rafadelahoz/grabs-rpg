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

        float exp_taylor3(float x)
        {
            return 1.0f + x + x * x * 0.5f + x * x * x / 6.0f;
        }

        float timeHorizon = 0.018f;
        float expDenominator = exp_taylor3(Time.deltaTime / timeHorizon);
        Vector3 target = player.transform.position;
        transform.position = target + (transform.position - target) / expDenominator;
    }
}
