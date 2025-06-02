using Photon.Pun;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TileContext tileContext;
    [SerializeField] private GameObject monsterPrefab;

    public void SpawnEnemy()
    {
        TileBehaviour startTile = null;

        foreach (Transform child in tileContext.TileParent)
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
            Debug.LogWarning("StartPoint�� �������� �ʾҽ��ϴ�.");
            return;
        }

        Vector3 spawnPos = startTile.transform.position + Vector3.up * 0.5f;
        GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        // MonsterMover�� ã�� ��� �̵� ����
        MonsterMover mover = monster.GetComponent<MonsterMover>();
        if (mover != null)
        {
            mover.MoveByPathfinding();
        }
        else
        {
            Debug.LogError("Monster �����տ� MonsterMover ��ũ��Ʈ�� �����ϴ�!");
        }
    }
}
