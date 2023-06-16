using Cinemachine;
using UnityEngine;

public class Camera_zoom : MonoBehaviour
{
    public Transform object1; // ù ��° ������Ʈ
    public Transform object2; // �� ��° ������Ʈ

    public float zoomSpeed = 5f; // �� �ӵ�
    public float minDistance = 5f; // �ּ� �Ÿ�
    public float maxDistance = 10f; // �ִ� �Ÿ�
    CinemachineBrain CB;


    private Vector3 initialOffset; // �ʱ� ī�޶� ��ġ�� �� ������Ʈ�� �߽����� ������

    private void Start()
    {
        // �ʱ� ī�޶� ��ġ ����
        CB = gameObject.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if (GameManager.Gs == GameManager.Gamesetting.GameStart)
            CB.enabled = false;
        // �� ������Ʈ�� �߽��� ���
        Vector3 centerPoint = GetCenterPoint();

        // �Ÿ��� ���� �� �ΰ� �� �ƿ�
        float distance = Vector3.Distance(object1.position, object2.position);
        float zoomLevel = Mathf.InverseLerp(minDistance, maxDistance, distance);
        // Field of view View �ٲ�
        float targetFOV = Mathf.Lerp(28f, 70f, zoomLevel);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    private Vector3 GetCenterPoint()
    {
        // �� ������Ʈ�� �߽��� ���
        Vector3 centerPoint = (object1.position + object2.position) / 2f;

        return centerPoint;
    }
}
