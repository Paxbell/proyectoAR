using Unity.VisualScripting;
using UnityEngine;

public class launcher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float force = 450;
    [SerializeField] private Transform pointer;

    private Camera ArCamera;

    void Start()
    {
        ArCamera = gameManager.instance.ARCamera;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            
            Vector3 InputPosicion = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;

            Ray ray = ArCamera.ScreenPointToRay(InputPosicion);

            Vector3 direccion = ray.direction;

            GameObject ball = Instantiate(ballPrefab, pointer.position, Quaternion.identity, transform);
            ball.GetComponent<Rigidbody>().AddForce(direccion.normalized * force);
        }
    }
}
