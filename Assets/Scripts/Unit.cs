using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

/**
 * Clase principal de los personajes. Almacena información como la distacia de escucha o la estamina. Requiere un nav  
 * mesh agent al que el arbol de decisiones accede para efectuar movimientos.
 */

public class Unit : MonoBehaviour
{
    public UnitGUI gui;

    public Transform target;
    private TargetList targetList;

    public bool isBusy = false;
    public bool isInPanic = false;
    //Resources
    [Header("Resources")]
    public float maxStamina = 100;

    private float stamina = 100;


    [Range(0, 360)]
    public float viewAngle;
    public float viewRadious;
    public float listeningDistance = 10;
    public float viewAngleBase;
    public float viewRadiousBase;
    public float listeningDistanceBase = 10;

    //Effects
    [Header("Effects")]
    public ParticleSystem restParticles;
    public ParticleSystem panicParticles;
    public Transform deathParticles;

    private void Awake()
    {
        gui = GetComponent<UnitGUI>();
        targetList = new TargetList();
    }
    // Start is called before the first frame update
    void Start()
    {

       
        stamina = maxStamina;
        viewAngleBase = viewAngle;
        viewRadiousBase = viewRadious;
        listeningDistanceBase = listeningDistance;
    }

    public void IncreaseStamina(float increase)
    {
        stamina += increase;
        if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    public float getStamina()
    {
        return stamina;
    }

    public void DecreaseStamina(ActionUnit action)
    {
        DecreaseStamina(action.staminaCost * Time.deltaTime);
    }

    public void DecreaseStamina(float decrease)
    {
        stamina -= decrease;
        if(stamina < 0)
        {
            stamina = 0;
        }
    }

    public void ModifySenses(float listeningDistanceIncrease, float viewAngleIncrease, float viewRadiousIncrease)
    {
        listeningDistance = listeningDistanceBase + listeningDistanceIncrease;
        viewAngle = viewAngleBase + viewAngleIncrease;
        viewRadious = viewRadiousBase + viewRadiousIncrease;
    }

    public void StartPanic()
    {
        panicParticles.Play();
        isInPanic = true;
    }

    internal void AddTarget(Transform t, bool knowsLocation)
    {
        targetList.AddTarget(t, knowsLocation);
    }

    public Transform GetTarget()
    {
        return targetList.currentTarget;
    }

    public bool KnowsTargetPos()
    {
        return targetList.KnowsPos();
    }

    public Transform GetClosestTarget()
    {
        targetList.SetNearestAsCurrent(transform);
        return targetList.currentTarget;
    }

    public Transform GetClosestVisibleTarget()
    {
        targetList.SetNearestVisibleAsCurrent(transform);
        return targetList.currentTarget;
    }

    public void RemoveTarget()
    {
        targetList.RemoveTarget();
        if (!targetList.isEmpty())
        {
            targetList.SetNearestAsCurrent(transform);
        }
    }

    public void RenewTarget()
    {
        targetList.SetNearestAsCurrent(transform);
    }

    public void Kill()
   {
        gui.Delete();
        GameManager.instance.PreyKilled(transform);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        isBusy = true;
        if(deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, deathParticles.rotation);
        }

        Destroy(gameObject,3);
    }

    public void TargetKilled(Transform t)
    {
        targetList.TryToRemoveTarget(t);
    }
}
