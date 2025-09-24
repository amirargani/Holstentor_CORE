using Holstentor.Data;
using Holstentor.Models.ProfileViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.ProfileRepository
{
    public class MessageRep : IDisposable
    {
        private ApplicationDbContext db = null;
        public MessageRep()
        {
            db = new ApplicationDbContext();
        }
        //public IList<MessageViewModel> GetMsgRecive(string username) // Inaktiv
        //{
        //    try
        //    {
        //        var quser = db.Users.Where(a => a.UserName.Equals(username)).FirstOrDefault().Id;
        //        if (quser == null)
        //            return null;
        //        else
        //        {
        //            //var qmsgrecive = (db.Tbl_Nachricht.Where(a => a.UserIdRecive.Equals(quser)).ToList()).OrderByDescending(a => a.Date);
        //            var qmsgrecive = db.Tbl_Nachricht.Where(a => a.UserIdRecive.Equals(quser)).ToList();
        //            if (qmsgrecive == null)
        //                return null;

        //            IList<MessageViewModel> lstmsg = new List<MessageViewModel>();
        //            foreach (var item in qmsgrecive)
        //            {
        //                MessageViewModel msg = new MessageViewModel();
        //                msg.Confirm = item.Confirm;
        //                msg.Date = item.Date;
        //                msg.ID = item.ID;
        //                msg.Text = item.Text;
        //                msg.TextAdmin = item.TextAdmin;
        //                msg.DateUpdate = item.DateUpdate;
        //                msg.UserIdRecive = item.UserIdRecive;
        //                msg.UserIdSend = item.UserIdSend;
        //                msg.UsernameSend = "Admin";
        //                lstmsg.Add(msg);
        //            }

        //            return lstmsg ?? null;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}
        public IList<MessageViewModel> GetMsgSend(string username)
        {
            try
            {
                var quser = db.Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser == null)
                    return null;
                else
                {
                    var qmsgsend = db.Tbl_Nachricht.Where(a => a.UserIdSend.Equals(quser.Id)).ToList();
                    if (qmsgsend == null)
                        return null;

                    IList<MessageViewModel> lstmsg = new List<MessageViewModel>();
                    foreach (var item in qmsgsend)
                    {
                        MessageViewModel msg = new MessageViewModel();
                        msg.Confirm = item.Confirm;
                        msg.Date = item.Date;
                        msg.ID = item.ID;
                        msg.Text = item.Text;
                        msg.TextAdmin = item.TextAdmin;
                        msg.DateUpdate = item.DateUpdate;
                        msg.UserIdRecive = item.UserIdRecive;
                        msg.UserIdSend = item.UserIdSend;
                        msg.UsernameSend = quser.NameFamily;
                        lstmsg.Add(msg);
                    }

                    return lstmsg ?? null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        ~MessageRep()
        {
            Dispose();
        }

        public void Dispose()
        {

        }
        public void Dispose(bool Dis)
        {
            if (Dis)
            {
                Dispose();
            }
        }
    }
}