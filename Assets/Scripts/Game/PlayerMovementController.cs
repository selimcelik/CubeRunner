using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    public GameObject PlayerHolder;

    private PlayerManager _playerManager;

    private void Awake()
    {
        _playerManager = PlayerManager.Instance;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _playerManager.PlayerSpeed);
    }

}
