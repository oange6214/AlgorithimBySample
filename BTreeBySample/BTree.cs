namespace AlgorithimBySample
{
    internal class BTree<T> where T : IComparable<T>
    {
        private int MinimumDegree { get; set; }
        private BTreeNode<T> Root { get; set; }

        public BTree(int minimumDegree)
        {
            MinimumDegree = minimumDegree;
            Root = new BTreeNode<T>();
        }

        public void Insert(T key)
        {
            var root = Root;
            if (root.Keys.Count == 2 * MinimumDegree - 1)
            {
                var newNode = new BTreeNode<T>();
                Root = newNode;
                newNode.Children.Add(root);
                SplitChild(newNode, 0);
                InsertNonFull(newNode, key);
            }
            else
            {
                InsertNonFull(root, key);
            }
        }

        private void InsertNonFull(BTreeNode<T> node, T key)
        {
            int index = node.Keys.Count - 1;
            if (node.IsLeaf)
            {
                while (index >= 0 && key.CompareTo(node.Keys[index]) < 0)
                {
                    index--;
                }
                node.Keys.Insert(index + 1, key);
            }
            else
            {
                while (index >= 0 && key.CompareTo(node.Keys[index]) < 0)
                {
                    index--;
                }
                index++;
                if (node.Children[index].Keys.Count == 2 * MinimumDegree - 1)
                {
                    SplitChild(node, index);
                    if (key.CompareTo(node.Keys[index]) > 0)
                    {
                        index++;
                    }
                }

                InsertNonFull(node.Children[index], key);
            }
        }

        private void SplitChild(BTreeNode<T> parentNode, int childIndex)
        {
            var child = parentNode.Children[childIndex];
            var newNode = new BTreeNode<T>();

            newNode.IsLeaf = child.IsLeaf;

            int midIndex = MinimumDegree - 1;

            newNode.Keys.AddRange(child.Keys.GetRange(MinimumDegree, midIndex));
            child.Keys.RemoveRange(MinimumDegree, midIndex);

            if (!child.IsLeaf)
            {
                newNode.Children.AddRange(child.Children.GetRange(MinimumDegree, midIndex + 1));
                child.Children.RemoveRange(MinimumDegree, midIndex + 1);
            }
            
            parentNode.Keys.Insert(childIndex, child.Keys[midIndex]);
            child.Keys.RemoveAt(midIndex);
            parentNode.Children.Insert(childIndex + 1, newNode);
        }

        public void Print()
        {
            Print(Root, "");
        }

        private void Print(BTreeNode<T> node, string indent)
        {
            Console.Write(indent);
            foreach(var key in node.Keys)
            {
                Console.Write(key + " ");
            }
            Console.WriteLine();

            if (!node.IsLeaf)
            {
                foreach (var child in node.Children)
                {
                    Print(child, indent + " ");
                }
            }
        }

    }
}
