/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 15,2021
 * File : FloatingPlatformController.cs
 * Description : This is the script for the Floating and Shrinking Platforms.
 * Revision History : v0.1 > Added Same Behaviour and Mechanics as Moving Platforms
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    [Header("Platform Basic")]
    public Transform start;
    public Transform end;
    public float platformTimer;
    public float threshold;

    public PlayerBehaviour player;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        platformTimer = 0.1f;
        platformTimer = 0;
        distance = end.position - start.position;
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer += Time.deltaTime;
        _Move();    
    }

    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
    }

    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }
}
