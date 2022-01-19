using BattleSea.Model;
using BattleSea.Service;
using System;
using BattleSea.Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSea
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region VARIABLES
        private Chatter Chatter = new Chatter();
        private string ShipCell = Environment.CurrentDirectory + @"\Resources\ship_cell.jpg";
        private string ShipClean = Environment.CurrentDirectory + @"\Resources\clean.jpg";
        private string Miss = Environment.CurrentDirectory + @"\Resources\miss.jpg";
        private string Dead = Environment.CurrentDirectory + @"\Resources\dead.jpg";
        private IShootService shootService;
        private IShootService compShootService;
        private Battle battle = new Battle();
        private Battle compBattle;

        #endregion

        public void NewGameToolStripMenuItem_Click(Object sender, EventArgs e) 
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите начать новую игру с компьютером?", "Новая игра с компьютером", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                TotalClearForNewGame();
                Chatter.UpdateGrouBoxText(groupBox1, "Осталось расположить кораблей:");
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Выберите способ расстановки кораблей");
                Chatter.GameWriteNotification(richTextBox2, "Вы начали новую игру с компьютером");
                Chatter.GameUpdateNotification(richTextBox2, "Расставьте корабли на вашем поле.\n");
                ControlEnabler1(true);
            }
        }

        private void TotalClearForNewGame()
        {
            dataGridView2.Enabled = false;
            groupBox1.Enabled = true;
            battle = new Battle();
            ClearRtb();
        }

        public void CellUpdate(DataGridView dataObj, System.Drawing.Point point, string bitmap)
        {
            if (dataObj.InvokeRequired)
            {
                dataObj.Invoke(new Action(delegate { dataObj[point.X, point.Y].Value = new Bitmap(bitmap); }));
            }
            else dataObj[point.X, point.Y].Value = new Bitmap(bitmap);
        }

        private void ClearRtb()
        {
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    System.Drawing.Point cell = new System.Drawing.Point(i, j);
                    CellUpdate(dataGridView1, cell, ShipClean);
                    CellUpdate(dataGridView2, cell, ShipClean);
                }
            }
        }

        private void ControlEnabler1(bool state)
        {
            if (groupBox1.InvokeRequired)
                groupBox1.Invoke(new Action(delegate { groupBox1.Visible = true; }));
            else groupBox1.Visible = true;

            if (chkBoxUser.InvokeRequired)
                chkBoxUser.Invoke(new Action(delegate { chkBoxUser.Enabled = state; }));
            else chkBoxUser.Enabled = state;

            if (chkBoxRandom.InvokeRequired)
                chkBoxRandom.Invoke(new Action(delegate { chkBoxRandom.Enabled = state; }));
            else chkBoxRandom.Enabled = state;

            if (button1.InvokeRequired)
                button1.Invoke(new Action(delegate { button1.Enabled = state; }));
            else button1.Enabled = state;

            if (dataGridView1.InvokeRequired)
                dataGridView1.Invoke(new Action(delegate { dataGridView1.Enabled = state; }));
            else dataGridView1.Enabled = state;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TotalClearForNewGame();
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Генерация случайного расположения");
            battle = RandomShips();

            foreach (var ship in battle.GetShips())
            {
                    dataGridView1[ship.GetXPoint(), ship.GetYPoint()].Value = new Bitmap(ShipCell);
            }
            button3.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Корабли расставлены");
            this.Cursor = Cursors.Default;
        }

        private Battle RandomShips()
        {
            return Battle.GenerateRandom();
        }

        private void chkBoxRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxRandom.Checked)
            {
                ControlEnablerRandom(true);
                chkBoxUser.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Случайное расположение кораблей");
            }
            else if (!chkBoxRandom.Checked)
            {
                ControlEnablerRandom(false);
                chkBoxUser.Checked = true;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Расположите корабли вручную");
            }

        }

        private void chkBoxUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxUser.Checked)
            {
                clearBtn_Click(sender, e);
                ControlEnablerRandom(false);
                chkBoxRandom.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Расположите корабли вручную");
            }
            else if (!chkBoxUser.Checked)
            {
                ControlEnablerRandom(true);
                chkBoxRandom.Checked = true;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Случайное расположение кораблей");
            }
        }

        private void ControlEnablerRandom(bool state)
        {
            button3.Enabled = state;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            compBattle = RandomShips();
            shootService = new ShootService(battle, compBattle);
            compShootService = new ShootService(compBattle, battle);
            Cursor = Cursors.WaitCursor;
            dataGridView2.Enabled = true;
            groupBox1.Enabled = false;
            Chatter.GameUpdateNotification(richTextBox1, "Игра началась, игрок стреляет первым");
            Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Ход игрока");

            Cursor = Cursors.Default;

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            TotalClearForNewGame();
        }


        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Model.Point startPoint = new Model.Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex);
                Ship ship = battle.CreateShip(startPoint);
                    dataGridView1[ship.GetXPoint(), ship.GetYPoint()].Value = new Bitmap(ShipCell);
                button2.Enabled = battle.IsAllShipAdded();
            } catch (Exception ex)
            {
                Chatter.Message(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model.Point shoot = new Model.Point(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex);
            try
            {
                bool isHit = shootService.Shoot(shoot);
                PrintHit(isHit, shoot, dataGridView2);
                if (!isHit)
                {
                    CompShoot();
                } else if (shootService.IsAllShipDead())
                {
                    Chatter.Message("Вы выиграли");
                }
            } catch(Exception ex)
            {
                Chatter.GameWriteNotification(richTextBox1, ex.Message);
            }
        }

        private void CompShoot()
        {
            bool isHit = true;
            while(isHit)
            {
                try
                {
                    Model.Point shoot = Model.Point.GenerateRandom();
                    isHit = compShootService.Shoot(shoot);
                    PrintHit(isHit, shoot, dataGridView1, true);
                }
                catch (Exception) { }
            }
            if (compShootService.IsAllShipDead())
            {
                Chatter.Message("Вы проиграли");
            }
        }

        private void PrintHit(bool isHit, Model.Point point, DataGridView view, bool isComp = false)
        {
            if (isHit)
            {
                CellUpdate(view, new System.Drawing.Point(point.X, point.Y), Dead);
            }
            else
            {
                Chatter.GameWriteNotification(richTextBox1, (isComp ? "Компьютер" : "Игрок") + " промахивается");
                Chatter.GameWriteNotification(richTextBox1, "Стреляет " + (isComp ? "игрок" : "компьютер"));
                CellUpdate(view, new System.Drawing.Point(point.X, point.Y), Miss);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Вы действительно хотите выйти из игры?";
            const string caption = "Выход из игры";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideSender("hide");
            for (int i = 1; i < 11; i++)
            {
                string[] myRow = { i.ToString() };
                dataGridView2.Rows.Add(myRow);
                dataGridView2.Rows[i - 1].Height = 30;
                dataGridView1.Rows.Add(myRow);
                dataGridView1.Rows[i - 1].Height = 30;
            }
            Chatter.GameWriteNotification(richTextBox2, "Добро пожаловать в игру Морской Бой");
            Chatter.GameWriteNotification(richTextBox2, "Начните новую игру с помощью меню");
        }

        private void HideSender(string state)
        {
            switch (state)
            {
                case "hide":
                    textBox1.Enabled = false;
                    button4.Enabled = false;
                    textBox1.Visible = false;
                    button4.Visible = false;
                    tabControl1.Height = 207;
                    break;
                case "show":
                    textBox1.Enabled = true;
                    button4.Enabled = true;
                    textBox1.Visible = true;
                    button4.Visible = true;
                    tabControl1.Height = 160;
                    break;
            }
        }


    }
}
