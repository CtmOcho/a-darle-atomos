using UnityEngine;
using System.Collections.Generic;

public class EdgeSlide : MonoBehaviour
{
    public float slideSpeed = 0.01f;  // Velocidad de deslizamiento en el eje Y
    private List<List<int>> vertexSetsByZ;  // Lista de listas de �ndices de v�rtices por valor de Z

    private Mesh mesh;
    private Vector3[] vertices;

    void Start()
    {
        // Inicializar la lista principal
        vertexSetsByZ = new List<List<int>>();

        // Obtener el mesh del objeto
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // Llamar al m�todo para organizar los v�rtices por su valor en Z
        OrganizeVerticesByZ();
    }

    void OrganizeVerticesByZ()
    {
        // Diccionario para agrupar v�rtices por su valor en Z
        Dictionary<float, List<int>> zGroups = new Dictionary<float, List<int>>();
        float tolerance = 1e-9f;  // Tolerancia para agrupar valores de Z similares

        // Agrupar v�rtices por su valor en Z
        for (int i = 0; i < vertices.Length; i++)
        {
            float zValue = vertices[i].z;
            bool foundGroup = false;

            // Comprobar si el v�rtice encaja en un grupo existente
            foreach (var key in zGroups.Keys)
            {
                if (Mathf.Abs(zValue - key) < tolerance)
                {
                    zGroups[key].Add(i);
                    foundGroup = true;
                    break;
                }
            }

            // Si no encaja, crear un nuevo grupo
            if (!foundGroup)
            {
                zGroups[zValue] = new List<int> { i };
            }
        }

        // Ordenar los grupos por valor de Z de mayor a menor y agregar a la lista principal
        foreach (var key in zGroups.Keys)
        {
            vertexSetsByZ.Add(zGroups[key]);
        }

        vertexSetsByZ.Sort((a, b) => vertices[b[0]].z.CompareTo(vertices[a[0]].z));
/*
        // Debugging: Mostrar cu�ntos grupos se han creado y sus tama�os
        for (int i = 0; i < vertexSetsByZ.Count; i++)
        {
            Debug.Log($"Group {i} - Z Value: {vertices[vertexSetsByZ[i][0]].z.ToString("F8")}, Count: {vertexSetsByZ[i].Count}");
        }
        */
    }

    void Update()
    {
        // Modificar los v�rtices comenzando desde el grupo con el valor de Z m�s alto
        for (int groupIndex = 0; groupIndex < vertexSetsByZ.Count - 1; groupIndex++)
        {
            List<int> vertexIndices = vertexSetsByZ[groupIndex];

            // Verificar si debemos empezar a mover este grupo de v�rtices
            if (vertexIndices.Count > 0 && vertices[vertexIndices[0]].z >= vertices[vertexSetsByZ[groupIndex == 0 ? 0 : groupIndex - 1][0]].z)
            {
                for (int i = 0; i < vertexIndices.Count; i++)
                {
                    int index = vertexIndices[i];
                    vertices[index].z -= slideSpeed * Time.deltaTime;
                }
            }
        }

        // Actualizar el mesh con los nuevos v�rtices
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}