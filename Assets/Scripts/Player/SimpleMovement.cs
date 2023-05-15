using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private Transform gCheckOrigin, gCheckOrigin2, gCheckOrigin3;
    [SerializeField] private float gCheckDistance = 0.5f, gMultiplier = 2f, speed = 7f, jumpForce = 30f;
    [SerializeField] private LayerMask whatIsGround;
    
    private Rigidbody rb;
    private float horizontal, vertical;
    private bool grounded, called = true;
    private Vector3 moveDir = Vector3.zero;
    private RaycastHit hit;

    public UnityEvent Jumped, GroundedEvent;

    #region References

    public Vector2 Inputs
    {
        get => new Vector2(horizontal, vertical);
    }

    public bool Grounded
    {
        get => grounded;
    }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = GroundCheck();

        if (grounded && !called)
        {
            called = true;
            GroundedEvent.Invoke();
        }
        else if(!grounded && called)
        {
            called = false;
        }
        
        InputDetection();
        
        JumpDetection();
        
        RotatePlayer();

        ApplyDrag();
        
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region MovementRelated

    private void Move()
    {
        moveDir = transform.forward * Math.Abs(horizontal);
        float angle = 0;
        
        if(hit.collider != null) angle = Vector3.Angle(Vector3.up, hit.normal); 
        
        if (angle != 0)
        {
            Vector3 projected = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
            rb.AddForce(projected * (speed * 12f), ForceMode.Force);
        }
        else
        {
            if(grounded) rb.AddForce(moveDir * (speed * 10f), ForceMode.Force);
            else if(!grounded) rb.AddForce(moveDir * (speed * 10f * 0.4f), ForceMode.Force);
        }
    }
    
    private void Jump()
    {
        Jumped.Invoke();
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    #endregion
    
    #region Utility

    private void InputDetection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void JumpDetection()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
    }
    
    private bool GroundCheck()
    {
        if (Physics.Raycast(gCheckOrigin.position, -gCheckOrigin.up, out hit,gCheckDistance, whatIsGround)) return true;
        if (Physics.Raycast(gCheckOrigin2.position, -gCheckOrigin.up, out hit,gCheckDistance * gMultiplier, whatIsGround)) return true;
        if (Physics.Raycast(gCheckOrigin3.position, -gCheckOrigin.up, out hit,gCheckDistance * gMultiplier, whatIsGround)) return true;

        return false;
    }

    private void RotatePlayer()
    {
        Quaternion rot = new Quaternion();
        if (horizontal > 0)
        {
            rot.eulerAngles = new Vector3(0, 90, 0);
            transform.rotation = rot;
        }
        else if(horizontal < 0)
        {
            rot.eulerAngles = new Vector3(0, -90, 0);
            transform.rotation = rot;
        }
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    
    private void ApplyDrag()
    {
        if (grounded) rb.drag = 2;
        else rb.drag = 0;
    }

    #endregion
    
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gCheckOrigin.position, -gCheckOrigin.up * gCheckDistance);
        Gizmos.DrawRay(gCheckOrigin2.position, -gCheckOrigin2.up * (gCheckDistance*gMultiplier));
        Gizmos.DrawRay(gCheckOrigin3.position, -gCheckOrigin3.up * (gCheckDistance*gMultiplier));
    }

    #endif
}
