using ProtoBuf;

namespace TestConsoleApp.ProtocolBuffers
{
    [ProtoContract]
    [ProtoInclude(100, typeof(Person))]
    public abstract class Being
    {
        [ProtoMember(1)]
        public string Type { get; set; }

        public abstract string Name { get; set; }
    }
}
