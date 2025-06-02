using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    private TileContext tileContext;
    private TileBehaviour[,] tiles;
    private int width, height;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        tileContext = GameObject.Find("GridSystem").GetComponent<TileContext>();
    }

    private void OnEnable()
    {
        TileBehaviour.OnAnyTileChanged += HandleTileChanged;
    }

    private void OnDisable()
    {
        TileBehaviour.OnAnyTileChanged -= HandleTileChanged;
    }

    private void HandleTileChanged()
    {
        StopCurrentMovement();
        MoveByPathfinding();
    }

    public void MoveByPathfinding()
    {
        width = tileContext.Width;
        height = tileContext.Height;
        tiles = new TileBehaviour[width, height];

        foreach (Transform tile in tileContext.TileParent)
        {
            var tileBehaviour = tile.GetComponent<TileBehaviour>();
            if (tileBehaviour == null) continue;

            string[] parts = tile.name.Split('_');
            int x = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);

            tiles[x, z] = tileBehaviour;
        }

        // ���� ��ġ �� ���������� ���
        Vector3 pos = transform.position;
        int startX = Mathf.FloorToInt(pos.x);
        int startZ = Mathf.FloorToInt(pos.z);
        Vector2Int start = new(startX, startZ);

        // ���� Ÿ�� Ž��
        Vector2Int? end = null;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (tiles[x, z]._tileState == TileState.EndPoint)
                {
                    end = new Vector2Int(x, z);
                    break;
                }
            }
            if (end != null) break;
        }

        if (end == null)
        {
            Debug.LogWarning("[MonsterMover] ���� Ÿ���� ã�� ���߽��ϴ�.");
            return;
        }

        // ��� Ž��
        AStarPathfinder pathfinder = new AStarPathfinder(tiles, width, height);
        List<Vector2Int> path = pathfinder.FindPath(start, end.Value);

        if (path == null)
        {
            Debug.LogWarning("[MonsterMover] ��θ� ã�� ���߽��ϴ�.");
            return;
        }

        // ��θ� ���� ��ǥ�� ��ȯ
        List<Vector3> worldPath = new();
        foreach (var tilePos in path)
        {
            Vector3 world = new Vector3(tilePos.x + 0.5f, 0, tilePos.y + 0.5f);
            worldPath.Add(world);
        }

        // �̵� ����
        moveCoroutine = StartCoroutine(FollowPath(worldPath));
    }

    private IEnumerator FollowPath(List<Vector3> path)
    {
        foreach (var targetPos in path)
        {
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 2f * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void StopCurrentMovement()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }
}
