using UnityEngine;

public class Camera_Test : MonoBehaviour
{
    public Transform object1; // ù ��° ������Ʈ
    public Transform object2; // �� ��° ������Ʈ


    public float cameraSpeed = 5f; // ī�޶� �̵� �ӵ�

    private void Start()
    {
    }
    private void Update()
    {

        float middle_x = (object1.position.x + object2.position.x) / 2;
        float middle_z = (object1.position.z + object2.position.z) / 2;

        // �� ������Ʈ�� ����� ������Ʈ�� ��ġ��Ų��.
        transform.position = Vector3.Lerp(transform.position, new Vector3(middle_x, 0, middle_z), cameraSpeed);

        transform.LookAt(object1);

        // ȸ�� �� ��������.
        Vector3 rotation = transform.rotation.eulerAngles;

        // x �� ȸ�� ���� ���ϴ� ������ �����մϴ�.
        rotation.x = 0;

        // ������ ȸ�� ���� �ٽ� �����մϴ�.
        transform.rotation = Quaternion.Euler(rotation);

    }
}
