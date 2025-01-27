using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
   [SerializeField] private float normBulletSpeed = 15f;
   [SerializeField] private float normBulletDamage = 1f;
   [SerializeField] private float desrtoyTime = 3f;
   [SerializeField] private LayerMask CollisKillBullMask;
   private Rigidbody2D rb;
   private float damage;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      SetStraightVelocity();
      SetKillTime();
   }

   private void SetStraightVelocity()
   {
      rb.velocity = transform.right * normBulletSpeed;
   }

   private void SetKillTime()
   {
      Destroy(gameObject,desrtoyTime);
   }
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if ((CollisKillBullMask.value & (1 << collision.gameObject.layer)) > 0)
      {
         //damage enemy
         IDamageble iDamageble = collision.gameObject.GetComponent<IDamageble>();
         if (iDamageble != null)
         {
            iDamageble.Damage(normBulletDamage);
         }
         //destroy bullt   
         Destroy(gameObject);
      }
   }
}
