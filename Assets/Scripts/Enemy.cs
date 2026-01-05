using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float health = 100;
    public int moneyGain = 25;
    public GameObject deathEffect;
    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamge(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStart.Money += moneyGain;
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.4f) {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStart.Lives--;
        Destroy(gameObject);
    }
}
