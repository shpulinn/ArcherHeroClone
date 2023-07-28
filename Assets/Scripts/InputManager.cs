using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private LayerMask clickableLayerMask;
    [SerializeField] private Joystick _moveJoystick;

    private Vector3 _moveVector;

    private bool _joystick;
    
    public Vector3 MoveVector => _moveVector;
    public bool Joystick => _joystick;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        //MousePosition = Mouse.current.position.ReadValue();
        if (_moveJoystick.Horizontal == 0 && _moveJoystick.Vertical == 0)
        {
            _joystick = false;
            return;
        }

        _joystick = true;
        _moveVector = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
    }
}
