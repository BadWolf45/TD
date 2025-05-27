using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Camera otherCamera;//Freecamera ����
    [SerializeField] private TMP_InputField saveFileInputName;
    [SerializeField] private TMP_InputField loadFileInputName;
    public GameObject _tilePrefab;
    public Transform _tileParent;
    public TMP_InputField _xInputField;
    public TMP_InputField _zInputField;
    public Button _generateButton;
    public TextMeshProUGUI tileCoordText;
    private TileEditManager tileEditManager;
    private float tileSize = 1f;
    

    void Start()
    {
        _generateButton.onClick.AddListener(GenerateGrid);
        tileEditManager = GetComponent<TileEditManager>();
    }
    public void GenerateGrid()
    {
        foreach(Transform child in _tileParent)
        {
            Destroy(child.gameObject);//���� Ÿ���� �����մϴ�.
        }
        if (!int.TryParse(_xInputField.text, out int xCount)) return;
        if (!int.TryParse(_zInputField.text, out int zCount)) return;
        for(int x=0;x < xCount; x++)
        {
            for (int z = 0; z < zCount; z++)
            {
                Vector3 pos = new Vector3(x + 0.5f, 0, z + 0.5f);
                GameObject tile = Instantiate(_tilePrefab, pos, Quaternion.identity, _tileParent);
                tile.name = $"Tile_ {x}_{z}";
            }
        }
    }

    void Update()
    {
        Ray ray = otherCamera.ScreenPointToRay((Input.mousePosition));
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.point;
            int x = Mathf.FloorToInt(pos.x);//FloorToInt�� �Ҽ��� ���ϸ� �����ϴ�. �� ���� ����� ������ �����մϴ�.
            int z = Mathf.FloorToInt(pos.z);
            tileCoordText.text = $"Tile Coord: {x}, {z}";
            if (Input.GetMouseButtonDown(0))
            {
                TileBehaviour tile = hit.collider.GetComponent<TileBehaviour>();
                if (tile == null) return;

                switch (tileEditManager.currentMode)
                {
                    case TileState.Installable:
                        tile.SetTileState(TileState.Installable);
                        break;

                    case TileState.Uninstallable:
                        tile.SetTileState(TileState.Uninstallable);
                        break;
                }
            }
        }
    }
    public void SaveGridToJson()
    {
        GridData gridData = new GridData();

        gridData.width = int.Parse(_xInputField.text);
        gridData.height = int.Parse(_zInputField.text);

        foreach (Transform tile in _tileParent)
        {
            TileBehaviour tileBehaviour = tile.GetComponent<TileBehaviour>();
            if (tileBehaviour != null)
            {
                string[] parts = tile.name.Split('_');
                int x = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);

                Debug.Log($"[�����] {tile.name} �� ����: {tileBehaviour._tileState}");

                gridData.tiles.Add(new TileData
                {
                    x = x,
                    z = z,
                    state = tileBehaviour._tileState
                });
            }
        }
        //foreach (Transform tile in _tileParent)
        //{
        //    TileBehaviour tileBehaviour = tile.GetComponent<TileBehaviour>();
        //    if (tileBehaviour != null)
        //    {
        //        Vector3 pos = tile.position;
        //        TileState currentState = tileBehaviour._tileState;
        //        Debug.Log($"[�����] Ÿ�� ��ġ: ({pos.x}, {pos.z}) �� ����: {currentState}");
        //        gridData.tiles.Add(new TileData
        //        {
        //            x = Mathf.FloorToInt(pos.x),
        //            z = Mathf.FloorToInt(pos.z),
        //            state = tileBehaviour._tileState
        //        });
        //    }
        //}

        // ����ڰ� �Է��� ���ϸ� ó��
        string fileName = saveFileInputName.text;
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogWarning("���ϸ��� ��� �־� �⺻ �̸����� �����մϴ�.");
            fileName = "map_data.json";
        }
        else if (!fileName.EndsWith(".json"))
        {
            fileName += ".json";
        }

        string json = JsonUtility.ToJson(gridData, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log($"�� ���� �Ϸ�: {path}");
    }
    public void LoadGridFromJson()
    {
        string fileName = loadFileInputName.text;
        if (!fileName.EndsWith(".json"))
            fileName += ".json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"������ �������� �ʽ��ϴ�: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        GridData gridData = JsonUtility.FromJson<GridData>(json);

        // ���� �׸��� ����
        foreach (Transform child in _tileParent)
            Destroy(child.gameObject);

        // Ÿ�� ���� ������ dictionary�� ���� (���� lookup�� ����)
        Dictionary<string, TileState> tileStates = new();
        foreach (TileData tileData in gridData.tiles)
        {
            string key = $"Tile_{tileData.x}_{tileData.z}";
            tileStates[key] = tileData.state;
        }

        // �׸��� ������ ���ÿ� ���� ����
        for (int x = 0; x < gridData.width; x++)
        {
            for (int z = 0; z < gridData.height; z++)
            {
                Vector3 pos = new Vector3(x + 0.5f, 0, z + 0.5f);
                string tileName = $"Tile_{x}_{z}";
                GameObject tile = Instantiate(_tilePrefab, pos, Quaternion.identity, _tileParent);
                tile.name = tileName;

                TileBehaviour tileBehaviour = tile.GetComponent<TileBehaviour>();
                if (tileBehaviour != null)
                {
                    TileState state = tileStates.TryGetValue(tileName, out var s) ? s : TileState.None;
                    tileBehaviour.SetTileState(state);
                    Debug.Log($"[���� + ���� ����] {tileName} �� ����: {state}");
                }
            }
        }

        Debug.Log($"�� �ε� �Ϸ�: {path}");
    }

}
