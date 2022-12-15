using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    public AudioSource collectSound;

    void Start()
    {
        

    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        ScoringSystem.theScore += 1;
        Destroy(gameObject);
        if (ScoringSystem.theScore == 6)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

}
