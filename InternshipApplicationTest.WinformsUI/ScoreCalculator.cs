namespace InternshipApplicationTest.WinformsUI
{
    public class ScoreCalculator
    {
        private static ScoreCalculator _instance;

        public short Score { get; set; }

        public static ScoreCalculator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreCalculator();
                }
                return _instance;
            }
        }

        private ScoreCalculator()
        {

        }
    }
}
