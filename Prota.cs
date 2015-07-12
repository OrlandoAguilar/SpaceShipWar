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
    public class Prota : nave
    {
        private const int av = 5;
        private bool press = false;
        private bool press2 = false;
        private int selecc;
        private bool fin= false;
        
        public Prota (Model text,Model[] disp){

            g_nave = new modelo(text);
            t_disp=disp;

            box = new caja(new Vector3(pos.X - 10f, pos.Y - 10f, -10f), new Vector3(pos.X + 10f, pos.Y + 10f, 10f));
        }

        public void set_pos(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
        }
        public void Selec(KeyboardState keyboardState)
        {
           
            pos.X = -380;
            //if (keyboardState.IsKeyDown(Keys.Left))
            //{
            //    ct += 0.1f;
            //    g_nave.rotar(0, ct, 0);
            //}
            //if (keyboardState.IsKeyDown(Keys.Right))
            //{
            //    ct -= 0.1f;
            //    g_nave.rotar(0, ct, 0);
            //}

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (press == false)
                {
                    selecc--;
                    press = true;
                }
            }
            else
                press = false;
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (press2 == false)
                {
                    selecc++;
                    press2 = true;
                }
            }
            else
                press2 = false;

            if (selecc < 0)
                selecc = 2;

            if (selecc > 2)
                selecc = 0;
            switch (selecc)
            {
                case 0:
                    pos.Y = 20;
                    if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.X))
                    {
                        Game1.pantalla = 2;
                        Game1.time = Game1.lastTime();
                        pos.X = -200;
                        pos.Y = 0;
                    }
                    break;
                case 1:
                    pos.Y = 110;
                    if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.X)) 
                    {
                        Game1.pantalla = 1;
                    }
                    break;
                case 2:
                    pos.Y = 200;
                    if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.X))
                    Game1.ext = true;
                    break;

            }
            

        }

        public void opc(KeyboardState keyboardState)
        {

            if (fin == false)
            {
                pos.X = -380;

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    if (press == false)
                    {
                        selecc--;
                        press = true;
                    }
                }
                else
                    press = false;
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    if (press2 == false)
                    {
                        selecc++;
                        press2 = true;
                    }
                }
                else
                    press2 = false;

                if (selecc < 0)
                    selecc = 3;

                if (selecc > 3)
                    selecc = 0;
                switch (selecc)
                {
                    case 0:
                        pos.Y = 20;
                        if (keyboardState.IsKeyDown(Keys.Left) && Game1.musicVolume > 0f)
                            Game1.musicVolume -= 0.01f;
                        if (keyboardState.IsKeyDown(Keys.Right) && Game1.musicVolume < 1f)
                            Game1.musicVolume += 0.01f;

                        break;
                    case 1:
                        pos.Y = 110;
                        if (keyboardState.IsKeyDown(Keys.Left) && Game1.fondoVolume > 0f)
                            Game1.fondoVolume -= 0.01f;
                        if (keyboardState.IsKeyDown(Keys.Right) && Game1.fondoVolume < 1f)
                            Game1.fondoVolume += 0.01f;
                        break;
                    case 2:
                        pos.Y = 200;
                        if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right))
                        {
                            Game1.max_record = 0;
                            EO.save(Microsoft.Xna.Framework.Storage.StorageContainer.TitleLocation + "/Content/datos/datos.txt", 0);
                        }
                        break;
                    case 3:
                        pos.Y = 300;
                        if (keyboardState.IsKeyDown(Keys.Enter))
                            fin = true;
                        break;

                }
            }
            else
                if (!keyboardState.IsKeyDown(Keys.Enter))
                {
                    fin = false;
                    Game1.pantalla = 0;
                }
            

        }
        public void update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pos.Y-=av;
            }
            if (keyboardState.IsKeyDown(Keys.Down ))
            {
                pos.Y += av;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
                pos.X -= av;

            if (keyboardState.IsKeyDown(Keys.Right))
                pos.X += av;
            if (keyboardState.IsKeyDown(Keys.X))
            {
                if (press == false)
                {
                    press = true;
                    aux = new disparo(1, true, t_disp[d_act], lista, 9.0f, pos);
                    if (Game1.efecto == 2)
                    {
                        aux = new disparo(1, true, t_disp[d_act], lista, 9.0f, new Vector2( pos.X,pos.Y + 20));
                        aux = new disparo(1, true, t_disp[d_act], lista, 9.0f, new Vector2(pos.X, pos.Y - 20));
                    }
                    Game1.sonidos.PlayCue("disparo2");
                }
            }
            else
                press = false;

            if (Math.Abs(pos.X) > 440)
                pos.X = (pos.X > 0 ? 440 : -440);
            if (Math.Abs(pos.Y) > 340)
                pos.Y = (pos.Y > 0 ? 340 : -340);

            box.actualiza(new Vector3(pos.X - 40f, pos.Y - 7f, -40f), new Vector3(pos.X + 40f, pos.Y + 16f, +40f));
        }
       
    }
}
