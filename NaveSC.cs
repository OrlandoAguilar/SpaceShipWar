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
    class NaveSC : enemigo
    {
        public float av;

        public NaveSC(Model text, Model[] t_disp2)
        {
            g_nave = new modelo(text);
            av = 440;
            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
            t_disp = t_disp2;
            d_act = 2;
           /* for (int z = 0; z < nave.cant; z++)
            {
                anm[z] = new anim(Game1.estr, -1, pos);
                vc[z] = new Vector2(-100);
            }*/
        }
        public override void update()
        {

            vel += 0.05f;
            av -= 3;
            pos.X = (float)av ;
            pos.Y = (float)(( Math.Sin(vel) + Math.Cos(vel) + Math.Sin(vel/2) + Math.Cos(vel/2) + Math.Sin(vel/4) + Math.Cos(vel/4)) * 100);
            box.actualiza(new Vector3(pos.X - 60f, pos.Y - 7f, -8f), new Vector3(pos.X + 42f, pos.Y + 14f, +8f));
            g_nave.rotar(0, angle += (float)Math.PI / 12f, 0);
            if (pos.X < -440)
                muerto = true;
            if (Game1.rnd.Next(15 + 50 / Game1.nivel) == 5)
            {
                aux = new disparo(-1, false, t_disp[d_act], lista, 10.0f, pos);
                Game1.sonidos.PlayCue("disparo");
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

