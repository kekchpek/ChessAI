namespace KChess.GameManagement.Core
{
    public interface IGameRecordManager
    {
        void MoveNext();
        void MovePrevious();
        void JumpTo(int moveIndex);
    }
}