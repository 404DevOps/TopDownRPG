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
    public Weapon weapon;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;

    //Public Weapon weapon

    //Logic
    public int gold;
    public int experience;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            Destroy(player.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
        //DeleteState();
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (gold >= weaponPrices[weapon.weaponLevel - 1])
        {
            gold -= weaponPrices[weapon.weaponLevel - 1];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public void OnHitpointChange(int hitpoint, int maxHitpoint)
    {
        float ratio = (float)hitpoint / (float)maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //MaxLevel
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            player.OnLevelUp();
            OnHitpointChange(player.hitpoint,player.maxHitpoint);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
        public void SaveState()
    {
        SaveData saveData = new SaveData
        {
            experience = experience,
            gold = gold,
            preferedSkin = 0,
            weaponLevel = weapon.weaponLevel
        };

        PlayerPrefs.SetString("SaveData", saveData.ToString());
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveData")) return;
        
        SaveData saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));

        //SetPlayerSkin
        
        weapon.SetWeaponLevel(saveData.weaponLevel);

        gold = saveData.gold;
        experience = saveData.experience;

        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
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