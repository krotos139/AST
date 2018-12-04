using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonSingleton  {
    public static int level_index = 0;
    public static List<Vector2> staffPos = new List<Vector2>();
    public static List<Quaternion> staffQuat = new List<Quaternion>();
    public static List<string> staffNames = new List<string>();


    public static void clearStaff()
    {
        staffPos.Clear();
        staffNames.Clear();
        staffQuat.Clear();
    }

    public static void AddStaff(Vector2 pos, Quaternion rot, string name)
    {
        staffPos.Add(pos);
        staffNames.Add(name);
        staffQuat.Add(rot);
    }

    public static void Next()
    {
        
    }
    public static void GetStaff(Vector2 pos, Quaternion rot, string name)
    {
        staffPos.Add(pos);
        staffNames.Add(name);
        staffQuat.Add(rot);
    }
}
