using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Posición final")]
    public Transform finalPosition; // Arrastra el punto final del mapa

    [Header("Movimiento")]
    public float moveSpeed = 2f;
    private bool hasReachedEnd = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (finalPosition != null && !hasReachedEnd)
        {
            // Mueve hacia el final del mapa
            transform.position = Vector3.MoveTowards(transform.position, finalPosition.position, moveSpeed * Time.deltaTime);

            // Para al llegar
            if (Vector3.Distance(transform.position, finalPosition.position) < 0.1f)
            {
                hasReachedEnd = true;
                // Opcional: Cambia a idle
                if (animator != null) animator.SetTrigger("Idle");
            }
        }
    }

    public void TriggerAngerAnimation()
    {
        if (animator != null && hasReachedEnd)
        {
            animator.SetTrigger("Enfado"); // Activa la animación de enfado
        }
    }
}