using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PathManager : MonoBehaviour
{
    [SerializeField] private TileContext _tileContext;

    public List<TileBehaviour> CalculatePath()
    {
        TileBehaviour startTile = null;
        TileBehaviour endTile = null;
        List<TileBehaviour> pathTiles = new();

        foreach (Transform child in _tileContext.TileParent)
        {
            var tile = child.GetComponent<TileBehaviour>();
            if (tile == null) continue;

            switch (tile._tileState)
            {
                case TileState.StartPoint:
                    startTile = tile;
                    break;
                case TileState.EndPoint:
                    endTile = tile;
                    break;
                case TileState.Installable://��ΰ� �� Ÿ��
                    pathTiles.Add(tile);
                    break;
            }
        }

        if (startTile == null || endTile == null)
        {
            Debug.LogWarning("StartPoint �Ǵ� EndPoint�� �������� �ʾҽ��ϴ�.");
            return null;
        }

        pathTiles = pathTiles
            .OrderBy(t => Vector3.Distance(startTile.transform.position, t.transform.position))// ���������� ����� ������ ����
            .ToList();

        pathTiles.Insert(0, startTile);
        pathTiles.Add(endTile);
        return pathTiles;
    }
}
