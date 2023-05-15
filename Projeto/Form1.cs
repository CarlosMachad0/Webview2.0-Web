
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Application = System.Windows.Forms.Application;

namespace Projeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Inicializa o controle WebView2
            webView21.CreationProperties = new CoreWebView2CreationProperties();
            webView21.CreationProperties.UserDataFolder = Application.UserAppDataPath;
            webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            webView21.EnsureCoreWebView2Async();

            // Manipula o evento Click do botão "Ir"
            button1.Click += Button1_Click;

            // Manipula o evento Click do botão "Capturar tela"
            button2.Click += Button2_Click;
        }

        private void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            // Carrega a página da web padrão no WebView2
            webView21.CoreWebView2.Navigate("https://www.example.com");
            webView21.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;
        }

        private void CoreWebView2_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            // Exibe a URL da página carregada no TextBox
            textBox1.Text = webView21.CoreWebView2.Source;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // Verifica se a URL digitada é válida
            if (Uri.TryCreate(textBox1.Text, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                // Navega para a URL digitada no TextBox
                webView21.CoreWebView2.Navigate(textBox1.Text);
            }
            else
            {
                // Exibe uma mensagem de erro se a URL não for válida
                MessageBox.Show("URL inválida. Por favor, insira uma URL válida começando com 'http://' ou 'https://'.");
            }
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            // Captura um screenshot da WebView2 e salva em um arquivo
            using (var fileStream = File.Create(@"C:\DEVSCOPE\Projeto\Projeto\bin\Debug\testeImg.png"))
            {
                await webView21.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, fileStream);
            }
            MessageBox.Show("Captura de tela salva como 'testeImg.png'.");
        }
    }
}
