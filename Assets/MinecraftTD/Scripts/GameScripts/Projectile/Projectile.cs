using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   public int damage;
   public Transform target;
   public float speed;
   public float rotationSpeed;
   private Rigidbody2D _rigidbody2D;

   private void Start()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void FixedUpdate()
   {
      if (target == null)
      {
         Destroy(gameObject);
      }

      Vector3 direction = (target.position - transform.position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
      _rigidbody2D.linearVelocity = transform.forward * speed;
   }

   public void SetTarget(Transform targetTransform)
   {
      target = targetTransform;
   }
   public void SetDamage(int newDamage)
   {
      damage = newDamage;
   }
}
