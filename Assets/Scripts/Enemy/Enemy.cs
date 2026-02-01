using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public Transform target;
    private Animator animator;
    private SpriteRenderer sr;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        // Movimiento horizontal
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Actualiza parámetro Speed
        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        // Gira sprite según dirección
        if (direction.x > 0) sr.flipX = false;
        else if (direction.x < 0) sr.flipX = true;

        // Ataque: si está cerca del jugador
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < 1f)
        {
            animator.SetTrigger("Attack");
        }
    }
}