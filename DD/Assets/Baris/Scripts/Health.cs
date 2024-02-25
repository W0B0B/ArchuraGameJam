using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject bloodParticle;
    [SerializeField] public int MaxHealt,CurrentHealt;
    
    private void Start() {
        CurrentHealt=MaxHealt;
    }
    public void TakeDamage(int damage ){
        CurrentHealt-=damage;
        Instantiate(bloodParticle,new Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
        if (CurrentHealt<=0)
        {
            this.gameObject.SetActive(false);        
        }
    }

}
