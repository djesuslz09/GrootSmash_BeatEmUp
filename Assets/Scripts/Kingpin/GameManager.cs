using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Boss")]
    public BossController boss; // Arrastra tu Boss aquí

    [Header("Evento enemigo muerto")]
    public UnityEvent onEnemyDeath = new UnityEvent();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NotifyEnemyDeath()
    {
        onEnemyDeath.Invoke();
        if (boss != null)
        {
            boss.TriggerAngerAnimation();
        }
    }
}


