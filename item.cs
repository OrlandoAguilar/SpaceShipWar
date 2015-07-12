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
    class item
    {
        protected Vector2 pos = new Vector2(0f, 0f);
        protected modelo g_nave;
        protected Vector3 angle;
        public caja box;
        public int tipo;
        public bool muerto= false;

        public item(int tip)
        {
            g_nave = new modelo(Game1.gema[tip]);
            tipo = tip;
            pos.X = 550;
            box = new caja(new Vector3(pos.X - 15f, pos.Y - 16f, -15f), new Vector3(pos.X + 15f, pos.Y + 4f, 15f));
            
        }
        public void update()
        {
            pos.X-=5;
            pos.Y = (float)Math.Sin(pos.X / 180f)*300f;
            angle.X += 0.2f;
            angle.Y += 0.3f;
            angle.Z += 0.1f ;

            if (pos.X < -500)
                muerto = true;
            box.actualiza(new Vector3(pos.X - 15f, pos.Y - 16f, -15f), new Vector3(pos.X + 15f, pos.Y + 4f, 15f));
            if (box.intersect(Game1.naveP.box))
            {
                Game1.sonidos.PlayCue("item");
                switch (tipo)
                {
                    case 0:
                        if (Game1.ener<50)
                            Game1.ener += 50;
                        else
                            Game1.ener = 100;
                        break;

                    case 1:
                        Game1.View = Matrix.CreateLookAt(Game1.cameraPosition, Vector3.Zero, Vector3.Up);
                        break;
                }
                Game1.efecto = tipo;
                
                muerto = true;
            }
        }

        public void draw()
        {
            //spriteBatch.Draw(g_nave[indice], pos, null, Color.White, angle, new Vector2(g_nave[indice].Width / 2, g_nave[indice].Height / 2), size, SpriteEffects.None, 1);
            g_nave.mueve(pos.X, pos.Y);
            g_nave.rotar(angle);
            g_nave.draw();

        }
    }
}
