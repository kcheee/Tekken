using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Maskman_ani_Setting : MonoBehaviour
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
    static public ani_state M_A_T;

    public GameObject Hand_R;
    public GameObject Hand_L;
    public GameObject kick_R;
    public GameObject kick_L;


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
        M_A_T = ani_state.idle;
        ani = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        // idle 일때만 lookat 컴포넌트 켜짐
        if (M_A_T == ani_state.idle)
            La.enabled = true;
        else
            La.enabled = false;

      
        // idle 상황일때 ani_state => idle
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            M_A_T = ani_state.idle;
        }
        // ani.state => sit
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("sit"))
        {
            M_A_T = ani_state.sit;
        }
        // ani.state => fwd
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("fwd"))
        {
            M_A_T = ani_state.forwardstep;
        }
        // ani.state => fwd
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("bwd"))
        {
            M_A_T = ani_state.backstep;
        }


        // 공격 모션일때 콜라이더가 들어있는 게임 오브젝트가 켜짐.
        if (M_A_T == ani_state.H_attack)
        {
            Hand_R.SetActive(true);
            Hand_L.SetActive(true);
        }
        else if (M_A_T == ani_state.K_attack)
        {
            kick_L.SetActive(true);
            kick_R.SetActive(true);

        }
        else
        {
            Hand_R.SetActive(false);
            Hand_L.SetActive(false);
            kick_L.SetActive(false);
            kick_R.SetActive(false);
        }

        // 방어 모션
        if (M_A_T == ani_state.backstep)
            ani.SetBool("Hit_possible", false);

        else
            ani.SetBool("Hit_possible", true);

        // 이동 공격 모션
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (D_flag)
                ani.SetBool("Dash", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            M_A_T = ani_state.forwardstep;
            ani.SetBool("walkfwd", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            D_flag = true;
            StartCoroutine(Delay());
            ani.SetBool("walkfwd", false);
        }
    }
    void WalkBwd()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (B_flag)
            {
                StartCoroutine(delay("BackStep"));
                ani.SetBool("BackStep", true);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            M_A_T = ani_state.backstep;
            ani.SetBool("walkbwd", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            B_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("walkbwd", false);
        }
    }
    void sit()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (S_flag)
                ani.SetBool("siderwd", true);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ani.SetBool("sit", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            S_flag = true;

            StartCoroutine(Delay());
            ani.SetBool("sit", false);
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ani.SetBool("jump", true);
            if (J_flag)
                ani.SetBool("sidelwd", true);
            J_flag = true;
            StartCoroutine(Delay());
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ani.SetBool("jump", false);
        }


    }

    void R_jab()
    {
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ani.SetBool("Jab_R", true);
            M_A_T = ani_state.H_attack;

            StartCoroutine(delay("Jab_R"));
        }
    }
    void L_jab()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            M_A_T = ani_state.H_attack;
            ani.SetBool("Jab_L", true);
            StartCoroutine(delay("Jab_L"));
        }
    }
    void R_kick()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ani.SetBool("Kick_R", true);
            M_A_T = ani_state.K_attack;
            StartCoroutine(delay("Kick_R"));
        }
    }
    void L_kick()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ani.SetBool("Kick_L", true);
            M_A_T = ani_state.K_attack;
            StartCoroutine(delay("Kick_L"));
        }
    }

}
