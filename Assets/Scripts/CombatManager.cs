using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Louie Louie;
    public Mahoney Mahoney;
    
    public GameObject MahoneyFloor;
    public GameObject LouieFloor;

    public float transitionSpeed = 0.6f;

    private Vector3 smallScale = new Vector3(0.5f, 0.5f, 1f);
    private Vector3 largeScale = new Vector3(1f,1f,1f);

    private Vector3 newPosForLouie;
    private Vector3 newPosForMahoney;
    
    private bool playerOneInCharge = true;
    // Update is called once per frame
    void Start()
    {
        Louie.InControl = playerOneInCharge;
        Mahoney.InControl = playerOneInCharge;

        newPosForLouie = MahoneyFloor.transform.position;
        newPosForMahoney = LouieFloor.transform.position;
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
            StartCoroutine(nameof(SwapFloors));
        }
    }

    IEnumerator SwapFloors()
    {
        var mahoneyFloorPos = MahoneyFloor.transform.position;
        var louieFloorPos = LouieFloor.transform.position;
        
        //LouieFloor.transform.position = Vector3.Lerp(louieFloorPos, newPosForLouie, transitionSpeed);
        //MahoneyFloor.transform.position = Vector3.Lerp(mahoneyFloorPos,newPosForMahoney, transitionSpeed);
        
        var louieFloorScale = LouieFloor.transform.localScale;
        var mahoneyFloorScale = MahoneyFloor.transform.localScale;
        
        //yield return RepeatLerp(mahoneyFloorPos, newPosForMahoney, 5, Switcher.MAHONEY_POS);
        //yield return RepeatLerp(louieFloorPos, newPosForLouie, 5, Switcher.LOUIE_POS);
        yield return RepeatLerp(mahoneyFloorPos, louieFloorPos, mahoneyFloorScale, louieFloorScale);
        var temp = newPosForLouie;
        newPosForLouie = newPosForMahoney;
        newPosForMahoney = temp;
        //MahoneyFloor.transform.localScale = Vector3.Lerp(new Vector3(mahoneyFloorScale.x, mahoneyFloorScale.y, mahoneyFloorScale.z), largeScale, transitionSpeed);
        //LouieFloor.transform.localScale  = Vector3.Lerp(new Vector3(louieFloorScale.x,louieFloorScale.y, louieFloorScale.z),smallScale, transitionSpeed);

        //yield return RepeatLerp(new Vector3(mahoneyFloorScale.x, mahoneyFloorScale.y, mahoneyFloorScale.z), largeScale, 5, Switcher.MAHONEY_POS);
        //yield return RepeatLerp(new Vector3(louieFloorScale.x,louieFloorScale.y, louieFloorScale.z),smallScale, 5, Switcher.LOUIE_POS);
        //yield return new WaitForSeconds(10f);
    }

    /*IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time, Switcher switcher)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * transitionSpeed;
        switch (switcher)
        {
            case(Switcher.MAHONEY_POS): 
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    MahoneyFloor.transform.position = Vector3.Lerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.LOUIE_POS):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    LouieFloor.transform.position = Vector3.Lerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.MAHONEY_SCALE):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    MahoneyFloor.transform.localScale = Vector3.Lerp(a, b, i);
                    yield return null;
                }
                break;
            case(Switcher.LOUIE_SCALE):
                while (i < 1.0f)
                {
                    i += Time.deltaTime * rate;
                    LouieFloor.transform.localScale = Vector3.Lerp(a, b, i);
                    yield return null;
                }
                break;
        }
    }*/

    IEnumerator RepeatLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        float i = 0.0f;
        float rate = (1.0f / 5.0f) * transitionSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            MahoneyFloor.transform.position = Vector3.Lerp(a,newPosForMahoney, transitionSpeed);
            LouieFloor.transform.position = Vector3.Lerp(b, newPosForLouie, transitionSpeed);
            MahoneyFloor.transform.localScale = Vector3.Lerp(c, largeScale, transitionSpeed);
            LouieFloor.transform.localScale = Vector3.Lerp(d, smallScale, transitionSpeed);   
            yield return null;
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
