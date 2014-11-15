namespace Rook
{
    public class GameModel
    {
        private static GameModel _model;

        private GameModel() { }

        public static GameModel Model
        {
            get { return _model ?? (_model = new GameModel()); }
        }
    }
}