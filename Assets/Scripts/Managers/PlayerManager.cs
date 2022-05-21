using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private GameManager _gameManager;

    public GameObject Player;
    private GameObject playerMesh;
    [HideInInspector]
    public float PlayerSpeed;

    public float DefaultPlayerSpeed = 6;

    public int PlayerMaxDeadCount = 3;
    [HideInInspector]
    public int PlayerLifeCount;

    public int PlayerCurrentDeadCount = 0;

    public int CollectableCountInALevel = 0;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }


    #region Player Start And Stop Options
    public void StopPlayer()
    {
        PlayerSpeed = 0;
    }

    public void StartPlayer()
    {
        playerMesh = Player.transform.GetChild(0).gameObject;
        PlayerLifeCount = PlayerMaxDeadCount;
        PlayerSpeed = DefaultPlayerSpeed;
        playerMesh.GetComponent<Animator>().SetBool("canRun", true);
        CollectableCountInALevel = 0;
    }

    public async void RestartPlayer()
    {
        playerMesh.GetComponent<Animator>().enabled = true;
        playerMesh.GetComponent<Rigidbody>().isKinematic = true;
        playerMesh.GetComponent<Animator>().SetBool("canRun", false);
        playerMesh.GetComponent<Animator>().SetBool("canWin", false);
        playerMesh.GetComponent<CapsuleCollider>().enabled = false;
        Player.transform.position = new Vector3(0, 0, 0);
        playerMesh.transform.localPosition = new Vector3(0, 0, 0);
        PlayerCurrentDeadCount = 0;
        await Task.Delay(1000);
        playerMesh.GetComponent<Animator>().SetBool("canRun", true);
        playerMesh.GetComponent<CapsuleCollider>().enabled = true;
        _gameManager.UpdateGameState(GameState.StartGame);
    }
    #endregion

    #region Player Health Options
    public void AddDamage(int amount)
    {
        PlayerCurrentDeadCount += amount;
        PlayerLifeCount -= amount;
        OnValueChangedCallback();
    }
    #endregion

    private void OnValueChangedCallback()
    {
        if(PlayerCurrentDeadCount >= PlayerMaxDeadCount)
        {
            _gameManager.UpdateGameState(GameState.LoseGame);
            playerMesh.GetComponent<RagdollScript>().die = true;
        }
    }

}
