using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassagerApp.DAL.Models
{
    public class ChatsEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        [MaxLength(64)]
        public string UserId { get; set; }
        [MaxLength(64)]
        public string UserName { get; set; }
   }
}
