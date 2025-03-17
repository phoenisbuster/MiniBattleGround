using System;
using System.IO;
using UnityEngine;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class Resource
    {
        public string resourceId;
        public string resourceType;
        public string resourcePath;

        public string FileName => resourceId + ((resourceType == "FBX") ? ".fbx" : ".png");

        public Texture Texture(string rootFolder)
        {
            // return ES3.LoadImage(Path.Combine(rootFolder, FileName));
            return null;
        }
    }
}