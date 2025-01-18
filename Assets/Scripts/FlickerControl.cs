using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    [Tooltip("Enabled?")]
    public bool flicker = true;

    [SerializeField]
    [Tooltip("Minimum delay in seconds")]
    private float timeDelayMin = 0.01f;
    [SerializeField]
    [Tooltip("Maximum delay in seconds")]
    private float timeDelayMax = 1f;

    private bool isFlickering = false;
    private float timeDelay;


    private void Update()
    {
        if (!isFlickering && flicker == true)
        {
            StartCoroutine(FlickeringRoutine());
        }
    }

    private IEnumerator FlickeringRoutine()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(timeDelayMin, timeDelayMax);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(timeDelayMin, timeDelayMax);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }

    public void SetFlicker(bool active)
    {
        flicker = active;
    }
}
