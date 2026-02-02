using UnityEngine;

public class ProyectilBoss : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float tiempoVida = 5f; // Se autodestruye después de 5 segundos

    private void Start()
    {
        // Autodestruye el proyectil después de un tiempo
        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si golpea al jugador
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.RecibirDaño(damage);
            }
            Destroy(gameObject); // Destruye el proyectil al impactar
        }

        // Si golpea paredes u obstáculos (opcional)
        if (collision.CompareTag("Wall") || collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}