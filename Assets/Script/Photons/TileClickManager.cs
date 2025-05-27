using Photon.Pun;
using UnityEngine;

public class TileClickManager : MonoBehaviour
{
    [SerializeField] private Camera otherCamera;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)  // �����͸� �۵�
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = otherCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TileBehaviour tile = hit.collider.GetComponent<TileBehaviour>();
                    if (tile != null)
                    {
                        tile.ToggleColor(); // ���� ���� ���游 ����
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = otherCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TileBehaviour tile = hit.collider.GetComponent<TileBehaviour>();
                    if (tile != null)
                    {
                        tile.CToggleColor(); // Ŭ���̾�Ʈ ���� ���� ���游 ����
                    }
                }
            }
        }
    }
}
