using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Clase que gestiona la interfaz de usuario, permitiendo elegir la cantidad de informacion mostrada, alterar la velocidad o reinicira la simulación
 * */
public class GUIManager : MonoBehaviour
{
    GameManager gameManager;

    public Toggle pathToggle;
    public Toggle sensesToggle;
    public Toggle statsToggle;
    public Slider timeSlider;
    public TextMeshProUGUI timeTMP;
    public Slider preySlider;
    public TextMeshProUGUI preyTMP;
    public Slider hunterSlider;
    public TextMeshProUGUI hunterTMP;

    private bool playing = true;

    private void Awake()
    {
        ReadPreferences();
    }

    void Start()
    {
        gameManager = GameManager.instance;
        
    }

    public void TogglePathRendering()
    {
        gameManager.TogglePathRendering(pathToggle.isOn);
    }

    public void ToggleStatsRendering()
    {
        gameManager.ToggleStatsRendering(statsToggle.isOn);
    }

    public void ToggleSensesRendering()
    {
        gameManager.ToggleSensesRendering(sensesToggle.isOn);
    }

    public void ChangeTime()
    {
        float value = (float)System.Math.Round((double)timeSlider.value,2);
        gameManager.SetTime(value);
        timeTMP.text = value.ToString();
    }

    public void ChangePrey()
    {
        if (playing)
        {
            int value = Mathf.RoundToInt(preySlider.value);
            preyTMP.text = value.ToString();
            SavePreferences();
        }

    }

    public void ChangeHunter()
    {
        if (playing)
        {
            int value = Mathf.RoundToInt(hunterSlider.value);
            hunterTMP.text = value.ToString();
            SavePreferences();
        }
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt("nPrey", Mathf.RoundToInt(preySlider.value));
        PlayerPrefs.SetInt("nHunter", Mathf.RoundToInt(hunterSlider.value));
    }

    private void ReadPreferences()
    {
        playing = false;
        int nPrey = PlayerPrefs.GetInt("nPrey");
        if (nPrey > 0) { 
            preySlider.value = nPrey;
            preyTMP.text = nPrey.ToString();
        }
        int nHunter = PlayerPrefs.GetInt("nHunter");
        if (nHunter > 0) {
            hunterSlider.value = nHunter;
            hunterTMP.text = nHunter.ToString();
        }
        playing = true;
    }
}
