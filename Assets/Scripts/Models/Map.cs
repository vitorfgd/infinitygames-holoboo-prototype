using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Prototype/Map")]
    public class Map : ScriptableObject
    {
        public Vector2 Size;
        public int Connectors;
    }   
}
