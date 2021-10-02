using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/**
 * Clase padre de mis acciones propias que guarda informacion sobre una unidad
 */
public class ActionUnit : IAction
{

    protected NavMeshAgent agent;
    protected Unit unit;
    public float staminaCost = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        Initialice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected void Initialice()
    {
        unit = transform.root.GetComponent<Unit>();
        agent = unit.GetComponent<NavMeshAgent>();
    }
}
