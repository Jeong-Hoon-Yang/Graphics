using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BP_Test : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;//실체화될 prefab, 미리 넣어야함
    public Material preMat;//실체화될 prefab의 Material, 미리 넣어야함
    public Touch tempTouch;//터치 변수
    Material mat;//마테리얼 변수
    MeshRenderer mesh;//메쉬 변수
    public bool place;//그 자리에 놓을 수 있는지 체크
    bool IsFurniture;//클릭된 것이 나인지 체크하는 변수
    private Vector3 ScreenCenter;//화면 중앙을 가리키는 벡터
    public Vector3 BP_eulerAngles;//체크용
    public Quaternion BP_Quaternian;//체크용
    public bool UIToched;//UI가 터치되었는지 확인하는 변수
    public int LayerMask;//레이어 마스크

    // Start is called before the first frame update
    void Start()//가구 배치 모드일때 이 함수가 불린다
    {
        BP_eulerAngles = gameObject.transform.eulerAngles;
        BP_Quaternian = gameObject.transform.rotation;
        //Debug.Log("BP_eulerAngles = " + BP_eulerAngles);
        //Debug.Log("BP_Quaternian = " + BP_Quaternian);

        gameObject.layer = 10;//BP레이어설정
        mesh = GetComponent<MeshRenderer>();//메쉬값을 불러움
        mat = preMat;

        place = true;
        IsFurniture = false;

        LayerMask = (1 << 0) + (1 << 1) + (1 << 2) + (1 << 6) + (1 << 9);//레이어 마스크 : 0빨래방, 1거실, 2화장실, 6바닥, 9가구

        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);//화면 중앙값
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);//최초 BP생성지는 화면 중간
        if (transform.position == Vector3.zero)//최초 포지션이 (0,0,0) 이면
        {
            if (Physics.Raycast(ray, out hit, 50000.0f, LayerMask))//레이어 마스크에 레이를 쏜다.
            {
                transform.position = hit.point;
            }
            //if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 9)))//레이어 9에 레이를 쏨, 레이어9는 가구.
            //{
            //    transform.position = hit.point;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        BP_eulerAngles = gameObject.transform.eulerAngles;
        if (Input.GetMouseButton(0))
        {
            Debug.Log(Input.mousePosition);
        }
        //터치 시작
        if (Input.touchCount > 0)//터치가 하나 이상이면
        {
            tempTouch = Input.GetTouch(0);//첫번째 터치를 인풋으로 한다
            Ray ray = Camera.main.ScreenPointToRay(tempTouch.position);
            if (tempTouch.position.x > 2640 && tempTouch.position.x < 3120) //G7:2648,3120   IPad:1914,2388
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

            if (tempTouch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 10)))//레이어 10에 레이를 쏨, 레이어10은 BP임
                {
                    if (hit.collider.gameObject == gameObject)//터치된게 나(가구)면 IsFurniture를 참으로 바꿈
                    {
                        IsFurniture = true;
                    }
                }
            }

            if (IsFurniture)
            {
                if (tempTouch.phase == TouchPhase.Moved)
                {
                    if (!UIToched)//터치한 곳이 오브젝트라면
                    {
                        if (Physics.Raycast(ray, out hit, 50000.0f, LayerMask))//터치한 곳이 벽,바닥,천장이거나
                        {
                            transform.position = hit.point;
                        }
                        //if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 9)))//가구이면
                        //{
                        //    transform.position = hit.point;
                        //}
                    }
                }

            }
            if (tempTouch.phase == TouchPhase.Ended)
            {
                IsFurniture = false; 
                UIToched = false;
            }
        }
        //터치 끝
    }

    private void OnTriggerEnter(Collider other)//충돌처리
    {
        if (!other.CompareTag("Ground")) //Ground를 제외한 태그에 닿으면 빨개짐
        {
            mat = mesh.material;
            mat.color = new Color(255, 0, 0);
            place = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            mat = mesh.material;
            mat.color = new Color(255, 0, 0);
            place = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))//Ground만 닿고 있으면 원래대로 돌아옴
        {
            // mat.color = new Color(0, 255, 0);
            Material material = preMat;
            mesh.material = material;
            place = true;
        }
    }

    public void OnMouseDrag()
    {
        //마우스
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (EventSystem.current.IsPointerOverGameObject())//클릭한 곳이 UI라면
        //{
        //    Debug.Log("포인터가 UI에 있다");
        //}
        //else//클릭한 곳이 오브젝트라면
        //{
        //    Debug.Log("포인터가 오브젝트에 있다");
        //    if (Physics.Raycast(ray, out hit, 50000.0f, LayerMask))//레이어 6에 레이를 쏨
        //    {
        //        //Debug.Log(hit.collider.transform.name);
        //        transform.position = hit.point;
        //    }
        //    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 9)))//레이어 9에 레이를 쏨, 레이어9는 가구.
        //    {
        //        transform.position = hit.point;
        //    }
        //}
        //마우스 끝
    }
    public bool retPlace()
    {
        return place;
    }

}
