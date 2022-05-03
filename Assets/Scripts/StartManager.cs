using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button continuegameBtn;

    // Start is called before the first frame update
    private void Start()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            continuegameBtn.interactable = false;
        }
    }

    public void ContinueGame()
    {
        LoadMain();   
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        LoadMain();
    }

    public void LoadMain() 
    {
        SceneManager.LoadScene("Main");
    }
}
