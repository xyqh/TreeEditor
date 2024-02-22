using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

public class TreeHelper : Singleton<TreeHelper>
{
    public static TreeNode GetRootNodeFromAsset(string path)
    {
        Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
        
        var dialogueContainer = Resources.Load(path) as DialogueContainer;
        foreach (var perNode in dialogueContainer.DialogueNodeData)
        {
            string name = perNode.DialogueText, guid = perNode.NodeGUID;
            if (nodes.ContainsKey(guid))
            {
                continue;
            }

            var tempNode = new TreeNode()
            {
                _name = name,
                _guid = guid,
                _children = new List<TreeNode>()
            };
            nodes.Add(guid, tempNode);
        }

        foreach (var (pGuid, pNode) in nodes)
        {
            var connections = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == pNode._guid).ToList();
            foreach (var connection in connections)
            {
                var guid = connection.TargetNodeGUID;
                var node = nodes[guid];
                node._parentNode = pNode;
                pNode._children.Add(node);
            }
        }

        if (nodes.Count < 1)
        {
            return null;
        }

        var root = nodes.ElementAt(0).Value;
        while (root._parentNode != null)
        {
            root = root._parentNode;
        }

        return root;
    }

    private static List<List<TreeNode>> LevelOrder(TreeNode node) 
    {
        if (node == null)
        {
            return null;
        }

        var ret = new List<List<TreeNode>>();
        Queue<TreeNode> que = new Queue<TreeNode>();
        que.Enqueue(node);
        while (que.Count > 0)
        {
            var list = new List<TreeNode>();
            var n = que.Count;
            for (var i = 0; i < n; ++i)
            {
                node = que.Dequeue();
                list.Add(node);
                for (var j = 0; j < node._children.Count; ++j)
                {
                    que.Enqueue(node._children[j]);
                }
            }
            ret.Add(list);
        }

        return ret;
    }

    public static void PrintTree(TreeNode root)
    {
        var lists = LevelOrder(root);
        var levelStr = "";
        for (var i = 0; i < lists.Count; ++i)
        {
            for (var j = 0; j < lists[i].Count; ++j)
            {
                var node = lists[i][j];
                levelStr += $"{node._name} ";
            }

            levelStr += "\n";
        }
        Debug.Log($"PrintTree \n{levelStr}");
    }
}
