using Photon.Pun;
using System;
using System.Diagnostics;
using UnityEngine;
public class TileBehaviour : MonoBehaviourPun
{
    public TileState _tileState { get; private set; } = TileState.None;
    public TileAccessType _accessType { get; private set; } = TileAccessType.Everyone;
    private Renderer _renderer;
    private Color _originalColor;
    private bool _isSelected = false;

    public static event Action OnAnyTileChanged;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        UnityEngine.Debug.Log($"[TileBehaviour] ViewID: {photonView.ViewID}");
    }
    public void SetTileState(TileState state)
    {
        SetTileState(state, TileAccessType.Everyone); // �⺻ ���� ����
    }

    public void SetTileState(TileState state, TileAccessType access)
    {
        _tileState = state;

        if (state == TileState.Installable)
        {
            _accessType = access;
        }
        else
        {
            // ? ��� ���� �߰�
            if (state != TileState.Installable)
            {
                UnityEngine.Debug.LogWarning($"?? {gameObject.name}: Installable Ÿ���� �ƴѵ� ���� Ÿ���� �ο��Ǿ����ϴ�. �� 'Installable Ÿ�� ���� Ÿ���� �ο��ϼ���'");
            }

            _accessType = TileAccessType.Everyone; // ������ �ʱ�ȭ
        }

        UnityEngine.Debug.Log($"[SetTileState] {gameObject.name} �� ����: {_tileState}, ����: {_accessType}");
        UpdateColor();
        OnAnyTileChanged?.Invoke();
    }

    private void UpdateColor()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();

        Color color = _tileState switch
        {
            TileState.Installable => _accessType switch
            {
                TileAccessType.Everyone => Color.blue,
                TileAccessType.MasterOnly => Color.cyan,
                TileAccessType.ClientOnly => Color.yellow,
                _ => Color.white
            },
            TileState.Uninstallable => Color.gray,
            TileState.Installed => Color.red,
            TileState.StartPoint => Color.green,
            TileState.EndPoint => Color.black,
            _ => Color.white
        };

        _renderer.material.color = color;
    }
    public void SetAccessType(TileAccessType access)
    {
        if (_tileState != TileState.Installable)
        {
            UnityEngine.Debug.LogWarning("?? Installable ���°� �ƴմϴ�. ���� ������ ������ �� �����ϴ�.");
            return;
        }

        _accessType = access;
        UnityEngine.Debug.Log($"[SetAccessType] {gameObject.name} �� ����: {_accessType}");
        UpdateColor();
    }
    [PunRPC]
    public void SetTileClickedColor(string role)
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();

        if (role == "Master")
            _renderer.material.color = Color.magenta;
        else if (role == "Client")
            _renderer.material.color = new Color(1.0f, 0.0f, 1.0f); // ����Ÿ
    }
    [PunRPC]
    public void RPC_SetTileState(int state, int access)
    {
        SetTileState((TileState)state, (TileAccessType)access);
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
}
