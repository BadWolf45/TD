using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TileContext _tileContext;
    [SerializeField] private GameObject _enemyPrefab;

    public void SpawnEnemy()
    {
        TileBehaviour startTile = null;

        foreach (Transform child in _tileContext.TileParent)
        {
            var tile = child.GetComponent<TileBehaviour>();
            if (tile != null && tile._tileState == TileState.StartPoint)
            {
                startTile = tile;
                break;
            }
        }

        if (startTile == null)
        {
            Debug.LogWarning("StartPoint�� �����ϴ�.");
            return;
        }

        Vector3 desiredSpawnPos = startTile.transform.position + Vector3.up * 0.5f;

        // NavMesh �� ��ġ�� ���� ��ġ ����
        if (NavMesh.SamplePosition(desiredSpawnPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("StartPoint �ֺ��� NavMesh�� �����ϴ�. ���͸� ������ �� �����ϴ�.");
        }
    }
}
