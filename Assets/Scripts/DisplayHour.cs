using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayHour : MonoBehaviour
{
    public TextMeshProUGUI hourDisplay;
    public string text;

    public int timeToBeActive;
    float timeAc;
    void Start()
    {
        hourDisplay.gameObject.SetActive(true);
        hourDisplay.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        timeAc += Time.deltaTime;

        if(timeAc >= timeToBeActive)
        {
            hourDisplay.gameObject.SetActive(false);
        }
    }
}
