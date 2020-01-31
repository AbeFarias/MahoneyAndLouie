using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Louie Louie;
    public Mahoney Mahoney;
    private bool playerOneInCharge = true;
    // Update is called once per frame
    void Start()
    {
        Louie.InControl = playerOneInCharge;
        Mahoney.InControl = playerOneInCharge;
    }
    void Update()
    {
        SwapPlayers();
    }
    
    void SwapPlayers()
    {
        if (PlayerInput.Instance.Swap.WasPressed)
        {
            playerOneInCharge = !playerOneInCharge;
            Louie.InControl = playerOneInCharge;
            Mahoney.InControl = playerOneInCharge;
        }
    }
}
