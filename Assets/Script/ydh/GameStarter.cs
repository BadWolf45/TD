using Photon.Pun;
using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviourPun
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TileContext tileContext;
    public void OnClickStartButton()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        photonView.RPC(nameof(RPC_Spawn), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Spawn()
    {
        enemySpawner.SpawnEnemy();
    }

    private IEnumerator WaitUntilGridReady()
    {
        // ����: Ÿ���� �ϳ��� ������ ������ ���
        yield return new WaitUntil(() => tileContext.TileParent.childCount > 0);

        yield return new WaitForSeconds(0.1f); // �ణ�� ���� ���

        enemySpawner.SpawnEnemy();
    }
}
