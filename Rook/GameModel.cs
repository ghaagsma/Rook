using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rook
{
    public class GameModel
    {
        private static GameModel model;

        private GameModel() { }

        public static GameModel Model
        {
            get 
            {
                if (model == null)
                    model = new GameModel();

                return model;
            }
        }
    }
}
