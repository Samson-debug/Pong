using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Input")]
    public KeyCode upKey;
    public KeyCode downKey;

    [Header("Movement")]
    public float speed = 3f;
    public float collisionSafeDistance = 0.1f;
    public LayerMask collisionMask;

    private float paddleLength;
    Vector3 lastFramePosition;
    Vector3 movementDir;
    
    public Vector3 Velocity { get; private set; }

    //debug
    bool canMove;
    Vector3 hitPoint;
    
    private void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        paddleLength = renderer.bounds.size.y;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        int input = GetInput();
        if (input == 0){
            SetVelocity();
            return;
        }

        movementDir = input == 1 ? Vector3.up : Vector3.down;

        // Start raycast from edge of paddle
        Vector3 rayOrigin = transform.position + movementDir * (paddleLength / 2f);
        RaycastHit2D raycastHit = Physics2D.Raycast(rayOrigin, movementDir, Mathf.Infinity, collisionMask);

        float moveDistance = speed * Time.deltaTime;

        if (raycastHit.collider != null)
        {
            float distanceToWall = raycastHit.distance - collisionSafeDistance;
            // Clamp to non-negative movement (don't go inside wall)
            moveDistance = Mathf.Min(moveDistance, Mathf.Max(0f, distanceToWall));
        }

        transform.position += movementDir * moveDistance;
        
        SetVelocity();
        lastFramePosition = transform.position;

        // Debug
        hitPoint = new Vector3(raycastHit.point.x, raycastHit.point.y, 0);
    }

    public void SetVelocity()
    {
        Velocity = (transform.position - lastFramePosition) / Time.deltaTime;
    }
    
    public int GetInput()
    {
        int input = 0;
        if(Input.GetKey(upKey))
            input = 1;
        if (Input.GetKey(downKey))
            input = -1;
        
        return input;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Velocity);
        Gizmos.color = canMove ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + movementDir *
            (paddleLength / 2f + collisionSafeDistance));
        if(!canMove) Gizmos.DrawSphere(hitPoint, 0.1f);
    }
}