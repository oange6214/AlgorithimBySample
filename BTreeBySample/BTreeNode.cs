namespace AlgorithimBySample;

internal class BTreeNode<T> where T : IComparable<T>
{
    public List<T> Keys { get; set; }
    public List<BTreeNode<T>> Children { get; set; }
    public bool IsLeaf { get; set; }
    public BTreeNode()
    {
        Keys = new List<T>();
        Children = new List<BTreeNode<T>>();
        IsLeaf = true;
    }
}
