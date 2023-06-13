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
    public TextMeshProUGUI timeText;    // �ð� üũ
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
        if (Input.GetKeyUp(KeyCode.KeypadEnter))    // ���Ƿ�
        {
            Gs = Gamesetting.GameStart;
            fightText.enabled = true;
            StartCoroutine(gamestart());
        }
        if (Gs == Gamesetting.GameStart)
        {

            time -= Time.deltaTime;

            int b = (int)time;  //  ����� ����ȯ
            timeText.text = b.ToString();   // ���ڿ��� ����ȯ int -> string
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
