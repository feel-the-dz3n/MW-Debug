using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MW_Debug_2
{
    public class ReplayInfo
    {
        // Fields
        public float RotationW;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float X;
        public float Y;
        public float Z;

        // Methods
        public ReplayInfo(float x, float y, float z, float rotx, float roty, float rotz, float rotw)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.RotationX = rotx;
            this.RotationY = roty;
            this.RotationZ = rotz;
            this.RotationW = rotw;
        }
    }

}