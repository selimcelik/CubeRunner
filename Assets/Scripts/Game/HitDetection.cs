using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private CollectManager _collectManager;
    private GameManager _gameManager;
    private PlayerManager _playerManager;
    private ObjectPooler _objectPooler;
    private LevelManager _levelManager;

    public PlayerMovementController playerMovementController;

    public GameObject Player;

    public int PlayerDamageCount = 1;

    public int CollectedDiamondToCoinCount;

    public int CollectedDiamond5SideToCoinCount;



    private void Awake()
    {
        _collectManager = CollectManager.Instance;
        _gameManager = GameManager.Instance;
        _playerManager = PlayerManager.Instance;
        _objectPooler = ObjectPooler.Instance;
        _levelManager = LevelManager.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            _playerManager.AddDamage(PlayerDamageCount);
            _collectManager.ObstacleObjects.Add(other.gameObject);
            GameObject particleGO = _objectPooler.SpawnForGameObject("Particle", new Vector3(other.transform.position.x,other.transform.position.y+1,other.transform.position.z), Quaternion.identity, _objectPooler.poolParent.transform.GetChild(0).transform);
            Destroy(particleGO, 1);
            other.gameObject.SetActive(false);

        }

        if (other.tag == "Collectable")
        {
            switch (other.GetComponent<Collectable>().collectableType)
            {
                case Collectable.CollectableType.diamond:
                    _collectManager.AddCoin(CollectedDiamondToCoinCount);
                    _collectManager.CollectedObjects.Add(other.gameObject);
                    _playerManager.CollectableCountInALevel += CollectedDiamondToCoinCount;
                    GameObject particleGO = _objectPooler.SpawnForGameObject("Particle", other.gameObject.transform.position, Quaternion.identity, _objectPooler.poolParent.transform.GetChild(0).transform);
                    Destroy(particleGO, 1);
                    other.gameObject.SetActive(false);
                    break;
                case Collectable.CollectableType.diamond5side:
                    _collectManager.AddCoin(CollectedDiamond5SideToCoinCount);
                    _collectManager.CollectedObjects.Add(other.gameObject);
                    _playerManager.CollectableCountInALevel += CollectedDiamond5SideToCoinCount;
                    GameObject particleGO1 = _objectPooler.SpawnForGameObject("Particle", other.gameObject.transform.position, Quaternion.identity, _objectPooler.poolParent.transform.GetChild(0).transform);
                    Destroy(particleGO1, 1);
                    other.gameObject.SetActive(false);
                    break;
            }
        }

        if (other.tag == "Finish")
        {
            _gameManager.UpdateGameState(GameState.WinGame);
            Player.GetComponent<Animator>().SetBool("canWin", true);
        }
    }
}
