using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneRoom_Menu : MonoBehaviour
{
    public GameObject furnitureMenu;
    public GameObject textureMenu;
    public GameObject Menu;
    public GameObject Open_btn;
    public GameObject backToSelect_btn;
    public GameObject Cam_Rotator;
    public GameObject Dynamic_Joystick;
    public GameObject selectRoom;
    public GameObject selectTile;

    void start()
    {

    }

    public void furnitureMenu_btn_clicked()
    {
        Menu.SetActive(false);
        furnitureMenu.SetActive(true);
        textureMenu.SetActive(false);
        backToSelect_btn.SetActive(false);
    }

    public void textureMenu_btn_clicked()
    {
        Menu.SetActive(false);
        furnitureMenu.SetActive(false);
        textureMenu.SetActive(true);
        selectTile.SetActive(false);
        selectRoom.SetActive(true);
        backToSelect_btn.SetActive(false);
    }

    public void back_btn_clicked()
    {
        Menu.SetActive(true);
        furnitureMenu.SetActive(false);
        textureMenu.SetActive(false);
    }

    public void open_btn_clicked()
    {
        Open_btn.SetActive(false);
        backToSelect_btn.SetActive(false);
        Cam_Rotator.SetActive(false);
        Dynamic_Joystick.SetActive(false);
        Menu.SetActive(true);
    }

    public void close_btn_clicked()
    {
        Open_btn.SetActive(true);
        backToSelect_btn.SetActive(true);
        Cam_Rotator.SetActive(true);
        Dynamic_Joystick.SetActive(true);
        Menu.SetActive(false);
        furnitureMenu.SetActive(false);
        textureMenu.SetActive(false);
        selectRoom.SetActive(true);
        selectTile.SetActive(false);
    }

    public void back_to_select_btn_clicked()
    {
        Debug.Log("Go back to select room.");
        SceneManager.LoadScene(1);
    }
}