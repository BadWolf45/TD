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
            Debug.LogWarning("StartPoint가 없습니다.");
            return;
        }

        Vector3 desiredSpawnPos = startTile.transform.position + Vector3.up * 0.5f;

        // NavMesh 위 위치로 스폰 위치 보정
        if (NavMesh.SamplePosition(desiredSpawnPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("StartPoint 주변에 NavMesh가 없습니다. 몬스터를 생성할 수 없습니다.");
        }
    }
}
