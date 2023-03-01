﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF7Scarlet.KernelEditor
{
    public class StatCurve
    {
        private readonly byte[] gradients = new byte[8], bases = new byte[8];

        public byte[] Gradients
        {
            get { return gradients; }
        }
        public byte[] Bases
        {
            get { return bases; }
        }

        public StatCurve(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var reader = new BinaryReader(ms))
            {
                for (int i = 0; i < 8; ++i)
                {
                    Gradients[i] = reader.ReadByte();
                    Bases[i] = reader.ReadByte();
                }
            }
        }
    }
}