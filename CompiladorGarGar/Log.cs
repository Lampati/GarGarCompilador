using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Threading;

namespace Utils
{
    internal class Log
    {
        // Fields
        private static TextWriter fileLog;
        private static FileStream fileStream;
        public static LogLevel logLevel = LogLevel.Debug;
        private static bool isOpen = false;
        private static object locker = new object();
        private static int counter = 0;
        public static string path;
        // Events
        public static event NewLogEntryHandler NewLogEntry;

        // Methods
        public static void AddDebug(string message)
        {
            if (logLevel >= LogLevel.Debug)
            {
                AddLine(message, "Dbg");
                OnNewLogEntry(LogType.Debug, message);
            }
        }

        public static void AddError(string message)
        {
            AddLine(message, "Err");
            OnNewLogEntry(LogType.Error, message);
        }

        public static void AddInformation(string message)
        {
            if (logLevel >= LogLevel.Info)
            {
                AddLine(message, "Inf");
                OnNewLogEntry(LogType.Info, message);
            }
        }

        private static void AddLine(string message, string type)
        {
            Interlocked.Increment(ref counter);

            lock (locker)
            {
                if (!isOpen)
                    Open();
            }

            string str = string.Format("{0}\t{1}\t{2}", DateTime.Now, type, message);
            fileLog.WriteLine(str);
            fileLog.Flush();

            lock (locker)
            {
                if (counter == 1)
                    Close();
            }

            Interlocked.Decrement(ref counter);
        }

        public static void Close()
        {
            fileLog.Close();
            isOpen = false;          
        }

        private static void OnNewLogEntry(LogType type, string message)
        {
            if (NewLogEntry != null)
            {
                NewLogEntry(type, message);
            }
        }

        public static void Open()
        {
            string logLocation = path+"\\log.txt";
            
            //RenameLog(directoryName, logLocation);
            fileStream = new FileStream(logLocation, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Close();

            fileLog = TextWriter.Synchronized(File.AppendText(logLocation));
            isOpen = true;
        }

        private static void RenameLog(string path, string logLocation)
        {
            if (File.Exists(logLocation))
            {
                DateTime creationTime = File.GetCreationTime(logLocation);
                DateTime now = DateTime.Now;
                if (((now.Year != creationTime.Year) || (now.Month != creationTime.Month)) || (now.Day != creationTime.Day))
                {
                    string[] files = Directory.GetFiles(path, "log-*.txt");
                    if (files.Length > 5)
                    {
                        ArrayList list = new ArrayList(files);
                        list.Sort();
                        while (list.Count > 5)
                        {
                            try
                            {
                                File.Delete((string)list[0]);
                            }
                            catch
                            {
                            }
                            list.RemoveAt(0);
                        }
                    }
                    string str = "log-" + creationTime.Year.ToString("0000") + "-" + creationTime.Month.ToString("00") + "-" + creationTime.Day.ToString("00") + ".txt";
                    try
                    {
                        File.Move(Path.Combine(path, "log.txt"), Path.Combine(path, str));
                    }
                    catch
                    {
                    }
                }
            }
        }

        // Nested Types
        public enum LogLevel
        {
            None,
            Info,
            Debug
        }

        public enum LogType
        {
            Info,
            Debug,
            Error
        }

        public delegate void NewLogEntryHandler(Log.LogType type, string message);
    } 
}
