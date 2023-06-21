using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum Gamesetting
    {
        GameStart,
        Ready,
        Loding,
        GameOver,
        KO
    }
    static public Gamesetting Gs;
    static public int a = 2;
    public TextMeshProUGUI time_text;    // �ð� üũ
    public TextMeshProUGUI ready_text;
    public TextMeshProUGUI fight_text;
    public TextMeshProUGUI TimeUp_text;
    public TextMeshProUGUI ko_text;
    public TextMeshProUGUI round1;
    public TextMeshProUGUI[] Round_text;
    public float time = 50;

    // ����
    public AudioClip[] Audioclip;
    AudioSource soundSource;

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(2f);
        Round_text[RoundCheckManager.Check_round].enabled = false; // ���� üũ�Լ� ������
        ready_text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        ready_text.enabled = false;
        fight_text.enabled = true;
        soundSource.clip = Audioclip[3];    // fight ����� Ŭ��
        soundSource.PlayOneShot(Audioclip[3]);
        yield return new WaitForSeconds(0.5f);
        fight_text.enabled = false;
        RoundCheckManager.Check_round++;
        Gs = Gamesetting.GameStart;
    }

    IEnumerator round()
    {
        yield return new WaitForSeconds(0.5f);
        // round sound
        Round_text[RoundCheckManager.Check_round].enabled = true;  // ���� üũ�Լ� ������      
        soundSource.PlayOneShot(Audioclip[RoundCheckManager.Check_round]);
        StartCoroutine(Fight());
    }

    IEnumerator delay(TextMeshProUGUI TMP)
    {
        TMP.enabled = true;
        yield return new WaitForSeconds(3f);
        TMP.enabled = false;
              
        SceneManager.LoadScene(2);
    }
    private void Awake()
    {
        // guard win �¸����
        if (RoundCheckManager.instance.rc1 && RoundCheckManager.instance.rc2)
        {           
            SceneManager.LoadScene(3);
        }
        // maskman win �¸����
        if (RoundCheckManager.instance.rc3 && RoundCheckManager.instance.rc4)
        {
            SceneManager.LoadScene(4);
        }
    }
    private void Start()
    {
        flag = true;
        soundSource = GetComponent<AudioSource>();
        Gs = Gamesetting.Loding;
        StartCoroutine(round());
    }
    bool flag;
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.KeypadEnter))    // ���Ƿ�
        {
            SceneManager.LoadScene(2);
        }
        if (Gs == Gamesetting.GameStart)
        {

            time -= Time.deltaTime;

            int b = (int)time;  //  ����� ����ȯ
            time_text.text = b.ToString();   // ���ڿ��� ����ȯ int -> string
        }

        // time out ��Ȳ��
        if (time <= 0)
        {
            time = 0;
            StartCoroutine(delay(TimeUp_text));
        }

        // Ko �����϶�
        if (M_HP.m_hp.hp <= 0 || G_HP.g_hp.hp <= 0)
        {
            Gs = Gamesetting.Loding;
            if (flag)
            {
                soundSource.PlayOneShot(Audioclip[4]);
                flag = false;
            }
            StartCoroutine(delay(ko_text));
        }


    }
}
