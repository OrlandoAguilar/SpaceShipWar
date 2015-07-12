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
    public class anim
    {
        Texture2D[] txt;
        int cant,ind;
        Vector2 pos;
        public bool muerto=false;
        Color Col=Color.White;
        float siz;
        BasicEffect quadEffect;
        Quad quad;
        VertexDeclaration quadVertexDecl;
        

        public anim(Texture2D[] t,int cantt,Vector2 p)
        {
            txt=t;
            cant=cantt;
            pos = p;
            init();
        }
        public anim(Texture2D t, Vector2 p)
        {
            txt = new Texture2D[1];
            txt[0] = t;
            cant = -1;
            pos = p;
            init();
        }

        private void init()
        {
            quad = new Quad(Vector3.Zero, new Vector3(0,0,-1), Vector3.Down, 100, 100);
            quadEffect = new BasicEffect(Game1.graphics.GraphicsDevice, null);
            quadEffect.EnableDefaultLighting();

            quadEffect.World = Matrix.CreateTranslation(new Vector3 (pos.X,pos.Y,0));// Matrix.Identity;
            quadEffect.View = Game1.View;
            quadEffect.Projection = Game1.Projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = txt[0];
            quadVertexDecl = new VertexDeclaration(Game1.graphics.GraphicsDevice,
               VertexPositionNormalTexture.VertexElements);
        }
        public void set_color(Color c)
        {
            Col = c;
        }
        public void set_size(float c)
        {
            siz = c;
        }
        public void set_pos(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
            quad.Origin.X = x;
            quad.Origin.Y = y;
        }

        public void draw()
        {
            //spriteBatch.Draw(txt[ind], pos, null, Col, 0, new Vector2(txt[ind].Width >> 1, txt[ind].Height >>1), siz, SpriteEffects.None, 1);
            quadEffect.Texture = txt[ind];
            if (cant != -1)
            {
                ind++;
                if (ind >= cant)
                    muerto = true;
            }
            
            Game1.dv.VertexDeclaration = quadVertexDecl;
            quadEffect.Begin();

            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                Game1.dv.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList, 
                    quad.Vertices, 0, 4, 
                    quad.Indexes, 0, 2);

                pass.End();
            }
            quadEffect.End();
            
        }
    }
}
