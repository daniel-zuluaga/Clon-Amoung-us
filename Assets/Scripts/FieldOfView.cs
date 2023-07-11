using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float fov = 90f;
    [SerializeField] private int numberAristas = 90;
    [SerializeField] private float anguloInicial = 0f;
    [SerializeField] private float distanciaVision = 8f;
    [SerializeField] private LayerMask layerChocaRayos;

    [SerializeField] private Mesh _mesh;
    private Vector3 origin;
    [SerializeField] private Vector3[] vertices;
    [SerializeField] private int[] triangulos;


    void Awake()
    {
        _mesh = new Mesh();
        _mesh.Clear();
        GetComponent<MeshFilter>().mesh = _mesh;
        origin = Vector3.zero;
    }

    private void FixedUpdate()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        float anguloActual = anguloInicial;
        float incrementoAngulo = fov / numberAristas;

        vertices = new Vector3[numberAristas + 2];
        triangulos = new int[numberAristas * 3];

        vertices[0] = origin;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i < numberAristas; i++)
        {
            Vector3 verticeActual;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(anguloActual), distanciaVision, layerChocaRayos);

            if (raycastHit2D.collider == null)
            {
                verticeActual = origin + GetVectorFromAngle(anguloActual) * distanciaVision;
            }
            else
            {
                verticeActual = raycastHit2D.point;
            }

            vertices[indiceVertices] = verticeActual;

            if (i > 0)
            {
                triangulos[indiceTriangulos + 0] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;

                indiceTriangulos += 3;
            }

            indiceVertices++;
            anguloActual -= incrementoAngulo;
        }

        // Formamos el último triángulo
        triangulos[indiceTriangulos + 0] = 0;
        triangulos[indiceTriangulos + 1] = indiceVertices - 1;
        triangulos[indiceTriangulos + 2] = 1;


        _mesh.vertices = vertices;
        _mesh.triangles = triangulos;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 newOrigin)
    {
        origin = newOrigin;
    }
}
