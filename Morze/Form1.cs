using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Morze
{
    public partial class Form1 : Form
    {
        //public System.Windows.Forms.Timer generalTimerObj = new System.Windows.Forms.Timer();
        //public System.Windows.Forms.Timer timerThreadObj = new System.Windows.Forms.Timer();
        public Thread StartThreadTimer = new Thread(ThreadTimerStart(timerThreadObj));//передать параметры нужно как-то. иначе нечего запускать ведь
        public Thread StopThreadTimer = new Thread(ThreadTimerStop);
        public Thread StartGeneralTimer = new Thread(GeneralTimerStart);
        public Thread StopGeneralTimer = new Thread(GeneralTimerStop);
        public int lengthOfThreadTimer = 0;
        string StringOfSignal { get; set; } = "";
        public Dictionary<string, char> ABC = new Dictionary<string, char>
        {
            { "01",'а' },
            { "1000",'б' },
            { "011",'в' },
            { "110",'г' },
            { "100",'д' },
            { "0",'е' },
            { "0001",'ж' },
            { "1100",'з' },
            { "00",'и' },
            { "0111",'й' },
            { "101",'к' },
            { "0100",'л' },
            { "11",'м' },
            { "10",'н' },
            { "111",'о' },
            { "0110",'п' },
            { "010",'р' },
            { "000",'с' },
            { "1",'т' },
            { "001",'у' },
            { "0010",'ф' },
            { "0000",'х' },
            { "1010",'ц' },
            { "1110",'ч' },
            { "1111",'ш' },
            { "1101",'щ' },
            { "1001",'ь' },
            { "1011",'ы' },
            { "00100",'э' },
            { "0011",'ю' },
            { "0101",'я' }
        };
        //public Task taskOfCountTime = new Task();
        public string direction = "";
        public Thread threadOfCountTime = new Thread(CountTime);
        public int lengthOfTime = 0;
        public Form1()
        {
            InitializeComponent();
            button1.BackColor = Color.Yellow;
            generalTimerObj = timer1;
            timerThreadObj = timerOfThread;
        }

        public static void CountTime(object timerObj)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)timerObj;
        }

        #region ThreadofTimers
        public static void ThreadTimerStart(object timerofThread)
        {
            var timer = (System.Windows.Forms.Timer)timerofThread;
            timer.Start();
        }

        public static void ThreadTimerStop(object timerofThread)
        {
            var timer = (System.Windows.Forms.Timer)timerofThread;
            timer.Stop();
        }

        public static void GeneralTimerStart(object generalTimer)
        {
            var timer = (System.Windows.Forms.Timer)generalTimer;
            timer.Start();
        }

        public static void GeneralTimerStop(object generalTimer)
        {
            var timer = (System.Windows.Forms.Timer)generalTimer;
            timer.Stop();
        }
        #endregion

        /// <summary>
        /// Метод который считывает окончание ввода, преобразует сигналы в букву, обнуляет данные
        /// </summary>
        /// <param name="timer1">Таймер</param>
        /// <param name="ofSignal">Строка сигналов: нулей и единиц</param>
        /// <param name="ABC">Словарь символов</param>
        /// <returns></returns>
        public static string WaitDirectionAndOutput(ref System.Windows.Forms.Timer timer1, string ofSignal, Dictionary<string, char> ABC)
        {
            int tempLengthTime = 0;
            timer1.Start();
            string result = "";

            while (true)
            {
                tempLengthTime += timer1.Interval;
                if (tempLengthTime > 3000)
                {
                    timer1.Stop();
                    foreach (var item in ABC)
                    {
                        if (item.Key == ofSignal)
                        {
                            result += item.Value;
                        }
                    }
                    break;
                }
            }
            return result;
        }

        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            StopThreadTimer.Start();
            StartGeneralTimer.Start();
            button1.BackColor = Color.Green;
        }

        private void Button1_MouseUp(object sender, MouseEventArgs e)
        {
            StopGeneralTimer.Start();
            button1.BackColor = Color.Red;
            if (lengthOfTime <= 1000)
            {
                //короткий сигнал 
                StringOfSignal += "0";
            }
            if (lengthOfTime > 1000)
            {
                //длинынй сигнал
                StringOfSignal += "1";
            }
            //метод для разборки слова и для нового начала
            StartThreadTimer.Start();
            //richTextBox1.Text+= WaitDirectionAndOutput(ref timer1, StringOfSignal,ABC);
            string result = "";

            while (true)
            {
                if (lengthOfThreadTimer > 3000)
                {
                    StopThreadTimer.Start();
                    foreach (var item in ABC)
                    {
                        if (item.Key == StringOfSignal)
                        {
                            result += item.Value;
                        }
                    }
                    break;
                }
            }
            lengthOfThreadTimer = 0;
            lengthOfTime = 0;
            button1.BackColor = Color.Yellow;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lengthOfTime += 100;
        }

        private void TimerOfThread_Tick(object sender, EventArgs e)
        {
            lengthOfThreadTimer += 100;
        }
    }
}