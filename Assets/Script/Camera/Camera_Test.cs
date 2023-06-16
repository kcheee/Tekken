using UnityEngine;

public class Camera_Test : MonoBehaviour
{
    public Transform object1; // 첫 번째 오브젝트
    public Transform object2; // 두 번째 오브젝트


    public float cameraSpeed = 5f; // 카메라 이동 속도

    private void Start()
    {
    }
    private void Update()
    {

        float middle_x = (object1.position.x + object2.position.x) / 2;
        float middle_z = (object1.position.z + object2.position.z) / 2;

        // 두 오브젝트의 가운데에 오브젝트를 위치시킨다.
        transform.position = Vector3.Lerp(transform.position, new Vector3(middle_x, 0, middle_z), cameraSpeed);

        transform.LookAt(object1);

        // 회전 값 가져오기.
        Vector3 rotation = transform.rotation.eulerAngles;

        // x 축 회전 값을 원하는 값으로 고정합니다.
        rotation.x = 0;

        // 고정된 회전 값을 다시 적용합니다.
        transform.rotation = Quaternion.Euler(rotation);

    }
}
