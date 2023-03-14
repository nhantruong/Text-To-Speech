﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CognitiveServices.Speech;
using SpeechLib;
using Text_To_Speech.Assets;
using static System.Net.Mime.MediaTypeNames;

namespace Text_To_Speech
{
    public partial class Form1 : Form
    {
        //SpeechSynthesizer speechSynthesizerObj;
        SpeechConfig speechConfig;
        //ISpeechAudio speechAudio;
        StringBuilder style;
        public Form1()
        {
            // These values should come from a config file
            string subscriptionKey = "1f3053fe2a644288989b165875bc1482";
            string region = "southeastasia";

            InitializeComponent();
            speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);

            style = new StringBuilder();

            cmbCountry.Items.Clear();
            cmbCountry.Items.Add("English (United Kingdom)");
            cmbCountry.Items.Add("English (United States)");
            cmbCountry.SelectedIndex = 0;

            cmbCountry.SelectedValueChanged += CmbCountry_SelectedValueChanged;

            //Chọn style Speaking
            treeview_SpeakingStyle.NodeMouseClick += Treeview_SpeakingStyle_NodeMouseClick;
            //Chọn Voice
            treeview_VoiceStyle.NodeMouseClick += Treeview_VoiceStyle_NodeMouseClick;

        }

        private void Treeview_VoiceStyle_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeview_VoiceStyle.SelectedNode == null || e.Node.Text == "Voice list") return;

            if (treeview_VoiceStyle.SelectedNode.Level == 1)
            {
                string VoiceStyle = e.Node.Text;
                style.Append(VoiceStyle);
                txtstatus.Text = style.ToString();
            }
        }

        private void Treeview_SpeakingStyle_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string SpeakingStyle = e.Node.Text;
            style.Append(SpeakingStyle);
            txtstatus.Text = style.ToString();
        }

        private void CmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            string seletedItem = cmbCountry.SelectedItem.ToString();
            if (string.IsNullOrEmpty(seletedItem)) { txtstatus.Text = "Chưa chọn Language"; return; }

            treeview_SpeakingStyle.Nodes.Clear();
            if (seletedItem == "English (United States)")
            {
                treeview_SpeakingStyle.Nodes.Add("General");
                treeview_SpeakingStyle.Nodes.Add("Assistant");
                treeview_SpeakingStyle.Nodes.Add("Chat");
                treeview_SpeakingStyle.Nodes.Add("Customer Service");
                treeview_SpeakingStyle.Nodes.Add("Newscast");
                treeview_SpeakingStyle.Nodes.Add("Angry");
                treeview_SpeakingStyle.Nodes.Add("Sad");
                treeview_SpeakingStyle.Nodes.Add("Excited");
                treeview_SpeakingStyle.Nodes.Add("Friendly");
                treeview_SpeakingStyle.Nodes.Add("Terrified");
                treeview_SpeakingStyle.Nodes.Add("Shouting");
                treeview_SpeakingStyle.Nodes.Add("Unfriendly");
                treeview_SpeakingStyle.Nodes.Add("Whispering");
                treeview_SpeakingStyle.Nodes.Add("Hopeful");
            }
            else
            {
                treeview_SpeakingStyle.Nodes.Add("General");
            }
            generateVoiceList(seletedItem);
        }


        private void generateVoiceList(string voicestyle)
        {
            treeview_VoiceStyle.Nodes.Clear();
            TreeNode main = new TreeNode("Voice list");
            string style = voicestyle ?? "English (United Kingdom)";
            switch (style)
            {
                case "English (United Kingdom)":
                    {
                        main.Nodes.Add(new TreeNode("en-GB-AbbiNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-AlfieNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-BellaNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-ElliotNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-EthanNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-HollieNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-LibbyNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-MaisieNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-NoahNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-OliverNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-OliviaNeural"));
                        main.Nodes.Add(new TreeNode("en-GB-RyanNeural1)"));
                        main.Nodes.Add(new TreeNode("en-GB-SoniaNeural1"));
                        main.Nodes.Add(new TreeNode("en-GB-ThomasNeural"));
                        break;
                    }
                case "English (United States)":
                    {
                        main.Nodes.Add(new TreeNode("en-US-AIGenerate1Neural1"));
                        main.Nodes.Add(new TreeNode("en-US-AIGenerate2Neural1"));
                        main.Nodes.Add(new TreeNode("en-US-AmberNeural"));
                        main.Nodes.Add(new TreeNode("en-US-AnaNeural"));
                        main.Nodes.Add(new TreeNode("en-US-AriaNeural"));
                        main.Nodes.Add(new TreeNode("en-US-AshleyNeural"));
                        main.Nodes.Add(new TreeNode("en-US-BrandonNeural"));
                        main.Nodes.Add(new TreeNode("en-US-ChristopherNeural"));
                        main.Nodes.Add(new TreeNode("en-US-CoraNeural"));
                        main.Nodes.Add(new TreeNode("en-US-DavisNeural"));
                        main.Nodes.Add(new TreeNode("en-US-ElizabethNeural"));
                        main.Nodes.Add(new TreeNode("en-US-EricNeural"));
                        main.Nodes.Add(new TreeNode("en-US-GuyNeural"));
                        main.Nodes.Add(new TreeNode("en-US-JacobNeural"));
                        main.Nodes.Add(new TreeNode("en-US-JaneNeural"));
                        main.Nodes.Add(new TreeNode("en-US-JasonNeural"));
                        main.Nodes.Add(new TreeNode("en-US-JennyMultilingualNeural3"));
                        main.Nodes.Add(new TreeNode("en-US-JennyNeural"));
                        main.Nodes.Add(new TreeNode("en-US-MichelleNeural"));
                        main.Nodes.Add(new TreeNode("en-US-MonicaNeural"));
                        main.Nodes.Add(new TreeNode("en-US-NancyNeural"));
                        main.Nodes.Add(new TreeNode("en-US-RogerNeural1"));
                        main.Nodes.Add(new TreeNode("en-US-SaraNeural"));
                        main.Nodes.Add(new TreeNode("en-US-SteffanNeural"));
                        main.Nodes.Add(new TreeNode("en-US-TonyNeural"));
                        break;
                    }
                case "VietNamese":
                    {
                        main.Nodes.Add(new TreeNode("vi-VN-HoaiMyNeural"));
                        main.Nodes.Add(new TreeNode("vi-VN-NamMinhNeural"));
                        break;
                    }
                default:
                    break;
            }
            treeview_VoiceStyle.Nodes.Add(main);
            treeview_VoiceStyle.CheckBoxes = true;
            treeview_VoiceStyle.ExpandAll();

        }

        private async void btnSpeak_Click(object sender, EventArgs e)
        {
            if (txtContent.Text == null || string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("Copy/Paste nội dung vào hộp thoại", "Thiếu nội dung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer(speechConfig))
            {
                string langstyle = treeview_VoiceStyle.SelectedNode.Text;
                if (string.IsNullOrEmpty(langstyle))
                {
                    MessageBox.Show("Chọn giọng nói", "Giọng nói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                try
                {
                    //
                }
                catch (Exception)
                {

                    throw;
                }
                //string langstyle = treeview_VoiceStyle.SelectedNode.Text;
                //speechConfig.SpeechRecognitionLanguage = langstyle;
                speechConfig.SpeechSynthesisVoiceName = langstyle;
                //speechConfig.

                using (SpeechSynthesisResult result = await synthesizer.SpeakTextAsync(txtContent.Text))
                {
                    switch (result.Reason)
                    {
                        case ResultReason.SynthesizingAudioCompleted:
                            txtstatus.Text = $"Speech resulted in status: {result.Reason}";
                            break;
                        case ResultReason.Canceled:
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            txtstatus.Text = $"CANCELED: Reason={cancellation.Reason}";
                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                txtstatus.Text = $"CANCELED: ErrorCode={cancellation.ErrorCode}";
                                txtstatus.Text = $"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]";
                                txtstatus.Text = $"CANCELED: Did you set the speech resource key and region values?";
                            }
                            break;
                        default:
                            break;
                    }
                    //txtstatus.Text = $"Speech resulted in status: {result.Reason}";
                }
            }
        }        


    }
}
