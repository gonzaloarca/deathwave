namespace Strategy
{
    public interface IScore
    {
        int Score { get; }
        
        void AddScore(int score);
        
        void ResetScore();
        
        void SubtractScore(int cost);

        bool CanSubtract(int cost);
    }
}