using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootPlacement : MonoBehaviour
{
    [SerializeField] private Rig rightFoot, leftFoot;
    [SerializeField] private SimpleMovement _movement;
    
    Animator anim;

    private void Start() {

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_movement.Grounded)
        {
            rightFoot.weight = anim.GetFloat("IKRightFootWeight");
            leftFoot.weight = anim.GetFloat("IKLeftFootWeight");
        }
        else
        {
            rightFoot.weight = 0;
            leftFoot.weight = 0;
        }
    }
}
