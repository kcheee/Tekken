using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Maskman_ani_Setting : MonoBehaviour
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
        Floating
    }

    public lookat La;

    Animator ani;
    Rigidbody rb;
    static public ani_state M_A_T;
    static public special_state M_S_T;

    // ����
    public AudioClip[] Audioclip;
    AudioSource soundSource;

    public GameObject Hand_R;
    public GameObject Hand_L;
    public GameObject kick_R;
    public GameObject kick_L;

  

    // ���߿� ���ִ��� üũ
    bool isfloating;

    // dash,backstep,sit,jump �÷���
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

    // Guard animation events 
    IEnumerator FloatingColliderdelay()
    {
        yield return new WaitForSeconds(0.4f);
        //�ݶ��̴� ������ ������ ����
        transform.Find("M_Wall_hit").transform.localEulerAngles = new Vector3(-90, 0, 0);
        transform.Find("M_Wall_hit").transform.localPosition = new Vector3(0, 0, 0.3f);
    }

    // floating ������ �� hit
    void floatingHit ()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
        
        ani.SetTrigger("air hit");
    }

    // hand_hit sound
    // �Ӹ� �´� �ִϸ��̼ǿ� ����
    void Head_hit()
    {        // �հ� ��
        if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[1];
            soundSource.PlayOneShot(Audioclip[1]);
            if (M_S_T == Maskman_ani_Setting.special_state.Floating)
            {
                Debug.Log("�Ӹ�");
                floatingHit();
            }
            // ������!         
            if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.upper && 
                M_S_T != Maskman_ani_Setting.special_state.Floating)
            {
                M_S_T = Maskman_ani_Setting.special_state.Floating;
                ani.SetTrigger("Upperhit");
                isfloating = true;
                rb.useGravity = true;
                rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

                // ���� �޺��� ���� ����ũ���� hitcollider�� ȸ����Ű�� ���� ������
                // floating collider 90�� ȸ��
                StartCoroutine(FloatingColliderdelay());
                 //Debug.Log(transform.Find("M_Wall_hit").transform.rotation);
                ani.SetBool("Floating", true);
            }
           

        }
        if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.K_attack)
        {
            Debug.Log(M_A_T);
            if (M_S_T == Maskman_ani_Setting.special_state.Floating)
            {
                Debug.Log("�Ӹ�");
                floatingHit();
            }
            soundSource.clip = Audioclip[4];
            soundSource.PlayOneShot(Audioclip[4]);
        }
    }

    // �ߴ� �´� �ִϸ��̼�
    void Middle_hit()
    {
        if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.H_attack)
        {
            soundSource.clip = Audioclip[2];
            soundSource.PlayOneShot(Audioclip[2]);

            // ���߿� ��������
            if (M_S_T == Maskman_ani_Setting.special_state.Floating)
            {
                Debug.Log("�ߴ�");
                floatingHit();
            }

            // ������! 
            if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.upper &&
                M_S_T != Maskman_ani_Setting.special_state.Floating)
            {
                //Debug.Log("����");
                M_S_T = Maskman_ani_Setting.special_state.Floating;
                Guard_ani_Setting.G_S_T = Guard_ani_Setting.special_state.idle;
                ani.SetTrigger("Upperhit");
                isfloating = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                rb.AddForce(Vector3.up * 7, ForceMode.Impulse);
                

                // floating collider 90�� ȸ��
                StartCoroutine(FloatingColliderdelay());
                //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
                ani.SetBool("Floating", true);
            }

            // ���߿� �� ������
           

        }
        if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.K_attack)
        {
            Debug.Log(M_A_T);
            if (M_S_T == Maskman_ani_Setting.special_state.Floating)
            {
                Debug.Log("�ߴ�");
                floatingHit();
            }
            soundSource.clip = Audioclip[5];
            soundSource.PlayOneShot(Audioclip[5]);
        }
    }

    // �ϴ� �´� �ִϸ��̼�
    void Low_hit()
    {
        Debug.Log("�Ӹ�");
    }

    void Start()
    {
        M_A_T = ani_state.idle;
        ani = gameObject.GetComponent<Animator>();
        soundSource = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public IEnumerator Knockback(float dur, float power)
    {
        float timer = 0f;
        int writeCall = 0;

        /*�˹��� ���� �ذ��ߴ�*/
        /*��𿡵� �̰� �Ⱦ˷��ֳ�...*/
        while (timer <= dur)
        {
            timer += Time.deltaTime;
            writeCall += 1;
            rb.AddRelativeForce(new Vector3(0f, 0, 1.2f * power));
        }
        //Debug.Log(writeCall);
        yield return 0;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            rb.velocity = new Vector3(0,0,0);
            StartCoroutine(Knockback(1f, 2f));
        }

        if (GameManager.Gs == GameManager.Gamesetting.GameStart)
        {
            // idle �϶��� lookat ������Ʈ ����
            if (M_A_T == ani_state.idle ||
            M_A_T == ani_state.sit ||
            M_A_T == ani_state.forwardstep ||
            M_A_T == ani_state.backstep)
                La.enabled = true;
            else if(M_A_T == ani_state.H_attack || M_A_T == ani_state.K_attack)
                La.enabled = false;


            // idle ��Ȳ�϶� ani_state => idle
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                M_A_T = ani_state.idle;
                transform.transform.Find("M_Wall_hit").GetComponent<BoxCollider>().enabled = true;
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
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Lay"))
            {
                M_A_T = ani_state.lay;
                transform.transform.Find("M_Wall_hit").GetComponent<BoxCollider>().enabled = false;

            }

            // ����ũ�� ��Ʈ�ڽ�
            if (M_A_T == ani_state.lay || M_S_T==special_state.Floating)
            {                
                transform.Find("M_Wall_hit").transform.localEulerAngles = new Vector3(-90, 0, 0);
                transform.Find("M_Wall_hit").transform.localPosition = new Vector3(0, 0, 0.3f);
            }
            else
            {
                transform.Find("M_Wall_hit").transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.Find("M_Wall_hit").transform.localPosition = new Vector3(0, 0, 0);
            }

            // ���� ����϶� �ݶ��̴��� ����ִ� ���� ������Ʈ�� ����.
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

            // ��� ���
            if (M_A_T == ani_state.backstep)
                ani.SetBool("Hit_possible", false);

            else
                ani.SetBool("Hit_possible", true);

            // �̵� ���� ���
            WalkFwd();
            WalkBwd();
            sit();
            jump();
            R_jab(); L_jab(); R_kick(); L_kick();
        }
        else
            M_A_T = ani_state.idle;

        // ko ��Ȳ�ϋ� �ݶ��̴� �����ִ� ���� ����
        if (GameManager.Gs == GameManager.Gamesetting.KO)
        {
            Hand_R.SetActive(false);
            Hand_L.SetActive(false);
            kick_L.SetActive(false);
            kick_R.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �ݶ��̴� ���� �����Ѱ� �ʱ�ȭ
            
            isfloating = false;
            ani.SetBool("Floating", false);
            // �ݶ��̴� ������ �����Ѱ� �ʱ�ȭ
            Debug.Log("df");
            
            M_S_T = Maskman_ani_Setting.special_state.idle;

        }
    }


    // �ִϸ��̼� events
    void False_dash()
    {
        ani.SetBool("Dash", false);
    }
    void False_backstep()
    {
        ani.SetBool("BackStep", false);
    }

    // �̵�
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
                ani.SetBool("BackStep", true);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

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

    // ����
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
