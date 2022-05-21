using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    private CollectManager _collectManager;
    private GameManager _gameManager;

    public int LevelNumber;

    [HideInInspector]
    public int MaxLevel;

    public int DisplayLevelNumber = 1;
    private int _coin;
    //private int _diamond5side;

    public GameObject LevelHolder;
    [HideInInspector]
    public GameObject[] SpawnedLevels;

    public GameObject[] Levels;

    
    
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _collectManager = CollectManager.Instance;
        MaxLevel = Levels.Length - 1;
    }

    void Start()
    {
        LoadLevel();
        NextLevelCall();
    }

    public void NextLevel()
    {
        if (LevelNumber == MaxLevel)
        {
            LevelNumber = 0;
        }
        else
        {
            LevelNumber++;
        }
        DisplayLevelNumber++;
        SaveLevel();
        NextLevelCall();
    }

    public void NextLevelCall()
    {
        SpawnedLevels = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < SpawnedLevels.Length; i++)
        {
            Destroy(SpawnedLevels[i].gameObject);
        }

        Instantiate(Levels[LevelNumber].gameObject, LevelHolder.transform);

    }

    public void RestartLevel()
    {
        SpawnedLevels = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < SpawnedLevels.Length; i++)
        {
            Destroy(SpawnedLevels[i].gameObject);
        }
        Instantiate(Levels[LevelNumber].gameObject, LevelHolder.transform);
    }


    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        PlayerPrefs.SetInt("LevelNumber", DisplayLevelNumber);
        PlayerPrefs.SetInt("Coin", _collectManager.CollectedCoin);

        //PlayerPrefs.SetInt("Diamond5Side", _collectManager.CollectedDiamond5Side);
    }

    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("Diamond"))
        {
            _coin = PlayerPrefs.GetInt("Coin");
            _collectManager.CollectedCoin = _coin;
        }

        /*if (PlayerPrefs.HasKey("Diamond5Side"))
        {
            _diamond5side = PlayerPrefs.GetInt("Diamond5Side");
            _collectManager.CollectedDiamond5Side = _diamond5side;
        }*/
        if (PlayerPrefs.HasKey("Level"))
        {
            LevelNumber = PlayerPrefs.GetInt("Level");
        }
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            DisplayLevelNumber = PlayerPrefs.GetInt("LevelNumber");
        }

        if (DisplayLevelNumber > 1)
        {
            Time.timeScale = 1;
        }
    }
}
