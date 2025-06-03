using Photon.Pun;
using UnityEngine;

public class PhotonMapLoader : MonoBehaviourPunCallbacks
{
    [SerializeField] private FirebaseMapDownloader downloader;
    [SerializeField] private GridManager gridManager;

    public async void LoadMapAndSync(string mapName)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        string fileName = mapName + ".json";
        string json = await downloader.DownloadMap(fileName);

        if (!string.IsNullOrEmpty(json))
        {
            photonView.RPC(nameof(RPC_LoadMap), RpcTarget.AllBuffered, json);//���� Ŭ���̾�Ʈ + ���� ������ Ŭ���̾�Ʈ���Ե� ����� (���ۿ� �����)
        }
    }

    [PunRPC]
    private void RPC_LoadMap(string json)
    {
        gridManager.LoadMapFromFirebase(json);
    }
}
