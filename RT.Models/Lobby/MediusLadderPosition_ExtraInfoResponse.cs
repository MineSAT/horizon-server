using Server.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RT.Models
{
	[MediusMessage(NetMessageTypes.MessageClassLobby, MediusLobbyMessageIds.LadderPosition_ExtraInfoResponse)]
    public class MediusLadderPosition_ExtraInfoResponse : BaseLobbyMessage
    {
		public override byte PacketType => (byte)MediusLobbyMessageIds.LadderPosition_ExtraInfoResponse;

        public MediusCallbackStatus StatusCode;
        public uint LadderPosition;
        public uint TotalRankings;

        public override void Deserialize(BinaryReader reader)
        {
            // 
            base.Deserialize(reader);

            // 
            reader.ReadBytes(3);
            StatusCode = reader.Read<MediusCallbackStatus>();
            LadderPosition = reader.ReadUInt32();
            TotalRankings = reader.ReadUInt32();
        }

        public override void Serialize(BinaryWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(new byte[3]);
            writer.Write(StatusCode);
            writer.Write(LadderPosition);
            writer.Write(TotalRankings);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
             $"StatusCode:{StatusCode} " +
$"LadderPosition:{LadderPosition} " +
$"TotalRankings:{TotalRankings}";
        }
    }
}
