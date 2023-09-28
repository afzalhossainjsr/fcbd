using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Hubs
{
    public class ConnectedUserModel
    {
        public string? UserName { get; set; }
        public string? HubConnectionId { get; set; }
    }
    public class ConnectedUserViewModel
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? HubConnectionId { get; set; }
        public string? ConnectionTime { get; set; }

    }
}
