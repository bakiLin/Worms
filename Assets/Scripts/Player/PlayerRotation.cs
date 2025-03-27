using UniRx;
using UnityEngine;
using Zenject;

public class PlayerRotation : MonoBehaviour
{
    [Inject]
    private PlayerInput input;

    private bool isFacingRight = true;

    private CompositeDisposable disposable = new CompositeDisposable();

    public void Rotate()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (input.GetKeyboardInput().x > 0f && !isFacingRight)
            {
                isFacingRight = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (input.GetKeyboardInput().x < 0f && isFacingRight)
            {
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }).AddTo(disposable);
    }

    public void StopRotate() => disposable.Clear();
}
