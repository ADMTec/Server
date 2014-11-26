﻿using System.Text;

namespace Dirac.GameServer.Network.Message
{
    public class AssignTraitsMessage : GameMessage
    {
        public int[] /* sno */ SNOPowers;

        public override void Parse(GameBitBuffer buffer)
        {
            SNOPowers = new int[3];
            for (int i = 0; i < SNOPowers.Length; i++) SNOPowers[i] = buffer.ReadInt(32);
        }

        public override void Encode(GameBitBuffer buffer)
        {
            for (int i = 0; i < SNOPowers.Length; i++) buffer.WriteInt(32, SNOPowers[i]);
        }

        public override void AsText(StringBuilder b, int pad)
        {
            b.Append(' ', pad);
            b.AppendLine("AssignTraitsMessage:");
            b.Append(' ', pad++);
            b.AppendLine("{");
            for (int i = 0; i < SNOPowers.Length; ) { b.Append(' ', pad + 1); for (int j = 0; j < 8 && i < SNOPowers.Length; j++, i++) { b.Append("0x" + SNOPowers[i].ToString("X8") + ", "); } b.AppendLine(); }
            b.Append(' ', --pad);
            b.AppendLine("}");
        }

    }
}
