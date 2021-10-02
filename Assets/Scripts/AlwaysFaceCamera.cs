using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
    }
}
