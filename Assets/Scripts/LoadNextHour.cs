using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextHour : MonoBehaviour
{
    public string sceneToLoad;

    public void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<PlayerController>().canProgressToNextHour == true)
        {
            SceneManager.LoadScene(sceneToLoad);

        }

    }

}
