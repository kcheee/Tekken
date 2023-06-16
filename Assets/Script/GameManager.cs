using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum Gamesetting
    {
        GameStart,
        Ready,
        Loding,
        GameOver
    }
    static public Gamesetting Gs;
    static public int a = 2;
    public TextMeshProUGUI timeText;    // 시간 체크
    public TextMeshProUGUI round1;
    public TextMeshProUGUI ready;
    public TextMeshProUGUI fight;
    public TextMeshProUGUI drawtText;
    public float time = 50;

    // 사운드
    public AudioClip[] Audioclip;
    AudioSource soundSource;

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(2f);
        round1.enabled = false;
        ready.enabled = true;
        yield return new WaitForSeconds(0.5f);
        ready.enabled = false;
        fight.enabled = true; 
        soundSource.clip = Audioclip[1];
        soundSource.PlayOneShot(Audioclip[1]);
        yield return new WaitForSeconds(0.5f);
        fight.enabled = false;
        Gs = Gamesetting.GameStart;
       
    }

    IEnumerator round()
    {      
        yield return new WaitForSeconds(0.5f);
        // round sound
        round1.enabled = true;
        soundSource.clip = Audioclip[0];
        soundSource.PlayOneShot(Audioclip[0]);
        StartCoroutine(Fight());
    }


    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
        Gs = Gamesetting.Loding;
        StartCoroutine(round());

    }
    float T;
    private void Update()
    {
       
        if (Input.GetKeyUp(KeyCode.KeypadEnter))    // 임의로
        {

            SceneManager.LoadScene(2);
        }
        if (Gs == Gamesetting.GameStart)
        {

            time -= Time.deltaTime;

            int b = (int)time;  //  명시적 형변환
            timeText.text = b.ToString();   // 문자열로 형변환 int -> string
        }
        //if (time <= 0)
        //{
        //    Gs = Gamesetting.GameOver;
        //    drawtText.enabled = true;
        //}
        //void T()
        //{
        //    Debug.Log("gg");
        //}
        //T();


    }
}
