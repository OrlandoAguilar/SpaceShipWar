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
    class NaveC : enemigo
    {
        public float av;

        public NaveC(Model text,Model[]  t_disp2)
        {
            g_nave = new modelo(text);
            av = 440;
            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
            t_disp=t_disp2;
            d_act = 3;
        }
        public override void update()
        {
            vel+=0.05f;
            av -= 3;
            pos.X = (float)(av+(1.5 * Math.Cos(vel) - Math.Cos(Math.PI / 6 * vel)) * 100);
            pos.Y =(float)(( 1.5 * Math.Sin(vel) - Math.Sin(Math.PI/6 * vel))*100);
            g_nave.rotar(0, angle += (float)Math.PI / 10f, 0);
            box.actualiza(new Vector3(pos.X - 33f, pos.Y - 6f, -21f), new Vector3(pos.X + 33f, pos.Y + 8f, +21f));
            if (pos.X  < -440)
                muerto = true;

            if (Game1.rnd.Next(30 + 50 / Game1.nivel) == 5)
            {
                aux = new disparo(-1, false, t_disp[d_act], lista, 9.0f, pos, 0.3f);
                Game1.sonidos.PlayCue("disparo2");
            }

               if (box.intersect(Game1.naveP.box))
                {
                    Game1.ener-=10;
                    Game1.sonidos.PlayCue("explosion");
                    muerto = true;
                }
        }
    }
}
