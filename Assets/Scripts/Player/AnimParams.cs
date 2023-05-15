using UnityEngine;

[RequireComponent(typeof(SimpleMovement))]
public class AnimParams : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private SimpleMovement _simpleMovement;

    private void Awake()
    {
        _simpleMovement = GetComponent<SimpleMovement>();
    }

    private void Start()
    {
        _simpleMovement.Jumped.AddListener(Jump);
        _simpleMovement.GroundedEvent.AddListener(OnGrounded);
    }

    private void Update()
    {
        SetAnimParameters();
    }

    private void SetAnimParameters()
    {
        anim.SetBool("Grounded", _simpleMovement.Grounded);
        
        if (_simpleMovement.Grounded)
        {
            if (_simpleMovement.Inputs.x != 0)
            {
                anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
            }
            else anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
    }

    private void OnGrounded()
    {
        anim.ResetTrigger("Jump");
    }
}
