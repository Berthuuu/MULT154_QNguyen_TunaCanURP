using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        GameManager.instance.Win();
    }
}