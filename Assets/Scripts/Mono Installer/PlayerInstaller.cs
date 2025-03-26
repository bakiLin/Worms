using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private PlayerGroundContact playerGroundContact;

    [SerializeField]
    private PlayerGravity playerGravity;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle().NonLazy();

        Container.Bind<PlayerGroundContact>().FromInstance(playerGroundContact).AsSingle().NonLazy();

        Container.Bind<PlayerGravity>().FromInstance(playerGravity).AsSingle().NonLazy();
    }
}