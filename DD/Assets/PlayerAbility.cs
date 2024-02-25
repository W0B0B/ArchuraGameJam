using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    Rigidbody2D rb;
   [SerializeField] LayerMask whatIsIce; 
   [SerializeField] GameObject prefabD;
    Vector2 spawnPoint;
    private void Start() {
        rb=GetComponent<Rigidbody2D>();
    }
   private void Update() {
    Collider2D _collider2D=Physics2D.OverlapCircle(new Vector2(transform.position.x+1,transform.position.y),10f,whatIsIce);
    if (_collider2D&&Input.GetKeyDown(KeyCode.Z))
    {
        _collider2D.gameObject.SetActive(false);
    }
    if (Input.GetMouseButtonDown(0))
    {
        spawnPoint=Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        Instantiate(prefabD,spawnPoint,Quaternion.identity);
    }
    if (Input.GetKeyDown(KeyCode.X))
    {
        switch (rb.gravityScale)
        {
            case 3:
                rb.gravityScale=-3;
                break;
            case -3:
                rb.gravityScale=3;
                break;
            
        }
    }
   }
}
