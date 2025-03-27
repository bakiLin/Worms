using UniRx;
using UnityEngine;
using Zenject;

public class WeaponRotation : MonoBehaviour
{
    [Inject]
    private PlayerInput input;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform target;

    private float zRotation;

    private CompositeDisposable disposable = new CompositeDisposable();

    private void Start()
    {
        target.position = transform.position + transform.right;
    }

    public void Rotate()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (input.GetKeyboardInput().y > 0f)
            {
                zRotation = transform.rotation.eulerAngles.z + (Time.deltaTime * speed);
                if (zRotation > 50f && zRotation < 310f) zRotation = 50f;
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, zRotation);
                target.position = transform.position + transform.right;
            }
            else if (input.GetKeyboardInput().y < 0f)
            {
                zRotation = transform.rotation.eulerAngles.z - (Time.deltaTime * speed);
                if (zRotation > 50f && zRotation < 310f) zRotation = 310f;
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, zRotation);
                target.position = transform.position + transform.right;
            }
        }).AddTo(disposable);
    }

    public void StopRotate() => disposable.Clear();
}
