using CefSharp;
using CefSharp.Example;
using CefSharp.WinForms;
using XCYN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.CefSharp
{
    public partial class InvokeJsForm : Form
    {
        public ChromiumWebBrowser browser;

        public InvokeJsForm()
        {
            InitializeComponent();

            // 初始化WebBrowser控件
            this.InitBrowser();
        }

        /// <summary>
        /// 初始化浏览器控件.
        /// </summary>
        public void InitBrowser()
        {
            var setting = new CefSettings();
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            Cef.Initialize(setting, true, false);
            // Cef.Initialize(new CefSettings());
            this.browser = new ChromiumWebBrowser(string.Format("{0}\\CefSharp\\html\\Test1.html", PathHelper.ApplicationPath));
            this.panel1.Controls.Add(this.browser);
            this.browser.Dock = DockStyle.Fill;
            this.browser.Name = "chromiumWebBrowser1";
            this.browser.IsBrowserInitializedChanged += (s, e) =>
            {
                this.browser.ShowDevTools();
            };

            // browser.RegisterJsObject("bound", new BoundObject());
            // browser.RegisterAsyncJsObject("boundAsync", new AsyncBoundObject());
            var eventObject = new ScriptedMethodsBoundObject();
            eventObject.EventArrived += OnJavascriptEventArrived;
            browser.RegisterJsObject("boundEvent", eventObject, camelCaseJavascriptNames: true);
        }

        private static void OnJavascriptEventArrived(string eventName, object eventData)
        {
            switch (eventName)
            {
                case "click":
                    {
                        var message = eventData.ToString();
                        var dataDictionary = eventData as Dictionary<string, object>;
                        if (dataDictionary != null)
                        {
                            var result = string.Join(", ", dataDictionary.Select(pair => pair.Key + "=" + pair.Value));
                            message = "event data: " + result;
                        }
                        MessageBox.Show(message, "Javascript event arrived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
        }

        private void InvokeJsForm_Load(object sender, EventArgs e)
        {
        }

        static InvokeJsForm()
        {
            if (CefSharpSettings.ShutdownOnExit)
            {
                Application.ApplicationExit += OnApplicationExit;
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        /// <summary>
        /// 获取源码.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var task = browser.GetSourceAsync();
            task.Wait();
            string content = task.Result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var zjz = @"/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAB+AGYDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9R3GenWhcrTsZzUioF61BgkGBgk0Kygde2aZcMkUUkkjiOJFLMxOAABya+A/2wP8Ago7Z+BZrjwp8PcXOrR5iuNZY5SJuhWNQCH4IO7IAyMZ5w1qW3Y+5PEXjrw74Vs3uda1vT9KgXq15dRxf+hEV8++Ov+CivwX8E3EllBrlzr12mcx6NZvOgx1/eHCEe6sR71+MXxB+KXiX4iay1/4i1a61a/fO64uZCzc9cdh+FclcX06W5iHzL7mtVEjmP2O0P/gql8OtS1JoH0jWI7bd/r3WLcB/u78fkxr6W+Ffx88GfGHTZLzw1qf2lI/vpIhjdeM8g+3pX869veyCEhuD9a774X/Hbxr8JJbiXw7rl1YpNndHEwUcjHpRyApH9EtnqUd4oeNsp61b3ds1+D2i/t6fGLT5I2TxnqPlIQ3kmQFTjsRjpX6Afsa/8FArH41zR+GfFzx6Z4pUfupUUpDcgDLFmY4VskAKOtZONiuY+4c0Um7png0tIoKKfRVlkCrtzSOG6jpTqduAXPYCp3IPkT/goz+0Ne/Bn4OvpOjTm213xB/okciOUkjgP+tdSOQcfLn/AGjzmvxbu9Qku7ouxy7HPHH8q+xP+CpfxFuPFn7RcmhpJ5ttoVlDbRjP3WkUTP8Aj+8VT/u187eBfh2NYSGa4i4bnOKmVRU1qbU6ftHY4eDQr7UJQ8UJce2as3Hhu9hyJLd1P+6a+tPA/wAKLK3KbY+n+zXoN18GNO1bIljyT/s5rgljbPRnf9S8j4BbQbk8iMke1I2j3AiK+UV/3hivveH9nHR7c5WI/wDfAqDUPgBobZEtspP94pzQsfHqyvqPkfAX2aWzbBGK2PDviC80DU7e/spPJvIZFljkB+6ykEH8CBX074z/AGc9Okjk+wpiTsdoFfOfjDwLe+C7+WC6GcH5DntXVTxUKrsjnrYb2cbn7UfsM/tNS/tD/DeR9YaMeI9NkEF15ZPzgglX5PcA/lX0yM96/Jr/AIJF655fxQ8Q6Zu+W60s3Tr/ALUUiKv6TNX6yjkV0yuedFjt1FN20VFzUY3SoZGkVSR93BqSRsCjiWIqfSnExkfgt+1y02oftWfEJ5h8za7PHz6B9v5YH5V3/wAOtIjjtLeIfdQYHFYv7Xuklf2v/HUIXltWLY9tqHP5Vr6TdahpV1ssrYzlDja2QK4MVserg+jPfPDGlIkanFd5ZW+1QRXgWn/ETxPZmMSaNDHFuGSrsTj8q9i8LeLY9Wtw0o8tj/D/APrr5+SPoFI6pWJ7VS1K1Ewbii91SCziL7jge1cFr3jfWHnKaZarMnYuSDWap3L5rFzVrPy2OK+Zv2hPDYv45LkLmRDgcdia9xk8Uaz5Z/tGxEZ7su41538UB/amgXE8IJbI6jHHOa9DCx5aiZxYpc1Nj/8Aglndf2T+05b2mdovNKu4T+AWT/2nX7J1+Qn/AATU8Oi6/aesrsLkW+lXc2fTIVc/+PY/Gv16WvpXLmPlUrMfRS0VFjUrcNwayvFHiTT/AAjol3qmoS+RaW6F5JMdFHJrTXJGa8E/bc8RW+l/AnVrKVsSXzwwKg/iBcFh/wB8g/lUzlyRbLw9L6xUjBdT89/2itU8M/FL9pqbxX4clebT9RtfNmaSExsJURYwCD7ID+JqlDrz+Hy8pSVwx6RKWOaydCsYYZI5Ys/Ku0bhjrXqXhnwzFqFj5xXMn0ryq1ZSSZ9JHC+xnKHZnKWPxain1JdNudOvlkcBt7RjABAOd27Hf1r0vQo4ftFuVdXSUbkZTkEVTXwdHBKGdNoHfFadhJbtrNtCjbvIGwfnXmSaex08tjoNYt/3bKBXneq/EzTfBF0ftzTqI22kxxFsH8K9YuwktxsNcr4k+Fen+ILuNpohJHIfMfK/wAQPFVBq+oOOlznV+JmleMFQW9wxEoyqyKUZh6hWwf0rnvGejiTQ71YBkiNj+ABr0uf4R6ZZxJcW8Ij8sYAC1g6zZRx28kWNzMCrLjhlPUVp7TlqabESjzU7Fz/AIJdaLDZeNPEOv3txbQiKxFhD5kyhyZHV8AE/wDTL9a/TJJFPHevyi+G/hWHwTHZmydoZ1u4ZXkUfMVVgSPyr9RfDt0dS021ut2/zE3bq9ilVUjw8RhfZLmZt0Uzd70V3HlcwxcBea+Xv28/DF1q/wAObC7tyzR2uoRtIR/ArKy5H4/L9HNfT7KG61z3xA8D2vxC8JXehXcrRQTgEsoBwQcj9cH8Kia500bYWs6FWM0fkc6rZzbeleoeAdU22SLnjiuj/aH/AGdovg7otvdnUZL1pJGQO8QU4G3GSDznJ9OleV+CtUkhjRWNfP1afKrH1f1lVqjkup65rl4w06aVOcc15zpHizTNP8U6db3UjpdX0bTp8vBUEA812iatBLbNFI2dwwRXLXml6eb63WY+ZDGMKjDheegrkhE6kuaVjtdQ+IGg/wDCTx6VBdhr5lZxGFJ4HXkcV6Hpsi3VvA6ZKEfNx3rzLT7fSp7iFo4owEG0HYCfz6iu/wBN1KG0tfJRqJLlZU1b3S/rmoRWdhJEjYryHVZjNebhzXX+JboyF8VxNvHJeanBbrjdI4Xk4HJxWUbznYznanTubGlWyyNE4/15dVUe5PFff/wpgnt/h/osd2u25W3AkX0OTXzl4F/Zt1+TxJYXF/DbxaZBKszESgsxVgQu30OK+t7a3W1iCKMAV9BQpOO542YYiFWEIxeyF20UtFenY+csQKu6l2EcY4pUbFKzA9aomx8//tp+CJvF3wiuJrOLzbuzlik2/wDTMN8x/Wvz60W4ij2ZbGa/XrUrG31axls7lQ8EylHQ9GUjBH5V+bP7RnwDl+FfiaR9KcSaa+XihUjeq5wDtHOODXm4im5LQ9PC1eWSRxV1dMYZHgOZQp21x8c2u30wa4kaI+gbj+Vafh/UjdNjd86nB5rtbTTIdQ/13f2rw9YOzPp6dTXmOOs7jX9PkU2sjXGOi7uD+lel+E9U1G6tQ+oxCCbP3QSa0dF8O2tjGDCOntS6tOLeQsTis5yuaylzSuTapcB1PNXfhL4UPi74g6db+X5kKyBpeM/KGBP6Vzlu02qXASLDK3qa+qf2W/A1pYQ3erjBv4W8psYONy88114SleSkedjK1oNH0YsKq27HzetSVVjldn54H1qwZAK+k2Pl73Db70U3zhRSuFhnl4oZadJw1OwGXHGa0RJXaJd2TXxP/wAFCLq+8Ka54U1vT2OZ7O4tJecZAZTg+x8w19d+LPH/AId8BadJfeIdYs9Ot4+GaadVI/AkV8E/tcftMeBfjk1hpfheafUY9LmZZLww7YZS2OEJOWAwDkDHTnrjnrSaib4eN5nyhH4wltb9pUgS1VjkohOK7TR/iQ77d0n61gar4Ogm3NGufwqnpfhhYZFVlx+FfPVHGWrPpqeisex6b8RysO3zf1qprHi6a/3eW278a5ax8NxtjatdJpvhnbj5K43Y6Cfw/wCJL+zCnpj3r7t/Y7un1DwHql1JzJLfkE+wjTA/U18WRaKkEO5lyfTFekeA/wBri1/Z78J3FpdeGrrV9Pa6Ess2nSgzx5UKQIyMMMLnO4V6GFqOMkjzsXT5k2j9AyoVsU4x9a+c/hB+3t8JPjFexadpmrXmnapJ92z1i1NvIfxyVz7bq+jUbcinIORnrX0V+ZXPnnHldiLyTRUysG5FFTYRzPjz4heH/hzoMmt+I9UttK06LG+S4lVMk9AMkZJxwOpr89f2jP8AgqfP9putI+F9nDDAMr/bt7GxkPukZxjOerdCOlfB/wAXPjt42+NWqNqXizXrrVLgZEfmkbYlJyVRQAFGewFcGIVktA56qMVoTI6n4gfGrxj8UNbN54o1+91i5kB3zXMu7r12r91R7KAK9L+Gq2/9k6Wtu27bGA3HfNeArGJG5rrfAHjm68M69BDt8+0H3oyf1HvXNiP4bO3C/EfY+j6WbqFVdau3HhFFm3Kv6Ung3WY9Y0/T7y3V0huI94WQAN174JrvPLDHmvmJbn0kTm9K0HYw+WuqtdI2x7ttTW8Kq3FbMX/HqaxkaHL6pbmO1YgVzNrokVw0pdA5lyGUj1Fd3qUYa1bNYe5NL025vdm8wxO+36An+lb0XqY1fhPj39orwxpfgnxnpt1pErWt+VaV/LUAq4YYI/IV9gfsa/8ABRhtLt7bwn8Trr7TZw26RWWrrEfNTHGyUdCMc7uOnSvz5+Knjq58beOtRv7gEL5p8pWP3FOOK5uG5d5CucbjnNfT4fWOp8zW+I/pC8K+NNC8YaamoaNqdpqFlMN8ctvKGDL60V+AXgv44+OfhhbyWvhjxNqOjRSf6wWdwYw+O5xRXbyo5j//2Q==";
            browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(
                string.Format("FillForm('张三', '{0}');", zjz));
        }
    }
}
