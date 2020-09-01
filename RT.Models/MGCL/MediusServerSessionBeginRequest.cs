using RT.Common;
using Server.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RT.Models
{
	[MediusMessage(NetMessageTypes.MessageClassLobbyReport, MediusMGCLMessageIds.ServerSessionBeginRequest)]
    public class MediusServerSessionBeginRequest : BaseMGCLMessage, IMediusRequest
    {

		public override byte PacketType => (byte)MediusMGCLMessageIds.ServerSessionBeginRequest;

        public string MessageID { get; set; }
        public int LocationID;
        public int ApplicationID;
        public MGCL_GAME_HOST_TYPE ServerType;
        public string ServerVersion; // MGCL_SERVERVERSION_MAXLEN
        public int Port;

        public override void Deserialize(BinaryReader reader)
        {
            // 
            base.Deserialize(reader);

            // 
            MessageID = reader.ReadString(Constants.MESSAGEID_MAXLEN);
            reader.ReadBytes(3);
            LocationID = reader.ReadInt32();
            ApplicationID = reader.ReadInt32();
            ServerType = reader.Read<MGCL_GAME_HOST_TYPE>();
            ServerVersion = reader.ReadString(Constants.MGCL_SERVERVERSION_MAXLEN);
            Port = reader.ReadInt32();
        }

        public override void Serialize(BinaryWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(MessageID, Constants.MESSAGEID_MAXLEN);
            writer.Write(new byte[3]);
            writer.Write(LocationID);
            writer.Write(ApplicationID);
            writer.Write(ServerType);
            writer.Write(ServerVersion, Constants.MGCL_SERVERVERSION_MAXLEN);
            writer.Write(Port);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
                $"MessageID:{MessageID} " +
                $"LocationID:{LocationID} " +
                $"ApplicationID:{ApplicationID} " +
                $"ServerType:{ServerType} " +
                $"ServerVersion:{ServerVersion} " +
                $"Port:{Port}";
        }
    }
}
