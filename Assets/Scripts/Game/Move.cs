using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    PlayerInputController playerInputController;
    private float? lastMousePoint = null;
    Vector2 readingValue;
    Vector3 movementValue;
    [SerializeField] float speed;

    public float xAxisClamp;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        readingValue = Vector2.zero;
        playerInputController = new PlayerInputController();


        playerInputController.CharacterControls.Move.started += movementInput;
        playerInputController.CharacterControls.Move.performed += movementInput;
        playerInputController.CharacterControls.Move.canceled += movementInput;
    }


    void FixedUpdate()
    {
        if(_gameManager.State == GameState.StartGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePoint = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                lastMousePoint = null;
                readingValue = Vector2.zero;
            }

            if (lastMousePoint != null)
            {
                transform.Translate(movementValue.x * speed, 0, 0);
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -xAxisClamp, xAxisClamp), transform.localPosition.y, transform.localPosition.z);
            }
        }

    }
    void movementInput(InputAction.CallbackContext context)
    {
        if (lastMousePoint != null)
        {
            readingValue = context.ReadValue<Vector2>();

            movementValue.x = readingValue.x;

        }


    }

    private void OnEnable()
    {
        playerInputController.Enable();
    }
    private void OnDisable()
    {
        playerInputController.Disable();
    }
}
