using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Jogo
{

    public enum GameState { Null, MainMenu, PlayHuman, PlayComputer, EndGame };

    public class Game1 : Game
    {


        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        Texture2D m_tex2D;
        Texture2D m_fundo;
        Texture2D m_playerM;
        Texture2D m_playerL;
        Texture2D m_playerM_wins;
        Texture2D m_playerB_wins;
        Texture2D m_draw;
        Texture2D m_tarja;
        Texture2D m_tarjaVert;
        Texture2D m_tarjaLat1;
        Texture2D m_tarjaLat2;
        SpriteFont m_font;
        Texture2D buttonBg;
        Texture2D buttonOneBlBg;

        Button m_buttonOnePlayer;
        //Button m_buttonTwoPlayers;
        Button m_buttonReset;
        Button m_buttonMenu;
        Button m_buttonDificult;
        Button m_buttonPlayerStarts;
        Button m_buttonGoPlay;

        SoundEffect m_sndInicio;
        SoundEffect m_sndJog_mario;
        SoundEffect m_sndJog_bowser;
        SoundEffect m_snd_bowser_win;
        SoundEffect m_snd_mario_win;
        SoundEffect m_snd_draw;
        SoundEffect m_snd_coin;

        SoundEffectInstance m_music;

        float m_timer = 0.0f;

        bool m_diffEasy = false;
        bool m_humanStarts = true;
        bool m_onePlayer = true;
        //bool m_pvp = false;
        //bool m_human = true;


        int i_state = 3;

        public static MouseState m_prevMouseState;

        Table m_table = new Table();

        GameState m_currState = GameState.Null;

        void EnterGameState(GameState newState)
        {
            LeaveGameState();

            m_currState = newState;
            switch (m_currState)
            {
                case GameState.MainMenu:
                    { }
                    break;

                case GameState.PlayHuman:
                    { }
                    break;

                case GameState.PlayComputer:
                    {

                        
                    }
                    break;

                case GameState.EndGame:
                    {

                       if(i_state == 1)
                        {
                            InitMarioWin();
                        }else if(i_state == -1)
                        {
                            InitBowserWin();
                        }
                        else if(i_state == 2)
                        {
                            InitDraw();
                        }
                    }
                    break;
            }
        }

        void LeaveGameState()
        {
            switch (m_currState)
            {
                case GameState.MainMenu:
                    {
                       m_sndInicio.Play();
                    }
                    break;

                case GameState.PlayHuman:
                    {
                        
                    }
                    break;

                case GameState.PlayComputer:
                    {
                        
                    }
                    break;

                case GameState.EndGame:
                    {
                        
                    }
                    break;
            }
        }

        void UpdateGameState(GameTime gameTime)
        {
            switch (m_currState)
            {
                case GameState.MainMenu:
                    {
                        if (m_buttonGoPlay.Update(gameTime))
                        {
                            //m_pvp = false;
                            //m_table.Reset();
                            if (m_humanStarts)
                            {
                                EnterGameState(GameState.PlayHuman);
                            }else
                            {
                                EnterGameState(GameState.PlayComputer);
                            }
                            
                        }

                        if (m_buttonDificult.Update(gameTime))
                        {
                            m_snd_coin.Play();
                            m_diffEasy = !m_diffEasy;
                            
                        }

                        if (m_diffEasy)
                        {
                            m_buttonDificult.m_text = "EASY";

                            //m_testButton5.m_bgColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
                            //m_testButton6.m_bgColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                        }
                        else
                        {
                            m_buttonDificult.m_text = "HARD";
                        }


                        if(m_buttonPlayerStarts.Update(gameTime))
                        {
                            m_snd_coin.Play();
                            m_humanStarts = !m_humanStarts;
                        }

                        if (m_humanStarts)
                        {
                            m_buttonPlayerStarts.m_text = "HUMAN START";
                        }
                        else
                        {
                            m_buttonPlayerStarts.m_text = "PC START";
                        }


                        if (m_buttonOnePlayer.Update(gameTime))
                        {
                            m_snd_coin.Play();
                            m_onePlayer = !m_onePlayer;
                        }
                        
                        if (m_onePlayer)
                        {
                            m_buttonOnePlayer.m_text = "1 PLAYER";
                        }
                        else
                        {
                            m_buttonOnePlayer.m_text = "2 PLAYERS";
                        }



                    }
                    break;

                case GameState.PlayHuman:
                    {

                        if (m_buttonReset.Update(gameTime))
                        {
                            m_snd_coin.Play();
                            m_table.Reset();

                            if (m_humanStarts)
                            {
                                EnterGameState(GameState.PlayHuman);
                            }else
                            {
                                EnterGameState(GameState.PlayComputer);
                            }
                            
                        }

                        if (m_buttonMenu.Update(gameTime))
                        {
                            m_snd_coin.Play();
                            m_table.Reset();
                            EnterGameState(GameState.MainMenu);
                        }

                        if ((Mouse.GetState().LeftButton == ButtonState.Pressed) &&
                        (m_prevMouseState.LeftButton != ButtonState.Pressed))
                        {

                            int x = Mouse.GetState().Position.X / 200;
                            int y = Mouse.GetState().Position.Y / 200;

                            if ((Mouse.GetState().Position.X < 600 && Mouse.GetState().Position.Y < 600) && m_table.Get(x, y).ToString().Equals(" "))
                            {
                                m_sndJog_mario.Play();
                                m_table.Set(x, y, 'X');
                                i_state = m_table.GetComputerScore('X');

                                if(i_state == 0)
                                {
                                    if (m_onePlayer)
                                    {
                                        m_timer = 0.4f;
                                    }
                                    EnterGameState(GameState.PlayComputer);
                                }
                                else
                                {
                                    m_timer = 1.0f;
                                    EnterGameState(GameState.EndGame);
                                }
                                
                            }

                        }
                        
                    }
                    break;

                case GameState.PlayComputer:
                    {
                        if (m_buttonReset.Update(gameTime))
                        {
                            m_table.Reset();
                            m_snd_coin.Play();

                            if (m_humanStarts)
                            {
                                EnterGameState(GameState.PlayHuman);
                            }
                            else
                            {
                                EnterGameState(GameState.PlayComputer);
                            }
                        }

                        if (m_buttonMenu.Update(gameTime))
                        {
                            m_table.Reset();
                            m_snd_coin.Play();
                            EnterGameState(GameState.MainMenu);
                        }


                        m_timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (m_timer < 0.0f)
                            m_timer = 0.0f;

                        if (m_onePlayer && m_diffEasy && m_timer <= 0)
                        {

                            Random rnd = new Random();
                            int x_rdn = rnd.Next(3);
                            int y_rdn = rnd.Next(3);

                            if (m_table.Get(x_rdn, y_rdn).ToString().Equals(" "))
                            {
                                m_sndJog_bowser.Play();
                                m_table.Set(x_rdn, y_rdn, 'O');
                                i_state = m_table.GetComputerScore('O');

                                if (i_state == 0)
                                {

                                    EnterGameState(GameState.PlayHuman);
                                }
                                else
                                {
                                    m_timer = 1.0f;
                                    EnterGameState(GameState.EndGame);
                                }


                            }

                        }
                        else if (!m_onePlayer)
                        {
                            if ((Mouse.GetState().LeftButton == ButtonState.Pressed) &&
                        (m_prevMouseState.LeftButton != ButtonState.Pressed))
                            {

                                int x = Mouse.GetState().Position.X / 200;
                                int y = Mouse.GetState().Position.Y / 200;

                                if ((Mouse.GetState().Position.X < 600 && Mouse.GetState().Position.Y < 600) && m_table.Get(x, y).ToString().Equals(" "))
                                {

                                    m_sndJog_bowser.Play();
                                    m_table.Set(x, y, 'O');
                                    i_state = m_table.GetComputerScore('O');

                                    if (i_state == 0)
                                    {
                                        EnterGameState(GameState.PlayHuman);
                                    }
                                    else
                                    {
                                        m_timer = 1.0f;
                                        EnterGameState(GameState.EndGame);
                                    }

                                }

                            }
                        }
                        else


                       if (m_onePlayer && !m_diffEasy && m_timer <= 0)
                        {
                            Minimax minimax = new Minimax();
                            minimax.calc_minimax(m_table);

                            m_sndJog_bowser.Play();
                            i_state = m_table.GetComputerScore('O');

                            if (i_state == 0)
                            {

                                EnterGameState(GameState.PlayHuman);
                            }
                            else
                            {
                                m_timer = 1.0f;
                                EnterGameState(GameState.EndGame);
                            }

                        }
                        
                    }
                    break;

                case GameState.EndGame:
                    {

                        if (m_buttonReset.Update(gameTime))
                        {
                            
                            m_music.Stop();
                            m_table.Reset();
                            m_snd_coin.Play();

                            if (m_humanStarts)
                            {
                                EnterGameState(GameState.PlayHuman);
                            }
                            else
                            {
                                EnterGameState(GameState.PlayComputer);
                            }
                        }

                        if (m_buttonMenu.Update(gameTime))
                        {
                            m_music.Stop();
                            m_table.Reset();
                            m_snd_coin.Play();
                            EnterGameState(GameState.MainMenu);
                        }
                    }
                    break;
            }
        }


        void DrawGameState(GameTime gameTime)
        {
            switch (m_currState)
            {
                case GameState.MainMenu:
                    {

                         m_spriteBatch.Draw(
                            m_fundo,              //texture
                            new Vector2(0.0f, 0.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            1.0f                  //depth
                        );

                        m_buttonOnePlayer.Draw(gameTime, m_spriteBatch);
                        m_buttonDificult.Draw(gameTime, m_spriteBatch);
                        m_buttonPlayerStarts.Draw(gameTime, m_spriteBatch);
                        m_buttonGoPlay.Draw(gameTime, m_spriteBatch);
                    }
                    break;

                case GameState.PlayHuman:
                    {

                        DrawBoard();
                        m_buttonReset.Draw(gameTime, m_spriteBatch);
                        m_buttonMenu.Draw(gameTime, m_spriteBatch);
                        
                    }
                    break;

                case GameState.PlayComputer:
                    {

                        DrawBoard();
                        m_buttonReset.Draw(gameTime, m_spriteBatch);
                        m_buttonMenu.Draw(gameTime, m_spriteBatch);
                        
                    }
                    break;

                case GameState.EndGame:
                    {
                       
                    DrawBoard();

                    int i = m_table.GetTarja();

                        if (i == 0)
                        {
                            m_spriteBatch.Draw(
                    m_tarja,          //texture
                    new Vector2(0.0f, 100.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                        }

                    if (i == 1)
                    {
                    m_spriteBatch.Draw(
                    m_tarja,          //texture
                    new Vector2(0.0f, 300.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                        }

                     if (i == 2)
                     {
                     m_spriteBatch.Draw(
                     m_tarja,          //texture
                     new Vector2(0.0f, 500.0f), //position
                     null,
                     null,
                     Vector2.Zero,         //origin
                     0.0f,                 //rotation
                     new Vector2(1.0f, 1.0f),            //scale
                     Color.White,          //color
                     SpriteEffects.None,   //flip?
                     0.0f                  //depth
                     );
                     }

                    if (i == 3)
                    {
                    m_spriteBatch.Draw(
                    m_tarjaVert,          //texture
                    new Vector2(100.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );

                        }

                    if (i == 4)
                    {
                    m_spriteBatch.Draw(
                    m_tarjaVert,          //texture
                    new Vector2(300.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );

                        }



                    if (i == 5)
                    {
                    m_spriteBatch.Draw(
                    m_tarjaVert,          //texture
                    new Vector2(500.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                        }



                    if (i == 6)
                    {
                    m_spriteBatch.Draw(
                    m_tarjaLat2,          //texture
                    new Vector2(0.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                        }


                    if (i == 7)
                    {
                    m_spriteBatch.Draw(
                    m_tarjaLat1,          //texture
                    new Vector2(0.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                        }

                    m_timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (m_timer < 0.0f)
                        m_timer = 0.0f;

                    if (i_state == 1 && m_timer == 0.0f)
                    {
                    m_table.Reset();

                    m_spriteBatch.Draw(
                    m_playerM_wins,          //texture
                    new Vector2(0.0f, 0.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                    }
                                   
                     if (i_state == -1 && m_timer == 0.0f)
                     {
                     m_table.Reset();

                     m_spriteBatch.Draw(
                     m_playerB_wins,          //texture
                     new Vector2(0.0f, 0.0f), //position
                     null,
                     null,
                     Vector2.Zero,         //origin
                     0.0f,                 //rotation
                     new Vector2(1.0f, 1.0f),            //scale
                     Color.White,          //color
                     SpriteEffects.None,   //flip?
                     0.0f                  //depth
                     );
                     }
                                  
                                   
                     if (i_state == 2)
                     {
                     m_table.Reset();

                     m_spriteBatch.Draw(
                     m_draw,          //texture
                     new Vector2(0.0f, 0.0f), //position
                     null,
                     null,
                     Vector2.Zero,         //origin
                     0.0f,                 //rotation
                     new Vector2(1.0f, 1.0f),            //scale
                     Color.White,          //color
                     SpriteEffects.None,   //flip?
                     0.0f                  //depth
                     );
                     }

                     m_buttonReset.Draw(gameTime, m_spriteBatch);
                     m_buttonMenu.Draw(gameTime, m_spriteBatch);

                    }
                    break;
            }
        }




        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.PreferredBackBufferWidth = 600;
            m_graphics.PreferredBackBufferHeight = 750;
            m_graphics.ApplyChanges();

            IsMouseVisible = true;
            
            Content.RootDirectory = "Content";
        }

     
        protected override void Initialize()
        {
            m_table.Reset();


            base.Initialize();

        }

 
        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_tex2D = Content.Load<Texture2D>("Sprites/Board");
            m_fundo = Content.Load<Texture2D>("Sprites/fundo");
            m_playerM = Content.Load<Texture2D>("Sprites/mario_player");
            m_playerL = Content.Load<Texture2D>("Sprites/bowser_player");

            m_playerM_wins = Content.Load<Texture2D>("Sprites/mario_wins");
            m_playerB_wins = Content.Load<Texture2D>("Sprites/browser_wins");
            m_draw = Content.Load<Texture2D>("Sprites/draw");
            m_tarja = Content.Load<Texture2D>("Sprites/tarja");
            m_tarjaVert = Content.Load<Texture2D>("Sprites/tarja_vert");
            m_tarjaLat1 = Content.Load<Texture2D>("Sprites/lat1");
            m_tarjaLat2 = Content.Load<Texture2D>("Sprites/lat2");

            m_font = Content.Load<SpriteFont>("Arial12");
            buttonBg = Content.Load<Texture2D>("Sprites/blocos");
            buttonOneBlBg = Content.Load<Texture2D>("Sprites/go");



            m_sndInicio = Content.Load<SoundEffect>("Sons/inicio");
            m_snd_coin = Content.Load<SoundEffect>("Sons/coin");
            m_sndJog_mario = Content.Load<SoundEffect>("Sons/jog_mario");
            m_sndJog_bowser = Content.Load<SoundEffect>("Sons/jog_bowser");
            m_snd_bowser_win = Content.Load<SoundEffect>("Sons/bowser_wins");
            m_snd_mario_win = Content.Load<SoundEffect>("Sons/mario_wins");
            m_snd_draw = Content.Load<SoundEffect>("Sons/draw");

            { //create buttons:
                


                m_buttonOnePlayer = new Button(
                    new Vector2(350, 050),
                    new Vector2(160, 140),
                    "1 PLAYER",
                    m_font,
                    buttonOneBlBg,
                    Color.Black
                );

                m_buttonGoPlay = new Button(
                    new Vector2(57, 85),
                    new Vector2(160, 163),
                    "GO PLAY!",
                    m_font,
                    buttonOneBlBg,
                    Color.Black
                );

                m_buttonReset = new Button(
                    new Vector2(75, 600),
                    new Vector2(160, 140),
                    "RESET",
                    m_font,
                    buttonOneBlBg,//buttonBg,
                    Color.Black
                );

                m_buttonMenu = new Button(
                    new Vector2(375, 600),
                    new Vector2(160, 140),
                    "MENU",
                    m_font,
                    buttonOneBlBg,//buttonBg,
                    Color.Black
                );

                m_buttonDificult = new Button(
                    new Vector2(350, 200),
                    new Vector2(160, 140),
                    "EASY",
                    m_font,
                    buttonOneBlBg,
                    Color.Black
                );

                m_buttonPlayerStarts = new Button(
                    new Vector2(350, 350), //new Vector2(275, 350),
                    new Vector2(160, 140), //new Vector2(323, 140),
                    "HUMAN START",
                    m_font,
                    buttonOneBlBg,//buttonBg,
                    Color.Black
                );

                //m_testButton.SetActive(false);
            }

            EnterGameState(GameState.MainMenu);
        }

        void InitMarioWin()
        {
            m_music = m_snd_mario_win.CreateInstance();
            m_music.Pan = 0.0f;
            m_music.Volume = 1.0f;
            m_music.Pitch = 0.0f;
            m_music.Play();
        }

        void InitBowserWin()
        {
            m_music = m_snd_bowser_win.CreateInstance();
            m_music.Pan = 0.0f;
            m_music.Volume = 1.0f;
            m_music.Pitch = 0.0f;
            m_music.Play();
        }

        void InitDraw()
        {
            m_music = m_snd_draw.CreateInstance();
            m_music.Pan = 0.0f;
            m_music.Volume = 1.0f;
            m_music.Pitch = 0.0f;
            m_music.Play();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //VERIFICA SE ESTA COM A TELA NO FOCO
            if (!IsActive)
            {
                return;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateGameState(gameTime);

            base.Update(gameTime);

            m_prevMouseState = Mouse.GetState();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            m_spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                null,
                SamplerState.LinearClamp
            );

            DrawGameState(gameTime);

            m_spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawBoard()
        {

            m_spriteBatch.Draw(
                            m_tex2D,              //texture
                            new Vector2(0.0f, 0.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            

            if (m_table.Get(0, 0).ToString().Equals("X"))
            {
                m_spriteBatch.Draw(
                            m_playerM,              //texture
                            new Vector2(30.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            }

            if (m_table.Get(0, 0).ToString().Equals("O"))
            {
                m_spriteBatch.Draw(
                            m_playerL,              //texture
                            new Vector2(30.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            }

            if (m_table.Get(1, 0).ToString().Equals("X"))
            {
                m_spriteBatch.Draw(
                            m_playerM,              //texture
                            new Vector2(230.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            }

            if (m_table.Get(1, 0).ToString().Equals("O"))
            {
                m_spriteBatch.Draw(
                            m_playerL,              //texture
                            new Vector2(230.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            }

            if (m_table.Get(2, 0).ToString().Equals("X"))
            {
                m_spriteBatch.Draw(
                            m_playerM,              //texture
                            new Vector2(430.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );
            }

            if (m_table.Get(2, 0).ToString().Equals("O"))
            {
                m_spriteBatch.Draw(
                            m_playerL,              //texture
                            new Vector2(430.0f, 30.0f), //position
                            null,
                            null,
                            Vector2.Zero,         //origin
                            0.0f,                 //rotation
                            new Vector2(1.0f, 1.0f),            //scale
                            Color.White,          //color
                            SpriteEffects.None,   //flip?
                            0.0f                  //depth
                        );

            }

            if (m_table.Get(0, 1).ToString().Equals("X"))
            {
                m_spriteBatch.Draw(
                        m_playerM,              //texture
                        new Vector2(30.0f, 230.0f), //position
                        null,
                        null,
                        Vector2.Zero,         //origin
                        0.0f,                 //rotation
                        new Vector2(1.0f, 1.0f),            //scale
                        Color.White,          //color
                        SpriteEffects.None,   //flip?
                        0.0f                  //depth
                    );
            }

            if (m_table.Get(0, 1).ToString().Equals("O"))
            {
                m_spriteBatch.Draw(
                        m_playerL,              //texture
                        new Vector2(30.0f, 230.0f), //position
                        null,
                        null,
                        Vector2.Zero,         //origin
                        0.0f,                 //rotation
                        new Vector2(1.0f, 1.0f),            //scale
                        Color.White,          //color
                        SpriteEffects.None,   //flip?
                        0.0f                  //depth
                    );
            }

                if (m_table.Get(1, 1).ToString().Equals("X"))
                {
                    m_spriteBatch.Draw(
                    m_playerM,              //texture
                    new Vector2(230.0f, 230.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }

                if (m_table.Get(1, 1).ToString().Equals("O"))
                {
                    m_spriteBatch.Draw(
                    m_playerL,              //texture
                    new Vector2(230.0f, 230.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }


                if (m_table.Get(2, 1).ToString().Equals("X"))
                {
                    m_spriteBatch.Draw(
                    m_playerM,              //texture
                    new Vector2(430.0f, 230.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }


                if (m_table.Get(2, 1).ToString().Equals("O"))
                {
                    m_spriteBatch.Draw(
                    m_playerL,              //texture
                    new Vector2(430.0f, 230.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }


                if (m_table.Get(0, 2).ToString().Equals("X"))
                {
                    m_spriteBatch.Draw(
                    m_playerM,              //texture
                    new Vector2(30.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }

                if (m_table.Get(0, 2).ToString().Equals("O"))
                {
                    m_spriteBatch.Draw(
                    m_playerL,              //texture
                    new Vector2(30.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }

                if (m_table.Get(1, 2).ToString().Equals("X"))
                {
                    m_spriteBatch.Draw(
                    m_playerM,              //texture
                    new Vector2(230.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }

                if (m_table.Get(1, 2).ToString().Equals("O"))
                {
                    m_spriteBatch.Draw(
                    m_playerL,              //texture
                    new Vector2(230.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }


                if (m_table.Get(2, 2).ToString().Equals("X"))
                {
                    m_spriteBatch.Draw(
                    m_playerM,              //texture
                    new Vector2(430.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }


                if (m_table.Get(2, 2).ToString().Equals("O"))
                {
                    m_spriteBatch.Draw(
                    m_playerL,              //texture
                    new Vector2(430.0f, 430.0f), //position
                    null,
                    null,
                    Vector2.Zero,         //origin
                    0.0f,                 //rotation
                    new Vector2(1.0f, 1.0f),            //scale
                    Color.White,          //color
                    SpriteEffects.None,   //flip?
                    0.0f                  //depth
                    );
                }
                
        }

    }
}
