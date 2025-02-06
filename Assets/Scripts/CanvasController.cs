using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject Main;
    [SerializeField]
    GameObject Settings;
    CameraMouseLook Camera;
    Slider sensivity;
    [SerializeField]
    GameObject LeverHelper;
    [SerializeField]
    GameObject panelWrong;
    [SerializeField]
    TextMeshProUGUI textWrong;
    

    public IEnumerator Wrong(string text)
    {
        panelWrong.SetActive(true);
        textWrong.text = text;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        Camera = GetComponentInParent<CameraMouseLook>();
        
    }
    public void ShowLeverHelper()
    {
        if(Main.activeSelf == Settings.activeSelf)
            LeverHelper.SetActive(true);
    }
    public void CloseLeverHelper()
    {
        LeverHelper.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Settings.activeSelf) 
                ShowCloseMenu(!Main.activeSelf);   
            else CloseSettings();
        } 
    }
    void ShowCloseMenu(bool active)
    {
        Main.SetActive(active);
        Camera.menu = active;
        Cursor.visible = active;
    }

    public void ShowSettings()
    {
        Main.SetActive (false);
        Settings.SetActive(true);
        GetComponentInChildren<Slider>().value = Camera.sensitivity / 10;
    }
    public void CloseSettings()
    {
        Main.SetActive(true);
        Settings.SetActive(false);
    }

    public void ConfirmSettings()
    {
        Camera.sensitivity = GetComponentInChildren<Slider>().value * 10;
        CloseSettings();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
