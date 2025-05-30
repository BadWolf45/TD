using UnityEngine;

public class Node : IHeapItem<Node>//Node는 경로 탐색용 정점, 이 클래스가 우선순위 큐에 들어갈 수 있다.
{
    public Vector2Int Position;//맵상에서 이 노드가 어떤 위치인지 저장.ex)3,5노드
    public float G, H;//G:시작점에서 이 노드까지의 실제 경로 비용 H:이 노드에서 목표 지점까지의 예상 비용(휴리스틱)
    public Node Parent;//경로를 역으로 추적할 때 부모 노드를 따라갑니다. 이 노드가 어디에서 왔는지를 추적하기 위한 것입니다.
    public int HeapIndex { get; set; } //priorityQueue<T> 안에서 자신이 몇 번째 인덱스에 있는지 기록합니다. 이 값은 정렬 시 빠르게 위치를 교환하기 위해 필요합니다.
    public Node(Vector2Int pos,float g, float h, Node parent)//Node를 생성할 때 초기화 하는 생성자 입니다.
    {
        Position = pos;
        G = g;
        H = h;
        Parent = parent;
    }
    public float F => G + H; //F 값은 A* 알고리즘의 우선순위 기준입니다. 이 노드의 "전체 예상 비용"으로, 작을수록 우선 탐색됩니다.

    public int CompareTo(Node other)//우선순위 큐에서 정렬할 기준을 제공합니다. F 값이 작은 것이 우선이므로, F값끼리 비교합니다.IComparable<T> 인터페이스 구현입니다.
    {
        return F.CompareTo(other.F);
    }
}
