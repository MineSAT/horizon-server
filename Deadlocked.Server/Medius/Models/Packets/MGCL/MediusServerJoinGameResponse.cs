using Deadlocked.Server.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Deadlocked.Server.Medius.Models.Packets.MGCL
{
	[MediusMessage(NetMessageTypes.MessageClassLobbyReport, MediusMGCLMessageIds.ServerJoinGameResponse)]
    public class MediusServerJoinGameResponse : BaseMGCLMessage
    {

		public override byte PacketType => (byte)MediusMGCLMessageIds.ServerJoinGameResponse;

        public MGCL_ERROR_CODE Confirmation;
        public string AccessKey; // MGCL_ACCESSKEY_MAXLEN
        public RSA_KEY pubKey;
        public int DmeClientIndex;

        public override void Deserialize(BinaryReader reader)
        {
            // 
            base.Deserialize(reader);

            // 
            Confirmation = reader.Read<MGCL_ERROR_CODE>();
            AccessKey = reader.ReadString(Constants.MGCL_ACCESSKEY_MAXLEN);
            reader.ReadBytes(1);
            pubKey = reader.Read<RSA_KEY>();
            DmeClientIndex = reader.ReadInt32();
        }

        public override void Serialize(BinaryWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(Confirmation);
            writer.Write(AccessKey, Constants.MGCL_ACCESSKEY_MAXLEN);
            writer.Write(new byte[1]);
            writer.Write(pubKey);
            writer.Write(DmeClientIndex);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
             $"Confirmation:{Confirmation}" + " " +
$"AccessKey:{AccessKey}" + " " +
$"pubKey:{pubKey}" + " " +
$"DmeClientIndex:{DmeClientIndex}";
        }
    }
}
