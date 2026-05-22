using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;
    
    private int combo = 0;
    private TextMeshProUGUI textoCombo;

    void Awake()
    {
        Instance = this;
        textoCombo = GetComponent<TextMeshProUGUI>();
    }

    public void SumarCombo()
    {
        combo++;
        textoCombo.text = combo.ToString();
    }

    public void ResetCombo()
    {
        combo = 0;
        textoCombo.text = "00";
    }
}