namespace Models
{
    /// <summary>
    /// Designed to be blank space, used for creating different paths.
    /// </summary>
    public class Empty : Node
    {
        // Empty node should never be connected to anything.
        public override void Connect()
        {
        }
    }
}
