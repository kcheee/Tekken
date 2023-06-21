using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundCheckManager : MonoBehaviour
{ 
    static public RoundCheckManager instance = null;

    public bool rc1 = false;
    public bool rc2 = false;
    public bool rc3 = false;
    public bool rc4 = false;

    static public int Check_round = 0;    // gamemanager���� ���
    
    // Update is called once per frame
    private void Awake()
    {
        
        // Sound Manager �ν��Ͻ��� �̹� �ִ��� Ȯ��, �� ���·� ����
        if (instance == null)
            instance = this;

        // �ν��Ͻ��� �̹� �ִ� ��� ������Ʈ ����
        else if (instance != this)
            Destroy(gameObject);

        // �̷��� �ϸ� ���� scene���� �Ѿ�� ������Ʈ�� ������� �ʽ��ϴ�.
        DontDestroyOnLoad(gameObject);      
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
            gameObject.GetComponent<AudioSource>().enabled = false;
        else
            gameObject.GetComponent<AudioSource>().enabled = true;
    }

}
