using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //LookAround();
        //Move();
    }

    // 카메라 회전 구현
    public void LookAround(Vector3 inputDirection)
    {
        Vector2 mouseDelta = inputDirection;
        Vector3 camAngle = cameraArm.rotation.eulerAngles;  // cameraArm의 rotation 값을 오일러 값으로 변환
        float x = camAngle.x - mouseDelta.y;
        
        if(x < 180f)    // 시점 회전 각도 제한
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        bool isMove = moveInput.magnitude != 0; // moveInput의 길이를 통해 입력을 여부를 판정하는 변수

        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;  // 바라보는 방향
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;  // 바라보는 방향을 기준으로 이동방향을 구해준다.

            characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * 50f;    // 이동 적용
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }
}
