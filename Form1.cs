using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SpeechLib;

namespace Text_To_Speech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speechSynthesizerObj;
       
        ISpeechAudio speechAudio;
        public Form1()
        {
            InitializeComponent();
            speechSynthesizerObj = new SpeechSynthesizer();
            btnResume.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;

           

        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            //Disposes the SpeechSynthesizer object   
            speechSynthesizerObj.Dispose();
            if (txtContent.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                //Asynchronously speaks the contents present in RichTextBox1   
                speechSynthesizerObj.SpeakAsync(txtContent.Text);
                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }

            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Gets the current speaking state of the SpeechSynthesizer object.   
                if (speechSynthesizerObj.State == SynthesizerState.Speaking)
                {
                    //Pauses the SpeechSynthesizer object.   
                    speechSynthesizerObj.Pause();
                    btnResume.Enabled = true;
                    btnSpeak.Enabled = false;
                }
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                if (speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    //Resumes the SpeechSynthesizer object after it has been paused.   
                    speechSynthesizerObj.Resume();
                    btnResume.Enabled = false;
                    btnSpeak.Enabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Disposes the SpeechSynthesizer object   
                speechSynthesizerObj.Dispose();
                btnSpeak.Enabled = true;
                btnResume.Enabled = false;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }



    }
}
