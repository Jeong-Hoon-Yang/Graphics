using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IA_Test : MonoBehaviour
{
    public GameObject BP;//이 가구의 BP, 미리 넣어야함
    public GameObject myCanvas;//이 가구의 UI, 미리 넣어야함
    public GameObject player;//씬에 있는 Player
    public GameObject RoomMenu;//메인 UI
    public GameObject MenuPanel;//메인 UI의 자식 UI
    public GameObject Open_Button;//열기 버튼
    public GameObject BackToSelect_button;//뒤로가기 버튼
    public GameObject furnitureMenu;//가구메뉴
    public GameObject textureMenu;//텍스쳐메뉴
    public int LayerMask;//레이어 마스크
    public Vector3 scaleVec;
    public bool BP_Mode;
    public bool IA_Mode;
    public bool touchMe;
    public bool UIToched;//UI가 터치되었는지 확인하는 변수
    public Touch tempTouch;
    RaycastHit hit;
    public Vector3 IA_eulerAngles;
    public Quaternion IA_Quaternian;
    public float IA_RotY;
    // Start is called before the first frame update
    void Start()
    {
        IA_eulerAngles = gameObject.transform.eulerAngles;
        IA_Quaternian = gameObject.transform.rotation;
        //Debug.Log("IA_eulerAngles = " + IA_eulerAngles);
        //Debug.Log("IA_Quaternian = " + IA_Quaternian);

        gameObject.layer = 9;//가구레이어
        player = GameObject.Find("Player");
        scaleVec = GameObject.Find("BP_Canvas").GetComponent<BP_Canvas>().BP_ScaleVec;//BP의 ScaleVec를 받아옴
        IA_RotY = GameObject.Find("BP_Canvas").GetComponent<BP_Canvas>().BP_RotY;//BP의 y축값
        RoomMenu = GameObject.Find("Canvas");
        RoomMenu = RoomMenu.transform.GetChild(0).gameObject;
        MenuPanel = RoomMenu.transform.GetChild(3).gameObject;
        furnitureMenu = RoomMenu.transform.GetChild(4).gameObject;
        textureMenu = RoomMenu.transform.GetChild(5).gameObject;
        Open_Button = RoomMenu.transform.GetChild(6).gameObject;
        BackToSelect_button = RoomMenu.transform.GetChild(7).gameObject;
        //Debug.Log("IA_RotY = " + IA_RotY);

        IA_Mode = false;
        touchMe = false;
        gameObject.transform.localScale = scaleVec;
    }

    // Update is called once per frame
    void Update()
    {
        IA_Mode = player.GetComponent<Move>().IA_Mode;
        BP_Mode = player.GetComponent<Move>().BP_Mode;
        //터치 시작
        if (Input.touchCount > 0)//터치가 하나 이상이면
        {
            tempTouch = Input.GetTouch(0);//첫번째 터치를 인풋으로 한다
            Ray ray = Camera.main.ScreenPointToRay(tempTouch.position);

            if (tempTouch.phase == TouchPhase.Began)
            {
                if (furnitureMenu.GetComponent<furnitureMenu>().furnitureChoosed == false)
                {
                    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 9)))//레이어 9에 레이를 쏨, 레이어9는 Furniture임
                    {
                        if (hit.collider.gameObject == gameObject)//터치된게 나(가구)면 touchMe를 참으로 바꿈
                        {
                            touchMe = true;
                        }
                    }
                }

            }
            if (tempTouch.position.x > 2640 && tempTouch.position.x < 3120) //G7:2640,3120   IPad:1914,2388
            {
                if (tempTouch.position.y < 472 && tempTouch.position.y > 0)//472,0     473,0
                {
                    UIToched = true;
                }
                else
                {
                    UIToched = false;
                }
            }

            if (touchMe)
            {
                if (!UIToched)
                {
                    if (tempTouch.phase == TouchPhase.Ended)
                    {
                        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 9)))//레이어 9에 레이를 쏨, 레이어9는 Furniture임
                        {
                            if (MenuPanel.activeSelf == false && furnitureMenu.activeSelf == false && textureMenu.activeSelf == false)
                            {
                                if (BP_Mode == false && IA_Mode == false)
                                {

                                    player.GetComponent<Move>().CanRotate = false;//UI띄우면서 플레이어 회전 멈춤
                                    myCanvas.SetActive(true);
                                    Open_Button.SetActive(false);
                                    BackToSelect_button.SetActive(false);
                                    //IA_Mode = true;
                                    player.GetComponent<Move>().IA_Mode = true;
                                    touchMe = false;

                                }
                            }
                        }
                    }
                }
            }
            if (tempTouch.phase == TouchPhase.Ended)
            {
                touchMe = false;
                UIToched = false;
            }
        }
        //터치 끝
    }

    private void OnMouseEnter()
    {

    }
    //마우스
    //private void OnMouseOver()//본인을 클릭,터치 하면 UI를 띄움
    //{
    //    if (MenuPanel.activeSelf == false && furnitureMenu.activeSelf == false && textureMenu.activeSelf == false)
    //    {
    //        if (Input.GetMouseButtonUp(0) && BP_Mode == false && IA_Mode == false)//아무 UI가 안띄어져 있으면
    //        {
    //            player.GetComponent<Move>().CanRotate = false;//UI띄우면서 플레이어 회전 멈춤
    //            player.GetComponent<Move>().IA_Mode = true;//UI모드 켜짐
    //            myCanvas.SetActive(true);//상호작용 Canvas 켜짐
    //            Open_Button.SetActive(false);//오픈버튼 끔
    //            BackToSelect_button.SetActive(false);//뒤로가기 버튼 끔
    //        }
    //    }
    //}
    //마우스 끝
    private void OnMouseExit()
    {

    }
}
