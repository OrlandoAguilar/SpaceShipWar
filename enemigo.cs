using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public abstract class enemigo : nave
    {
        public bool muerto = false;
        public abstract void update();

        public bool colis(nave id)
        {
            return (true);
        }
    }
}
