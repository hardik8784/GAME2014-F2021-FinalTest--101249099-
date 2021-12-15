/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 15,2021
 * File : FloatingPlatformController.cs
 * Description : This is the script for the Floating and Shrinking Platforms.
 * Revision History : v0.1 > Added Same Behaviour and Mechanics as Moving Platforms
 *                    v0.2 > Added Shrinking and Expanding Functionality implemented with Player's Events
 *                    v0.3 > Check Player Collides with the Platform then Shrink otherwise Expand,if not both then Float
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

    [Header("Shrinking Behaviour of Platform")]
    [SerializeField] private float ShrinkTimeElapsed;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float ShrinkTime;
    [SerializeField] private Vector3 Platform_Position;
  
    /// <summary>
    /// Scaling properties for the Platform
    /// </summary>
    private Vector3 TempScale;
    private Vector2 BoxColliderScale;

    [Range(0.0f, 1.0f)]
    [SerializeField] float ScalingFactor;
    private BoxCollider2D BoxCollider;

    [Header("Platform Behaviour")]
    [SerializeField] private bool isActive;
    [SerializeField] private bool isExpanding;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        BoxCollider = GetComponent<BoxCollider2D>();
        Platform_Position = transform.position;
        BoxColliderScale = BoxCollider.size;
        platformTimer = 0.1f;
        platformTimer = 0;
        isActive = false;
        isExpanding = false;
        distance = end.position - start.position;
        ShrinkTimeElapsed = ShrinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            if (!isExpanding)
            {
                platformTimer += Time.deltaTime;
                Float();
            }
            else
            {
                Expand();
            }
        }
        else
        {
            Shrink();
        }
    }

    /// <summary>
    /// The Platform will Shrink if Player is on it
    /// </summary>
    void Shrink()
    {
        ShrinkTimeElapsed -= Time.deltaTime;
        ScalingFactor = ShrinkTimeElapsed / ShrinkTime;

        TempScale = transform.localScale;
        TempScale.x = ScalingFactor;
        TempScale.y = ScalingFactor;
        transform.localScale = TempScale;

        Vector2 NewBoxScale = BoxColliderScale * ScalingFactor;
        BoxCollider.size = NewBoxScale;

    }

    /// <summary>
    /// The Platform will Expand if the Player is not on it.
    /// </summary>
    void Expand()
    {
        ShrinkTimeElapsed += Time.deltaTime;
        ScalingFactor = ShrinkTimeElapsed / ShrinkTime;
        transform.position = start.transform.position;          //Platform Expands from it's original position,Not somewhere else

        TempScale = transform.localScale;
        TempScale.x = ScalingFactor;
        TempScale.y = ScalingFactor;
        transform.localScale = TempScale;
       
        Vector2 NewBoxScale = BoxColliderScale * ScalingFactor;
        BoxCollider.size = NewBoxScale;

        if (ScalingFactor >= 1.0f)
        {
            ScalingFactor = 1.0f;
            isExpanding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  //Check the Player and Platform Collision
    {

        if (player != null)
        {
            isActive = true;
            isExpanding = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)   ////Check the Player move somewhere else from Platform 
    {
        isActive = false;
        isExpanding = true;

    }


    private void Float()
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
