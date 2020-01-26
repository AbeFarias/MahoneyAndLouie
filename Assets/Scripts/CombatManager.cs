using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Louie Louie;
    public Mahoney Mahoney;
    private bool playerOneInCharge = true;
    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.Swap.WasPressed)
        {
            Debug.Log("Here" + playerOneInCharge);
            playerOneInCharge = !playerOneInCharge;
            Louie.InControl = playerOneInCharge;
            Mahoney.InControl = playerOneInCharge;
        }
    }
}
