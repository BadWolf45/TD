using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private TileContext _tileContext;
    [SerializeField] private TMP_InputField FileName;
    [SerializeField] private NavMeshController _navMeshController;

    private string SaveFileName => string.IsNullOrWhiteSpace(FileName.text)
        ? "map_data.json" : AppendJson(FileName.text);
    private string LoadFileName => AppendJson(FileName.text);
    private string AppendJson(string fileName) =>
        fileName.EndsWith(".json") ? fileName : fileName + ".json";

    public void SaveGridToJson()
    {
        GridData gridData = new GridData
        {
            width = _tileContext.Width,
            height = _tileContext.Height
        };

        foreach (Transform tile in _tileContext.TileParent)
        {
            TileBehaviour tileBehaviour = tile.GetComponent<TileBehaviour>();
            if (tileBehaviour != null)
            {
                string[] parts = tile.name.Split('_');
                int x = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);

                gridData.tiles.Add(new TileData
                {
                    x = x,
                    z = z,
                    state = tileBehaviour._tileState
                });
            }
        }

        string path = Path.Combine(Application.persistentDataPath, SaveFileName);
        File.WriteAllText(path, JsonUtility.ToJson(gridData, true));

        Debug.Log($"�� ���� �Ϸ�: {path}");
    }

    public void LoadGridFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, LoadFileName);
        if (!File.Exists(path))
        {
            Debug.LogError($"������ �������� �ʽ��ϴ�: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        GridData gridData = JsonUtility.FromJson<GridData>(json);

        foreach (Transform child in _tileContext.TileParent)
            Destroy(child.gameObject);

        Dictionary<string, TileState> tileStates = new();
        foreach (TileData tileData in gridData.tiles)
        {
            tileStates[$"Tile_{tileData.x}_{tileData.z}"] = tileData.state;
        }

        for (int x = 0; x < gridData.width; x++)
        {
            for (int z = 0; z < gridData.height; z++)
            {
                Vector3 pos = new Vector3(x + 0.5f, 0, z + 0.5f);
                string tileName = $"Tile_{x}_{z}";
                GameObject tile = Instantiate(_tileContext.TilePrefab, pos, Quaternion.identity, _tileContext.TileParent);
                tile.name = tileName;

                var tileBehaviour = tile.GetComponent<TileBehaviour>();
                if (tileBehaviour != null)
                {
                    tileBehaviour.SetTileState(tileStates.TryGetValue(tileName, out var s) ? s : TileState.None);
                }
            }
        }
        _navMeshController.BakeNavMesh();
        Debug.Log($"�� �ε� �Ϸ�: {path}");
    }
}