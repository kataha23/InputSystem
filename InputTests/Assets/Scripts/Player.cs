using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private ControlSytem controls; // Input Actions Class를 저장할 변수
    private Rigidbody2D rigidbody2D;
    [Header("이동")]
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [Header("발사")]
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알 프리팹
    [SerializeField] private float fireRate = 2; // 총알 발사 주기
    [SerializeField] private float shootSpeed = 5; // 총알 속도
    private float previousFireTime;
    private bool shouldFire;

    private void Awake()
    {
        controls = new ControlSytem(); // controls에 Controls 데이터 영역을 생성 후 저장
        controls.Player.Enable(); // controls의 Player Actions 
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        controls.Player.Fire.performed += (callbacks) => shouldFire = true;
        controls.Player.Fire.canceled += (callbacks) => shouldFire = false;
    }

    private void OnDisable()
    {
        controls.Player.Fire.performed -= (callbacks) => shouldFire = true;
        controls.Player.Fire.canceled -= (callbacks) => shouldFire = false;
    }
    private void Update()
    {
        #region 플레이어 이동
        Vector2 dir = controls.Player.Move.ReadValue<Vector2>();
        rigidbody2D.linearVelocity = dir * moveSpeed;
        #endregion

        #region 총알 발사
        if (!shouldFire) { return; }
        if (Time.time < previousFireTime + (1 / fireRate)) { return; }

        Rigidbody2D rb = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * shootSpeed;
            
        previousFireTime = Time.time;
        #endregion

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
