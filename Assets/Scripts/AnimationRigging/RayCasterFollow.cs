using UnityEngine;

public class RayCasterFollow : MonoBehaviour
{
    [SerializeField] private Transform targetObject, stepTarget;
    [SerializeField] private LayerMask whatIsGround;

    private float offset = 0.1052218f;

    private void Update()
    {
        Follow();
        StepPosition();
    }

    private void Follow()
    {
        transform.position = targetObject.position;
    }

    private void StepPosition()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, -transform.up, out hit,0.5f, whatIsGround))
        {
            stepTarget.position = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z);
        }
    }
}
