//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public Ticket()
        {
            this.User = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string TicketName { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public int Count { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual ICollection<User> User { get; set; }
    }
}
