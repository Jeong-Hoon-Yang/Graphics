using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCam : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Transform camPivot;  // Transform 컴포넌트를 받아올 객체
    public float rotationSpeed = 0.8f;  // 카메라 회전 속도
    
    public Vector3 beginPos;   // 드래그를 시작할 때 좌표 저장
    public Vector3 draggingPos;    // 드래그 중일 때 좌표 저장
    float x_angle;   // 드래그를 하기 전후의 camPivot의 rotation값을 저장
    float y_angle;
    float x_temp;   // 드래그를 시작할 때의 camPivot의 rotation값을 저장
    float y_temp;

    private void Start(){
        //Debug.Log("start");
        x_angle = camPivot.rotation.eulerAngles.x;   // camPivot의 각 Angle을 초기화 해준다.
        y_angle = camPivot.rotation.eulerAngles.y;
    }

    public void OnBeginDrag(PointerEventData beginPoint)
    {
        //Debug.Log("start drag");
        beginPos = beginPoint.position; // 드래그 시작 시 좌표 저장

        // 드래그를 시작하면 초기화해둔 Angle 값들을 따로 빼둔다.
        x_temp = x_angle;
        y_temp = y_angle;
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        //Debug.Log("dragging...");
        draggingPos = draggingPoint.position;   // 드래그 중일 때의 좌표 저장

        // 드래그 중일 때의 스크린 좌표와 Temp의 스크린좌표의 차이를 통해 변화량을 구하고 camPivot의 Angle에 더해준다.
        y_angle = y_temp + (draggingPos.x - beginPos.x) * 180 / Screen.width * rotationSpeed;
        x_angle = x_temp - (draggingPos.y - beginPos.y) * 90 / Screen.height * rotationSpeed;

        // 위아래 회전 각의 상/하한을 정해줌
        if(x_angle > 60) x_angle = 60;
        if(x_angle < -60) x_angle = -60;

        camPivot.rotation = Quaternion.Euler(x_angle, y_angle, 0.0f);   // 오일러각을 쿼터니언으로 변환
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}





/*
public class RotateCam : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform Camera;

    private Vector2 moverotation;
    private Vector2 FirstPosition;

    public float speed;
    private bool isTouch = false;

    Quaternion pitch;
    Quaternion yaw;

    void Start()
    {
        Camera = GameObject.Find("CamPivot").transform;
        speed = 5f * Time.deltaTime;
    }

    void Update()
    {
        if (isTouch)
        {
            pitch = Quaternion.Euler(-moverotation.y, 0, 0);
            yaw = Quaternion.Euler(0, moverotation.x, 0);

            Camera.localRotation = yaw * Camera.localRotation;
            Camera.localRotation = Camera.localRotation * pitch;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - FirstPosition;
        value = value.normalized;

        moverotation = new Vector2(value.x * 100 + Time.deltaTime, value.y * 100 * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FirstPosition = eventData.position;
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FirstPosition = Vector2.zero;
        moverotation = Vector2.zero;
        isTouch = false;
    }
}
*/