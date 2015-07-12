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
    public class caja
    {
        public Vector3 pos,tam;
        private BoundingBox box;

        public BoundingBox Rect()
        {
            box.Min = pos;
            box.Max = tam;
            return (box);
        }
        public caja(Vector3 posc, Vector3 centor)
        {
            pos = posc;
            tam = centor;
            box = new BoundingBox(pos, tam);
        }

        public void actualiza(Vector3 posc, Vector3 centor)
        {
            box.Min = pos=posc;
            box.Max = tam=centor;
        }

        public bool intersect(caja otra)
        {
            return (Rect().Intersects(otra.Rect()));
        }
    }
}
