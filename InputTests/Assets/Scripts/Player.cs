using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private ControlSytem controls; // Input Actions Class�� ������ ����
    private Rigidbody2D rigidbody2D;
    [Header("�̵�")]
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [Header("�߻�")]
    [SerializeField] private GameObject bulletPrefab; // �߻��� �Ѿ� ������
    [SerializeField] private float fireRate = 2; // �Ѿ� �߻� �ֱ�
    [SerializeField] private float shootSpeed = 5; // �Ѿ� �ӵ�
    private float previousFireTime;
    private bool shouldFire;

    private void Awake()
    {
        controls = new ControlSytem(); // controls�� Controls ������ ������ ���� �� ����
        controls.Player.Enable(); // controls�� Player Actions 
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
        #region �÷��̾� �̵�
        Vector2 dir = controls.Player.Move.ReadValue<Vector2>();
        rigidbody2D.linearVelocity = dir * moveSpeed;
        #endregion

        #region �Ѿ� �߻�
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
