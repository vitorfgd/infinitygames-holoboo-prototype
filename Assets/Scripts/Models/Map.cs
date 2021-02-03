using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Prototype/Map")]
    public class Map : ScriptableObject
    {
        public Vector2 Size;
        public int Connectors;
        public int EmptySpaces;
        public Queue<Node> Nodes;
        
        // TODO: Save current map configuration. Save as a queue (respects position) use it to recreate a previously created level.
        public void Save(Node node)
        {
        }
    }
}
