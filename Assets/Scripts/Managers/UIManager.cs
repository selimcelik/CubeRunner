using System.Threading.Tasks;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public int PanelDelay = 2000;

    public GameObject GamePanel;

    public GameObject StartPanel;

    public GameObject WinPanel;

    public GameObject LosePanel;

    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        UpdatePanelState(PanelCode.StartPanel,true);
    }

    #region UI Panel Options
    public void UpdatePanelState(PanelCode panelCode, bool opened)
    {
        switch (panelCode)
        {
            case PanelCode.WinPanel:
                openPanels(opened, 1);
            break;
            case PanelCode.LosePanel:
                openPanels(opened, 2);
            break;
            case PanelCode.StartPanel:
                openPanels(opened, 3);
            break;
            case PanelCode.GamePanel:
                openPanels(opened, 4);
            break;
        }
    }

    private async void openPanels(bool opened, int panelNumber)
    {
        if (opened)
        {
            CloseAllPanels();
            if (panelNumber == 1)
            {
                await Task.Delay(PanelDelay);
                WinPanel.SetActive(opened);
            }
            if (panelNumber == 2)
            {
                await Task.Delay(PanelDelay);
                LosePanel.SetActive(opened);
            }
            if (panelNumber == 3)
            {
                StartPanel.SetActive(opened);
            }
            if (panelNumber == 4)
            {
                GamePanel.SetActive(opened);
            }
        }
    }
    
    public void CloseAllPanels()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        StartPanel.SetActive(false);
        GamePanel.SetActive(false);
    }
    #endregion
}

public enum PanelCode
{
    WinPanel,
    LosePanel,
    StartPanel,
    GamePanel
}