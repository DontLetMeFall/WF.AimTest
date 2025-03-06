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
        private Label lblCont;

        // timer
        private System.Windows.Forms.Timer timer;

        // random
        private Random random;

        // Stopwatch
        private Stopwatch stopwatch;

        //contador de pontos
        private int cont;
        List<Color> cores = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Ivory, Color.Khaki};
        Random rand = new Random();

        List<double> placar = new List<double>();

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
                Size = new Size(100, 100),
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


            lblCont = new Label();
            lblCont.Text = "...";
            lblCont.Size = new Size(60, 180);
            lblCont.Location = new Point(50, 50);
            lblCont.Visible = true;
            this.Controls.Add(lblCont);
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
            timer.Interval = 600;
            timer.Start();
        }

        private void MostrarBotaoAlvo(object sender,EventArgs e)
        {
            // para o timer
            timer.Stop();
            stopwatch.Restart();
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
            stopwatch.Stop();
            btnAlvo.Size = new Size(btnAlvo.Size.Width - 10, btnAlvo.Size.Width - 10);
            string placarTexto = "";
            if (btnAlvo.BackColor == Color.Blue)
            {

                // adivciona no placar
                placar.Add(stopwatch.ElapsedMilliseconds);

                // se placar ja tem 5
                if (placar.Count() > 5)
                {
                    // remove a mais antiga
                    placar.RemoveAt(0);
                }

                foreach (double placarAtual in placar)
                {
                    placarTexto += $"{placarAtual} ms \n";
                }
                lblCont.Text = placarTexto;
                btnAlvo.Visible = false;
                // MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds}", "ms");
                Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                 TaskScheduler.FromCurrentSynchronizationContext());
               
            }
            else
            {
                stopwatch.Stop();
                placarTexto = "";
                lblCont.Text = placarTexto;
                MessageBox.Show($"Você Perdeu");
                timer.Start();

            }
        }

    }
}
