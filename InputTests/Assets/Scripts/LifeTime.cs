using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float life = 3f;
    private void Start()
    {
        Destroy(gameObject, life);
    }
}
