using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeFirstScene()
    {
        SceneManager.LoadScene("Start_Scene");
    }

    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
