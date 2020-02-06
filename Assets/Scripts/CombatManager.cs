using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Louie Louie;
    public Mahoney Mahoney;
    
    public GameObject MahoneyFloor;
    public GameObject LouieFloor;

    public Vector3 topPos;
    public Vector3 botPos;
    
    public float transitionSpeed = 0.6f;

    private Vector3 smallScale = new Vector3(0.5f, 0.5f, 1f);
    private Vector3 largeScale = new Vector3(1f,1f,1f);

    //private Vector3 topPos = new Vector3(-0.5f, 1.3f, 0);
    //private Vector3 botPos = new Vector3(0, 0, 0);
    
    private bool LouieInControl = true;
    // Update is called once per frame
    void Start()
    {
        Louie.InControl = LouieInControl;
        Mahoney.InControl = LouieInControl;
    }
    void Update()
    {
        SwapPlayers();
    }
    
    void SwapPlayers()
    {
        if (PlayerInput.Instance.Swap.WasPressed)
        {
            var mahoneyFloorPos = MahoneyFloor.transform.position;
            var louieFloorPos = LouieFloor.transform.position;
            var louieFloorScale = LouieFloor.transform.localScale;
            var mahoneyFloorScale = MahoneyFloor.transform.localScale;
            
            LouieInControl = !LouieInControl;
            Louie.InControl = LouieInControl;
            Mahoney.InControl = LouieInControl;
            //StartCoroutine(nameof(SwapFloors));

            StartCoroutine(RepeatLerp(mahoneyFloorPos, LouieInControl ? topPos: botPos, 5, Switcher.MAHONEY_POS));
            StartCoroutine(RepeatLerp(louieFloorPos, LouieInControl ? botPos: topPos, 5, Switcher.LOUIE_POS));
            StartCoroutine(RepeatLerp(mahoneyFloorScale, LouieInControl ? smallScale : largeScale, 5, Switcher.MAHONEY_SCALE));
            StartCoroutine(RepeatLerp(louieFloorScale, LouieInControl ? largeScale : smallScale, 5, Switcher.LOUIE_SCALE));
        }
    }

    IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time, Switcher switcher)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * transitionSpeed;
        switch (switcher)
        {
            case(Switcher.MAHONEY_POS): 
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    MahoneyFloor.transform.position = Vector3.Slerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.LOUIE_POS):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    LouieFloor.transform.position = Vector3.Slerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.MAHONEY_SCALE):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    MahoneyFloor.transform.localScale = Vector3.Slerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.LOUIE_SCALE):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    LouieFloor.transform.localScale = Vector3.Slerp(a, b, i);
                    yield return null;
                }
                break;
        }
    }

    public enum Switcher
    {
        LOUIE_POS,
        LOUIE_SCALE,
        MAHONEY_POS,
        MAHONEY_SCALE
    }
}
