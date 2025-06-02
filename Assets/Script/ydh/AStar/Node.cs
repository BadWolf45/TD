using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Vector2Int Position;
    public float G, H;
    public Node Parent;
    public int HeapIndex { get; set; }

    public Node(Vector2Int pos, float g, float h, Node parent)
    {
        Position = pos;
        G = g;//������ �� ���� ������ ������ �� �Ÿ�
        H = h;//���� ��� �� ��ǥ �������� ���� �Ÿ� (�޸���ƽ)
        Parent = parent;
    }

    public float F => G + H;//���� ���� F�� �������� Ž�� �켱���� ����

    public int CompareTo(Node other)
    {
        return F.CompareTo(other.F);
    }
}