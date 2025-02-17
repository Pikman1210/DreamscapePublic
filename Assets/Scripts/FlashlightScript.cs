using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public bool isOn = false;
    public GameObject lightSource;
    public float delayTime = 0.05f;
    [SerializeField]
    private bool delay = false;

    private void Update()
    {
        if (Input.GetButtonDown("FKey") && isOn == false && delay == false)
        {
            delay = true;
            lightSource.SetActive(true);
            AudioManager.Instance.Play("FlashlightClick");
            isOn = true;
            StartCoroutine(Delay());
        } else if (Input.GetButtonDown("FKey") && isOn == true && delay == false)
        {
            delay = true;
            lightSource.SetActive(false);
            AudioManager.Instance.Play("FlashlightClick");
            isOn = false;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        delay = false;
    }
}
