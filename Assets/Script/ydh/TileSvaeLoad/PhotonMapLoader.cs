using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonMapLoader : MonoBehaviourPunCallbacks
{
    [SerializeField] private FirebaseMapDownloader downloader;
    [SerializeField] private GridManager gridManager;

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // "MapName"이 갱신됐는지 확인
        if (propertiesThatChanged.ContainsKey("MapName"))
        {
            string mapName = propertiesThatChanged["MapName"] as string;
            Debug.Log($"[PhotonMapLoader] MapName 변경 감지: {mapName}");
            StartCoroutine(LoadMapAndSync(mapName));
        }
    }

    public IEnumerator LoadMapAndSync(string mapName)
    {
        var downloadTask = downloader.DownloadMap(mapName + ".json");
        while (!downloadTask.IsCompleted) yield return null;

        string json = downloadTask.Result;
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("맵 데이터가 비어 있음");
            yield break;
        }

        photonView.RPC("RPC_LoadMapJson", RpcTarget.AllBuffered, json);
    }

    [PunRPC]
    private void RPC_LoadMapJson(string json)
    {
        gridManager.LoadMapFromFirebase(json);
    }
}

