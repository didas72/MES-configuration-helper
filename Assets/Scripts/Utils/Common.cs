using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Common
    {
        public static bool TryFindIndex<T>(List<T> list, T obj, out int Index)
        {
            Index = -1;

            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].Equals(obj))
                {
                    Index = i;
                    return true;
                }
            }

            return false;
        }

        public static void ClearChildren(GameObject go)
        {
            GameObject[] children = new GameObject[go.transform.childCount];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = go.transform.GetChild(i).gameObject;
            }

            foreach (GameObject child in children)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}