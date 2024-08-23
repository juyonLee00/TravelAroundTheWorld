using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestPointEffect : MonoBehaviour
{
    public float speed = 1f;
    private float maxDistance = 0.5f;

    private Vector3 startPosition;
    private float pingpongValue;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //pingpongValue = Mathf.PingPong(Time.time * speed, maxDistance);
        //transform.position = startPosition + new Vector3(0, pingpongValue, 0);
        transform.position = new Vector3(startPosition.x, startPosition.y+Mathf.PingPong(Time.time * speed, maxDistance), startPosition.z);
    }
}
