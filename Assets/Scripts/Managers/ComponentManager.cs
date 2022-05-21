using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentManager : Singleton<ComponentManager>
{

    private UIManager _uiManager;
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private CollectManager _collectManager;
    private PlayerManager _playerManager;

    public Button PlayButton;

    public Button RetryButton;

    public Button WinButton;
    

    public TextMeshProUGUI PlayButtonText;

    public TextMeshProUGUI RetryButtonText;

    public TextMeshProUGUI WinButtonText;

    public TextMeshProUGUI CoinNumberText;

    public TextMeshProUGUI CoinNumberTextOnStartPanel;

    public TextMeshProUGUI LevelNumberTextOnStartPanel;

    public TextMeshProUGUI LifeNumberText;

    public TextMeshProUGUI CollectedTotalCoinTextForWin;

    public TextMeshProUGUI CollectedInThisLevelTextForWin;

    public TextMeshProUGUI CollectedTotalCoinTextForLose;

    public TextMeshProUGUI CollectedInThisLevelTextForLose;

    public TextMeshProUGUI CollectedInThisLevelTextForGame;

    //public TextMeshProUGUI Diamond5SideNumberText;

    public GameObject CoinHolder;

    public bool isCoinHolder;

    public GameObject LevelNumberText;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _gameManager = GameManager.Instance;
        _collectManager = CollectManager.Instance;
        _levelManager = LevelManager.Instance;
        _playerManager = PlayerManager.Instance;
    }

    private void Start()
    {
        PlayButton.onClick.AddListener(() => HandlePlayButton());
        WinButton.onClick.AddListener(() => HandleNextButton());
        RetryButton.onClick.AddListener(() => HandleRetryButton());

        if (isCoinHolder)
        {
            CoinHolder.SetActive(true);
        }
        
    }

    private void Update()
    {
        LifeNumberText.text = _playerManager.PlayerLifeCount.ToString();
        CoinNumberText.text = _collectManager.CollectedCoin.ToString();
        CoinNumberTextOnStartPanel.text = _collectManager.CollectedCoin.ToString();
        LevelNumberText.GetComponent<TextMeshProUGUI>().text = "Level : " + _levelManager.DisplayLevelNumber.ToString();
        LevelNumberTextOnStartPanel.text = "Level : " + _levelManager.DisplayLevelNumber.ToString();
        CollectedTotalCoinTextForWin.text = "TOTAL COIN : " + _collectManager.CollectedCoin.ToString();
        CollectedInThisLevelTextForWin.text = "IN THIS LVL : " + _playerManager.CollectableCountInALevel.ToString();
        CollectedTotalCoinTextForLose.text = "TOTAL COIN : " + (_collectManager.CollectedCoin - _playerManager.CollectableCountInALevel).ToString();
        CollectedInThisLevelTextForLose.text = "IN THIS LVL : " + _playerManager.CollectableCountInALevel.ToString();
        CollectedInThisLevelTextForGame.text = _playerManager.CollectableCountInALevel.ToString();
    }

    #region UI Button Options
    private void HandleNextButton()
    {
        _levelManager.NextLevel();
        _collectManager.ActiveCollectedObject();
        _collectManager.ActiveObstacleObject();
        _gameManager.UpdateGameState(GameState.RestartGame);
    }

    private void HandleRetryButton()
    {
        _collectManager.ActiveCollectedObject();
        _collectManager.ActiveObstacleObject();
        _levelManager.LoadLevel();
        _gameManager.UpdateGameState(GameState.RestartGame);
    }

    private void HandlePlayButton()
    {
        _gameManager.UpdateGameState(GameState.StartGame);
    }
    #endregion
}
