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
        yield return new WaitForSeconds(0.1f); // NavMesh�� ����� �ð��� ��

        // NavMesh ���� ��ġ ����
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        if (!_agent.isOnNavMesh)
        {
            Debug.LogError($"? NavMeshAgent�� NavMesh ���� �����ϴ�! ��ġ: {transform.position}");
            yield break;
        }

        var endTile = Object.FindObjectsByType<TileBehaviour>(FindObjectsSortMode.None)
            .FirstOrDefault(t => t._tileState == TileState.EndPoint);

        if (endTile != null)
        {
            Vector3 destination = endTile.transform.position + Vector3.up * 0.5f;
            Debug.Log($"������: {destination}");
            _agent.SetDestination(destination);
        }
        else
        {
            Debug.LogWarning("EndPoint�� �����ϴ�.");
        }
    }
}
