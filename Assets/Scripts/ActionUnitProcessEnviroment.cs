using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUnitProcessEnviroment : MonoBehaviour
{
    [System.Serializable]
    public struct Enemy { 
        public Transform transform;
        public bool knowLocation;

        public Enemy(Transform t, bool seen)
        {
            transform = t;
            knowLocation = seen;
        }
    }

    public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddEnemy(Transform t, bool seen)
    {
        foreach(Enemy e in enemies)
        {
            if (e.transform.Equals(t))
            {
                return false;
            }
        }
        enemies.Add(new Enemy(t, seen));
        return true;
    }

    public bool KnowsLocation()
    {
        foreach (Enemy e in enemies)
        {
            if (e.knowLocation == true)
            {
                return true;
            }
        }
        return false;
    }
}
