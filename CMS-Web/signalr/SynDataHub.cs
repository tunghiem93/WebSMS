using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS_DTO.CMSSims;
using Microsoft.AspNet.SignalR;

namespace CMS_Web.signalr
{
    public class SynDataHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("Xin Chao");
        }

        public static void SycnDataSim(List<CMS_SimsModels> listData)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SynDataHub>();
            hubContext.Clients.All.sycnDataSim(listData);
        }

    }
}