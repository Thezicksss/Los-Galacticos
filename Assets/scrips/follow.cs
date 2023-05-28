using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] GameObject targ;
    // Start is called before the first frame update
 
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, .03f);
    }

}
