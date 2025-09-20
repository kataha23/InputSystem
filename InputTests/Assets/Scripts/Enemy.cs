using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("속성")]
    [SerializeField] private float speed = 2f; // 적의 이동 속도
    [SerializeField] private float FollowingChance = 30; // 플레이어를 따라올 확률
    [SerializeField] private float BossChance = 10; // 정예 적이 될 확률
    private Vector3 dir; // 적의 이동 방향
    private float random;
    private int requireHit = 1;
    private void Start()
    {
        WhereToGo();
        IsitBoss();
    }
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void WhereToGo()
    {
        random = Random.Range(1, 101);

        if (random <= FollowingChance)
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position; 
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime; 
        }
        else
        {
            dir = Vector3.right;
            transform.position += dir * speed * Time.deltaTime; 
        }
    }
    private void IsitBoss()
    {
        random = Random.Range(1, 101);
        if (random <= BossChance)
        {
            speed *= 1.5f;
            transform.localScale *= 1.5f;
            GetComponent<SpriteRenderer>().color = Color.purple;
            requireHit = 3;
        }
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            requireHit--;

            if (requireHit <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Outline"))
        {
            Destroy(gameObject);
        }
    }
}
