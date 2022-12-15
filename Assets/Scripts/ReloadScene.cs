using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public ScoringSystem counter;
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            ScoringSystem.theScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
