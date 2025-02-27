using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WF.AimTest
{
    public partial class Form1 : Form
    {
        // Local de
        // variaveis
        // propriedades

        // 2 Buttons
        private Button btnIniciar;
        private Button btnAlvo;

        // timer
        private System.Windows.Forms.Timer timer;

        // random
        private Random random;

        // Stopwatch
        private Stopwatch stopwatch;
       
        List<Color> cores = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Ivory, Color.Khaki};
        Random rand = new Random();



        // construtor da tela
        public Form1()
        {
            InitializeComponent();

            // determina o titulo da tela
            this.Text = "Aim Tester";
            // determina largura e altura
            this.Size = new Size(500, 500);
            // determina a posição inicial da tela -> nesse caso centralizada
            this.StartPosition = FormStartPosition.CenterParent;

            btnIniciar = new Button()
            {
                Text = "iniciar",
                Size = new Size(100, 50),
            }; 

             btnIniciar.Click += IniciarJogo;
            //adiciona o botão na tela;

            this.Controls.Add(btnIniciar);

            this.btnAlvo = new Button()
            {
                Size = new Size(50, 50),
                BackColor = Color.Red,
                Visible = false,
            };

            btnAlvo.Click += btnAlvoClick;
            // adiciona botao alvo na tela
            this.Controls.Add(btnAlvo);


            timer = new System.Windows.Forms.Timer();
            timer.Tick += MostrarBotaoAlvo;


            random = new Random();
            stopwatch = new Stopwatch();

            // fim construtor




        }

        // methods

        private void IniciarJogo(object sender, EventArgs e)
        {
            // desabilita o botaão 
            btnIniciar.Enabled = false;
            IniciarNovaRodada();
        }

        private void IniciarNovaRodada()
        {
            timer.Interval = 10;
            timer.Start();
        }

        private void MostrarBotaoAlvo(object sender,EventArgs e)
        {
            // para o timer
            timer.Stop();
            int x = random.Next(50, this.ClientSize.Width - 70);
            int y = random.Next(50, this.ClientSize.Height - 70);
            btnAlvo.Location = new Point(x, y);
            btnAlvo.Visible = true;
            // definir a cor aleatoria
            int aleatorio = random.Next(0, 4);
            btnAlvo.BackColor = cores.ElementAt(aleatorio);
            Thread.Sleep(10);
            IniciarNovaRodada();
        }
        private void SumirBotao (object sender, EventArgs e)
        {
            timer.Start();
            btnAlvo.Visible= false;
        }

        private void btnAlvoClick(object sender, EventArgs e)
        {
            if (btnAlvo.BackColor == Color.Blue)
            {
                stopwatch.Stop();
                btnAlvo.Visible = false;
                MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds}", "ms");
                Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
 
                
            }
        }

    }
}
