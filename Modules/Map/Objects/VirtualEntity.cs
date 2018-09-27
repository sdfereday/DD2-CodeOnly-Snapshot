public class VirtualEntity
{
    public System.Guid Guid = System.Guid.NewGuid();
    public string id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public ENTITY_TYPE type { get; set; }
}