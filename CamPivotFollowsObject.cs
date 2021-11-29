using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPivotFollowsObject : MonoBehaviour
{
    public Transform following_object;
    
    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;  // CamPivot의 position
        this.transform.position = Vector3.Lerp(pos, following_object.position, 0.4f);   // Lerp함수를 이용해서 카메라가 플레이어를 좀 더 부드럽게 쫓아가도록 함
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
