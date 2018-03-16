using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace ConsoleTest
{
    class Program
    {

        Entities db = new Entities();
        static void Main(string[] args)
        {
            Console.WriteLine("--------Start---------");
            // new Program().AddTicket();
            for (int i = 0; i < 100; i++)
            {
                Action<int> a = new Action<int>(new Program().GetTicket);
                a.BeginInvoke(i, new AsyncCallback(t =>
                {
                    a.EndInvoke(t);
                    int j = 0;
                    var jd = "";
                    while (!t.IsCompleted)
                    {
                        if (j < 100)
                        {
                            jd += "*";
                            Console.WriteLine("进度{0}", jd);
                        }
                        if (j == 100)
                        {
                            Console.WriteLine("即将完成，请等待......");
                            j++;
                        }
                        Thread.Sleep(1000);
                    }
                }), null);
            }


            //  new Program().GetTicket("张三");
            Console.WriteLine("--------End---------");
            Console.ReadLine();
        }

        public static string Book(string BookName)
        {
            return BookName;
        }
        public static void BubbleSort()
        {
            int[] array = new int[] { 3, 2, 1 };
            int temp = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[i])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            foreach (int item in array)
            {
                Console.WriteLine(item);
            }

        }

        public static void GetValue()
        {
            int Num = 4;
            int Sum = 0;
            for (int i = 0; i < Num + 1; i++)
            {
                if ((i % 2) == 1)
                {
                    Sum += i;
                }
                else
                {
                    Sum = Sum - i;
                }
            }
            Console.WriteLine(Sum);
        }

        public void AddTicket()
        {
            Ticket t = new Ticket()
            {
                TicketName = "一等座",
                Departure = "上海",
                Destination = "重庆",
                Count = 1
            };
            db.Set<Ticket>().Add(t);
            db.SaveChanges();
        }
        public void GetTicket(int i)
        {
            try
            {
                Ticket ticket = db.Ticket.Where(w => w.Id == 1).AsNoTracking().FirstOrDefault();
                if (ticket.Count == 0)
                {
                    Console.WriteLine(i + "先生/女士,抱歉，票已卖完了。");
                    return;
                }
                ticket.Count = ticket.Count - 1;
                db.Entry<Ticket>(ticket).State = EntityState.Modified;
                User user = new User()
                {
                    Count = 1,
                    GetTime = DateTime.Now,
                    UserName = i + "先生/女士",
                    TicketId = ticket.Id
                };
                db.Set<User>().Add(user);
                db.SaveChanges();
                Console.WriteLine(i + "抢到票啦,库存剩余:" + ticket.Count + "张");
                //Console.WriteLine("当前托管线程标识符:" + Thread.CurrentThread.ManagedThreadId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = entry.GetDatabaseValues();
                var databaseValuesAsTicket = (Ticket)databaseValues.ToObject();
                Console.WriteLine(i + "未能抢到票,剩余" + databaseValuesAsTicket.Count + "张");
               // Console.WriteLine("当前托管线程标识符:" + Thread.CurrentThread.ManagedThreadId);
                //  throw;
            }

        }


    }
}
