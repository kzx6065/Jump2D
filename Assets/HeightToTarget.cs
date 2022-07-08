using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightToTarget : MonoBehaviour
{
    public Transform target;
    public float offsetY;
    void Start()
    {
        offsetY = target.position.y - transform.position.y;
    }

    void Update()
    {
        var newPos = transform.position;
        newPos.y = target.position.y - offsetY;
        transform.position = newPos;
    }
}
