using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }
    private void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed; //reset lai speed sau khi ra khoi tam ban laser
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
