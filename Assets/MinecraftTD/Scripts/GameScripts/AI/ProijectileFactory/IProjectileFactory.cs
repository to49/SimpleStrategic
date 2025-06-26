using UnityEngine;

public interface IProjectileFactory
{ 
    Projectile CreateProjectile( GameObject prefab);
}