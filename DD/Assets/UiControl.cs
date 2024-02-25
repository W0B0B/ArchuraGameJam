using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControl : MonoBehaviour
{
    public Health health;
    public GameObject GameOverScene;
    void Start()
    {
        health=GetComponent<Health>();
    }

    
    void Update()
    {
        if (health.CurrentHealt<=0)
        {
            GameOverScene.SetActive(true);
        }    
    }
}
