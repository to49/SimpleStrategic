using Unity.VisualScripting;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour, IProjectileFactory
{ 
    public Projectile CreateProjectile(GameObject prefab)
    {
        Projectile projectile = Instantiate(prefab, transform.position, Quaternion.identity).GetOrAddComponent<Projectile>();
        return projectile;
    }
}
