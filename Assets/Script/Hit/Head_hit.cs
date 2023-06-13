using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_hit : MonoBehaviour
{
    public Animator ani;
    void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
       
        // Hit_possible -> 플레이어 상태가 bwd 일때
        //if (ani.GetBool("Hit_possible"))
      
            if (collision.collider.CompareTag("Hand_L"))
            {
                ani.SetTrigger("hit_head_L");
            }
            if (collision.collider.CompareTag("Hand_R"))
            {
                ani.SetTrigger("hit_head_R");
            }
        if (collision.collider.CompareTag("Kick_L"))
        {
            ani.SetTrigger("hit_head_L");
        }
        if (collision.collider.CompareTag("Kick_R"))
        {
            ani.SetTrigger("hit_head_R");
        }

    }
   
}
