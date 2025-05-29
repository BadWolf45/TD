using Photon.Pun;
using System.Diagnostics;
using Unity.AI.Navigation;
using UnityEngine;
public class TileBehaviour : MonoBehaviourPun
{
    public TileState _tileState { get; private set; } = TileState.None;
    private static NavMeshController _navMeshController;
    private Renderer _renderer;
    private Color _originalColor;
    private bool _isSelected = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        UnityEngine.Debug.Log($"[TileBehaviour] ViewID: {photonView.ViewID}");
    }
    public void SetTileState(TileState newState)
    {
        _tileState = newState;
        UpdateColor();
        UpdateNavMeshModifier();
        if (_tileState == TileState.Installable)
        {
            // ���� 1ȸ�� ã�� static�� ����
            if (_navMeshController == null)
                _navMeshController = FindFirstObjectByType<NavMeshController>();

            _navMeshController?.BakeNavMesh();
        }
    }
    private void UpdateNavMeshModifier()
    {
        if (_tileState == TileState.Installable || _tileState == TileState.StartPoint || _tileState == TileState.EndPoint)
        {
            gameObject.layer = LayerMask.NameToLayer("WalkableTile");

            if (GetComponent<NavMeshModifier>() == null)
            {
                var modifier = gameObject.AddComponent<NavMeshModifier>();
                modifier.overrideArea = true;
                modifier.area = 0; // Walkable
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");

            var modifier = GetComponent<NavMeshModifier>();
            if (modifier != null)
                Destroy(modifier);
        }
    }

    private void UpdateColor()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();

        Color color = _tileState switch
        {
            TileState.Installable => Color.blue,
            TileState.Uninstallable => Color.gray,
            TileState.Installed => Color.red,
            TileState.StartPoint => Color.green,
            TileState.EndPoint => Color.black,
            _ => Color.white
        };

        UnityEngine.Debug.Log($"[UpdateColor] {gameObject.name} �� ���� ��: {color}");

        _renderer.material.color = color;

    }
    [PunRPC]
    public void SetColorRPC(bool isSelected)
    {
        _isSelected = isSelected;
        _renderer.material.color = isSelected ? Color.green : _originalColor;
        Color color = _renderer.material.color;
    }
    public void ToggleColor()
    {
        bool nextState = !_isSelected;
        photonView.RPC("SetColorRPC", RpcTarget.AllBuffered, nextState);
    }
    [PunRPC]
    public void CSetColorRPC(bool isSelected)
    {
        _isSelected = isSelected;
        _renderer.material.color = isSelected ? Color.red : _originalColor;
        Color color = _renderer.material.color;
    }
    
    public void CToggleColor()
    {
        bool nextState = !_isSelected;
        photonView.RPC("CSetColorRPC", RpcTarget.AllBuffered, nextState);
    }
    //public void ToggleColorLocalOnly()
    //{
    //    _isSelected = !_isSelected;
    //    _renderer.material.color = _isSelected ? Color.green : _originalColor;
    //    Color currentColor = _renderer.material.color;
    //    Debug.Log($"���� ��Ƽ���� ����: {currentColor}");
    //}
    //public void ToggleColorRPC()
    //{
    //    _isSelected = !_isSelected;
    //    Color newColor = _isSelected ? Color.green : _originalColor;
    //    _renderer.material.color = newColor;
    //    Debug.Log($"[TileBehaviour] ���� �����: {newColor}");
    //}
}
