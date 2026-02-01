using UnityEngine;

public class BossAngryController : MonoBehaviour
{
    [Header("Animaciones")]
    public Animator animator;                    // Animator del boss
    public string idleStateName = "BossIdle";   // nombre del clip idle
    public string angryStateName = "BossAngry"; // nombre del clip enfado

    [Header("Enemigos")]
    public int totalEnemies = 5;                // cuántos enemigos hay
    private int enemiesKilled = 0;

    void Start()
    {
        // Empezamos en idle
        if (animator != null && !string.IsNullOrEmpty(idleStateName))
        {
            animator.Play(idleStateName);
        }
    }

    // Llamar desde los enemigos cuando mueran
    public void OnEnemyKilled()
    {
        enemiesKilled++;

        // Reproducir animación de enfado
        if (animator != null && !string.IsNullOrEmpty(angryStateName))
        {
            animator.Play(angryStateName);
        }

        // Si se murieron todos
        if (enemiesKilled >= totalEnemies)
        {
            Debug.Log("¡Todos los enemigos muertos! Fase del boss final.");
            // Aquí puedes: activar ataques del boss, cambiar música, etc.
            // animator.Play("BossAttack");
        }
    }
}