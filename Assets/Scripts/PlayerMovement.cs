using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private int speedWalk = 16;
  private Vector2 movement = Vector2.zero;
  private Rigidbody2D rb = null;
  private Animator animator = null;
  private SpriteRenderer spriteRenderer;
  

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    
  }

  private void OnMovement(InputValue value)
  {
    movement = value.Get<Vector2>();
    
    //animator.SetBool("isWalking", true);
  }

  private void FixedUpdate()
  {
    //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    rb.AddForce(movement * speedWalk);
    if(movement.x != 0 || movement.y != 0)
    {
      //transform.localScale = Vector3.one;
      animator.SetBool("isWalking", true);
      /*if (movement.x < 0)
      {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
      }
      if (movement.x > 0)
      {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
      }*/
    }
    else
    {
      //transform.localScale = new Vector3(-1f, 1f, 1f);
      animator.SetBool("isWalking", false);
    }
  }
}
