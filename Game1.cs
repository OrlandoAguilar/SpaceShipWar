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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region variables
        //constantes
        public const int pAncho = 800;
        public const int pAlto = 600;
        public const int FPS = 30;
        public const int totalEnem = 15;

        //globales
        public static Prota naveP;
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        public static enemigo[] enemigos = new enemigo[totalEnem];
        public static bool gameover = false;
        public static int record = 0;
        public static Texture2D estr;
        public static int ener=100;
        public static byte nivel = 1;
        public static AudioEngine audio;
        public static WaveBank ondas;
        public static SoundBank sonidos;
        public static Vector3 cameraPosition = new Vector3(0.0f, 0.0f, -900.0f);
        public static float aspectRatio;
        public static ContentManager cnt;
        public static Model[] gema = new Model[4];
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice dv;
        public static Matrix View, Projection;
        public static int efecto=-1;
        public static int max_record;
        public static int pantalla = 0;
        public static bool ext = false;
        public static float musicVolume = 1.0f;
        public static float fondoVolume = 0.25f;
        public static float time=0.0f;

        private Model[] g_disp = new Model[4];
        private Texture2D[] g_explo = new Texture2D[21];
        private Model g_nave;
        private Model[] g_enems = new Model[3];
        private Model g_nab;
        private Model g_npics;
        private Model g_naanch;

        Texture2D scroll;
        private Vector2 scrollP;
        private anim[] animaciones = new anim[totalEnem];
        private Texture2D barra;
        private Texture2D borde;

        
        SpriteBatch spriteBatch;
        KeyboardState keyboardState = Keyboard.GetState();
        SpriteFont fuente;
        SpriteFont f_presentacion;
        disps d_lis;
        private int cont = 100;
        private bool flag;
        //modelo md;
        int ind = 0;
        item objeto;
        int contad=0;
        bool salvado;
        AudioCategory musicCategory;
        modelo logo;
        #endregion

        // using XnaConsole;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cnt = this.Content;
            graphics.PreferredBackBufferWidth = pAncho;
            graphics.PreferredBackBufferHeight = pAlto;
            Window.Title = "Guerra de Naves";
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / FPS);
            for (int z = 0; z < totalEnem; z++)
            {
                enemigos[z] = null;
                animaciones[z] = null;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            g_nave = Content.Load<Model>("modelos/nave1");
            g_enems[1] = g_enems[1] = Content.Load<Model>("modelos/nave6"); //misil
            g_enems[2] = Content.Load<Model>("modelos/nave5");//bomba
            g_nab = Content.Load<Model>("modelos/nave3");
            g_npics = Content.Load<Model>("modelos/nave4");
            g_naanch = Content.Load<Model>("modelos/nave2");
            for (int z = 0; z < 4; z++)
                gema[z] = Content.Load<Model>("modelos/gems"+(z+1));
            for (int z = 0; z < 4; z++)
                g_disp[z] = Content.Load<Model>("modelos/disparo"+z);
            for (int z = 0; z < 21; z++)
                g_explo[z] = Content.Load<Texture2D>("graphics/explos/screen" + z);
            fuente = Content.Load<SpriteFont>("fuentes/SpriteFont1");
            f_presentacion = Content.Load<SpriteFont>("fuentes/presentacion");
            estr = Content.Load<Texture2D>("graphics/est");
            barra = Content.Load<Texture2D>("graphics/barra");
            borde = Content.Load<Texture2D>("graphics/borde");
            audio = new AudioEngine("content/sound/sonidos.xgs");
            ondas = new WaveBank(audio,"content/sound/Wave Bank.xwb");
            sonidos = new SoundBank(audio,"content/sound/Sound Bank.xsb");
            Song miCancion;
	        miCancion = Content.Load<Song>("sound/rockshit");
            MediaPlayer.Play(miCancion);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = fondoVolume;
            aspectRatio = (float)graphics.PreferredBackBufferWidth /(float)graphics.PreferredBackBufferHeight;

            max_record = EO.load(StorageContainer.TitleLocation + "/Content/datos/datos.txt");
            View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                    aspectRatio, 1.0f, 2000.0f);

            musicCategory = audio.GetCategory("Default");
            pantalla = -2;
            logo = new modelo(Content.Load<Model>("modelos/logo"));
            
            init();
            base.Initialize();
        }
        private void init()
        {
            d_lis = new disps();
            scroll = Content.Load<Texture2D>("graphics/sc" + nivel);
            naveP = new Prota(g_nave, g_disp);
            naveP.set_pos(-150, 0);
            scrollP = new Vector2(0);
            ind = 0;
            objeto=null;
            contad = 0;
            efecto = -1;
            salvado = false;
            ener = 100;
            record = 0;
            max_record = EO.load(StorageContainer.TitleLocation + "/Content/datos/datos.txt");
            View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
            for (int z = 0; z < totalEnem; z++)
                enemigos[z] = null;

        }
          /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        private bool pres;
        private static float ltime=0.0f;

        public static float lastTime(){
            return ltime;
        }
         
        protected override void Update(GameTime gameTime)
        {
            ltime=gameTime.TotalRealTime.Seconds;
            //            int cont = 0;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ext)
                this.Exit();
            
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (pantalla == -1)
                {
                    cameraPosition.Z = -900;
                    View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
                }
                pantalla = 0;
            }
            
            if (keyboardState.IsKeyDown(Keys.P) && (pantalla==3 || pantalla==4))
            {
                if (!pres)
                    if (pantalla == 3)
                        pantalla = 4;
                    else
                        pantalla = 3;
                pres = true;
            }
            else
                pres = false;
            switch (pantalla)
            {
                case -2:    //inicializa todo
                    time = lastTime();
                    pantalla = -1;
                    cameraPosition.Z = -450;
                    break;
                case -1:    //anima introducción
                    if (++contad<70)
                    {
                        cameraPosition.Z += 4;
                        logo.mueve(0, contad/2.001f);
                        logo.rotar(cameraPosition.Z / 27.5f, 0, 0);
                        logo.tamanio(contad / 35f);
                        View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
                    }
                    if (contad>120)
                    {
                        cameraPosition.Z = -900;
                        View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
                        contad = 0;
                        pantalla = 0;
                    }
                    break;
                case 2:

                    if (lastTime() - time > 3)
                    {
                        pantalla = 3;
                    }
                    
                    break;
                case 3:
                    #region juego
                    if (efecto != -1)
                    {
                        switch (efecto)
                        {
                            case 1:
                                contad = 500;
                                efecto = -1;
                                break;
                            case 3:
                                for (int z = 0; z < totalEnem; z++)
                                    if (enemigos[z] != null)
                                    {
                                        animaciones[z] = new anim(g_explo, 21, enemigos[z].get_pos());
                                        record += 5;
                                        enemigos[z] = null;
                                    }
                                ind = 0;
                                efecto = -1;
                                break;
                        }


                    }
                    if (contad == 10)
                    {
                        View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
                    }
                    if (contad > 0)
                        contad--;

                    //si el prota esta vivo, animar todo
                    if (ener > 0)
                    {
                        naveP.update(keyboardState);
                        if (rnd.Next(500) > 290)
                            for (int z = 0; z < totalEnem && ind < (nivel * 2 + 5); z++)
                            {
                                if (enemigos[z] == null)
                                {
                                    switch (rnd.Next(5))
                                    {
                                        case 0:
                                            enemigos[z] = new NaveBsl(g_naanch, g_disp);
                                            break;
                                        case 1:
                                            enemigos[z] = new NaveC(g_nab, g_disp);
                                            break;
                                        case 2:
                                            enemigos[z] = new NaveSC(g_npics, g_disp);
                                            break;
                                        case 3:
                                            enemigos[z] = new misil(g_enems[1], g_disp);
                                            break;
                                        case 4:
                                            enemigos[z] = new bomba(g_enems[2], g_disp);
                                            break;
                                    }

                                    ind++;
                                    break;
                                }
                            }

                        for (int z = 0; z < totalEnem; z++)
                            if (enemigos[z] != null)
                                if (enemigos[z].muerto)
                                {
                                    if (enemigos[z].get_pos().X > -440)
                                    {
                                        animaciones[z] = new anim(g_explo, 21, enemigos[z].get_pos());
                                        disps aux2 = d_lis;
                                        while (aux2.sig != null)
                                        {
                                            aux2 = aux2.sig;
                                        }
                                        aux2.sig = enemigos[z].lista.sig;
                                        if (aux2.sig != null)
                                            (aux2.sig).ant = aux2;
                                    }
                                    enemigos[z] = null;

                                    ind--;
                                }
                                else
                                {
                                    enemigos[z].update();
                                    disps aux2 = enemigos[z].lista.sig;
                                    while (aux2 != null)
                                    {
                                        if (aux2.elem.muerto)
                                        {
                                            disps auxA = aux2.ant;
                                            auxA.sig = aux2.sig;
                                            auxA = aux2.sig;
                                            if (auxA != null)
                                                auxA.ant = aux2.ant;
                                        }
                                        else
                                            aux2.elem.update();
                                        aux2 = aux2.sig;

                                    }
                                }

                        disps aux = naveP.lista.sig;
                        while (aux != null)
                        {
                            if (aux.elem.muerto)
                            {
                                disps auxA = aux.ant;
                                auxA.sig = aux.sig;
                                auxA = aux.sig;
                                if (auxA != null)
                                    auxA.ant = aux.ant;
                            }
                            else
                                aux.elem.update();
                            aux = aux.sig;

                        }

                        disps aux3 = d_lis.sig;

                        while (aux3 != null)
                        {
                            if (aux3.elem.muerto)
                            {
                                //   aux3.elem = null;
                                disps auxA = aux3.ant;
                                auxA.sig = aux3.sig;
                                auxA = aux3.sig;
                                if (auxA != null)
                                    auxA.ant = aux3.ant;
                            }
                            else
                                aux3.elem.update();
                            aux3 = aux3.sig;
                        }
                        if (objeto == null)
                        {
                            if (rnd.Next(nivel * 50 + 10) == nivel)
                                objeto = new item(rnd.Next(4));
                        }
                        else
                            if (objeto.muerto)
                                objeto = null;
                            else
                                objeto.update();
                    }
                    else
                    //si no esta vivo el prota
                    {
                        if (!salvado && record > max_record)
                        {
                            EO.save(StorageContainer.TitleLocation + "/Content/datos/datos.txt", record);
                            salvado = true;
                        }
                    }
                    #endregion 
                    break;
                case 0:
                    naveP.Selec(keyboardState);
                    if (pantalla == 2)
                    {
                        init();
                        time = lastTime();
                    }
                    else
                    {
                        View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Down);
                    }
                    break;
                case 1:
                    naveP.opc(keyboardState);
                    if (pantalla==0)
                    {
                        musicCategory.SetVolume(musicVolume);
                        sonidos.PlayCue("disparo");
                        MediaPlayer.Volume = fondoVolume;
                    }
                    break; 
                    
            }
            audio.Update();
            base.Update(gameTime);
        }
        private void Dscroll(SpriteBatch sp, GameTime gameTime)
        {
            if (ener > 0 && pantalla==3 )
            {
                scrollP.X -= pAncho / (FPS * 60f);
                scrollP.Y -= pAlto / (FPS * 60f);
            }
            sp.Draw(scroll, scrollP, null, Color.White);
            if (scrollP.Y < (-scroll.Height + pAlto) || scrollP.X < (-scroll.Width + pAncho))
            {
                nivel = (byte)((int)(++nivel) % 6);
                time = (float)gameTime.TotalRealTime.Seconds;
                init();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        	

	// Posición de la cámara para la matriz de vista
        Color colorese=new Color(255,255,255,200);
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            dv = GraphicsDevice;
            
            switch (pantalla)
            {
     
                case 3:case 4:
                    #region pintado 2d
                    spriteBatch.Begin();
                    Dscroll(spriteBatch, gameTime);
                    spriteBatch.Draw(borde, new Vector2(5, 15), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(barra, new Vector2(10, 20), new Rectangle(0, 0, ener, 14), new Color((byte)0, (byte)0, (byte)cont), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(fuente, "Puntuacion " + record, new Vector2(130, 10), Color.White);
                    spriteBatch.DrawString(fuente, "Max Puntuacion " + max_record, new Vector2(130, 30), Color.Red);
                    if (ener<=0)
                    {
                        spriteBatch.DrawString(f_presentacion, "Juego Terminado", new Vector2(pAncho >> 1, (pAlto >> 1) - 10), Color.Red);
                        spriteBatch.DrawString(f_presentacion, "Presiona ESC", new Vector2(pAncho >> 1, (pAlto >> 1) + 15), Color.Red);
                    }
                    if (pantalla==4)
                        spriteBatch.DrawString(f_presentacion, "Pausa", new Vector2(pAncho >> 1, pAlto >> 1), Color.Blue);
                    spriteBatch.End();
                    #endregion

                    #region pintado 3d
                    GraphicsDevice.RenderState.DepthBufferEnable = true;
                    GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    //GraphicsDevice.RenderState.AlphaTestEnable = false;
                    naveP.draw();
                    for (int z = 0; z < totalEnem; z++)
                    {
                        if (enemigos[z] != null)
                        {
                            enemigos[z].draw();
                            disps aux = enemigos[z].lista;
                            while (aux.sig != null)
                            {
                                aux = aux.sig;
                                aux.elem.draw();
                            }
                        }

                        if (animaciones[z] != null)
                            if (animaciones[z].muerto)
                                animaciones[z] = null;
                            else
                                animaciones[z].draw();
                    }
                    //dibuja disparos
                    disps aux2 = naveP.lista;
                    while (aux2.sig != null)
                    {
                        aux2 = aux2.sig;
                        aux2.elem.draw();
                    }

                    disps aux3 = d_lis;
                    while (aux3.sig != null)
                    {

                        aux3 = aux3.sig;
                        //if (!aux3.elem.muerto)
                        aux3.elem.draw();

                    }
                    if (flag)
                    {
                        if ((++cont) > 254)
                            flag = false;
                    }
                    else
                        if (--cont < 150)
                            flag = true;


                    if (objeto != null)
                        objeto.draw();
                    #endregion
                    break;
                case -1:
                    spriteBatch.Begin();
                    Dscroll(spriteBatch, gameTime);
                    spriteBatch.End();
                    GraphicsDevice.RenderState.DepthBufferEnable = true;
                    GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    logo.draw();
                    break;
                case 0:
                    #region pintado 2d y 3d presentacion
                    spriteBatch.Begin();
                    Dscroll(spriteBatch, gameTime);
                    colorese.R = (byte) cont;
                    colorese.G = (byte)(cont-30);
                    colorese.B = (byte)(cont - 50);
                    spriteBatch.DrawString(f_presentacion, "Guerra de Naves ", new Vector2(pAncho/3, pAlto>>2), colorese);
                    spriteBatch.DrawString(f_presentacion, "Iniciar ", new Vector2(177, 308), colorese);
                    spriteBatch.DrawString(f_presentacion, "Opciones ", new Vector2(177, 377), colorese);
                    spriteBatch.DrawString(f_presentacion, "Salir ", new Vector2(177, 454), colorese);
                    spriteBatch.End();
                    GraphicsDevice.RenderState.DepthBufferEnable = true;
                    GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    
                    naveP.draw();
                    if (flag)
                    {
                        if ((++cont) > 254)
                            flag = false;
                    }
                    else
                        if (--cont < 150)
                            flag = true;
                    #endregion
                    break;
                case 1:
                    #region pintado 2d y 3d opciones
                    spriteBatch.Begin();
                    Dscroll(spriteBatch, gameTime);
                    colorese.R = (byte)cont;
                    colorese.G = (byte)(cont - 30);
                    colorese.B = (byte)(cont - 50);
                    spriteBatch.DrawString(f_presentacion, "Volumen fx", new Vector2(177, 308), colorese);
                    spriteBatch.Draw(borde, new Vector2(445, 303), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(barra, new Vector2(450, 308), new Rectangle(0, 0, (int)(musicVolume*100), 14), new Color((byte)0, (byte)0, (byte)cont), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(f_presentacion, "Volumen Musica", new Vector2(177, 377), colorese);
                    spriteBatch.Draw(borde, new Vector2(445, 372), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(barra, new Vector2(450, 377), new Rectangle(0, 0, (int)(fondoVolume * 100f), 14), new Color((byte)0, (byte)0, (byte)cont), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(f_presentacion, "Reiniciar Puntuación Max", new Vector2(177, 454), colorese);
                    spriteBatch.DrawString(f_presentacion, "Salir", new Vector2(177, 524), colorese);
                    spriteBatch.End();
                    GraphicsDevice.RenderState.DepthBufferEnable = true;
                    GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    naveP.draw();
                    if (flag)
                    {
                        if ((++cont) > 254)
                            flag = false;
                    }
                    else
                        if (--cont < 150)
                            flag = true;
                    #endregion
                    break;
                case 2:
                    #region pintado 2d y 3d presentacion
                    spriteBatch.Begin();
                    Dscroll(spriteBatch, gameTime);
                    colorese.R = (byte)cont;
                    colorese.G = (byte)(cont - 30);
                    colorese.B = (byte)(cont - 50);
                    spriteBatch.DrawString(f_presentacion, "Nivel "+ nivel, new Vector2(pAncho>>1, pAlto>>1), colorese);
                    spriteBatch.DrawString(f_presentacion, "" + (3-lastTime() + time), new Vector2(pAncho >> 1, (pAlto >> 1)+70), colorese);
                    
                    spriteBatch.End();
                    GraphicsDevice.RenderState.DepthBufferEnable = true;
                    GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    naveP.draw();
                    if (flag)
                    {
                        if ((++cont) > 254)
                            flag = false;
                    }
                    else
                        if (--cont < 150)
                            flag = true;
                    #endregion
                    break;
            }

            base.Draw(gameTime);    
        }
        
    }
}

