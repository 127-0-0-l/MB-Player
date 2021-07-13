using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MB_Player
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //изменение цвета кнопок при наведении
            foreach (var pb in Controls.OfType<PictureBox>().Where(pb => pb.Tag.ToString() != "stat"))
            {
                pb.MouseEnter += (s, a) =>
                {
                    pb.BackColor = Color.FromArgb(238, 238, 238);
                };

                pb.MouseLeave += (s, a) =>
                {
                    pb.BackColor = Color.FromArgb(0, 146, 202);
                };
            }

            //прокрутка списка
            int point = 0;
            pbListSlider.MouseDown += (s, a) =>
            {
                point = a.Y;
            };
            pbListSlider.MouseMove += (s, a) =>
            {
                new Thread(() =>
                {
                    if (a.Button == MouseButtons.Left &&
                        pbListSlider.Top >= 3 &&
                        pbListSlider.Top <= pnlListBorder.Height - 17)
                    {
                        if (pbListSlider.Top + a.Y - point < 3)
                        {
                            pbListSlider.Top = 3;
                        }
                        else if (pbListSlider.Top + a.Y - point >= pnlListBorder.Height - 17)
                        {
                            pbListSlider.Top = pnlListBorder.Height - 17;
                        }
                        else
                        {
                            pbListSlider.Top += a.Y - point;
                        }
                    }
                }).Start();
            };
            
            //перетаскивание формы
            MouseDown += delegate
            {
                Capture = false;
                var msg = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref msg);
            };
        }

        //сворачивание окна
        private void pbMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //закрытие окна
        private void pbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //открыть папку
        private void pbOpen_Click(object sender, EventArgs e)
        {
            if (Controls.OfType<Button>().Count() == 5)
            {
                MessageBox.Show("Максимум 5 папок\nМожете удалить одну для освобождения места");
                return;
            }

            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            string[] filesPaths = null;
            try
            {
                filesPaths = Directory.GetFiles(folder.SelectedPath, "*.mp3");
            }
            catch
            {
                return;
            }

            //заполнение списка
            if (filesPaths.Length != 0)
            {
                //кнопка-вкладка
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Size = new Size(60, 20);
                button.Location = new Point(20 + Controls.OfType<Button>().Count() * 60, 270);
                button.BackgroundImage = Properties.Resources.folder;
                button.Text = new DirectoryInfo(folder.SelectedPath).Name;
                button.Font = new Font("JetBrains Mono", 8);
                button.ForeColor = Color.FromArgb(238, 238, 238);
                Controls.Add(button);

                //панель для кнопок с песнями
                foreach (var panel in pnlListBorder.Controls.OfType<Panel>().Where(pnl => pnl.Tag == "active"))
                {
                    panel.Tag = null;
                    panel.Hide();
                }
                Panel pnlList = new Panel();
                pnlList.Location = new Point(3, 3);
                pnlList.Size = new Size(337, 204);
                pnlList.BackColor = Color.FromArgb(57, 62, 70);
                pnlList.Tag = "active";
                pnlListBorder.Controls.Add(pnlList);

                //нажатие на кнопку-вкладку
                button.Click += (s, a) =>
                {
                    foreach (var panel in pnlListBorder.Controls.OfType<Panel>().Where(pnl => pnl.Tag == "active"))
                    {
                        panel.Tag = null;
                        panel.Hide();
                    }
                    pnlList.Tag = "active";
                    pnlList.Show();
                };

                //кнопки с песнями
                int yLocation = 0;
                foreach (var file in filesPaths)
                {
                    Button song = new Button();
                    song.FlatStyle = FlatStyle.Flat;
                    song.FlatAppearance.BorderSize = 0;
                    song.Size = new Size(pnlList.Width, 40);
                    song.Location = new Point(0, yLocation);
                    yLocation += 41;
                    song.TextAlign = ContentAlignment.MiddleLeft;
                    song.Text = Path.GetFileNameWithoutExtension(file);
                    song.BackColor = Color.FromArgb(57, 62, 70);
                    song.Font = new Font("JetBrains Mono", 9);
                    song.ForeColor = Color.FromArgb(238, 238, 238);
                    pnlList.Controls.Add(song);

                    //нажатие на кнопку из списка
                    song.Click += (s, a) =>
                    {

                    };
                }
            }
            else
                MessageBox.Show("Нет mp3-файлов");
        }

        //очистить список
        private void pbClear_Click(object sender, EventArgs e)
        {

        }

        private void pnlListBorder_Scroll(object sender, ScrollEventArgs e)
        {

        }

        //предыдущая песня
        private void pbBack_Click(object sender, EventArgs e)
        {

        }

        //запустить/остановить проигрывание
        private void pbStartStop_Click(object sender, EventArgs e)
        {

        }

        //следующая песня
        private void pbNext_Click(object sender, EventArgs e)
        {

        }

        //повтор одной песни
        private void pbRepeat_Click(object sender, EventArgs e)
        {

        }

        //перемешивание песен
        private void pbMix_Click(object sender, EventArgs e)
        {

        }

        //пред. тема
        private void pbThemeBack_Click(object sender, EventArgs e)
        {

        }

        //след. тема
        private void pbThemeNext_Click(object sender, EventArgs e)
        {

        }

        //пред. тема визуализатора
        private void pbVisualizerBack_Click(object sender, EventArgs e)
        {

        }

        //след. тема визуализатора
        private void pbVisualizerNext_Click(object sender, EventArgs e)
        {

        }
    }
}
