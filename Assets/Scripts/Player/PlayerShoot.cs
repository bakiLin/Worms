using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootForce;

    private Transform targetRotation;

    private Vector3 position, direction;

    private void Awake()
    {
        targetRotation = transform.Find("Target Sprite");
    }

    public void Shoot()
    {
        direction = targetRotation.position - transform.position;
        position = transform.position + direction * 0.5f;
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * shootForce, ForceMode2D.Impulse);
    }
}
