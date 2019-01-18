using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
   public static class CordsSaver
{
    // Fields
    public static List<GameCord> checkpoints = new List<GameCord>();

    // Methods
    public static void AddCheckpointInfo(int offset)
    {
        checkpoints.Add(GameCordFuncs.GetCurrentCords(offset));
    }

    public static void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("[checkpoints]");
            for (int i = 0; i < checkpoints.Count; i++)
            {
                writer.WriteLine(string.Format("checkpoint{0} - {1}, {2}, {3}, Rotation - {4}, {5}, {6}, {7}", new object[] { i, checkpoints[i].x, checkpoints[i].y, checkpoints[i].z, checkpoints[i].r1, checkpoints[i].r2, checkpoints[i].r3, checkpoints[i].r4 }));
            }
        }
    }
}




}