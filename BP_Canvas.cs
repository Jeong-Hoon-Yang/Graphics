using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BP_Canvas : MonoBehaviour
{
    public GameObject myMenu;
    public GameObject player;//씬에 있는 player
    public GameObject Menu;//캔버스의 메뉴
    public GameObject parentPrefab;//BP의 프리팹, 구현될 가구
    public GameObject RoomMenu;
    public GameObject Open_Button;//열기버튼
    public GameObject BackToSelect_button;//뒤로가기버튼
    public GameObject parentCode;
    public GameObject Joystick;//조이스틱
    public GameObject rotater;//회전시켜주는거
    
    public Slider RotationSlider;//회전슬라이더
    public Slider ScaleSlider;//크기슬라이더
    public bool place;        //가구를 놓을 수 있는지 체크하는 변수
    public bool furnitureChoosed;
    public Vector3 BP_ScaleVec;//BP의 크기벡터
    //public Vector3 BP_RotaionVec;
    public Vector3 tempVec;//임시값
    public Vector3 IA_Scale;//구현된 가구의 크기벡터
    //public Vector3 IA_Rotation;
    public Vector3 OriginScale;//가구의 원래크기
    public float OriginRotY;//가구의 원래 회전크기
    public float BP_RotY;//BP의 회전값
    public float IA_RotY;//구현된 가구의 회전값
    //public bool BP_On;
    int ScaleCount;
    int ScaleUpLimit;
    int ScaleDownLimit;
    // Start is called before the first frame update
    void Start()
    {
        //BP_On = true;//배치모드 On
        //player찾기
        player = GameObject.Find("Player");//씬의 Player를 찾는다
        player.GetComponent<Move>().BP_Mode = true;//BP_Mode를 true로 바꿈 -> 회전,이동 불가능
        myMenu = GameObject.Find("Canvas");
        RoomMenu = myMenu.transform.GetChild(0).gameObject;
        myMenu = RoomMenu.transform.GetChild(3).gameObject;
        rotater = RoomMenu.transform.GetChild(1).gameObject;
        Joystick = RoomMenu.transform.GetChild(2).gameObject;
        Open_Button = RoomMenu.transform.GetChild(6).gameObject;
        BackToSelect_button = RoomMenu.transform.GetChild(7).gameObject;
        furnitureChoosed = RoomMenu.transform.GetChild(4).gameObject.GetComponent<furnitureMenu>().furnitureChoosed;
        Open_Button.SetActive(false);
        BackToSelect_button.SetActive(false);
        rotater.SetActive(false);
        Joystick.SetActive(false);
        //
        //Debug.Log("BP_Canvas start "+Time.time);
        //ScaleCount = 0;//버튼식 구현
        //ScaleUpLimit = 5;
        //ScaleDownLimit = -5;

        if (null != gameObject.transform.parent.GetComponent<BP_Test>())
        {
            parentPrefab = gameObject.transform.parent.GetComponent<BP_Test>().prefab;
            place = gameObject.transform.parent.GetComponent<BP_Test>().place;
        }
        else if (null != gameObject.transform.parent.GetComponent<BP_Deco_script>())
        {
            parentPrefab = gameObject.transform.parent.GetComponent<BP_Deco_script>().prefab;
            place = gameObject.transform.parent.GetComponent<BP_Deco_script>().place;
        }
        //parentPrefab = gameObject.transform.parent.GetComponent<BP_Test>().prefab;//부모BP의 프리팹

        OriginScale = parentPrefab.transform.localScale;//부모BP의 원래 크기..
        OriginRotY = 180.0f;//부모BP의 원래 Y축값

        BP_ScaleVec = transform.parent.gameObject.transform.localScale;
        BP_RotY = transform.parent.gameObject.transform.eulerAngles.y;

        //Debug.Log(GameObject.Find("IA_Canvas"));
        if (null != GameObject.Find("IA_Canvas"))//구현된 가구를 편집할 때
        {
            IA_Scale = GameObject.Find("IA_Canvas").GetComponent<IA_Canvas>().IA_Scale;//구현된 가구의 스케일벡터
            BP_ScaleVec = IA_Scale;

            IA_RotY = GameObject.Find("IA_Canvas").GetComponent<IA_Canvas>().IA_RotY;
            BP_RotY = IA_RotY;
            //Debug.Log("BP_RotY = " + BP_RotY);
        }

        //Debug.Log(tempVec);        
        tempVec = BP_ScaleVec;//임시값에 저장
        if (tempVec.x != OriginScale.x)//원래 크기랑 다르면
        {
            //Debug.Log(tempVec.x);
            //Debug.Log(OriginScale.x);
            float tempX = tempVec.x / OriginScale.x;
            float tempY = tempVec.y / OriginScale.y;
            float tempZ = tempVec.z / OriginScale.z;            
            BP_ScaleVec.x /= tempX;
            BP_ScaleVec.y /= tempY;
            BP_ScaleVec.z /= tempZ;
            tempX = (tempX - 1) * 10;
            tempY = (tempY - 1) * 10;
            tempZ = (tempZ - 1) * 10;
            ScaleSlider.value += tempX;
            //Debug.Log(ScaleSlider.value);
            //Debug.Log(BP_ScaleVec);
        }

        if (BP_RotY != OriginRotY)//원래 각도랑 다르면
        {
            float tempY = BP_RotY - OriginRotY;
            //Debug.Log("tempY = " + tempY);
            BP_RotY -= tempY;
            //Debug.Log("BP_RotY = " + BP_RotY);
            tempY = tempY / 15;
            RotationSlider.value += tempY;
            //Debug.Log("RotSlider.value = " + RotationSlider.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (null != gameObject.transform.parent.GetComponent<BP_Test>())//충돌 처리를 위해 부모의 place 변수를 계속 받아옴
        {
            place = gameObject.transform.parent.GetComponent<BP_Test>().place;
        }
        else if (null != gameObject.transform.parent.GetComponent<BP_Deco_script>())
        {
            place = gameObject.transform.parent.GetComponent<BP_Deco_script>().place;
        }
        tempVec = BP_ScaleVec;
        transform.parent.gameObject.transform.eulerAngles = new Vector3(-90, 0, RotationSlider.value*15 );//슬라이더가 움직이면 회전함
        BP_RotY = RotationSlider.value * 15;
        tempVec.x *= (ScaleSlider.value / 10);
        tempVec.y *= (ScaleSlider.value / 10);
        tempVec.z *= (ScaleSlider.value / 10);
        transform.parent.gameObject.transform.localScale = tempVec;//슬라이더가 움직이면 크기가 변함
    }

    public void ApplyButton()//배치하기 버튼
    {
        if (place == true)
        {
            BP_ScaleVec = tempVec;
            Instantiate(parentPrefab,
                gameObject.transform.parent.gameObject.transform.position,
                gameObject.transform.parent.gameObject.transform.rotation);//내가 조절한 크기,회전값을 가진 부모의 prefab을 인스턴스화 한다
            //배치모드 off
            RoomMenu.transform.GetChild(4).gameObject.GetComponent<furnitureMenu>().furnitureChoosed = false;
            RoomMenu.SetActive(true);
            myMenu.SetActive(false);
            Open_Button.SetActive(true);
            BackToSelect_button.SetActive(true);
            rotater.SetActive(true);
            Joystick.SetActive(true);
            player.GetComponent<Move>().BP_Mode = false;//플레이어의 배치모드를 false로 바꿈
            player.GetComponent<Move>().Menu_Mode = false;
            Destroy(transform.parent.gameObject);
        }
    }
    public void CancleButton()//삭제하기 버튼
    {
        //배치모드 off
        RoomMenu.transform.GetChild(4).gameObject.GetComponent<furnitureMenu>().furnitureChoosed = false;
        RoomMenu.SetActive(true);
        myMenu.SetActive(false);
        Open_Button.SetActive(true);
        BackToSelect_button.SetActive(true);
        rotater.SetActive(true);
        Joystick.SetActive(true);
        player.GetComponent<Move>().BP_Mode = false;
        player.GetComponent<Move>().Menu_Mode = false;
        Destroy(transform.parent.gameObject);
    }
}
