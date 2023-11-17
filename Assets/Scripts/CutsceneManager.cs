using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    public GameObject player;
    public PlayerInventory pi;
    public PlayerItem pt;
    public Animator anim;


    public bool inRange;
    public bool loadTitle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.playerItems.Contains(pt.gameObject))
        {
            if (inRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(player);
                    anim.Play("Ending");
                }
            }
        }

        if (loadTitle)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
}
