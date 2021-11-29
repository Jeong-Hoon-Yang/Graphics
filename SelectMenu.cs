using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickedBackButton()
    {
        Debug.Log("SELECT ROOM");
        SceneManager.LoadScene(0);
    }

    public void OnclickedOneRoomButton1()
    {
        Debug.Log("SELECT ONE ROOM 1");
        SceneManager.LoadScene(2);
    }

    public void OnclickedOneRoomButton2()
    {
        Debug.Log("SELECT ONE ROOM 2");
        SceneManager.LoadScene(3);
    }

    public void OnclickedTwoRoomButton()
    {
        Debug.Log("SELECT TWO ROOM");
        SceneManager.LoadScene(4);
    }
}
