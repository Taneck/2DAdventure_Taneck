using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("Checking Parameters")]
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundLayer; 

    [Header("State")]
    public bool isGround;

    
    private void Update() 
    {
        Check();
    }
    public void Check()
    {
        // Check if the player is grounded
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    } 
}
