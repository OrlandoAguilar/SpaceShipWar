using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XnaInput;

namespace WindowsGame1
{
    public class modelo
    {
        Model miModelo;
        //new Vector3(Game1.pAncho>>1,Game1.pAlto>>1,0)
        Vector3 modelPosition = Vector3.Zero, rotacion=Vector3.Zero;
        float tamano=1f;

        public modelo(String nom)
        {
        miModelo = Game1.cnt.Load<Model>(nom);

        }

        public modelo(Model nom)
        {
            miModelo = nom;
        }

        public Vector3 get_pos(){
            return modelPosition;
        }

        public void mueve(float x, float y)
        {
            modelPosition.X = x;
            modelPosition.Y = y;
        }
        
        public void rotar(float x, float y,float z)
        {
            rotacion.X = x;
            rotacion.Y = y;
            rotacion.Z = z;
        }

        public void rotar(Vector3 r)
        {
            rotacion = r;
        }

        public void tamanio(float tam)
        {
            tamano = tam;
        }
        float trans;
        float giro;
        public void draw()
        {
            // Dibujamos el modelo, puede tener múltiples mallas, asi que iteramos sobre todas.
            foreach (ModelMesh mesh in miModelo.Meshes)
            {
                //Aquí es donde se fija la orientación de la malla, así como nuestra cámara y la proyección.
                trans = 1;
                if (mesh.Name == "cristal")
                    trans = 0.5f;

                if (mesh.Name == "gema")
                    trans = 0.8f;
                if (mesh.Name == "picos")
                    giro += 0.03f;
                
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    
                    effect.Alpha = trans;
                    if (mesh.Name == "picos")
                        effect.World = Matrix.CreateFromYawPitchRoll(rotacion.X + giro, rotacion.Y, rotacion.Z) *
                        Matrix.CreateTranslation(modelPosition);
                    else 
                        effect.World = Matrix.CreateScale(tamano) * Matrix.CreateFromYawPitchRoll(rotacion.X, rotacion.Y, rotacion.Z) * 
                        Matrix.CreateTranslation(modelPosition);
                        //* Matrix.CreateRotationY(rotacion.Y) * Matrix.CreateRotationX(rotacion.X) * Matrix.CreateRotationZ(rotacion.Z) ;
                    effect.View = Game1.View;
                    effect.Projection = Game1.Projection;
                }
                //Dibuja la malla, usando los valores de arriba.
                mesh.Draw();
                
                
            }
        }
    }
}
