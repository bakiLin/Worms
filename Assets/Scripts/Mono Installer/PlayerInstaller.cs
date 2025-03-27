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

    [SerializeField]
    private PlayerShoot playerShoot;

    [SerializeField]
    private PlayerRotation playerRotation;

    [SerializeField]
    private WeaponRotation weaponRotation;

    [SerializeField]
    private PlayerAnimation playerAnimation;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle().NonLazy();

        Container.Bind<PlayerGroundContact>().FromInstance(playerGroundContact).AsSingle().NonLazy();

        Container.Bind<PlayerGravity>().FromInstance(playerGravity).AsSingle().NonLazy();

        Container.Bind<PlayerShoot>().FromInstance(playerShoot).AsSingle().NonLazy();

        Container.Bind<PlayerRotation>().FromInstance(playerRotation).AsSingle().NonLazy();

        Container.Bind<WeaponRotation>().FromInstance(weaponRotation).AsSingle().NonLazy();

        Container.Bind<PlayerAnimation>().FromInstance(playerAnimation).AsSingle().NonLazy();
    }
}