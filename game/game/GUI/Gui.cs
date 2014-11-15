using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace game.gui
{
    public partial class Gui : Form
    {
        private GameManager gameManager = GameManager.getGameManagerInstance();
        public Gui() : base()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            InitializeComponent();
            this.board.Paint += board_PaintMap;
            this.board.Paint += board_PaintEntities;
            this.board.KeyPress += board_KeyPress;
            this.chatInput.KeyPress += chat_KeyPress;
        }

        private void board_PaintMap(object sender, PaintEventArgs e)
        {
            Map map = this.gameManager.getMap();
            Field[,] fields = map.getFields();
            Size tileSize = this.getTileSize();
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(this.board.CreateGraphics(), this.board.DisplayRectangle);
            Graphics graphics = this.CreateGraphics();
            for (int i = 0; i < map.getHeight(); i++)
            {
                for (int j = 0; j < map.getWidth(); j++)
                {
                    this.drawMapField(buffer.Graphics, fields[i,j], j * tileSize.Width, i * tileSize.Height, tileSize.Width, tileSize.Height);
                }
            }
            buffer.Render();
        }

        protected void drawMapField(Graphics graphics, Field field, int absX, int absY, int width, int height)
        {
            Color colour = Color.BurlyWood;
            if (field.isForest())
            {
                if (field.isHuntable())
                {
                    colour = Color.YellowGreen;
                }
                else
                {
                    colour = Color.Green;
                }
            }
            else if (field.isWater())
            {
                colour = Color.Blue;
            }
            else if (!field.isWalkable())
            {
                colour = Color.DimGray;
            }
            
            graphics.FillRectangle(new SolidBrush(colour), absX, absY, width, height);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(absX, absY, width, height));
        }

        private void board_PaintEntities(object sender, PaintEventArgs e)
        {
            //List<Dragon> dragons = this.gameManager.getDragons();
            //List<Player> players = this.gameManager.getPlayers();
            List<Dragon> dragons = gameManager.getDragons();
            List<Player> players = gameManager.getPlayers();

            foreach (Dragon dragon in dragons)
            {
                this.drawDragon(e.Graphics, dragon);
            }
            foreach (Player player in players)
            {
                this.drawPlayer(e.Graphics, player);
            }
        }

        private void drawDragon(Graphics graphics, Dragon dragon)
        {
            Size tileSize = this.getTileSize();
            graphics.FillRectangle(new SolidBrush(Color.DarkRed),
                dragon.getXPos() *tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
                dragon.getYPos() *tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4,
                tileSize.Width / 2,
                tileSize.Height / 2);
        }

        protected void drawPlayer(Graphics graphics, Player player)
        {
            Size tileSize = this.getTileSize();
            graphics.FillRectangle(new SolidBrush(Color.DarkGoldenrod),
               player.getXPos() * tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
               player.getYPos() * tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4,
               tileSize.Width / 2,
               tileSize.Height / 2);
        }

        private Size getTileSize()
        {
            int width = 10;
            int height = 10;
            Field[,] fields = this.gameManager.getMap().getFields();
            if (fields == null)
            {
                throw new ArgumentNullException("backend returned null as map");
            }
            if (fields.Length == 0)
            {
                throw new IndexOutOfRangeException("outer dimension of the retrieved map has length 0");
            }
            if (fields.GetLength(0) == 0)
            {
                throw new IndexOutOfRangeException("inner dimension of the retrieved map has length 0");
            }
            int cellWidth = this.board.Size.Width / fields.GetLength(0);
            int cellHeight = this.board.Size.Height / fields.GetLength(1);
            return new Size(cellWidth, cellHeight);
        }

        private void board_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // fall-through-cases for capital letters
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    this.chatInput.Focus();
                    break;
                case 'a':
                case 'A':
                    Console.Out.WriteLine("Command sent: 'ask:mv:lft'");//this.backend.sendCommand("ask:mv:lft");
                    break;
                case 'd':
                case 'D':
                    Console.Out.WriteLine("Command sent: 'ask:mv:rgt'");//this.backend.sendCommand("ask:mv:rgt");
                    break;
                case 'w':
                case 'W':
                    Console.Out.WriteLine("Command sent: 'ask:mv:up'");//this.backend.sendCommand("ask:mv:up");
                    break;
                case 's':
                case 'S':
                    Console.Out.WriteLine("Command sent: 'ask:mv:dwn'");//this.backend.sendCommand("ask:mv:dwn");
                    break;
            }
        }

        private void chat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string input = this.chatInput.Text.Trim();
                this.chatInput.Text = "";
                // ignore empty input
                if (input != "")
                {
                    if (input.StartsWith("/"))
                    {
                        input = input.Substring(1, input.Length - 1);
                        Console.Out.WriteLine("Command sent: '/" + input +"'");//this.backend.sendCommand(input);
                    }
                    else
                    {
                        Console.Out.WriteLine("Message sent: '" + input + "'");//this.backend.sendChat(input);
                    }
                }
                this.board.Focus();
            }

        }
 
    }
}
