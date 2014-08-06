using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CollisionDetection
{
    // Original C# implementation by xDaniel. Integrated into Unity by Jack Walker.
    public static void FixCollision(ref List<byte> Data, int VertOff, int TriOff, int TriCount)
    {
        int i, pos, end = TriOff + (TriCount << 4);
        int vertex1, vertex2, vertex3, dn;
        int[] p1 = new int[3], p2 = new int[3], p3 = new int[3], dx = new int[2], dy = new int[2], dz = new int[2], ni = new int[3];
        float nd;
        float[] nf = new float[3];

        for (pos = TriOff; pos < end; pos += 0x10)
        {
            vertex1 = Read16(Data, pos + 2);
            vertex2 = Read16(Data, pos + 4);
            vertex3 = Read16(Data, pos + 6);

            for (i = 0; i < 3; i++)
            {
                p1[i] = Read16S(Data, VertOff + (vertex1 * 0x6) + (i << 1));
                p2[i] = Read16S(Data, VertOff + (vertex2 * 0x6) + (i << 1));
                p3[i] = Read16S(Data, VertOff + (vertex3 * 0x6) + (i << 1));
            }

            dx[0] = p1[0] - p2[0]; dx[1] = p2[0] - p3[0];
            dy[0] = p1[1] - p2[1]; dy[1] = p2[1] - p3[1];
            dz[0] = p1[2] - p2[2]; dz[1] = p2[2] - p3[2];

            ni[0] = (dy[0] * dz[1]) - (dz[0] * dy[1]);
            ni[1] = (dz[0] * dx[1]) - (dx[0] * dz[1]);
            ni[2] = (dx[0] * dy[1]) - (dy[0] * dx[1]);

            for (i = 0; i < 3; i++)
                nf[i] = (float)ni[i] * ni[i];

            nd = nf[0] + nf[1] + nf[2];

            for (i = 0; i < 3; i++)
            {
                if (nd != 0)
                    nf[i] /= nd;
                nf[i] *= 0x3FFF0001;
                nf[i] = (float)Math.Sqrt((double)nf[i]);
                if (ni[i] < 0)
                    nf[i] *= -1;
            }

            dn = (int)((nf[0] / 0x7FFF * p1[0]) + (nf[1] / 0x7FFF * p1[1]) + (nf[2] / 0x7FFF * p1[2])) * -1;

            if (dn < 0)
                dn += 0x10000;

            Overwrite16(ref Data, pos + 0xE, (ushort)(dn & 0xFFFF));

            for (i = 0; i < 3; i++)
            {
                ni[i] = (int)nf[i];
                if (ni[i] < 0)
                    ni[i] += 0x10000;
                Overwrite16(ref Data, (pos + 8 + (i << 1)), (ushort)(ni[i] & 0xFFFF));
            }
        }
    }

    private static ushort Read16(List<byte> Data, int Offset)
    {
        return (ushort)(Data[Offset] << 8 | Data[Offset + 1]);
    }

    private static short Read16S(List<byte> Data, int Offset)
    {
        return (short)(Data[Offset] << 8 | Data[Offset + 1]);
    }

    private static void Overwrite16(ref List<byte> Data, int Offset, ushort Value)
    {
        OverwriteXX(ref Data, Offset, Value, 1);
    }

    private static void OverwriteXX(ref List<byte> Data, int Offset, ulong Value, int Shifts)
    {
        if (Offset >= Data.Count)
        {
            AppendXX(ref Data, Value, Shifts);
        }
        else
        {
            for (int i = Shifts; i >= 0; --i)
            {
                byte DataByte = (byte)((Value >> (i * 8)) & 0xFF);
                Data.RemoveAt(Offset);
                Data.Insert(Offset++, DataByte);
            }
        }
    }
    private static void AppendXX(ref List<byte> Data, ulong Value, int Shifts)
    {
        for (int i = Shifts; i >= 0; --i)
        {
            byte DataByte = (byte)((Value >> (i * 8)) & 0xFF);
            Data.Add(DataByte);
        }
    }
}
