using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Calendar;
using Google.GData.Extensions;

namespace Core.GoogleCalendar
{
    public class Google日历助手
    {
        public const string 访问网址 = "http://www.google.com/calendar/feeds/default/allcalendars/full";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="用户名">日历登录用户名，通常为一个邮件地址</param>
        /// <param name="密码">密码</param>
        /// <param name="日历名称">为null则为默认的主日历</param>
        public Google日历助手(string 程序名称, string 用户名, string 密码, string 日历名称)
        {
            _日历服务 = new CalendarService(程序名称);
            日历服务.setUserCredentials(用户名, 密码);
            _操作日历名称 = 日历名称;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="用户名">日历登录用户名，通常为一个邮件地址</param>
        /// <param name="密码">密码</param>
        /// <param name="日历名称">为null则为默认的主日历</param>
        public Google日历助手(string 用户名, string 密码, string 日历名称)
            : this("CalendarHelper", 用户名, 密码, 日历名称)
        {
        }

        public string 操作日历名称
        {
            get
            {
                return _操作日历名称;
            }
        }
        private string _操作日历名称;



        public CalendarService 日历服务
        {
            get
            {
                return _日历服务;
            }
        }
        private CalendarService _日历服务;

        public EventEntry 创建活动(string 标题, string 说明, string 地点, DateTime 开始时间, DateTime 结束时间, Reminder.ReminderMethod 提醒方式, TimeSpan 提前提醒时间)
        {
            if (提醒方式 != Reminder.ReminderMethod.none && 提前提醒时间.TotalMinutes < 1) throw new Exception("提前提醒时间不得小于1分钟，因为低于分钟的单位将被忽略");

            var q = new EventEntry(标题, 说明, 地点);
            q.Times.Add(new When(开始时间, 结束时间));
            q.Reminder = new Reminder { Minutes = 提前提醒时间.Minutes, Days = 提前提醒时间.Days, Hours = 提前提醒时间.Hours, Method = Reminder.ReminderMethod.all };

            if (操作日历名称 == null)
            {
                return 日历服务.Insert(new Uri(访问网址), q) as EventEntry;
            }
            else
            {
                var query = new CalendarQuery(访问网址);
                CalendarEntry c = null;
                foreach (CalendarEntry f in 日历服务.Query(query).Entries)
                {
                    if (f.Title.Text == 操作日历名称) c = f;
                }
                return 日历服务.Insert(new Uri(c.Content.AbsoluteUri), q) as EventEntry;
            }
        }

        public EventEntry 创建活动(string 标题, string 说明, string 地点, DateTime 开始时间, TimeSpan 持续时间, Reminder.ReminderMethod 提醒方式, TimeSpan 提前提醒时间)
        {
            return 创建活动(标题, 说明, 地点, 开始时间, 开始时间.Add(持续时间), 提醒方式, 提前提醒时间);
        }
    }
}
