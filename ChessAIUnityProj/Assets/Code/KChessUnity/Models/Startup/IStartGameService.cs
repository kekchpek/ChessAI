using UnityAuxiliaryTools.Promises;

namespace KChessUnity.Models.Startup
{
    public interface IStartGameService
    {
        IPromise StartSingleGame();
        IPromise StartGameWithAI();
    }
}