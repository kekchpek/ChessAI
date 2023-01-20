using UnityEngine;

namespace KChessUnity.Core.Assets
{
    public class AssetManager : IAssetManager
    {
        public T Get<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}