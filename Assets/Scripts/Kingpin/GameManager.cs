using UnityEngine;
using UnityEngine.Events;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    [Header("Boss")]
    public BossController boss; // Arrastra tu Boss aqu�

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
            BossAngryController bossScript = boss.GetComponent<BossAngryController>();
            if (bossScript != null)
            {
            bossScript.OnEnemyKilled();
            }
        }
        /*onEnemyDeath.Invoke();
        if (boss != null)
        {
            boss.TriggerAngerAnimation();
        }*/
    }
}