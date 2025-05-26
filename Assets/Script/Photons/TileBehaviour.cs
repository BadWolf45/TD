using Photon.Pun;
using System.Diagnostics;
using UnityEngine;
public class TileBehaviour : MonoBehaviourPun
{
    private Renderer _renderer;
    private Color _originalColor;
    private bool _isSelected = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }
    //public void ToggleColorLocalOnly()
    //{
    //    _isSelected = !_isSelected;
    //    _renderer.material.color = _isSelected ? Color.green : _originalColor;
    //    Color currentColor = _renderer.material.color;
    //    Debug.Log($"���� ��Ƽ���� ����: {currentColor}");
    //}
    [PunRPC]
    //public void ToggleColorRPC()
    //{
    //    _isSelected = !_isSelected;
    //    Color newColor = _isSelected ? Color.green : _originalColor;
    //    _renderer.material.color = newColor;
    //    Debug.Log($"[TileBehaviour] ���� �����: {newColor}");
    //}
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
}
