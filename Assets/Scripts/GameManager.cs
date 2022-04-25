using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public FloatingTextManager floatingTextManager;

    //Public Weapon weapon

    //Logic
    public int gold;
    public int experience;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
            
        Instance = this;

        DontDestroyOnLoad(this);
        //DeleteState();
        SceneManager.sceneLoaded += LoadState;
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState()
    {
        SaveData saveData = new SaveData
        {
            experience = experience,
            gold = gold,
            preferedSkin = 0,
            weaponLevel = 0
        };

        PlayerPrefs.SetString("SaveData", saveData.ToString());
        Debug.Log("SaveState");
    }

    public void LoadState(Scene scene, LoadSceneMode mode) 
    {
        if (!PlayerPrefs.HasKey("SaveData")) return;
        
        SaveData saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));

        //SetPlayerSkin
        //SetWeaponLevel
        gold = saveData.gold;
        experience = saveData.experience;
        Debug.Log("LoadState");
    }

    public void DeleteState()
    {
        PlayerPrefs.DeleteAll();
    }
}

public class SaveData
{
    public int preferedSkin;
    public int gold;
    public int experience;
    public int weaponLevel;
    public int health;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}