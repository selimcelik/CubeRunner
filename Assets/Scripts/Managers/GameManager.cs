using System;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GameState State;
    [HideInInspector]
    public TimeState StateTime;
    private UIManager _uiManager;
    private PlayerManager _playerManager;
    private CollectManager _collectManager;
    private ComponentManager _componentManager;
    private CameraFollow _cameraFollow;
    private ObjectPooler _objectPooler;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _uiManager = UIManager.Instance;
        _playerManager = PlayerManager.Instance;
        _collectManager = CollectManager.Instance;
        _componentManager = ComponentManager.Instance;
        _cameraFollow = CameraFollow.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    private void Start()
    {
        UpdateGameState(GameState.WaitGame);
    }

    #region Game State Options
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.WinGame:
                handleWinGame();
            break;
            case GameState.LoseGame:
                handleLoseGame();
            break;
            case GameState.StartGame:
                handleStartGame();
            break;
            case GameState.WaitGame:
                handleWaitGame();
            break;
            case GameState.RestartGame:
                handleRestartGame();
            break;
        }
    }

    private void handleWaitGame()
    {
        _cameraFollow.offset.x = 0;
        _cameraFollow.offset.y = 5f;
        _cameraFollow.offset.z = -7.22f;
        _cameraFollow.smoothSpeed = 0.125f;
        _uiManager.UpdatePanelState(PanelCode.StartPanel, true);
        _playerManager.StopPlayer();
    }
    private void handleStartGame()
    {
        _cameraFollow.offset.x = 0;
        _cameraFollow.offset.y = 5f;
        _cameraFollow.offset.z = -7.22f;
        _cameraFollow.smoothSpeed = 0.125f;
        _objectPooler.CreatePoolObjects();
        _uiManager.UpdatePanelState(PanelCode.GamePanel, true);
        _playerManager.StartPlayer();
       
    }
    private void handleWinGame()
    {
        _cameraFollow.offset.x = 0;
        _cameraFollow.offset.y = 4.51f;
        _cameraFollow.offset.z = 5.62f;
        _cameraFollow.smoothSpeed = 0.015f;
        _uiManager.UpdatePanelState(PanelCode.WinPanel, true);
        _playerManager.StopPlayer();
    }
    private void handleLoseGame()
    {
        _cameraFollow.offset.x = 0;
        _cameraFollow.offset.y = 5f;
        _cameraFollow.offset.z = -7.22f;
        _cameraFollow.smoothSpeed = 0.125f;
        _uiManager.UpdatePanelState(PanelCode.LosePanel, true);
        _playerManager.StopPlayer();
    }
    private void handleRestartGame()
    {
        _cameraFollow.offset.x = 0;
        _cameraFollow.offset.y = 5f;
        _cameraFollow.offset.z = -7.22f;
        _cameraFollow.smoothSpeed = 0.125f;
        _uiManager.UpdatePanelState(PanelCode.GamePanel, true);
        _playerManager.RestartPlayer();
        _objectPooler.CreatePoolObjects();
        _collectManager.ActiveCollectedObject();
        _collectManager.ActiveObstacleObject();
    }
   
    private void OnValueChangedCallback()
    {
        UpdateGameState(State);
    }
    #endregion

}


public enum GameState
{
    WinGame,
    LoseGame,
    StartGame,
    WaitGame,
    RestartGame 
}

public enum TimeState
{
    HandleSetMaxTime
}