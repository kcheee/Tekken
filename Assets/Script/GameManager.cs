using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum Gamesetting
    {
        GameStart,
        Ready,
        Loding,
        GameOver
    }
    static public Gamesetting Gs;
    static public int a=2;
    public TextMeshProUGUI timeText;    // 시간 체크
    public TextMeshProUGUI readyText;
    public TextMeshProUGUI fightText;
    public TextMeshProUGUI drawtText;
    public float time = 50;

    IEnumerator Ready()
    {
        yield return new WaitForSeconds(0.5f);
        Gs = Gamesetting.Ready;
        readyText.enabled = true;

    }
    IEnumerator gamestart()
    {
        readyText.enabled = false;
        yield return new WaitForSeconds(0.5f);
        fightText.enabled = false;
    }

    private void Start()
    {
        Gs = Gamesetting.Loding;
        StartCoroutine(Ready());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadEnter))    // 임의로
        {
            Gs = Gamesetting.GameStart;
            fightText.enabled = true;
            StartCoroutine(gamestart());
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
