using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;

    public void BakeNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
        Debug.Log("NavMesh ºôµå ¿Ï·á");
    }
}
