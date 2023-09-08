using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    public class SaveSystem : MonoBehaviour
    {


        public string saveFolderPath = "";

        [ContextMenu("Save")]
        public bool Save()
        {
            return false;
        }

        [ContextMenu("Load")]
        public (bool, string) Load()
        {
            return (false, "");
        }

    }
}