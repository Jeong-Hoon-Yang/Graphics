using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class Background : MonoBehaviour
{
    public GameObject[] arrGameObjects;
    private Material[] arrMaterials;
 
    //버튼 테스트
    //public Button btn;          //누를때마다 낮밤이 바뀜
    private bool isNight;       //밤인가?
 
 
    //시간 테스트
    //public Text text;        //test 시간 찍어보기 용
    public Text state;       //무슨 상태인가 
    private float elpasedTime;  //경과시간
    private float r = 1f;       //r,g,b 색깔들
    private float g = 1f;
    private float b = 1f;  
 
    public GameObject skyDome;  //빙글빙글 돌 스카이돔
    private Material skyDomeMaterial;   //스카이돔의 메테리얼
    private float offsetValueX = 0;
 
    void Start()
    {
        this.skyDomeMaterial = this.skyDome.GetComponent<Renderer>().material; 
        //스카이돔 메테리얼의 오프셋에 접근하는 방법
        //오프셋에서 접근해서 색을 바꿔줄거임
        this.skyDomeMaterial.SetTextureOffset("_MainTex", new Vector2(this.offsetValueX,0));
 
        this.isNight = false;
        this.arrMaterials = new Material[this.arrGameObjects.Length];
 
        int index1 = 0;
        for (int i = 0; i < this.arrGameObjects.Length; i++)
        {
            index1 = i;
            this.arrMaterials[index1] = this.arrGameObjects[index1].GetComponent<Renderer>().material;
            this.arrMaterials[index1].EnableKeyword("_EMISSION");
            this.arrMaterials[index1].globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
 
        }
 
 /*
        //버튼 테스트
        this.btn.onClick.AddListener(() =>
        {
            int index2 = 0;
 
            if (!this.isNight)
            {
                for (int i = 0; i < this.arrMaterials.Length; i++)
                {
                    index2 = i;
                    this.arrMaterials[index2].SetColor("_EmissionColor", Color.white);  //건물 안 조명키기
                    RenderSettings.ambientLight = Color.black;  //씬 전체 라이트렌더링 어둡게 하기
 
                }
                this.isNight = true;
            }
            else
            {
                for (int i = 0; i < this.arrMaterials.Length; i++)
                {
                    index2 = i;
                    this.arrMaterials[index2].SetColor("_EmissionColor", Color.black);  //건물 안 조명 끄기
                    RenderSettings.ambientLight = Color.white;  //씬 전체 라이트렌더링 밝게 하기
                }
                this.isNight = false;
 
            }
 
        });
        */
 
        //시간테스트 코루틴 시작
        if (!this.isNight)
        {
            this.StartCoroutine(this.DayToNightImpl());
        }
 
    }
 
    //낮 -> 밤
    IEnumerator DayToNightImpl()
    {
        while (true)
        {
            this.elpasedTime += Time.deltaTime;
            //this.text.text = this.elpasedTime.ToString();
            this.state.text = "낮->밤";
            RenderSettings.ambientLight = new Color(this.r, this.g, this.b, 1);
            this.r -= r / 1000;
            this.g -= g / 1000;
            this.b -= b / 1000;
 
            if (this.r <= 0 || this.g <= 0 || this.b <= 0)
            {
                this.r = 0;
                this.g = 0;
                this.b = 0;
            }
 
            this.offsetValueX += 0.05f * Time.deltaTime;
            this.skyDomeMaterial.SetTextureOffset("_MainTex", new Vector2(this.offsetValueX, 0));
 
            if (this.elpasedTime >= 10)
            {
                this.offsetValueX = 0.5f;
                this.elpasedTime = 0;
                //this.text.text = this.elpasedTime.ToString();
                this.StartCoroutine(this.NightImpl());
                break;
            }
 
            yield return null;
        }
  
 
    }
 
    //밤
    IEnumerator NightImpl()
    {
        this.skyDomeMaterial.SetTextureOffset("_MainTex", new Vector2(this.offsetValueX, 0));
 
        for (int i = 0; i < this.arrMaterials.Length; i++)
        {
            this.arrMaterials[i].SetColor("_EmissionColor", Color.white);
 
        }
 
        while (true)
        {
            this.elpasedTime += Time.deltaTime;
            //this.text.text = this.elpasedTime.ToString();
            this.state.text = "밤";
            
 
            if (this.elpasedTime>=10)
            {
                this.elpasedTime = 0;
                this.StartCoroutine(this.NightToDayImpl());
                break;
            }
 
            yield return null;
        }
 
    }
 
    //밤 -> 낮
    IEnumerator NightToDayImpl()
    {
        
 
        while (true)
        {
            this.elpasedTime += Time.deltaTime;
            //this.text.text = this.elpasedTime.ToString();
            this.state.text = "밤->낮";
            RenderSettings.ambientLight = new Color(this.r, this.g, this.b, 1);
            this.r += r / 1000;
            this.g += g / 1000;
            this.b += b / 1000;
 
            if (this.r >= 1 || this.g >= 1 || this.b >= 1)
            {
                this.r = 1;
                this.g = 1;
                this.b = 1;
            }
 
            this.offsetValueX -= 0.05f*Time.deltaTime;
            this.skyDomeMaterial.SetTextureOffset("_MainTex", new Vector2(this.offsetValueX, 0));
 
            if (this.elpasedTime >= 10)
            {
                this.offsetValueX = 0;
                this.elpasedTime = 0;
                //this.text.text = this.elpasedTime.ToString();
                this.StartCoroutine(this.DayImpl());
                break;
            }
 
            yield return null;
        }
        
 
    }
 
    //낮
    IEnumerator DayImpl()
    {
        this.skyDomeMaterial.SetTextureOffset("_MainTex", new Vector2(this.offsetValueX, 0));
 
 
        for (int i = 0; i < this.arrMaterials.Length; i++)
        {
            this.arrMaterials[i].SetColor("_EmissionColor", Color.black);
        }
 
        while (true)
        {
            this.elpasedTime += Time.deltaTime;
            //this.text.text = this.elpasedTime.ToString();
            this.state.text = "낮";
 
 
            if (this.elpasedTime >= 10)
            {
                this.StopAllCoroutines();
                this.elpasedTime = 0;
                this.StartCoroutine(this.DayToNightImpl());
                break;
            }
 
            yield return null;
        }
 
        
    }
 
    private void Update()
    {
        //스카이돔 돌아라
        this.skyDome.transform.Rotate(Vector3.up* 3* Time.deltaTime);
    }
}