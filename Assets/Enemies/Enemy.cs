using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHP = 100f;

    private float hp = 100f;

    public float healthAsPercentage
    {
        get
        {
            return (hp / maxHP);
        }
    }
}
