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
    class NaveBsl : enemigo
    {
        public float av;

        public NaveBsl(Model text, Model[] t_disp2)
        {
            av = 440f;
            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
            g_nave = new modelo(text);
            t_disp = t_disp2;
            d_act = 1;
        }
        public override void update()
        {
            vel += 0.01f;
            av -= 2;
            pos.X = (float)(av + (vel + 2 * Math.Sin(2 * vel)) * 100);
            pos.Y = (float)(-300+ (vel + 2 * Math.Cos(5 * vel))*50);
            box.actualiza(new Vector3(pos.X - 47f, pos.Y - 5f, -28f), new Vector3(pos.X + 41f, pos.Y + 4f,28f));
            g_nave.rotar(0, angle += (float)Math.PI / 14f, 0);
            if (pos.X  < -440)
                muerto = true;

            if (Game1.rnd.Next(25 + 50 / Game1.nivel) == 5)
            {
                aux = new disparo(-1, false, t_disp[d_act], lista, 8.0f, pos);
                Game1.sonidos.PlayCue("disparo3");
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
