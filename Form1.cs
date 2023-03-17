using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using SpeechLib;
using Text_To_Speech.Assets;
using static System.Net.Mime.MediaTypeNames;

namespace Text_To_Speech
{
    public partial class Form1 : Form
    {
        //SpeechSynthesizer speechSynthesizerObj;
        /// <summary>
        /// Object giọng nói
        /// </summary>
        SpeechSynthesizer synthesizer;

        /// <summary>
        /// Cài đặt giọng nói
        /// </summary>
        SpeechConfig speechConfig;



        //ISpeechAudio speechAudio;
        StringBuilder ErrorContent;

        /// <summary>
        /// Danh sách Ngôn ngữ
        /// </summary>
        readonly List<string> LanguageList = new List<string>() { "English (United Kingdom)", "English (United States)", "Vietnamese (Vietnam)" };
        /// <summary>
        /// Kiểu đọc
        /// </summary>
        readonly List<string> SpeakingStyle = new List<string>() { "General","Assistant","Chat","Customer Service","Newscast","Angry","Sad",
               "Excited","Friendly","Terrified","Shouting","Unfriendly","Whispering","Hopeful" };
        /// <summary>
        /// Giọng tiếng anh- Anh
        /// </summary>
        readonly List<string> EnglishUK = new List<string>() {"en-GB-AbbiNeural","en-GB-AlfieNeural","en-GB-BellaNeural",
        "en-GB-ElliotNeural","en-GB-EthanNeural","en-GB-HollieNeural","en-GB-LibbyNeural","en-GB-MaisieNeural",
        "en-GB-NoahNeural","en-GB-OliverNeural","en-GB-OliviaNeural","en-GB-RyanNeural1","en-GB-SoniaNeural1","en-GB-ThomasNeural" };
        /// <summary>
        /// Giọng tiếng anh - Mỹ
        /// </summary>
        readonly List<string> EnglishUS = new List<string>() {"en-US-AmberNeural","en-US-AnaNeural","en-US-AriaNeural","en-US-AshleyNeural",
        "en-US-BrandonNeural","en-US-ChristopherNeural","en-US-CoraNeural","en-US-DavisNeural","en-US-ElizabethNeural","en-US-EricNeural","en-US-GuyNeural","en-US-JacobNeural","en-US-JaneNeural","en-US-JasonNeural","en-US-JennyMultilingualNeural3","en-US-JennyNeural","en-US-MichelleNeural","en-US-MonicaNeural","en-US-NancyNeural","en-US-RogerNeural1","en-US-SaraNeural","en-US-SteffanNeural","en-US-TonyNeural"};

        //"en-US-AIGenerate1Neural1","en-US-AIGenerate2Neural1",
        /// <summary>
        /// Giọng Việt Nam
        /// </summary>
        readonly List<string> Vietnamese = new List<string>() { "vi-VN-HoaiMyNeural", "vi-VN-NamMinhNeural" };

        string voicestyle = "";

        public Form1()
        {
            InitializeComponent();
            // These values should come from a config file
            string subscriptionKey = "1f3053fe2a644288989b165875bc1482";
            string region = "southeastasia";
            speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);

            ErrorContent = new StringBuilder();

            cmbCountry.Items.Clear();
            cmbCountry.Items.AddRange(LanguageList.ToArray());
            //cmbCountry.SelectedIndex = 0;

            cmbCountry.SelectedValueChanged += CmbCountry_SelectedValueChanged;


            //Startup            
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            //Test
            //var synth = new System.Speech.Synthesis.SpeechSynthesizer();
            //synth.Rate = (int)-10;

       
            //AudioConfig af = new AudioConfig;

            synthesizer = new SpeechSynthesizer(speechConfig);

            txtContent.Text = "Constructing the steel structure for the central Skylight";
        }

        private void Treeview_VoiceStyle_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (treeview_VoiceStyle.SelectedNode == null || e.Node.Text == "Voice list") return;

            //if (treeview_VoiceStyle.SelectedNode.Level == 1)
            //{
            //    string VoiceStyle = e.Node.Text;
            //    ErrorContent.Append(VoiceStyle);
            //    txtstatus.Text = ErrorContent.ToString();
            //}
        }

        private void Treeview_SpeakingStyle_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string SpeakingStyle = e.Node.Text;
            ErrorContent.Append(SpeakingStyle);
            txtstatus.Text = ErrorContent.ToString();
        }

        private void CmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            string seletedItem = cmbCountry.SelectedItem.ToString();
            if (string.IsNullOrEmpty(seletedItem)) { txtstatus.Text = "Chưa chọn Language"; return; }
            cmbVoice.Items.Clear();
            cmbSpeakStyle.Items.Clear();
            //treeview_SpeakingStyle.Nodes.Clear();

            switch (cmbCountry.SelectedIndex)
            {
                case 0://UK
                    {
                        cmbVoice.Items.AddRange(EnglishUK.ToArray());
                        cmbVoice.SelectedIndex = 0;
                        cmbSpeakStyle.Items.Add("General");
                        cmbSpeakStyle.SelectedIndex = 0;
                        break;
                    }
                case 1: //US
                    {
                        cmbVoice.Items.AddRange(EnglishUS.ToArray());
                        cmbVoice.SelectedIndex = 0;
                        cmbSpeakStyle.Items.AddRange(SpeakingStyle.ToArray());
                        cmbSpeakStyle.SelectedIndex = 0;
                        break;
                    }
                case 2: //VN
                    {
                        cmbVoice.Items.AddRange(Vietnamese.ToArray());
                        cmbVoice.SelectedIndex = 0;
                        cmbSpeakStyle.Items.Add("General");
                        cmbSpeakStyle.SelectedIndex = 0;
                        break;
                    }
                default:
                    {
                        cmbVoice.Items.AddRange(EnglishUS.ToArray());
                        cmbVoice.SelectedIndex = 0;
                        cmbSpeakStyle.Items.AddRange(SpeakingStyle.ToArray());
                        cmbSpeakStyle.SelectedIndex = 0;
                        break;
                    }

            }

        }


        //private void generateVoiceList(string voicestyle)
        //{
        //    treeview_VoiceStyle.Nodes.Clear();
        //    TreeNode main = new TreeNode("Voice list");
        //    string style = voicestyle ?? "English (United Kingdom)";
        //    switch (style)
        //    {
        //        case "English (United Kingdom)":
        //            {
        //                main.Nodes.Add(new TreeNode("en-GB-AbbiNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-AlfieNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-BellaNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-ElliotNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-EthanNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-HollieNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-LibbyNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-MaisieNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-NoahNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-OliverNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-OliviaNeural"));
        //                main.Nodes.Add(new TreeNode("en-GB-RyanNeural1)"));
        //                main.Nodes.Add(new TreeNode("en-GB-SoniaNeural1"));
        //                main.Nodes.Add(new TreeNode("en-GB-ThomasNeural"));
        //                break;
        //            }
        //        case "English (United States)":
        //            {
        //                main.Nodes.Add(new TreeNode("en-US-AIGenerate1Neural1"));
        //                main.Nodes.Add(new TreeNode("en-US-AIGenerate2Neural1"));
        //                main.Nodes.Add(new TreeNode("en-US-AmberNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-AnaNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-AriaNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-AshleyNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-BrandonNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-ChristopherNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-CoraNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-DavisNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-ElizabethNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-EricNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-GuyNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-JacobNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-JaneNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-JasonNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-JennyMultilingualNeural3"));
        //                main.Nodes.Add(new TreeNode("en-US-JennyNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-MichelleNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-MonicaNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-NancyNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-RogerNeural1"));
        //                main.Nodes.Add(new TreeNode("en-US-SaraNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-SteffanNeural"));
        //                main.Nodes.Add(new TreeNode("en-US-TonyNeural"));
        //                break;
        //            }
        //        case "VietNamese":
        //            {
        //                main.Nodes.Add(new TreeNode("vi-VN-HoaiMyNeural"));
        //                main.Nodes.Add(new TreeNode("vi-VN-NamMinhNeural"));
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //    treeview_VoiceStyle.Nodes.Add(main);
        //    treeview_VoiceStyle.CheckBoxes = true;
        //    treeview_VoiceStyle.ExpandAll();

        //}

        private async void btnSpeak_Click(object sender, EventArgs e)
        {
            if (txtContent.Text == null || string.IsNullOrEmpty(txtContent.Text) || txtContent.Text == "")
            {
                MessageBox.Show("Copy/Paste nội dung vào hộp thoại", "Thiếu nội dung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (cmbVoice.SelectedItem == null)
            {
                MessageBox.Show("Chọn ngôn ngữ và giọng đọc", "Thiếu nội dung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtContent.Text != "" && cmbVoice.SelectedItem != null)
            {
                voicestyle = cmbVoice.SelectedItem.ToString();
                speechConfig.SpeechSynthesisLanguage = cmbCountry.SelectedItem.ToString();
                speechConfig.SpeechSynthesisVoiceName = voicestyle;
                txtstatus.Text = "";
                using (synthesizer = new SpeechSynthesizer(speechConfig))
                {
                    if (btnSpeak.Text == "Speak")
                    {
                        btnSpeak.Text = "Stop";
                        btnSpeak.Enabled = false;

                        #region SSML
                        //string updown = trackBar_SpeakSpeed.Value >= 0 ? "+": trackBar_SpeakSpeed.Value.ToString();
                        //double value = trackBar_SpeakSpeed.Value/10;
                        string rate = $"{trackBar_SpeakSpeed.Value}0%";
                        
                        // Build an SSML prompt in a string.  
                        string CustomStyle = "";
                        CustomStyle += $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\"";
                        CustomStyle += $" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"en-US\">";
 
                        CustomStyle += $"<voice name=\"{cmbVoice.SelectedItem}\">";

                        CustomStyle += $"<mstts:express-as style=\"{cmbSpeakStyle.SelectedItem}\">";
                        
                        CustomStyle += $"<prosody rate=\"{rate}\">";
                        CustomStyle += $"{txtContent.Text}";
                        CustomStyle += $"</prosody>";
                        CustomStyle += $"</mstts:express-as>";
                        CustomStyle += $"</voice>";
                        CustomStyle += $"</speak>";


                        #endregion

                        //SpeechSynthesisResult result = await synthesizer.SpeakTextAsync(txtContent.Text);
                        SpeechSynthesisResult result = await synthesizer.SpeakSsmlAsync(CustomStyle);
                        ErrorContent.Clear();
                        txtstatus.Text = "";
                        switch (result.Reason)
                        {
                            case ResultReason.SynthesizingAudioCompleted:
                                ErrorContent.Append($"Speech resulted in status: {result.Reason}");
                                btnSpeak.Enabled = true;
                                btnSpeak.Text = "Speak";                                
                                break;
                            case ResultReason.Canceled:
                                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                                ErrorContent.Append($"CANCELED: Reason={cancellation.Reason}");
                                if (cancellation.Reason == CancellationReason.Error)
                                {
                                    ErrorContent.Append($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                    ErrorContent.Append($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                    MessageBox.Show($"{cancellation.ErrorDetails}","Speak Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                    //ErrorContent.Append($"CANCELED: Did you set the speech resource key and region values?");
                                    btnSpeak.Text = "Speak";
                                    btnSpeak.Enabled = true;
                                }
                                break;
                            default:
                                break;
                        }
                        txtstatus.Text = ErrorContent.ToString();
                    }
                    else if (btnSpeak.Text == "Stop")
                    {
                        btnSpeak.Text = "Speak";
                        var result = synthesizer.StopSpeakingAsync();
                        txtstatus.Text = $"Stop Speak {result.AsyncState}";

                    }
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Audio files(*.wav)| *.wav";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog(this))
            {

                string filename = saveFileDialog.FileName;
                txtstatus.Text = filename;

                voicestyle = cmbVoice.SelectedItem.ToString();
                speechConfig.SpeechSynthesisLanguage = cmbCountry.SelectedItem.ToString();
                speechConfig.SpeechSynthesisVoiceName = voicestyle;
                ErrorContent.Clear();


                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, AudioConfig.FromWavFileOutput(filename)))
                {
                    //
                    string rate = $"{trackBar_SpeakSpeed.Value}0%";

                    // Build an SSML prompt in a string.  
                    string CustomStyle = "";
                    CustomStyle += $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\"";
                    CustomStyle += $" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"en-US\">";

                    CustomStyle += $"<voice name=\"{cmbVoice.SelectedItem}\">";

                    CustomStyle += $"<mstts:express-as style=\"{cmbSpeakStyle.SelectedItem}\">";

                    CustomStyle += $"<prosody rate=\"{rate}\">";
                    CustomStyle += $"{txtContent.Text}";
                    CustomStyle += $"</prosody>";
                    CustomStyle += $"</mstts:express-as>";
                    CustomStyle += $"</voice>";
                    CustomStyle += $"</speak>";

                    //
                    //SpeechSynthesisResult result = await speechSynthesizer.SpeakTextAsync(txtContent.Text);
                    SpeechSynthesisResult result = await speechSynthesizer.SpeakSsmlAsync(CustomStyle);
                    switch (result.Reason)
                    {
                        case ResultReason.SynthesizingAudioCompleted:
                            ErrorContent.Append($"Speech resulted in status: {result.Reason}");                            
                            break;
                        case ResultReason.Canceled:
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            ErrorContent.Append($"CANCELED: Reason={cancellation.Reason}");
                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                ErrorContent.Append($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                ErrorContent.Append($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                ErrorContent.Append($"CANCELED: Did you set the speech resource key and region values?");
                            }
                            break;
                        default:
                            break;
                    }
                    txtstatus.Text = ErrorContent.ToString();                    
                }
            }

        }
    }
}
