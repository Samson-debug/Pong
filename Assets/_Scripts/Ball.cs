using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float movementForce;
    
    Rigidbody2D rb;

    Vector3 movementDir;
    
    //Debug
    private bool normalSet;
    private Vector3 normal;
    private Vector3 normalOrigin;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if(rb == null)
            Debug.LogError("Ball has no Rigidbody!");
    }

    private void Start()
    {
        transform.position = Vector2.zero; 
        SetVelocity(Random.insideUnitCircle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug
        normalSet = true;
        normalOrigin = other.contacts[0].point;
        normal = other.contacts[0].normal;
        
        Vector2 reflectedVel = Vector2.Reflect(movementDir, normal);
        SetVelocity(reflectedVel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DeadZone")){
            
        }
    }

    private void SetVelocity(Vector2 _velocity)
    {
        movementDir = _velocity.normalized;
        rb.linearVelocity = movementDir * movementForce;
    }

    private void OnDrawGizmos()
    {
        if(!normalSet) return;
        
        Gizmos.color = Color.red; 
        Gizmos.DrawSphere(normalOrigin, 0.2f);
        Gizmos.DrawRay(normalOrigin, normal * 5f);
    }
}