using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour {

    Collider2D col;
    public int attackDamage;
    public float knockBack;
    public int hitsToDestroy;
    private bool canBeKilled = false;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (canBeKilled)
                Destroy(gameObject);
            if (hitsToDestroy > 0)
            {
                Attack(collision.transform.gameObject);
                hitsToDestroy--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Attack(GameObject target)
    {
        target.GetComponent<PlayerManager>().TakeDamage(attackDamage);
        target.transform.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position) * knockBack, ForceMode2D.Impulse);
        if (hitsToDestroy == 0)
            Destroy(gameObject);
    }

    void ChangeKillState(int i)
    {
        if (i == 1)
            canBeKilled = true;
        else if(i == 0)
            canBeKilled = false;
    }
}
