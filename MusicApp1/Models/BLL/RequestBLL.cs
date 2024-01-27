using Android.Views;
using Android.Widget;
using MusicApp1.Models.DAL;
using MusicApp1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Models.BLL
{
    class RequestBLL
    {
        RequestDAL requestDAL = new RequestDAL();

        public void CreateRequestTable()
        {
            requestDAL.CreateRequestTable();
        }
        public void AddRequest(RequestTable request)
        {
            requestDAL.AddRequest(request);
        }

        public void DeleteRequest(RequestTable request)
        {
            requestDAL.DeleteRequest(request);
        }

        public void UpdateRequest(RequestTable request)
        {
            requestDAL.UpdateRequest(request);
        }

        public RequestTable[] GetAllRequests()
        {
            return requestDAL.GetAllRequests().ToArray();
        }

        public RequestTable GetRequestByID(int id)
        {
            var data = requestDAL.GetAllRequests();
            var data11 = data.Where(x => x.requestId == id).FirstOrDefault();
            return data11;
        }

        public RequestTable[] GetRequestsByUser(int userID)
        {
            var data = requestDAL.GetAllRequests();
            var data11 = data.Where(x => x.loginIdFK == userID);
            return data11.ToArray();
        }
    }
}