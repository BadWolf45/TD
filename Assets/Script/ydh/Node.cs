using UnityEngine;

public class Node : IHeapItem<Node>//Node�� ��� Ž���� ����, �� Ŭ������ �켱���� ť�� �� �� �ִ�.
{
    public Vector2Int Position;//�ʻ󿡼� �� ��尡 � ��ġ���� ����.ex)3,5���
    public float G, H;//G:���������� �� �������� ���� ��� ��� H:�� ��忡�� ��ǥ ���������� ���� ���(�޸���ƽ)
    public Node Parent;//��θ� ������ ������ �� �θ� ��带 ���󰩴ϴ�. �� ��尡 ��𿡼� �Դ����� �����ϱ� ���� ���Դϴ�.
    public int HeapIndex { get; set; } //priorityQueue<T> �ȿ��� �ڽ��� �� ��° �ε����� �ִ��� ����մϴ�. �� ���� ���� �� ������ ��ġ�� ��ȯ�ϱ� ���� �ʿ��մϴ�.
    public Node(Vector2Int pos,float g, float h, Node parent)//Node�� ������ �� �ʱ�ȭ �ϴ� ������ �Դϴ�.
    {
        Position = pos;
        G = g;
        H = h;
        Parent = parent;
    }
    public float F => G + H; //F ���� A* �˰����� �켱���� �����Դϴ�. �� ����� "��ü ���� ���"����, �������� �켱 Ž���˴ϴ�.

    public int CompareTo(Node other)//�켱���� ť���� ������ ������ �����մϴ�. F ���� ���� ���� �켱�̹Ƿ�, F������ ���մϴ�.IComparable<T> �������̽� �����Դϴ�.
    {
        return F.CompareTo(other.F);
    }
}
