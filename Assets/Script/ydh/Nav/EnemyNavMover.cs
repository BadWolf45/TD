using UnityEngine.AI;
using UnityEngine;
using System.Linq;
using System.Collections;

public class EnemyNavMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(InitializeNavMesh());
    }

    private IEnumerator InitializeNavMesh()
    {
        yield return new WaitForSeconds(0.1f); // NavMesh가 빌드될 시간을 줌

        // NavMesh 위에 위치 조정
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        if (!_agent.isOnNavMesh)
        {
            Debug.LogError($"? NavMeshAgent가 NavMesh 위에 없습니다! 위치: {transform.position}");
            yield break;
        }

        var endTile = Object.FindObjectsByType<TileBehaviour>(FindObjectsSortMode.None)
            .FirstOrDefault(t => t._tileState == TileState.EndPoint);

        if (endTile != null)
        {
            Vector3 destination = endTile.transform.position + Vector3.up * 0.5f;
            Debug.Log($"목적지: {destination}");
            _agent.SetDestination(destination);
        }
        else
        {
            Debug.LogWarning("EndPoint가 없습니다.");
        }
    }
}
