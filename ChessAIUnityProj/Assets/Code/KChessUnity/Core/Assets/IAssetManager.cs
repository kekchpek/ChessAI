using UnityEngine;

namespace KChessUnity.Core.Assets
{
    public interface IAssetManager
    {
        T Get<T>(string path) where T : Object;
    }
}