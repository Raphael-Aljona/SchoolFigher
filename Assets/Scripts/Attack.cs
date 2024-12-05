using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        if (enemy != null)
        {
            // Inimigo recebe dano
            enemy.TakeDamage(damage);
        }
    }
}
