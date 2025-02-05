using RT.Common;
using Server.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RT.Models
{
	[MediusMessage(NetMessageTypes.MessageClassLobby, MediusLobbyMessageIds.FileUploadServerReq)]
    public class MediusFileUploadServerRequest : BaseLobbyMessage, IMediusRequest
    {

		public override byte PacketType => (byte)MediusLobbyMessageIds.FileUploadServerReq;

        public MessageId MessageID { get; set; }

        public int iReqStartByteIndex;
        public int iPacketNumber;
        public MediusFileXferStatus iXferStatus;
        public MediusCallbackStatus StatusCode;

        public override void Deserialize(Server.Common.Stream.MessageReader reader)
        {
            // 
            iReqStartByteIndex = reader.ReadInt32();
            iPacketNumber = reader.ReadInt32();
            iXferStatus = reader.Read<MediusFileXferStatus>();
            StatusCode = reader.Read<MediusCallbackStatus>();

            // 
            base.Deserialize(reader);

            //
            MessageID = reader.Read<MessageId>();
            reader.ReadBytes(3);
        }

        public override void Serialize(Server.Common.Stream.MessageWriter writer)
        {
            // 
            writer.Write(iReqStartByteIndex);
            writer.Write(iPacketNumber);
            writer.Write(iXferStatus);
            writer.Write(StatusCode);

            // 
            base.Serialize(writer);

            //
            writer.Write(MessageID ?? MessageId.Empty);
            writer.Write(new byte[3]);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
                $"MessageID:{MessageID} " +
                $"iReqStartByteIndex:{iReqStartByteIndex} " +
                $"iPacketNumber:{iPacketNumber} " +
                $"iXferStatus:{iXferStatus} " +
                $"StatusCode:{StatusCode}";
        }
    }
}
