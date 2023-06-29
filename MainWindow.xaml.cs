using LitJson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using AIBox.ChatApi;
using System.Windows.Input;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace AIBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 在 MainWindow 类中添加以下变量
        private int badgeCount;

        private ObservableCollection<MessageItem> conversation;
        private ChatApi.ChatApi api = new ChatApi.ChatApi();

        public class MessageItem
        {
            public string UserMessage { get; set; }
            public string AgentMessage { get; set; }
        }

        public MainWindow()
        {

            InitializeComponent();
            badgeCount = 0;

            // 初始化对话集合
            conversation = new ObservableCollection<MessageItem>();
            ConversationListBox.ItemsSource = conversation;
            
        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // 最小化窗口
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取屏幕的宽度和高度
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // 获取任务栏的高度
            double taskbarHeight = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Height;

            // 设置窗口的高度为屏幕的高度减去任务栏高度
            this.Height = screenHeight - taskbarHeight;

            // 设置窗口的位置
            this.Left = screenWidth - this.Width;
            this.Top = (screenHeight - this.Height) / 2;

            this.WindowStyle = WindowStyle.None;
            DisableWindowDragging(this);
            this.Topmost = true; // 设置窗口始终显示在顶部
            this.Top = 0; // 设置窗口纵坐标为0，将窗口放置在屏幕的顶部
            this.Topmost = true; // 设置窗口始终显示在顶部
        }

        private void DisableWindowDragging(Window window)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            HwndSource.FromHwnd(hwnd)?.AddHook(WindowProc);
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            if (msg == WM_SYSCOMMAND && (int)wParam == SC_MOVE)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Send();
        }

        private void Send()
        {
            string userMessage = InputTextBox.Text;
            if (!string.IsNullOrEmpty(userMessage) | !string.IsNullOrWhiteSpace(userMessage))
            {
                if (userMessage == "清空对话" | userMessage == "clean")
                {
                    api = null;
                    api = new ChatApi.ChatApi();
                    conversation.Clear();
                    InputTextBox.Text = "";
                }
                else if (userMessage == "退出" | userMessage == "exit" | userMessage == "quit")
                {
                    MessageBoxResult result = MessageBox.Show("你确定要关闭AI助手吗？", "AIBox", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    InputTextBox.Text = "";
                    if (result == MessageBoxResult.OK)
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                }
                else if (userMessage == "帮助" | userMessage == "help")
                {
                    /// 创建消息项，包含用户对话和对方回复
                    MessageItem messageItem = new MessageItem
                    {
                        UserMessage = userMessage,
                        AgentMessage = @"[AIBox使用帮助]
    使用方法：
        1.对话：在下方输入框中输入想问的问题，点击发送按钮。
        2.发送文件：将文件直接拖入窗口内即可（建议10KB以下的小文件）。

    额定命令：
        1.退出AIBox：退出 | exit | quit
        2.清空聊天记录： 清空对话 | clean
        3.展示这条消息： 帮助 | help

    快捷键：
        1.最小化：Ctrl + S
        2.发送： Ctrl + Enter

    若出现错误，可到Github链接：https://github.com/DanKE123abc/AIBox 反馈"
                    };

                    // 添加消息项到对话集合
                    conversation.Add(messageItem);


                    // 清空输入框
                    InputTextBox.Text = "";

                    // 滚动到最新的对话
                    ConversationListBox.ScrollIntoView(conversation.LastOrDefault());

                    // 修改焦点以便于继续输入
                    InputTextBox.Focus();
                }
                else
                {


                    string agentMessage = api.Get(userMessage);

                    /// 创建消息项，包含用户对话和对方回复
                    MessageItem messageItem = new MessageItem
                    {
                        UserMessage = userMessage,
                        AgentMessage = agentMessage
                    };

                    // 添加消息项到对话集合
                    conversation.Add(messageItem);


                    // 清空输入框
                    InputTextBox.Text = "";

                    // 滚动到最新的对话
                    ConversationListBox.ScrollIntoView(conversation.LastOrDefault());

                    // 修改焦点以便于继续输入
                    InputTextBox.Focus();
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                if(this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Minimized;
                }
                
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
            {
                Send();
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    string filePath = file;
                    string fileContent = System.IO.File.ReadAllText(filePath);

                    //MessageBoxResult result = MessageBox.Show("确定要向AI发送[文件]"+filePath+"吗？", "AIBox", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    //if (result == MessageBoxResult.OK)
                    //{
                        string sendfile = @"" + filePath + "\n```\n" + fileContent + "\n```";
                        Send();

                        string userMessage = "\n[文件] "+filePath+"\n  ";
                        string agentMessage = api.Get(sendfile);
                        MessageItem messageItem = new MessageItem
                        {
                            UserMessage = userMessage,
                            AgentMessage = agentMessage
                        };

                        // 添加消息项到对话集合
                        conversation.Add(messageItem);

                        // 滚动到最新的对话
                        ConversationListBox.ScrollIntoView(conversation.LastOrDefault());

                        // 修改焦点以便于继续输入
                        InputTextBox.Focus();

                    //}
                }
            }
        }

    }


}
