using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealt,CurrentHealt;
    private void Start() {
        CurrentHealt=MaxHealt;
    }
    public void TakeDamage(float damage ){
        CurrentHealt=-damage;
        if (CurrentHealt<=0)
        {
            Destroy(this.gameObject,3);        
        }
    }

}
