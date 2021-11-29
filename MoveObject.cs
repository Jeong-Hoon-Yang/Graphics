using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform moving_object; // 플레이어의 Transform
    //public GameObject collision;
    public Transform camPivot;  // 플레이어가 몸을 돌리면 camPivot도 같이 회전하기 위해 추가
    public float speed = 20f;   // 이동 속도

    private Joystick controller;
    private void Awake()
    {
        controller = this.GetComponent<Joystick>(); // 조이스틱 불러오기
    }

    private void FixedUpdate()
    {
        Vector3 controllerDir = Vector3.forward * controller.Vertical;  // 세로 방향 조이스틱 이동 시 controllerDir 벡터에 전후 방향 설정
        controllerDir += Vector3.right * controller.Horizontal; // 가로 방향 조이스틱 이동 시 controllerDir 벡터에 좌우 방향 설정

        if (controllerDir == Vector3.zero) return;  // 조이스틱이 가운데로 오면 이동하지 않는다.

        Vector3 conDirAngle = Quaternion.LookRotation(controllerDir).eulerAngles;
        Vector3 camPivotAngle = camPivot.eulerAngles;   // 카메라가 바라보는 방향

        // 카메라가 바라보는 방향(camPivotAngle)을 기준으로 조이스틱이 회전한 각도(conDirAngle)만큼 플레이어는 몸을 돌려야 하므로 두 벡터의 y성분을 더해서 최종 각을 구한다.
        Vector3 moveAngle = Vector3.up * (conDirAngle.y + camPivotAngle.y);

        moving_object.rotation = Quaternion.Euler(moveAngle);   // moveAngle 오일러각을 쿼터니안으로 변형해서 플레이어를 돌려준다.
        moving_object.Translate(Vector3.forward * Time.fixedDeltaTime * speed); // 방향은 앞에서 설정이 되었으니 forward 방향으로 플레이어를 speed값에 맞게 이동변환 시켜준다.
    }

    void Update() {
        //collision.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
