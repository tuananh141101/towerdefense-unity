using UnityEngine;


public class Turret : MonoBehaviour
{
    [Header("Targeting Settings")]
    public Transform target;
    public Enemy targetEnemy;
    public float range = 15f;
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform partToRotate; //rotate theo enemy

    [Header("General Shooting")]
    public float fireRate = 1f; //so vien ban tren 1s - 1v/1s
    private float fireCountDown = 0f;
    public Transform firePoint;

    [Header("Laser Settings")]
    public bool useLaser = false;
    public int damageOverTime = 35;
    public float slowAmount = .5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //chay lien tuc cach nhau 0.5s
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity; // dung de ss voi kcach enemy gan nhat 
        GameObject nearestEnemy = null; 

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }
        
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamge(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)

        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGo  = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
