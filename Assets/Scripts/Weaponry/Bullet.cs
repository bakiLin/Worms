using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform cutterTransform;

    private Cutter cutter;

    private void Awake()
    {
        cutterTransform = GameObject.Find("Cutter").transform;
        cutter = cutterTransform.GetComponent<Cutter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Cutter"))
        {
            cutterTransform.position = transform.position;
            Invoke("DoCut", 0.1f);
        }
    }

    private void DoCut()
    {
        cutter.DoCut();
        Destroy(gameObject);
    }
}
