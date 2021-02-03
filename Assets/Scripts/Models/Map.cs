using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Prototype/Map")]
    public class Map : ScriptableObject
    {
        public Vector2 Size;
        public int Connectors;
        public List<Node> Nodes;
        
        public void Save(Node node)
        {
            Nodes.Add(node);
        }
    }
}
