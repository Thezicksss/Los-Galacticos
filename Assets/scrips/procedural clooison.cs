using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralclooison : MonoBehaviour
{
    [SerializeField] controlik pam;
  void start()
    {
        pam.proceduralInfluence = 0f;
    }
   /* public void OnCollisionEnter()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaa");

        pam.UpdateInfuelnce(0.7f);
    }*/
     void OnTriggerEnter()
    {
        pam.UpdateInfuelnce(0.7f);
    }
    void OnTriggerExit()
    {
        pam.UpdateInfuelnce(0f);
    }
}
