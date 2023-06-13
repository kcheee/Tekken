using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Guard_ani_Setting : MonoBehaviour
{
    public enum ani_state
    {
        idle,
        forwardstep,
        backstep,
        sit,
        jump,
        H_attack,
        K_attack,
    }

    public lookat La;

    Animator ani;
    static public ani_state G_A_T;

    // guard는 gameobject가 아니고 collider
    public BoxCollider Hand_R;
    public BoxCollider Hand_L;
    public BoxCollider kick_R;
    public BoxCollider kick_L;

    // dash,backstep,sit,jump 플래그
    bool D_flag;
    bool B_flag;
    bool S_flag;
    bool J_flag;

    IEnumerator delay(string S)
    {
        yield return new WaitForSeconds(0.2f);
        ani.SetBool(S, false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        D_flag = false;
        B_flag = false;
        J_flag = false;
        S_flag = false;
    }
    void Start()
    {
        G_A_T = ani_state.idle;
        ani = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        // idle 일때만 lookat 컴포넌트 켜짐
        if (G_A_T == ani_state.idle)
            La.enabled = true;
        else
            La.enabled = false;

        // idle 상황일때 ani_state => idle
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            G_A_T = ani_state.idle;
        }
        // ani.state => sit
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("sit"))
        {
            G_A_T = ani_state.sit;
        }
        // ani.state => fwd
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("fwd"))
        {
            G_A_T = ani_state.forwardstep;
        }
        // ani.state => fwd
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("bwd"))
        {
            G_A_T = ani_state.backstep;
        }

        //Debug.Log(G_A_T);
        // 공격 모션일때 콜라이더가 들어있는 게임 오브젝트가 켜짐.
        if (G_A_T == ani_state.H_attack)
        {
            Hand_R.enabled = true;
            Hand_L.enabled = true;
        }
        else if (G_A_T == ani_state.K_attack)
        {
            kick_L.enabled = true;
            kick_R.enabled = true;
        }
        else
        {
            Hand_R.enabled = false;
            Hand_L.enabled = false;
            kick_L.enabled = false;
            kick_R.enabled = false;

        }


        // 방어 모션
        if (G_A_T == ani_state.backstep)
            ani.SetBool("Hit_possible", false);

        else
            ani.SetBool("Hit_possible", true);


        WalkFwd();
        WalkBwd();
        sit();
        jump();
        R_jab(); L_jab(); R_kick(); L_kick();
    }

    // 애니메이션 events
    void False_dash()
    {
        ani.SetBool("Dash", false);
    }
    void False_backstep()
    {
        ani.SetBool("BackStep", false);
    }
    void WalkFwd()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (D_flag)
                ani.SetBool("Dash", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            G_A_T = ani_state.forwardstep;
            ani.SetBool("walkfwd", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            D_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("walkfwd", false);
        }
    }
    void WalkBwd()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (B_flag)
                ani.SetBool("BackStep", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            G_A_T = ani_state.backstep;
            ani.SetBool("walkbwd", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            B_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("walkbwd", false);
        }
    }
    void sit()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (S_flag)
                ani.SetBool("siderwd", true);
        }
        if (Input.GetKey(KeyCode.S))
        {

            ani.SetBool("sit", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            S_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("sit", false);
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("jump", true);
            if (J_flag)
                ani.SetBool("sidelwd", true);
            J_flag = true;
            StartCoroutine(Delay());
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ani.SetBool("jump", false);
        }


    }
    void R_jab()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            G_A_T = ani_state.H_attack;
            ani.SetBool("Jab_R", true);
            StartCoroutine(delay("Jab_R"));
        }
    }
    void L_jab()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            G_A_T = ani_state.H_attack;
            ani.SetBool("Jab_L", true);
            StartCoroutine(delay("Jab_L"));
        }
    }
    void R_kick()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            G_A_T = ani_state.K_attack;
            ani.SetBool("Kick_R", true);
            StartCoroutine(delay("Kick_R"));
        }
    }
    void L_kick()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            G_A_T = ani_state.K_attack;
            ani.SetBool("Kick_L", true);
            StartCoroutine(delay("Kick_L"));
        }
    }

}
