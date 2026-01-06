using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float speed;
    public float health = 100;
    public int worth  = 25;

    public GameObject deathEffect;

    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamge(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
        
    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }


    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStart.Money += worth;
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
