using Server.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RT.Models
{
	[MediusMessage(NetMessageTypes.MessageClassLobby, MediusLobbyMessageIds.TextFilter)]
    public class MediusTextFilterRequest : BaseLobbyMessage
    {
		public override byte PacketType => (byte)MediusLobbyMessageIds.TextFilter;

        public string SessionKey; // SESSIONKEY_MAXLEN
        public MediusTextFilterType TextFilterType;
        public string Text; // CHATMESSAGE_MAXLEN

        public override void Deserialize(BinaryReader reader)
        {
            // 
            base.Deserialize(reader);

            // 
            SessionKey = reader.ReadString(Constants.SESSIONKEY_MAXLEN);
            reader.ReadBytes(2);
            TextFilterType = reader.Read<MediusTextFilterType>();
            Text = reader.ReadString(Constants.CHATMESSAGE_MAXLEN);
        }

        public override void Serialize(BinaryWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(SessionKey, Constants.SESSIONKEY_MAXLEN);
            writer.Write(new byte[2]);
            writer.Write(TextFilterType);
            writer.Write(Text, Constants.CHATMESSAGE_MAXLEN);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
             $"SessionKey:{SessionKey} " +
$"TextFilterType:{TextFilterType} " +
$"Text:{Text}";
        }
    }
}
