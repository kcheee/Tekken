using System.Collections;
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
        lay,
        guard,
        H_attack,
        K_attack,
        Hit_hand,
        Hit_kick,
        Upper,
        
    }

    public enum special_state
    {
        idle,
        upper,
        Floating,
        specialattack
    }

    public lookat La;

    // 컴포넌트 가져오기
    Animator ani;
    Rigidbody rb;

    // 애니메이터 상황
    static public ani_state G_A_T;
    static public special_state G_S_T;

    // 사운드
    public AudioClip[] Audioclip;
    AudioSource soundSource;

    // guard는 gameobject가 아니고 collider
    public BoxCollider Hand_R;
    public BoxCollider Hand_L;
    public BoxCollider kick_R;
    public BoxCollider kick_L;

    // 공중에 떠있는지 체크
    bool isfloating;

    // dash,backstep,sit,jump 플래그
    bool D_flag;
    bool B_flag;
    bool S_flag;
    bool J_flag;

    // 딜레이 함수
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

    IEnumerator FloatingColliderdelay()
    {
        yield return new WaitForSeconds(0.4f);
        //콜라이더 각도와 포지션 수정
        transform.Find("G_Wall_hit").transform.localEulerAngles = new Vector3(-90, 0, 0);
        transform.Find("G_Wall_hit").transform.localPosition = new Vector3(0, 0, 0.3f);
    }

    // floating 상태일 때 hit
    void floatingHit()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);

        ani.SetTrigger("air hit");
    }

    // hit 애니메이션, 애니메이션 이벤트로 호출
    // 머리 맞는 애니메이션에 넣음
    void Head_hit()
    {
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[1];
            soundSource.PlayOneShot(Audioclip[1]);

            if (G_S_T == Guard_ani_Setting.special_state.Floating)
            {         
                floatingHit();
            }

            // 어퍼컷!         
            if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.upper &&
                G_S_T != Guard_ani_Setting.special_state.Floating)
            {
                Debug.Log("fdsf");
                G_S_T = Guard_ani_Setting.special_state.Floating;
                ani.SetTrigger("Upperhit");
                isfloating = true;
                rb.useGravity = true;
                rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

                // 공중 콤보를 위한 마스크맨의 hitcollider을 회전시키기 위해 가져옴
                // floating collider 90도 회전
                StartCoroutine(FloatingColliderdelay());
                //Debug.Log(transform.Find("M_Wall_hit").transform.rotation);
                ani.SetBool("Floating", true);
            }

        }
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.K_attack)
        {
            soundSource.clip = Audioclip[4];
            soundSource.PlayOneShot(Audioclip[4]);
          
            if (G_S_T == Guard_ani_Setting.special_state.Floating)
            {
                Debug.Log("머리");
                floatingHit();
            }

        }
    }

    // 중단 맞는 애니메이션
    void Middle_hit()
    {
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[2];
            soundSource.PlayOneShot(Audioclip[2]);

            // 공중에 떠있을때
            if (G_S_T == Guard_ani_Setting.special_state.Floating)
            {
                Debug.Log("중단");
                floatingHit();
            }
            Debug.Log("mid");
            // 어퍼컷! 
            if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.upper &&
                G_S_T != Guard_ani_Setting.special_state.Floating)
            {
                //Debug.Log("실행");
                G_S_T = Guard_ani_Setting.special_state.Floating;
                ani.SetTrigger("Upperhit");
                isfloating = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

                // floating collider 90도 회전
                StartCoroutine(FloatingColliderdelay());
                //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
                ani.SetBool("Floating", true);
            }

        }
        if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.K_attack)
        {
            soundSource.clip = Audioclip[5];
            soundSource.PlayOneShot(Audioclip[5]);

            if (G_S_T == Guard_ani_Setting.special_state.Floating)
            {
                
                floatingHit();
            }
        }
    }

    // 하단 맞는 애니메이션
    void Low_hit()
    {

    }

    void Start()
    {
        G_A_T = ani_state.idle;
        rb = gameObject.GetComponent<Rigidbody>();
        ani = gameObject.GetComponent<Animator>();
        soundSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(G_A_T);
        // idle 일때만 lookat 컴포넌트 켜짐
        if (GameManager.Gs == GameManager.Gamesetting.GameStart)
        {
            if (G_A_T == ani_state.idle ||
                G_A_T == ani_state.sit ||
                G_A_T == ani_state.forwardstep ||
                G_A_T == ani_state.backstep)
                La.enabled = true;
            else
                La.enabled = false;

            // 상대가 공중에 뜰 때 lookat 취소

           

            // idle 상황일때 ani_state => idle
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                G_A_T = ani_state.idle;
                transform.transform.Find("G_Wall_hit").GetComponent<BoxCollider>().enabled = true;
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
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Lay"))
            {
                G_A_T = ani_state.lay;
                transform.transform.Find("G_Wall_hit").GetComponent<BoxCollider>().enabled = false;
            }

           

            // 가드 히트박스
            if (G_A_T == ani_state.lay || G_S_T == special_state.Floating)
            {
                transform.Find("G_Wall_hit").transform.localEulerAngles = new Vector3(-90, 0, 0);
                transform.Find("G_Wall_hit").transform.localPosition = new Vector3(0, 0, 0.3f);
            }
            else
            {
                transform.Find("G_Wall_hit").transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.Find("G_Wall_hit").transform.localPosition = new Vector3(0, 0, 0);
            }

            //Debug.Log(G_A_T);
            // 공격 모션일때 콜라이더가 들어있는 게임 오브젝트가 켜짐.
            // 대쉬하고 손공격할때 콜라이더 켜지는거 수정
            if (G_A_T == ani_state.H_attack && !ani.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {              
                Hand_R.enabled = true;
                Hand_L.enabled = true;
            }
            // 대쉬하고 발공격할때 콜라이더 켜지는거 수정
            else if (G_A_T == ani_state.K_attack&& !ani.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
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
        else
            G_A_T = ani_state.idle;

        // ko 상황일떄 콜라이더 켜져있는 버그 수정
        if(GameManager.Gs == GameManager.Gamesetting.KO)
        {
            Hand_R.enabled = false;
            Hand_L.enabled = false;
            kick_L.enabled=false;
            kick_R.enabled=false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 콜라이더 각도 설정한거 초기화

            isfloating = false;
            ani.SetBool("Floating", false);
            // 콜라이더 포지션 수정한것 초기화
        

            G_S_T = Guard_ani_Setting.special_state.idle;

        }
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


    IEnumerator ExecuteForDuration(float duration, ani_state state)
    {
        float timer = 0f;

        while (timer < duration)
        {
            // 일정 시간 동안 실행될 코드

            G_A_T = state;
            // 시간 업데이트
            timer += Time.deltaTime;

            yield return null;
        }
    }
    // 공격
    void R_jab()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ani.SetBool("Jab_R", true);
            StartCoroutine(ExecuteForDuration(0.2f, ani_state.H_attack));
            StartCoroutine(delay("Jab_R"));
        }
    }
    void L_jab()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(ExecuteForDuration(0.2f, ani_state.H_attack));
            ani.SetBool("Jab_L", true);
            StartCoroutine(delay("Jab_L"));
        }
    }
    void R_kick()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ani.SetBool("Kick_R", true);
            StartCoroutine(ExecuteForDuration(0.2f, ani_state.K_attack));
            StartCoroutine(delay("Kick_R"));
        }
    }
    void L_kick()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ani.SetBool("Kick_L", true);
            StartCoroutine(ExecuteForDuration(0.2f, ani_state.K_attack));
            StartCoroutine(delay("Kick_L"));
        }
    }
}
