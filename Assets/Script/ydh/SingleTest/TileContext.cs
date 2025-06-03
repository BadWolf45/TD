using TMPro;
using UnityEngine;

public class TileContext : MonoBehaviour
{
    [Header("Ÿ�� ���� ����")]
    [SerializeField] private Transform _tileParent;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private TMP_InputField _xInputField;
    [SerializeField] private TMP_InputField _zInputField;

    public Transform TileParent => _tileParent;
    public GameObject TilePrefab=> _tilePrefab;
    public int Width=> int.TryParse(_xInputField.text,out int val) ? val : 0 ;
    public int Height => int.TryParse(_zInputField.text, out int val) ? val : 0;
    public void SetDimensions(int width, int height)//�ʱ�ȭ ���� �Լ�.
    {
        _xInputField.text = width.ToString();
        _zInputField.text = height.ToString();
    }
}
