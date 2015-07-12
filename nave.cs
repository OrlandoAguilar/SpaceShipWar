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
    public abstract class  nave
    {
        public const int cant = 7;
        protected Vector2 pos = new Vector2(0f, 0f);
        protected modelo g_nave;
        protected Model[] t_disp;
        protected byte d_act = 0;
        protected float angle, vel = 0;
        public disps lista = new disps();
        protected disparo aux;
        public caja box;

        public Vector2 get_pos()
        {
            return (pos);
        }
        public float get_angle()
        {
            return (angle);
        }

        public void draw()
        {
            //spriteBatch.Draw(g_nave[indice], pos, null, Color.White, angle, new Vector2(g_nave[indice].Width / 2, g_nave[indice].Height / 2), size, SpriteEffects.None, 1);
           g_nave.mueve(pos.X, pos.Y);
            g_nave.draw();
         
        }

    }
}
