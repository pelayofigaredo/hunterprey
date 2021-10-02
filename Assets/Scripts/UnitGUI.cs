using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(DisplayFieldOfView))]
/**
 * Clase que gestione el paso de información entre Unit y la interfaz grafica, mostrando y ocultando el panel de informacion y el renderizado de 
 * sentidos y caminos.
 */
public class UnitGUI : MonoBehaviour
{
    private Unit unit;
    private NavMeshAgent navMeshAgent;

    //GUI
    public bool displayPath = false;
    public LineRenderer pathRenderer;
    public bool displayStats = false;
    public GameObject GUIGO;
    public Slider staminaSlider;
    public TextMeshProUGUI actionNameTMP;
    public string actionName = "";
    public Transform listeningSphere;
    private DisplayFieldOfView fieldOfViewDisplay;
    public TextMeshProUGUI nameTMP;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
        fieldOfViewDisplay = GetComponent<DisplayFieldOfView>();

        nameTMP.text = gameObject.name;

        GameManager.instance.TogglePathRendering += OnPathRenderToggled;
        GameManager.instance.ToggleStatsRendering += OnStatsRenderToggled;
        GameManager.instance.ToggleSensesRendering += OnSensesRenderToggled;
    }

    void Update()
    {
        if (displayPath)
        {
            pathRenderer.positionCount = navMeshAgent.path.corners.Length;
            pathRenderer.SetPositions(navMeshAgent.path.corners);
        }
        if (displayStats)
        {
            staminaSlider.value = unit.getStamina() / unit.maxStamina;
            actionNameTMP.text = actionName;
        }
        fieldOfViewDisplay.viewAngle = unit.viewAngle;
        fieldOfViewDisplay.viewRadious = unit.viewRadious;
        listeningSphere.transform.localScale = Vector3.one * 2 * unit.listeningDistance;
    }

    void OnPathRenderToggled(bool display)
    {
        displayPath = display;
        if (displayPath)
        {
            pathRenderer.enabled = true;
        }
        else
        {
            pathRenderer.enabled = false;
        }
    }

    void OnStatsRenderToggled(bool display)
    {
        displayStats = display;
        if (displayStats)
        {
            GUIGO.SetActive(true);
        }
        else
        {
            GUIGO.SetActive(false);
        }
    }

    void OnSensesRenderToggled(bool display)
    {
        bool displaySenses = display;
        if (displaySenses)
        {
            listeningSphere.GetComponent<Renderer>().enabled = true;
            fieldOfViewDisplay.SetShow(true);
        }
        else
        {
            listeningSphere.GetComponent<Renderer>().enabled = false;
            fieldOfViewDisplay.SetShow(false);
        }
    }

    public void Delete()
    {
        OnSensesRenderToggled(false);
        OnPathRenderToggled(false);
        OnStatsRenderToggled(false);
        GameManager.instance.TogglePathRendering -= OnPathRenderToggled;
        GameManager.instance.ToggleStatsRendering -= OnStatsRenderToggled;
        GameManager.instance.ToggleSensesRendering -= OnSensesRenderToggled;
    }
    }
