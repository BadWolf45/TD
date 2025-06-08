using UnityEngine;
using Photon.Pun;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    private void TryPlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        TileBehaviour tile = hit.collider.GetComponent<TileBehaviour>();
        if (tile == null) return;

        // �̹� ��ġ�� Ÿ���� ����
        if (tile._tileState == TileState.Uninstallable)
        {
            Debug.Log("? �̹� ��ġ�� Ÿ���Դϴ�.");
            return;
        }

        // ��ġ ������ �������� Ȯ��
        bool isInstallable = tile._tileState == TileState.Installable ||
                             tile._tileState == TileState.MasterInstallable ||
                             tile._tileState == TileState.ClientInstallable;

        if (!isInstallable)
        {
            Debug.Log("? ��ġ �Ұ����� �����Դϴ�.");
            return;
        }

        // ���� Ȯ��
        bool isMaster = PhotonNetwork.IsMasterClient;
        bool hasAccess = tile._accessType switch
        {
            TileAccessType.Everyone => true,
            TileAccessType.MasterOnly => isMaster,
            TileAccessType.ClientOnly => !isMaster,
            _ => false
        };

        if (!hasAccess)
        {
            Debug.Log("? Ÿ�Ͽ� ���� ��ġ ������ �����ϴ�.");
            return;
        }

        // ��ġ ����
        Vector3 spawnPos = tile.transform.position + Vector3.up * 0.5f;
        PhotonNetwork.Instantiate(_towerPrefab.name, spawnPos, Quaternion.identity);

        // Ÿ�� ���¸� 'Installed'�� ���� (��� Ŭ���̾�Ʈ ����ȭ)
        tile.photonView.RPC(nameof(TileBehaviour.RPC_SetTileState), RpcTarget.AllBuffered,
            (int)TileState.Installed, (int)tile._accessType);
    }
}
//Ÿ���̳� ���� ��ġ�� ���� ������Ʈ�� IsMine == false�� �� ����

//�� ������ PhotonNetwork.Instantiate()�� �������� �ʰ�, ������ Ŭ���̾�Ʈ�� ���� ���� ������Ʈ�̱� ����
