using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoiceRecorder.Audio;

namespace SpeechTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //private SpeechRecognizer speech = new SpeechRecognizer();

        public MainWindow()
        {
            InitializeComponent();

            //1
            /*
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak("Hello,world.my name is ok baba");
            */

            //2
            /*
            PromptBuilder prompt = new PromptBuilder();
            prompt.AppendText("Hello,world.my name is ok baba");
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(prompt);
            */

            /*
            speech.SpeechDetected += Speech_SpeechDetected;
            speech.SpeechHypothesized += Speech_SpeechHypothesized;
            speech.SpeechRecognized += Speech_SpeechRecognized;
            speech.SpeechRecognitionRejected += Speech_SpeechRecognitionRejected;
            button1.Focus();              //按钮获取输入焦点
            
            */



            SRE.SetInputToDefaultAudioDevice();
            GrammarBuilder GB = new GrammarBuilder();
            GB.Append("选择");
            GB.Append(new Choices(new string[] { "红色", "绿色" }));
            Grammar G = new Grammar(GB);
            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);
            SRE.LoadGrammar(G);
            SRE.RecognizeAsync(RecognizeMode.Multiple);


        }

        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine();
        


        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Text = e.Result.Text;
            switch (e.Result.Text)
            {
                case "选择红色":
                    MessageBox.Show("选择红色");
                    break;
                case "选择绿色":
                    MessageBox.Show("选择绿色");
                    break;
            }
        }


        /*


        private void Speech_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            MessageBox.Show("say2");
        }

        private void Speech_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            MessageBox.Show("say1" );
        }

        private void Speech_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            MessageBox.Show("say" + e.Result.Text);
        }

        private void Speech_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("say" + e.Result.Text);
        }

        public PromptBuilder BuildPB()                //建立并构建PromptBuilder 对象并返回此对象
        {
            PromptBuilder pb = new PromptBuilder();
            pb.StartVoice("大哥");                      //构建pb对象内容
            pb.AppendText("主人现在是北京时间");
            pb.AppendTextWithHint(DateTime.Now.ToString("HH:MM"), SayAs.Time24);
            pb.AppendBreak(new TimeSpan(0, 0, 4));
            pb.EndVoice();


            return pb;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            label2.Content = "现在是北京时间" + DateTime.Now.ToString("HH:MM") + "分";
            SpeechSynthesizer syn = new SpeechSynthesizer();
            syn.SpeakAsync(BuildPB());                 //通过调用SpeechSynthesizer对象的SpeakAsync（）方法，输出语音
            button1.Focus();

        }
        */
    }
}
