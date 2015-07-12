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
    public class disparo
    {
        private Vector2 pos = new Vector2(0f, 0f);
        private float angle = 0, vel = 1, angle_p;
        private int dir = 0;
        private bool tip;
        private modelo graph;
        disps Mstruct;
        public bool muerto = false;
        caja box;

        public disparo(int direccion, bool tipo,Model graphA,disps str,float veloc, Vector2 posicion)
        {
            init(direccion, tipo, graphA, str, veloc, posicion);
        }
        public disparo(int direccion, bool tipo, Model graphA, disps str, float veloc, Vector2 posicion, float pl_ang)
        {
            init(direccion, tipo, graphA, str, veloc, posicion);
            angle_p = pl_ang;
        }

        private void init(int direccion, bool tipo, Model graphA, disps str, float veloc, Vector2 posicion)
        {
            disps aux = str;
            dir = direccion;
            tip = tipo;
            graph = new modelo(graphA);
            Mstruct = new disps();
            Mstruct.elem = this;
            while (aux.sig != null)
                aux = aux.sig;
            aux.sig = Mstruct;
            Mstruct.ant = aux;

            vel = veloc;
            pos = posicion;
            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
                            
        }

        public Vector2 get_pos()
        {
            return (pos);
        }
        public float get_angle()
        {
            return (angle);
        }
        public void update()
        {
            angle += angle_p;
            pos.X += dir* vel;
            //if (Math.Abs(pos.X) > 245) 
            if (Math.Abs(pos.X) > 440)
                muerto = true;
            box.actualiza(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, +10f));
               collision();
        }

        public void draw()
        {
            //spriteBatch.Draw(graph, pos, null, Color.White, angle, new Vector2(graph.Width / 2, graph.Height / 2), size, tip ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 1);
            graph.mueve(pos.X, pos.Y);
            graph.draw();
        }

        public bool collision ()
        {
            if (tip)
            {//busca colision con enemigos
                for (int z = 0; z < Game1.totalEnem; z++)
                    if (Game1.enemigos[z] != null) 
                        
                        if (box.intersect(Game1.enemigos[z].box))
                        {
                            Game1.enemigos[z].muerto = true;
                            muerto = true;
                            Game1.record += 5;
                            Game1.sonidos.PlayCue("explosion2");
                        }
            }
            else
            {
                if (box.intersect(Game1.naveP.box))
                {
                    Game1.ener -= 10;
                    muerto = true;
                    Game1.sonidos.PlayCue("explosion");
                }
            }
                return false;
        }
    }
}
