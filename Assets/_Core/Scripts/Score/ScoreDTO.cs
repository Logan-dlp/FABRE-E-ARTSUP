namespace FABRE.Score
{
    [System.Serializable]
    public class ScoreDTO
    {
        public ScoreDTO(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
        
        private string name;
        public string Name => name;
        
        private int score;
        public int Score => score;
    }
}