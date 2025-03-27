using UniRx;
using UnityEngine;
using Zenject;

public class PlayerAnimation : MonoBehaviour
{
    [Inject]
    private PlayerInput playerInput;

    private Animator animator;

    private CompositeDisposable disposable = new CompositeDisposable();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Mathf.Abs(playerInput.GetKeyboardInput().x) > 0f)
            {
                if (!animator.GetBool("isWalking"))
                    animator.SetBool("isWalking", true);
            }
            else
            {
                if (animator.GetBool("isWalking"))
                    animator.SetBool("isWalking", false);
            }
        }).AddTo(disposable);
    }

    public void StopWalk()
    {
        animator.SetBool("isWalking", false);
        disposable.Clear();
    }
}
