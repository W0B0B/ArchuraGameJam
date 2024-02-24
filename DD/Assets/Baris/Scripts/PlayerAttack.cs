using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Power")]
    [SerializeField] float attackCD;
    [SerializeField] float damage;
    [SerializeField] float attackDuration;
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPosition;
    [SerializeField] LayerMask whatIsEnemy;
    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        Collider2D[] enemysToDamage= Physics2D.OverlapCircleAll(attackPosition.position,attackRange,whatIsEnemy);
        
        yield return new WaitForSeconds(attackDuration); 
        for (int i = 0; i < enemysToDamage.Length; i++)
        {
            enemysToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(attackCD);
        
    }
    private void OnDrawGizmosSelected() {
        if (attackPosition!=null)
        {
            Gizmos.DrawWireSphere(attackPosition.position,attackRange);            
        }

    }
}
