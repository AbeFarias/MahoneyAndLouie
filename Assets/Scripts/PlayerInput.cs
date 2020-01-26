using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public PlayerAction Left { get; set; }

    public PlayerAction Right { get; set; }

    public PlayerAction Attack { get; set; }

    public PlayerAction Swap { get; set; }

    private KeyCode leftKey;

    private KeyCode rightKey;

    private KeyCode attackKey;

    private KeyCode swapKey;
    // Start is called before the first frame update
    void Awake()
    {
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        attackKey = KeyCode.Space;
        swapKey = KeyCode.R;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Left = new PlayerAction(Input.GetKeyDown(leftKey), Input.GetKey(leftKey), Input.GetKeyUp(leftKey));
        Right = new PlayerAction(Input.GetKeyDown(rightKey), Input.GetKey(rightKey), Input.GetKeyUp(rightKey));
        Attack = new PlayerAction(Input.GetKeyDown(attackKey), Input.GetKey(attackKey), Input.GetKeyUp(attackKey));
        Swap = new PlayerAction(Input.GetKeyDown(swapKey), Input.GetKey(swapKey), Input.GetKeyUp(swapKey));
    }    

    public struct PlayerAction
    {
        public bool WasPressed { get; set; }
        public bool IsPressed { get; set; }
        public bool WasReleased { get; set; }
        public PlayerAction(bool wasPressed, bool isPressed, bool wasReleased)
        {
            WasPressed = wasPressed;
            IsPressed = isPressed;
            WasReleased = wasReleased;
        }
    }
    
    
}
