using UnityEngine;

public class Camera_moving : MonoBehaviour
{
    public Transform object1; // ù ��° ������Ʈ
    public Transform object2; // �� ��° ������Ʈ

    public float cameraSpeed = 5f; // ī�޶� �̵� �ӵ�

    private void Update()
    {

        Vector3 middlepoint = new Vector3(((object1.position.x + object2.position.x) / 2),
            1.5f, ((object1.position.z + object2.position.z) / 2));

        Vector3 targetposition = new Vector3(((object1.position.x + object2.position.x) / 2),
            1.5f, ((object1.position.z + object2.position.z) / 2) + 5); ;

        transform.position = Vector3.Lerp(transform.position, targetposition, cameraSpeed);

        Vector3 cameraPo = middlepoint;

        transform.LookAt(cameraPo);



    }
}


