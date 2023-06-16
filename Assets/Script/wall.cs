using UnityEngine;

public class wall : MonoBehaviour
{

    public Transform object1; // ù ��° ������Ʈ
    public Transform object2; // �� ��° ������Ʈ

    private void Start()
    {

    }
    private void Update()
    {

        float middle_x = (object1.position.x + object2.position.x) / 2;
        float middle_z = (object1.position.z + object2.position.z) / 2;

        transform.position = Vector3.Lerp(transform.position, new Vector3(middle_x, 0, middle_z), 5);

        transform.LookAt(object1);

    }
}
