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
    class misil : enemigo
    {
        public misil(Model text, Model[] t_disp2)
        {

            g_nave = new modelo(text);
            pos.X = 240;
            pos.Y = 0;
            box = new caja(new Vector3(pos.X - 16f, pos.Y - 8f, -7f), new Vector3(pos.X + 92f, pos.Y + 5f, 6f));
            t_disp = t_disp2;
            angle = getf_angle(Game1.naveP);
            g_nave.rotar(0, 0, angle);
            vel = 9+Game1.nivel;
        }
        public float getf_angle(nave n)
        {
            float ang;
            ang = (float)Math.Atan2((double)(n.get_pos().Y - pos.Y), (double)(n.get_pos().X - pos.X));
            return (ang);
        }
        public override void update()
        {
            pos.X +=(float) Math.Cos(angle) * vel;
            pos.Y +=(float) Math.Sin(angle) * vel;
            box.actualiza(new Vector3(pos.X - 92f, pos.Y -5f, -6f), new Vector3(pos.X + 16f, pos.Y + 8f, 7f));
            if (pos.X  < -440 || Math.Abs(pos.Y)>340)
                muerto = true;
            //box.intersect(Game1.naveP.box) &&
            if ((new Ray(new Vector3(pos.X,pos.Y,0),new Vector3((float)Math.Cos(angle),(float)Math.Sin(angle),0.0f))).Intersects(Game1.naveP.box.Rect())<100 )
            {
                Game1.ener-=10;
                Game1.sonidos.PlayCue("explosion");
                muerto = true;
            }
        }
    }
}