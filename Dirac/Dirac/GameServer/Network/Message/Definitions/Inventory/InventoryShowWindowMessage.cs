using System.Text;
using Dirac.GameServer.Core;

namespace Dirac.GameServer.Network.Message
{
    public class InventoryShowWindowMessage : GameMessage
    {
        public InventoryWindowsID windowId; // Item's DynamicID
        public bool visible;

        public InventoryShowWindowMessage()
            :base(Opcodes.InventoryShowWindowMessage)
        {
            this.Consumer = Consumers.Inventory;
        }

        public override void Parse(GameBitBuffer buffer)
        {
            windowId = (InventoryWindowsID)buffer.ReadInt(32);
            visible = buffer.ReadBool();
        }

        public override void Encode(GameBitBuffer buffer)
        {
            buffer.WriteInt(32, (int)windowId);
            buffer.WriteBool(visible);
        }

        public override void AsText(StringBuilder b, int pad)
        {
            b.Append(' ', pad);
            b.AppendLine("InventoryShowWindowMessage:");
            b.Append(' ', pad++);
            b.AppendLine("{");
            b.Append(' ', pad); b.AppendLine("windowId: " + windowId.ToString());
            b.Append(' ', pad); b.AppendLine("visible: " + visible.ToString());
            b.Append(' ', --pad);
            b.AppendLine("}");
        }
    }
}
