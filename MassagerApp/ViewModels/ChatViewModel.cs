using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MassagerApp.PL.ViewModels
{
    public class ChatViewModel
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
