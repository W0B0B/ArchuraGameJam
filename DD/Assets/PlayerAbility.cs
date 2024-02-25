using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{   
    Rigidbody2D rb;
   [SerializeField] LayerMask whatIsIce;
   [SerializeField] GameObject prefabD;
   [SerializeField] GameObject panel;
    Vector2 spawnPoint;
    bool cyanCheck=false;
    bool morCheck=false;
    bool turuncuCheck=false;
    bool isPanelActive;
    
    private void OnEnable() {
        ElementSelecter.OnElementPressed+= SelectedColor;
    }
    private void OnDisable() {
        ElementSelecter.OnElementPressed-= SelectedColor;
    }
    private void Start() {
        rb=GetComponent<Rigidbody2D>();
    }
    void SelectedColor(int ElementIndex){
        switch (ElementIndex)
        {
            case 0:
                Debug.Log("Mor");
                morCheck=true;
                turuncuCheck=false;
                cyanCheck=false;
                break;
            case 1:
                Debug.Log("turucu");
                morCheck=false;
                turuncuCheck=true;
                cyanCheck=false;
                break;
            case 2:
                Debug.Log("cyan");
                morCheck=false;
                turuncuCheck=false;
                cyanCheck=true;
                break;
            
        }
    }
   private void Update() {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
        isPanelActive=true;
        
        panel.SetActive(true);
    }
    if (Input.GetKeyUp(KeyCode.Tab))
    {
        isPanelActive=false;
        panel.SetActive(false);
    }
    if (isPanelActive==false)
    {
            Collider2D _collider2D=Physics2D.OverlapCircle(new Vector2(transform.position.x+1,transform.position.y),10f,whatIsIce);
    if (_collider2D&&turuncuCheck==true)
    {
        if (Input.GetMouseButtonDown(1))
        {
            _collider2D.gameObject.SetActive(false);    
        }
        
    }
    if (cyanCheck==true)
    {
        if (Input.GetMouseButtonDown(1))
        {
            spawnPoint=Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
            GameObject A= Instantiate(prefabD,spawnPoint,Quaternion.identity);
            Destroy(A,3);
        }
    }
    if (morCheck==true)
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (rb.gravityScale)
        {
            case 3:
                rb.gravityScale=-2;
                break;
            case -2:
                rb.gravityScale=3;
                break;        
        }    
        }
        
    }
    }
   }
}
