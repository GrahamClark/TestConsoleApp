using ProtoBuf;

namespace TestConsoleApp.ProtocolBuffers
{
    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)]
        public int HouseNumber { get; set; }

        [ProtoMember(2)]
        public string StreetName { get; set; }
    }
}
