using System.Numerics;
using MathTypes;
using Unity.Mathematics;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject ARPrefab;
    [SerializeField] private float spawnIntervalo = 10.0f;

    private Camera ArCamera;
    private MeshRenderer meshRender;
    private MeshFilter meshFilter;

    private int minVertex = 250;
    private float timeSinceSpawn = 11.0f;
    private float normalHorizontalTolerance = 0.9f;
    private float minDisCenter = 0.15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        ArCamera = gameManager.instance.ARCamera;
    }

    // Update is called once per frame
    void Update()
    {
        int vertexCount = meshFilter.sharedMesh.vertexCount;
        if (vertexCount < minVertex)
        {
            return;
        }
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnIntervalo && visibleCamara())
        {
            spawnearObjecto();
        }
    }

    private bool visibleCamara()
    {
        UnityEngine.Plane[] planes = GeometryUtility.CalculateFrustumPlanes(ArCamera);
        return GeometryUtility.TestPlanesAABB(planes, meshRender.bounds);

    }

    private void spawnearObjecto()
    {
        Mesh mesh = meshFilter.sharedMesh;
        int indexVertex = UnityEngine.Random.Range(0, mesh.vertexCount);
        UnityEngine.Vector3 normal = mesh.normals[indexVertex];
        bool isHorizontal = normal.y > normalHorizontalTolerance;
        UnityEngine.Vector3 VertexPosicion = mesh.vertices[indexVertex];

        UnityEngine.Vector3 globalPosicion = transform.TransformPoint(VertexPosicion);
        bool isCloseTOorigen = globalPosicion.y < minDisCenter;

        if (isHorizontal && isCloseTOorigen)
        {
            UnityEngine.Vector3 randomRot = UnityEngine.Vector3.up * UnityEngine.Random.Range(0, 360);

            GameObject arObject = Instantiate(ARPrefab, globalPosicion, UnityEngine.Quaternion.Euler(randomRot), transform);

            timeSinceSpawn = 0;
        }


    }

}
