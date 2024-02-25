using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ElementSelecter : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public static Action<int> OnElementPressed;
    private void Awake() {
        for (int i = 0; i < buttons.Length; i++)
        {
            int a=i;
            buttons[i].onClick.AddListener( ()=>
            {
                ElementInfo(a);

            });
        }
    }

    void ElementInfo(int index){
        Debug.Log(index);
        OnElementPressed?.Invoke(index);
    }
    public void RestartB(){
        SceneManager.LoadScene(0);
    }
    

}
