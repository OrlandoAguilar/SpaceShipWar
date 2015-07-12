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
    class bomba : enemigo
    {
        float angdest,auxx,auy;
        int cont;
        public bomba(Model text,  Model[] t_disp2)
        {
            g_nave = new modelo(text);
            auxx = 240;
            auy = 0;
            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
            t_disp = t_disp2;
            vel = 2+Game1.nivel;
        }
        public float getf_angle(nave n)
        {
            float ang;
            ang = (float)Math.Atan2((double)(n.get_pos().Y - pos.Y), (double)(n.get_pos().X - pos.X));
            return (ang);
        }
        public override void update()
        {
            angle += 0.05f;
            if (angle > 2 * Math.PI)
                angle = 0;
            angdest = getf_angle(Game1.naveP);
            auxx += (float)Math.Cos(angdest) * vel;
            auy += (float)Math.Sin(angdest) * vel;
            pos.X = (float)(auxx + Math.Cos(angle) * (Math.Cos(angle) * 130) + 80);
            pos.Y = (float)(auy + Math.Sin(angle) * (Math.Cos(angle) * 130) + 80);
            g_nave.rotar(angle, angle*2f, angle/2f);
          box.actualiza(new Vector3(pos.X - 30f, pos.Y - 30f, -30f), new Vector3(pos.X + 30f, pos.Y + 30f, 30f));
            if (++cont>2000)
                muerto = true;

            if (box.intersect(Game1.naveP.box))
            {
                Game1.ener-=10;
                Game1.sonidos.PlayCue("explosion");
                muerto = true;
            }
        }
    }
}