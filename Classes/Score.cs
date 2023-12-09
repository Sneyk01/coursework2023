using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPongGame.Classes
{
    internal class Score
    {
        private int _score;
        private double _modifiers;
        private int[] _formula;

        public Score()
        {
            this._score     = 0;
            this._modifiers = 0;
            this._formula   = new int[3];

            this._formula[0] = 10;
            this._formula[1] = 20;
            this._formula[2] = 100;
        }

        public void setModifiers(int mapType, int botType) {
            this._modifiers = 1;
            if (botType == 1)
                this._modifiers += 0.4;
            if (mapType == 1)
                this._modifiers += 0.2;
            if (mapType == 2)
                this._modifiers += 0.4;
        }
        public void setFormula(int[] formula) { this._formula =  formula; }

        public void calcScore(int goals, int lives, TimeSpan gameTime)
        {
            this._score = Convert.ToInt32( (this._modifiers * ( (goals * _formula[0]) + (gameTime.TotalSeconds * this._formula[2] / 60) )) - ((5 - lives) * this._formula[1]) );
            // MessageBox.Show($"total score: {_score} time score: {(gameTime.TotalSeconds * this._formula[2] / 60)} g score: {goals * _formula[0]} l score: {-(5 - lives) * this._formula[1]} mods: {_modifiers}");
        }

        public int getScore() { return this._score; }
    }
}
