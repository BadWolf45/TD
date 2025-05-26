using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Camera otherCamera;//Freecamera ����
    public GameObject _tilePrefab;
    public Transform _tileParent;
    public TMP_InputField _xInputField;
    public TMP_InputField _zInputField;
    public Button generateButton;
    public TextMeshProUGUI tileCoordText;

    private float tileSize = 1f;
    

    void Start()
    {
        generateButton.onClick.AddListener(GenerateGrid);
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
                tile.name = $"Tile_{x}_{z}";
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
                if(tile != null)
                {

                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                TileBehaviour tile = hit.collider.GetComponent<TileBehaviour>();
                if (tile != null)
                {

                }
            }
        }
    }
}
