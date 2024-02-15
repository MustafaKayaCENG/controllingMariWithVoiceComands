using System;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace controllingMario
{
    class Program
    {
        public static void Main(string[] args)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

            try
            {
                recognizer.SetInputToDefaultAudioDevice();

                Choices commands = new Choices();
                commands.Add(new string[] { "jump", "move", "back" });

                GrammarBuilder grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(commands);

                Grammar grammar = new Grammar(grammarBuilder);

                recognizer.LoadGrammar(grammar);
                recognizer.RecognizeAsync();

                recognizer.SpeechRecognized += recognizer_SpeechRecognized;

                Console.WriteLine("I am started to listening. Press any key to shut down the program!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                recognizer.Dispose();
            }
        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized: " + e.Result.Text);

            if (e.Result.Text == "jump")
            {
                SendKeys.SendWait("{SPACE}");
            }
            else if (e.Result.Text == "move")
            {
                SendKeys.SendWait("{RIGHT}");
            }
            else if (e.Result.Text == "back")
            {
                SendKeys.SendWait("{LEFT}");
            }
        }
    }
}
