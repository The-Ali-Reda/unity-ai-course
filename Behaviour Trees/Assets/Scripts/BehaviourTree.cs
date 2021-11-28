using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree: Node
{
    public BehaviourTree()
    {
        Name = "Tree";
    }
    public BehaviourTree(string name): base(name)
    {
    }

    public override Status Process()
    {
        return Children[_currentChild].Process();
    }

    struct NodeLevel
    {
        public int level;
        public Node node;
        public NodeLevel(Node node, int level)
        {
            this.node = node;
            this.level = level;
        }
    }
    public void PrintTree()
    {
        string treeString = string.Empty;
        Stack<NodeLevel> stack = new Stack<NodeLevel>();
        NodeLevel currentNode = new NodeLevel(this, 0);
        stack.Push(currentNode);
        while(stack.Count>0)
        {
            NodeLevel nextNode = stack.Pop();
            treeString += $"{new string('-', nextNode.level)} {nextNode.node.Name} \n";
            for(var i= nextNode.node.Children.Count-1; i>=0; i--)
            {
                var child = nextNode.node.Children[i];
                stack.Push(new NodeLevel(child, nextNode.level+1));
            }

        }
        Debug.Log(treeString);
    }
}
